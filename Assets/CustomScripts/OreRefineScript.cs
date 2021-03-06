﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreRefineScript : MonoBehaviour {

    public float refineTime = 7.5f;
    public GameObject prefab;
    public SpawnOre spawner;

    private float currRefiningTime;

    void Awake()
    {
        currRefiningTime = 0;
    }

    public bool Refine(float time)
    {
        currRefiningTime += time;
        //Debug.Log("Time accumulated: " + currRefiningTime);
        if (currRefiningTime < refineTime) return false;
        Instantiate(prefab, transform.position, Quaternion.identity);
        spawner.DecrementPopulation();
        Destroy(gameObject);
        return true;
    }
}
