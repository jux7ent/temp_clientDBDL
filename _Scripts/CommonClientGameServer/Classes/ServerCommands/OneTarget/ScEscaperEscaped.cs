using DBDL.CommonDLL;

namespace GameServer {
    public class ScEscaperEscaped : BaseScOneTarget  {
        public ScEscaperEscaped() : base() {}

        public ScEscaperEscaped(int targetId) : base(targetId) { }
        
        public ScEscaperEscaped(BinaryStreamReader reader) : base(reader) {}
        
        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }
    }
}