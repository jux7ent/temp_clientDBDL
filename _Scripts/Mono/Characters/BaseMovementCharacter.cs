using GameServer;
using Kuhpik;
using Constants = Kuhpik.Constants;

public abstract class BaseMovementCharacter : BaseCharacter {
    protected float MovementSpeed;
    protected float AngularSpeed;

    private bool lastIsIdle = true;
    
    protected void Idle() {
        if (!lastIsIdle) {
            animator.SetTrigger(GameExtensions.GetAnimatorHashFromTag(Constants.AnimatorTags.Idle));
            lastIsIdle = true;
        }
    }

    protected void Walk() {
        if (lastIsIdle) {
            animator.SetTrigger(GameExtensions.GetAnimatorHashFromTag(Constants.AnimatorTags.Walk));
            lastIsIdle = false;
        }
    }

    public void UpdateSpeed(CharacterSettings characterSettings) {
        MovementSpeed = characterSettings.MovementSpeed;
        AngularSpeed = characterSettings.AngularSpeed;
    }
}
