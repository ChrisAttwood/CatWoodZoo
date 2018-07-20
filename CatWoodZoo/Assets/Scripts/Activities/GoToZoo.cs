using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GoToZoo : Activity {

    public override bool IsApplicable(Entity entity)
    {

        if (!entity.HasPaid)
        {
            return true;
        }
        return false;

    }

    public override ActivityData GetActivityData(Entity entity)
    {
        return new ActivityData
        {
            Target = Zoo.instance.Enterance,
            Zone = Zoo.instance.Path

        };
    }
}
