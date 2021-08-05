using System.Threading;
using DBDL.CommonDLL;

namespace GameServer {
    public abstract class BaseAbilityWithCooldownDuration : BaseAbilityWithCooldown {
        public float Duration;
        public int TotalDurationSec;

        public BaseAbilityWithCooldownDuration(int playerId) : base(playerId) {
            TotalDurationSec = GetTotalDuration();
            Duration = 0f;
        }

        public BaseAbilityWithCooldownDuration(BinaryStreamReader reader) : base(reader) { }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            
            writer.Write(TotalDurationSec);
            writer.Write(Duration);
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            
            TotalDurationSec = reader.ReadInt32();
            Duration = reader.ReadSingle();
        }

        public abstract int GetTotalDuration();
        
        public void StartCooldownDuration() {
            Cooldown = TotalCooldown;
            Duration = TotalDurationSec;
            
            new Thread(() => {
                while (Cooldown > 0) {
                    Cooldown -= 0.1f;
                    Duration -= 0.1f;

                    if (Duration < 0) {
                        Duration = 0f;
                    }

                    Thread.Sleep(100);
                }

                Cooldown = 0f;
            }).Start();
        }
    }
}