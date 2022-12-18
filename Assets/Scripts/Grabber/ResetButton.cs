using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ResetButton : NetworkBehaviour
{
    public GameObject Grabber;

    void OnMouseDown() {
        Grabber.GetComponent<MoveGrabber>().ResetGrabberMovement();
    }
}
