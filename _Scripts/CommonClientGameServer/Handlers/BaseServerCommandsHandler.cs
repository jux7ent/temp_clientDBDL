



namespace GameServer {
    public abstract class BaseServerCommandsHandler {
        public abstract void Handle(ScHidePlayer hidePlayer);
        public abstract void Handle(ScInvokeAbility commandInvokeAbility);
        public abstract void Handle(ScMovementPlayer commandMovementPlayer);
        public abstract void Handle(ScPlaceInCage commandPlaceInCage);
        public abstract void Handle(ScAttack commandAttack);
        public abstract void Handle(ScStartInteraction commandStartInteraction);
        public abstract void Handle(ScTakeInBag commandTakeInBag);
        public abstract void Handle(ScSpawnObject commandSpawnObject);
        public abstract void Handle(ScDestroyObject commandDestroyObject);
        public abstract void Handle(ScGateOpened commandGateOpened);
        public abstract void Handle(ScHideInGrass commandHideInGrass);
        public abstract void Handle(ScLostPlayer commandLostPlayer);
        public abstract void Handle(ScGatesInteractable commandGatesInteractable);
        public abstract void Handle(ScEscaperEscaped commandEscaperEscaped);
        public abstract void Handle(ScMedkitTaken commandMedkitTaken);
        public abstract void Handle(ScEscaperHealing commandEscaperHealing);
        public abstract void Handle(ScReceiveDamage commandReceiveDamage);
    }
}