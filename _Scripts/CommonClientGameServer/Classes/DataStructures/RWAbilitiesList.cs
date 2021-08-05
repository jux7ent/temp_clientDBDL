using System;
using System.Collections.Generic;
using DBDL.CommonDLL;

namespace GameServer {
    public class RWAbilitiesList : BaseReadableWritable {
        public readonly List<BaseAbility> list;

        public RWAbilitiesList() {
            list = new List<BaseAbility>();
        }

        public void Clear() {
            list.Clear();
        }

        public void Add(BaseAbility ability) {
            list.Add(ability);
        }

        public void AddFromType(Type abilityType, int playerId) {
            list.Add((BaseAbility) LambdaActivator.CreateInstance(abilityType, playerId));
        }

        public BaseAbility this[int key] {
            get => list[key];
        }

        public int Count => list.Count;

        public override void FillsFromReader(BinaryStreamReader reader) {
            list.Clear();

            int size = reader.ReadInt32();

            for (int i = 0; i < size; ++i) {
                Add((BaseAbility) LambdaActivator.CreateInstance(CGSContext.AssemblyClassesDict[reader.ReadInt()], reader));
            }
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(list.Count);

            foreach (var ability in list) {
                writer.Write(CGSContext.AssemblyClassesDict[ability.GetType()]);
                ability.Write(writer);
            }
        }
    }
}