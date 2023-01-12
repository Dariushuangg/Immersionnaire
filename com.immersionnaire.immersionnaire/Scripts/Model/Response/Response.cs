/*
 * Base class for all responses.
 */
public abstract class Response
{
    public bool isSelected { get; }
    public Response(bool isSelected) { this.isSelected = isSelected; }
}
