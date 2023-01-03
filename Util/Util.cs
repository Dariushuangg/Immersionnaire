using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{  
    public static string indexToLetter(int index)
    {
        if (index == 0) return "A";
        else if (index == 1) return "B";
        else if (index == 2) return "C";
        else if (index == 3) return "D";
        else throw new Exception("No such index: index");
    }
    /*
     * Return a list of unordered child object of the given parent GameObject. 
     */
    public static List<GameObject> getAllChildGameObjects(GameObject parent) {
        Transform parentTransform = parent.transform;
        List<GameObject> childGameObjects = new List<GameObject>();
        foreach (Transform child in parentTransform)
        {
            childGameObjects.Add(child.gameObject);
        }
        return childGameObjects;
    }
}
