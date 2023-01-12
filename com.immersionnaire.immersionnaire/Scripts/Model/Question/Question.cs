using System;

/*
 * Base class for different types of questions.
 */
abstract public class Question
{
    public enum QuestionType { MC, S };
    public QuestionType questionType { get; }
    public string prompt { get; }
    public bool canSkip { get; }
    public int index { get; }
    public Question(QuestionData rawData) {
        questionType = (QuestionType) Enum.Parse(typeof(QuestionType), rawData.questionType);
        prompt = rawData.prompt;
        canSkip = rawData.canSkip;
        index = rawData.idx;
    }

    /// <summary>
    /// The number of question that can identify the asset. 
    /// </summary>
    public abstract int GetNumOfQuestion();
 }
