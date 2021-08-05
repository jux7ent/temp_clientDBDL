using DBDL.CommonDLL;


namespace GameServer {
    public class MovementAction : BaseAction {
        public bool MovementStarted = false;

        public MovementAction() : base() { }

        public MovementAction(BinaryStreamReader reader) : base(reader) {}

        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            MovementStarted = reader.ReadBoolean();
        }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            writer.Write(MovementStarted);
        }
        
        public override void Accept(BaseActionsHandler handler) {
            handler.Handle(this);
        }
    }
}