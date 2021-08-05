using System.Threading.Tasks;
using DBDL.CommonDLL;
using GameServer;
using Kuhpik;
using UnityEngine;
using UnityEngine.SceneManagement;
using Constants = Kuhpik.Constants;

public class ServerCommandsHandler : BaseServerCommandsHandler {
    private GameData gameData;
    private GameConfig gameConfig;

    public ServerCommandsHandler(GameData gameData, GameConfig gameConfig) {
        this.gameData = gameData;
        this.gameConfig = gameConfig;
    }

    public override void Handle(ScHidePlayer hidePlayer) {
        BasePlayerInfo targetPlayer = gameData.PlayerIdInfoDict[hidePlayer.TargetId];

        if (!targetPlayer.MyControlled) {
            targetPlayer.gameObject.SetActive(!hidePlayer.Hidden);
        }
    }

    public override void Handle(ScInvokeAbility commandInvokeAbility) {
        BasePlayerInfo basePlayer = gameData.PlayerIdInfoDict[commandInvokeAbility.TargetId];
        basePlayer.PlayAnimation(Constants.AnimatorTags.TakeDamge);
    }

    public override void Handle(ScMovementPlayer commandMovementPlayer) {
        BasePlayerInfo basePlayerInfo = gameData.PlayerIdInfoDict[commandMovementPlayer.TargetId];
        basePlayerInfo.PlayAnimation(commandMovementPlayer.MovementStarted
            ? Constants.AnimatorTags.Walk
            : Constants.AnimatorTags.Idle);
    }

    public override void Handle(ScPlaceInCage commandPlaceInCage) {
        Cage cage = gameData.StaticObjects[commandPlaceInCage.TargetId] as Cage;
        BaseEscaperInfo baseEscaperInfo = gameData.PlayerIdInfoDict[cage.ContainsPlayerId] as BaseEscaperInfo;

        baseEscaperInfo.transform.parent = baseEscaperInfo.DefaultParent;
        baseEscaperInfo.transform.position = cage.transform.position;
    }

    public override void Handle(ScAttack commandAttack) {
        BaseCatcherInfo baseCatcherInfo = gameData.PlayerIdInfoDict[commandAttack.TargetId] as BaseCatcherInfo;
        baseCatcherInfo.PlayAnimation(Constants.AnimatorTags.Attack);
    }

    public override void Handle(ScStartInteraction commandStartInteraction) {
        BaseEscaperInfo baseEscaperInfo = gameData.PlayerIdInfoDict[commandStartInteraction.OwnerId] as BaseEscaperInfo;

        baseEscaperInfo.PlayAnimation(Constants.AnimatorTags.Stunned);
    }

    public override void Handle(ScTakeInBag commandTakeInBag) {
        BaseCatcherInfo baseCatcherInfo = gameData.PlayerIdInfoDict[commandTakeInBag.OwnerId] as BaseCatcherInfo;
        BaseEscaperInfo baseEscaperInfo = gameData.PlayerIdInfoDict[commandTakeInBag.TargetId] as BaseEscaperInfo;

        baseCatcherInfo.PlayAnimation(Constants.AnimatorTags.TakeDamge);
        baseEscaperInfo.transform.parent = baseCatcherInfo.transform;
    }

    public override void Handle(ScSpawnObject commandSpawnObject) {
        Debug.Log("ServerCommandSpawn object");
        GameObject prefab = gameConfig.PrefabsData.ServerSpawnObjects[commandSpawnObject.SpawnObjectType];

        GameObject spawnedObject = Object.Instantiate(prefab, commandSpawnObject.SpawnPosition,
            Quaternion.identity);

        Trap trap = spawnedObject.GetComponent<Trap>();
        trap.SetValues(commandSpawnObject.SpawnObjectId);
    }

    public override void Handle(ScDestroyObject commandDestroyObject) {
        Debug.Log("Destroy object");

        Trap[] traps = Object.FindObjectsOfType<Trap>();

        for (int i = 0; i < traps.Length; ++i) {
            if (traps[i].Id == commandDestroyObject.TargetId) {
                Object.Destroy(traps[i].gameObject);
                break;
            }
        }
    }

    public override void Handle(ScHideInGrass commandHideInGrass) {
        BasePlayerInfo targetPlayer = gameData.PlayerIdInfoDict[commandHideInGrass.TargetId];

        if (!targetPlayer.MyControlled) {
            targetPlayer.HideInGrass(commandHideInGrass.Hidden);
        }
    }

    public override void Handle(ScLostPlayer commandLostPlayer) {
        if (commandLostPlayer.TargetId == gameData.MyPlayerId) {
            Printer.Print("YOU LOOSER");
        }
    }

    public override void Handle(ScGatesInteractable commandGatesInteractable) {
        Printer.Print("All campfire fire. Gate is interactable");
    }

    public override async void Handle(ScEscaperEscaped commandEscaperEscaped) {
        if (commandEscaperEscaped.TargetId == gameData.MyPlayerId) {
            Printer.Print("YOU WIN");

            await Task.Delay(5000);
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public override void Handle(ScMedkitTaken commandMedkitTaken) {
        Medkit medkit = gameData.StaticObjects[commandMedkitTaken.TargetId] as Medkit;
        medkit.gameObject.SetActive(false);
    }

    public override void Handle(ScEscaperHealing commandEscaperHealing) {
        BaseEscaperInfo baseEscaperInfo = gameData.PlayerIdInfoDict[commandEscaperHealing.TargetId] as BaseEscaperInfo;

        baseEscaperInfo.PlayAnimation(commandEscaperHealing.HeadlingStarted
            ? Constants.AnimatorTags.TakeDamge
            : Constants.AnimatorTags.Idle);
    }

    public override void Handle(ScReceiveDamage commandReceiveDamage) {
        BaseEscaperInfo baseEscaperInfo = gameData.PlayerIdInfoDict[commandReceiveDamage.TargetId] as BaseEscaperInfo;
        baseEscaperInfo.PlayAnimation(Constants.AnimatorTags.TakeDamge);
    }

    public override void Handle(ScGateOpened commandGateOpened) {
        Printer.Print("Gate opened");
    }
}