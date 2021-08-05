using System.Net;
using DBDL.CommonDLL;
using GameServer.UdpRequestsResponses;

namespace GameServer.ClientRequests {
    public class ReconnectRequest : BaseHandleableRequest {
        public int UserId;

        public ReconnectRequest(int userId) {
            UserId = userId;
        }
        
        public ReconnectRequest(BinaryStreamReader reader) : base(reader) { }

        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            UserId = reader.ReadInt();
        }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            writer.Write(UserId);
        }

        public override void Accept(BaseUdpRequestsHandler udpRequestsHandler, IPEndPoint receivedFrom) {
            udpRequestsHandler.Handle(this, receivedFrom);
        }
    }
}