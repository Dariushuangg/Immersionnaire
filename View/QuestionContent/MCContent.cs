using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCContent : MonoBehaviour
{
    public GameObject MCChoice;

    /*
     * Generate a multiple choice question's choice, consists of the text and the choice (A~D).
     */
    public List<GameObject> generateChoices(Question question)
    {
        MCChoice = Resources.Load<GameObject>("Prefabs/ContentBoard/MCChoice");

        List<GameObject> generateChoices = new List<GameObject>();
        string[] choiceTexts = ((MCQuestion)question).choiceText;
        if (choiceTexts == null) throw new Exception("No choiceText field found: choiceTexts");
        for (int i = 0; i < ((MCQuestion)question).numOfChoices; i ++)
        {
            GameObject generatedChoice = Instantiate(MCChoice, Vector3.zero, Quaternion.identity);
            generatedChoice
                .transform.Find("MCChoiceCanvas")
                .transform.Find("MCChoiceText")
                .GetComponent<TMPro.TextMeshProUGUI>().text = Util.indexToLetter(i) + ". " + choiceTexts[i];
            generateChoices.Add(generatedChoice);
        }
        return generateChoices;
    }
}
