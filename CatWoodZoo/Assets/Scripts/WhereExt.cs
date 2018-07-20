using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WhereExt  {

	public static Vector2Int Where(this Transform tran)
    {
        return new Vector2Int(Mathf.RoundToInt(tran.position.x), Mathf.RoundToInt(tran.position.z));
    }
}
