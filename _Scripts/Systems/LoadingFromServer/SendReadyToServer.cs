using System.Collections;
using System.Collections.Generic;
using DBDL.CommonDLL;
using Kuhpik;
using Server;
using UnityEngine;

public class SendReadyToServer : ExpectedWaitingGameSystem {
    protected override void OnPrevGameSystemReady() {
        game.GameServer.MessagesHandler.AllPlayersReadyAction += OnAllPlayersReady;
        game.GameServer.SendLoadingReady();
        
        Printer.Print("SendREADY TO SERVER ON PLAYERS READ");
    }

    private void OnAllPlayersReady() {
        Bootstrap.ChangeGameState(EGamestate.Game);
    }
}