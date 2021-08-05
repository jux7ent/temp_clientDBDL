using System.Reflection;
using DBDL.CommonDLL;
using ServerConsole.CommonClientGameServer.Handlers;


namespace GameServer {
    public class ConnectedSM : BaseServerMessage {
        public int PlayerId;
        public RWAssemblyClassesDict AssemblyClassesDict = new RWAssemblyClassesDict();

        private byte[] rwAssemblyClassesTable;

        public ConnectedSM() : base() { }
        public ConnectedSM(BinaryStreamReader reader) : base(reader) { }

        public ConnectedSM(int playerId, byte[] rwAssemblyClassesTable) {
            PlayerId = playerId;
            this.rwAssemblyClassesTable = rwAssemblyClassesTable;
        }

        public override void Accept(BaseGameServerMessagesHandler handler) {
            handler.Handle(this);
        }
        
        public override void FillsFromReader(BinaryStreamReader reader) {
            PlayerId = reader.ReadInt();
            AssemblyClassesDict.FillsFromReader(reader);
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(PlayerId);
            writer.Write(rwAssemblyClassesTable);
        }

     /*   protected override EGameMessageTypeFromServer GetMessageTypeFromServer() {
            return EGameMessageTypeFromServer.Connected;
        }

        protected override void FillsFromReaderWithoutType(BinaryStreamReader reader) {
            PlayerId = reader.ReadInt32();
            AssemblyClassesDict.FillsFromReader(reader);
        }

        protected override void WriteWithoutType(BinaryStreamWriter writer) {
            writer.Write(PlayerId);
            writer.Write(rwAssemblyClassesDict);
        }*/
    }
}