namespace UnitConverter.WebApi.Exceptions;

public sealed class UnsupportedCategoryException : ApiException
{
    public UnsupportedCategoryException(string category)
        : base(
            "unsupported_category",
            $"Unsupported category '{category}'. Supported categories are length, temperature, and weight.",
            StatusCodes.Status400BadRequest)
    {
    }
}
