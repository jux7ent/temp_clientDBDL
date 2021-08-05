using DBDL.CommonDLL;

namespace GameServer {
    public abstract class BaseAction : BaseReadableWritable {
        public int Id;
        public int OwnerId;

        public abstract void Accept(BaseActionsHandler handler);

        public BaseAction() {
        }
        
        public BaseAction(int id, int ownerId) {
            Id = id;
            OwnerId = ownerId;
        }

        public BaseAction(BinaryStreamReader reader) {
            FillsFromReader(reader);
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            Id = reader.ReadInt();
            OwnerId = reader.ReadInt();
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(Id);
            writer.Write(OwnerId);
        }
    }
}