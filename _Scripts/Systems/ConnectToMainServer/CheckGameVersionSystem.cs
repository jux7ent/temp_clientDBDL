using System.Collections;
using System.Collections.Generic;
using DBDL.CommonDLL;
using Kuhpik;
using UnityEngine;

public class CheckGameVersionSystem : ExpectedWaitingGameSystem {
    protected override void OnPrevGameSystemReady() {
        game.MainServer.GetGameVersion(OnGettingGameVersion);
    }

    private void OnGettingGameVersion(int gameVersion) {
        if (gameVersion != config.GameVersion) {
            FullScreenMessage.Instance.ShowMessage("Invalid game versions\nPlease update");
        } else {
            Ready();
        }
    }
}