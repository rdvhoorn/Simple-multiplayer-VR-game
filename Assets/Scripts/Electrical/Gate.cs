using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public List<GameObject> InputWires;

    public List<GameObject> OutputWires;

    public GateTypes type;

    public bool selectable = false;
    public Color baseColor = new Color(102f/255, 119f/255, 97f/255, 1);
    public Color selectedColor = new Color(84f/255, 94f/255, 86f/255, 1);

    private GateSlot gateSlot;
    private Electrical electrical;


    void Start() {
        if (selectable) {
            updateColor(selectedColor);
        }
    }

    public void gate_update() {
        List<bool> inputs = new List<bool>();
        foreach (GameObject inputWire in InputWires) {
            inputs.Add(inputWire.GetComponent<Wire>().getState());
        }

        bool output = true;
        if (type == GateTypes.AND) {
            output = AND(inputs);
        } else if (type == GateTypes.OR) {
            output = OR(inputs);
        } else if (type == GateTypes.XOR) {
            output = XOR(inputs);
        } else if (type == GateTypes.NAND) {
            output = NAND(inputs);
        }

        foreach(GameObject outputWire in OutputWires) {
            outputWire.GetComponent<Wire>().setState(output);
        }
    }

    private bool AND(List<bool> inputs) {
        if (inputs[0] && inputs[1]) {
            return true;
        }

        return false;
    }

    private bool OR(List<bool> inputs) {
        if (inputs[0] || inputs[1]) {
            return true;
        }

        return false;
    }

    private bool XOR(List<bool> inputs) {
        if (inputs[0] && inputs[1]) {
            return false;
        } else if (inputs[0] || inputs[1]) {
            return true;
        } 

        return false;
    }

    private bool NAND(List<bool> inputs) {
        return !AND(inputs);
    }

    public void SetWires(List<GameObject> i, List<GameObject> o) {
        InputWires = i;
        OutputWires = o;
    }

    public void SetGateSlot(GateSlot slot) {
        gateSlot = slot;
    }

    public void SetElectrical(Electrical e) {
        electrical = e;
    }

    void OnMouseDown() {
        if (!selectable) return;

        electrical.clickedSlot(gateSlot);
    }

    public void activate() {
        if (!selectable) return;

        updateColor(selectedColor);
    }

    public void deactivate() {
        if (!selectable) return;

        updateColor(baseColor);
    }

    public void updateColor(Color color) {
        GetComponent<MeshRenderer>().material.SetColor("_Color", color);
    }
}
