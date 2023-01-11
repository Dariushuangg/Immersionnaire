using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUISubpromptsController : MonoBehaviour
{
    private Dictionary<int, TMPro.TextMeshProUGUI> SubpromptTexts;

    public void InitSubprompts(string[] initialSubprompts)
    {
        SubpromptTexts = new Dictionary<int, TMPro.TextMeshProUGUI>();
        List<GameObject> subprompts = Util.GetAllChildGameObjects(gameObject);
        for (int i = 0; i < subprompts.Count; i ++)
        {
            int idx = i + 1;
            GameObject subpromptTextGO = subprompts[i].transform
                .Find("SubpromptCanvas"+ idx)
                .Find("SubpromptText"+ idx)
                .gameObject;
            SubpromptTexts.Add(idx, subpromptTextGO.GetComponent<TMPro.TextMeshProUGUI>());
        }

        for (int i = 0; i < initialSubprompts.Length; i ++ )
        {
            SetSubpromptText(i + 1, initialSubprompts[i]);
        }
    }

    /// <summary>
    /// Set the subprompt text of the given question to the given string
    /// </summary>
    public void SetSubpromptText(int idx, string text)
    {
        if (!SubpromptTexts.ContainsKey(idx)) throw new System.Exception("No such question: " + idx);
        SubpromptTexts[idx].text = text;
    }
}
