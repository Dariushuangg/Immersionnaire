using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class SUISliderColliderController : MonoBehaviour
{
    private UnityEvent<Transform> ColliderGrabbedBy; // defined in slider controller 
    private UnityEvent<SelectEnterEventArgs> colliderGrabbedEvent; // defined in grab interactable
    public void InitController()
    {
        colliderGrabbedEvent = GetComponent<XRBaseInteractable>().selectEntered;
        colliderGrabbedEvent.AddListener(GetGrabberTransform);
        ColliderGrabbedBy = new UnityEvent<Transform>();
        ColliderGrabbedBy.AddListener(gameObject.transform.parent.gameObject
            .GetComponent<SUISliderController>().SetGrabberTo);
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
}
