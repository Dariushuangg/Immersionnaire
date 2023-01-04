using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* Installed on the collider component in the MutipleChoice GameObject*/
public class MCUIColliderController : MonoBehaviour
{
    private bool selectable; // Can't select if user has unselected but hasn't exit the collision.
    private bool unselectable; // Can't unselect if user has selected but hasn't exit the collision.
    private UnityEvent unSelecting;
    private UnityEvent selecting;
    private UnityEvent exitedWhileSelecting;
    private UnityEvent exitedWhileUnselecting;
    private GameObject progressBar;
    private string letter;

    void Start()
    {
        progressBar = Util.findPeerGameObjectByName(gameObject, "ProgressBar");
        selecting = new UnityEvent();
        selecting.AddListener(progressBar.GetComponent<MCUIProgressBarController>().increaseProgressBarValue);
        unSelecting = new UnityEvent();
        unSelecting.AddListener(progressBar.GetComponent<MCUIProgressBarController>().decreaseProgressBarValue);
        exitedWhileSelecting = new UnityEvent();
        exitedWhileSelecting.AddListener(progressBar.GetComponent<MCUIProgressBarController>().zeroProgressBarValue);
        exitedWhileUnselecting = new UnityEvent();
        exitedWhileUnselecting.AddListener(progressBar.GetComponent<MCUIProgressBarController>().fullProgressBarValue);
        letter = Util.findPeerGameObjectByName(gameObject, "Choices").GetComponent<MCUIChoicesController>().letter;
        selectable = true;
        unselectable = true;
    }

    public void setSelectable(string pletter) 
    {
        Debug.Log("setSelectable called with param:" + pletter + ", my letter is:" + letter);
        if (this.letter == pletter) selectable = false;
    }
    public void setUnselectable(string pletter) 
    {
        Debug.Log("setUnselectable called with param:" + pletter + ", my letter is:" + letter);
        if (this.letter == pletter) unselectable = false;
    }

    private bool letterIsSelected() {
        return gameObject.transform.parent.parent.gameObject
            .GetComponent<MCUIMainController>().isLetterSelected[letter];
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
            exitedWhileUnselecting.Invoke();
            unselectable = true;
        }
    }
}
