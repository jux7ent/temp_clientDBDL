using DBDL.CommonDLL;

namespace GameServer {
    public class ScHideInGrass : BaseScOneTarget {
        public bool Hidden;
        
        public ScHideInGrass() : base() { }

        public ScHideInGrass(bool hidden, int targetId) : base(targetId) {
            Hidden = hidden;
        }

        public ScHideInGrass(BinaryStreamReader reader) : base(reader) { }

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