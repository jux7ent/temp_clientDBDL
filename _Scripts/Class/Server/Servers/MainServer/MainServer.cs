using System;
using System.Net;
using System.Threading;
using DBDL.CommonDLL;
using DBDL.CommonDLL.Requests;
using DBDL.CommonDLL.Responses;
using Kuhpik;

public class MainServer {
    private IPEndPoint endPoint;

    private readonly MainServerConnector mainServerConnector;

    public Action OnConnected;
    public Action OnDisconnected;

    public MainServer(IPEndPoint endPoint) {
        this.endPoint = endPoint;
        mainServerConnector = new MainServerConnector(endPoint, new MainServerRequestsHandler());

        mainServerConnector.OnConnected += () => {
            Printer.Print("OnConnected");
            OnConnected?.Invoke();
        };

        mainServerConnector.OnDisconnected += () => {
            Printer.Print("OnDisconnected");
            OnDisconnected?.Invoke();
        };
    }

    public void Connect() {
        mainServerConnector.Connect();
    }

    public void Disconnect() {
        mainServerConnector.Disconnect();
    }

    public void CheckGameServerAvailability(IPEndPoint ipEndPoint, Action<bool> onCheckServerAvailability) {
        new Thread(() => {
            mainServerConnector.SendRequest(new CheckServerAvailabilityRequest(ipEndPoint))
                .ContinueWith(task => {
                    if (task.IsCompleted) {
                        CheckServerAvailabilityResponse response = task.Result as CheckServerAvailabilityResponse;
                        Bootstrap.InvokeInMainThread(() => onCheckServerAvailability.Invoke(response.Available));
                    }
                });
        }).Start();
    }

    public void GetGameVersion(Action<int> onGettingGameVersion) {
        new Thread(() => {
            mainServerConnector.SendRequest(new GetGameVersionRequest())
                .ContinueWith(task => {
                    if (task.IsCompleted) {
                        GetGameVersionResponse response = task.Result as GetGameVersionResponse;
                        Bootstrap.InvokeInMainThread(() =>
                            onGettingGameVersion.Invoke(response.GameVersion));
                    }
                });
        }).Start();
    }

    public void GetNewUserId(Action<int> onGettingNewUserId) {
        new Thread(() => {
            mainServerConnector.SendRequest(new GetNewUserIdRequest())
                .ContinueWith(task => {
                    if (task.IsCompleted) {
                        GetNewUserIdResponse response = task.Result as GetNewUserIdResponse;
                        Bootstrap.InvokeInMainThread(() => onGettingNewUserId.Invoke(response.UserId));
                    }
                });
        }).Start();
    }

    public void GetInfoForUserId(int userId, Action<UserEntity, bool> onReceive) {
        new Thread(() => {
            mainServerConnector.SendRequest(new GetInfoForUserIdRequest(userId, "157.245.129.95:61234"))
                .ContinueWith(task => {
                    if (task.IsCompleted) {
                        GetInfoForUserIdResponse response = task.Result as GetInfoForUserIdResponse;
                        Bootstrap.InvokeInMainThread(() => onReceive.Invoke(response.UserEntity, response.CanReconnect));
                    }
                });
        }).Start();
    }

    public void NotifyConnectionToGameServer(int userId, IPEndPoint gameServerEndPoint, bool isCatcher,
        Action onReceive) {
        new Thread(() => {
            mainServerConnector.SendRequest(new NotifyConnectToGameServerRequest(userId, gameServerEndPoint, isCatcher))
                .ContinueWith(task => {
                    if (task.IsCompleted) {
                        NotifyConnectToGameServerResponse response = task.Result as NotifyConnectToGameServerResponse;
                        Bootstrap.InvokeInMainThread(() => onReceive.Invoke());
                    }
                });
        }).Start();
    }
}