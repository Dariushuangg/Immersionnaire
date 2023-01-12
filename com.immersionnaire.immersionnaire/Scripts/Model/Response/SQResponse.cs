public class SQResponse : Response
{
    private int scaleValueSelected { get; }
    public SQResponse(bool isSelected, int scaleValueSelected) : base(isSelected)
    {
        this.scaleValueSelected = scaleValueSelected;
    }
}
