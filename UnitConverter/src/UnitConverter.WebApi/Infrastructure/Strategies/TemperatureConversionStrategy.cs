using UnitConverter.WebApi.Domain;
using UnitConverter.WebApi.Exceptions;
using UnitConverter.WebApi.Infrastructure.Interfaces;

namespace UnitConverter.WebApi.Infrastructure.Strategies;

public sealed class TemperatureConversionStrategy : IUnitConversionStrategy
{
    public ConversionCategory Category => ConversionCategory.Temperature;

    public decimal Convert(decimal value, UnitDefinition fromUnit, UnitDefinition toUnit)
    {
        if (fromUnit.Category != Category || toUnit.Category != Category)
        {
            throw new InvalidOperationException("Temperature conversion strategy received mismatched units.");
        }

        if (fromUnit.CanonicalName.Equals(toUnit.CanonicalName, StringComparison.OrdinalIgnoreCase))
        {
            return value;
        }

        var celsius = ConvertToCelsius(value, fromUnit.CanonicalName);
        return ConvertFromCelsius(celsius, toUnit.CanonicalName);
    }

    private static decimal ConvertToCelsius(decimal value, string fromUnit)
        => fromUnit.ToLowerInvariant() switch
        {
            "celsius" => value,
            "fahrenheit" => (value - 32m) * 5m / 9m,
            "kelvin" => value - 273.15m,
            _ => throw new ConversionNotSupportedException(nameof(ConversionCategory.Temperature), fromUnit, "celsius")
        };

    private static decimal ConvertFromCelsius(decimal celsius, string toUnit)
        => toUnit.ToLowerInvariant() switch
        {
            "celsius" => celsius,
            "fahrenheit" => (celsius * 9m / 5m) + 32m,
            "kelvin" => celsius + 273.15m,
            _ => throw new ConversionNotSupportedException(nameof(ConversionCategory.Temperature), "celsius", toUnit)
        };
}
