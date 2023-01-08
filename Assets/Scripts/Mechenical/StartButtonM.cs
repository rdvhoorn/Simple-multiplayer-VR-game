using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonM : MonoBehaviour
{
    public GameObject mechenical;

    void OnMouseDown() {
        mechenical.GetComponent<Mechenical>().StartMechenical();
    }
}
