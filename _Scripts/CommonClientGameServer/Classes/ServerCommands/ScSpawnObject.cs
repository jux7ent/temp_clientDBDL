using DBDL.CommonDLL;

namespace GameServer {
    public class ScSpawnObject : BaseSc {
        public ESpawnObjectType SpawnObjectType;
        public int SpawnObjectId;
        public RWVector3 SpawnPosition = new RWVector3();
        public RWQuaternion SpawnRotation = new RWQuaternion();

        public ScSpawnObject() : base() { }
        public ScSpawnObject(BinaryStreamReader reader) : base(reader) {}

        public ScSpawnObject(ESpawnObjectType spawnObjectType, int spawnObjectId, RWVector3 spawnPosition,
            RWQuaternion spawnRotation) {
            SpawnObjectType = spawnObjectType;
            SpawnObjectId = spawnObjectId;
            SpawnPosition = spawnPosition;
            SpawnRotation = spawnRotation;
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            
            SpawnObjectType = (ESpawnObjectType) reader.ReadInt32();
            SpawnObjectId = reader.ReadInt32();
            SpawnPosition.FillsFromReader(reader);
            SpawnRotation.FillsFromReader(reader);
        }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            
            writer.Write((int) SpawnObjectType);
            writer.Write(SpawnObjectId);
            SpawnPosition.Write(writer);
            SpawnRotation.Write(writer);
        }

        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }
    }
}