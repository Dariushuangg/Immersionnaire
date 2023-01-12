using System.Collections.Generic;

public class MCQResponse : Response {
    public List<string> SelectedChoices { get; }
    public MCQResponse(bool isSelected, List<string> selectedChoices) : base(isSelected) {
        SelectedChoices = selectedChoices;
    }
}
