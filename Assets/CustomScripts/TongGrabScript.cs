using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongGrabScript : MonoBehaviour {
    public PC_Grab.HandSide side;

    public Rigidbody rbody;
    public GameObject grabbableObject;
    public bool grabbing;

    // Use this for initialization
    void Awake()
    {
        rbody = GetComponentInParent<Rigidbody>();
        grabbing = false;
        grabbableObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!grabbing && grabbableObject != null && ((side == PC_Grab.HandSide.right && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0) || (side == PC_Grab.HandSide.left && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0)))
        {
            grabbing = true;
            FixedJoint joint = grabbableObject.AddComponent<FixedJoint>();
            joint.connectedBody = rbody;
        }

        if (grabbing && ((side == PC_Grab.HandSide.right && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) == 0) || (side == PC_Grab.HandSide.left && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) == 0)))
        {
            grabbing = false;
            FixedJoint joint = grabbableObject.GetComponentInParent<FixedJoint>();
            Destroy(joint);
            grabbableObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity((side == PC_Grab.HandSide.left) ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch);
            //grabbableObject = null;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Tongs collided with object: " + other.name);
        if (grabbing) return;
        if (other.gameObject.GetComponent<Rigidbody>() != null && other.tag.Equals("Grabbable"))
        {
            Debug.Log("Tongs found grabbable object");
            grabbableObject = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (grabbing) return;
        if (other.gameObject == grabbableObject) grabbableObject = null;
    }
}
