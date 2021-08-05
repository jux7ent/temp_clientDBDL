using System;
using System.Collections.Generic;

namespace GameServer {
    public class Character2Ability {
        public Dictionary<ECharacter, Type> Character2AbilityTypes = new Dictionary<ECharacter, Type>();

        public Character2Ability() {
            Character2AbilityTypes.Add(ECharacter.Shade, typeof(InvisAbility));
            Character2AbilityTypes.Add(ECharacter.Bird, typeof(InvisAbility));
            Character2AbilityTypes.Add(ECharacter.Egglet, typeof(SpawnTrapAbility));
            Character2AbilityTypes.Add(ECharacter.Shadow, typeof(InvisAbility));
            Character2AbilityTypes.Add(ECharacter.Spook, typeof(InvisAbility));
            Character2AbilityTypes.Add(ECharacter.SpiderKing, typeof(InvisAbility));

            if (Character2AbilityTypes.Count != Enum.GetValues(typeof(ECharacter)).Length) {
                Console.WriteLine("Error in Character2AbilityTypes");
            }
        }
    }
}