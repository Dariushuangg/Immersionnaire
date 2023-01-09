using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionnairePresenter : MonoBehaviour
{
    public Transform DefaultUIBoardTransform;
    public Transform DefaultContentBoardTransform;
    private int currQuestionIndex;
    private int firstUnansweredIndex;
    private QuestionnireModel model;
    private ContentFactory contentFactory;
    private UIFactory uiFactory;
    private GameObject currentContentBoard;
    private GameObject currentUIBoard;

    private GameObject prevContentBoard; // Necessary; See comments for RemoveCurrentContentBoard()
    private GameObject prevUIBoard;

    void Start()
    {
        model = GetComponent<QuestionnireModel>();
        model.initQuestionnaireModel();
        contentFactory = GetComponent<ContentFactory>();
        uiFactory = GetComponent<UIFactory>();

        /* Generate the first question */
        Question firstQuestion = model.getQuestionAt(0);
        currentContentBoard = contentFactory.GenerateContentBoard(firstQuestion, DefaultContentBoardTransform);
        currentUIBoard = uiFactory.GenerateUIBoard(firstQuestion, DefaultUIBoardTransform);

        // move the initial UIBoard slightly
        currentUIBoard.transform.position = currentUIBoard.transform.position - new Vector3(0, 0.1f, 0);
        currentUIBoard.transform.rotation = Quaternion.Euler(0, 180f, 0);
        currentUIBoard.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        currQuestionIndex = 0;
        firstUnansweredIndex = 0;
    }

    /*
     * Display the next question.
     * Listen to user selecting the "next" button.
     */
    public void ShowNextQuestion() {
        currQuestionIndex++;
        Question nextQuestion = model.getQuestionAt(currQuestionIndex);
        // Util.SetDebugLog("index debug", "getQuestionAt:" + currQuestionIndex + "prompt is" + nextQuestion.prompt, true);

        // Display question on ContentBoard at the previous ContentBoard's location
        Transform PrevContentBoardTransform = currentContentBoard.transform;
        RemoveCurrentContentBoard();
        currentContentBoard = contentFactory.GenerateContentBoard(nextQuestion, PrevContentBoardTransform);

        // Display correct UIBoard at the previous UIBoard's location
        Transform PrevUIBoardTransform = currentUIBoard.transform;
        RemoveCurrentUIBoard();
        currentUIBoard = uiFactory.GenerateUIBoard(nextQuestion, PrevUIBoardTransform);

        SetBackForwardButtons();

    }

    /*
     * Display the previous question.
     * Listen to user selecting the "back" button.
     */
    public void ShowPrevQuestion()
    {
        currQuestionIndex--;
        Question prevQuestion = model.getQuestionAt(currQuestionIndex);
        // Display question

        if (currQuestionIndex == 0)
        {
            // Allow "back" button
        }
        else
        {
            // Disallow "back" button
        }
    }

    /// <summary>
    /// Callback function for user clicking the Confirm button. 
    /// </summary>
    /// <param name="response">The response to the currently displayed question.</param>
    public void Confirm(Response response) {
        if (currQuestionIndex == firstUnansweredIndex)
        {
            // Confirm submission of new response, and display the next question automatically.
            model.addResponse(response);
            firstUnansweredIndex++;
        }
        else
        {
            // Confirm modification of previous response, and display the next question automatically.
            model.changeResponseAt(response, currQuestionIndex);
        }
        ShowNextQuestion();
        Util.SetDebugLog("111Check Index", "currQuestionIndex = " + currQuestionIndex + ", firstUnansweredIndex = " + firstUnansweredIndex, true);
    }

    private void RemoveCurrentContentBoard() {
        // This is necessary because Destroy() happens very late in the life cycle
        // If we Destroy(currentContentBoard) directly, it occurs after the next line
        // of RemoveCurrentContentBoard() is executed, and will thus destroy the newly
        // generated ContentBoard.
        prevContentBoard = currentContentBoard; 
        Destroy(prevContentBoard);
    }
    private void RemoveCurrentUIBoard() {
        prevUIBoard = currentUIBoard; // Same reason.
        Destroy(prevUIBoard);
    }

    /// <summary>
    /// Wrapper around SetForwardButtonStatus() in MCUIBackForwardButtonController
    /// </summary>
    private void SetForwardButtonStatus(MCUIBackForwardButtonController.ForwardButtonStatus status) 
    {
        currentUIBoard.transform.Find("BackForwardButtons")
            .GetComponent<MCUIBackForwardButtonController>()
            .SetForwardButtonStatus(status);
    }

    /// <summary>
    /// Wrapper around SetBackwardButtonStatus() in MCUIBackForwardButtonController
    /// </summary>
    private void SetBackwardButtonStatus(MCUIBackForwardButtonController.BackwardButtonStatus status)
    {
        currentUIBoard.transform.Find("BackForwardButtons")
            .GetComponent<MCUIBackForwardButtonController>()
            .SetBackwardButtonStatus(status);
    }

    private void SetBackForwardButtons()
    {
        // Set forward button visibility
        if (currQuestionIndex == firstUnansweredIndex) SetForwardButtonStatus(MCUIBackForwardButtonController.ForwardButtonStatus.Hide);
        else SetForwardButtonStatus(MCUIBackForwardButtonController.ForwardButtonStatus.Show);

        // Set backward button visibility
        if (currQuestionIndex != 0) SetBackwardButtonStatus(MCUIBackForwardButtonController.BackwardButtonStatus.Show);
        else SetBackwardButtonStatus(MCUIBackForwardButtonController.BackwardButtonStatus.Hide);
    }

    private void SetMainButton() 
    {
        if (currQuestionIndex == firstUnansweredIndex)
        currentUIBoard.transform.Find("MainButton")
            .GetComponent<MCUIMainButtonController>()
            .SetCurrentStatus(MCUIMainButtonController.MainBottomStatus.Confirm);
        else
        currentUIBoard.transform.Find("MainButton")
            .GetComponent<MCUIMainButtonController>()
            .SetCurrentStatus(MCUIMainButtonController.MainBottomStatus.Modify);
    }

}
