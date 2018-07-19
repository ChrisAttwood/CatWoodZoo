using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BluePrint : ScriptableObject {

    public GameObject Structure;

    public bool IsPath;
    public bool IsFence;

    public BuildType BuildType;

    public bool SingleGameObject;

    [Header("Footprint")]
    public FootPrintData FootPrintData;
   

}
