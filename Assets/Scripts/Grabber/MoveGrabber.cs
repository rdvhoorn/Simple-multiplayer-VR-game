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
        startPosition = transform.position;
        startRotation = transform.rotation;
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
    public void ResetGrabber() {
        transform.position = startPosition;
        transform.rotation = startRotation;
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
