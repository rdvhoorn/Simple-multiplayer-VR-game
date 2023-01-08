using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SnapGearScript : NetworkBehaviour
{

    public Vector3 startingLocation;
    public List<GameObject> snapObjects;
    private GameObject snappedTo = null;
    public GameObject mechenical;

    private Camera currentCamera = null;
    private Vector3 mOffset;
    private float mZCoord;

    void OnMouseDown() {
        currentCamera = NetworkManager.LocalClient.PlayerObject.GetComponentInChildren(typeof(Camera)).gameObject.GetComponent<Camera>();

        mZCoord = currentCamera.WorldToScreenPoint(transform.position).z;
        // Store offset = gameobject world pos - mouse world pos
        mOffset = transform.position - GetMouseAsWorldPoint();

        snappedTo = null;
        // mechenical.GetComponent<Mechenical>().DisableGears();
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

        foreach (GameObject snapObject in snapObjects) {
            if (Vector3.Distance(transform.position, snapObject.transform.position) < 0.5) {
                SetBoxPositionServerRpc(snapObject.transform.position);
                snappedTo = snapObject;
                // mechenical.GetComponent<Mechenical>().EnableGears();
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

    void Update() {
        if (snappedTo == null) return;

        RotateServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    public void RotateServerRpc() {
        float rotationalSpeed = snappedTo.GetComponent<SnapRotationObject>().GetRotationSpeed();
        transform.Rotate(Vector3.right * rotationalSpeed * Time.deltaTime);
    }
}
