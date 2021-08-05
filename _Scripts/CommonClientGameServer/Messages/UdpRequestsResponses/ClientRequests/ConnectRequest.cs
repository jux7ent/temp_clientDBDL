using System.Net;
using DBDL.CommonDLL;
using DBDL.CommonDLL.UdpServerClient.Messages;
using GameServer.UdpRequestsResponses;

namespace GameServer.ClientRequests {
    public class ConnectRequest : BaseHandleableRequest {
        public int UserId;

        public ConnectRequest(int userId) {
            UserId = userId;
        }
        
        public ConnectRequest(BinaryStreamReader reader) : base(reader) { }

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