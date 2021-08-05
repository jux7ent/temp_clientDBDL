using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshChildrenCombiner))]
public class MeshChildrenCombinerEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        
        MeshChildrenCombiner myTarget = (MeshChildrenCombiner) target;

        if(GUILayout.Button("Combine")) {
            myTarget.Combine();
        }
    }
}