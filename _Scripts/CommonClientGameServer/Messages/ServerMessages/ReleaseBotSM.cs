using DBDL.CommonDLL;
using ServerConsole.CommonClientGameServer.Handlers;

namespace GameServer {
    public class ReleaseBotSM : BaseServerMessage {
        public int BotId;

        public ReleaseBotSM() : base() { }
        public ReleaseBotSM(BinaryStreamReader reader) : base(reader) { }

       /* public ReleaseBotSM(EGameMessageTypeFromServer messageTypeFromServer, BinaryStreamReader reader) : base(
            messageTypeFromServer, reader) { }*/

        public ReleaseBotSM(int botId) : base() {
            BotId = botId;
        }

        public override void Accept(BaseGameServerMessagesHandler handler) {
            handler.Handle(this);
        }

      /*  protected override EGameMessageTypeFromServer GetMessageTypeFromServer() {
            return EGameMessageTypeFromServer.ReleaseBot;
        }*/

        public override void FillsFromReader(BinaryStreamReader reader) {
            BotId = reader.ReadInt();
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(BotId);
        }
    }
}