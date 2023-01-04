using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MCUIMainController : MonoBehaviour
{
    public Dictionary<string, bool> isLetterSelected { get; } = new Dictionary<string, bool>();
    private int numOfQuestions;
    private bool allowMultipleChoices;
    private Dictionary<string, GameObject> selectedFrames;

    void OnEnable()
    {
        // initializeMainMCUI() must be called after UIFactory has generated the UI content.
        initializeMainMCUI(2, false);
    }

    public void initializeMainMCUI(int numOfQuestions, bool allowMultipleChoices)
    {
        this.numOfQuestions = numOfQuestions;
        this.allowMultipleChoices = allowMultipleChoices;
        selectedFrames = new Dictionary<string, GameObject>();

        for (int i = 0; i < numOfQuestions; i ++)
        {
            isLetterSelected.Add(Util.indexToLetter(i), false);
        }
        
        List<GameObject> choices = Util.getAllChildGameObjects(gameObject);
        if (choices == null) throw new Exception("choices is null: choices");
        foreach(GameObject choiceParent in choices)
        {
            GameObject selectedFrame = choiceParent.transform.Find("SelectedFrame").gameObject;
            string letter = choiceParent.transform.Find("Choices").GetComponent<MCUIChoicesController>().letter;
            selectedFrames.Add(letter, selectedFrame);
        }
    }

    public void unselectingChoice(string letter)
    {
        selectedFrames[letter].SetActive(false);
        isLetterSelected[letter] = false;
    }

    public void selectingChoice(string letter)
    {
        selectedFrames[letter].SetActive(true);
        isLetterSelected[letter] = true;

        if (!allowMultipleChoices)
        {
            for (int i = 0; i < numOfQuestions; i ++)
            {
                if (Util.indexToLetter(i) != letter)
                {
                    selectedFrames[Util.indexToLetter(i)].SetActive(false);
                    isLetterSelected[Util.indexToLetter(i)] = false;
                    Util.findPeerGameObjectByName(selectedFrames[Util.indexToLetter(i)], "ProgressBar")
                        .GetComponent<MCUIProgressBarController>().zeroProgressBarValue();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
