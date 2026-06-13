using UnitConverter.WebApi.Domain;
using UnitConverter.WebApi.Infrastructure.Interfaces;

namespace UnitConverter.WebApi.Infrastructure.Strategies;

public sealed class LengthConversionStrategy : IUnitConversionStrategy
{
    public ConversionCategory Category => ConversionCategory.Length;

    public decimal Convert(decimal value, UnitDefinition fromUnit, UnitDefinition toUnit)
    {
        if (fromUnit.Category != Category || toUnit.Category != Category)
        {
            throw new InvalidOperationException("Length conversion strategy received mismatched units.");
        }

        if (fromUnit.CanonicalName.Equals(toUnit.CanonicalName, StringComparison.OrdinalIgnoreCase))
        {
            return value;
        }

        if (fromUnit.FactorToBaseUnit is null || toUnit.FactorToBaseUnit is null)
        {
            throw new InvalidOperationException("Length units must have a factor to the base unit.");
        }

        var valueInMeters = value * fromUnit.FactorToBaseUnit.Value;
        return valueInMeters / toUnit.FactorToBaseUnit.Value;
    }
}
