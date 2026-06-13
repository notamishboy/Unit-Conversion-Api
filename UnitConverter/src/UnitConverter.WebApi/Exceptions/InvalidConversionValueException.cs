namespace UnitConverter.WebApi.Exceptions;

public sealed class InvalidConversionValueException : ApiException
{
    public InvalidConversionValueException()
        : base(
            "invalid_value",
            "Value must be provided.",
            StatusCodes.Status400BadRequest)
    {
    }
}
