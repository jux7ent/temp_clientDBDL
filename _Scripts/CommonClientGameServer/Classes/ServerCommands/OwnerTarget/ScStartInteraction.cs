using DBDL.CommonDLL;

namespace GameServer {
    public class ScStartInteraction : BaseScOwnerTarget {
        public ScStartInteraction() : base() {}

        public ScStartInteraction(int ownerId, int targetId) : base(ownerId, targetId) { }

        public ScStartInteraction(BinaryStreamReader reader) : base(reader) {}
        
        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }
    }
}