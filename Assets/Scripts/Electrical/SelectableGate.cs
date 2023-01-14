using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableGate : MonoBehaviour
{
    public GateTypes type;
    private Electrical electrical;

    void Start() {
        electrical = GetComponentInParent<Electrical>();
    }

    void OnMouseDown() {
        electrical.clickedSelectableGate(type);
    }
}
