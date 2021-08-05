using DBDL.CommonDLL;

namespace GameServer.BaseClientMessages {
    public struct CheckPingMessage {
        public int SynchronizationId;

        public CheckPingMessage(int synchronizationId) {
            SynchronizationId = synchronizationId;
        }

        public CheckPingMessage(BinaryStreamReader reader) {
            SynchronizationId = reader.ReadInt32();
        }
    }
}