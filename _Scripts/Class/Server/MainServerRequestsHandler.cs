using System.Collections;
using System.Collections.Generic;
using System.Net;
using DBDL.CommonDLL;
using DBDL.CommonDLL.Requests;
using DBDL.CommonDLL.Responses;
using UnityEngine;

public class MainServerRequestsHandler : BaseRequestHandler {
    public override BaseResponse HandleRequest(GetGameVersionRequest gameVersionRequest, IPEndPoint receivedFrom) {
        throw new System.NotImplementedException();
    }

    public override BaseResponse HandleRequest(GetNewUserIdRequest gameVersionRequest, IPEndPoint receivedFrom) {
        throw new System.NotImplementedException();
    }

    public override BaseResponse HandleRequest(GetInfoForUserIdRequest getInfoForUserIdRequest, IPEndPoint receivedFrom) {
        throw new System.NotImplementedException();
    }

    public override BaseResponse HandleRequest(CheckServerAvailabilityRequest checkServerAvailabilityRequest, IPEndPoint receivedFrom) {
        throw new System.NotImplementedException();
    }

    public override BaseResponse HandleRequest(GetUserInfoForGameServerRequest getUserInfoForGameServerRequest, IPEndPoint receivedFrom) {
        throw new System.NotImplementedException();
    }

    public override BaseResponse HandleRequest(NotifyConnectToGameServerRequest notifyConnectToGameServerRequest, IPEndPoint receivedFrom) {
        throw new System.NotImplementedException();
    }

    public override BaseResponse HandleRequest(GameServerAliveRequest gameServerAliveRequest, IPEndPoint receivedFrom) {
        throw new System.NotImplementedException();
    }

    public override BaseResponse HandleRequest(CheckConnectionToGameServerRequest checkConnectionToGameServerRequest,
        IPEndPoint receivedFrom) {
        throw new System.NotImplementedException();
    }
}