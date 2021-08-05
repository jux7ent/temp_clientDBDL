using DBDL.CommonDLL;
using GameServer;
using GameServer.BaseClientMessages;
using GameServer.MessageHandlers;

namespace GameServer {
    public class WorldInitializedCM : BaseGameClientMessage {
        public WorldInitializedCM() {}
        public WorldInitializedCM(BinaryStreamReader reader) : base(reader) {}

        public override void Accept(BaseClientGameMessagesHandler handler, RoomClientInfo roomClientInfo) {
            handler.Handle(this, roomClientInfo);
        }
    }
}