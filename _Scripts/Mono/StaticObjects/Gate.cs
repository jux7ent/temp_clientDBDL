using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : BaseStaticObjectWithProgress {
    private void Start() {
        OnClearProgress();
    }

    public new void SetValues(int id) {
        base.SetValues(id);
        Progress01 = 1f;
    }

    protected override void OnFullProgress() {
    }

    protected override void OnClearProgress() {
    }
}