using System.Collections.Generic;
using DBDL.CommonDLL;
using GameServer;
using Kuhpik;
using UnityEngine;

internal struct InteractableInfo {
    public Vector3 Position;
    public int Id;
    public EActionType ActionType;
    public BaseStaticObjectWithProgress BaseStaticObject;

    public InteractableInfo(Vector3 position, int id, EActionType actionType,
        BaseStaticObjectWithProgress baseStaticObject) {
        Position = position;
        Id = id;
        ActionType = actionType;
        BaseStaticObject = baseStaticObject;
    }
}

public class InteractableControllerSystem : GameSystem, IIniting, IUpdating {
    [SerializeField] private float howOftenCheck = 0.1f;
    [SerializeField] private float escaperInteractableRadius = 3f;
    [SerializeField] private float catcherInteractableRadius = 2f;

    private float lastCheckTime = 0f;

    private BaseInteractor interactor;

    void IIniting.OnInit() {
        if (game.IsCatcher) interactor = new CatcherInteractor(catcherInteractableRadius, game);
        else interactor = new EscaperInteractor(escaperInteractableRadius, game);
    }

    void IUpdating.OnUpdate() {
        if (Time.time - lastCheckTime > howOftenCheck) {
            interactor.Check();
            lastCheckTime = Time.time;
        }
    }
}

internal abstract class BaseInteractor {
    protected readonly GameData game;

    protected BaseInteractor(GameData game) {
        this.game = game;
    }

    public abstract void Check();
}

internal class CatcherInteractor : BaseInteractor {
    private readonly float sqrInteractableRadius;
    private readonly List<BaseEscaperInfo> escaperInfos = new List<BaseEscaperInfo>();
    private readonly List<Cage> cages = new List<Cage>();
    private readonly BaseCatcherInfo me;

    public CatcherInteractor(float interactableRadius, GameData game) : base(game) {
        sqrInteractableRadius = interactableRadius * interactableRadius;

        FillInteractableInfo();
        me = UnityEngine.Object.FindObjectOfType<BaseCatcherInfo>();
    }

    public override void Check() {
        Vector3 characterPosition = game.Character.Transform.position;

        if (me.State == ECatcherState.Free) {
            for (int i = 0; i < escaperInfos.Count; ++i) {
                if (escaperInfos[i].State == EEscaperState.Stunned) {
                    if ((characterPosition - escaperInfos[i].transform.position).sqrMagnitude < sqrInteractableRadius) {
                        Actions.OnStartInteractable?.Invoke(
                            escaperInfos[i].PlayerId,
                            EActionType.TakeInBag);
                        return;
                    }
                }
            }

            Actions.OnStopInteractable?.Invoke();
        } else if (me.State == ECatcherState.CarryingEscaper) {
            for (int i = 0; i < cages.Count; ++i) {
                if (cages[i].ContainsPlayerId == -1) {
                    if ((characterPosition - cages[i].transform.position).sqrMagnitude < sqrInteractableRadius) {
                        Actions.OnStartInteractable?.Invoke(cages[i].Id, EActionType.PlaceEscaperInCage);
                        return;
                    }
                }
            }

            Actions.OnStopInteractable?.Invoke();
        }
    }

    private void FillInteractableInfo() {
        escaperInfos.Clear();
        cages.Clear();

        escaperInfos.AddRange(UnityEngine.Object.FindObjectsOfType<BaseEscaperInfo>());
        cages.AddRange(UnityEngine.Object.FindObjectsOfType<Cage>());
    }
}

internal class EscaperInteractor : BaseInteractor {
    private readonly float sqrInteractableRadius;
    private readonly List<InteractableInfo> allInteractableInfos = new List<InteractableInfo>();
    private readonly BaseEscaperInfo me;

    public EscaperInteractor(float interactableRadius, GameData game) : base(game) {
        sqrInteractableRadius = interactableRadius * interactableRadius;

        FillInteractableInfo();

        BaseEscaperInfo[] escapers = UnityEngine.Object.FindObjectsOfType<BaseEscaperInfo>();
        for (int i = 0; i < escapers.Length; ++i) {
            if (escapers[i].PlayerId == game.MyPlayerId) {
                me = escapers[i];
                return;
            }
        }
    }

    public override void Check() {
        if (me.State == EEscaperState.Free || me.State == EEscaperState.ReceivedDamage) {
            Vector3 characterPosition = game.Character.Transform.position;

            for (int i = 0; i < allInteractableInfos.Count; ++i) {
                if (allInteractableInfos[i].BaseStaticObject.Progress01 < 1f) {
                    if ((characterPosition - allInteractableInfos[i].Position).sqrMagnitude < sqrInteractableRadius) {
                        Actions.OnStartInteractable?.Invoke(allInteractableInfos[i].Id,
                            allInteractableInfos[i].ActionType);
                        return;
                    }
                }
            }
        }

        Actions.OnStopInteractable?.Invoke();
    }

    private void FillInteractableInfo() {
        allInteractableInfos.Clear();

        Cage[] cages = UnityEngine.Object.FindObjectsOfType<Cage>();
        for (int i = 0; i < cages.Length; ++i) {
            allInteractableInfos.Add(new InteractableInfo(cages[i].transform.position, cages[i].Id,
                EActionType.StartCageInteraction, cages[i]));
        }

        CampFire[] campFires = UnityEngine.Object.FindObjectsOfType<CampFire>();
        for (int i = 0; i < campFires.Length; ++i) {
            allInteractableInfos.Add(new InteractableInfo(campFires[i].transform.position, campFires[i].Id,
                EActionType.StartCampFireInteraction, campFires[i]));
        }

        Hatch[] hatches = UnityEngine.Object.FindObjectsOfType<Hatch>();
        for (int i = 0; i < hatches.Length; ++i) {
            allInteractableInfos.Add(new InteractableInfo(hatches[i].transform.position, hatches[i].Id,
                EActionType.StartHatchInteraction, hatches[i]));
        }

        Gate[] gates = Object.FindObjectsOfType<Gate>();
        for (int i = 0; i < gates.Length; ++i) {
            allInteractableInfos.Add(new InteractableInfo(gates[i].transform.position, gates[i].Id,
                EActionType.StartGateInteraction, gates[i]));
        }
    }
}