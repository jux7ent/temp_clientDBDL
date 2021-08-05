using DBDL.CommonDLL;

namespace GameServer {
    public abstract class BaseScOwnerTarget : BaseScOneTarget {
        public int OwnerId;
        
        public BaseScOwnerTarget() : base() {}

        public BaseScOwnerTarget(int ownerId, int targetId) : base(targetId) {
            OwnerId = ownerId;
        }
        
        public BaseScOwnerTarget(BinaryStreamReader reader) : base(reader) {}

        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            
            OwnerId = reader.ReadInt32();
        }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            
            writer.Write(OwnerId);
        }
    }
}