using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Manages content changes on UI events. All ContentController implements two methods: 
 *  - showContentEffectOn()
 *  - hideContentEffectOn()
 */
public class MCContentController : MonoBehaviour
{
    private Dictionary<string, GameObject> MCChoiceEffects = new Dictionary<string, GameObject>();

    void Start()
    {
        /* Initialize the list of cubes that would change on UI event */
        Dictionary<System.Object, UnityEngine.Object> indexedGeneratedContent = GetComponent<ContentFactory>().indexedGeneratedChoices;
        
        foreach (string letter in indexedGeneratedContent.Keys)
        {
            MCChoiceEffects.Add(letter, ((GameObject)indexedGeneratedContent[letter]).transform.Find("SelectCube").gameObject);
        }
    }

    public void showContentEffectOn(string choice) 
    { 
    
    }

    public void hideContentEffectOn(string choice)
    {

    }
}
