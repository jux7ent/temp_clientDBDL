using System;
using DBDL.CommonDLL;

namespace GameServer {
    public class RWActionsQueue : BaseReadableWritable {
        public readonly LimitedQueue<BaseAction> queue;

        public RWActionsQueue(int limitCount) {
            queue = new LimitedQueue<BaseAction>(limitCount);
        }

        public void Clear() {
            queue.Clear();
        }

        public void Enqueue(BaseAction action) {
            queue.Enqueue(action);
        }

        public BaseAction Dequeue() {
            return queue.Dequeue();
        }

        public int Count => queue.Count;

        public override void FillsFromReader(BinaryStreamReader reader) {
            queue.Clear();

            int size = reader.ReadInt32();

            for (int i = 0; i < size; ++i) {
                Enqueue((BaseAction) LambdaActivator.CreateInstance(CGSContext.AssemblyClassesDict[reader.ReadInt()],
                    reader));
            }
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(queue.Count);

            foreach (var action in queue) {
                writer.Write(CGSContext.AssemblyClassesDict[action.GetType()]);
                action.Write(writer);
            }
        }
    }
}