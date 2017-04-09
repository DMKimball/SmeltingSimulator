using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongGrabScript : MonoBehaviour {

    public float grabForce = 50.0f;

    private GameObject grabbedObject;
    private bool grabbing;
    private Rigidbody rbody;

	// Use this for initialization
	void Awake () {
        grabbedObject = null;
        rbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Tong hit something");
        if (grabbedObject != null || !collision.gameObject.tag.Equals("Grabbable"))
        {
            Debug.Log("Can't tong the something");
            return;
        }

        Debug.Log("Found Tongable object");

        grabbedObject = collision.gameObject;
        FixedJoint joint = grabbedObject.AddComponent<FixedJoint>();
        joint.connectedBody = rbody;
        //joint.breakForce = grabForce;
    }

    public void Release()
    {
        if (grabbedObject == null) return;

        Destroy(grabbedObject.GetComponent<FixedJoint>());
        grabbedObject.GetComponent<Rigidbody>().velocity = rbody.velocity;
        grabbedObject = null;
    }
}
