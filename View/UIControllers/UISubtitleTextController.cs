using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISubtitleTextController : MonoBehaviour
{
    TMPro.TextMeshProUGUI SubtitleText;

    void Start()
    {
        GameObject canvas = gameObject.transform.Find("OptionalQuestionCanvas").gameObject;
        SubtitleText = canvas.transform.Find("OptionalQuestionText").GetComponent<TMPro.TextMeshProUGUI>();
    }

    /// <summary>
    /// Set the subtitle text to the string passed in.
    /// </summary>
    public void setSubtitleText(string text)
    {
        SubtitleText.text = text;
    }

    /* TODO: Support resizing of text */
}
