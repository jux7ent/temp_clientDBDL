using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStaticObjectWithProgress : BaseStaticObject {
    public float Progress01 = 0f;

    public new void SetValues(int id) {
        base.SetValues(id);
        
        Progress01 = 0f;
        OnClearProgress();
    }
    
    public void SetProgress(float progress01) {
        if (Mathf.Abs(progress01) < Single.Epsilon && Mathf.Abs(Progress01) > Single.Epsilon) {
            OnClearProgress();
        } else if (Mathf.Abs(progress01 - 1) < Single.Epsilon && Mathf.Abs(Progress01 - 1) > Single.Epsilon) {
            OnFullProgress();
        }

        Progress01 = progress01;
    }

    protected abstract void OnFullProgress();
    protected abstract void OnClearProgress();
}