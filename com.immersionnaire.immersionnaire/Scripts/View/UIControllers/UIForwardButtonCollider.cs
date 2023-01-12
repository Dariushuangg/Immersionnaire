using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIForwardButtonCollider : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        GameObject UIBoard = gameObject.transform.parent.parent.parent.gameObject;
        Util.checkNull(UIBoard);
        UIMainController UIMainController = (UIMainController)UIBoard.GetComponent(typeof(UIMainController));
        UIMainController.ForwardButtonSelected.Invoke();
    }
}
