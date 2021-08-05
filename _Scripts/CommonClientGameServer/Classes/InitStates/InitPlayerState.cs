using DBDL.CommonDLL;

namespace GameServer {
    public class InitPlayerState : BaseReadableWritable {
        public int Id;
        public ECharacter CharacterType;
        public LocationData LocationData = new LocationData();

        public void Update(int id, ECharacter characterType, RWVector3 pos, RWQuaternion rot) {
            Id = id;
            CharacterType = characterType;
            LocationData.Update(pos, rot);
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            Id = reader.ReadInt32();
            CharacterType = (ECharacter)reader.ReadInt32();
            LocationData.FillsFromReader(reader);
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(Id);
            writer.Write((int)CharacterType);
            LocationData.Write(writer);
        }
    }
}