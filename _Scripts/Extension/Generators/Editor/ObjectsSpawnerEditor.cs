using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectsSpawner))]
public class ObjectsSpawnerEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        
        ObjectsSpawner myTarget = (ObjectsSpawner) target;

        if(GUILayout.Button("Spawn")) {
            myTarget.Spawn();
        }
    }
}