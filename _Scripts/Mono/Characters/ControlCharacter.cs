using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ControlCharacter : BaseMovementCharacter {
    private CharacterController characterController;

    private void Awake() {
        base.Awake();
        
        characterController = gameObject.GetComponent<CharacterController>();
    }

    public void Idle() {
        base.Idle();
    }

    public void Walk(Vector3 direction) {
        base.Walk();
        
        characterController.SimpleMove(direction * (MovementSpeed * Time.fixedDeltaTime));
        
        Quaternion toRotation = Quaternion.FromToRotation(GameExtensions.forwardVector, direction);
        toRotation = Quaternion.Lerp(transform.rotation, toRotation,
            AngularSpeed * Time.deltaTime);

        transform.rotation = toRotation;
    }
}