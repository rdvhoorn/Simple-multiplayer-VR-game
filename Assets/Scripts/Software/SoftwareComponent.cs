using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SoftwareComponent : NetworkBehaviour
{
    public List<GameObject> CodeBlockContentsBlock0;
    public List<GameObject> CodeBlockContentsBlock1;
    public List<GameObject> CodeBlockContentsBlock2;

    public GameObject selected = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CodeBlockSelect(int codeBlockId) {
    }

    public void newSelected(GameObject newlySelectedObject) {
        if (selected != null) {
            selected.GetComponent<CodeBlockSelect>().Deselect();
        }

        if (newlySelectedObject == selected) {
            selected = null;
            newlySelectedObject.GetComponent<CodeBlockSelect>().Deselect();
            return;
        }

        newlySelectedObject.GetComponent<CodeBlockSelect>().Select();
        selected = newlySelectedObject;
    }
}
