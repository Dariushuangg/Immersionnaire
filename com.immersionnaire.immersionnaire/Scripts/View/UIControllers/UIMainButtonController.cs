using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainButtonController : MonoBehaviour
{
    // Use FSM pattern.
    public enum MainButtonType { Confirm, Modify };
    public enum MainBottomStatus { Hide, Show };
    private MainButtonType mainButtomType;
    private MainBottomStatus CurrentStatus;
    private GameObject MainBottomCollider;
    private GameObject MainBottomHider;

    void Start()
    {
        MainBottomCollider = Util.FindChildGameObjectByName(gameObject, "MainButtonCollider");
        MainBottomHider = Util.FindChildGameObjectByName(gameObject, "MainButtonHider");
        CurrentStatus = MainBottomStatus.Hide;
    }

    /// <summary>
    /// Initialize the type of the button. Must be called first.
    /// </summary>
    public void SetButtonType(MainButtonType mainButtonType)
    {
        this.mainButtomType = mainButtonType;
    }

    /// <summary>
    /// Hide or show the main button.
    /// </summary>
    public void SetButtonStatus(MainBottomStatus status)
    {
        CurrentStatus = status;
        ChangeStatusTo(CurrentStatus);
    }

    private void ChangeStatusTo(MainBottomStatus status)
    {
        if (status == MainBottomStatus.Hide)
        {
            MainBottomCollider.SetActive(false);
            MainBottomHider.SetActive(true);
        }
        else 
        {
            MainBottomCollider.SetActive(true);
            MainBottomHider.SetActive(false);
        }
    }

}
