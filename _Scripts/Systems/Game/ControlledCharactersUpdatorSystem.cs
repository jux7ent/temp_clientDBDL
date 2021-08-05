using GameServer;
using Kuhpik;

public class ControlledCharactersUpdatorSystem : GameSystem, IIniting {
    private bool systemInited = false;

    void IIniting.OnInit() {
        if (!systemInited) {
            game.GameServer.MessagesHandler.OnGetServerSceneUpdateData += OnGetServerUpdateData;
            systemInited = true;
        }
    }

    private void OnGetServerUpdateData(ServerUpdateStateSM serverWorldUpdate) {
        game.Character.CanDo.Update(serverWorldUpdate.Players[game.MyPlayerId].CanDo);
    }
}