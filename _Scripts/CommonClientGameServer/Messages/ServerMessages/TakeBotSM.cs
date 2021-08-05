using DBDL.CommonDLL;
using ServerConsole.CommonClientGameServer.Handlers;

namespace GameServer {
    public class TakeBotSM : BaseServerMessage {
        public int BotId;
        
        public TakeBotSM() : base() {}
        public TakeBotSM(BinaryStreamReader reader) : base(reader) {}
       /* public TakeBotSM(EGameMessageTypeFromServer messageTypeFromServer, BinaryStreamReader reader) : base(
            messageTypeFromServer, reader) { }*/

        public TakeBotSM(int botId) : this() {
            BotId = botId;
        }
        
        public override void Accept(BaseGameServerMessagesHandler handler) {
            handler.Handle(this);
        }

     /*   protected override EGameMessageTypeFromServer GetMessageTypeFromServer() {
            return EGameMessageTypeFromServer.TakeBot;
        }*/

        public override void FillsFromReader(BinaryStreamReader reader) {
            BotId = reader.ReadInt32();
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(BotId);
        }
    }
}