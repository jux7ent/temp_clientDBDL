using System.Threading;
using DBDL.CommonDLL;


namespace GameServer {
    public abstract class BaseAbilityWithCharges : BaseAbilityWithCooldown {
        public int TotalChargesCount;
        public int CurrentChargesCount;

        public BaseAbilityWithCharges(int playerId) : base(playerId) {
            TotalChargesCount = GetTotalChargesCount();
            CurrentChargesCount = TotalChargesCount;
        }
        
        public BaseAbilityWithCharges(BinaryStreamReader reader) : base(reader) { }

        public abstract int GetTotalChargesCount();

        public override void FillsFromReader(BinaryStreamReader reader) {
            base.FillsFromReader(reader);
            TotalChargesCount = reader.ReadInt32();
            CurrentChargesCount = reader.ReadInt32();
        }

        public override void Write(BinaryStreamWriter writer) {
            base.Write(writer);
            writer.Write(TotalChargesCount);
            writer.Write(CurrentChargesCount);
        }

        private bool cooldownStarted = false;
        public void TakeOneCharge() {
            --CurrentChargesCount;
            StartCooldown();
        }

        private new void StartCooldown() {
            if (!cooldownStarted) {
                cooldownStarted = true; // пофиг на гонки
                new Thread(() => {
                    while (CurrentChargesCount < TotalChargesCount) {
                        Cooldown = TotalCooldown;
                    
                        while (Cooldown > 0) {
                            Cooldown -= 0.1f;
                            Thread.Sleep(100);
                        }

                        ++CurrentChargesCount;
                    }

                    Cooldown = 0f;
                    cooldownStarted = false;
                }).Start();
            }
        }
    }
}