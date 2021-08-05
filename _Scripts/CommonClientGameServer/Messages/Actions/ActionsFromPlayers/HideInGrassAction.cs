using DBDL.CommonDLL;

namespace GameServer {
    public class HideInGrassAction : BaseAction {
        public bool Hidden;

        public HideInGrassAction() : base() { }

        public HideInGrassAction(bool hidden) : base() {
            Hidden = hidden;
        }

        public HideInGrassAction(BinaryStreamReader reader) : base(reader) { }

        public override void Accept(BaseActionsHandler handler) {
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