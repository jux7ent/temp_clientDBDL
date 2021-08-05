using System.Net;
using DBDL.CommonDLL;
using DBDL.CommonDLL.UdpServerClient.Messages;

namespace GameServer.UdpRequestsResponses {
    public abstract class BaseHandleableRequest : UdpRequest {
        public BaseHandleableRequest() { }
        public BaseHandleableRequest(BinaryStreamReader reader) : base(reader) { }

        public abstract void Accept(BaseUdpRequestsHandler udpRequestsHandler, IPEndPoint receivedFrom);
    }
}