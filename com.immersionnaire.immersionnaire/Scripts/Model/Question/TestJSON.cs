using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJSON : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        QuestionData qd1 = new QuestionData("MCQ", "Who is the best engineer?", false, 4, new string[] { "Darius", "Jack", "JoJo", "Durant" }, -1, null);
        QuestionDatas qds = new QuestionDatas(new QuestionData[] { qd1 });
        string json = JsonConvert.SerializeObject(qds);
        Debug.Log(json);
        QuestionDatas qdsz = JsonConvert.DeserializeObject<QuestionDatas>(json);
        Debug.Log(qdsz.rawQuestionDatas[0].prompt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
