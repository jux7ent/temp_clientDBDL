using DBDL.CommonDLL;

namespace GameServer {
    public struct MessageHeader {
        public const byte DefaultStartByte = 7;
        
        public byte StartByte;
        public EMessageType MessageType;
        public int UserId;

        public MessageHeader(EMessageType messageType, int userId) {
            StartByte = DefaultStartByte;
            MessageType = messageType;
            UserId = userId;
        }

        public MessageHeader(BinaryStreamReader reader) {
            StartByte = reader.ReadByte();
            MessageType = (EMessageType) reader.ReadEnum();
            UserId = reader.ReadInt32();
        }

        public void Write(BinaryStreamWriter writer) {
            writer.Write(StartByte);
            writer.Write(MessageType);
            writer.Write(UserId);
        }
    }
}