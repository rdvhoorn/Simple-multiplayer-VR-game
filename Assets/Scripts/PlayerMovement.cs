using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float positionRange = 5f;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        RandomSpawnServerRpc();
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure only owner executes code below
        if (!IsOwner) return;

        // Get input from player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        // Move player
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);

        // Rotate player
        if (movementDirection != Vector3.zero) {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    [ServerRpc(RequireOwnership=false)]
    private void RandomSpawnServerRpc() {
        // transform player to random spawn position
        transform.position = new Vector3(Random.Range(positionRange, -positionRange), 0, Random.Range(positionRange, -positionRange));
        transform.rotation = new Quaternion(0, 180, 0, 0);
    }
}
