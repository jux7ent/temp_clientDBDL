using DBDL.CommonDLL;

namespace GameServer {
    public class StartGateInteractionAction : BaseActionWithTarget {
        public StartGateInteractionAction() : base() {}
        public StartGateInteractionAction(BinaryStreamReader reader) : base(reader) {}

        public override void Accept(BaseActionsHandler handler) {
            handler.Handle(this);
        }
    }
}