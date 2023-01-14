using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public Color onColor = Color.green;
    public Color offColor = Color.red;
    

    private bool state = false;
    private Renderer[] meshRenderers;

    void Start() {
        meshRenderers = gameObject.GetComponentsInChildren<Renderer>();

        updateColor();
    }

    public bool getState() {
        return state;
    }

    public void setState(bool newState) {
        state = newState;

        updateColor();
    }

    private void updateColor() {
        if (state) {
            foreach (Renderer r in meshRenderers) {
                r.material.SetColor("_Color", onColor);
            }
        } else {
            foreach (Renderer r in meshRenderers) {
                r.material.SetColor("_Color", offColor);
            }
        }
    }
}
