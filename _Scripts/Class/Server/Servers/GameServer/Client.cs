using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using DBDL.CommonDLL;
using Kuhpik;
using ServerConsole;
using UnityEngine;

public class Client {
    private class UdpState {
        public IPEndPoint EndPoint = new IPEndPoint(IPAddress.Any, 0);
    }

    public event Action<byte[]> OnDataReceived;
    public bool Connected { get; private set; }

    private UdpClient udpClient;
    private ManualResetEvent dataReceived = new ManualResetEvent(false);
    private byte[] lastReceivedMessage = null;

    public Client(IPEndPoint gameServerEndPoint) {
        udpClient = InitializeSocket(gameServerEndPoint);
        OnDataReceived += UpdateLastReceivedMessage;
    }

    public void Connect() {
        Connected = true;
        StartReceiving(new UdpState());
    }

    public void Disconnect() {
        Connected = false;
        udpClient.Close();
    }

    public void SendPacket(byte[] data) {
        udpClient.BeginSend(data, data.Length, OnPacketSent, null);
    }

    public Task<byte[]> WaitMessage() {
        Task<byte[]> task = new Task<byte[]>(WaitData);
        task.Start();

        return task;
    }

    private byte[] WaitData() {
        dataReceived.WaitOne();
        return lastReceivedMessage;
    }

    private void UpdateLastReceivedMessage(byte[] lastReceivedMessage) {
        this.lastReceivedMessage = lastReceivedMessage;
        dataReceived.Set();
    }

    private UdpClient InitializeSocket(IPEndPoint serverEndPoint) {
        UdpClient result = new UdpClient(0);
        result.Connect(serverEndPoint);
        return result;
    }

    private void StartReceiving(UdpState udpState) {
        udpClient.BeginReceive(OnMessageReceived, udpState);
        dataReceived.Reset();
    }

    private void OnMessageReceived(IAsyncResult asyncResult) {
        try {
            OnDataReceived?.Invoke(
                udpClient.EndReceive(asyncResult, ref ((UdpState) (asyncResult.AsyncState)).EndPoint));
        }
        catch (Exception exception) {
            Printer.PrintError($"Error while handling received message from server\n{exception}");
        }

        StartReceiving((UdpState) asyncResult.AsyncState);
    }

    private void OnPacketSent(IAsyncResult asyncResult) {
        int bytesSent = udpClient.EndSend(asyncResult);
    }
}