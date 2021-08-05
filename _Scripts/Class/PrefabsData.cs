using GameServer;
using UnityEngine;

[CreateAssetMenu(menuName = "PrefabsData")]
public class PrefabsData : ScriptableObject {
    [field: SerializeField] public GameObject Cage { get; private set; }
    [field: SerializeField] public GameObject CampFire { get; private set; }
    [field: SerializeField] public GameObject Hatch { get; private set; }
    [field: SerializeField] public GameObject Medkit { get; private set; }

    [field: SerializeField] public GenericDictionary<ECharacter, GameObject> Characters { get; private set; }

    [field: SerializeField]
    public GenericDictionary<ESpawnObjectType, GameObject> ServerSpawnObjects { get; private set; }
}