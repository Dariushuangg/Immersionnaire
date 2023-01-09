using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface UIMainController
{   
    public UnityEvent ForwardButtonSelected { get; set; }
    public UnityEvent BackwardButtonSelected { get; set; }

    /// <summary>
    /// Render UIBoard and ContentBoard based upon previous response.
    /// </summary>
    /// <param name="response">Previously saved response.</param>
    public void ShowResponseHistory(Response response);

    /// <summary>
    /// Initialize main and sub-controllers based upon question contents.
    /// </summary>
    /// <param name="question">Question to-be-rendered.</param>
    public void InitControllers(Question question);
}
