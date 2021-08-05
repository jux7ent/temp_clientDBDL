using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldIconButton {
    public Button Button { get; private set; }
    public Image Image { get; private set; }
        
    public RectTransform RectTransform { get; private set; }

    public WorldIconButton(GameObject gameObject) {
        Button = gameObject.GetComponent<Button>();
        Image = gameObject.GetComponent<Image>();
        RectTransform = Image.rectTransform;
    }

    public void SetImageFill(float fillAmount) {
        Image.fillAmount = fillAmount;
    }
        
    private WorldIconButton() {}
}
