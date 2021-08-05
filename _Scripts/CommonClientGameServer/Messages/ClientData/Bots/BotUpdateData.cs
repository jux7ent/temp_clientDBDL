using DBDL.CommonDLL;


namespace GameServer {
    public class BotUpdateData : BaseReadableWritable {
        public int BotId;
        public readonly LocationData LocationData = new LocationData();
        
        public BotUpdateData() {}

        public BotUpdateData(int botId) {
            BotId = botId;
        }

        public void Update(LocationData locationData) {
            LocationData.Update(locationData);
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            BotId = reader.ReadInt32();
            LocationData.FillsFromReader(reader);
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(BotId);
            LocationData.Write(writer);
        }
    }
}