using DBDL.CommonDLL;

namespace GameServer {
    public class ScEscaperHealing : BaseScOneTarget {
        public bool HeadlingStarted = false;
        
        public ScEscaperHealing() : base() { }

        public ScEscaperHealing(bool headlingStarted, int targetId) : base(targetId) {
            HeadlingStarted = headlingStarted;
        }

        public ScEscaperHealing(BinaryStreamReader reader) : base(reader) { }

        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }
        
        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            
            HeadlingStarted = reader.ReadBoolean();
        }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            
            writer.Write(HeadlingStarted);
        }
    }
}