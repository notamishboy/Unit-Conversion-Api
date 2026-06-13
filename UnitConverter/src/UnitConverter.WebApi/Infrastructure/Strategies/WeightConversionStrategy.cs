using UnitConverter.WebApi.Domain;
using UnitConverter.WebApi.Infrastructure.Interfaces;

namespace UnitConverter.WebApi.Infrastructure.Strategies;

public sealed class WeightConversionStrategy : IUnitConversionStrategy
{
    public ConversionCategory Category => ConversionCategory.Weight;

    public decimal Convert(decimal value, UnitDefinition fromUnit, UnitDefinition toUnit)
    {
        if (fromUnit.Category != Category || toUnit.Category != Category)
        {
            throw new InvalidOperationException("Weight conversion strategy received mismatched units.");
        }

        if (fromUnit.CanonicalName.Equals(toUnit.CanonicalName, StringComparison.OrdinalIgnoreCase))
        {
            return value;
        }

        if (fromUnit.FactorToBaseUnit is null || toUnit.FactorToBaseUnit is null)
        {
            throw new InvalidOperationException("Weight units must have a factor to the base unit.");
        }

        var valueInKilograms = value * fromUnit.FactorToBaseUnit.Value;
        return valueInKilograms / toUnit.FactorToBaseUnit.Value;
    }
}
