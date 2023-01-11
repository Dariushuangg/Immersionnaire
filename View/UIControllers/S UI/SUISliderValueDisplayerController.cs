using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUISliderValueDisplayerController : MonoBehaviour
{
    private TMPro.TextMeshProUGUI DisplayValueText;
    private int SliderValue;

    void Start()
    {
        InitController();
    }

    public void InitController()
    {
        DisplayValueText = gameObject.transform
            .Find("SliderValueCanvas")
            .Find("SliderValueText")
            .GetComponent<TMPro.TextMeshProUGUI>();
        SliderValue = 1;
    }

    /// <summary>
    /// Display the given slider value on the UI board.
    /// </summary>
    public void SetSliderValueTo(int sliderIdentifier, int sliderValue)
    {
        // Don't actually need to check sliderIdentifier, since slider controller
        // will only send event to the child value displayer.
        this.SliderValue = sliderValue;
        DisplayValueText.text = this.SliderValue.ToString();
    }


}
