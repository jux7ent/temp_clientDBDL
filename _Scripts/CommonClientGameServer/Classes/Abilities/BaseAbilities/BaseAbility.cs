using DBDL.CommonDLL;


namespace GameServer {
    public abstract class BaseAbility : BaseReadableWritable {
        public int OwnerId;

        public BaseAbility(int playerId) {
            OwnerId = playerId;
        }

        public BaseAbility() {
        }

        public BaseAbility(BinaryStreamReader reader) {
            FillsFromReader(reader);
        }

        public abstract bool Accept(BaseAbilitiesHandler abilitiesHandler);

        public override void FillsFromReader(BinaryStreamReader reader) {
            OwnerId = reader.ReadInt();
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(OwnerId);
        }
    }
}