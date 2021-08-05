using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kuhpik;
using UnityEngine;

public class ShowHideFOVSystem : GameSystem, IIniting, IDisposing {
    [SerializeField] private float howOftenCheckVisibleTargetsSec = 0.1f;
    
    private List<FogCoverable> fogCoverables;
    private List<FieldOfView> fieldOfViews;

    private Coroutine showHideVisibleTargetsCoroutine;
    
    void IIniting.OnInit() {
        StartCoroutine(GameExtensions.Coroutines.WaitWhile(() => game.Character == null, () => {
            fogCoverables = FindObjectsOfType<FogCoverable>().ToList();
            fieldOfViews = FindObjectsOfType<FieldOfView>().ToList();

            showHideVisibleTargetsCoroutine = StartCoroutine(ShowHideVisibleTargets());
        }));
    }

    void IDisposing.OnDispose() {
        if (showHideVisibleTargetsCoroutine != null) {
            StopCoroutine(showHideVisibleTargetsCoroutine);
        }
    }

    private IEnumerator ShowHideVisibleTargets() {
        while (true) {
            for (int i = 0; i < fogCoverables.Count; ++i) {
                if (fogCoverables[i].enabled) {
                    bool visible = false;
                    for (int j = 0; j < fieldOfViews.Count; ++j) {
                        if (fieldOfViews[j].visibleTargets.Contains(fogCoverables[i].transform)) {
                            visible = true;
                            break;
                        }
                    }
                    fogCoverables[i].ObjForDisabling.SetActive(visible);
                }
            }

            yield return GameExtensions.GetWaitForSeconds(howOftenCheckVisibleTargetsSec);
        }
    }

}