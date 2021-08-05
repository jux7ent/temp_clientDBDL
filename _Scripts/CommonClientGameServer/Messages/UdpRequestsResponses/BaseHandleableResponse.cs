using DBDL.CommonDLL;
using DBDL.CommonDLL.UdpServerClient.Messages;

namespace GameServer.UdpRequestsResponses {
    public abstract class BaseHandleableResponse : UdpResponse {
        public BaseHandleableResponse() { }
        public BaseHandleableResponse(BinaryStreamReader reader) : base(reader) { }

        public abstract void Accept(BaseUdpResponsesHandler udpRequestsHandler);
    }
}