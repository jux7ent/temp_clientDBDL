using DBDL.CommonDLL;

namespace GameServer {
    public class InvisAbility : BaseAbilityWithCooldownDuration {
        
        public InvisAbility(int playerId) : base(playerId) {}
        
        public InvisAbility(BinaryStreamReader reader) : base(reader) {}

        public override bool Accept(BaseAbilitiesHandler abilitiesHandler) {
            return abilitiesHandler.Handle(this);
        }

        public override int GetTotalCooldown() {
            return 30;
        }

        public override int GetTotalDuration() {
            return 10;
        }
    }
}