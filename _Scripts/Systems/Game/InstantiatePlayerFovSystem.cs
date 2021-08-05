using System.Collections;
using System.Collections.Generic;
using DBDL.CommonDLL;
using Kuhpik;
using UnityEngine;

public class InstantiatePlayerFovSystem : ExpectedWaitingGameSystem, IIniting {
    [SerializeField] private FieldOfView fovPrefab;

    private bool systemInited = false;

    void IIniting.OnInit() {
        if (!systemInited) {
            systemInited = true;
        }
    }

    private void InitPlayerWithFOV() {
        FieldOfView fov = Instantiate(fovPrefab, game.Character.Transform);
        fov.transform.localPosition = GameExtensions.zeroVector;

        DestroyImmediate(game.Character.Transform.GetComponent<FogCoverable>());
        
        Ready();
    }

    protected override void OnPrevGameSystemReady() {
        InitPlayerWithFOV();    
    }
}