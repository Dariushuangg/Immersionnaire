using System;
using System.Collections.Generic;

abstract public class Question
{
    public enum QuestionType { multipleChoice, sliderScale };
    public QuestionType questionType { get; }
    public string prompt { get; }
    public bool canSkip { get; }

    public Question(QuestionData rawData) {
        questionType = (QuestionType) Enum.Parse(typeof(QuestionType), rawData.questionType);
        prompt = rawData.prompt;
        canSkip = rawData.canSkip;
    }
 }
