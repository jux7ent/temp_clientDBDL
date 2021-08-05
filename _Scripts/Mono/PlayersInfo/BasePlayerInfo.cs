using DBDL.CommonDLL;
using DG.Tweening;
using GameServer;
using Kuhpik;
using UnityEngine;

public abstract class BasePlayerInfo : MonoBehaviour {
    public bool IsCatcher;
    public int PlayerId = -1;
    public bool MyControlled = false;
    public GameObject ModelGameObject;

    public Transform DefaultParent;

    private Animator animator;

    protected virtual void Awake() {
        DefaultParent = transform.parent;
        animator = GetComponent<Animator>();
        ModelGameObject = GetComponentInChildren<SkinnedMeshRenderer>().gameObject;
    }

    public void UpdateMovement(RWBasePlayerUpdateState playerUpdateState) {
        if (playerUpdateState.CanDo.Contains(ECanDo.Move)) {
            if (!MyControlled) {
                transform.DOKill();
                transform.DOMove(playerUpdateState.LocationData.Position, Ping.Value).SetEase(Ease.Linear);
                transform.DORotate(((Quaternion)playerUpdateState.LocationData.Rotation).eulerAngles, Ping.Value).SetEase(Ease.Linear);
            }
        }
    }

    public void PlayAnimation(string triggerStr) {
        animator.SetTrigger(triggerStr);
    }

    public abstract void Accept(PlayersUpdater updater, RWBasePlayerUpdateState playerUpdateState);

    public void HideInGrass(bool hidden) {
        Printer.Print($"Hide in grass: {hidden}");
        ModelGameObject.layer = hidden ? LayerMask.NameToLayer("HiddenInGrass") : LayerMask.NameToLayer("Default");
    }
}