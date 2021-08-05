using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;
using UnityEngine.AI;

public class ObjectsSpawner : MonoBehaviour {
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Transform parent;
    [SerializeField] private int count = 10;
    [SerializeField] private float spawnRadius = 100;

    public void Spawn() {
        for (int i = 0; i < count; ++i) {
            SpawnAtPosition(GetRandomNavmeshPosition());
        }
    }

    private Vector3 GetRandomNavmeshPosition() {
        Vector3 randomPos = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0f,
            Random.Range(-spawnRadius, spawnRadius));

        NavMesh.SamplePosition(randomPos, out NavMeshHit hit, spawnRadius, 1);

        return hit.position;
    }

    private void SpawnAtPosition(Vector3 position) {
        GameObject randomPrefab = GameExtensions.Random.GetRandomElementFromArray(prefabs);

        GameObject spawnedObj = Instantiate(randomPrefab, position, Quaternion.identity);
        spawnedObj.transform.parent = parent;
    }
}