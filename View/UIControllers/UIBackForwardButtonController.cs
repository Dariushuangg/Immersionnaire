using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBackForwardButtonController : MonoBehaviour
{
    public enum ForwardButtonStatus { Hide = 0, Show = 1 };
    private ForwardButtonStatus CurrentForwardButtonStatus;
    private GameObject ForwardButtonStatusCollider;
    private GameObject ForwardButtonStatusHider;

    public enum BackwardButtonStatus { Hide = 0, Show = 1 };
    private BackwardButtonStatus CurrentBackwardButtonStatus;
    private GameObject BackwardButtonStatusCollider;
    private GameObject BackwardButtonStatusHider;

    /// <summary>
    /// Must be called when generating the UI Board.
    /// </summary>
    public void InitForBackwardButtonControllers()
    {
        CurrentForwardButtonStatus = ForwardButtonStatus.Hide;
        ForwardButtonStatusCollider = Util.FindChildGameObjectByName(
            Util.FindChildGameObjectByName(gameObject, "ForwardButton"),
            "ForwardButtonCollider");
        ForwardButtonStatusCollider.SetActive(false);
        ForwardButtonStatusHider = Util.FindChildGameObjectByName(
            Util.FindChildGameObjectByName(gameObject, "ForwardButton"),
            "ForwardButtonHider");

        CurrentBackwardButtonStatus = BackwardButtonStatus.Hide;
        BackwardButtonStatusCollider = Util.FindChildGameObjectByName(
            Util.FindChildGameObjectByName(gameObject, "BackwardButton"),
            "BackwardButtonCollider");
        BackwardButtonStatusCollider.SetActive(false);
        BackwardButtonStatusHider = Util.FindChildGameObjectByName(
            Util.FindChildGameObjectByName(gameObject, "BackwardButton"),
            "BackwardButtonHider");
    }

    /// <summary>
    /// Set the status of the forward button, change corresponding UI.
    /// </summary>
    /// <param name="status"></param>
    public void SetForwardButtonStatus(ForwardButtonStatus status)
    {
        CurrentForwardButtonStatus = status;
        ChangeForwardButtonTo(status);
    }

    private void ChangeForwardButtonTo(ForwardButtonStatus status) 
    {
        if (status == ForwardButtonStatus.Hide)
        {
            ForwardButtonStatusCollider.SetActive(false);
            ForwardButtonStatusHider.SetActive(true);
        }
        else 
        {
            ForwardButtonStatusCollider.SetActive(true);
            ForwardButtonStatusHider.SetActive(false);
        }
    }

    /// <summary>
    /// Set the status of the forward button, change corresponding UI.
    /// </summary>
    /// <param name="status"></param>
    public void SetBackwardButtonStatus(BackwardButtonStatus status)
    {
        CurrentBackwardButtonStatus = status;
        ChangeBackwardButtonTo(status);
    }

    private void ChangeBackwardButtonTo(BackwardButtonStatus status)
    {
        if ((int)status == 0)
        {
            BackwardButtonStatusCollider.SetActive(false);
            BackwardButtonStatusHider.SetActive(true);
        }
        else
        {
            BackwardButtonStatusCollider.SetActive(true);
            BackwardButtonStatusHider.SetActive(false);
        }
    }
}
