using DBDL.CommonDLL;

namespace GameServer {
    public class ScDestroyObject : BaseScOneTarget {
        public ScDestroyObject() : base() {}

        public ScDestroyObject(int targetId) : base(targetId) { }
        
        public ScDestroyObject(BinaryStreamReader reader) : base(reader) {}
        
        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }
    }
}