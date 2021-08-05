using System.Collections;
using System.Collections.Generic;
using GameServer;
using Kuhpik;
using UnityEngine;

public class MedkitsControllerSystem : GameSystem, IIniting {
    void IIniting.OnInit() {
        if (!game.IsCatcher) {
            foreach (var pair in game.StaticObjects) {
                if (pair.Value.GetType() == typeof(Medkit)) {
                    pair.Value.GetComponent<CollisionListener>().TriggerEnterEvent += OnMedkitEnter;
                } 
            }
        }
    }

    private void OnMedkitEnter(Transform medkitTransform, Transform myTransform) {
        TakeMedkitAction takeMedkitAction = new TakeMedkitAction();
        takeMedkitAction.OwnerId = game.MyPlayerId;
        takeMedkitAction.TargetId = medkitTransform.GetComponent<Medkit>().Id;
        
        game.GameServer.AppendAction(takeMedkitAction);
    }
}