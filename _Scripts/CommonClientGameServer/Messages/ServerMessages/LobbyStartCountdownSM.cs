using DBDL.CommonDLL;
using ServerConsole.CommonClientGameServer.Handlers;

namespace GameServer {
    public class LobbyStartCountdownSM : BaseServerMessage {
        public int CountdownSec;
        
        public LobbyStartCountdownSM() {}
        public LobbyStartCountdownSM(BinaryStreamReader reader) : base(reader) {}

        public LobbyStartCountdownSM(int countdownSec) : this() {
            CountdownSec = countdownSec;
        }
        
        public override void Accept(BaseGameServerMessagesHandler handler) {
            handler.Handle(this);
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            CountdownSec = reader.ReadInt32();
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(CountdownSec);
        }
    }
}