using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FOVCamera : MonoBehaviour {
    [SerializeField] private Projector fogProjector;

#if UNITY_EDITOR
    private void Update() {
        Camera camera = GetComponent<Camera>();
        camera.orthographic = true;
        camera.orthographicSize = fogProjector.orthographicSize;
    }
    #endif
}