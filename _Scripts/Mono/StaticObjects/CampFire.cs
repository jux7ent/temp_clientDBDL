using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class CampFire : BaseStaticObjectWithProgress {
    [SerializeField] private GameObject torchFireObj;
    [SerializeField] private GameObject fovObj;
    [SerializeField] private GameObject interactableTriggerObj;
    [SerializeField] private bool enabledOnStart = false;
    

    protected override void OnFullProgress() {
        torchFireObj.SetActive(true);
        fovObj.SetActive(true);
        interactableTriggerObj.SetActive(true);
    }

    protected override void OnClearProgress() {
        torchFireObj.SetActive(false);
        fovObj.SetActive(false);
        interactableTriggerObj.SetActive(false);
    }
}