using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//One per scene. Using static instance for easy access.
public class Builder : MonoBehaviour {

    public static Builder instance;
   
    public GameObject GoodBluePrint;
    public GameObject BadBluePrint;
    GameObject[] good;
    GameObject[] bad;
    int bpSize = 1000;
    BluePrint CurrentBluePrint;

    Vector2Int? StartPos;
    bool IsDrawing;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateAndHideBluePrintBlocks();
    }

    void CreateAndHideBluePrintBlocks()
    {
        GameObject holder = new GameObject("bluePrintDesigner");
        good = new GameObject[bpSize];
        bad = new GameObject[bpSize];
        for (int i = 0; i < bpSize; i++)
        {
            good[i] = Instantiate(GoodBluePrint);
            good[i].transform.parent = holder.transform;
            good[i].gameObject.SetActive(false);

            bad[i] = Instantiate(BadBluePrint);
            bad[i].transform.parent = holder.transform;
            bad[i].gameObject.SetActive(false);
        }
    }

    public void SetActiveBluePrint(BluePrint bp)
    {
        ClearBluePrints();
        IsDrawing = false;
        CurrentBluePrint = bp;
    }



   

    void Update () {

        //If the mouse pointer is over a UI element, do nothing and return.
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (CurrentBluePrint != null)
            {
                IsDrawing = true;
                StartPos = ClickPlane.instance.GetGridPosition();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (CurrentBluePrint != null)
            {
                Build();
                IsDrawing = false;
                ClearBluePrints();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            CurrentBluePrint = null;
            IsDrawing = false;
        }


        if (IsDrawing)
        {
            UpdateBluePrint();
        }

    }

    void UpdateBluePrint()
    {

        ClearBluePrints();
        List<Vector2Int> Shape = GetShape();

        if(!CanBuildHere(Shape))
        {
            for (int j = 0; j < Shape.Count; j++)
            {
                bad[j].gameObject.SetActive(true);
                bad[j].transform.position = new Vector3(Shape[j].x,0f, Shape[j].y);

            }
        }
        else
        {
            for (int j = 0; j < Shape.Count; j++)
            {
                good[j].gameObject.SetActive(true);
                good[j].transform.position = new Vector3(Shape[j].x,0f, Shape[j].y);

            }
        }

       

    }

    void ClearBluePrints()
    {
        for (int i = 0; i < bpSize; i++)
        {
            good[i].gameObject.SetActive(false);
            bad[i].gameObject.SetActive(false);
        }
    }



   

    void Build()
    {
        var shape = GetShape();

        if (!CanBuildHere(shape)) return;

       

        if (CurrentBluePrint.SingleGameObject)
        {
            if (Zoo.instance.Cash >= CurrentBluePrint.Cash)
            {
                Zoo.instance.Cash -= CurrentBluePrint.Cash;
                var center = ClickPlane.instance.GetGridPosition().Value;
                var block = Instantiate(CurrentBluePrint.Structure);
                block.transform.position = new Vector3(center.x, 0f, center.y);
                block.transform.SetParent(Zoo.instance.transform);
                
                foreach (var spot in shape)
                {
                    Zoo.instance.Spaces[spot] = !CurrentBluePrint.IsBlocker;
                    Zoo.instance.Path[spot] = CurrentBluePrint.IsPath;
                    Zoo.instance.Structures[spot] = CurrentBluePrint.IsStructure;
                }
            }
        }
        else
        {
            foreach (var spot in shape)
            {
                if(Zoo.instance.Cash >= CurrentBluePrint.Cash)
                {
                    Zoo.instance.Cash -= CurrentBluePrint.Cash;
                    var block = Instantiate(CurrentBluePrint.Structure);
                    block.transform.position = new Vector3(spot.x, 0f, spot.y);
                    block.transform.SetParent(Zoo.instance.transform);
                    Zoo.instance.Spaces[spot] = !CurrentBluePrint.IsBlocker;
                    Zoo.instance.Path[spot] = CurrentBluePrint.IsPath;
                    Zoo.instance.Structures[spot] = CurrentBluePrint.IsStructure;
                }

              
            }
        }
    }


    List<Vector2Int> GetShape()
    {
        List<Vector2Int> Shape = new List<Vector2Int>();

        switch (CurrentBluePrint.BuildType)
        {
            case BuildType.Single:
                Shape = new List<Vector2Int> { ClickPlane.instance.GetGridPosition().Value };
                break;
            case BuildType.Line:
                Shape = ShapeMaths.Line(StartPos.Value, ClickPlane.instance.GetGridPosition().Value);
                break;
            case BuildType.Box:
                Shape = ShapeMaths.Box(StartPos.Value, ClickPlane.instance.GetGridPosition().Value);
                break;
            case BuildType.Footprint:
                Shape = FromFootPrint();
                break;
        }

        return Shape;
    }

    List<Vector2Int> FromFootPrint()
    {
        var pos = ClickPlane.instance.GetGridPosition().Value;
        List<Vector2Int> shape = new List<Vector2Int>();
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                if (CurrentBluePrint.FootPrintData.rows[x+2].row[y+2])
                {
                    shape.Add(new Vector2Int(pos.x + x, pos.y + y));
                }
            }
        }

        return shape;
    }


    bool CanBuildHere(List<Vector2Int> shape)
    {
        foreach (var spot in shape)
        {
            if (Zoo.instance.Structures[spot] == true )
            {
                return false;
            }
        }

        return true;
    }
}
