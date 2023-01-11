using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SUISliderController : MonoBehaviour
{
    private UnityEvent<int, float> SliderValueChanged;
    private GameObject SliderTorus;
    private GameObject SliderValueDisplayer;
    private Transform ActiveGrabberTransform;
    private bool IsActive;
    private int SliderID;

    void Update()
    {
        if (IsActive) UpdateSliderStatus();
    }

    public void InitController() 
    {
        SliderValueChanged = new UnityEvent<int, float>();
        SliderTorus = gameObject.transform.Find("SliderTorus").gameObject;
        SliderValueDisplayer = gameObject.transform.Find("SliderValueDisplayer").gameObject;
        IsActive = false;
        SliderID = int.Parse(gameObject.name.Last().ToString());
        Util.SetDebugLog("CONVERT check: ", SliderID + "", true);
    }

    /// <summary>
    /// Cache the active grabber's transform for the slider bar to follow.
    /// </summary>
    /// <param name="ActiveGrabberTransform">Transform of the active grabber of the collider.</param>
    public void SetGrabberTo(Transform ActiveGrabberTransform)
    {
        this.ActiveGrabberTransform = ActiveGrabberTransform;
    }

    /// <summary>
    /// Follow active grabber and broadcast slider value change event. 
    /// </summary>
    private void UpdateSliderStatus() { }
}
