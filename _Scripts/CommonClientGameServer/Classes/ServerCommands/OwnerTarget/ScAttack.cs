using DBDL.CommonDLL;

namespace GameServer {
    public class ScAttack : BaseScOneTarget {
        public ScAttack() : base() {}

        public ScAttack(int targetId) : base(targetId) { }

        public ScAttack(BinaryStreamReader reader) : base(reader) {}
        
        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }
    }
}