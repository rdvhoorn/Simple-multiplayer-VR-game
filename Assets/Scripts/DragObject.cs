using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;



public class DragObject : NetworkBehaviour {
    private Camera currentCamera = null;
    private Vector3 mOffset;
    private float mZCoord;

    void OnMouseDown() {
        currentCamera = NetworkManager.LocalClient.PlayerObject.GetComponentInChildren(typeof(Camera)).gameObject.GetComponent<Camera>();

        mZCoord = currentCamera.WorldToScreenPoint(transform.position).z;
        // Store offset = gameobject world pos - mouse world pos
        mOffset = transform.position - GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint() {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;
        
        // Convert it to world points
        return currentCamera.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag() {
        SetBoxPositionServerRpc(GetMouseAsWorldPoint() + mOffset);
    }

    void OnMouseUp() {
        currentCamera = null;
    }

    [ServerRpc(RequireOwnership = false)]
    void SetBoxPositionServerRpc(Vector3 position) {
        transform.position = position;
    }
}