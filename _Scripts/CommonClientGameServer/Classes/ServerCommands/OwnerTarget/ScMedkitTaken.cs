using DBDL.CommonDLL;

namespace GameServer {
    public class ScMedkitTaken : BaseScOwnerTarget {
        public ScMedkitTaken() : base() {}

        public ScMedkitTaken(int ownerId, int targetId) : base(ownerId, targetId) { }

        public ScMedkitTaken(BinaryStreamReader reader) : base(reader) {}
        
        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }
    }
}