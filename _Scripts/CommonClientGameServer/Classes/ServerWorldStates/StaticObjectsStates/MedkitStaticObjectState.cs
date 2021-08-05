using DBDL.CommonDLL;

namespace GameServer {
    public class MedkitStaticObjectState : BaseStaticObjectState {
        public MedkitStaticObjectState() : base() {}

        public MedkitStaticObjectState(int id) : base(id) {
            Progress01 = 1f;
        }
        
        public MedkitStaticObjectState(BinaryStreamReader reader) : base(reader) {}
        
        public static MedkitStaticObjectState Build(BinaryStreamReader reader) {
            return new MedkitStaticObjectState(reader);
        }
    }
}