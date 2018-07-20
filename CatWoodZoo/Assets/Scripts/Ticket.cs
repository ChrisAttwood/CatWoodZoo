using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ticket : MonoBehaviour {

    Text TicketText;
	// Use this for initialization
	void Start () {
        TicketText = GetComponent<Text>();

    }

    public void Alter(int amount)
    {
        Zoo.instance.Ticket += amount;
    }
	
	// Update is called once per frame
	void Update () {
        TicketText.text = "£"+Zoo.instance.Ticket;

    }
}
