using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoo : MonoBehaviour {
    
    public static Zoo instance;
    public Dictionary<Vector2Int, bool> Spaces;

    private void Awake()
    {
        instance = this;
        Spaces = new Dictionary<Vector2Int, bool>();
    }

}
