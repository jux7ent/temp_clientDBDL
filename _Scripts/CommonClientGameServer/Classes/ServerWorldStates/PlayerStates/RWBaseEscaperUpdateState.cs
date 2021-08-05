using DBDL.CommonDLL;


namespace GameServer {
    public class RWBaseEscaperUpdateState : RWBasePlayerUpdateState {
        public EEscaperState State;
        public int Health = 2;
        public float DieProgress = 0f;
        public int MedkitsCount = 1;
        
        public RWBaseEscaperUpdateState(HeaderPlayerUpdateState header, BinaryStreamReader reader) : base(header, reader) {}
        
        public RWBaseEscaperUpdateState(int playerId) : base(playerId) {}

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            writer.Write((int)State);
            writer.Write(Health);
            writer.Write(DieProgress);
            writer.Write(MedkitsCount);
        }

        public override bool BeCatcher() {
            return false;
        }

        public override void FillsFromReaderWithoutHeader(BinaryStreamReader reader) {
            State = (EEscaperState) reader.ReadInt32();
            Health = reader.ReadInt32();
            DieProgress = reader.ReadSingle();
            MedkitsCount = reader.ReadInt();
        }
    }
}