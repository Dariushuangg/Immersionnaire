using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SContent : MonoBehaviour
{
    public GameObject SChoices;

    /*
     * Generate a linear scale question's choice, consists of a subprompt and the scale bar.
     */
    public List<GameObject> generateChoices(Question question) {
        Debug.Log("generateChoices called for scale question");
        List<GameObject> generateChoices = new List<GameObject>();
        string[] choiceTexts = ((SQuestion)question).subprompts;
        if (choiceTexts == null) throw new Exception("No choiceText field found: choiceTexts");
        for (int i = 0; i < ((SQuestion)question).numOfSubprompts; i++)
        {
            GameObject generatedChoice = Instantiate(SChoices, Vector3.zero, Quaternion.identity);
            generatedChoice
                .transform.Find("MCChoiceCanvas")
                .transform.Find("MCChoiceText")
                .GetComponent<TMPro.TextMeshProUGUI>().text = Util.indexToLetter(i) + ". " + choiceTexts[i];
            generateChoices.Add(generatedChoice);
        }
        return generateChoices;
    }
}
