using DBDL.CommonDLL;


namespace GameServer {
    public class UseAbilityAction : BaseAction {
        public UseAbilityAction() : base() { }

        public UseAbilityAction(BinaryStreamReader reader) : base(reader) { }

        public override void Accept(BaseActionsHandler handler) {
            handler.Handle(this);
        }
    }
}