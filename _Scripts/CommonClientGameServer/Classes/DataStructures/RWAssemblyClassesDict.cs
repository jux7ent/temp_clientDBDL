using System;
using System.Collections;
using System.Collections.Generic;
using DBDL.CommonDLL;

namespace GameServer {
    public class RWAssemblyClassesDict : BaseReadableWritable, IEnumerable<KeyValuePair<int, Type>> {
        private DoubleSidedDict<int, Type> innerDict = new DoubleSidedDict<int, Type>();

        public void Add(int key, Type value) {
            innerDict.Add(key, value);
        }
        
        public Type this[int key] {
            get => innerDict[key];
        }

        public int this[Type key] {
            get => innerDict[key];
        }

        public int Count => innerDict.Count;

        public override void FillsFromReader(BinaryStreamReader reader) {
            innerDict.Clear();
            
            int size = reader.ReadInt();

            for (int i = 0; i < size; ++i) {
                int key = reader.ReadInt();
                Type value = Type.GetType(reader.ReadString());
                
                if (value != null) innerDict.Add(key, value);
            }
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(innerDict.Count);

            foreach (var pair in innerDict) {
                writer.Write(pair.Key);
                writer.Write(pair.Value.ToString());
            }
        }

        public IEnumerator<KeyValuePair<int, Type>> GetEnumerator() {
            return innerDict.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}