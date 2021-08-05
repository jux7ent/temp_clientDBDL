using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DBDL.CommonDLL;
using DBDL.CommonDLL.UdpServerClient;
using DBDL.CommonDLL.UdpServerClient.Messages;
using GameServer.ServerResponses;
using GameServer.UdpRequestsResponses;
using Kuhpik;
using Server;
using UnityEngine;

public class GameServerResponsesHandler : BaseUdpResponsesHandler {
    private readonly GameData _gameData;
    private readonly GameServerConnector _gameServerConnector;

    public GameServerResponsesHandler(GameData gameData, GameServerConnector gameServerConnector) {
        _gameData = gameData;
        _gameServerConnector = gameServerConnector;
    }
    
    public override void Handle(ConnectedResponse connectedResponse) {
        if (_gameServerConnector.NetworkState == ENetworkClientState.SayingHello) {
            _gameServerConnector.SetState(ENetworkClientState.Connected);
            _gameData.MyPlayerId = connectedResponse.PlayerId;
            
            Bootstrap.ChangeGameState(EGamestate.Lobby);
            _gameData.GameServer.StartSendingOutgoingsPackets(ENetworkClientState.Connected);
        }
        
        Printer.Print($"Connected to game server.\nPlayerId: {connectedResponse.PlayerId}");
    }

    public override void Handle(ReconnectedResponse reconnectedResponse) {
        Printer.Print($"ReconnectionSM received. current state {_gameServerConnector.NetworkState}");
        Printer.Print($"ReconnectedResponse:\nplayerId:{reconnectedResponse.PlayerId}\nreconnectionAvailable:{reconnectedResponse.ReconnectAvailable}");
        
        if (_gameServerConnector.NetworkState == ENetworkClientState.TryingReconnect) {
            _gameData.GameServer.MessagesHandler.HandleReconnection(reconnectedResponse);
        }
    }
}