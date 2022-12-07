using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSettings : NetworkBehaviour
{
    // List of spawn locations
    public List<Vector3> spawnPositions = new List<Vector3>();

    void Update() {
        // Ensure that only the server executes code below
        if (!IsServer) return;

        // If the player presses the S key, call the ClientRpc
        if (Input.GetKeyDown(KeyCode.S)) {
            GoToSpawnLocationClientRpc();
        }
    }

    [ClientRpc]
    void GoToSpawnLocationClientRpc() {
        // Ensure its the owner of the object that executes the code below
        if (!IsOwner) return;

        // Move the player to the spawn location
        transform.position = spawnPositions[(int)OwnerClientId];
    }
}
