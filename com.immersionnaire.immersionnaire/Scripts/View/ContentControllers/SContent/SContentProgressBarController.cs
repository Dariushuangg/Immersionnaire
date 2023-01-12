using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SContentProgressBarController : MonoBehaviour
{
    private int ProgressBarID;
    private Dictionary<int, GameObject> ProgressBarValuesDict;

    void Start()
    {
        InitProgressBarController();
    }

    public void InitProgressBarController() 
    {
        ProgressBarID = int.Parse(gameObject.name.Last().ToString());
        GameObject ProgressBarValueParent = gameObject.transform.Find("ProgressBarValue").gameObject;
        List<GameObject> ProgressBarValues = Util.GetAllChildGameObjects(ProgressBarValueParent);
        ProgressBarValuesDict = new Dictionary<int, GameObject>();
        foreach (GameObject go in ProgressBarValues)
        {
            ProgressBarValuesDict.Add(int.Parse(go.name), go);
            if (int.Parse(go.name) == 1) ProgressBarValuesDict.Add(0, go); // ...bullshit warning here. Fix this later.
        }
        UpdateProgressValueTo(ProgressBarID, 4);
    }

    public void UpdateProgressValueTo(int identifer, int currentValue)
    {
        Dictionary<int, string> hintText = new Dictionary<int, string>();
        hintText.Add(0, "Not stressful");
        hintText.Add(1, "Not stressful");
        hintText.Add(2, "Not stressful");
        hintText.Add(3, "A bit stressful");
        hintText.Add(4, "A bit stressful");
        hintText.Add(5, "Stressful");
        hintText.Add(6, "Stressful");
        hintText.Add(7, "Very stressful");
        hintText.Add(8, "Very stressful");
        hintText.Add(9, "Extremely stressful");
        hintText.Add(10, "Extremely stressful");

        if (identifer != ProgressBarID) return;
        foreach(int value in ProgressBarValuesDict.Keys)
        {
            if (value <= currentValue) ProgressBarValuesDict[value].GetComponent<MeshRenderer>().enabled = true;
            else ProgressBarValuesDict[value].GetComponent<MeshRenderer>().enabled = false;
        }

        gameObject.transform
            .Find("ProgressBarHintCanvas")
            .Find("ProgressBarHintText").gameObject
            .GetComponent<TMPro.TextMeshProUGUI>().text = hintText[currentValue];
    }

    
}
