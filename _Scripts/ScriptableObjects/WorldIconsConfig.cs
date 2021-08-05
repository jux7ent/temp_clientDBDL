using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CatcherHiderSprites {
    [field: SerializeField] public Sprite CatcherSprite { get; private set; }
    [field: SerializeField] public Sprite HiderSprite { get; private set; }
}

[CreateAssetMenu(menuName="GameData/WorldIconsConfig")]
public class WorldIconsConfig : ScriptableObject {
    [field: SerializeField] public GenericDictionary<EWorldIconType, CatcherHiderSprites> Data { get; private set; }
}