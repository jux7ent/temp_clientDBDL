using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour {
    protected Animator animator;

    protected void Awake() {
        animator = GetComponent<Animator>();
    }
}