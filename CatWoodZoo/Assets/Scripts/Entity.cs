﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity : MonoBehaviour {

    public Activity[] Activities;


    Vector2Int? Target;
    PathFinder pf;

    public float Speed = 1f;

    Dictionary<Vector2Int, bool> Zone;

    void Update()
    {
        End();

        if (Target != null)
        {

            GoToTarget();
        }
        else
        {
            FindTarget();
        }
    }

    void FindTarget()
    {
       foreach(Activity a in Activities)
       {
            if (a.IsApplicable(this))
            {
                var data = a.GetActivityData(this);
                if (data != null)
                {
                    Zone = data.Zone;
                    MoveTo(data.Target,data.Zone);
                }

               
            }
        }

       
    }


    public Vector2Int Where()
    {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }



    void End()
    {
        if (Where() == new Vector2Int(2, 0))
        {
            Destroy(gameObject);
        }
    }

    List<Vector2Int> Path;
    int Step = 0;



    public Vector2Int GetStep()
    {
        return Path[Step];
    }

    public void GoToTarget()
    {
        if (Vector2.Distance(Where(), Path[Step]) > 0)
        {

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Path[Step].x, 0f, Path[Step].y), Speed * Time.deltaTime);
            Vector3 targetDir = new Vector3(Path[Step].x, 0f, Path[Step].y) - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 0.1f,0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
        else
        {
            if (Path.Count > Step + 1)
            {
                Step++;
                NewStep();
            }
            else
            {
                
                Target = null;
            }
        }
    }
  
    public void MoveTo(Vector2Int targetLocation,Dictionary<Vector2Int,bool> zone)
    {


        Target = targetLocation;
        pf = new PathFinder(zone, false, 1000);

        Path = pf.GetPath(Where(), targetLocation).ToList();
        Step = 0;
        NewStep();
    }

    void NewStep()
    {
        if (Path.Count == 0)
        {
            
            Target = null;
        }
        else
        {
            if (!Zone[Path[Step]] && Path[Step] != Target)
            {
                Target = null;
                return;
            }
        }
    }
}
