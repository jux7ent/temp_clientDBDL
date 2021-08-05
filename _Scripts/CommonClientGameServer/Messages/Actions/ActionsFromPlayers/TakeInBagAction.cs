using DBDL.CommonDLL;


namespace GameServer {
    public class TakeInBagAction : BaseActionWithTarget {
        public TakeInBagAction() : base() {}
        public TakeInBagAction(BinaryStreamReader reader) : base(reader) {}
        
        public override void Accept(BaseActionsHandler handler) {
            handler.Handle(this);
        }
    }
}