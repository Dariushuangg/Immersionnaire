using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SContentGenerator : MonoBehaviour, ContentGenerator
{
    public GameObject SContentBoard;

    /*
     * Generate a linear scale question's choice, consists of a subprompt and the scale bar.
     */
    public GameObject GenerateContents(Question question) {
        SContentBoard = Resources.Load<GameObject>("Prefabs/ContentBoard/SQContentBoard");
        GameObject generatedContentBoard = Instantiate(SContentBoard, Vector3.zero, Quaternion.identity);

        /* Initialize content controllers */
        // 1. Main Text
        generatedContentBoard.transform
            .Find("MainTitleTextRef")
            .Find("MainTextCanvas")
            .Find("MainText")
            .gameObject
            .GetComponent<TMPro.TextMeshProUGUI>().text = "Question " + question.index;

        // 2. Prompt text
        generatedContentBoard.transform
            .Find("QuestionTextRef")
            .Find("QuestionTextCanvas")
            .Find("QuestionText")
            .gameObject
            .GetComponent<TMPro.TextMeshProUGUI>().text = question.prompt;

        // 3. Question text
        // I am stealing from subprompt controller for UI Board :)
        string[] subpromptTexts = ((SQuestion)question).subprompts;
        if (subpromptTexts == null) throw new Exception("No subpromptTexts field found: subpromptTexts");
        generatedContentBoard.transform.Find("Subprompts")
            .GetComponent<SUISubpromptsController>().InitSubprompts(subpromptTexts);

        return generatedContentBoard;
    }
}
