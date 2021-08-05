using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using DBDL.CommonDLL;
using Firebase.RemoteConfig;
using Kuhpik;
using UnityEngine;
using Newtonsoft.Json;

public class ServersData {
    [Serializable]
    private struct ServersListDataForJson {
        public string main_server;
        public string[] game_servers_list;
    }

    public IPEndPoint MainServer;
    public HashSet<IPEndPoint> GameServersSet = new HashSet<IPEndPoint>();

    public ServersData() { }

    public ServersData(string json) {
        ServersListDataForJson helper = JsonConvert.DeserializeObject<ServersListDataForJson>(json);

        MainServer = IpPortParser.Parse(helper.main_server);

        for (int i = 0; i < helper.game_servers_list.Length; ++i) {
            GameServersSet.Add(IpPortParser.Parse(helper.game_servers_list[i]));
        }
    }

    public static string GetDefaultSerialized() {
        return "{\"main_server\":\"157.245.129.95:63333\",\"game_servers_list\":[\"157.245.129.95:64444\"]}";
    }

    public static ServersData GetDefault() {
        return new ServersData(GetDefaultSerialized());
    }
}

public class FbRemoteConfig {
    private static class RemoteConfigKeys {
        public const string ServersData = "servers_data";
    }

    private FirebaseRemoteConfig config;

    public FbRemoteConfig(FirebaseRemoteConfig config) {
        this.config = config;
        LoadConfigDataAsync();
    }

    public FbRemoteConfig() { }

    public ServersData GetServersData() {
#if UNITY_EDITOR || UNITY_ANDROID
        foreach (var pair in config.AllValues) {
            Printer.Print($"VALUE: {pair.Key} {pair.Value.StringValue}");
        }

        return new ServersData(config.GetValue(RemoteConfigKeys.ServersData).StringValue);
#elif UNITY_STANDALONE_WIN
        return ServersData.GetDefault();
#endif
    }

    private void LoadConfigDataAsync() {
#if UNITY_EDITOR || UNITY_ANDROID
        config.FetchAndActivateAsync().ContinueWith(task => {
            Bootstrap.InvokeInMainThread(() => {
                if (task.IsFaulted || task.IsCanceled) {
                    Printer.Print($"Some error while fetching remote config");
                    return;
                }

                /*   Dictionary<string, object> configDefaults = new Dictionary<string, object>();
                   configDefaults.Add(RemoteConfigKeys.ServersData, ServersData.GetDefaultSerialized());
                   config.SetDefaultsAsync(configDefaults);*/
            });
        });
#elif UNITY_STANDALONE_WIN
#endif
    }
}