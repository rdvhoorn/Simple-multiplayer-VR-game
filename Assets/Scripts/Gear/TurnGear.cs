using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TurnGear : NetworkBehaviour
{

    public float rotationalSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        UpdateGearServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    void UpdateGearServerRpc() {
        transform.Rotate(Vector3.up * rotationalSpeed * Time.deltaTime);
    }
}
