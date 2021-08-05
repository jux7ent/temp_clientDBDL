using DBDL.CommonDLL;


namespace GameServer {
    public class BaseStaticObjectState : BaseReadableWritable {
        public int Id;
        public float Progress01 = 0f;
        
        public BaseStaticObjectState() : this(-1) {}

        public BaseStaticObjectState(BinaryStreamReader reader) : base(reader) { }

        public BaseStaticObjectState(int id) {
            Id = id;
        }
        
        public override void FillsFromReader(BinaryStreamReader reader) {
            Id = reader.ReadInt32();
            Progress01 = reader.ReadSingle();
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(Id);
            writer.Write(Progress01);
        }
    }
}