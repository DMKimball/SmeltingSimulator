using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrucibleRefineScript : MonoBehaviour {

    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<OreRefineScript>() == null) return;
        other.GetComponent<OreRefineScript>().Refine(Time.deltaTime);
    }
}
