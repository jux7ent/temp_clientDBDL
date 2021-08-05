using DBDL.CommonDLL;


namespace GameServer {
    public class PlaceInCageAction : BaseActionWithTarget {
        public PlaceInCageAction() : base() {}
        public PlaceInCageAction(BinaryStreamReader reader) : base(reader) {}

        public override void Accept(BaseActionsHandler handler) {
            handler.Handle(this);
        }
    }
}