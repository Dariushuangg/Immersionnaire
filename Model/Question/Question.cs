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

    public Question(QuestionData rawData) {
        questionType = (QuestionType) Enum.Parse(typeof(QuestionType), rawData.questionType);
        prompt = rawData.prompt;
        canSkip = rawData.canSkip;
    }

    public abstract int GetNumOfQuestion();
 }
