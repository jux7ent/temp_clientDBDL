using DBDL.CommonDLL;


namespace GameServer {
    public enum ECanDo {
        Move,
        Interact,
    }
    
    public class BitMap {
        private const int Length = 8;
        private readonly byte[] bytes = new byte[Length];

        public BitMap(params ECanDo[] value) {
            for (int i = 0; i < Length; ++i) {
                bytes[i] = 0;
            }
            
            SetValues(value);
        }

        public void Update(BitMap bitMap) {
            for (int i = 0; i < bitMap.bytes.Length; ++i) {
                bytes[i] = bitMap.bytes[i];
            }
        }

        public void SetValue(ECanDo canDo) {
            bytes[(int) canDo] = 1;
        }

        public void SetValues(params ECanDo[] canDos) {
            for (int i = 0; i < canDos.Length; ++i) {
                SetValue(canDos[i]);
            }
        }

        public void RemoveValue(ECanDo canDo) {
            bytes[(int) canDo] = 0;
        }
        
        public void RemoveValues(params ECanDo[] canDos) {
            for (int i = 0; i < canDos.Length; ++i) {
                RemoveValue(canDos[i]);
            }
        }

        public bool Contains(ECanDo canDo) {
            return bytes[(int) canDo] == 1;
        }

        public void FillsFromReader(BinaryStreamReader reader) {
            for (int i = 0; i < Length; ++i) {
                bytes[i] = reader.ReadByte();
            }
        }

        public void Write(BinaryStreamWriter writer) {
            for (int i = 0; i < Length; ++i) {
                writer.Write(bytes[i]);
            }
        }
    }
}