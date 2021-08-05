using System.Collections;
using System.Collections.Generic;
using DBDL.CommonDLL;
using Kuhpik;
using UnityEngine;

public class SetupMainPlayerSystem : ExpectedWaitingGameSystem {
    protected override void OnPrevGameSystemReady() {
        int myPlayerId = game.MyPlayerId;

        int i;
        for (i = 0; i < game.PlayersList.Count; ++i) {
            if (game.PlayersList[i].PlayerId == myPlayerId) {
                game.SetupCharacter(game.PlayersList[i].gameObject);
                break;
            }
        }

        if (i == game.PlayersList.Count)
            Printer.PrintError(
                $"Hmm... game.PlayersList doesn't contain myPlayerId: {game.MyPlayerId}\nPlayers count: {game.PlayersList.Count}");

        Ready();
    }
}