using DBDL.CommonDLL;
using GameServer.MessageHandlers;

namespace GameServer.BaseClientMessages {
    public abstract class BaseGameClientMessage : BaseReadableWritable {
        public int UserId;
        public int PacketNumber;

        public BaseGameClientMessage() {
        }

        public BaseGameClientMessage(BinaryStreamReader reader) : this() {
            FillsFromReader(reader);
        }
        
        public override void FillsFromReader(BinaryStreamReader reader) {
            UserId = reader.ReadInt();
            PacketNumber = reader.ReadInt();
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(UserId);
            writer.Write(PacketNumber);
        }
        
        public abstract void Accept(BaseClientGameMessagesHandler handler, RoomClientInfo roomClientInfo);
    }
}