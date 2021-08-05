using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using DBDL.CommonDLL;
using DBDL.CommonDLL.Responses;
using DBDL.CommonDLL.UdpServerClient;
using DBDL.CommonDLL.UdpServerClient.Messages;
using GameServer;
using GameServer.BaseClientMessages;
using GameServer.ClientRequests;
using GameServer.ServerResponses;
using Kuhpik;
using UnityEngine;
using EMessageType = GameServer.EMessageType;

namespace Server {
    public enum ENetworkClientState {
        Disconnected,
        SayingHello,
        Connected,
        TryingReconnect,
        WorldInitialization,
        UpdatePlayer
    }

    public class GameServersPicker {
        public void GetGameServerWithBestPing(HashSet<IPEndPoint> serversEndPoint, Action<IPEndPoint> onChooseBest) {
            //TODO
            onChooseBest?.Invoke(IpPortParser.Parse("157.245.129.95:61234"));
        }
    }

    public class GameServerConnector {
        public ENetworkClientState NetworkState { get; private set; }
        public PlayerUpdateCM PlayerUpdateCm { get; } = new PlayerUpdateCM();

        private int _clientPacketNumber = 0;
        private int _serverPacketNumber = 0;

        private readonly GameData _gameData;
        private readonly ClientUDP _client;

        public GameServerMessageHandler MessagesHandler { get; private set; }
        public GameServerResponsesHandler ResponsesHandler { get; private set; }

        public GameServerConnector(IPEndPoint gameServerEndPoint, GameData gameData) {
            MessagesHandler = new GameServerMessageHandler(this, gameData);
            ResponsesHandler = new GameServerResponsesHandler(gameData, this);
            
            _gameData = gameData;

            NetworkLogger.Instance.LogNetworkStateTMP(ENetworkClientState.Disconnected);

            _client = new ClientUDP(InitializeSocket(gameServerEndPoint), typeof(ConnectRequest));
            CGSContext.AssemblyClassesDict = _client.StringTypeHashToType;
            _client.OnMessageReceived += HandleServerMessage;
        }

        public Task<UdpResponse> Connect(int userId) {
            _client.Connect();
            
            SetState(ENetworkClientState.SayingHello);
            return _client.SendRequest(new ConnectRequest(userId), 1000);
        }

        public Task<UdpResponse> Reconnect(int userId) {
            _client.Connect();

            SetState(ENetworkClientState.TryingReconnect);
            return _client.SendRequest(new ReconnectRequest(userId), 1000);
        }

        private UdpClient InitializeSocket(IPEndPoint serverEndPoint) {
            UdpClient result = new UdpClient(0);
            result.Connect(serverEndPoint);
            return result;
        }

        public void StartSendingOutgoingsPackets(ENetworkClientState initialState) {
            SetState(initialState);
            new Thread(SendOutgoingPackets).Start();
        }

        public void SetState(ENetworkClientState newState) {
          //  Printer.Print($"SetState: {newState}");
            NetworkLogger.Instance.LogNetworkStateTMP(newState);
            NetworkState = newState;
        }

        private ConnectedSM WaitConnection() {
            Printer.PrintError("Error wait connection not implemented");
          /*  Task<byte[]> taskMessage = client.WaitMessage();
            taskMessage.Wait();

            UnpackServerMessage(taskMessage.Result, out int classIdInTable, out BinaryStreamReader reader);
            return new ConnectedSM(reader);*/

          return null;
        }

        /*private ReconnectionSM WaitReconnection() {
            Printer.PrintError("Error wait reconnection not implemented");
            return null;*/
            /*
            Task<byte[]> taskMessage = client.WaitMessage();
            taskMessage.Wait();

            UnpackServerMessage(taskMessage.Result, out int classIdInTable, out BinaryStreamReader reader);
            return new ReconnectionSM(reader);*/
       // }

        private void SendOutgoingPackets() {
            while (_client.Connected) {
                try {
                    NetworkLogger.Instance.LogLastSentTMP(NetworkState.ToString());
                    switch (NetworkState) {
                        case ENetworkClientState.Connected: {
                            SendLobbyInfo();

                            Thread.Sleep(Timeouts.RareMessage);
                            break;
                        }
                        case ENetworkClientState.WorldInitialization: {
                            SendWorldInitialized();

                            Thread.Sleep(Timeouts.RareMessage);
                            break;
                        }
                        case ENetworkClientState.UpdatePlayer: {
                            SendNewPlayerState();
                            Thread.Sleep(Timeouts.OftenMessage);
                            break;
                        }
                        default: {
                            Printer.Print($"Invalid SendOutgoingPackets {NetworkState}");
                            break;
                        }
                    }
                }
                catch (Exception ex) {
                    Printer.Print($"Exception in senderThread\n{ex}");
                    break;
                }
            }
        }

        public void AppendAction(BaseAction baseAction) {
            baseAction.Id = ActionsIdGenerator.GenId();
            PlayerUpdateCm.ActionsQueue.Enqueue(baseAction);
        }

        private void SendLobbyInfo() {
            SendMessage(_gameData.LobbyInfoCm);
        }

        private void SendWorldInitialized() {
            SendMessage(new WorldInitializedCM());
        }

        private void SendNewPlayerState() {
            SendMessage(PlayerUpdateCm);
        }

        public void SendBotTakenMessage(int botId) {
            SendMessage(new BotTakenCM(botId));
        }

        private void SendMessage(BaseGameClientMessage message) {
            message.UserId = _gameData.UserData.UserId;
            _client.SendMessage(_clientPacketNumber++, message);
        }

        private void HandleServerMessage(int packetNumber, BaseReadableWritable readableWritable, IPEndPoint receivedFrom) {
            if (_gameData.GameServer.NetworkState == ENetworkClientState.TryingReconnect) return;
            
            BaseServerMessage serverMessage = readableWritable as BaseServerMessage;
            serverMessage.Accept(MessagesHandler);
            NetworkLogger.Instance.LogLastReceivedTMP($"{serverMessage.GetType()}");

            if (NetworkState == ENetworkClientState.SayingHello ||
                NetworkState == ENetworkClientState.TryingReconnect) {
                NetworkLogger.Instance.LogLastReceivedTMP($"WTF MAN...");
            }

            /*
            if (NetworkState != ENetworkClientState.SayingHello &&
                NetworkState != ENetworkClientState.TryingReconnect &&
                UnpackServerMessage(data, out int classIdInTable, out BinaryStreamReader reader)) {
                Printer.Print($"{CGSContext.RwAssemblyClassesDict.Count}");
                BaseServerMessage serverMessage = ((BaseServerMessage) LambdaActivator
                        .CreateInstance(CGSContext.RwAssemblyClassesDict[classIdInTable], reader));
                serverMessage.Accept(MessagesHandler);
                NetworkLogger.Instance.LogLastReceivedTMP($"{serverMessage.GetType()}");
            } else {
                NetworkLogger.Instance.LogLastReceivedTMP($"IGNORED");
            }*/
        }

        public void Disconnect() {
            SendDisconnectionPacket();
            _client.Disconnect();
        }

        private void SendDisconnectionPacket() {
           /* client.SendPacket(BuildPacket(EMessageType.Disconnect).GetBuffer());*/
        }

        public void SendLoadingReady() {
            MessagesHandler.SendLoadingReady();
        }

       /* private BinaryStreamWriter BuildPacket(EMessageType messageType, BaseReadableWritable readableWritable = null) {
            BinaryStreamWriter writer = new BinaryStreamWriter(new MemoryStream());

            new MessageHeader(messageType, gameData.UserData.UserId).Write(writer);

            if (messageType == EMessageType.GameMessage) {
                new GameMessageHeader(clientPacketNumber++).Write(writer);
                writer.Write(CGSContext.RwAssemblyClassesDict[readableWritable.GetType()]);
                readableWritable.Write(writer);
            } else if (messageType == EMessageType.ConnectMessage || messageType == EMessageType.Reconnect) {
                readableWritable.Write(writer);
            }

            return writer;
        }*/

        private bool UnpackServerMessage(byte[] data, out int classIdInTable, out BinaryStreamReader reader) {
            reader = new BinaryStreamReader(new MemoryStream(data));

            bool result = CheckServerPacketNumber(reader.ReadInt32());
            classIdInTable = reader.ReadInt32();

            return result;
        }

        private bool CheckServerPacketNumber(int receivedPacketNumber) {
            if (_serverPacketNumber >= receivedPacketNumber) {
                return false;
            }

            _serverPacketNumber = receivedPacketNumber;
            return true;
        }
    }
}