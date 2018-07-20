using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rate : MonoBehaviour {

    Text RateText;

    // Use this for initialization
    void Start()
    {
        RateText = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        RateText.text =  Zoo.instance.Rate + "%";

    }
}
