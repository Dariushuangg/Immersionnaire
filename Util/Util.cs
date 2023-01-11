using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    static GameObject[] debugTexts = GameObject.FindGameObjectsWithTag("DebugText");

    public static string indexToLetter(int index)
    {
        if (index == 0) return "A";
        else if (index == 1) return "B";
        else if (index == 2) return "C";
        else if (index == 3) return "D";
        else throw new Exception("No such index:" + index);
    }

    /*
     * Return a list of unordered child object of the given parent GameObject. 
     * Raise exception if no child object is found.
     */
    public static List<GameObject> GetAllChildGameObjects(GameObject parent) {
        Transform parentTransform = parent.transform;
        List<GameObject> childGameObjects = new List<GameObject>();
        foreach (Transform child in parentTransform)
        {
            childGameObjects.Add(child.gameObject);
        }
        if (childGameObjects.Count == 0) throw new Exception("No child game objects found: " + childGameObjects.ToString());
        return childGameObjects;
    }

    public static GameObject FindChildGameObjectByName(GameObject parent, string childObjectName)
    {
        Transform tagetTransform = parent.transform.Find(childObjectName);
        if (tagetTransform == null) throw new Exception("No such child object " + childObjectName);
        return tagetTransform.gameObject;
    }

    /* Find the peer game object of the given gameobject that has the given name. */
    public static GameObject FindPeerGameObjectByName(GameObject peer, string targetPeerObjectName)
    {
        Transform targetPeerTransform = peer.transform.parent.transform.Find(targetPeerObjectName);
        if (targetPeerTransform == null) SetDebugLog("FindPeerGameObjectByName Null Error", targetPeerObjectName, true);
        return targetPeerTransform.gameObject;
    }

    public static void checkNull(Transform t) 
    { 
        if (t == null)
        {
            SetDebugLog("Transform is null: ", t.gameObject.name, true);
            Debug.Log("Transform is null");
            throw new Exception("Transform is null");
        }
    }

    public static void checkNull(GameObject go)
    {
        if (go == null)
        {
            SetDebugLog("GameObject is null: ", go.name, true);
            Debug.Log("GameObject is null");
            throw new Exception("GameObject is null");
        }
    }

    public static void showDebugText(int index, string text)
    {
        debugTexts[index].GetComponent<TMPro.TextMeshProUGUI>().text = "" + text;
    }

    /// <summary>
    /// Add a line to debug log and render debug text in VR.
    /// </summary>
    /// <param name="debugKey">Unique identifier to the debug line; If the identifier already exists,
    /// update the debug line. </param>
    /// <param name="debugText">Debug content.</param>
    /// <param name="add">Add (true) or remove (false) this identifier.</param>
    public static void SetDebugLog(string identifier, string debugText, bool add) {
        if (identifier == null || debugText == null)
        {
            identifier = "DebugLog is Null";
            debugText = "identifier or debugText is null! Check input to SetDebugLog()";
        }
        GameObject.FindGameObjectWithTag("DebugParent").GetComponent<DebugDisplay>().SetDebugLog(identifier, debugText, add);
    }
}
