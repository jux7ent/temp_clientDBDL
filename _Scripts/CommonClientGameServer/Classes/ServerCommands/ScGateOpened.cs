using DBDL.CommonDLL;

namespace GameServer {
    public class ScGateOpened : BaseSc {
        public ScGateOpened() : base() { }

        public ScGateOpened(BinaryStreamReader reader) : base(reader) { }

        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }
    }
}