using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Manages content changes on UI events. All ContentController implement two methods: 
 *  - showContentEffectOn()
 *  - hideContentEffectOn()
 */
public class MCContentController : MonoBehaviour
{
    private Dictionary<string, GameObject> MCChoiceEffects = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> MCChoiceTexts = new Dictionary<string, GameObject>();
    private static readonly Color offColor = Color.white;
    private static readonly Color onColor = Color.green;

    void Start()
    {
        /* Initialize the list of cubes that would change on UI event */
        // Dictionary<string, GameObject> indexedGeneratedContent = GetComponent<ContentFactory>().indexedGeneratedChoices;
        
        if (MCChoiceEffects.Count == 0) throw new Exception("Initialization failed: MCChoiceEffects");
    }

    // Turn on the selection frame
    public void showContentEffectOn(string choice) 
    { 
        foreach(string letter in MCChoiceEffects.Keys)
        {
            if (letter == choice) MCChoiceEffects[letter].GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }

    // Turn off the selection frame
    public void hideContentEffectOn(string choice)
    {
        foreach (string letter in MCChoiceEffects.Keys)
        {
            if (letter == choice) MCChoiceEffects[letter].GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}
