using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Kuhpik;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class AttachForProjectorToPlayerSystem : GameSystem, IUpdating {
    [SerializeField] private Projector fogProjector;
    [SerializeField] private Vector3 localFogProjectorPos = Vector3.up * 10f;

    void IUpdating.OnUpdate() {
        //fogProjectorTransform.position = game.playerCharacter.transform.position + localFogProjectorPos;
     //   CheckInBounds();
    }
}