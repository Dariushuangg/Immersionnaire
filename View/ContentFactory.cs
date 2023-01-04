using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ContentFactory : MonoBehaviour
{
    public GameObject ContentBoard;
    public GameObject mandatory;
    public GameObject prompt;
    public Transform testTransform;
    // Every time the factor produces, it stores information about the objects
    // it created in indexedGeneratedChoices.
    public Dictionary<System.Object, UnityEngine.Object> indexedGeneratedChoices;

    public void generateAndfillContentBoard(Question question)
    {
        /* Generate content board background */
        // GameObject contentBoardBackground = Instantiate(ContentBoard, Vector3.zero, Quaternion.identity);

        /* Generate choice boxes */
        Type contentClassName = Type.GetType(question.questionType + "Content");
        MethodInfo generateChoices = contentClassName.GetMethod("generateChoices");
        if (generateChoices == null) throw new Exception("No such method: generateChoices");
        List<GameObject> generatedChoiceBoxes = (List<GameObject>) generateChoices.Invoke(Activator.CreateInstance(contentClassName), new object[] { question });
        indexedGeneratedChoices = (Dictionary<System.Object, UnityEngine.Object>) contentClassName
            .GetMethod("getIndexedGenerateChoices")
            .Invoke(Activator.CreateInstance(contentClassName), new object[] { });
        if (generatedChoiceBoxes == null) throw new Exception("Invoke() returns null: generatedChoices");

        /* Generate prompts box */
        GameObject promptBox = Instantiate(prompt, Vector3.zero, Quaternion.identity);

        /* Generate mandatory-or-not box */
        GameObject mandatoryBox = Instantiate(mandatory, Vector3.zero, Quaternion.identity);

        /* Arrange these generated contents */
        // contentBoardBackground.transform.position = testTransform.position;
        promptBox.transform.position = testTransform.position + new Vector3(0, 0.3f, 0);
        mandatoryBox.transform.position = testTransform.position + new Vector3(1, 0.3f, 0);
        for (int i = 0; i < generatedChoiceBoxes.Count; i ++)
        {
            generatedChoiceBoxes[i].transform.position = testTransform.position
                - new Vector3(0, 0.2f * i, 0) 
                + new Vector3(0, 0.3f, 0);
        }
    }
}
