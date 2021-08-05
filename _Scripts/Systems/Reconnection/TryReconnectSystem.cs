using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using DBDL.CommonDLL;
using GameServer.UdpRequestsResponses;
using Kuhpik;
using Server;
using UnityEngine;

public class TryReconnectSystem : GameSystem, IIniting {

    void IIniting.OnInit() {
        Printer.Print("Try Reconnect");
        ConnectAndStartQuickGame();
    }
    
    private void ConnectAndStartQuickGame() {
      //  FullScreenMessage.Instance.ShowMessage("Connecting...");
        InitServer(IpPortParser.Parse(player.LastConnectedGameServer));
    }
    

    private void InitServer(IPEndPoint endPoint) {
        game.MainServer.NotifyConnectionToGameServer(game.UserData.UserId, endPoint, game.IsCatcher, () => {
            game.SetServer(new GameServerConnector(endPoint, game));

            Printer.Print("Start reconnection to game server...");
            game.GameServer.Reconnect(game.UserData.UserId).ContinueWith((task) => {
                if (task.Result != null) {
                    (task.Result as BaseHandleableResponse).Accept(game.GameServer.ResponsesHandler);
                } else {
                    Printer.Print("Error while reconnection...");
                }             
            }); 
        });
    }
}