using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQuestion : Question
{
    public int numOfSubprompts { get; }
    public string[] subprompts { get; }
    public SQuestion(QuestionData rawData) : base(rawData) {
        numOfSubprompts = rawData.numOfSubprompts;
        subprompts = rawData.subprompts;
        if (numOfSubprompts != subprompts.Length) throw new Exception("Invalid Question Input: subprompts");
    }
}
