using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* Installed on the collider component in the MutipleChoice GameObject*/
public class MCUIColliderController : MonoBehaviour
{
    private bool selectable; // Can't select if user has unselected but hasn't exit the collision.
    private bool unselectable; // Can't unselect if user has selected but hasn't exit the collision.
    private bool unselectingSucceeded; // Temporary state valid throughout one exiting event; Default to false.
    private UnityEvent unSelecting;
    private UnityEvent selecting;
    private UnityEvent exitedWhileSelecting;
    private UnityEvent exitedWhileUnselecting;
    private GameObject progressBar;
    private string letter;


    GameObject[] debugTexts;
    void Start()
    {
        progressBar = Util.FindPeerGameObjectByName(gameObject, "ProgressBar");
        selecting = new UnityEvent();
        selecting.AddListener(progressBar.GetComponent<MCUIProgressBarController>().increaseProgressBarValue);
        unSelecting = new UnityEvent();
        unSelecting.AddListener(progressBar.GetComponent<MCUIProgressBarController>().decreaseProgressBarValue);
        exitedWhileSelecting = new UnityEvent();
        exitedWhileSelecting.AddListener(progressBar.GetComponent<MCUIProgressBarController>().zeroProgressBarValue);
        exitedWhileUnselecting = new UnityEvent();
        exitedWhileUnselecting.AddListener(progressBar.GetComponent<MCUIProgressBarController>().fullProgressBarValue);
        letter = Util.FindPeerGameObjectByName(gameObject, "Choices").GetComponent<MCUIChoicesController>().letter;
        selectable = true;
        unselectable = true;
        unselectingSucceeded = false;
    }

    public void setUnselectingSucceeded(string pletter) 
    {
        // debugTexts[1].GetComponent<TMPro.TextMeshProUGUI>().text = "setUnselectingSucceeded called with letter: " + letter;
        Debug.Log("setUnselectingSucceeded called with letter: " + letter);
        if (this.letter == pletter) unselectingSucceeded = true;
    }
    public void setSelectable(string letter) 
    {
        // debugTexts[2].GetComponent<TMPro.TextMeshProUGUI>().text = "setUnselectingSucceeded called with letter: " + letter;
        Debug.Log("setSelectable called with letter: " + letter);
        if (this.letter == letter) selectable = false;
    }
    public void setUnselectable(string letter) 
    {
        if (this.letter == letter) unselectable = false;
    }

    private bool letterIsSelected() {
        Debug.Log("Key not in dictoionary? Letter:" + letter);
        return gameObject.transform.parent.parent.gameObject
            .GetComponent<MCUIMainController>().IsLetterSelected[letter];
     }

    void OnTriggerStay(Collider other)
    {
        if (!letterIsSelected())
        {
            if (selectable) selecting.Invoke();
        }
        else 
        {
            if (unselectable) unSelecting.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!letterIsSelected())
        {
            exitedWhileSelecting.Invoke();
            selectable = true;
        }
        else 
        {
            if (!unselectingSucceeded)
            {
                exitedWhileUnselecting.Invoke();
            }
            unselectingSucceeded = false;
            unselectable = true;
        }
    }
}
