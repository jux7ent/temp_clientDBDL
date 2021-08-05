namespace GameServer {
    public enum EActionType { // remove this file
        Base,
        
        Movement, // actions from client
        Attack,
        TakeInBag,
        PlaceEscaperInCage,
        StartCageInteraction,
        StartCampFireInteraction,
        StartHatchInteraction,
        StartGateInteraction,
        UseAbility,
        HideInGrass,
        EnterToWinZone,
        TakeMedkit,
        UseMedkit,
    }
    
   /* public enum EActionType {
        Idle,
        Walk,
        Attack,
        InteractionCage,
        InteractionCampFire,
        InteractionHatch,
        TakeDamage,
        Stunned,
        TakeInBag,
        InBag,
        MoveInCage,
        RestoreFromCage
    }*/
}