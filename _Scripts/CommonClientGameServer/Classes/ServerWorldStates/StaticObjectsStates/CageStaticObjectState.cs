using DBDL.CommonDLL;


namespace GameServer {
    public class CageStaticObjectState : BaseStaticObjectState {
        public int ContainsPlayerId = -1;
        
        public CageStaticObjectState() : base() {}
        
        public CageStaticObjectState(BinaryStreamReader reader) : base(reader) { }
        

        public CageStaticObjectState(int id) : base(id) {
            Progress01 = 1f;
        }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            writer.Write(ContainsPlayerId);
        }
        
        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            ContainsPlayerId = reader.ReadInt32();
        }

        public static CageStaticObjectState Build(BinaryStreamReader reader) {
            return new CageStaticObjectState(reader);
        }
    }
}