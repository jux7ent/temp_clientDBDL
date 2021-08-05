using DBDL.CommonDLL;

namespace GameServer {
    public class TakeMedkitAction : BaseActionWithTarget {
        public TakeMedkitAction() : base() {}
        public TakeMedkitAction(BinaryStreamReader reader) : base(reader) {}
        public override void Accept(BaseActionsHandler handler) {
            handler.Handle(this);
        }
    }
}