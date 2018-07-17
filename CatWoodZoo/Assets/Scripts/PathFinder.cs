using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class PathFinder
{

    Dictionary<Vector2Int, Node> Open { get; set; }
    Dictionary<Vector2Int, Node> Closed { get; set; }
    Dictionary<Vector2Int, bool> Spaces { get; set; }
    bool NoDiagonals;
    Vector2Int Target;

    int Range; 

    public PathFinder(Dictionary<Vector2Int, bool> spaces, bool diagonals,int range)
    {
        Range = range;
        NoDiagonals = !diagonals;
        Spaces = spaces;
        Open = new Dictionary<Vector2Int, Node>();
        Closed = new Dictionary<Vector2Int, Node>();
    }

    public IEnumerable<Vector2Int> GetPath(Vector2Int start, Vector2Int target)
    {
        Target = target;

        if (!Spaces.ContainsKey(target))
        {
            Debug.Log("Invalide target");
            return new List<Vector2Int>();
        }
        if (Vector2Int.Distance(start, target) == 0)
        {
            return new List<Vector2Int>();
        }
        //if (!Spaces[target])
        //{
        //    return new List<Vector2Int>();
        //}


        Node startNode = new Node();
        startNode.G = 0;
        startNode.H = GetEffort(start, target);
        startNode.F = startNode.G + startNode.H;
        startNode.position = start;
        GetSteps(startNode, target);
        return Path(start, target);
    }
    List<Node> path;
    private IEnumerable<Vector2Int> Path(Vector2Int start, Vector2Int target)
    {
        path = new List<Node>();

        if (Closed.ContainsKey(target))
        {
            var current = Closed[target];
            while (Vector2Int.Distance(start, current.position) != 0)
            {
                path.Add(Closed[current.position]);
                current = Closed[current.parent];
            }
            return path.Select(x => x.position).Reverse();
        }
        else
        {
            return new List<Vector2Int> {
                start
            };
        }
    }

    List<Vector2Int> n;
    public void GetSteps(Node parent, Vector2Int target)
    {


        //if (!Spaces.ContainsKey(target)) target = parent.position;

        Closed[parent.position] = parent;
        if (Open.ContainsKey(parent.position))
        {
            Open.Remove(parent.position);
        }

        n = new List<Vector2Int>();
        if (NoDiagonals)
        {
            n = GetNeighboursRoads(parent.position).ToList();
        }
        else
        {
            n = GetNeighbours(parent.position).ToList();
        }

        foreach (var v in n)
        {

            if (!Closed.ContainsKey(v))
            {
                Node node = new Node();
                node.parent = parent.position;
                node.position = v;
                node.H = GetEffort(v, target);
                node.G = parent.G + GetEffort(parent.position, v);
                node.F = node.H + node.G;

                if (Open.ContainsKey(v))
                {
                    if (Open[v].F > node.F)
                    {
                        Open[v] = node;
                    }
                }
                else
                {
                    Open[v] = node;
                }

                if (v == target)
                {
                    Closed[v] = node;
                    return;
                }
            }
        }


        if (Open.Count > 0 && Open.Count< Range)
        {
            int min = Open.Min(x => x.Value.F);


            var nd = Open.Where(x => x.Value.F == min).First();

            GetSteps(nd.Value, target);
        }
        else
        {
            return;
        }
    }

    public IEnumerable<Vector2Int> GetNeighboursRoads(Vector2Int start)
    {
        n = new List<Vector2Int>();

        TryAdd(n, new Vector2Int(start.x, start.y + 1));
        TryAdd(n, new Vector2Int(start.x, start.y - 1));
        TryAdd(n, new Vector2Int(start.x + 1, start.y));
        TryAdd(n, new Vector2Int(start.x - 1, start.y));
        return n;
    }

    void TryAdd(List<Vector2Int> n, Vector2Int v2)
    {

        if (!Spaces.ContainsKey(v2)) return;


        if(v2==Target || Spaces[v2])
        {
            n.Add(v2);
        }

    }
   // List<Vector2Int> n;
    public IEnumerable<Vector2Int> GetNeighbours(Vector2Int start)
    {
        n = new List<Vector2Int>();

        for (int x = start.x - 1; x <= start.x + 1; x++)
        {
            for (int y = start.y - 1; y <= start.y + 1; y++)
            {
                Vector2Int coords = new Vector2Int(x, y);

                if (coords != start)
                {
                    TryAdd(n, new Vector2Int(x, y));
                    //if ( Target != coords && Spaces.ContainsKey(coords) && Spaces[coords] == false)
                    //{

                    //}
                    //else if (Spaces.ContainsKey(coords))
                    //{
                    //    n.Add(coords);
                    //}
                }

            }
        }
        return n;
    }

    public int GetEffort(Vector2Int start, Vector2Int target)
    {

        if (NoDiagonals)
        {
            int x = Mathf.Abs(start.x - target.x);
            int y = Mathf.Abs(start.y - target.y);

            return Mathf.RoundToInt(((x + y) * 10f));
        }
        else
        {
            int x = Mathf.Abs(start.x - target.x);
            int y = Mathf.Abs(start.y - target.y);
            if (x > y)
            {
                return Mathf.RoundToInt((14f * y + 10f * (x - y)));
            }
            else
            {
                return Mathf.RoundToInt((14f * x + 10f * (y - x)));
            }
        }
    }

}
