using DBDL.CommonDLL;
using ServerConsole.CommonClientGameServer.Handlers;


namespace GameServer {
    public class InitWorldStateSM : BaseServerMessage {
        public readonly RWList<InitPlayerState> Players = new RWList<InitPlayerState>();
        public readonly RWList<InitStaticObjectWorldState> Cages = new RWList<InitStaticObjectWorldState>();
        public readonly RWList<InitStaticObjectWorldState> CampFires = new RWList<InitStaticObjectWorldState>();
        public readonly RWList<InitStaticObjectWorldState> Gates = new RWList<InitStaticObjectWorldState>();
        public InitStaticObjectWorldState Hatch = new InitStaticObjectWorldState();
        public readonly RWList<InitStaticObjectWorldState> Medkits = new RWList<InitStaticObjectWorldState>();

        public InitWorldStateSM() { }
        public InitWorldStateSM(BinaryStreamReader reader) : base(reader) {}
       /* public InitWorldStateSM(EGameMessageTypeFromServer messageTypeFromServer, BinaryStreamReader reader) : base(
            messageTypeFromServer, reader) { }*/

        public override void Accept(BaseGameServerMessagesHandler handler) {
            handler.Handle(this);
        }

      /*  protected override EGameMessageTypeFromServer GetMessageTypeFromServer() {
            return EGameMessageTypeFromServer.InitWorldState;
        }*/

        public override void FillsFromReader(BinaryStreamReader reader) {
            Players.FillsFromReader(reader);
            Cages.FillsFromReader(reader);
            CampFires.FillsFromReader(reader);
            Gates.FillsFromReader(reader);
            Hatch.FillsFromReader(reader);
            Medkits.FillsFromReader(reader);
        }

        public override void Write(BinaryStreamWriter writer) {
            Players.Write(writer);
            Cages.Write(writer);
            CampFires.Write(writer);
            Gates.Write(writer);
            Hatch.Write(writer);
            Medkits.Write(writer);
        }
    }
}