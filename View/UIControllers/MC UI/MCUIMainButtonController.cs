using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCUIMainButtonController : MonoBehaviour
{
    public enum MainBottomStatus { Hide = 0, Confirm = 1, Modify = 2 };
    public MainBottomStatus CurrentStatus;
    private GameObject MainBottomCollider;
    private GameObject MainBottomHider;

    void Start()
    {
        MainBottomCollider = Util.FindChildGameObjectByName(gameObject, "MainButtonCollider");
        MainBottomHider = Util.FindChildGameObjectByName(gameObject, "MainButtonHider");
        CurrentStatus = MainBottomStatus.Hide;
    }

    /// <summary>
    /// Set the status of the main button to the given parameter.
    /// </summary>
    public void SetCurrentStatus(MainBottomStatus status)
    {
        CurrentStatus = status;
        ChangeTo(CurrentStatus);
    }

    private void ChangeTo(MainBottomStatus status)
    {
        if ((int)status == 0)
        {
            MainBottomCollider.SetActive(false);
            MainBottomHider.SetActive(true);
        }
        else if ((int)status == 1)
        {
            MainBottomCollider.SetActive(true);
            MainBottomHider.SetActive(false);
        }
        else
        {
            /* TODO: Add the "Modify" button asset */
        }

    }

}
