using GameServer;
using UnityEngine;

public class Character {
    public GameObject GameObject { get; private set; }
    public Transform Transform { get; private set; }
    public ControlCharacter Control;

    public BitMap CanDo = new BitMap();

    public Character(GameObject characterObj) {
        GameObject = characterObj;
        Transform = characterObj.transform;

        Control = characterObj.AddComponent<ControlCharacter>();
    }
}
