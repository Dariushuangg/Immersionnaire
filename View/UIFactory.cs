using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFactory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Check the location of UIFactory if this is not working. 
        // These codes are for testing only
        GameObject UIBoard = GameObject.FindGameObjectWithTag("Immersionnaire-UIBoard");
        List<GameObject> MCs = Util.getAllChildGameObjects(UIBoard);
        for (int i = 0; i < MCs.Count; i ++)
        {
            MCs[i].transform
                .Find("Choices")
                .GetComponent<MCUIChoicesController>()
                .showChoiceLetter(Util.indexToLetter(i));
        }
        GetComponent<MCUIMainController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
