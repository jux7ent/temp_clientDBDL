using System;
using System.Diagnostics;
using DBDL.CommonDLL;


namespace GameServer {
    public struct GameMessageHeader {
        public int PacketNumber;

        public GameMessageHeader(int packetNumber) {
            PacketNumber = packetNumber;
        }

        public GameMessageHeader(BinaryStreamReader reader) {
            PacketNumber = reader.ReadInt32();
        }

        public void Write(BinaryStreamWriter writer) {
            writer.Write(PacketNumber);
        }
    }
}