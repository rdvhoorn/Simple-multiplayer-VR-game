using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeClick : MonoBehaviour
{
    public GameObject CodeStuff;
    public string showText;

    void OnMouseDown() {
        CodeStuff.GetComponent<SoftwareComponent>().appendCodeToSelected(gameObject);
    }
}
