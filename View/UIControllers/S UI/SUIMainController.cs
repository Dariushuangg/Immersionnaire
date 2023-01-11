using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SUIMainController : MonoBehaviour, UIMainController
{
    public UnityEvent ForwardButtonSelected { get; set; }
    public UnityEvent BackwardButtonSelected { get; set; }

    public void InitControllers(Question question)
    {
        SQuestion squestion = (SQuestion) question;
        int numOfQuestion = squestion.GetNumOfQuestion();
        int index = squestion.index;

        /* Initialize controllers */
        // Initialize slider controller
        for (int i = 0; i < numOfQuestion; i++)
        {
            int idx = i + 1;
            gameObject.transform
                .Find("SliderBar" + idx)
                .Find("Slider" + idx)
                .gameObject
                .GetComponent<SUISliderController>().InitSUISlider();
        }

        // Initialize subprompt controllers
        gameObject.transform.Find("Subprompts").GetComponent<SUISubpromptsController>().InitSubprompts(squestion.subprompts);

        // Initialize main text
        gameObject.transform.Find("MainTitleTextRef").GetComponent<UIMainTextController>().InitMainText(index);
    }

    public void ShowResponseHistory(Response response)
    {

    }
}
