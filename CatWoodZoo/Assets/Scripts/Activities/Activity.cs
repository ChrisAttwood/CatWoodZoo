using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : ScriptableObject {

  
    public virtual bool IsApplicable(Entity entity)
    {
        return false;
    }

    public virtual ActivityData GetActivityData(Entity entity)
    {
        return null;
    }

}


public class ActivityData
{
    public Vector2Int Target;
    public Dictionary<Vector2Int, bool> Zone;

}
