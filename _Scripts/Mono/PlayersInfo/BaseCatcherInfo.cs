using GameServer;
using Constants = Kuhpik.Constants;

public class BaseCatcherInfo : BasePlayerInfo {
    public ECatcherState State;

    protected void Awake() {
        base.Awake();
        IsCatcher = true;
    }
    
    public override void Accept(PlayersUpdater updater, RWBasePlayerUpdateState playerUpdateState) {
        updater.Update(this, playerUpdateState as RWBaseCatcherUpdateState);
    }

    public void SetState(ECatcherState catcherState) {
        if (catcherState != State) {
            PlayAnimation(catcherState);
        }
        
        State = catcherState;
    }

    private void PlayAnimation(ECatcherState catcherState) {
        switch (catcherState) {
            case ECatcherState.StartCarryEscaper: {
                PlayAnimation(Constants.AnimatorTags.TakeDamge);
                break;
            }
            case ECatcherState.CarryingEscaper: {
                PlayAnimation(Constants.AnimatorTags.Idle);
                break;
            }
            case ECatcherState.StopCarryEscaper: {
                PlayAnimation(Constants.AnimatorTags.TakeDamge);
                break;
            }
            case ECatcherState.MovingPlayerInCage: {
                PlayAnimation(Constants.AnimatorTags.TakeDamge);
                break;
            }
        }
    }
}