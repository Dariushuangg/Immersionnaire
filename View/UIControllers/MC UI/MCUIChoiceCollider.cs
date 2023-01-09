using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MCUIChoiceCollider : MonoBehaviour
{
    public string letter;
    private bool selectable; // Can't select if user has unselected but hasn't exit the collision.
    private bool unselectable; // Can't unselect if user has selected but hasn't exit the collision.
    private bool unselectingSucceeded; // Temporary state valid throughout one unselection attempt; Default to false.
    private UnityEvent unSelecting; // Signal user is making an attempt to unselect a choice (the attempt might be unsuccessful)
    private UnityEvent selecting;
    private UnityEvent exitedWhileSelecting;
    private UnityEvent exitedWhileUnselecting;
    private GameObject progressBar;

    void OnEnable()
    {
        letter = gameObject.transform.parent.name;
        progressBar = Util.FindPeerGameObjectByName(gameObject, "ProgressBar" + letter);
        selecting = new UnityEvent();
        selecting.AddListener(progressBar.GetComponent<MCUIProgressBarController>().increaseProgressBarValue);
        unSelecting = new UnityEvent();
        unSelecting.AddListener(progressBar.GetComponent<MCUIProgressBarController>().decreaseProgressBarValue);
        exitedWhileSelecting = new UnityEvent();
        exitedWhileSelecting.AddListener(progressBar.GetComponent<MCUIProgressBarController>().zeroProgressBarValue);
        exitedWhileUnselecting = new UnityEvent();
        exitedWhileUnselecting.AddListener(progressBar.GetComponent<MCUIProgressBarController>().fullProgressBarValue);
        selectable = true;
        unselectable = true;
        unselectingSucceeded = false;
    }

    /// <summary>
    /// Signal that unselection was successful during last UI interaction.
    /// </summary>
    public void setUnselectingSucceeded(string pletter)
    {
        if (this.letter == pletter) unselectingSucceeded = true;
    }

    /// <summary>
    /// Set the current choice to can not be selected.
    /// </summary>
    public void setSelectable(string letter)
    {
        if (this.letter == letter) selectable = false;
    }

    /// <summary>
    /// Set the current choice to can not be unselected.
    /// </summary>
    public void setUnselectable(string letter)
    {
        if (this.letter == letter) unselectable = false;
    }

    private bool letterIsSelected()
    {
        return gameObject.transform.parent.parent.parent.gameObject
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
