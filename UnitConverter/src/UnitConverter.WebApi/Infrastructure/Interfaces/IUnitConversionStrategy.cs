using UnitConverter.WebApi.Domain;

namespace UnitConverter.WebApi.Infrastructure.Interfaces;

public interface IUnitConversionStrategy
{
    ConversionCategory Category { get; }

    decimal Convert(decimal value, UnitDefinition fromUnit, UnitDefinition toUnit);
}
