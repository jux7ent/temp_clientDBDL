using DBDL.CommonDLL;

namespace GameServer {
    public class UseMedkitAction : BaseAction {
        public UseMedkitAction() : base() { }

        public UseMedkitAction(BinaryStreamReader reader) : base(reader) {}
        
        public override void Accept(BaseActionsHandler handler) {
            handler.Handle(this);
        }
    }
}