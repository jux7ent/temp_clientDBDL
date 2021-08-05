using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class WaitAndSwitchToTutorialSystem : WaitingGameSystem {
    protected override void OnPrevGameSystemReady() {
        Bootstrap.ChangeGameState(EGamestate.Tutorial);
    }
}