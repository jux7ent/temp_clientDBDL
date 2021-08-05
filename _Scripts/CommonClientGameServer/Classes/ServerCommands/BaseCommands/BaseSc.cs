using System;
using DBDL.CommonDLL;


namespace GameServer {
    public abstract class BaseSc : BaseReadableWritable {
        public int Id;

        public BaseSc() { }

        public BaseSc(BinaryStreamReader reader) : base() {
            FillsFromReader(reader);
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            Id = reader.ReadInt();
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(Id);
        }
        
        public abstract void Accept(BaseServerCommandsHandler handler);
    }
}