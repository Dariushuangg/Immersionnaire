using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCUIForwardButtonCollider : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        GameObject UIBoard = gameObject.transform.parent.parent.parent.gameObject;
        if (UIBoard == null) throw new Exception("Can't find GameObject: UIBoard");
        UIBoard.GetComponent<MCUIMainController>().ForwardButtonSelected.Invoke();
    }
}
