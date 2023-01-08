using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFactory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Check the location of UIFactory if this is not working. 
        
        // 1. Generate UI and Content

        // 2. Initialize Main Controller
        GetComponent<MCUIMainController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
