using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MCUIMainController : MonoBehaviour
{
    public Dictionary<string, bool> IsLetterSelected { get; } = new Dictionary<string, bool>();
    public UnityEvent MainButtomSelected;
    public UnityEvent ForwardButtonSelected { get; }
    public UnityEvent BackwardButtonSelected { get; }
    private int numOfQuestions;
    private bool allowMultipleChoices;
    private Dictionary<string, GameObject> SelectedFrames = new Dictionary<string, GameObject>();

    void OnEnable()
    {
        // InitializeMCUI() must be called after UIFactory has generated the UI content.
        // UI Factory should initialize the MainController by informing it the number of questions, and if MC is allowed.
        InitializeMCUI(4, true);
    }

    /// <summary>
    /// Callback function when a choice is unselected.
    /// </summary>
    /// <param name="letter">The choice that is unselected.</param>
    public void UnselectingChoice(string letter)
    {
        SelectedFrames[letter].SetActive(false);
        IsLetterSelected[letter] = false;
        ShowMainButton(IsAnyChoiceSelected());
        debugPrintSelected();
    }

    /// <summary>
    /// Callback function when a choice is selected.
    /// </summary>
    /// <param name="letter">The choice that is selected.</param>
    public void SelectingChoice(string letter)
    {
        SelectedFrames[letter].SetActive(true);
        IsLetterSelected[letter] = true;

        if (!allowMultipleChoices)
        {
            for (int i = 0; i < numOfQuestions; i ++)
            {
                if (Util.indexToLetter(i) != letter)
                {
                    SelectedFrames[Util.indexToLetter(i)].SetActive(false);
                    IsLetterSelected[Util.indexToLetter(i)] = false;
                    Util.FindPeerGameObjectByName(SelectedFrames[Util.indexToLetter(i)], "ProgressBar" + Util.indexToLetter(i))
                        .GetComponent<MCUIProgressBarController>().zeroProgressBarValue();
                }
            }
        }
        ShowMainButton(IsAnyChoiceSelected());
        debugPrintSelected();
    }

    private void InitializeMCUI(int numOfQuestions, bool allowMultipleChoices)
    {
        /* Register event handlers for UI events */
        MainButtomSelected = new UnityEvent();
        MainButtomSelected.AddListener(ConfirmingChoice);

        this.numOfQuestions = numOfQuestions;
        this.allowMultipleChoices = allowMultipleChoices;

        /* Initialize an array to keep track of whether a choice is selected */
        for (int i = 0; i < numOfQuestions; i++)
        {
            IsLetterSelected.Add(Util.indexToLetter(i), false);
        }

        /* Initialize an array to keep track of selected frames that will react to UI events */
        GameObject choices = gameObject.transform.Find("Choices").gameObject;
        List<GameObject> allChoiceParents = Util.GetAllChildGameObjects(choices);
        if (choices == null) throw new Exception("choices is null: choices");
        foreach (GameObject choiceParent in allChoiceParents)
        {
            string letter = choiceParent.name;
            Debug.Log("Letter:" + letter);
            Transform selectedFrame = choiceParent.transform.Find("SelectedFrame" + letter);
            if (selectedFrame == null) throw new Exception("No such frame found: selectedFrame");
            SelectedFrames.Add(letter, selectedFrame.gameObject);
        }
    }

    private void ConfirmingChoice()
    {
        List<string> selectedChoices = new List<string>();
        foreach (string letter in IsLetterSelected.Keys) {
            if (IsLetterSelected[letter]) selectedChoices.Add(letter);
        }
        MCQResponse response = new MCQResponse(IsAnyChoiceSelected(), selectedChoices);
        GameObject presenter = GameObject.FindGameObjectWithTag("Immersionnaire-Presenter");
        Util.SetDebugLog("Presenter check: ", presenter.name, true);
        presenter.GetComponent<QuestionnairePresenter>().Confirm(response);
    }

    /// <summary>
    /// Return true if there exists a selected choice.
    /// </summary>
    private bool IsAnyChoiceSelected() {
        bool show = false;
        foreach (string letter in IsLetterSelected.Keys) {
            show |= IsLetterSelected[letter];
        }
        return show;
    }

    private void ShowMainButton(bool show) {
        if (show)
        {
            gameObject.transform.Find("MainButton")
                .GetComponent<MCUIMainButtonController>()
                .SetCurrentStatus(MCUIMainButtonController.MainBottomStatus.Confirm);
        }
        else
        {
            gameObject.transform.Find("MainButton")
                .GetComponent<MCUIMainButtonController>()
                .SetCurrentStatus(MCUIMainButtonController.MainBottomStatus.Hide);
        }
    }

    private void debugPrintSelected() { 
        foreach(string l in IsLetterSelected.Keys)
        {
            Util.SetDebugLog("IsLetterSelected -" + l, " " + IsLetterSelected[l], true);
        }
    }
}
