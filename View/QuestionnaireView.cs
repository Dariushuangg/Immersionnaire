using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionnaireView : MonoBehaviour
{
    public void showContentBoardOf(Question question)
    {
        ContentFactory factory = GetComponent<ContentFactory>();
        factory.generateAndfillContentBoard(question);
    }
}
