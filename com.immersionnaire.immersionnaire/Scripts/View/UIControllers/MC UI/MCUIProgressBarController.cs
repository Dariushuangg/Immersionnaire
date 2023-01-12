using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* Installed on the progress bar component in the MutipleChoice GameObject*/
public class MCUIProgressBarController : MonoBehaviour
{
    private UnityEvent<string> selected;
    // UnityEvent<string> unselected is the Event that a choice is unselected *by direct user interaction*,
    // e.g. unselection caused by another question selected in a single-choice question should NOT fire this
    private UnityEvent<string> unselected;

    private GameObject selectedFrame;
    private float progressBarValue;
    private string letter;
    private static readonly float progressBarMaxWidthScale = 0.3f;
    private static readonly float progressBarMaxLengthScale = 0.5f;
    private static readonly float progressBarHeightScale = 0.3f;
    private static readonly float selectingSpeedConstant = 1.1f;
    private static readonly float unselectingSpeedConstant = 0.7f;

    void Start()
    {
        letter = gameObject.transform.parent.name;
        selectedFrame = Util.FindPeerGameObjectByName(gameObject, "SelectedFrame" + letter);
        selected = new UnityEvent<string>();
        unselected = new UnityEvent<string>();
        selected.AddListener(gameObject.transform.parent.parent.parent.gameObject
            .GetComponent<MCUIMainController>().SelectingChoice);
        selected.AddListener(Util.FindPeerGameObjectByName(gameObject, "Collider" + letter)
            .GetComponent<MCUIChoiceCollider>().setUnselectable);
        // selected.AddListener(GameObject.FindGameObjectWithTag("Immersionnaire-ContentBoard")
        //    .GetComponent<MCContentController>().showContentEffectOn);
        unselected.AddListener(gameObject.transform.parent.parent.parent.gameObject
            .GetComponent<MCUIMainController>().UnselectingChoice);
        unselected.AddListener(Util.FindPeerGameObjectByName(gameObject, "Collider" + letter)
            .GetComponent<MCUIChoiceCollider>().setSelectable);
        unselected.AddListener(Util.FindPeerGameObjectByName(gameObject, "Collider" + letter)
            .GetComponent<MCUIChoiceCollider>().setUnselectingSucceeded);
        // unselected.AddListener(GameObject.FindGameObjectWithTag("Immersionnaire-ContentBoard")
        //    .GetComponent<MCContentController>().hideContentEffectOn);
        progressBarValue = 0;
    }

    void Update()
    {
        updateProgressBarUI();
    }

    /// <summary>
    /// Increase progress bar value by time elapsed. Listen to collision event at the button.
    /// </summary>
    public void increaseProgressBarValue()
    {
       progressBarValue += Time.deltaTime * selectingSpeedConstant;
        progressBarValue = Mathf.Clamp(progressBarValue, 0, 1);
        if (progressBarValue == 1)
        {
            selected.Invoke(letter);
            selectedFrame.SetActive(true);
        }
    }

    /// <summary>
    /// Decrease progress bar value by time elapsed. Listen to collision event at the button.
    /// </summary>
    public void decreaseProgressBarValue()
    {
        bool isSelected = gameObject.transform.parent.parent.parent.gameObject
            .GetComponent<MCUIMainController>().IsLetterSelected[letter];
        if (!isSelected) return; // only decrease if the choice is already selected.
        progressBarValue -= Time.deltaTime * unselectingSpeedConstant; // unselecting should be slower
        progressBarValue = Mathf.Clamp(progressBarValue, 0, 1);
        if (progressBarValue == 0)
        {
            unselected.Invoke(letter);
            selectedFrame.SetActive(false);
        }
    }

    /// <summary>
    /// Set progress bar value to zero. Listen to collision exit event.
    /// </summary>
    public void zeroProgressBarValue()
    {
        progressBarValue = 0;
    }

    /// <summary>
    /// Set progress bar value to full. Listen to collision exit event.
    /// </summary>
    public void fullProgressBarValue()
    {
        progressBarValue = 1;
    }

    private void updateProgressBarUI()
    {
        gameObject.transform.localScale = new Vector3(progressBarMaxLengthScale * progressBarValue,
            progressBarMaxWidthScale * progressBarValue,
            progressBarHeightScale);
    }

}
