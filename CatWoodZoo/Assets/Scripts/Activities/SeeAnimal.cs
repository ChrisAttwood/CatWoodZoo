using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SeeAnimal : Activity {

    public override bool IsApplicable(Entity entity)
    {
        entity.Tiredness++;
        
        return true;
    }

    public override ActivityData GetActivityData(Entity entity)
    {
        var point = Zoo.instance.AnimalView(entity);
        if (point!=null)
        {
            
            return new ActivityData
            {
                Target = point.Value,
                Zone = Zoo.instance.Path

            };
        }

        return null;
       
    }

}
