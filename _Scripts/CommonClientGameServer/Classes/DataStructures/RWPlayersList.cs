using System.Collections.Generic;
using DBDL.CommonDLL;


namespace GameServer {
    public class RWPlayersList : BaseReadableWritable {
        private readonly List<RWBasePlayerUpdateState> list = new List<RWBasePlayerUpdateState>();
        private readonly HeaderPlayerUpdateState playerHeader = new HeaderPlayerUpdateState();

        public void Add(RWBasePlayerUpdateState playerUpdateState) {
            list.Add(playerUpdateState);
        }

        public int Count => list.Count;

        public RWBasePlayerUpdateState this[int key] {
            get => list[key];
            set => list[key] = value;
        }
        
        public override void FillsFromReader(BinaryStreamReader reader) {
            list.Clear();
            
            int count = reader.ReadInt32();

            for (int i = 0; i < count; ++i) {
                playerHeader.FillsFromReader(reader);

                if (playerHeader.IsCatcher) {
                    list.Add(new RWBaseCatcherUpdateState(playerHeader, reader));
                } else {
                    list.Add(new RWBaseEscaperUpdateState(playerHeader, reader));
                }
            }
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(list.Count);

            for (int i = 0; i < list.Count; ++i) {
                list[i].Write(writer);
            }
        }
    }
}