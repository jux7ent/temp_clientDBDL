using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgressIndicator : MonoBehaviour {
    [SerializeField] private Image innerImage;
    [SerializeField] [Range(0, 1)] private float progress01 = 0f;

    private void Awake() {
        innerImage.fillAmount = progress01;
    }

    public void SetProgress(float progress01) {
        if (Mathf.Abs(progress01 - this.progress01) > Mathf.Epsilon) {
            innerImage.DOFillAmount(progress01, 0.3f).SetEase(Ease.Linear);
            this.progress01 = progress01;
        }
    }
}
