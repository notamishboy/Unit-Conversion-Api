namespace UnitConverter.WebApi.Contracts;

public sealed record ConversionResponse(
    string Category,
    string FromUnit,
    string ToUnit,
    decimal InputValue,
    decimal ConvertedValue);
