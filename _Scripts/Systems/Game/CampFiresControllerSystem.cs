using System.Collections.Generic;
using System.Linq;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
/*
public class CampFiresControllerSystem : BaseWorldIconsControllerSystem {
    protected override List<BaseMonoWithHealth> InitWorldObjectsWithHealth() {
        List<CampFire> campFires = FindObjectsOfType<CampFire>().ToList();
        List<BaseMonoWithHealth> result = new List<BaseMonoWithHealth>();

        for (int i = 0; i < campFires.Count; ++i) {
            result.Add((BaseMonoWithHealth)campFires[i]);
        }

        return result;
    }
    
    protected override TweenerCore<int, int, NoOptions> InteractWithWorldObject(BaseMonoWithHealth baseMonoWithHealth) {
        if (game.playerCharacterType == ECharacterType.Catcher) {
            return baseMonoWithHealth.ChangeHealth(false, config.CatcherEscaperCampFireInteractionDurationSec[0], () => UpdateWorldIconIndicator(baseMonoWithHealth));
        } else {
            return baseMonoWithHealth.ChangeHealth(true, config.CatcherEscaperCampFireInteractionDurationSec[1], () => UpdateWorldIconIndicator(baseMonoWithHealth));
        }
    return null;
    }
}*/