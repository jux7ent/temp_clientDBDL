namespace GameServer {
    public enum EServerCommandType {
        Base,
        BaseOnePlayer,
        BaseOwnerTarget,
        
        HidePlayer,
        Movement,
        Attack,
        TakeInBag,
        PlaceInCage,
        StartInteraction,
        InvokeAbility,
        SpawnObject,
        DestroyObject,
        HideInGrass,
        LostPlayer,
        GatesInteractable,
        GateOpened,
        EscaperEscaped,
        MedkitTaken,
        EscaperHealing,
        ReceiveDamage,
    }
}