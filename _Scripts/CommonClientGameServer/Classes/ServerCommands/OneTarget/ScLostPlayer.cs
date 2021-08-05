using DBDL.CommonDLL;

namespace GameServer {
    public class ScLostPlayer : BaseScOneTarget {
        public ScLostPlayer() : base() {}

        public ScLostPlayer(int targetId) : base(targetId) { }
        
        public ScLostPlayer(BinaryStreamReader reader) : base(reader) {}
        
        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }
    }
}