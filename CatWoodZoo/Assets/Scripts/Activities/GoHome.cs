using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GoHome : Activity {

    public override bool IsApplicable(Entity entity)
    {
        return true;
    }

    public override ActivityData GetActivityData(Entity entity)
    {
        return new ActivityData
        {
            Target = Zoo.instance.Exit,
            Zone = Zoo.instance.Path

        };
    }

    //public override Vector2Int? GetTarget(Entity entity)
    //{
    //    return Zoo.instance.Exit;
    //}

    //public override Dictionary<Vector2Int, bool> GetZone(Entity entity)
    //{
    //    return Zoo.instance.Path;
    //}
}
