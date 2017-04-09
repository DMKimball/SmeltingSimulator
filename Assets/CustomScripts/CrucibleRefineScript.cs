using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrucibleRefineScript : MonoBehaviour {

    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<OreRefineScript>() == null) return;
        if(other.GetComponent<OreRefineScript>().Refine(Time.deltaTime)) GetComponent<AudioSource>().Play();
    }
}
