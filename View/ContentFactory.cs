using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ContentFactory : MonoBehaviour
{
    /// <summary>
    /// Generate and fill the content board for the given question.
    /// </summary>
    public GameObject GenerateContentBoardFor(Question question)
    {
        // Generate raw content board
        Type contentGeneratorClassName = Type.GetType(question.questionType + "ContentGenerator");
        MethodInfo GenerateContents = contentGeneratorClassName.GetMethod("GenerateContents");
        if (GenerateContents == null) throw new Exception("No such method: GenerateContents");
        GameObject GeneratedContents = (GameObject)GenerateContents.Invoke(Activator.CreateInstance(contentGeneratorClassName), new object[] { question });

        // Set position of content board
        // for testing only, ImmersionnaireScripts is set to the desired location/rotation
        GeneratedContents.transform.position = gameObject.transform.position; 
        GeneratedContents.transform.rotation = gameObject.transform.rotation;

        return GeneratedContents;
    }
}
