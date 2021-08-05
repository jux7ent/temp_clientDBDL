using DBDL.CommonDLL;
using DBDL.CommonDLL.Interfaces;
using GameServer.BaseClientMessages;
using GameServer.MessageHandlers;


namespace GameServer {
    public class LobbyInfoCM : BaseGameClientMessage, IUpdatable<LobbyInfoCM> {
        public ECharacter CharacterType;
        public bool ReadyState;
        
        public LobbyInfoCM() : base() {}
        public LobbyInfoCM(BinaryStreamReader reader) : base(reader) {}
        
        public LobbyInfoCM Update(LobbyInfoCM copyFrom) {
            CharacterType = copyFrom.CharacterType;
            ReadyState = copyFrom.ReadyState;
            
            return this;
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            CharacterType = (ECharacter) reader.ReadEnum();
            ReadyState = reader.ReadBoolean();
        }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            writer.Write(CharacterType);
            writer.Write(ReadyState);
        }

        public override void Accept(BaseClientGameMessagesHandler handler, RoomClientInfo roomClientInfo) {
            handler.Handle(this, roomClientInfo);
        }

        public static LobbyInfoCM GetDefault(bool isCatcher) {
            LobbyInfoCM lobbyInfoCm = new LobbyInfoCM();
            lobbyInfoCm.CharacterType = isCatcher ? ECharacter.Shade : ECharacter.Egglet;
            lobbyInfoCm.ReadyState = false;

            return lobbyInfoCm;
        }
    }
}