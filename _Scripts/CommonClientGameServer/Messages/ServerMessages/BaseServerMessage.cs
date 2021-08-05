using DBDL.CommonDLL;
using ServerConsole.CommonClientGameServer.Handlers;

namespace GameServer {
    public abstract class BaseServerMessage : BaseReadableWritable {

        public BaseServerMessage() { }

        public BaseServerMessage(BinaryStreamReader reader) : this() {
            FillsFromReader(reader);
        }

        public abstract void Accept(BaseGameServerMessagesHandler handler);
    }
}