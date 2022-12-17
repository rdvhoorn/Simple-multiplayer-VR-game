using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGrabber : MonoBehaviour
{

    public float upwardsSpeed = 0.2f;
    public float rotationalSpeed = 20f;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private bool moving = true;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 6) {
            moving = false;
        }

        if (!moving) return; 

        transform.position += Vector3.up * upwardsSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotationalSpeed * Time.deltaTime);
    }

    public void StartGrabberMovement() {
        Debug.Log("MoveGRABBER");
        moving = true;
    }

    public void ResetGrabber() {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
