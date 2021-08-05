using DBDL.CommonDLL;

namespace GameServer {
    public abstract class BaseActionsHandler {
        protected IdGenerator actionIdGenerator = new IdGenerator(100);

        public abstract void Handle(AttackAction attackAction);
        public abstract void Handle(MovementAction movementAction);
        public abstract void Handle(PlaceInCageAction placeInCageAction);
        public abstract void Handle(TakeInBagAction takeInBagAction);
        public abstract void Handle(StartCageInteractionAction cageInteractionAction);
        public abstract void Handle(StartCampFireInteractionAction campFireInteractionAction);
        public abstract void Handle(StartHatchInteractionAction hatchInteractionAction);
        public abstract void Handle(StartGateInteractionAction gateInteractionAction);
        public abstract void Handle(UseAbilityAction useAbilityAction);
        public abstract void Handle(HideInGrassAction hideInGrassAction);
        public abstract void Handle(EnterToWinZoneAction enterToWinZoneAction);
        public abstract void Handle(TakeMedkitAction takeMedkitAction);
        public abstract void Handle(UseMedkitAction useMedkitAction);
    }
}