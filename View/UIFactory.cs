using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFactory : MonoBehaviour
{
    public GameObject GenerateUIBoard(Question question, Transform tf)
    {
        string UIBoardType = question.questionType.ToString();
        string numOfQuestions = question.GetNumOfQuestion().ToString();
        GameObject UIBoardResource = Resources.Load<GameObject>("Prefabs/UIBoard/" + UIBoardType + numOfQuestions);
        GameObject generatedUIBoard = Instantiate(UIBoardResource, Vector3.zero, Quaternion.identity);
        // Util.SetDebugLog("UIFactory check, loading", "Prefabs/UIBoard/" + UIBoardType + numOfQuestions, true);

        // Set text

        // Set transform of UI board
        generatedUIBoard.transform.position = tf.position;
        generatedUIBoard.transform.rotation = tf.rotation;
        generatedUIBoard.transform.localScale = tf.localScale;

        // Enable main controller of the UI Board.
        generatedUIBoard.GetComponent<MCUIMainController>().enabled = true;
        return generatedUIBoard;
    }
}
