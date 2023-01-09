using System;

public class MCQuestion : Question
{
    public int numOfChoices { get; }
    public string[] choiceText { get; }
    public MCQuestion(QuestionData rawData) : base(rawData) {
        numOfChoices = rawData.numOfChoices;
        choiceText = rawData.choiceText;
        if (numOfChoices != choiceText.Length) throw new Exception("Invalid Question Input: choiceText");
    }

    public override int GetNumOfQuestion()
    {
        return numOfChoices;
    }
}
