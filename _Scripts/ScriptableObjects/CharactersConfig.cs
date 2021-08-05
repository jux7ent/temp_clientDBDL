using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Characters/CharactersConfig")]
public class CharactersConfig : ScriptableObject {
    [field: SerializeField] public GenericDictionary<ECharacter, CharacterData> CharacterDatas { get; private set; }
}