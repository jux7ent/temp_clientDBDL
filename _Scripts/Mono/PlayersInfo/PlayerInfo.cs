using DG.Tweening;
using Kuhpik;
using UnityEngine;
/*
public class PlayerInfo : MonoBehaviour {
    public int Id;
    public int Health; // 0, 1, 2
    //public EPlayerState PlayerState;

    public Transform ParentTransform;
    
    private Animator animator;
    private float ignoreAnimationsUntilTime = 0f;
    private Collider[] allColliders;
    private FogCoverable fogCoverable;

    protected void Awake() {
        animator = GetComponent<Animator>();
        allColliders = GetComponents<Collider>();

        ParentTransform = transform.parent;

        StartCoroutine(GameExtensions.Coroutines.WaitAndDo(1f, () => {
            fogCoverable = GetComponent<FogCoverable>();
        }));
        
      //  PlayAnimation(EActionType.Idle, true);
    }

    public void EnableDisableColliders(bool enable) {
        for (int i = 0; i < allColliders.Length; ++i) {
            allColliders[i].enabled = enable;
        }

        if (fogCoverable != null) {
            fogCoverable.enabled = enable;
        }
    }

    public void PlayAnimation(EActionType actionType, bool force=false) {
        animator.SetTrigger(GetAnimatorTriggerNameFromActionType(actionType));
    }

    private string GetAnimatorTriggerNameFromActionType(EActionType actionType) {
    /*    switch (actionType) {
            case EActionType.Idle: {
                return "Idle";
            }
            case EActionType.Walk: {
                return "Walk";
            }
            case EActionType.Attack: {
                return "Attack";
            }
            case EActionType.InteractionCage: {
                return "Interaction";
            }
            case EActionType.InteractionCampFire: {
                return "Interaction";
            }
            case EActionType.InteractionHatch: {
                return "Interaction";
            }
            case EActionType.TakeDamage: {
                return "TakeDamage";
            }
            case EActionType.Stunned: {
                return "Stunned";
            }
            case EActionType.TakeInBag: {
                return "Interaction";
            }
            case EActionType.InBag: {
                return "Stunned";
            }
        }

        return "Idle";
    }
}*/