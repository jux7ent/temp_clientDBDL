using DBDL.CommonDLL;

namespace GameServer {
    public class AttackAction : BaseAction {
        public AttackAction() : base() {}
        public AttackAction(BinaryStreamReader reader) : base(reader) {}

        public override void Accept(BaseActionsHandler handler) {
            handler.Handle(this);
        }
    }
}