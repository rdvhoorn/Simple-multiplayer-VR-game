using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonM : MonoBehaviour
{
    public GameObject mechenical;

    void OnMouseDown() {
        mechenical.GetComponent<Mechenical>().ResetGearRotations();
    }
}
