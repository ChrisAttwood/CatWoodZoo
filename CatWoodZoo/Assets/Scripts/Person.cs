using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Person : MonoBehaviour {

    List<Vector2Int> BeenHere;
    Vector2Int? Target;

	// Use this for initialization
	void Start () {
        BeenHere = new List<Vector2Int>();
        BeenHere.Add(Where());
    }
	
	// Update is called once per frame
	void Update () {
        //if (Target == null)
        //{
        //    Target = FindTarget();

        //}

        //if (Target != null)
        //{
        //    Move();
        //}

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
       // Target = new Vector2Int(-Zoo.instance.Size + 2, -Zoo.instance.Size);
       // MoveTo(new Vector2Int(-Zoo.instance.Size + 2, -Zoo.instance.Size));
        MoveTo(new Vector2Int(2,1));
    }


    Vector2Int Where()
    {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }



    void End()
    {
        if (transform.position == new Vector3(-Zoo.instance.Size + 2, 0f, -Zoo.instance.Size))
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

            transform.position = Vector3.MoveTowards(transform.position, new Vector3( Path[Step].x,0f, Path[Step].y), 10f* Time.deltaTime);

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
    PathFinder pf;
    public void MoveTo(Vector2Int targetLocation)
    {
       

        Target = targetLocation;
        pf = new PathFinder(Zoo.instance.Path , false, 1000);
        
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
            if (!Zoo.instance.Path[Path[Step]] && Path[Step] != Target)
            {
                Target = null;
                return;
            }
        }
    }



}
