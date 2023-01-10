using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLever : MonoBehaviour
{
    public GameObject connectedGear;
    private Rigidbody connectedGearRb;
    public Rigidbody leverRb;

    private Quaternion previousRotation;


    // Start is called before the first frame update
    void Start()
    {
        connectedGearRb = connectedGear.GetComponent<Rigidbody>();
        previousRotation = leverRb.rotation;
        connectedGear.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion newRotation = leverRb.rotation;

        connectedGearRb.MoveRotation(newRotation);

        previousRotation = newRotation;
    }
}
