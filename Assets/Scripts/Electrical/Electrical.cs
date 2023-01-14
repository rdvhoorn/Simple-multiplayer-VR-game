using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GateTypes {AND, OR, XOR, NAND};

public class Electrical : MonoBehaviour
{
    private bool initialized = false;
    private Gate[] standardGates;

    private GateSlot currentSelectedSlot = null;
    private GameObject[] instantiatedGates = new GameObject[5];
    private GateSlot[] allSlots;

    public GameObject AndGatePrefab;
    public GameObject OrGatePrefab;
    public GameObject XorGatePrefab;
    public GameObject NandGatePrefab;

    void Start() {
        standardGates = GetComponentsInChildren<Gate>();
        allSlots = GetComponentsInChildren<GateSlot>();

        for (int i = 0; i < instantiatedGates.Length; i++) {
            instantiatedGates[i] = null;
        }
    }

    void Update() {
        if (initialized) return;

        update_gates();
        initialized = true;
    }

    public void update_gates() {
        foreach (Gate gate in standardGates) {
            gate.gate_update();
        }

        foreach (GameObject gateGameObject in instantiatedGates) {
            if (gateGameObject == null) return;

            gateGameObject.GetComponent<Gate>().gate_update();
        }
    }

    public void clickedSlot(GateSlot clickedSlot) {
        bool currentlySelected = clickedSlot.getSelected();

        foreach (GateSlot slot in allSlots) {
            slot.deactivate();
            
        }

        for (int i = 0; i < allSlots.Length; i++) {
            allSlots[i].deactivate();
            if (instantiatedGates[i] != null) {
                instantiatedGates[i].GetComponent<Gate>().deactivate();
            }
        }

        if (!currentlySelected) {
            clickedSlot.activate();
            currentSelectedSlot = clickedSlot;
            if (instantiatedGates[clickedSlot.ID] != null) {
                instantiatedGates[clickedSlot.ID].GetComponent<Gate>().activate();
            }

        } else {
            currentSelectedSlot = null;
        }
    }

    public void clickedSelectableGate(GateTypes type) {
        if (currentSelectedSlot == null) return;

        GameObject currentSpawnedObject = instantiatedGates[currentSelectedSlot.ID];
        Destroy(currentSpawnedObject);

        GameObject gateGameObject = Instantiate(GetPrefab(type), currentSelectedSlot.transform.position + new Vector3(0, 0, 0.1f), currentSelectedSlot.transform.rotation, gameObject.transform);

        Gate gate = gateGameObject.GetComponent<Gate>();
        gate.SetElectrical(gameObject.GetComponent<Electrical>());
        gate.selectable = true;
        gate.SetWires(currentSelectedSlot.inputWires, currentSelectedSlot.outputWires);
        gate.SetGateSlot(currentSelectedSlot);
        instantiatedGates[currentSelectedSlot.ID] = gateGameObject;

        update_gates();
    }

    private GameObject GetPrefab(GateTypes type) {
        if (type == GateTypes.AND) {
            return AndGatePrefab;
        } else if (type == GateTypes.OR) {
            return OrGatePrefab;
        } else if (type == GateTypes.XOR) {
            return XorGatePrefab;
        } else {
            return NandGatePrefab;
        }
    } 
}
