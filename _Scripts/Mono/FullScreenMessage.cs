using DG.Tweening;
using Kuhpik;
using TMPro;
using UnityEngine;

public class FullScreenMessage : Singleton<FullScreenMessage> {
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI messageTMP;

    [SerializeField] private float fadeDurationSec = 0.1f;

    private new void Awake() {
        base.Awake();
        canvasGroup.alpha = 0f;
    }

    public void ShowMessage(string message) {
        canvasGroup.blocksRaycasts = true;
        messageTMP.text = message;

        canvasGroup.DOKill();
        canvasGroup.DOFade(1, fadeDurationSec);
    }

    public void HideMessage() {
        canvasGroup.blocksRaycasts = false; 
        canvasGroup.DOKill();
        canvasGroup.DOFade(0, fadeDurationSec);
    }
}