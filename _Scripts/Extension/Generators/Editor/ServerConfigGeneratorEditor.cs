using UnityEditor;
using UnityEngine;  

[CustomEditor(typeof(ServerConfigGenerator))]
public class ServerConfigGeneratorEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        
        ServerConfigGenerator myTarget = (ServerConfigGenerator) target;

        if(GUILayout.Button("Generate Json")) {
            myTarget.GenerateJson();
        }
    }
}