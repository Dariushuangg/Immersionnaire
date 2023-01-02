using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ParseQuestions : MonoBehaviour
{
    public TextAsset jsonFile;
    public List<Question> questions;

    void Start()
    {
        parseQuestions();
    }

    /*
     * Parse the questions stored in JSON file into a list of Question objects.
     * Called when initializing the questionnaire.
     */
    public void parseQuestions() {
        QuestionDatas qds = JsonConvert.DeserializeObject<QuestionDatas>(jsonFile.text);
        if (qds.rawQuestionDatas == null) throw new Exception("Failed to parse JSON: rawQuestionDatas");

        questions = new List<Question>();
        foreach (QuestionData rawData in qds.rawQuestionDatas)
        {
            Type questionClassName = Type.GetType(rawData.questionType);
            ConstructorInfo ctor = questionClassName.GetConstructor(new[] { typeof(QuestionData) });
            Question question = (Question)ctor.Invoke(new object[] { rawData });
            questions.Add(question);
        }
    }
}
