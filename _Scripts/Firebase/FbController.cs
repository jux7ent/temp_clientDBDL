using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DBDL.CommonDLL;
using Firebase;
using Firebase.RemoteConfig;
using Kuhpik;
using UnityEngine;

public class FbController {
    private object lockObj = new object();
    private FirebaseApp app;

    private bool inited = false;
    private FbRemoteConfig fbRemoteConfig;

    public FbController(Action<bool> onInit) {
#if UNITY_ANDROID || UNITY_EDITOR
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith((task) => {
            DependencyStatus status = task.Result;

            if (status == Firebase.DependencyStatus.Available) {
                app = FirebaseApp.DefaultInstance;
                fbRemoteConfig = new FbRemoteConfig(FirebaseRemoteConfig.DefaultInstance);
                inited = true;
            } else {
                Printer.Print($"Could not resolve all Firebase dependencies: {status}");
            }

            Bootstrap.InvokeInMainThread(() => onInit?.Invoke(status == Firebase.DependencyStatus.Available));
        });
#elif UNITY_STANDALONE_WIN
        fbRemoteConfig = new FbRemoteConfig();
        onInit?.Invoke(true);
#endif
    }

    public ServersData GetServersData() {
#if UNITY_EDITOR || UNITY_ANDROID
        return fbRemoteConfig.GetServersData();
#elif UNITY_STANDALONE_WIN
        return ServersData.GetDefault();
#endif
        //   return new IPEndPoint(IPAddress.Parse("127.0.0.1"), 63333);
    }
}