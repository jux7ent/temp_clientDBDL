using DBDL.CommonDLL;
using GameServer.BaseClientMessages;
using GameServer.MessageHandlers;


namespace GameServer {

    public class PlayerUpdateCM : BaseGameClientMessage {
        public int LastHandledServerCommandId;
        public readonly LocationData LocationData = new LocationData();
        public readonly RWActionsQueue ActionsQueue = new RWActionsQueue(5);
        public readonly BotsUpdateData BotsUpdateData = new BotsUpdateData();
        
        public PlayerUpdateCM() : base() { }
        public PlayerUpdateCM(BinaryStreamReader reader) : base(reader) {}

        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            
            LastHandledServerCommandId = reader.ReadInt();
            LocationData.FillsFromReader(reader);
            ActionsQueue.FillsFromReader(reader);
            BotsUpdateData.FillsFromReader(reader);
        }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            
            writer.Write(LastHandledServerCommandId);
            LocationData.Write(writer);
            ActionsQueue.Write(writer);
            BotsUpdateData.Write(writer);
        }

        public override void Accept(BaseClientGameMessagesHandler handler, RoomClientInfo roomClientInfo) {
            handler.Handle(this, roomClientInfo);
        }
    }
}