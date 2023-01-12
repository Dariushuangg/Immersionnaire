using UnityEngine.Events;

public interface UIMainController
{   
    public UnityEvent MainButtomSelected { get; set; }
    public UnityEvent ForwardButtonSelected { get; set; }
    public UnityEvent BackwardButtonSelected { get; set; }

    /// <summary>
    /// Render UIBoard and ContentBoard based upon previous response.
    /// </summary>
    /// <param name="response">Previously saved response.</param>
    public void ShowResponseHistory(Response response);

    /// <summary>
    /// Initialize main controller, which initialize all sub-controllers,
    /// based upon the given question contents.
    /// </summary>
    /// <param name="question">Question to-be-rendered.</param>
    public void InitControllers(Question question);
}
