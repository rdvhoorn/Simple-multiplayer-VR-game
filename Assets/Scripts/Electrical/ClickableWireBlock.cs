using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableWireBlock : MonoBehaviour
{

    private Wire wire;
    private Electrical electrical;

    void Start() {
        wire = gameObject.GetComponentInParent<Wire>();
        electrical = GetComponentInParent<Electrical>();
    }

    void OnMouseDown() {
        if (wire.getState()) {
            wire.setState(false);
        } else {
            wire.setState(true);
        }

        electrical.update_gates();
    }
}
