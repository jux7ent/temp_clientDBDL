using System.Threading;
using DBDL.CommonDLL;

namespace GameServer {
    public abstract class BaseAbilityWithCooldown : BaseAbility {
        public float Cooldown;
        public int TotalCooldown;

        public BaseAbilityWithCooldown(int playerId) : base(playerId) {
            TotalCooldown = GetTotalCooldown();
            Cooldown = 0f;
        }

        public BaseAbilityWithCooldown(BinaryStreamReader reader) : base(reader) { }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);

            writer.Write(Cooldown);
            writer.Write(TotalCooldown);
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            
            Cooldown = reader.ReadSingle();
            TotalCooldown = reader.ReadInt32();
        }

        public abstract int GetTotalCooldown();

        public void StartCooldown() {
            Cooldown = TotalCooldown;

            new Thread(() => {
                while (Cooldown > 0) {
                    Cooldown -= 0.1f;
                    Thread.Sleep(100);
                }

                Cooldown = 0f;
            }).Start();
        }
    }
}