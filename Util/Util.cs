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
    public static List<GameObject> getAllChildGameObjects(GameObject parent) {
        Transform parentTransform = parent.transform;
        List<GameObject> childGameObjects = new List<GameObject>();
        foreach (Transform child in parentTransform)
        {
            childGameObjects.Add(child.gameObject);
        }
        if (childGameObjects.Count == 0) throw new Exception("No child game objects found: " + childGameObjects.ToString());
        return childGameObjects;
    }

    /* Find the peer game object of the given gameobject that has the given name. */
    public static GameObject findPeerGameObjectByName(GameObject peer, string targetPeerObjectName)
    {
        GameObject targetPeer = peer.transform.parent.transform.Find(targetPeerObjectName).gameObject;
        if (targetPeer == null) throw new Exception("No such target GameObject found:" + targetPeerObjectName);
        return targetPeer;
    }

    public static void showDebugText(int index, string text)
    {
        debugTexts[index].GetComponent<TMPro.TextMeshProUGUI>().text = "" + text;
    }
}
