namespace GameServer {
    public enum EGameMessageTypeFromServer { // delete this file
        Invalid,
        Connected,
        LobbyInfo,
        InitWorldState,
        ServerUpdateState,
        LobbyStartCountdown,
        TakeBot,
        ReleaseBot,
    }
}