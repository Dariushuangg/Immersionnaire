using System.Collections.Generic;

public class MCQResponse : Response {
    public List<string> selectedChoices { get; }
    public MCQResponse(bool isSelected, List<string> selectedChoices) : base(isSelected) {
        this.selectedChoices = selectedChoices;
    }
}
