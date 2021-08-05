using System;
using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using Server;
using TMPro;
using UnityEngine;

public class NetworkLogger : Singleton<NetworkLogger> {
    [SerializeField] private TextMeshProUGUI networkStateTMP;
    [SerializeField] private TextMeshProUGUI lastSentTMP;
    [SerializeField] private TextMeshProUGUI lastReceivedTMP;

    private float startMessagingTimestamp = 0f;
    private float currTimestamp = 0f;

    private void Start() {
        startMessagingTimestamp = Time.time;
    }

    private void Update() {
        currTimestamp = Time.time;
    }

    public void LogNetworkStateTMP(ENetworkClientState networkClientState) {
        Bootstrap.InvokeInMainThread(() => networkStateTMP.text = networkClientState.ToString());
    }

    public void LogLastSentTMP(string str) {
        Bootstrap.InvokeInMainThread(() => lastSentTMP.text = $"{(int)(currTimestamp - startMessagingTimestamp)} {str}");
    }

    public void LogLastReceivedTMP(string str) {
        Bootstrap.InvokeInMainThread(() => lastReceivedTMP.text = $"{(int)(currTimestamp - startMessagingTimestamp)} {str}");
    }
}