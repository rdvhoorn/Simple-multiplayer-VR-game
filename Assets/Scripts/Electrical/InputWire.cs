using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputWire : MonoBehaviour
{
    public bool state;
    private bool initialized = false;


    void Update() {
        if (initialized) return;

        GetComponent<Wire>().setState(state);
        initialized = true;
    }
}
