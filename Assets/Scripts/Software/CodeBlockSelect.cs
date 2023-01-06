using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeBlockSelect : MonoBehaviour
{
    public GameObject CodeStuff;
    public GameObject bgBlock;
    public GameObject textObject;

    bool selected = false;
    public Material BaseMaterial;
    public Material SelectedMaterial;
    private Renderer Renderer;

    private TextMeshPro text;

    private GameObject codeBlock = null;
    
    public GameObject[] AllowedButtonGroups;

    void Start() {
        Renderer = bgBlock.GetComponent<Renderer>();
        Renderer.material = BaseMaterial;

        text = textObject.GetComponent<TextMeshPro>();
    }

    void OnMouseDown() {
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

    public void ResetCodeBlock() {
        codeBlock = null;
    }

    public void SetCode(GameObject newCodeBlock) {
        codeBlock = newCodeBlock;
        text.text = newCodeBlock.GetComponent<CodeClick>().showText;
    }
}
