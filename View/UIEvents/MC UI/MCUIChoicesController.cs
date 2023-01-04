using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCUIChoicesController : MonoBehaviour
{
    public string letter;
    /* Display the given choice letter on the base board. */
    public void showChoiceLetter(string letter)
    {
        gameObject.transform.Find(letter).gameObject.SetActive(true);
        this.letter = letter;
    }
}
