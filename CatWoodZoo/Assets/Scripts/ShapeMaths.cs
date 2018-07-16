using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShapeMaths  {



    public static List<Vector2Int> Line(Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> coords = new List<Vector2Int>();

        float length = Vector2Int.Distance(start, end);

        for (int step = 0; step <= length; step++)
        {
            var t = length == 0 ? 0.0f : step / length;
            Vector2 c = Vector2.Lerp(start, end, t);
            Vector2Int cInt = new Vector2Int(Mathf.RoundToInt(c.x), Mathf.RoundToInt(c.y));
            if (!coords.Contains(cInt))
            {
                coords.Add(cInt);
            }

        }

        return coords;

    }

    public static List<Vector2Int> Box(Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> coords = new List<Vector2Int>();

        int lowestX = start.x < end.x ? start.x : end.x;
        int heighestX = start.x > end.x ? start.x : end.x;

        int lowestY = start.y < end.y ? start.y : end.y;
        int heighestY = start.y > end.y ? start.y : end.y;

        for (int x = lowestX; x <= heighestX; x++)
        {
            for (int y = lowestY; y <= heighestY; y++)
            {
                coords.Add(new Vector2Int(x, y));
            }
        }


        return coords;

    }

}
