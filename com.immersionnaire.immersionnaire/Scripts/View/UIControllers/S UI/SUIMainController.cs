using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SUIMainController : MonoBehaviour, UIMainController
{
    public UnityEvent MainButtomSelected { get; set; }
    public UnityEvent ForwardButtonSelected { get; set; }
    public UnityEvent BackwardButtonSelected { get; set; }

    public void InitControllers(Question question)
    {
        /* Initialize and register for forward and backward button events */
        GameObject presenter = GameObject.FindGameObjectWithTag("Immersionnaire-Presenter");
        ForwardButtonSelected = new UnityEvent();
        ForwardButtonSelected.AddListener(presenter.GetComponent<QuestionnairePresenter>().Forward);
        BackwardButtonSelected = new UnityEvent();
        BackwardButtonSelected.AddListener(presenter.GetComponent<QuestionnairePresenter>().Back);
        MainButtomSelected = new UnityEvent();
        MainButtomSelected.AddListener(ConfirmingSliderValues);

        /* Initialize controllers */
        SQuestion squestion = (SQuestion)question;
        int numOfQuestion = squestion.GetNumOfQuestion();
        int index = squestion.index;

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

    // Maybe force this to be a method in the interface
    private void ConfirmingSliderValues() 
    {
        Util.SetDebugLog("ConfirmingSliderValues called", "", true);
        SQResponse response = new SQResponse(true, 1);
        GameObject presenter = GameObject.FindGameObjectWithTag("Immersionnaire-Presenter");
        presenter.GetComponent<QuestionnairePresenter>().Submit(response);
    }
}
