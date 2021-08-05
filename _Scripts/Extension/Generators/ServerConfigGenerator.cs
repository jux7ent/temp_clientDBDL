using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ServerConfigGenerator : MonoBehaviour {
    [SerializeField] private ServerConfig serverConfig;
    
    private const string ServerInfoFilePath = "Assets/Resources/Server/serverConfig.json";

    public void GenerateJson() {
        StreamWriter writer = new StreamWriter(ServerInfoFilePath);
        writer.Write(JsonUtility.ToJson(serverConfig));
        writer.Close();
    }
}