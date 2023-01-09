using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MCUIBaseBoardCollider : MonoBehaviour
{
    public int count;

    private void Start()
    {
        count = 0;
    }

    public void colliderGrabbedHandler()
    {
        count += 1;
        GameObject baseBoard = Util.FindPeerGameObjectByName(gameObject.transform.parent.gameObject, "BaseBoard");
        // Util.SetDebugLog("hi", "" + count, true);
        // Util.SetDebugLog("baseboard check", baseBoard.name, true);
        baseBoard.GetComponent<MCUIBaseController>().colliderDragged.Invoke(gameObject.name, true);
    }


    public void colliderPlacedHandler()
    {
        GameObject baseBoard = Util.FindPeerGameObjectByName(gameObject.transform.parent.gameObject, "BaseBoard");
        baseBoard.GetComponent<MCUIBaseController>().colliderDragged.Invoke(gameObject.name, false);
    }
}
