using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Person : MonoBehaviour {


    Transform model;

    float xSeed;
    float ySeed;

    private void Awake()
    {
        model = transform.GetChild(0);
        xSeed = Random.Range(0f, 999f);
        ySeed = Random.Range(0f, 999f);
    }

    private void Update()
    {
        model.localPosition = new Vector3(Mathf.PerlinNoise(xSeed, Time.time) - 0.5f,0f, Mathf.PerlinNoise(ySeed, Time.time) - 0.5f);
    }
}
