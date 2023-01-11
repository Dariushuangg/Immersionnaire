using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainTextController : MonoBehaviour
{
    TMPro.TextMeshProUGUI MainText;

    void Start()
    {
        GameObject canvas = gameObject.transform.Find("MainTextCanvas").gameObject;
        MainText = canvas.transform.Find("MainText").GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void InitMainText(int questionIndex)
    {
        string mainText = "Question " + questionIndex;
        SetMainText(mainText);
    }

    /// <summary>
    /// Set the main text to the string passed in.
    /// </summary>
    public void SetMainText(string text)
    {
        MainText.text = text;
    }

    /* TODO: Support resizing of text */
} 
