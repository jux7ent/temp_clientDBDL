using System.Collections;
using System.Collections.Generic;
using DBDL.CommonDLL;
using GameServer;
using Kuhpik;
using UnityEngine;

public class GrassHidingControllerSystem : GameSystem, IIniting {
    [SerializeField] private CollisionListener[] collisionListeners;
    [SerializeField] private Material grassDefaultMaterial;
    [SerializeField] private Material grassTransparentMaterial;

    private bool systemInitiated = false;
    
    void IIniting.OnInit() {
        if (!systemInitiated) {
            systemInitiated = true;

            for (int i = 0; i < collisionListeners.Length; ++i) {
                collisionListeners[i].TriggerEnterEvent += OnEnterToGrass;
                collisionListeners[i].TriggerExitEvent += OnExitFromGrass;
            }
        }
    }

    private void OnEnterToGrass(Transform owner, Transform other) {
        owner.GetComponent<MeshRenderer>().material = grassTransparentMaterial;

        HideInGrassAction hideInGrassAction = new HideInGrassAction(true);
        hideInGrassAction.OwnerId = game.MyPlayerId;
        
        game.GameServer.AppendAction(hideInGrassAction);
        game.VisionInHighGrass(true);
    }

    private void OnExitFromGrass(Transform owner, Transform other) {
        owner.GetComponent<MeshRenderer>().material = grassDefaultMaterial;
        
        HideInGrassAction hideInGrassAction = new HideInGrassAction(false);
        hideInGrassAction.OwnerId = game.MyPlayerId;
        
        game.GameServer.AppendAction(hideInGrassAction);
        game.VisionInHighGrass(false);
    }
}