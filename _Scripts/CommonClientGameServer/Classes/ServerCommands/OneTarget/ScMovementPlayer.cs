using DBDL.CommonDLL;


namespace GameServer {
    public class ScMovementPlayer : BaseScOneTarget {
        public bool MovementStarted = true;
        
        public ScMovementPlayer() : base() {}

        public ScMovementPlayer(int targetId, bool movementStarted) : base(targetId) {
            MovementStarted = movementStarted;
        }
        
        public ScMovementPlayer(BinaryStreamReader reader) : base(reader) {}
        
        public override void Accept(BaseServerCommandsHandler handler) {
            handler.Handle(this);
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            
            MovementStarted = reader.ReadBoolean();
        }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            
            writer.Write(MovementStarted);
        }
    }
}