using DBDL.CommonDLL;


namespace GameServer {
    public class LocationData : BaseReadableWritable {
        public RWVector3 Position = new RWVector3();
        public RWQuaternion Rotation = new RWQuaternion(); 

        public LocationData() { }

        public void Update(LocationData locationData) {
            Update(locationData.Position, locationData.Rotation);
        }

        public void Update(RWVector3 position, RWQuaternion rotation) {
            Position.Update(position);
            Rotation.Update(rotation);
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            Position.FillsFromReader(reader);
            Rotation.FillsFromReader(reader);
        }

        public override void Write(BinaryStreamWriter writer) {
            Position.Write(writer);
            Rotation.Write(writer);
        }
    }
}