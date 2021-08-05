using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ServerConfig {
    public string Ip;
    public int Port;

    public static ServerConfig GetLocalServerConfig() {
        ServerConfig localServerConfig = new ServerConfig();
        localServerConfig.Ip = "127.0.0.1";
        localServerConfig.Port = 8080;

        return localServerConfig;
    }
}