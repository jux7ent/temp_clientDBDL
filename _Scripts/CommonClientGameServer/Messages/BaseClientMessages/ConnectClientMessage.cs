using DBDL.CommonDLL;

namespace GameServer.BaseClientMessages {
    public class ConnectClientMessage : BaseReadableWritable {
        public int UserId;
        
        public ConnectClientMessage() {}

        public ConnectClientMessage(int userId) : base() {
            UserId = userId;
        }
        
        public ConnectClientMessage(BinaryStreamReader reader) : base(reader) {}
        
        public override void FillsFromReader(BinaryStreamReader reader) {
            UserId = reader.ReadInt32();
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(UserId);
        }
    }
}