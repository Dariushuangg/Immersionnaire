using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainButtonCollider : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        // Invoke the selecting event defined in main controller 
        GameObject UIBoard = gameObject.transform.parent.parent.gameObject;
        if (UIBoard == null) throw new Exception("GameObject is null: " + UIBoard);
        UIBoard.GetComponent<MCUIMainController>().MainButtomSelected.Invoke();
    }
}
