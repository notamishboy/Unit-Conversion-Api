namespace UnitConverter.WebApi.Exceptions;

public sealed class UnsupportedUnitException : ApiException
{
    public UnsupportedUnitException(string category, string unit)
        : base(
            "unsupported_unit",
            $"Unsupported unit '{unit}' for category '{category}'.",
            StatusCodes.Status400BadRequest)
    {
    }
}
