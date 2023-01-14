using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSlot : MonoBehaviour
{

    public Color baseColor = new Color(102f/255, 119f/255, 97f/255, 1);
    public Color selectedColor = new Color(84f/255, 94f/255, 86f/255, 1);
    [Range(0, 4)]
    public int ID;

    public List<GameObject> inputWires;
    public List<GameObject> outputWires;

    private Renderer blockRenderer;

    private bool selected = false;


    // Start is called before the first frame update
    void Start()
    {
        blockRenderer = GetComponentInChildren<Renderer>();
        
        updateColor();
    }

    public bool getSelected() {
        return selected;
    }

    public void toggle() {
        selected = !selected;

        updateColor();
    }

    public void activate() {
        selected = true;
        updateColor();
    }

    public void deactivate() {
        selected = false;
        updateColor();
    }

    public void updateColor() {
        if (selected) {
            blockRenderer.material.SetColor("_Color", selectedColor);
        } else {
            blockRenderer.material.SetColor("_Color", baseColor);
        }
    }

    
}
