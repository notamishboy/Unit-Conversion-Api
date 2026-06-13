namespace UnitConverter.WebApi.Exceptions;

public sealed class ConversionNotSupportedException : ApiException
{
    public ConversionNotSupportedException(string category, string fromUnit, string toUnit)
        : base(
            "conversion_not_supported",
            $"Conversion from '{fromUnit}' to '{toUnit}' is not supported for category '{category}'.",
            StatusCodes.Status400BadRequest)
    {
    }
}
