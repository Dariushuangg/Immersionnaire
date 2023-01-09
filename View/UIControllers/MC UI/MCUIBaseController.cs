using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MCUIBaseController : MonoBehaviour
{
    public UnityEvent<string, bool> colliderDragged;
    public GameObject BaseBoardColliderLeft;
    public GameObject BaseBoardColliderRight;
    private readonly Vector3 OffSetVector = new Vector3(2.4f, 0, 0); // Right-to-left Vector
    private Vector3 ScaledOffSetVector;
    private bool FollowLeftCollider;
    private bool FollowRightCollider;

    void Start()
    {
        ScaledOffSetVector = OffSetVector* gameObject.transform.parent.localScale.x;
        colliderDragged = new UnityEvent<string, bool>();
        colliderDragged.AddListener(followCollider);
        FollowLeftCollider = false;
        FollowRightCollider = false;
    }

    void Update()
    {
        if (FollowLeftCollider) followLeftCollider();
        // if (FollowRightCollider) followRightCollider();
        // Util.SetDebugLog("Collider left dragged", "" + FollowLeftCollider, true);
        // Util.SetDebugLog("Collider right dragged", "" + FollowRightCollider, true);
    }

    private void followCollider(string which, bool isDragged) {
        // Util.SetDebugLog("Collider " + which + " Dragged", "" + isDragged, true);
        if (which == "Left") FollowLeftCollider = isDragged;
        if (which == "Right") FollowRightCollider = isDragged;
    }

    private void followLeftCollider() {

        // Set right collider transform
        BaseBoardColliderRight.transform.position = BaseBoardColliderLeft.transform.position - ScaledOffSetVector;
        BaseBoardColliderRight.transform.rotation = BaseBoardColliderLeft.transform.rotation;
        
        // Set main board transform
        gameObject.transform.parent.position = BaseBoardColliderLeft.transform.position - 0.5f * ScaledOffSetVector;
        gameObject.transform.parent.rotation = BaseBoardColliderLeft.transform.rotation;
    }

    private void followRightCollider() { }
}
