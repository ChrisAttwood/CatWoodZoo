using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GetBoard : Activity {

    public override bool IsApplicable(Entity entity)
    {
        entity.Boredom++;
        return false;
    }

    public override ActivityData GetActivityData(Entity entity)
    {
        return null;

    }
}
