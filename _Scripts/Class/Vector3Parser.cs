using UnityEngine;

public static class Vector3Parser {
    private const char Separator = '|';
    
    public static string Vector3ToString(Vector3 vec) {
        return $"{vec.x}{Separator}{vec.y}{Separator}{vec.z}";
    }

    public static Vector3 StringToVector3(string str) {
        string[] xyz = str.Split(Separator);

        Vector3 result;
        result.x = float.Parse(xyz[0]);
        result.y = float.Parse(xyz[1]);
        result.z = float.Parse(xyz[2]);

        return result;
    }
}
