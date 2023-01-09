using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCContentGenerator : MonoBehaviour, ContentGenerator
{
    public GameObject MCContentBoard;
    public string letter;
    /*
     * Generate a multiple choice question's choice, consists of the text and the choice (A~D).
     */
    public GameObject GenerateContents(Question question)
    {
        /* Generated MC ContentBoard. */
        MCQuestion mcquestion = (MCQuestion)question;
        MCContentBoard = Resources.Load<GameObject>("Prefabs/ContentBoard/MCQContentBoard");
        string[] choiceTexts = mcquestion.choiceText;
        GameObject generatedContentBoard = Instantiate(MCContentBoard, Vector3.zero, Quaternion.identity);

        /* Generate text for MC ContentBoard. */
        // 1. Question texts
        for (int i = 0; i < mcquestion.numOfChoices; i ++)
        {
            string letter = Util.indexToLetter(i);
            generatedContentBoard
                .transform.Find("ChoiceTextRef" + letter)
                .Find("ChoiceTextCanvas" + letter)
                .Find("ChoiceText" + letter)
                .gameObject
                .GetComponent<TMPro.TextMeshProUGUI>().text = Util.indexToLetter(i) + ". " + choiceTexts[i];
        }

        // 2. Main texts
        generatedContentBoard.transform
            .Find("MainTitleTextRef")
            .Find("MainTextCanvas")
            .Find("MainText")
            .gameObject
            .GetComponent<TMPro.TextMeshProUGUI>().text = "Question" + "1";

        // 3. Prompt text
        generatedContentBoard.transform
            .Find("QuestionTextRef")
            .Find("QuestionTextCanvas")
            .Find("QuestionText")
            .gameObject
            .GetComponent<TMPro.TextMeshProUGUI>().text = question.prompt;

        return generatedContentBoard;
    }
}
