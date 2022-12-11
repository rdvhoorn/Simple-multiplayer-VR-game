using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CameraController : NetworkBehaviour
{
    public GameObject cameraHolder;

    public override void OnNetworkSpawn() {
        if (!IsLocalPlayer) { 
            cameraHolder.SetActive(false);
        }
    }

    public void Update() {
        cameraHolder.transform.position = transform.position;
    }
}
