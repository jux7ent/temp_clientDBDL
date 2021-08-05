using DBDL.CommonDLL;


namespace GameServer {
    public class ScTakeInBag : BaseScOwnerTarget {
        public ScTakeInBag() : base() {}

        public ScTakeInBag(int ownerId, int targetId) : base(ownerId, targetId) { }

        public ScTakeInBag(BinaryStreamReader reader) : base(reader) {}
        
        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }
    }
}