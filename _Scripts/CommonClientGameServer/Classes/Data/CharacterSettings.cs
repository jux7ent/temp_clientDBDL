using System;
using DBDL.CommonDLL;


namespace GameServer {
    [Serializable]
    public class CharacterSettings : BaseReadableWritable {
        public ECharacter CharacterType;
        public float MovementSpeed;
        public float AngularSpeed;
        public float FovRadius;
        public float FovAngle;

        public void Update(CharacterSettings characterSettings) {
            MovementSpeed = characterSettings.MovementSpeed;
            AngularSpeed = characterSettings.AngularSpeed;
            FovRadius = characterSettings.FovRadius;
            FovAngle = characterSettings.FovAngle;
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            CharacterType = (ECharacter) reader.ReadInt32();
            MovementSpeed = reader.ReadSingle();
            AngularSpeed = reader.ReadSingle();
            FovRadius = reader.ReadSingle();
            FovAngle = reader.ReadSingle();
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write((int)CharacterType);
            writer.Write(MovementSpeed);
            writer.Write(AngularSpeed);
            writer.Write(FovRadius);
            writer.Write(FovAngle);
        }
    }
}