using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class CameraChasingSystem : GameSystem, IIniting, IUpdating {
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool setOffsetOnInit;

    private Transform playerTransform;

    void IIniting.OnInit() {
        StartCoroutine(GameExtensions.Coroutines.WaitWhile(() => game.Character == null, () => {
            playerTransform = game.Character.Transform;
        
            if (setOffsetOnInit) {
                offset = game.MainCameraTransform.position - playerTransform.position;
            }
        }));
        
    }

    void IUpdating.OnUpdate() {
        if (game.Character != null) {
            game.MainCameraTransform.position = playerTransform.position + offset;
        }
    }
    
}