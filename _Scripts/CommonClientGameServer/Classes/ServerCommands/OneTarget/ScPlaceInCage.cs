using DBDL.CommonDLL;

namespace GameServer {
    public class ScPlaceInCage : BaseScOneTarget {
        public ScPlaceInCage() : base() {}

        public ScPlaceInCage(int targetId) : base(targetId) { }
        
        public ScPlaceInCage(BinaryStreamReader reader) : base(reader) {}
        
        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }
    }
}