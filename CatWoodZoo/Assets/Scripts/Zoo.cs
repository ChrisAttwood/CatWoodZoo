using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoo : MonoBehaviour {
    
    public static Zoo instance;
    public Dictionary<Vector2Int, bool> Spaces;

    public int Size = 100;

    private void Awake()
    {
        instance = this;
        Spaces = new Dictionary<Vector2Int, bool>();

        for(int x = -Size; x < Size; x++)
        {
            for (int y = -Size; y < Size; y++)
            {
                Spaces[new Vector2Int(x, y)] = true;
            }
        }

    }

}
