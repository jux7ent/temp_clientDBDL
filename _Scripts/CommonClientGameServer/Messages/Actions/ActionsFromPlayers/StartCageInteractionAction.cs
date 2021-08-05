using DBDL.CommonDLL;


namespace GameServer {
    public class StartCageInteractionAction : BaseActionWithTarget {
        public StartCageInteractionAction() : base() {}
        public StartCageInteractionAction(BinaryStreamReader reader) : base(reader) {}

        public override void Accept(BaseActionsHandler handler) {
            handler.Handle(this);
        }
    }
}