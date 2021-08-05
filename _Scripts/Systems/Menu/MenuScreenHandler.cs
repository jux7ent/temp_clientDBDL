using System;
using System.Net;
using System.Threading.Tasks;
using DBDL.CommonDLL;
using GameServer.UdpRequestsResponses;
using Kuhpik;
using Server;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreenHandler : GameSystem, IIniting {
    [SerializeField] private Button connectBtn;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Toggle isCatcherToggle;

    private bool _systemInitialized = false;

    void IIniting.OnInit() {
        if (!_systemInitialized) {
            connectBtn.onClick.AddListener(ConnectAndStartQuickGame);

            game.IsCatcher = isCatcherToggle.isOn;

            inputField.text = game.UserData.Nickname;

            inputField.onValueChanged.AddListener((value) => { Printer.Print("TODO"); });

            isCatcherToggle.onValueChanged.AddListener((value) => { game.IsCatcher = value; });

            _systemInitialized = true;
        }
    }

    private void OnDisable() {
        game.GameServer?.Disconnect();
    }

    private void ConnectAndStartQuickGame() {
        FullScreenMessage.Instance.ShowMessage("Connecting...");
        ChooseBestAvailableGameServer((point) => {
            if (point != null) {
                FullScreenMessage.Instance.HideMessage();
                InitServer(point);
            } else {
                FullScreenMessage.Instance.ShowMessage("There is no available game servers.");
            }
        });
    }

    private void ChooseBestAvailableGameServer(Action<IPEndPoint> onChooseBestServer) {
        if (game.ServersData.GameServersSet.Count == 0) {
            onChooseBestServer?.Invoke(null);
        } else {
            GameServersPicker gameServersPicker = new GameServersPicker();
            gameServersPicker.GetGameServerWithBestPing(game.ServersData.GameServersSet, point => {
                Printer.Print($"ADDRESS FOR CHECK: {point.Address} {point.Port}");
                game.MainServer.CheckGameServerAvailability(point, async available => {
                    Printer.Print($"Server {point.Address}:{point.Port} available: {available}");
                    if (!available) {
                        game.ServersData.GameServersSet.Remove(point);
                        await Task.Delay(2000);
                        ChooseBestAvailableGameServer(onChooseBestServer);
                    } else {
                        onChooseBestServer?.Invoke(point);
                    }
                });
            });
        }
    }


    private void InitServer(IPEndPoint endPoint) {
        game.MainServer.NotifyConnectionToGameServer(game.UserData.UserId, endPoint, game.IsCatcher, () => {
            game.SetServer(new GameServerConnector(endPoint, game));

            Printer.Print("Start connection to game server...");
            
            game.GameServer.Connect(game.UserData.UserId).ContinueWith((task) => {
                Bootstrap.InvokeInMainThread(() => {
                    if (task.Result != null) {
                        (task.Result as BaseHandleableResponse).Accept(game.GameServer.ResponsesHandler);
                    } else {
                        Printer.PrintError("task.Result == null");
                    }
                });
            });

            player.LastConnectedGameServer = IpPortParser.GetString(endPoint);
            Bootstrap.SavePlayerData();
        });
    }
}