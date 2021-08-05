using DBDL.CommonDLL;
using GameServer;
using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionControllerSystem : GameSystem, IIniting {
    [SerializeField] private Button interactionBtn;
    [SerializeField] private Button useAbilityBtn;
    [SerializeField] private Button medkitBtn;
    [SerializeField] private TextMeshProUGUI interactionBtnTMP;

    private int lastTargetId;
    private EActionType lastActionType;
    private float ignoreInteractableWhileTime = 0;

    private bool systemInited = false;

    void IIniting.OnInit() {
        interactionBtn.gameObject.SetActive(true);
        
        if (!systemInited) {
            Actions.OnStartInteractable += OnStartInteractable;
            Actions.OnStopInteractable += OnStopInteractable;
            
            interactionBtn.onClick.AddListener(OnInteractionClicked);
            useAbilityBtn.onClick.AddListener(OnUseAbilityClicked);
            medkitBtn.onClick.AddListener(OnUseMedkit);
                
            systemInited = true;
        }
    }

    private void OnUseAbilityClicked() {
        game.GameServer.AppendAction(BuildAbilityAction(game.MyPlayerId));
    }

    private void OnUseMedkit() {
        UseMedkitAction action = new UseMedkitAction();
        action.OwnerId = game.MyPlayerId;
        
        game.GameServer.AppendAction(action);
    }

    private void OnInteractionClicked() {
        if (!game.Character.CanDo.Contains(ECanDo.Interact)) {
            return;
        }

        if (interactionBtnTMP.text != "Attack") {
            game.GameServer.AppendAction(BuildTargetAction(game.MyPlayerId, lastActionType, lastTargetId));
            interactionBtn.gameObject.SetActive(true);
        } else {
            AttackAction action = new AttackAction();
            action.OwnerId = game.MyPlayerId;
            
            game.GameServer.AppendAction(action);
        }
    }

    private void OnStartInteractable(int targetId, EActionType actionType) {
        lastTargetId = targetId;
        lastActionType = actionType;
        
        interactionBtn.gameObject.SetActive(true);
        
        if (game.IsCatcher) interactionBtnTMP.text = "Interaction";
    }

    private void OnStopInteractable() {
        if (!game.Character.CanDo.Contains(ECanDo.Interact)) {
            interactionBtn.gameObject.SetActive(false);
        } else {
            interactionBtn.gameObject.SetActive(game.IsCatcher);

            if (game.IsCatcher) interactionBtnTMP.text = "Attack";
        }
    }

    private BaseAction BuildAbilityAction(int ownerId) {
        Debug.Log("Build ability action");
        UseAbilityAction useAbilityAction = new UseAbilityAction();
        useAbilityAction.OwnerId = ownerId;

        return useAbilityAction;
    }

    private BaseAction BuildTargetAction(int actionOwnerId, EActionType actionType, int targetId) {
        BaseActionWithTarget actionWithTarget;
        
        switch (actionType) {
            case EActionType.StartCageInteraction: {
                actionWithTarget = new StartCageInteractionAction();
                break;
            }
            case EActionType.StartHatchInteraction: {
                actionWithTarget = new StartHatchInteractionAction();
                break;
            }
            case EActionType.StartCampFireInteraction: {
                actionWithTarget = new StartCampFireInteractionAction();
                break;
            }
            case EActionType.TakeInBag: {
                actionWithTarget = new TakeInBagAction();
                break;
            }
            case EActionType.PlaceEscaperInCage: {
                actionWithTarget = new PlaceInCageAction();
                break;
            }
            case EActionType.StartGateInteraction: {
                actionWithTarget = new StartGateInteractionAction();
                break;
            }
            default: {
                Printer.Print($"Error. {actionType} not found");
                actionWithTarget = new StartHatchInteractionAction();
                break;
            }
        }
        
        actionWithTarget.OwnerId = actionOwnerId;
        actionWithTarget.Id = ActionsIdGenerator.GenId();
        actionWithTarget.TargetId = targetId;

        return actionWithTarget;
    }
}