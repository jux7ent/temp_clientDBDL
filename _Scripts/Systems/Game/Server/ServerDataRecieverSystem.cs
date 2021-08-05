using System;
using System.Collections.Generic;
using DBDL.CommonDLL;
using GameServer;
using Kuhpik;
using UnityEngine;

public class ServerDataRecieverSystem : GameSystem, IIniting {
    [SerializeField] private float pingMultiplier = 1.2f;

    private float lastReceivedDataTimestamp = 0f;

    private Dictionary<int, BasePlayerInfo> playerIdPlayerInfo = new Dictionary<int, BasePlayerInfo>();
    private Dictionary<int, Cage> cageIdStaticInfo = new Dictionary<int, Cage>();
    private Dictionary<int, CampFire> campFireIdStaticInfo = new Dictionary<int, CampFire>();
    private Dictionary<int, Gate> gateIdStaticInfo = new Dictionary<int, Gate>();
    private Hatch hatch;

    private PlayersUpdater playersUpdater = new PlayersUpdater();

    private ServerCommandsHandler serverCommandsHandler;

    void IIniting.OnInit() {
        game.GameServer.MessagesHandler.OnGetServerSceneUpdateData += UpdateWorld;
        FillIds();

        serverCommandsHandler = new ServerCommandsHandler(game, config);
    }

    private void FillIds() {
        foreach (var playerInfo in game.PlayersList) {
            playerIdPlayerInfo.Add(playerInfo.PlayerId, playerInfo);
        }

        Cage[] cages = FindObjectsOfType<Cage>();
        foreach (var cage in cages) {
            if (!cageIdStaticInfo.ContainsKey(cage.Id)) {
                cageIdStaticInfo.Add(cage.Id, cage);
            }
        }

        CampFire[] campFires = FindObjectsOfType<CampFire>();
        foreach (var campFire in campFires) {
            if (!campFireIdStaticInfo.ContainsKey(campFire.Id)) {
                campFireIdStaticInfo.Add(campFire.Id, campFire);
            }
        }

        Gate[] gates = FindObjectsOfType<Gate>();
        foreach (var gate in gates) {
            if (!gateIdStaticInfo.ContainsKey(gate.Id)) {
                gateIdStaticInfo.Add(gate.Id, gate);
            }
        }

        hatch = FindObjectOfType<Hatch>();
    }

    private void UpdateWorld(ServerUpdateStateSM serverWorldUpdate) {
        Bootstrap.InvokeInMainThread(() => {
           // Printer.Print($"Ping ms: {(Time.time - lastReceivedDataTimestamp) * 1000}");
            Ping.Value = (Time.time - lastReceivedDataTimestamp) * pingMultiplier;
            lastReceivedDataTimestamp = Time.time;

            UpdatePlayers(serverWorldUpdate);
            UpdateCages(serverWorldUpdate);
            UpdateCampFires(serverWorldUpdate);
            UpdateHatch(serverWorldUpdate);
            UpdateGates(serverWorldUpdate);
            HandleServerCommands(serverWorldUpdate);
            ClearHandledActions(serverWorldUpdate);
        });
    }

    private void ClearHandledActions(ServerUpdateStateSM serverWorldUpdate) {
        while (game.GameServer.PlayerUpdateCm.ActionsQueue.Count > 0) {
            if (game.GameServer.PlayerUpdateCm.ActionsQueue.queue.Peek().Id < serverWorldUpdate.LastHandledActionId) {
                game.GameServer.PlayerUpdateCm.ActionsQueue.Dequeue();
            } else {
                break;
            }
        }
    }

    private void HandleServerCommands(ServerUpdateStateSM serverWorldUpdate) {
        try {
            while (serverWorldUpdate.ServerCommandsQueue.Count > 0) {
                BaseSc serverCommand = serverWorldUpdate.ServerCommandsQueue.Dequeue();
                if (game.GameServer.PlayerUpdateCm.LastHandledServerCommandId < serverCommand.Id) {
                    serverCommand.Accept(serverCommandsHandler);
                    game.GameServer.PlayerUpdateCm.LastHandledServerCommandId = serverCommand.Id;
                }
            }
        }
        catch (Exception ex) {
            Debug.Log($"Error handle actions: {ex.Message}\n{ex.StackTrace}");
        }
    }

    private void UpdatePlayers(ServerUpdateStateSM serverWorldUpdate) {
        foreach (var pair in serverWorldUpdate.Players) {
            playerIdPlayerInfo[pair.Key].Accept(playersUpdater, pair.Value);
        }
    }

    private void UpdateCages(ServerUpdateStateSM serverWorldUpdate) {
        foreach (var pair in serverWorldUpdate.Cages) {
            cageIdStaticInfo[pair.Key].SetProgress(pair.Value.Progress01);
            cageIdStaticInfo[pair.Key].ContainsPlayerId = pair.Value.ContainsPlayerId;
        }
    }

    private void UpdateCampFires(ServerUpdateStateSM serverWorldUpdate) {
        foreach (var pair in serverWorldUpdate.CampFires) {
            campFireIdStaticInfo[pair.Key].SetProgress(pair.Value.Progress01);
        }
    }

    private void UpdateGates(ServerUpdateStateSM serverWorldUpdate) {
        foreach (var pair in serverWorldUpdate.Gates) {
            gateIdStaticInfo[pair.Key].SetProgress(pair.Value.Progress01);
        }
    }

    /*  private void MovePlayerInCage(CageStaticObjectState cage) {
          Transform playerTransform = playerIdPlayerInfo[cage.ContainsPlayerId].transform;
          playerTransform.DOKill();
          playerTransform.position = cageIdStaticInfo[cage.Id].transform.position;
          playerTransform.rotation = Quaternion.identity;
          
        //  playerIdPlayerInfo[cage.ContainsPlayerId].PlayAnimation(EActionType.Idle);
          playerIdPlayerInfo[cage.ContainsPlayerId].transform.parent =
              playerIdPlayerInfo[cage.ContainsPlayerId].ParentTransform;
      }*/

    private void UpdateHatch(ServerUpdateStateSM serverWorldUpdate) {
        hatch.SetProgress(serverWorldUpdate.Hatch.Progress01);
    }
}