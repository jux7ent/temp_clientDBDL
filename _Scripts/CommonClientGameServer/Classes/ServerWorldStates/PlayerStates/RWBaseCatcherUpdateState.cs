using DBDL.CommonDLL;


namespace GameServer {
    public class RWBaseCatcherUpdateState : RWBasePlayerUpdateState {
        public ECatcherState State;
        public int CarryingPlayerId = -1;
        
        public RWBaseCatcherUpdateState(HeaderPlayerUpdateState header, BinaryStreamReader reader) : base(header, reader) {}
        
        public RWBaseCatcherUpdateState(int playerId) : base(playerId) {}

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            writer.Write((int)State);
            writer.Write(CarryingPlayerId);
        }

        public override bool BeCatcher() {
            return true;
        }

        public override void FillsFromReaderWithoutHeader(BinaryStreamReader reader) {
            State = (ECatcherState) reader.ReadInt32();
            CarryingPlayerId = reader.ReadInt32();
        }
    }
}