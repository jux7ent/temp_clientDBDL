using DBDL.CommonDLL;

namespace GameServer {
    public class ScHidePlayer : BaseScOneTarget {
        public bool Hidden = true;
        
        public ScHidePlayer() : base() {}

        public ScHidePlayer(int targetId, bool hidden) : base(targetId) {
            Hidden = hidden;
        }
        
        public ScHidePlayer(BinaryStreamReader reader) : base(reader) {}

        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            
            Hidden = reader.ReadBoolean();
        }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            
            writer.Write(Hidden);
        }
    }
}