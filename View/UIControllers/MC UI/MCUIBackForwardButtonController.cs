using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCUIBackForwardButtonController : MonoBehaviour
{
    public enum ForwardButtonStatus { Hide = 0, Show = 1 };
    private ForwardButtonStatus CurrentForwardButtonStatus;
    private GameObject ForwardButtonStatusCollider;
    private GameObject ForwardButtonStatusHider;

    public enum BackwardButtonStatus { Hide = 0, Show = 1 };
    private BackwardButtonStatus CurrentBackwardButtonStatus;
    private GameObject BackwardButtonStatusCollider;
    private GameObject BackwardButtonStatusHider;

    void Start()
    {
        CurrentForwardButtonStatus = ForwardButtonStatus.Hide;
        ForwardButtonStatusCollider = Util.FindChildGameObjectByName(
            Util.FindChildGameObjectByName(gameObject, "ForwardButton"),
            "ForwardButtonCollider");
        ForwardButtonStatusHider = Util.FindChildGameObjectByName(
            Util.FindChildGameObjectByName(gameObject, "ForwardButton"),
            "ForwardButtonHider");

        CurrentBackwardButtonStatus = BackwardButtonStatus.Hide;
        BackwardButtonStatusCollider = Util.FindChildGameObjectByName(
            Util.FindChildGameObjectByName(gameObject, "BackwardButton"),
            "BackwardButtonCollider");
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
        if ((int)status == 0)
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
