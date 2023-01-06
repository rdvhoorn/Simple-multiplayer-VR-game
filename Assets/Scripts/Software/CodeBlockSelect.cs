using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockSelect : MonoBehaviour
{
    public GameObject CodeStuff;
    public int codeBlockId;

    bool selected = false;
    public Material BaseMaterial;
    public Material SelectedMaterial;
    private Renderer Renderer;

    void Start() {
        Renderer = gameObject.GetComponent<Renderer>();
        Renderer.material = BaseMaterial;
    }

    void OnMouseDown() {
        CodeStuff.GetComponent<SoftwareComponent>().CodeBlockSelect(codeBlockId);
        CodeStuff.GetComponent<SoftwareComponent>().newSelected(gameObject);
    }

    public void Select() {
        selected = true;
        Renderer.material = SelectedMaterial;
    }

    public void Deselect() {
        selected = false;
        Renderer.material = BaseMaterial;
    }
}
