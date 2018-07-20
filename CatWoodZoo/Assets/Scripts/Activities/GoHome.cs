using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GoHome : Activity {

    public override bool IsApplicable(Entity entity)
    {

        if (entity.Tiredness >= 100)
        {
            return true;
        }

        if (entity.Boredom >= 100)
        {
            return true;
        }

        return false;

    }

    public override ActivityData GetActivityData(Entity entity)
    {
        return new ActivityData
        {
            Target = Zoo.instance.Home,
            Zone = Zoo.instance.Path

        };
    }


}
