using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameServer;
using Kuhpik;
using UnityEngine;
using Constants = Kuhpik.Constants;

public class WinZonesControllerSystem : GameSystem, IIniting, IDisposing {
    private CollisionListener[] winZoneCollisionListeners;

    void IIniting.OnInit() {
        winZoneCollisionListeners = GameObject.FindGameObjectsWithTag(Constants.Tags.WinZone)
            .Select(x => x.GetComponent<CollisionListener>()).ToArray();

        for (int i = 0; i < winZoneCollisionListeners.Length; ++i) {
            winZoneCollisionListeners[i].TriggerEnterEvent += OnEnterToWinZone;
        }
    }

    void IDisposing.OnDispose() {
        for (int i = 0; i < winZoneCollisionListeners.Length; ++i) {
            winZoneCollisionListeners[i].TriggerEnterEvent -= OnEnterToWinZone;
        }
    }

    private void OnEnterToWinZone(Transform zone, Transform obj) {
        game.GameServer.AppendAction(new EnterToWinZoneAction());
    }
}