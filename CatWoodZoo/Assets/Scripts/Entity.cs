using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity : MonoBehaviour {

    public Activity[] Activities;

    public bool HasPaid = false;
    public bool HasRated = false;

    public int Boredom = 0;
    public int Tiredness = 0;
    public int Rating = 0;

    public List<Animal> SeenAnimals;

    Vector2Int? Target;
    PathFinder pf;

    public float Speed = 1f;

    Dictionary<Vector2Int, bool> Zone;

    private void Start()
    {
        SeenAnimals = new List<Animal>();
    }

    void Update()
    {
        SpotCheck();

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

                    return;
                }

               
            }
        }

    }



    void SpotCheck()
    {
        if (transform.Where() == Zoo.instance.Enterance)
        {
            if (!HasPaid)
            {
                HasPaid = true;
                Zoo.instance.Cash += Zoo.instance.Ticket;
                Rating -= Zoo.instance.Ticket;
                
            }

        }

        if (transform.Where() == Zoo.instance.Exit)
        {
            if (!HasRated)
            {
                HasRated = true;
                Zoo.instance.Rate += Rating;
                if (Zoo.instance.Rate < 0)
                {
                    Zoo.instance.Rate = 0;
                }

                if (Zoo.instance.Rate > 100)
                {
                    Zoo.instance.Rate = 100;
                }
            }
            
        }

        if (transform.Where() == Zoo.instance.Home)
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
        if (Vector2.Distance(transform.Where(), Path[Step]) > 0)
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

        Path = pf.GetPath(transform.Where(), targetLocation).ToList();
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
