using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongReleaseScript : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        TongGrabScript tong = other.GetComponent<TongGrabScript>();
        if (tong != null) tong.Release();
    }
}
