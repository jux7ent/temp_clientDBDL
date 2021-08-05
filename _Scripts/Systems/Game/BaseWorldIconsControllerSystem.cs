using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Kuhpik;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
/*
public abstract class BaseWorldIconsControllerSystem : GameSystem, IIniting, IUpdating {
    [SerializeField] private Button worldIconBtnPrefab;
    [SerializeField] private RectTransform dynamicCanvasRectTransform;
    [SerializeField] private Vector2 uiIconOffset;

    private List<BaseMonoWithHealth> worldObjectsWithHealths;
    private List<WorldIconButton> worldIconButtons = new List<WorldIconButton>();

    // for moving world icons every frame
    private List<KeyValuePair<Transform, RectTransform>> worldObjectTransformAndIcon =
        new List<KeyValuePair<Transform, RectTransform>>();

    private Dictionary<RectTransform, WorldIconButton> worldObjectTransformUIDict = new Dictionary<RectTransform, WorldIconButton>();
    
    private Dictionary<Transform, RectTransform> worldObjectTransformAndIconDict =
        new Dictionary<Transform, RectTransform>();

    private TweenerCore<int, int, NoOptions> healthChangingTween;
    
    private Vector2 normalizedForScreenOffset;
    private Vector2 dynamicCanvasSizeDelta;

    private bool systemInited = false;
    
    void IIniting.OnInit() {
        if (!systemInited) {
            Actions.MainCharacterStartMovement += StopAllCoroutinesAndTweensForThisSystem;
            Actions.WorldObjectStateChanged += OnWorldObjectStateChanged;
            
            systemInited = true;
        }

        dynamicCanvasSizeDelta = dynamicCanvasRectTransform.sizeDelta;
        
        normalizedForScreenOffset =
            GameExtensions.UI.NormalizeResolutionVector2(uiIconOffset, new Vector2(1920f, 1080f), dynamicCanvasSizeDelta);
        
        InitNeededCampFireInfosButtons();
    }

    void IUpdating.OnUpdate() {
        UpdateWorldObjectIconsOnPositions();
    }

    private void StopAllCoroutinesAndTweensForThisSystem() {
        StopAllCoroutines();
        healthChangingTween.Kill();
        game.ControlCharacter.transform.DOKill();
    }
    
    private void UpdateWorldObjectIconsOnPositions() {
        for (int i = 0; i < worldObjectTransformAndIcon.Count; ++i) {
            worldObjectTransformAndIcon[i].Value.position =
                WorldToCanvasPosition(worldObjectTransformAndIcon[i].Key.position) + normalizedForScreenOffset;
        }
    }

    private void OnWorldObjectStateChanged(BaseMonoWithHealth baseMonoWithHealth) {
        if (!worldObjectTransformAndIconDict.ContainsKey(baseMonoWithHealth.transform)) {
            return;
        }

        if (baseMonoWithHealth.Enabled(game.playerCharacterType)) {
            worldObjectTransformAndIconDict[baseMonoWithHealth.transform].localScale = Vector3.one;
            worldObjectTransformUIDict[worldObjectTransformAndIconDict[baseMonoWithHealth.transform]].SetImageFill(1f);
        } else {
            worldObjectTransformAndIconDict[baseMonoWithHealth.transform].localScale = Vector3.zero;
            worldObjectTransformUIDict[worldObjectTransformAndIconDict[baseMonoWithHealth.transform]].SetImageFill(0f);
        }
    }

    private Vector2 WorldToCanvasPosition(Vector3 worldPoint) {
        return Vector2.Scale(game.MainCamera.WorldToViewportPoint(worldPoint), dynamicCanvasSizeDelta);
    }

    private void InitNeededCampFireInfosButtons() {
        worldObjectTransformAndIcon.Clear();
        worldObjectTransformAndIconDict.Clear();
        worldObjectTransformUIDict.Clear();

        worldObjectsWithHealths = InitWorldObjectsWithHealth();

        // init buttons
        int neededForInit = worldObjectsWithHealths.Count - worldIconButtons.Count;

        for (int i = 0; i < neededForInit; ++i) {
            worldIconButtons.Add(new WorldIconButton(Instantiate(worldIconBtnPrefab, dynamicCanvasRectTransform).gameObject));
        }

        for (int i = 0; i < worldObjectsWithHealths.Count; ++i) {
            worldObjectTransformUIDict.Add(worldIconButtons[i].RectTransform, worldIconButtons[i]);
            worldObjectTransformAndIconDict.Add(worldObjectsWithHealths[i].transform, worldIconButtons[i].RectTransform);
            worldObjectTransformAndIcon.Add(new KeyValuePair<Transform, RectTransform>(worldObjectsWithHealths[i].transform, worldIconButtons[i].RectTransform));
            worldObjectTransformAndIconDict[worldObjectsWithHealths[i].transform].localScale = Vector3.one * (worldObjectsWithHealths[i].Enabled(game.playerCharacterType) ? 1f : 0f);
        }
        
        SetupButtonListeners();
    }

    private void SetupButtonListeners() {
        for (int i = 0; i < worldIconButtons.Count; ++i) {
            bool thisButtonActive = i < worldObjectsWithHealths.Count;
            
            worldIconButtons[i].Button.onClick.RemoveAllListeners();
            worldIconButtons[i].Button.gameObject.SetActive(thisButtonActive);

            if (thisButtonActive) {
                int index = i;
                worldIconButtons[i].Button.onClick.AddListener(() => {
                    StopAllCoroutinesAndTweensForThisSystem();

                    Vector3 movementDirection =
                        worldObjectsWithHealths[index].transform.position - game.ControlCharacter.transform.position;
                    movementDirection.y = 0f;
                    movementDirection.Normalize();

                    StartCoroutine(GameExtensions.Coroutines.ObjectArrivedAtPosition(
                        game.ControlCharacter.gameObject,
                        worldObjectsWithHealths[index].transform.position,
                        () => {
                            game.ControlCharacter.Idle();
                            healthChangingTween = InteractWithWorldObject(worldObjectsWithHealths[index]);
                            Actions.WorldObjectStateChanged?.Invoke(worldObjectsWithHealths[index]);
                        },
                        () => {
                            game.ControlCharacter.Walk(movementDirection);
                        },
                        Time.fixedDeltaTime, 3f));
                });
                
                worldIconButtons[i].Image.sprite =
                    config.WorldIconsConfig.Data[EWorldIconType.CampFire].CatcherSprite;
            }
        }
    }

    protected void UpdateWorldIconIndicator(BaseMonoWithHealth baseMonoWithHealth) {
        worldObjectTransformUIDict[worldObjectTransformAndIconDict[baseMonoWithHealth.transform]].SetImageFill(baseMonoWithHealth.GetFillAmount());
    }

    protected abstract List<BaseMonoWithHealth> InitWorldObjectsWithHealth();
    
    protected abstract TweenerCore<int, int, NoOptions> InteractWithWorldObject(BaseMonoWithHealth baseMonoWithHealth);
}*/