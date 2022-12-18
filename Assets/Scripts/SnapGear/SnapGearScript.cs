using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SnapGearScript : NetworkBehaviour
{

    public Vector3 startingLocation;
    public List<Vector3> snapLocations;

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

        foreach (Vector3 snapLocation in snapLocations) {
            if (Vector3.Distance(transform.position, snapLocation) < 0.5) {
                SetBoxPositionServerRpc(snapLocation);
                return;
            }
        }

        SetBoxPositionServerRpc(startingLocation);
    }

    [ServerRpc(RequireOwnership = false)]
    void SnapOnMouseUpServerRpc() {
    }

    [ServerRpc(RequireOwnership = false)]
    void SetBoxPositionServerRpc(Vector3 position) {
        transform.position = position;
    }
}
