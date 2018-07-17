using UnityEngine;
using System.Collections;

//public class Node
//{
//    public Vector2Int position { get; set; }
//    public Vector2Int parent { get; set; }

//    public int H { get; set; }
//    public int G { get; set; }
//    public int F { get; set; }

//    public float Distance { get; set; }
//}


public struct Node
{
    public Vector2Int position { get; set; }
    public Vector2Int parent { get; set; }

    public int H { get; set; }
    public int G { get; set; }
    public int F { get; set; }

    public float Distance { get; set; }
}
