using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionnairePresenter : MonoBehaviour
{
    private int currQuestionIndex;
    private int firstUnansweredIndex;
    private QuestionnireModel model;
    private QuestionnaireView view;


    void Start()
    {
        model = GetComponent<QuestionnireModel>();
        model.initQuestionnaireModel();
        view = GetComponent<QuestionnaireView>();

        currQuestionIndex = 0;
        firstUnansweredIndex = 0;

        showNextQuestion();
    }

    /*
     * Display the next question.
     * Listen to user selecting the "next" button.
     */
    public void showNextQuestion() {
        currQuestionIndex++;
        Question nextQuestion = model.getQuestionAt(currQuestionIndex);
        // Display question
        view.showContentBoardOf(nextQuestion);

        if (currQuestionIndex == firstUnansweredIndex)
        {
            // Display "confirm" button but not "modify" button
        } else
        {
            // Display "modify" button but not "confirm" button
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
    public void showPrevQuestion()
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

    /*
    * Confirm submission of response to the question, and display the next question automatically.
    * Listen to user selecting the "confirm" button.
    */
    public void confirm(Response response) {
        model.addResponse(response);
        firstUnansweredIndex++;
        showNextQuestion();
    }

    /*
    * Confirm modification of response to a question, and display the next question automatically.
    * Listen to user selecting the "modify" button.
    */
    public void modify(Response response, int index)
    {
        model.changeResponseAt(response, index);
        showNextQuestion();
    }
}
