using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/CharactersSprites")]
public class CharactersSprites : ScriptableObject {
    [field: SerializeField] public GenericDictionary<ECharacter, Sprite> Dictionary;
}