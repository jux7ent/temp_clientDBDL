using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshChildrenCombiner : MonoBehaviour {
    [SerializeField] private string savePath;
    [SerializeField] private string saveName;

    public void Combine() {
#if UNITY_EDITOR
        MeshFilter[] meshFiltersForCombine = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFiltersForCombine.Length];

        for (int i = 0; i < meshFiltersForCombine.Length; ++i) {
            combine[i].mesh = meshFiltersForCombine[i].sharedMesh;
            combine[i].transform = meshFiltersForCombine[i].transform.localToWorldMatrix;
            meshFiltersForCombine[i].gameObject.SetActive(false);
        }

        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);

        gameObject.SetActive(true);

        string fullSavePath = $"{savePath}/{saveName}.asset";
        AssetDatabase.CreateAsset(transform.GetComponent<MeshFilter>().sharedMesh, fullSavePath);
#endif
    }
}