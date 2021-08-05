using DBDL.CommonDLL;

namespace GameServer {
    public class GateStaticObjectState : BaseStaticObjectState {
        public GateStaticObjectState() : base() {}

        public GateStaticObjectState(int id) : base(id) {
            Progress01 = 1f;
        }
        
        public GateStaticObjectState(BinaryStreamReader reader) : base(reader) {}
        
        public static GateStaticObjectState Build(BinaryStreamReader reader) {
            return new GateStaticObjectState(reader);
        }
    }
}