using DBDL.CommonDLL;
using DBDL.CommonDLL.UdpServerClient.Messages;
using GameServer.UdpRequestsResponses;

namespace GameServer.ServerResponses {
    public class ConnectedResponse : BaseHandleableResponse {
        public int PlayerId;

        public ConnectedResponse(int playerId) {
            PlayerId = playerId;
        }
        
        public ConnectedResponse(BinaryStreamReader reader) : base(reader) { }

        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            PlayerId = reader.ReadInt();
        }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            writer.Write(PlayerId);
        }

        public override void Accept(BaseUdpResponsesHandler udpRequestsHandler) {
            udpRequestsHandler.Handle(this);
        }
    }
}