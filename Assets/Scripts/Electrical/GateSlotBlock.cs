using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSlotBlock : MonoBehaviour
{

    private GateSlot slot;
    private Electrical electrical;

    void Start() {
        slot = GetComponentInParent<GateSlot>();
        electrical = GetComponentInParent<Electrical>();
    }

    void OnMouseDown() {
        electrical.clickedSlot(slot);
    }
}
