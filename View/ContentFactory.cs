using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ContentFactory : MonoBehaviour
{
    /// <summary>
    /// Generate and fill the content board for the given question at the given transform.
    /// </summary>
    public GameObject GenerateContentBoard(Question question, Transform tf)
    {
        // Generate raw content board
        Type contentGeneratorClassName = Type.GetType(question.questionType + "ContentGenerator");
        MethodInfo GenerateContents = contentGeneratorClassName.GetMethod("GenerateContents");
        if (GenerateContents == null) throw new Exception("No GenerateContents method in generator: " + contentGeneratorClassName.ToString());
        GameObject GeneratedContents = (GameObject)GenerateContents.Invoke(Activator.CreateInstance(contentGeneratorClassName), new object[] { question });

        // Set position of content board
        GeneratedContents.transform.position = tf.position; 
        GeneratedContents.transform.rotation = tf.rotation;
        GeneratedContents.transform.localScale = tf.localScale;

        return GeneratedContents;
    }
}
