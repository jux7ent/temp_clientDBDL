using DBDL.CommonDLL;
using GameServer;
using GameServer.BaseClientMessages;
using GameServer.MessageHandlers;

namespace GameServer {
    public class BotTakenCM : BaseGameClientMessage {
        public int BotId;
        
        public BotTakenCM() : base() {}
        public BotTakenCM(BinaryStreamReader reader) : base(reader) {}

        public BotTakenCM(int botId) : base() {
            BotId = botId;
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            BotId = reader.ReadInt32();
        }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            writer.Write(BotId);
        }

        public override void Accept(BaseClientGameMessagesHandler handler, RoomClientInfo roomClientInfo) {
            handler.Handle(this, roomClientInfo);
        }
    }
}