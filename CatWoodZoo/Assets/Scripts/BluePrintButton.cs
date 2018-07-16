using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrintButton : MonoBehaviour {

    public BluePrint BluePrint;

    public void Click()
    {
        Builder.instance.SetActiveBluePrint(BluePrint);
    }
}
