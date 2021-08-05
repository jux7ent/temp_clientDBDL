using GameServer;

public class PlayersUpdater {
    public void Update(BaseCatcherInfo catcherInfo, RWBaseCatcherUpdateState catcherUpdateState) {
        catcherInfo.SetState(catcherUpdateState.State);
        catcherInfo.UpdateMovement(catcherUpdateState);
    }

    public void Update(BaseEscaperInfo escaperInfo, RWBaseEscaperUpdateState escaperUpdateState) {
        escaperInfo.SetState(escaperUpdateState.State);
        escaperInfo.UpdateMovement(escaperUpdateState);

        escaperInfo.Health = escaperUpdateState.Health;
    }
}
