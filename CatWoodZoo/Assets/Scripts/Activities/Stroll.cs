using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class Stroll : Activity {

    public override bool IsApplicable(Entity entity)
    {
        return true;
    }

    public override ActivityData GetActivityData(Entity entity)
    {
        var zone = Zoo.instance.GetZone(entity.transform.Where());

        List<Vector2Int> values = Enumerable.ToList(zone.Keys);

        var target = values[Random.Range(0, values.Count)];


        return new ActivityData
        {
             Target =target,
             Zone = zone
        };

       
    }
}
