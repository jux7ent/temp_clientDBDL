using GameServer;

namespace ServerConsole.CommonClientGameServer.Handlers {
    public abstract class BaseGameServerMessagesHandler {
        public abstract void Handle(ConnectedSM connectedSm);
        public abstract void Handle(InitWorldStateSM initWorldStateSm);
        public abstract void Handle(LobbyInfoSM lobbyInfoSm);
        public abstract void Handle(ServerUpdateStateSM serverUpdateStateSm);
        public abstract void Handle(LobbyStartCountdownSM lobbyStartCountdownSm);
        public abstract void Handle(TakeBotSM takeBotSm);
        public abstract void Handle(ReleaseBotSM releaseBotSm);
        public abstract void Handle(ReconnectionInfo reconnectionInfo);
    }
}