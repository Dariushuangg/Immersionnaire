using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestionnireModel : MonoBehaviour
{
    private List<Question> questions;
    private List<Response> responses;
    private string sessionIdentifier;

    /* Get the data of a specific question for the caller to display.*/
    public Question getQuestionAt(int index) { return questions[index]; }

    /* Get the number of questions in the questionnaire */
    public int getNumOfQuestions() { return questions.Count; }

    /* Add response to the end of the list of responses. Called when completing a new question. */
    public void addResponse(Response response) { 
        if (responses.Count >= questions.Count) { throw new Exception("No new responses allowed: response"); }
        else { responses.Add(response); }
    }

    /* Change previous response at the given index. */
    public void changeResponseAt(Response newResponse, int index) { 
        if (index > questions.Count - 1) { throw new Exception("Index too large: index"); }
        else { responses[index] = newResponse; }
    }

    /* Initialize questionnaire model contents. Called if the user provide session ID */
    public void initQuestionnaireModel(string customSessionID) {
        sessionIdentifier = customSessionID;
        initializeQuestions();
    }

    /* Initialize questionnaire model contents. Called if the user does not provide session ID */
    public void initQuestionnaireModel()
    {
        sessionIdentifier = initializeUserID();
        initializeQuestions();
    }

    /* Serialize and save the completed questionnaire model contents. */
    public void saveQuestionnaire() { }

    private void initializeQuestions() {
        try { questions = GetComponent<ParseQuestions>().parseQuestions(); }
        catch (Exception e) { /* Display error message to user */ }
    }

    private string initializeUserID() { 
        return DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + UnityEngine.Random.Range(0f, 100.0f);
    }
}
