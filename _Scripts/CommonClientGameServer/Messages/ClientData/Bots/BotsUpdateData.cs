using DBDL.CommonDLL;
using GameServer.BaseClientMessages;
using GameServer.MessageHandlers;

namespace GameServer {
    public class BotsUpdateData : BaseReadableWritable {
        public readonly RWList<BotUpdateData> BotsUpdateCms = new RWList<BotUpdateData>();

        public BotsUpdateData() { }
        public BotsUpdateData(BinaryStreamReader reader) : base(reader) {}
        public override void FillsFromReader(BinaryStreamReader reader) {
            BotsUpdateCms.FillsFromReader(reader);
        }

        public override void Write(BinaryStreamWriter writer) {
            BotsUpdateCms.Write(writer);
        }
    }
}