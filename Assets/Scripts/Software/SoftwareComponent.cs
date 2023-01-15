using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public enum SoftwareState {CORRECT, PLAUSIBLE, WRONG};

public class SoftwareComponent : NetworkBehaviour
{

    private GameObject selected = null;
    public GameObject[] buttonGroups;

    public void appendCodeToSelected(GameObject newCode) {
        if (selected == null) return;
        
        selected.GetComponent<CodeBlockSelect>().SetCode(newCode);
    }

    public void newSelected(GameObject newlySelectedObject) {
        foreach (GameObject obj in buttonGroups) {
                obj.GetComponent<ButtonGroup>().deactivate();
            }

        if (selected != null) {
            selected.GetComponent<CodeBlockSelect>().Deselect();

            foreach (GameObject obj in selected.GetComponent<CodeBlockSelect>().AllowedButtonGroups) {
                obj.GetComponent<ButtonGroup>().deactivate();
            }
        }

        if (newlySelectedObject == selected) {
            selected = null;
            newlySelectedObject.GetComponent<CodeBlockSelect>().Deselect();

            foreach (GameObject obj in buttonGroups) {
                obj.GetComponent<ButtonGroup>().activate();
            }

            return;
        }

        newlySelectedObject.GetComponent<CodeBlockSelect>().Select();
        foreach (GameObject obj in newlySelectedObject.GetComponent<CodeBlockSelect>().AllowedButtonGroups) {
                obj.GetComponent<ButtonGroup>().activate();
            }
        selected = newlySelectedObject;
    }

    public SoftwareState CalculateSoftwareState() {
        return SoftwareState.CORRECT;
    }

    public int[] GetCurrentSoftwareParameters() {
        return new int[1];
    }
}
