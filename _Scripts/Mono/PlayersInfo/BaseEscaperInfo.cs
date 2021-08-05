using GameServer;
using Constants = Kuhpik.Constants;

public class BaseEscaperInfo : BasePlayerInfo {
    public EEscaperState State;
    public int Health = 2;
    
    protected void Awake() {
        base.Awake();
        IsCatcher = true;
    }

    public override void Accept(PlayersUpdater updater, RWBasePlayerUpdateState playerUpdateState) {
        updater.Update(this, playerUpdateState as RWBaseEscaperUpdateState);
    }
    
    public void SetState(EEscaperState escaperState) {
        if (escaperState != State) {
            PlayAnimation(escaperState);
        }
        
        State = escaperState;
    }

    private void PlayAnimation(EEscaperState escaperState) {
        switch (escaperState) {
            case EEscaperState.Stunned: {
                PlayAnimation(Constants.AnimatorTags.Stunned);
                break;
            }
            case EEscaperState.Caged: {
                PlayAnimation(Constants.AnimatorTags.Idle);
                break;
            }
            case EEscaperState.ReceivedDamage: {
                PlayAnimation(Constants.AnimatorTags.TakeDamge);
                break;
            }
            case EEscaperState.Died: {
                PlayAnimation(Constants.AnimatorTags.Stunned);
                break;
            }
            case EEscaperState.InBag: {
                PlayAnimation(Constants.AnimatorTags.Stunned);
                break;
            }
            case EEscaperState.CampFireInteraction: case EEscaperState.HatchInteraction: case EEscaperState.CageInteraction: {
                PlayAnimation(Constants.AnimatorTags.Interaction);
                break;
            }
        }
    }
}