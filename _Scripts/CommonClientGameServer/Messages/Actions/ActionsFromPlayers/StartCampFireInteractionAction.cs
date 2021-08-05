using DBDL.CommonDLL;

namespace GameServer {
    public class StartCampFireInteractionAction : BaseActionWithTarget {
        public StartCampFireInteractionAction() : base() {}
        public StartCampFireInteractionAction(BinaryStreamReader reader) : base(reader) {}

        public override void Accept(BaseActionsHandler handler) {
            handler.Handle(this);
        }
    }
}