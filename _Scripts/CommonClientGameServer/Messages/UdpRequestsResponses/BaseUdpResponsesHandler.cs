using GameServer.ClientRequests;
using GameServer.ServerResponses;

namespace GameServer.UdpRequestsResponses {
    public abstract class BaseUdpResponsesHandler {
        public abstract void Handle(ConnectedResponse connectedResponse);
        public abstract void Handle(ReconnectedResponse reconnectedResponse);
    }
}