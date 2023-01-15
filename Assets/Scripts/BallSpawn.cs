using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour
{
    public GameObject ballPrefab;
    private GameObject ballInstance = null;

    public void SpawnBall() {
        if (ballInstance == null) {
            ballInstance = Instantiate(ballPrefab, transform.position, transform.rotation, transform);
        }
    }

    public void DeleteBall() {
        if (ballInstance != null) {
            Destroy(ballInstance);
            ballInstance = null;
        }
    }
}
