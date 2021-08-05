using DBDL.CommonDLL;


namespace GameServer {
    public class ScInvokeAbility : BaseScOneTarget {
        public ScInvokeAbility() : base() {}

        public ScInvokeAbility(int targetId) : base(targetId) { }
        
        public ScInvokeAbility(BinaryStreamReader reader) : base(reader) {}
        
        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }
    }
}