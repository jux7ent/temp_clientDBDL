using System.Collections.Generic;
using GameServer;
using Kuhpik;
using UnityEngine;
using Object = UnityEngine.Object;

public class IndicatorsControllerSystem : GameSystem, IIniting, IUpdating {
    [SerializeField] private GameObject loadingPrefab;
    [SerializeField] private RectTransform canvasTransform;
    [SerializeField] private Vector2 offset;

    private BaseIndicators baseIndicators;

    void IIniting.OnInit() {
        if (game.IsCatcher) {
            baseIndicators = new CatcherIndicators();
        } else {
            baseIndicators = new EscaperIndicators(loadingPrefab, canvasTransform, offset, game);
        }
    }
    
    void IUpdating.OnUpdate() {
        baseIndicators.Update();
    }
}

internal abstract class BaseIndicators {
    public BaseIndicators() {
        Actions.OnStartInteractable += OnStartInteractable;
        Actions.OnStopInteractable += OnStopInteractable;
    }
    
    public abstract void Update();

    protected abstract void OnStartInteractable(int targetId, EActionType actionType);
    protected abstract void OnStopInteractable();
}

internal class CatcherIndicators : BaseIndicators {
    public override void Update() {
    }

    protected override void OnStartInteractable(int targetId, EActionType actionType) { }
    protected override void OnStopInteractable() { }
}

internal class EscaperIndicators : BaseIndicators {
    private readonly List<BaseStaticObjectWithProgress> allStaticObjectsList = new List<BaseStaticObjectWithProgress>();
    private readonly List<ProgressIndicator> progressIndicators = new List<ProgressIndicator>();
    private readonly Dictionary<int, ProgressIndicator> objIdCorrespondProgressIndicator =
        new Dictionary<int, ProgressIndicator>();
    private readonly BaseEscaperInfo me;
    
    private Vector2 offset;
    private GameData game;
    private int lastEnabledObjectId = -1;

    public EscaperIndicators(GameObject loadingPrefab, RectTransform canvasTransform, Vector2 offset, GameData game) {
        this.offset = offset;
        this.game = game;
        
        InitStaticObjectsList();
        InitProgressIndicators(loadingPrefab, canvasTransform);

        BaseEscaperInfo[] escapers = UnityEngine.Object.FindObjectsOfType<BaseEscaperInfo>();
        for (int i = 0; i < escapers.Length; ++i) {
            if (escapers[i].PlayerId == game.MyPlayerId) {
                me = escapers[i];
                break;
            }
        }
    }

    public override void Update() {
        for (int i = 0; i < allStaticObjectsList.Count; ++i) {
            if (progressIndicators[i].gameObject.activeInHierarchy) {
                // indicators movement
                ((RectTransform) progressIndicators[i].transform).anchoredPosition =
                    (Vector2)game.MainCamera.WorldToScreenPoint(allStaticObjectsList[i].transform.position) + offset;
                progressIndicators[i].SetProgress(allStaticObjectsList[i].Progress01);
            }
        }
    }

    protected override void OnStartInteractable(int objectId, EActionType actionType) {
        objIdCorrespondProgressIndicator[objectId].gameObject.SetActive(true);
        lastEnabledObjectId = objectId;
    }

    protected override void OnStopInteractable() {
        if (lastEnabledObjectId != -1 && !(me.State == EEscaperState.CageInteraction || me.State == EEscaperState.HatchInteraction || me.State == EEscaperState.CampFireInteraction || me.State == EEscaperState.GateInteraction)) {
            objIdCorrespondProgressIndicator[lastEnabledObjectId].gameObject.SetActive(false);
            lastEnabledObjectId = -1;
        }
    }

    private void InitStaticObjectsList() {
        allStaticObjectsList.Clear();
        
        allStaticObjectsList.AddRange(Object.FindObjectsOfType<Cage>());
        allStaticObjectsList.AddRange(Object.FindObjectsOfType<CampFire>());
        allStaticObjectsList.AddRange(Object.FindObjectsOfType<Hatch>());
        allStaticObjectsList.AddRange(Object.FindObjectsOfType<Gate>());
    }

    private void InitProgressIndicators(GameObject loadingPrefab, RectTransform canvasTransform) {
        progressIndicators.Clear();
        objIdCorrespondProgressIndicator.Clear();

        for (int i = 0; i < allStaticObjectsList.Count; ++i) {
            progressIndicators.Add(Object.Instantiate(loadingPrefab, canvasTransform).GetComponent<ProgressIndicator>());
            objIdCorrespondProgressIndicator.Add(allStaticObjectsList[i].Id, progressIndicators[progressIndicators.Count - 1]);
            progressIndicators[progressIndicators.Count - 1].gameObject.SetActive(false);
        }
    }
}