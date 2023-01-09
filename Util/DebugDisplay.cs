using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DebugDisplay : MonoBehaviour
{
    Dictionary<string, string> debugLogs = new Dictionary<string, string>();
    Dictionary<string, GameObject> debugLogObject = new Dictionary<string, GameObject>();
    public GameObject TextBox;
    public Transform TextBoxTransform;

    private void RenderDebugText() {
        DestroyOldDebugText();
        List<string> myKeys = debugLogs.Keys.ToList();
        for (int i = 0; i < debugLogs.Count; i++)
        {
            GameObject generatedText = Instantiate(TextBox, Vector3.zero, Quaternion.identity);
            generatedText.transform.SetParent(TextBoxTransform);
            generatedText.transform.localPosition = new Vector3(0, 10 * i, 0);
            generatedText.transform.localScale = new Vector3(1, 1, 1);
            generatedText.transform.localRotation = Quaternion.identity;
            generatedText.GetComponent<TMPro.TextMeshProUGUI>().text = myKeys[i] + " : " + debugLogs[myKeys[i]];
            debugLogObject.Add(myKeys[i], generatedText);
        }
    }

    private void DestroyOldDebugText() {
        foreach (string key in debugLogObject.Keys) {
            Destroy(debugLogObject[key]);
        }
        debugLogObject = new Dictionary<string, GameObject>();
    }

    public void SetDebugLog(string debugKey, string debugText, bool add) {
        if (add)
        {
            if (debugLogs.ContainsKey(debugKey))
            {
                debugLogs[debugKey] = debugText;
            }
            else
            {
                debugLogs.Add(debugKey, debugText);
            }
        } else
        {
            if (debugLogs.ContainsKey(debugKey))
            {
                debugLogs.Remove(debugKey);
            }
            else 
            {
                debugLogs.Add("Tried to remove non-existing key from debug logging: ", debugKey);
            }
        }
        RenderDebugText();
    }
}

