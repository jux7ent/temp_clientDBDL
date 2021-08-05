using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class StartTutorialSystem : GameSystem, IIniting {
    void IIniting.OnInit() {
        player.tutorialPassed = true;

        if (player.tutorialPassed) {
            Bootstrap.ChangeGameState(EGamestate.Loading);
        }
    }
}