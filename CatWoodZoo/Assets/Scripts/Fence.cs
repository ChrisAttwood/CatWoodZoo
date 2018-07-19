using UnityEngine;
using System.Collections;

public class Fence : MonoBehaviour {

    public GameObject North;
    public GameObject South;
    public GameObject East;
    public GameObject West;


    [SerializeField]
    Fence[] neighbors;

    void Awake()
    {
        North.SetActive(false);
        South.SetActive(false);
        East.SetActive(false);
        West.SetActive(false);
        neighbors = new Fence[4];
       
    }

    private void Start()
    {
        //var pos = Where();
        //Zoo.instance.Fences[pos] = this;
        var pos = Where();
        Zoo.instance.Fences[pos] = this;
        SetNeighbor(BlockDirection.N, Zoo.instance.Fences[new Vector2Int(pos.x, pos.y + 1)]);
        SetNeighbor(BlockDirection.S, Zoo.instance.Fences[new Vector2Int(pos.x, pos.y - 1)]);
        SetNeighbor(BlockDirection.E, Zoo.instance.Fences[new Vector2Int(pos.x + 1, pos.y)]);
        SetNeighbor(BlockDirection.W, Zoo.instance.Fences[new Vector2Int(pos.x - 1, pos.y)]);
        BuildFence();
    }

    Vector2Int Where()
    {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }

    public void SetJoin(BlockDirection bd,bool on)
    {
        switch (bd)
        {
            case BlockDirection.N:
                North.SetActive(on);
                break;
            case BlockDirection.S:
                South.SetActive(on);
                break;
            case BlockDirection.E:
                East.SetActive(on);
                break;
            case BlockDirection.W:
                West.SetActive(on);
                break;

        }
    }

    public Fence GetNeighbor(BlockDirection direction)
    {
        return neighbors[(int)direction];
    }

    public void SetNeighbor(BlockDirection direction, Fence fence)
    {
        neighbors[(int)direction] = fence;
        if (fence != null)
        {
            fence.neighbors[(int)direction.Opposite()] = this;
        }
        
    }

    public void BuildFence()
    {

      


        foreach (BlockDirection bd in System.Enum.GetValues(typeof(BlockDirection)))
        {
            Fence n = GetNeighbor(bd);
            if (n != null)
            {
                if (n != null)
                {
                    n.SetJoin(bd.Opposite(), true);
                    SetJoin(bd, true);
                }
            }
        }
    }

    public void RemoveFence()
    {
        foreach (BlockDirection bd in System.Enum.GetValues(typeof(BlockDirection)))
        {
            Fence n = GetNeighbor(bd);
            if (n != null)
            {
                
                 n.SetJoin(bd.Opposite(), false);
                
            }
        }

        
    }


    public void DestroyNeighbors()
    {

        foreach (BlockDirection bd in System.Enum.GetValues(typeof(BlockDirection)))
        {
            Fence n = GetNeighbor(bd);
            if (n != null)
            {
                Destroy(n.gameObject);
            }
        }

    }

}

public enum BlockDirection
{
    //N, NE, E, SE, S, SW, W, NW
    N, E, S, W
}

public static class BlockDirectionExtensions
{

    //public static BlockDirection Opposite(this BlockDirection direction)
    //{
    //    return (int)direction < 4 ? (direction + 4) : (direction - 4);
    //}

    public static BlockDirection Opposite(this BlockDirection direction)
    {
        return (int)direction < 2 ? (direction + 2) : (direction - 2);
    }
}