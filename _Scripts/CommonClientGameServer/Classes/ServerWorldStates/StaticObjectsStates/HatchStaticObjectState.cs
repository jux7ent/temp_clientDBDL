using DBDL.CommonDLL;

namespace GameServer {
    public class HatchStaticObjectState : BaseStaticObjectState {
        public HatchStaticObjectState() : base() {}
        public HatchStaticObjectState(int id) : base(id) {}
        public HatchStaticObjectState(BinaryStreamReader reader) : base(reader) {}
    }
}