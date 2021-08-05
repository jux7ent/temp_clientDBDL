using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DBDL.CommonDLL;
using GameServer;
using GameServer.ServerResponses;
using Kuhpik;
using Server;
using ServerConsole.CommonClientGameServer.Handlers;
using UnityEngine;

public class GameServerMessageHandler : BaseGameServerMessagesHandler {
    private readonly GameServerConnector _gameServer;
    private readonly GameData _gameData;

    public event Action AllPlayersReadyAction;
    public event Action<LobbyInfoSM> OnGetServerLobbyData;
    public event Action<InitWorldStateSM> OnGetServerSceneInitData;
    public event Action<ServerUpdateStateSM> OnGetServerSceneUpdateData;
    public event Action<int> OnTakeBot;
    public event Action<int> OnReleaseBot;

    public GameServerMessageHandler(GameServerConnector gameServerConnector, GameData gameData) {
        _gameServer = gameServerConnector;
        _gameData = gameData;
    }

    public void SendLoadingReady() {
        AllPlayersReadyAction?.Invoke();
    }

    public void HandleReconnection(ReconnectedResponse reconnectedResponse) {
        Bootstrap.InvokeInMainThread(() => {
            Printer.Print($"CURR STATE: {Bootstrap.GetCurrentGameState()}");
            _gameData.MyPlayerId = reconnectedResponse.PlayerId;

            Bootstrap.ChangeGameState(EGamestate.LoadingFromServer);
            Thread.Sleep(300);

            _gameData.LastLobbyInfoFromServer = reconnectedResponse.LobbyInfoSm;

            OnGetServerSceneInitData?.Invoke(reconnectedResponse.InitWorldStateSm);
            Thread.Sleep(300);

            OnGetServerSceneUpdateData?.Invoke(reconnectedResponse.ServerUpdateStateSm);

            _gameData.GameServer.StartSendingOutgoingsPackets(ENetworkClientState.UpdatePlayer);
        });
    }

    public override void Handle(ConnectedSM connectedSm) {
        if (_gameServer.NetworkState == ENetworkClientState.SayingHello && connectedSm.AssemblyClassesDict.Count > 20) {
            _gameServer.SetState(ENetworkClientState.Connected);
            _gameData.MyPlayerId = connectedSm.PlayerId;
            // CGSContext.RwAssemblyClassesDict = connectedSm.AssemblyClassesDict;
        } else {
            if (connectedSm.AssemblyClassesDict.Count <= 20) {
                Printer.Print("ConnectedSM.AssemblyClassesDict.Count <= 20");
            }
        }
    }

    public override void Handle(LobbyInfoSM lobbyInfoSm) {
        OnGetServerLobbyData?.Invoke(lobbyInfoSm);
    }

    public override void Handle(InitWorldStateSM initWorldStateSm) {
        _gameServer.SetState(ENetworkClientState.WorldInitialization);

        if (Bootstrap.GetCurrentGameState() == EGamestate.Lobby) {
            Bootstrap.InvokeInMainThread(() => { Bootstrap.ChangeGameState(EGamestate.LoadingFromServer); });

            Thread.Sleep(500);

            OnGetServerSceneInitData?.Invoke(initWorldStateSm);
        }
    }

    public override void Handle(ServerUpdateStateSM serverUpdateStateSm) {
        _gameServer.SetState(ENetworkClientState.UpdatePlayer);

        Bootstrap.InvokeInMainThread(() =>
            OnGetServerSceneUpdateData?.Invoke(serverUpdateStateSm));
    }

    public override async void Handle(LobbyStartCountdownSM lobbyStartCountdownSm) {
        for (int i = 0; i < 5; ++i) {
            Printer.Print($"Countdown: {5 - i}");
            await Task.Delay(1000);
        }
    }

    public override void Handle(TakeBotSM takeBotSm) {
        if (OnTakeBot != null) {
            OnTakeBot.Invoke(takeBotSm.BotId);
            _gameServer.SendBotTakenMessage(takeBotSm.BotId);
        }
    }

    public override void Handle(ReleaseBotSM releaseBotSm) {
        OnReleaseBot?.Invoke(releaseBotSm.BotId);
    }

    public override void Handle(ReconnectionInfo reconnectionInfo) {
        Printer.Print("RECONNECTION INFO WTFFFFFFFFFFFFFFFFFFFFFFFFFF");
    }

    /*public override void Handle(ReconnectionSM reconnectionSm) {
        Printer.Print($"ReconnectionSM received. current state {gameServer.NetworkState}");
        
        if (gameServer.NetworkState == ENetworkClientState.TryingReconnect) {
            gameData.MyPlayerId = reconnectionSm.PlayerId;
            CGSContext.RwAssemblyClassesDict = reconnectionSm.AssemblyClassesDict;

            if (reconnectionSm.AssemblyClassesDict.Count < 20) {
                Printer.PrintError("Reconnection. AssemblyClassesDict.Count < 20");
                return;
            }
            
            Bootstrap.ChangeGameState(EGamestate.LoadingFromServer);

            Thread.Sleep(1000);
            
            OnGetServerSceneInitData?.Invoke(reconnectionSm.InitWorldStateSm);
            OnGetServerSceneUpdateData?.Invoke(reconnectionSm.ServerUpdateStateSm);
            
            gameData.GameServer.MessagesHandler.SendLoadingReady();
            gameServer.SetState(ENetworkClientState.UpdatePlayer);
        }
    }*/
}