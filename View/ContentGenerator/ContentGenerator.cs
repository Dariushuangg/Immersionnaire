using System.Collections.Generic;
using UnityEngine;

public interface ContentGenerator
{
    /// <summary>
    /// Generate content onto the ContentBoard based upon the given question.
    /// </summary>
    /// <returns>Return the generated content.</returns>
    public GameObject GenerateContents(Question question);
}
