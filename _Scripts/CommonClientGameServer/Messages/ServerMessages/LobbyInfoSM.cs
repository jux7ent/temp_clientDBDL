using DBDL.CommonDLL;
using ServerConsole.CommonClientGameServer.Handlers;


namespace GameServer {
    public class LobbyInfoSM : BaseServerMessage {
        public RWList<LobbyPlayerInfo> Players = new RWList<LobbyPlayerInfo>();

        public LobbyInfoSM() { }
        
        public LobbyInfoSM(BinaryStreamReader reader) : base(reader) { }
        
      /*  public LobbyInfoSM(EGameMessageTypeFromServer messageTypeFromServer, BinaryStreamReader reader) : base(
            messageTypeFromServer, reader) { }*/

        public void AddPlayer(int playerId, string name, LobbyInfoCM lobbyInfoCm) {
            LobbyPlayerInfo player = new LobbyPlayerInfo(playerId, name, lobbyInfoCm);
            Players.Add(player);
        }

        public void AddPlayer(int playerId, string name, ECharacter characterType, bool ready) {
            Players.Add(new LobbyPlayerInfo(playerId, name, characterType, ready));
        }

        public void UpdatePlayerLobbyInfo(int playerId, LobbyInfoCM lobbyInfoCm) {
            Players[Players.FindIndex(x => x.PlayerId == playerId)].Update(lobbyInfoCm);
        }

        public override void Accept(BaseGameServerMessagesHandler handler) {
            handler.Handle(this);
        }

       /* protected override EGameMessageTypeFromServer GetMessageTypeFromServer() {
            return EGameMessageTypeFromServer.LobbyInfo;
        }*/

        public override void FillsFromReader(BinaryStreamReader reader) {
            Players.FillsFromReader(reader);
        }

        public override void Write(BinaryStreamWriter writer) {
            Players.Write(writer);
        }
    }
}