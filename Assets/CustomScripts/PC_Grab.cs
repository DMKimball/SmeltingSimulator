using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Grab : MonoBehaviour {

    public enum HandSide { left, right };

    public HandSide side;

    private Collider hand_collider;
    private Rigidbody rbody;

    private GameObject grabbableObject;
    private bool grabbing;

	// Use this for initialization
	void Awake () {
        hand_collider = GetComponent<Collider>();
        rbody = GetComponent<Rigidbody>();
        grabbing = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!grabbing && grabbableObject != null && ((side == HandSide.right && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0) || (side == HandSide.left && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0)))
        {
            grabbing = true;
            FixedJoint joint = grabbableObject.AddComponent<FixedJoint>();
            joint.connectedBody = rbody;
        }

        if (grabbing && ((side == HandSide.right && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) == 0) || (side == HandSide.left && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) == 0)))
        {
            grabbing = false;
            FixedJoint joint = grabbableObject.GetComponent<FixedJoint>();
            Destroy(joint);
            grabbableObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity((side == HandSide.left) ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch);
            grabbableObject = null;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collided with object");
        if (grabbing) return;
        if (other.gameObject.GetComponent<Rigidbody>() != null && other.tag.Equals("Grabbable"))
        {
            //Debug.Log("Found grabbable object");
            grabbableObject = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (grabbing) return;
        if (other.gameObject == grabbableObject) grabbableObject = null;
    }
}
