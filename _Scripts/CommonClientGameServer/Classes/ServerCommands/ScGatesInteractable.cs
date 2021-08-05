using DBDL.CommonDLL;

namespace GameServer {
    public class ScGatesInteractable : BaseSc {
        public ScGatesInteractable() : base() { }
        public ScGatesInteractable(BinaryStreamReader reader) : base(reader) { }
        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }
    }
}