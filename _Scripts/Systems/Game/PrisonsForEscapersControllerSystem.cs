using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Kuhpik;
using UnityEngine;
/*
public class PrisonsForEscapersControllerSystem : BaseWorldIconsControllerSystem {

    protected override List<BaseMonoWithHealth> InitWorldObjectsWithHealth() {
        List<PrisonForEscapers> campFires = FindObjectsOfType<PrisonForEscapers>().ToList();
        List<BaseMonoWithHealth> result = new List<BaseMonoWithHealth>();

        for (int i = 0; i < campFires.Count; ++i) {
            result.Add((BaseMonoWithHealth)campFires[i]);
        }

        return result;
    }
    
    protected override TweenerCore<int, int, NoOptions> InteractWithWorldObject(BaseMonoWithHealth baseMonoWithHealth) {
        if (game.playerCharacterType != ECharacterType.Catcher) {
            return baseMonoWithHealth.ChangeHealth(false, 5f, () => UpdateWorldIconIndicator(baseMonoWithHealth));
        }

        return null;
    }

}*/