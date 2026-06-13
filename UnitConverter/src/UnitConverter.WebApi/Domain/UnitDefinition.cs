namespace UnitConverter.WebApi.Domain;

public sealed record UnitDefinition(
    ConversionCategory Category,
    string CanonicalName,
    decimal? FactorToBaseUnit,
    IReadOnlyCollection<string> Aliases);
