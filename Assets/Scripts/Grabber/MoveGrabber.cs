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


    public GameObject leftHandle;
    public GameObject rightHanlde;
    private bool GrabberOpen = true;
    private bool GrabberStatic = true;
    private float GrabberSpeed = 5f;


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

    [ServerRpc(RequireOwnership = false)]
    void ToggleGrabberHandleServerRpc() {
        GrabberOpen = !GrabberOpen;
        GrabberStatic = false;
    }

    // Reset Grabber
    public void ResetGrabberMovement() {
        ResetGrabberMovementServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ResetGrabberMovementServerRpc() {
        transform.position = startPosition;
        transform.rotation = startRotation;

        ToggleGrabberHandleServerRpc();
        moving = false;
    }

    // update Grabber Movement (server side )
    [ServerRpc(RequireOwnership = false)]
    void UpdateGrabberServerRpc() {
        if (leftHandle.transform.rotation.y*90 > 16 && !GrabberOpen && !GrabberStatic) {
            GrabberStatic = true;
        } else if (leftHandle.transform.rotation.y < 0 && GrabberOpen && !GrabberStatic) {
            GrabberStatic = true;
        }

        if (!GrabberStatic) {
            if (GrabberOpen) {
                leftHandle.transform.Rotate(Vector3.down * GrabberSpeed * Time.deltaTime);
                rightHanlde.transform.Rotate(Vector3.up * GrabberSpeed * Time.deltaTime);
            } else {
                leftHandle.transform.Rotate(Vector3.up * GrabberSpeed * Time.deltaTime);
                rightHanlde.transform.Rotate(Vector3.down * GrabberSpeed * Time.deltaTime);
            }
        }

        if (transform.position.y > 6) {
            moving = false;
        }

        if (!moving) return; 

        transform.position += Vector3.up * upwardsSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotationalSpeed * Time.deltaTime);
    }

    [ServerRpc(RequireOwnership = false)]
    void CloseGrabberHandleServerRpc() {

    }

}
