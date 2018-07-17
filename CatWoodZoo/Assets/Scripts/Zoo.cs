using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoo : MonoBehaviour {
    
    public static Zoo instance;
    public Dictionary<Vector2Int, bool> Spaces;
    public Dictionary<Vector2Int, bool> Path;

    public GameObject WallPrefab;
    public GameObject PathPrefab;


    public int Size = 100;

    public Person PersonPrefab;

    public float GuestRate = 500f;



    private void Awake()
    {
        instance = this;
        Spaces = new Dictionary<Vector2Int, bool>();
        Path = new Dictionary<Vector2Int, bool>();
        for (int x =0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                Spaces[new Vector2Int(x, y)] = true;
                Path[new Vector2Int(x, y)] = false;
            }

           
        }
        BuildOutterEdge();
    }

    private void Start()
    {
        Invoke("Guest", 1000f/GuestRate);
    }

    void Guest()
    {
        var preson = Instantiate(PersonPrefab);
        preson.transform.position = new Vector3( 2,0f,Size-1);
       // Invoke("Guest", 1000f / GuestRate);
    }

    void BuildOutterEdge()
    {
        for (int x = 5; x < Size; x++)
        {
            AddWall(new Vector2Int(x, 0));
            AddWall(new Vector2Int(x, Size));
        }

        for (int y = 0; y < Size; y++)
        {
            AddWall(new Vector2Int(Size, y));

            if (y != Size/2 -1 && y != Size / 2 + 1 && y != Size / 2)
            {
                AddWall(new Vector2Int( 5, y));
            }
        }

        for (int y = 0; y < Size; y++)
        {
            if (y != Size / 2)
            {
                AddPath(new Vector2Int( 2, y));
            }
           
        }

        for (int x =  3 ; x <  8; x++)
        {
            AddPath(new Vector2Int(x, Size / 2 - 1));
            AddPath(new Vector2Int(x, Size / 2 + 1));
        }

        AddPath(new Vector2Int( 7, 50));
    }


    void AddPath(Vector2Int pos)
    {
        var block = Instantiate(PathPrefab);
        block.transform.position = new Vector3(pos.x, 0f, pos.y);
        block.transform.SetParent(Zoo.instance.transform);
        instance.Spaces[pos] = false;
        instance.Path[pos] = true;

    }
    void AddWall(Vector2Int pos)
    {
        var block = Instantiate(WallPrefab);
        block.transform.position = new Vector3(pos.x, 0f, pos.y);
        block.transform.SetParent(Zoo.instance.transform);
        instance.Spaces[pos] = false;
        

    }


}
