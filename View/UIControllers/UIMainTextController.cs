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

    /// <summary>
    /// Set the main text to the string passed in.
    /// </summary>
    public void setMainText(string text)
    {
        MainText.text = text;
    }

    /* TODO: Support resizing of text */
} 
