namespace GameServer.MessageHandlers {
    public abstract class BaseClientGameMessagesHandler {
        public abstract void Handle(LobbyInfoCM lobbyInfoCm, RoomClientInfo clientInfo);
        public abstract void Handle(PlayerUpdateCM playerUpdateCm, RoomClientInfo clientInfo);
        public abstract void Handle(WorldInitializedCM worldInitializedCm, RoomClientInfo clientInfo);
        public abstract void Handle(BotTakenCM botTakenCm, RoomClientInfo clientInfo);
    }
}