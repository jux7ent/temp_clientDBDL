using GameServer;
using Kuhpik;
using ServerConsole;
using UnityEngine;

public class PlayerControllerSystem : GameSystemWithScreen<GameUIScreen>, IIniting, IFixedUpdating {

    private bool prevStateIsMovement = false;

    private RWVector3 cachedPos = new RWVector3();
    private RWQuaternion cachedRot = new RWQuaternion();

    void IIniting.OnInit() {
    }

    void IFixedUpdating.OnFixedUpdate() {
        if (game.Character != null) {
            CharacterMovement();
        }
    }

    private void CharacterMovement() {
        if (!game.Character.CanDo.Contains(ECanDo.Move)) {
            return;
        }
        
        Vector2 joystickDirection = screen.Joystick.Direction;

        if (joystickDirection != Vector2.zero) {
            MoveAndRotateMainCharacter(joystickDirection);

            if (!prevStateIsMovement) {
                game.GameServer.AppendAction(GetMovementAction(true));
            }

            prevStateIsMovement = true;

        } else {
            if (prevStateIsMovement) {
                game.GameServer.AppendAction(GetMovementAction(false));
                game.Character.Control.Idle();
            }
            
            prevStateIsMovement = false;
        }
        
        UpdatePos(game.Character.Transform.position);
        UpdateRot(game.Character.Transform.rotation);
        game.GameServer.PlayerUpdateCm.LocationData.Update(cachedPos, cachedRot);
    }

    private MovementAction GetMovementAction(bool movementStarted) {
        MovementAction result = new MovementAction();
        result.OwnerId = game.MyPlayerId;
        result.MovementStarted = movementStarted;

        return result;
    }

    private void UpdatePos(Vector3 vector3) {
        cachedPos.X = vector3.x;
        cachedPos.Y = vector3.y;
        cachedPos.Z = vector3.z;
    }

    private void UpdateRot(Quaternion quaternion) {
        cachedRot.X = quaternion.x;
        cachedRot.Y = quaternion.y;
        cachedRot.Z = quaternion.z;
        cachedRot.W = quaternion.w;
    }

    private void MoveAndRotateMainCharacter(Vector2 joystickDirection) {
        Vector3 dir = Vector3.zero;
        dir.x = joystickDirection.x;
        dir.z = joystickDirection.y;

        game.Character.Control.Walk(dir.normalized);
    }
}