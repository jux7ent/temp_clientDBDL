using DBDL.CommonDLL;

namespace GameServer {
    public class ScReceiveDamage : BaseScOneTarget {
        public ScReceiveDamage() : base() { }

        public ScReceiveDamage(int targetId) : base(targetId) { }

        public ScReceiveDamage(BinaryStreamReader reader) : base(reader) { }

        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }
    }
}