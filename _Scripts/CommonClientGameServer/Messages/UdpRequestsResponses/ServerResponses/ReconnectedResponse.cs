using DBDL.CommonDLL;
using GameServer.UdpRequestsResponses;

namespace GameServer.ServerResponses {
    public class ReconnectedResponse : BaseHandleableResponse {
        public int PlayerId;
        public bool ReconnectAvailable;
        public InitWorldStateSM InitWorldStateSm = new InitWorldStateSM();
        public ServerUpdateStateSM ServerUpdateStateSm = new ServerUpdateStateSM();
        public LobbyInfoSM LobbyInfoSm = new LobbyInfoSM();

        public ReconnectedResponse(int playerId, bool reconnectAvailable, InitWorldStateSM initWorldStateSm,
            ServerUpdateStateSM serverUpdateStateSm, LobbyInfoSM lobbyInfoSm) {
            PlayerId = playerId;
            ReconnectAvailable = reconnectAvailable;
            InitWorldStateSm = initWorldStateSm;
            ServerUpdateStateSm = serverUpdateStateSm;
            LobbyInfoSm = lobbyInfoSm;
        }

        public ReconnectedResponse(BinaryStreamReader reader) : base(reader) { }

        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            PlayerId = reader.ReadInt();
            ReconnectAvailable = reader.ReadBoolean();

            if (ReconnectAvailable) {
                InitWorldStateSm.FillsFromReader(reader);
                ServerUpdateStateSm.FillsFromReader(reader);
                LobbyInfoSm.FillsFromReader(reader);
            }
        }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            writer.Write(PlayerId);
            writer.Write(ReconnectAvailable);

            if (ReconnectAvailable) {
                InitWorldStateSm.Write(writer);
                ServerUpdateStateSm.Write(writer);
                LobbyInfoSm.Write(writer);
            }
        }

        public override void Accept(BaseUdpResponsesHandler udpRequestsHandler) {
            udpRequestsHandler.Handle(this);
        }
    }
}