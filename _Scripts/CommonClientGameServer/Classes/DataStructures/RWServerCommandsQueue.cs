using System;
using System.Collections.Generic;
using DBDL.CommonDLL;


namespace GameServer {
    public class RWServerCommandsQueue : BaseReadableWritable {
        private readonly LimitedQueue<BaseSc> queue;

        public RWServerCommandsQueue(int limitedQueueLimit) {
            queue = new LimitedQueue<BaseSc>(limitedQueueLimit);
        }

        public void Clear() {
            
            queue.Clear();
        }

        public void Enqueue(BaseSc sc) {
            if (sc != null) {
                queue.Enqueue(sc);
            } else {
                Console.WriteLine("Trying enqueue null to server commands queue.");
            }
        }

        public BaseSc Dequeue() {
            return queue.Dequeue();
        }

        public int Count => queue.Count;

        public override void FillsFromReader(BinaryStreamReader reader) {
            queue.Clear();

            int size = reader.ReadInt32();

            for (int i = 0; i < size; ++i) {
                Enqueue((BaseSc) LambdaActivator.CreateInstance(CGSContext.AssemblyClassesDict[reader.ReadInt()], reader));
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