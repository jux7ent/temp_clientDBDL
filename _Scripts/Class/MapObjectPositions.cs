using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName="MapInfo")]
public class MapObjectPositions {
    public Vector3[] CagePositions;
    public Vector3[] CampFirePositions;
    public Vector3[] HatchPositions;
    public Vector3[] EscaperPositions;
    public Vector3 CatcherPosition;
}