using System.Collections;
using System.Collections.Generic;
using GameServer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EscaperStateIndicator : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI indicationTMP;
    [SerializeField] private TextMeshProUGUI nicknameTMP;
    [SerializeField] private Image dieIndicator;

    public void SetNickname(string nickname) {
        nicknameTMP.text = nickname;
    }

    public void UpdateState(EEscaperState escaperState) {
        switch (escaperState) {
            case EEscaperState.Free: {
                indicationTMP.text = "F";
                break;
            }
            case EEscaperState.Caged: {
                indicationTMP.text = "C";
                break;
            }
            case EEscaperState.ReceivedDamage: {
                indicationTMP.text = "I";
                break;
            }
            case EEscaperState.InBag: {
                indicationTMP.text = "B";
                break;
            }
            case EEscaperState.Died: {
                indicationTMP.text = "D";
                break;
            }
            case EEscaperState.Escaped: {
                indicationTMP.text = "E";
                break;
            }
        }
    }

    public void SetDieProgress(float dieProgress) {
        dieIndicator.fillAmount = Mathf.Clamp01(dieProgress);
    }
}
