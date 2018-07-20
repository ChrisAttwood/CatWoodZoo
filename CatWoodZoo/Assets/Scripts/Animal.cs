using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {

    [Range(1,5)]
    public int Size = 1;

   
    [Range(1,5)]
    public int Appeal = 1;


    void Start () {
        Zoo.instance.Animals.Add(this);
	}
	
	
	void Update () {
		
	}
}
