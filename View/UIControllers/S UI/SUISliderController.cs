using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SUISliderController : MonoBehaviour
{
    private UnityEvent<int, int> SliderValueChanged;
    private GameObject SliderTorus;
    private GameObject SliderValueDisplayer;
    private GameObject SliderCollider;
    private Transform ActiveGrabberTransform;
    private bool IsActive;
    private int SliderID;

    // 0.62 -> -0.81 // Magic Number. Bad!!! To-be refactor into property files in future version
    private readonly float SliderLeft = 0.62f;
    private readonly float SliderRight = -0.81f;

    void Start()
    {
        InitSUISlider();
    }

    void Update()
    {
        if (IsActive) UpdateSliderStatus();
    }

    public void InitSUISlider() 
    {
        SliderTorus = gameObject.transform.Find("SliderTorus").gameObject;
        SliderValueDisplayer = gameObject.transform.Find("SliderValueDisplayer").gameObject;
        SliderCollider = gameObject.transform.Find("SliderCollider").gameObject;
        IsActive = false;
        SliderID = int.Parse(gameObject.name.Last().ToString());
        SliderValueChanged = new UnityEvent<int, int>();
        SliderValueChanged.AddListener(SliderValueDisplayer.GetComponent<SUISliderValueDisplayerController>().SetSliderValueTo);

        /* Initialize sub-controllers */
        SliderValueDisplayer.GetComponent<SUISliderValueDisplayerController>().InitSUIValueDisplayer();
        SliderCollider.GetComponent<SUISliderColliderController>().InitSliderCollider();
    }

    /// <summary>
    /// Cache the active grabber's transform for the slider bar to follow.
    /// </summary>
    /// <param name="ActiveGrabberTransform">Transform of the active grabber of the collider.</param>
    public void SliderGrabbedHandler(Transform ActiveGrabberTransform)
    {
        this.ActiveGrabberTransform = ActiveGrabberTransform;
        IsActive = true;
    }

    public void SliderUngrabbedHandler() 
    {
        ActiveGrabberTransform = null;
        IsActive = false;

        // Reset collider's position
        SliderCollider.transform.position = SliderTorus.transform.position;
        SliderCollider.transform.rotation = Quaternion.identity; // TODO: Rotate by 90 degrees in X
    }

    /// <summary>
    /// Follow active grabber and broadcast slider value change event. 
    /// </summary>
    private void UpdateSliderStatus() 
    {
        /* Move slider and value displayer forward */
        Vector3 localChanges = gameObject.transform.InverseTransformPoint(ActiveGrabberTransform.position);

        float newX = Mathf.Clamp(localChanges.x, SliderRight, SliderLeft);
        SliderTorus.transform.localPosition = new Vector3(newX,
            SliderTorus.transform.localPosition.y,
            SliderTorus.transform.localPosition.z);
        SliderValueDisplayer.transform.localPosition = new Vector3(newX,
            SliderValueDisplayer.transform.localPosition.y,
            SliderValueDisplayer.transform.localPosition.z);

        /* Broadcast slider value change event */
        int sliderValue = (int) Math.Ceiling(Mathf.Abs(newX - SliderLeft) / Mathf.Abs(SliderRight - SliderLeft) * 10);
        SliderValueChanged.Invoke(SliderID, sliderValue);
    }
}
