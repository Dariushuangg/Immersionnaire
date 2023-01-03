/*
 * C# representation of one question stored in JSON.
 * Contains all definitions of types for questions.
 */
[System.Serializable]
public class QuestionData
{
    public string questionType;
    public string prompt;
    public bool canSkip;

    /* only for MultipleChoice question */
    public int numOfChoices; 
    public string[] choiceText;

    /* only for SliderScale question */
    public int numOfSubprompts;
    public string[] subprompts;

    public QuestionData(string questionType,
        string prompt,
        bool canSkip,
        int numOfChoices,
        string[] choiceText,
        int numOfSubprompts,
        string[] subprompts)
    {
        this.questionType = questionType;
        this.prompt = prompt;
        this.canSkip = canSkip;
        this.numOfChoices = numOfChoices;
        this.choiceText = choiceText;
        this.numOfSubprompts = numOfSubprompts;
        this.subprompts = subprompts;
    }
}
