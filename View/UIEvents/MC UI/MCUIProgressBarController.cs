using System;
using UnityEngine;
using UnityEngine.Events;

/* Installed on the progress bar component in the MutipleChoice GameObject*/
public class MCUIProgressBarController : MonoBehaviour
{
    private GameObject selectedFrame;
    private float progressBarValue;
    private UnityEvent<string> selected;
    // unselected is the Event that a choice is unselected *by direct user interaction*,
    // i.e. unselection caused by another question selected in a single-choice question does NOT fire this
    private UnityEvent<string> unselected;
    private string letter;
    private static readonly int progressBarMaxScale = 8;
    private static readonly float selectingSpeedConstant = 1.1f;
    private static readonly float unselectingSpeedConstant = 0.8f;

    void Start()
    {
        selectedFrame = Util.findPeerGameObjectByName(gameObject, "SelectedFrame");
        letter = Util.findPeerGameObjectByName(gameObject, "Choices")
                .GetComponent<MCUIChoicesController>().letter;
        selected = new UnityEvent<string>();
        unselected = new UnityEvent<string>();
        selected.AddListener(gameObject.transform.parent.parent.gameObject
            .GetComponent<MCUIMainController>().selectingChoice);
        selected.AddListener(Util.findPeerGameObjectByName(gameObject, "Collider")
            .GetComponent<MCUIColliderController>().setUnselectable);
        unselected.AddListener(gameObject.transform.parent.parent.gameObject
            .GetComponent<MCUIMainController>().unselectingChoice);
        unselected.AddListener(Util.findPeerGameObjectByName(gameObject, "Collider")
            .GetComponent<MCUIColliderController>().setSelectable);
        progressBarValue = 0;
    }

    void Update()
    {
        updateProgressBarUI();
    }

    private void updateProgressBarUI()
    {
        gameObject.transform.localScale = new Vector3(progressBarMaxScale * progressBarValue,
            progressBarMaxScale * progressBarValue,
            1);
    }

    /* 
     * Increase progress bar value by time elapsed. 
     * Listen to collision event at the button.
     */
    public void increaseProgressBarValue()
    {
        progressBarValue += Time.deltaTime * selectingSpeedConstant;
        progressBarValue = Mathf.Clamp(progressBarValue, 0, 1);
        if (progressBarValue == 1) {
            if (letter == null) throw new Exception("Letter not set:" + letter);
            selected.Invoke(letter);
            selectedFrame.SetActive(true);
        }
    }

    /* 
     * Decrease progress bar value by time elapsed. 
     * Listen to collision event at the button.
     */
    public void decreaseProgressBarValue()
    {
        bool isSelected = gameObject.transform.parent.parent.gameObject
            .GetComponent<MCUIMainController>().isLetterSelected[letter];
        if (!isSelected) return; // only decrease if the choice is already selected.
        progressBarValue -= Time.deltaTime * unselectingSpeedConstant; // unselecting should be slower
        progressBarValue = Mathf.Clamp(progressBarValue, 0, 1);
        if (progressBarValue == 0)
        {
            string unselectedLetter = Util.findPeerGameObjectByName(gameObject, "Choices")
                .GetComponent<MCUIChoicesController>().letter;
            unselected.Invoke(unselectedLetter);
            selectedFrame.SetActive(false);
        }
    }

    /*
     * Set progress bar value to zero.
     * Listen to collision exit event.
     */
    public void zeroProgressBarValue()
    {
        progressBarValue = 0;
        /* 
         * Code Path Explanation: If the exit event occurs when the choice is still selected, then 
         * it means that the "unselecting" attempt failed. We need to re-pump the progress bar value
         * back to full. 
         */
        // else progressBarValue = 1;
    }

    public void fullProgressBarValue() { progressBarValue = 1; }

}
