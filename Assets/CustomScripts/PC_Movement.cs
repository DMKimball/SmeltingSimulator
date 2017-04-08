using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Movement : MonoBehaviour {

    private Rigidbody rbody;
    public Transform referenceCam;
    public Transform player;

    public float moveSpeed = 5.0f;
    public float jumpSpeed = 1.0f;
    public float jumpWait = 0.5f;
    public float teleportWait = 0.5f;
    public float verticalOffset = 0.5f;

    private bool jumpReady;
    private bool teleportReady;

	// Use this for initialization
	void Awake () {
        rbody = GetComponent<Rigidbody>();
        teleportReady = jumpReady = true;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 lStickPos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        float xMove = -lStickPos.x;
        float zMove = lStickPos.y;

        Vector3 forward = referenceCam.forward;
        Vector3 right = Vector3.Cross(forward, Vector3.up);

        Vector3 velocity = Vector3.Normalize(forward * zMove + right * xMove) * moveSpeed;

        velocity.y = (OVRInput.Get(OVRInput.Button.One) && jumpReady) ? jumpSpeed : rbody.velocity.y;
        if (OVRInput.Get(OVRInput.Button.One) && jumpReady) StartCoroutine("JumpCooldown");

        rbody.velocity = velocity;

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick) && teleportReady)
        {
            player.position = transform.position + Vector3.up * verticalOffset;
            StartCoroutine("TeleportCooldown");
        }
    }

    IEnumerator JumpCooldown()
    {
        jumpReady = false;
        yield return new WaitForSeconds(jumpWait);
        jumpReady = true;
        yield break;
    }

    IEnumerator TeleportCooldown()
    {
        teleportReady = false;
        yield return new WaitForSeconds(teleportWait);
        teleportReady = true;
        yield break;
    }
}
