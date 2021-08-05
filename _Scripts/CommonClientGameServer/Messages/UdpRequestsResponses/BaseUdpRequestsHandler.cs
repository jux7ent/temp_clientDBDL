using System.Net;
using DBDL.CommonDLL;
using GameServer.ClientRequests;

namespace GameServer.UdpRequestsResponses {
    public abstract class BaseUdpRequestsHandler {
        public abstract void Handle(ConnectRequest connectRequest, IPEndPoint receivedFrom);
        public abstract void Handle(ReconnectRequest reconnectRequest, IPEndPoint receivedFrom);
    }
}