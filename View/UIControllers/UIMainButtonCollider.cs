using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainButtonCollider : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        Util.SetDebugLog("OnTriggerExit called", "", true); 
        // Invoke the selecting event defined in main controller 
        GameObject UIBoard = gameObject.transform.parent.parent.gameObject;
        if (UIBoard == null) throw new Exception("GameObject is null: " + UIBoard);
        UIMainController mainController = (UIMainController) UIBoard.GetComponent(typeof(UIMainController));
        if (mainController == null) throw new Exception("mainController is null in " + UIBoard.name);
        mainController.MainButtomSelected.Invoke();
        Util.SetDebugLog("OnTriggerExit called", "", true);
    }

}
