using System;
using System.Linq;
using UnityEngine;
/*
[Serializable]
[CreateAssetMenu(menuName="MapInfo")]
public class MapInfoForGeneration : ScriptableObject {
    public Vector3[] CagePositions;
    public Vector3[] CampFirePositions;
    public Vector3[] HatchPositions;
    public Vector3[] EscaperPositions;
    public Vector3[] CatcherPosition;

    public MapInfo GetMapInfo() {
        MapInfo result = new MapInfo();
        
        result.CagePositions = CagePositions.Select(item => new ServerConsole.MyExtensions.Vector3(item.x, item.y, item.z)).ToArray();
        result.CampFirePositions = CampFirePositions.Select(item => new ServerConsole.MyExtensions.Vector3(item.x, item.y, item.z)).ToArray();
        result.HatchPositions = HatchPositions.Select(item => new ServerConsole.MyExtensions.Vector3(item.x, item.y, item.z)).ToArray();
        result.EscaperPositions = EscaperPositions.Select(item => new ServerConsole.MyExtensions.Vector3(item.x, item.y, item.z)).ToArray();
        result.CatcherPositions = CatcherPosition.Select(item => new ServerConsole.MyExtensions.Vector3(item.x, item.y, item.z)).ToArray();

        return result;
    }
}*/