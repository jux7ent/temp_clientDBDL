using DBDL.CommonDLL;


namespace GameServer {
    public class StartHatchInteractionAction : BaseActionWithTarget {
        public StartHatchInteractionAction() : base() { }
        public StartHatchInteractionAction(BinaryStreamReader reader) : base(reader) { }

        public override void Accept(BaseActionsHandler handler) {
            handler.Handle(this);
        }
    }
}