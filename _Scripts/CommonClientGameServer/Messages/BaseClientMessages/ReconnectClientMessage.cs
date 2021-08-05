using DBDL.CommonDLL;

namespace GameServer.BaseClientMessages {
    public class ReconnectClientMessage : BaseReadableWritable {
        public int UserId;
        
        public ReconnectClientMessage() {}

        public ReconnectClientMessage(int userId) : base() {
            UserId = userId;
        }
        
        public ReconnectClientMessage(BinaryStreamReader reader) : base(reader) {}
        
        public override void FillsFromReader(BinaryStreamReader reader) {
            UserId = reader.ReadInt32();
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(UserId);
        }
    }
}