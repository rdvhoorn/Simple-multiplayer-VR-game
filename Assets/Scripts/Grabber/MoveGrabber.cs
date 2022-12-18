using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MoveGrabber : NetworkBehaviour
{

    public float upwardsSpeed = 0.2f;
    public float rotationalSpeed = 20f;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        startRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGrabberServerRpc();
    }

    // Start grabber movement
    public void StartGrabberMovement() {
        StartGrabberMovementServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    void StartGrabberMovementServerRpc() {
        moving = true;
    }

    // Reset Grabber
    public void ResetGrabberMovement() {
        ResetGrabberMovementServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ResetGrabberMovementServerRpc() {
        transform.position = startPosition;
        transform.rotation = startRotation;

        moving = false;
    }

    // update Grabber Movement (server side )
    [ServerRpc(RequireOwnership = false)]
    void UpdateGrabberServerRpc() {
        if (transform.position.y > 6) {
            moving = false;
        }

        if (!moving) return; 

        transform.position += Vector3.up * upwardsSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotationalSpeed * Time.deltaTime);
    }

}
