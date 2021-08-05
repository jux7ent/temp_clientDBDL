using System;
using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class Cage : BaseStaticObjectWithProgress {
    [SerializeField] private Animator prisonAnimator;

    public int ContainsPlayerId = -1;

    private void Start() {
        OnClearProgress();
    }

    public new void SetValues(int id) {
        base.SetValues(id);
        Progress01 = 1f;
    }

    protected override void OnFullProgress() {
        prisonAnimator.SetBool(GameExtensions.GetAnimatorHashFromTag(Constants.AnimatorTags.PrisonDoorOpen), true);
    }

    protected override void OnClearProgress() {
        prisonAnimator.SetBool(GameExtensions.GetAnimatorHashFromTag(Constants.AnimatorTags.PrisonDoorOpen), false);
    }
}