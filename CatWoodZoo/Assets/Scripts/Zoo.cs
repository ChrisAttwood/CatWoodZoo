using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Zoo : MonoBehaviour {
    
    public static Zoo instance;
    public Dictionary<Vector2Int, bool> Spaces;
    public Dictionary<Vector2Int, bool> Structures;
    public Dictionary<Vector2Int, bool> Path;
    public Dictionary<Vector2Int, Fence> Fences;

    public List<Animal> Animals;

    public GameObject WallPrefab;
    public GameObject PathPrefab;




    public int Size = 100;

    public Person PersonPrefab;

    public int Cash = 1000;
    public int Rate = 0;
    public int Ticket = 10;


    public Vector2Int Arrive = new Vector2Int(2, 99);
    public Vector2Int Enterance = new Vector2Int(5, 51);
    public Vector2Int Exit = new Vector2Int(5, 49);
    public Vector2Int Home = new Vector2Int(2, 0);
   



    private void Awake()
    {
        instance = this;
        Spaces = new Dictionary<Vector2Int, bool>();
        Path = new Dictionary<Vector2Int, bool>();
        Fences = new Dictionary<Vector2Int, Fence>();
        Structures = new Dictionary<Vector2Int, bool>();
        Animals = new List<Animal>();
        for (int x =0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                Spaces[new Vector2Int(x, y)] = true;
                Path[new Vector2Int(x, y)] = false;
                Fences[new Vector2Int(x, y)] = null;
                Structures[new Vector2Int(x, y)] = false;
            }

           
        }
        BuildOutterEdge();
    }

    private void Start()
    {
       
        Guest();
       
    }

    void Guest()
    {
        var preson = Instantiate(PersonPrefab);
        preson.transform.position = new Vector3(Arrive.x, 0f, Arrive.y);
        Invoke("Guest", (50f / (Rate+10f)) + Random.Range(0f,1f));
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
       // AddPath(new Vector2Int(2, 50));
    }


    void AddPath(Vector2Int pos)
    {
        var block = Instantiate(PathPrefab);
        block.transform.position = new Vector3(pos.x, 0f, pos.y);
        block.transform.SetParent(Zoo.instance.transform);
        instance.Spaces[pos] = true;
        instance.Path[pos] = true;
        instance.Structures[pos] = true;
    }
    void AddWall(Vector2Int pos)
    {
        var block = Instantiate(WallPrefab);
        block.transform.position = new Vector3(pos.x, 0f, pos.y);
        block.transform.SetParent(Zoo.instance.transform);
        instance.Spaces[pos] = false;
        instance.Structures[pos] = true;

    }

    public Dictionary<Vector2Int,bool> GetZone(Vector2Int start)
    {
        Dictionary<Vector2Int, bool> zone = new Dictionary<Vector2Int, bool>();
        Fill(zone, start);

        return zone;
    }

    void Fill(Dictionary<Vector2Int, bool> zone, Vector2Int v2)
    {
        if (zone.ContainsKey(v2)) return;
        if (!Spaces.ContainsKey(v2)) return;
        if (!Spaces[v2]) return;

        zone.Add(v2, true);


        Fill(zone, new Vector2Int(v2.x + 1, v2.y));
        Fill(zone, new Vector2Int(v2.x - 1, v2.y));
        Fill(zone, new Vector2Int(v2.x, v2.y + 1));
        Fill(zone, new Vector2Int(v2.x, v2.y - 1));

    }

    public Vector2Int? AnimalView(Entity entity)
    {
        Dictionary<Vector2Int,Animal> points = new Dictionary<Vector2Int, Animal>();
        foreach (Animal a in Animals.Except(entity.SeenAnimals))
        {
            Vector2Int aPos = a.transform.Where();
            for(int x = -a.Size + aPos.x; x < a.Size + aPos.x; x++)
            {
                for (int y = -a.Size + aPos.y; y < a.Size+ aPos.y; y++)
                {
                    var pos = new Vector2Int(x, y);
                    if (instance.Path.ContainsKey(pos) && instance.Path[pos])
                    {
                        if (!points.ContainsKey(pos))
                        {
                            points.Add(pos, a);
                        }
                        
                    }
                }
            }
        }
        if (points.Count > 0)
        {
            int index = Random.Range(0, points.Count);
            entity.SeenAnimals.Add(points.ElementAt(index).Value);
            entity.Rating += points.ElementAt(index).Value.Appeal;
            return points.ElementAt(index).Key;
        }
       

        return null;
    }


}
