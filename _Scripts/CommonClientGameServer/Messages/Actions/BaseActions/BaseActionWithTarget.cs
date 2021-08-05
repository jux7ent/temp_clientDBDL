using DBDL.CommonDLL;


namespace GameServer {
    public abstract class BaseActionWithTarget : BaseAction {
        public int TargetId = -1;
        
        public BaseActionWithTarget() : base() {}

        public BaseActionWithTarget(BinaryStreamReader reader) : base(reader) {}

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