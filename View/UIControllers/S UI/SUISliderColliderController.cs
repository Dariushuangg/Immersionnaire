using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class SUISliderColliderController : MonoBehaviour
{
    // Notify slider controller that the slider is grabbed by a Transform
    private UnityEvent<Transform> ColliderGrabbedBy;
    // Notify slider controller that the slider is no longer grabbed
    private UnityEvent ColliderUngrabbed;
    private UnityEvent<SelectExitEventArgs> ColliderUngrabbedEvent;
    private UnityEvent<SelectEnterEventArgs> ColliderGrabbedEvent; // defined in grab interactable

    public void InitSliderCollider()
    {
        ColliderGrabbedEvent = GetComponent<XRGrabInteractable>().selectEntered;
        ColliderGrabbedEvent.AddListener(GetGrabberTransform);
        ColliderUngrabbedEvent = GetComponent<XRGrabInteractable>().selectExited;
        ColliderUngrabbedEvent.AddListener(UnsetGrab);
        ColliderUngrabbed = new UnityEvent();
        ColliderUngrabbed.AddListener(gameObject.transform.parent.gameObject
            .GetComponent<SUISliderController>().SliderUngrabbedHandler);
        ColliderGrabbedBy = new UnityEvent<Transform>();
        ColliderGrabbedBy.AddListener(gameObject.transform.parent.gameObject
            .GetComponent<SUISliderController>().SliderGrabbedHandler);
    }

    /// <summary>
    /// Extract the interactor's transform from SelectEnterEventArgs
    /// Maybe we should try anonymous function here.
    /// </summary>
    /// <param name="eventArgs"></param>
    private void GetGrabberTransform(SelectEnterEventArgs eventArgs)
    {
        // SelectEnterEventArgs is-a BaseInteractionEventArgs
        Transform grabberTransform = eventArgs.interactor.gameObject.transform;
        ColliderGrabbedBy.Invoke(grabberTransform);
    }

    private void UnsetGrab(SelectExitEventArgs eventArgs)
    {
        ColliderUngrabbed.Invoke();
    }
}
