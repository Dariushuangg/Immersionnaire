using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCContent : MonoBehaviour
{
    public GameObject MCChoice;
    public Dictionary<System.Object, UnityEngine.Object> indexedGenerateChoices;
    public string letter;
    /*
     * Generate a multiple choice question's choice, consists of the text and the choice (A~D).
     */
    public List<GameObject> generateChoices(Question question)
    {
        MCChoice = Resources.Load<GameObject>("Prefabs/ContentBoard/MCChoice");
        indexedGenerateChoices = new Dictionary<System.Object, UnityEngine.Object>();
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
            indexedGenerateChoices.Add(Util.indexToLetter(i), generatedChoice);
        }
        return generateChoices;
    }

    public Dictionary<System.Object, UnityEngine.Object> getIndexedGenerateChoices() { return indexedGenerateChoices; }
}
