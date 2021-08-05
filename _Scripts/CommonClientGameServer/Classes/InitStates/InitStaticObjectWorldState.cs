using DBDL.CommonDLL;

namespace GameServer {
    public class InitStaticObjectWorldState : BaseReadableWritable {
        public int Id;
        public LocationData LocationData = new LocationData();
        
        public InitStaticObjectWorldState() {}

        public InitStaticObjectWorldState(int id, RWVector3 position, RWQuaternion rotation) {
            Id = id;
            LocationData.Update(position, rotation);
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            Id = reader.ReadInt32();
            LocationData.FillsFromReader(reader);
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(Id);
            LocationData.Write(writer);
        }
    }
}