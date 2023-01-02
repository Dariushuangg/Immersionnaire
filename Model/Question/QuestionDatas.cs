using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionDatas
{
    public QuestionData[] rawQuestionDatas;

    public QuestionDatas(QuestionData[] rawQuestionDatas) {
        this.rawQuestionDatas = rawQuestionDatas;
    }
}
