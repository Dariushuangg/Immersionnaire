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
    private GameObject SliderCollider;
    private Transform ActiveGrabberTransform;
    private bool IsActive;
    private int SliderID;

    void Start()
    {
        InitController();
    }

    void Update()
    {
        if (IsActive) UpdateSliderStatus();
    }

    public void InitController() 
    {
        SliderValueChanged = new UnityEvent<int, float>();
        SliderTorus = gameObject.transform.Find("SliderTorus").gameObject;
        SliderValueDisplayer = gameObject.transform.Find("SliderValueDisplayer").gameObject;
        SliderCollider = gameObject.transform.Find("SliderCollider").gameObject;
        IsActive = false;
        SliderID = int.Parse(gameObject.name.Last().ToString());
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
        Vector3 localChanges = gameObject.transform.InverseTransformPoint(ActiveGrabberTransform.position);
        // 0.62 -> -0.81 // Magic Number. Bad! To-be refactor into property files in future version
        float newX = Mathf.Clamp(localChanges.x, -0.81f, 0.62f);
        SliderTorus.transform.localPosition = new Vector3(newX,
            SliderTorus.transform.localPosition.y,
            SliderTorus.transform.localPosition.z);
        SliderValueDisplayer.transform.localPosition = new Vector3(newX,
            SliderValueDisplayer.transform.localPosition.y,
            SliderValueDisplayer.transform.localPosition.z);
    }
}
