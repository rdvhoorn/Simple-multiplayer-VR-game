using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ClickObject : NetworkBehaviour
{
    // Whether the block is currently active (only checked on server)
    public bool isActive = true;

    // The material to change
    [SerializeField] private Material material; 

    void OnMouseDown() {
        // If mouse down, switch color through serverRpc
        SwitchColorServerRpc();
    }

    [ClientRpc]
    void SwitchColorClientRpc(bool switchToActive) {
        if (switchToActive) {
            material.color = Color.green;
        } else {
            material.color = Color.blue;
        }
    }


    [ServerRpc(RequireOwnership=false)]
    void SwitchColorServerRpc() {
        // If server says its currently active, switch on all clients to off.
        if (isActive) {
            SwitchColorClientRpc(false);
        } else {
            // Else switch all clients to on.
            SwitchColorClientRpc(true);
        }
        // Switch the boolean.
        isActive = !isActive;
    }
}
