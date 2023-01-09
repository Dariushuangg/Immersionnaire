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

        /* Generate the first question on UIBoard and ContentBoard */
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

    /// <summary>
    /// Callback function for user clicking the Confirm button. 
    /// </summary>
    /// <param name="response">The response to the currently displayed question.</param>
    public void Submit(Response response) {
        if (currQuestionIndex == firstUnansweredIndex)
        {
            // Submission of new response, and display the next question automatically.
            model.addResponse(response);
            firstUnansweredIndex++;
        }
        else
        {
            // Submission of modification of previous response, and display the next question automatically.
            model.changeResponseAt(response, currQuestionIndex);
        }
        currQuestionIndex++;
        ShowQuestionAt(currQuestionIndex);
    }

    public void Back()
    {

    }

    /// <summary>
    /// Display question at the given index on both UI board and content board. 
    /// </summary>
    private void ShowQuestionAt(int index)
    {
        Question questionToDisplay = model.getQuestionAt(index);

        // Display question on ContentBoard at the previous ContentBoard's location
        Transform LastContentBoardTransform = currentContentBoard.transform;
        RemoveCurrentContentBoard();
        currentContentBoard = contentFactory.GenerateContentBoard(questionToDisplay, LastContentBoardTransform);

        // Display correct UIBoard at the previous UIBoard's location
        Transform PrevUIBoardTransform = currentUIBoard.transform;
        RemoveCurrentUIBoard();
        currentUIBoard = uiFactory.GenerateUIBoard(questionToDisplay, PrevUIBoardTransform);

        // Set buttons on the UIBoard
        SetBackForwardButtons();
        SetMainButtonType();
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
        prevUIBoard = currentUIBoard; // Same reason as above
        Destroy(prevUIBoard);
    }

    /// <summary>
    /// Wrapper around SetForwardButtonStatus() in UIBackForwardButtonController
    /// </summary>
    private void SetForwardButtonStatus(UIBackForwardButtonController.ForwardButtonStatus status) 
    {
        currentUIBoard.transform.Find("BackForwardButtons")
            .GetComponent<UIBackForwardButtonController>()
            .SetForwardButtonStatus(status);
    }

    /// <summary>
    /// Wrapper around SetBackwardButtonStatus() in UIBackForwardButtonController
    /// </summary>
    private void SetBackwardButtonStatus(UIBackForwardButtonController.BackwardButtonStatus status)
    {
        currentUIBoard.transform.Find("BackForwardButtons")
            .GetComponent<UIBackForwardButtonController>()
            .SetBackwardButtonStatus(status);
    }

    private void SetBackForwardButtons()
    {
        // Set forward button visibility
        if (currQuestionIndex == firstUnansweredIndex) SetForwardButtonStatus(UIBackForwardButtonController.ForwardButtonStatus.Hide);
        else SetForwardButtonStatus(UIBackForwardButtonController.ForwardButtonStatus.Show);

        // Set backward button visibility
        if (currQuestionIndex != 0) SetBackwardButtonStatus(UIBackForwardButtonController.BackwardButtonStatus.Show);
        else SetBackwardButtonStatus(UIBackForwardButtonController.BackwardButtonStatus.Hide);
    }

    private void SetMainButtonType() 
    {
        if (currQuestionIndex == firstUnansweredIndex)
        currentUIBoard.transform.Find("MainButton")
            .GetComponent<UIMainButtonController>()
            .SetButtonType(UIMainButtonController.MainButtonType.Confirm);
        else
        currentUIBoard.transform.Find("MainButton")
            .GetComponent<UIMainButtonController>()
            .SetButtonType(UIMainButtonController.MainButtonType.Modify);
    }

}
