using DBDL.CommonDLL;

namespace GameServer {
    public class SpawnTrapAbility : BaseAbilityWithCharges {
        public SpawnTrapAbility(int playerId) : base(playerId) { }
        public SpawnTrapAbility(BinaryStreamReader reader) : base(reader) { }
        public override bool Accept(BaseAbilitiesHandler abilitiesHandler) {
            return abilitiesHandler.Handle(this);
        }

        public override int GetTotalCooldown() {
            return 15;
        }

        public override int GetTotalChargesCount() {
            return 5;
        }
    }
}