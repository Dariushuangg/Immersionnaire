using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionnairePresenter : MonoBehaviour
{
    private int currQuestionIndex;
    private int firstUnansweredIndex;
    private QuestionnireModel model;
    private ContentFactory contentFactory;
    private GameObject currentContentBoard;
    private GameObject currentUIBoard;

    void Start()
    {
        model = GetComponent<QuestionnireModel>();
        model.initQuestionnaireModel();
        contentFactory = GetComponent<ContentFactory>();

        currQuestionIndex = 0;
        firstUnansweredIndex = 0;

        ShowNextQuestion();
    }

    /*
     * Display the next question.
     * Listen to user selecting the "next" button.
     */
    public void ShowNextQuestion() {
        currQuestionIndex++;
        Question nextQuestion = model.getQuestionAt(currQuestionIndex);
        // Display question on ContentBoard
        currentContentBoard = contentFactory.GenerateContentBoardFor(nextQuestion);

        if (currQuestionIndex == firstUnansweredIndex)
        {
            // Display "Confirm" button but not "modify" button
        } else
        {
            // Display "modify" button but not "Confirm" button
        }
        if (currQuestionIndex == model.getNumOfQuestions())
        {
            // Allow "next" button
        } else
        {
            // Disallow "next" button
        }
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
        if (currQuestionIndex < firstUnansweredIndex)
        {
            // Confirm submission of new response, and display the next question automatically.
            model.addResponse(response);
            firstUnansweredIndex++;
            ShowNextQuestion();
        }
        else 
        {
            // Confirm modification of previous response, and display the next question automatically.
            model.changeResponseAt(response, currQuestionIndex);
            ShowNextQuestion();
        }
    }
}
