using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cash : MonoBehaviour {

    Text CashText;

	// Use this for initialization
	void Start () {
        CashText = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
        CashText.text = "£"+Zoo.instance.Cash;

    }
}
