namespace BusinessLogicLayer.Infrastructure;

public class DTOValidationException : Exception
{
    public string Property { get; protected set; }

    public DTOValidationException(string message, string prop) : base(message)
    {
        Property = prop;
    }
}