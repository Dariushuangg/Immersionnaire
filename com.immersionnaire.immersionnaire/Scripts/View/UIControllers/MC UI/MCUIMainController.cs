using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MCUIMainController : MonoBehaviour, UIMainController
{
    public Dictionary<string, bool> IsLetterSelected { get; } = new Dictionary<string, bool>();
    public UnityEvent MainButtomSelected { get; set; }
    public UnityEvent ForwardButtonSelected { get; set; }
    public UnityEvent BackwardButtonSelected { get; set; }

    private int numOfQuestions;
    private bool allowMultipleChoices;
    private Dictionary<string, GameObject> SelectedFrames = new Dictionary<string, GameObject>();

    public void InitControllers(Question question)
    {
        MCQuestion mcquestion = (MCQuestion)question;

        /* Register event handlers for UI events */
        MainButtomSelected = new UnityEvent();
        MainButtomSelected.AddListener(ConfirmingChoice);
        GameObject presenter = GameObject.FindGameObjectWithTag("Immersionnaire-Presenter");
        ForwardButtonSelected = new UnityEvent();
        ForwardButtonSelected.AddListener(presenter.GetComponent<QuestionnairePresenter>().Forward);
        BackwardButtonSelected = new UnityEvent();
        BackwardButtonSelected.AddListener(presenter.GetComponent<QuestionnairePresenter>().Back);

        this.numOfQuestions = mcquestion.numOfChoices;
        this.allowMultipleChoices = mcquestion.AllowMultipleChoice();

        /* Initialize an array to keep track of whether a choice is selected */
        for (int i = 0; i < numOfQuestions; i++)
        {
            IsLetterSelected.Add(Util.indexToLetter(i), false);
        }

        /* Initialize backward and forward button controllers */
        gameObject.transform.Find("BackForwardButtons")
            .GetComponent<UIBackForwardButtonController>()
            .InitForBackwardButtonControllers();

        GameObject choices = gameObject.transform.Find("Choices").gameObject;
        List<GameObject> allChoiceParents = Util.GetAllChildGameObjects(choices);
        if (choices == null) throw new Exception("choices is null: choices");
        foreach (GameObject choiceParent in allChoiceParents)
        {
            /* Initialize an array to keep track of selected frames that will react to UI events */
            string letter = choiceParent.name;
            Transform selectedFrame = choiceParent.transform.Find("SelectedFrame" + letter);
            if (selectedFrame == null) throw new Exception("No such frame found: selectedFrame");
            SelectedFrames.Add(letter, selectedFrame.gameObject);

            /* Enable collider controllers */
            Transform collider = choiceParent.transform.Find("Collider" + letter);
            Util.checkNull(collider);
            collider.GetComponent<MCUIChoiceCollider>().enabled = true;
        }

        // Initialize main text
        gameObject.transform.Find("MainTitleTextRef").gameObject
            .GetComponent<UIMainTextController>().InitMainText(question.index);
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

    public void ShowResponseHistory(Response response)
    {
        MCQResponse mcqResponse = (MCQResponse)response;
        foreach(string letter in mcqResponse.SelectedChoices)
        {
            IsLetterSelected[letter] = true;
        }

        GameObject choices = gameObject.transform.Find("Choices").gameObject;
        List<GameObject> allChoiceParents = Util.GetAllChildGameObjects(choices);
        foreach (GameObject choiceParent in allChoiceParents)
        {
            if (IsLetterSelected[choiceParent.name]) RenderAsSelected(choiceParent);
        }
    }

    /// <summary>
    /// Render a choice in MCQ UIBoard as selected by setting progress bar and selected frame.
    /// </summary>
    private void RenderAsSelected(GameObject choiceParent)
    {
        string letter = choiceParent.name;
        GameObject selectedFrame = choiceParent.transform.Find("SelectedFrame" + letter).gameObject;
        selectedFrame.SetActive(true);
        GameObject progressBar = choiceParent.transform.Find("ProgressBar" + letter).gameObject;
        progressBar.GetComponent<MCUIProgressBarController>().fullProgressBarValue();
    }


    private void ConfirmingChoice()
    {
        List<string> selectedChoices = new List<string>();
        foreach (string letter in IsLetterSelected.Keys) {
            if (IsLetterSelected[letter]) selectedChoices.Add(letter);
        }
        MCQResponse response = new MCQResponse(IsAnyChoiceSelected(), selectedChoices);
        GameObject presenter = GameObject.FindGameObjectWithTag("Immersionnaire-Presenter");
        presenter.GetComponent<QuestionnairePresenter>().Submit(response);
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
                .GetComponent<UIMainButtonController>()
                .SetButtonStatus(UIMainButtonController.MainBottomStatus.Show);
        }
        else
        {
            gameObject.transform.Find("MainButton")
                .GetComponent<UIMainButtonController>()
                .SetButtonStatus(UIMainButtonController.MainBottomStatus.Hide);
        }
    }

    private void debugPrintSelected() { 
        foreach(string l in IsLetterSelected.Keys)
        {
            // Util.SetDebugLog("IsLetterSelected -" + l, " " + IsLetterSelected[l], true);
        }
    }
}
