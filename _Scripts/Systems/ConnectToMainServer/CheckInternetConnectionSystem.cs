using System.Collections;
using System.Collections.Generic;
using DBDL.CommonDLL;
using Kuhpik;
using UnityEngine;

public class CheckInternetConnectionSystem : ExpectedGameSystem, IIniting {

    private bool systemInited = false;

    void IIniting.OnInit() {
        if (!systemInited) {
            Printer.OnLog += Debug.Log;
            Printer.OnLogError += Debug.LogError;
            
            systemInited = true;
        }
        
        FullScreenMessage.Instance.ShowMessage("Waiting internet connection...");
        StartCoroutine(GameExtensions.CheckInternetConnection(OnInternetConnection));
    }

    private void OnInternetConnection(bool thereIsConnection) {
        if (thereIsConnection) {
            FullScreenMessage.Instance.HideMessage();
            Ready();
        }
        
        Printer.Print($"Connection: {thereIsConnection}");
    }
}