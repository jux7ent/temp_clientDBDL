using System;
using UnityEngine;

public abstract class BaseStaticObject : MonoBehaviour {
    public int Id = -1;

    public void SetValues(int id) {
        Id = id;
    }
}