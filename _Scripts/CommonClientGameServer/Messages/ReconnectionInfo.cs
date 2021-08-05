using DBDL.CommonDLL;
using ServerConsole.CommonClientGameServer.Handlers;

namespace GameServer {
    public class ReconnectionInfo {
        public int PlayerId;
        public InitWorldStateSM InitWorldStateSm = new InitWorldStateSM();
        public ServerUpdateStateSM ServerUpdateStateSm = new ServerUpdateStateSM();
        public LobbyInfoSM LobbyInfoSm = new LobbyInfoSM();

        public ReconnectionInfo() { }

        public ReconnectionInfo(int playerId, InitWorldStateSM initWorldStateSm,
            ServerUpdateStateSM serverUpdateStateSm, LobbyInfoSM lobbyInfoSm) : this() {
            PlayerId = playerId;
            InitWorldStateSm = initWorldStateSm;
            ServerUpdateStateSm = serverUpdateStateSm;
            LobbyInfoSm = lobbyInfoSm;
        }
    }
}