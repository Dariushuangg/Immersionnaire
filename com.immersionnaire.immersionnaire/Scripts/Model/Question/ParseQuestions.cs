using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ParseQuestions : MonoBehaviour
{
    public TextAsset jsonFile;

    /*
     * Parse the questions stored in JSON file into a list of Question objects.
     * Called when initializing the questionnaire.
     */
    public List<Question> parseQuestions() {
        QuestionDatas qds = JsonConvert.DeserializeObject<QuestionDatas>(jsonFile.text);
        if (qds.rawQuestionDatas == null) throw new Exception("Failed to parse JSON: rawQuestionDatas");

        List<Question> questions = new List<Question>();
        foreach (QuestionData rawData in qds.rawQuestionDatas)
        {
            Type questionClassName = Type.GetType(rawData.questionType + "Question");
            ConstructorInfo ctor = questionClassName.GetConstructor(new[] { typeof(QuestionData) });
            Question question = (Question)ctor.Invoke(new object[] { rawData });
            questions.Add(question);
        }
        return questions;
    }

    public void testParse() {
        List<Question> questions = parseQuestions();
        foreach (Question q in questions) {
            Debug.Log(q.prompt);
        }
    }
}
