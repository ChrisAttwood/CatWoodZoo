using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FootPrintData {

    [System.Serializable]
    public struct Row
    {
        public bool[] row;
    }

    public Row[] rows = new Row[5];
}
