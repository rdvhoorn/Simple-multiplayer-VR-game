using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SnapRotationObject : NetworkBehaviour
{
    public float rotationSpeed = 10f;

    public float GetRotationSpeed() {
        return rotationSpeed;
    }
}
