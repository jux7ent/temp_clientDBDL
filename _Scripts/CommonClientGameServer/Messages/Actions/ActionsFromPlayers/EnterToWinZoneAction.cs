using DBDL.CommonDLL;

namespace GameServer {
    public class EnterToWinZoneAction : BaseAction {
        public EnterToWinZoneAction() : base() { }

        public EnterToWinZoneAction(BinaryStreamReader reader) : base(reader) { }

        public override void Accept(BaseActionsHandler handler) {
            handler.Handle(this);
        }
    }
}