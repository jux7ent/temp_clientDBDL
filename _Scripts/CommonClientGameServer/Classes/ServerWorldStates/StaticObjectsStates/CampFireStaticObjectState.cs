using DBDL.CommonDLL;

namespace GameServer {
    public class CampFireStaticObjectState : BaseStaticObjectState {
        public CampFireStaticObjectState() : base() {}
        public CampFireStaticObjectState(int id) : base(id) {}
        public CampFireStaticObjectState(BinaryStreamReader reader) : base(reader) {}

        public static CampFireStaticObjectState Build(BinaryStreamReader reader) {
            return new CampFireStaticObjectState(reader);
        }
    }
}