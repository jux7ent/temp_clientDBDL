using System;
using System.Collections.Generic;
using System.Net;
using DBDL.CommonDLL.Responses;

namespace GameServer {
    public class RoomClientInfo {
        public IPEndPoint ClientEndPoint;
        public readonly int UserId;
        public readonly int PlayerId;
        public readonly GetUserInfoForGameServerResponse UserInfo;
        public long TimestampLastReceivedMessage;
        public readonly HashSet<int> ControlledBotIds = new HashSet<int>();
        public bool Disconnected;

        private int _serverPacketNumber;
        private int _clientPacketNumber;

        public bool WorldInitialized;

        public RoomClientInfo(IPEndPoint clientEndEndPoint, int userId, int playerId, GetUserInfoForGameServerResponse userInfo) {
            ClientEndPoint = clientEndEndPoint;
            UserId = userId;
            PlayerId = playerId;
            UserInfo = userInfo;
            

            _serverPacketNumber = 0;
            _clientPacketNumber = 0;

            WorldInitialized = false;

            TimestampLastReceivedMessage = DateTime.Now.ToFileTimeUtc();
        }

        public override bool Equals(object? obj) {
            RoomClientInfo roomClientInfo = (RoomClientInfo) obj;
            return UserId == (roomClientInfo).UserId && ClientEndPoint.Equals(roomClientInfo.ClientEndPoint);
        }

        public int GetServerPacketNumber() {
            return _serverPacketNumber++;
        }

        public bool CheckClientPacketNumber(int packetNumber) {
            if (packetNumber < _clientPacketNumber) {
                return false;
            }

            _clientPacketNumber = packetNumber;
            return true;
        }

        public long GetPingMs() {
            double sec = (double) ((DateTime.Now.ToFileTimeUtc() - TimestampLastReceivedMessage)) /
                         Timeouts.Sec2NanoSec;
            return (long) (sec * 1000);
        }

        public void FlushClientPacketNumber() {
            _clientPacketNumber = -1;
        }
    }
}