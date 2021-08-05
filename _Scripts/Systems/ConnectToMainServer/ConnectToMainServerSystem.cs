using System;
using System.Collections;
using System.Collections.Generic;
using DBDL.CommonDLL;
using Kuhpik;
using UnityEngine;

public class ConnectToMainServerSystem : ExpectedWaitingGameSystem {
    private bool firstConnection = true;

    protected override void OnPrevGameSystemReady() {
        game.SetFbController(new FbController((success) => {
            if (success) {
                Printer.Print("ConnectToMainServer");
                ConnectToMainServer();
            } else {
                FullScreenMessage.Instance.ShowMessage("Error while connection to firebase");
            }
        }));
    }

    private void ConnectToMainServer() {
#if UNITY_EDITOR || UNITY_ANDROID
        game.ServersData = game.FbController.GetServersData();
#elif UNITY_STANDALONE_WIN
        game.ServersData = ServersData.GetDefault();
#endif
        game.SetMainServer(new MainServer(game.ServersData.MainServer));
      
        game.MainServer.OnConnected += OnConnected;
        game.MainServer.OnDisconnected += OnDisconnected;
        
        game.MainServer.Connect();
    }

    private void OnConnected() {
        Bootstrap.InvokeInMainThread(() => {
            FullScreenMessage.Instance.HideMessage();

            if (firstConnection) {
                firstConnection = false;
                Ready();
            }
        });
    }

    private void OnDisconnected() {
        Bootstrap.InvokeInMainThread(() => FullScreenMessage.Instance.ShowMessage("Waiting connection"));
    }

    private void OnDestroy() {
        game.MainServer.Disconnect();
    }
}