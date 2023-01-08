using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechenical : MonoBehaviour
{
    public GameObject startObject;
    public GameObject ball;

    public void StartMechenical() {
        Debug.Log("Start!");
        startObject.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeRotationX;
        startObject.GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
        ball.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeAll;
    }
}
