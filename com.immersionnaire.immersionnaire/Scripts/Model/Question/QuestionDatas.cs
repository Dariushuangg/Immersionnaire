/*
 * C# representation of a list of questions stored in JSON.
 */
[System.Serializable]
public class QuestionDatas
{
    public QuestionData[] rawQuestionDatas;

    public QuestionDatas(QuestionData[] rawQuestionDatas) {
        this.rawQuestionDatas = rawQuestionDatas;
    }
}
