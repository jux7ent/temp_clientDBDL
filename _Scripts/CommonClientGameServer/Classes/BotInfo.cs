using System.Net;

namespace GameServer {
    public class BotInfo {
        public readonly int PlayerId;
        public IPEndPoint OwnerEndPoint;

        public BotInfo(int playerId) {
            PlayerId = playerId;
            OwnerEndPoint = null;
        }
    }
}