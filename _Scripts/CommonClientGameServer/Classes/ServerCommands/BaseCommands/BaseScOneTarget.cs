using DBDL.CommonDLL;


namespace GameServer {
    public abstract class BaseScOneTarget : BaseSc {
        public int TargetId;

        public BaseScOneTarget() : base() {}

        public BaseScOneTarget(int targetId) : base() {
            TargetId = targetId;
        }
        
        public BaseScOneTarget(BinaryStreamReader reader) : base(reader) {}

        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            
            TargetId = reader.ReadInt32();
        }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            
            writer.Write(TargetId);
        }
    }
}