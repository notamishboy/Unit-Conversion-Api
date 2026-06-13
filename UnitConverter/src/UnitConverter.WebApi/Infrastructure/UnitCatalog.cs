using UnitConverter.WebApi.Domain;
using UnitConverter.WebApi.Infrastructure.Interfaces;

namespace UnitConverter.WebApi.Infrastructure;

public sealed class UnitCatalog : IUnitCatalog
{
    private readonly Dictionary<ConversionCategory, IReadOnlyCollection<UnitDefinition>> _unitsByCategory;
    private readonly Dictionary<string, ConversionCategory> _categoryAliases;

    public UnitCatalog()
    {
        _categoryAliases = new Dictionary<string, ConversionCategory>(StringComparer.OrdinalIgnoreCase)
        {
            ["length"] = ConversionCategory.Length,
            ["distance"] = ConversionCategory.Length,

            ["temperature"] = ConversionCategory.Temperature,
            ["temp"] = ConversionCategory.Temperature,

            ["weight"] = ConversionCategory.Weight,
            ["mass"] = ConversionCategory.Weight
        };

        _unitsByCategory = new Dictionary<ConversionCategory, IReadOnlyCollection<UnitDefinition>>
        {
            [ConversionCategory.Length] = new[]
            {
                new UnitDefinition(ConversionCategory.Length, "meter", 1m, new[] { "meter", "meters", "m" }),
                new UnitDefinition(ConversionCategory.Length, "kilometer", 1000m, new[] { "kilometer", "kilometers", "km" }),
                new UnitDefinition(ConversionCategory.Length, "centimeter", 0.01m, new[] { "centimeter", "centimeters", "cm" }),
                new UnitDefinition(ConversionCategory.Length, "millimeter", 0.001m, new[] { "millimeter", "millimeters", "mm" }),
                new UnitDefinition(ConversionCategory.Length, "mile", 1609.344m, new[] { "mile", "miles", "mi" }),
                new UnitDefinition(ConversionCategory.Length, "yard", 0.9144m, new[] { "yard", "yards", "yd" }),
                new UnitDefinition(ConversionCategory.Length, "foot", 0.3048m, new[] { "foot", "feet", "ft" }),
                new UnitDefinition(ConversionCategory.Length, "inch", 0.0254m, new[] { "inch", "inches", "in" })
            },

            [ConversionCategory.Temperature] = new[]
            {
                new UnitDefinition(ConversionCategory.Temperature, "celsius", null, new[] { "celsius", "c", "degc" }),
                new UnitDefinition(ConversionCategory.Temperature, "fahrenheit", null, new[] { "fahrenheit", "f", "degf" }),
                new UnitDefinition(ConversionCategory.Temperature, "kelvin", null, new[] { "kelvin", "k" })
            },

            [ConversionCategory.Weight] = new[]
            {
                new UnitDefinition(ConversionCategory.Weight, "kilogram", 1m, new[] { "kilogram", "kilograms", "kg" }),
                new UnitDefinition(ConversionCategory.Weight, "gram", 0.001m, new[] { "gram", "grams", "g" }),
                new UnitDefinition(ConversionCategory.Weight, "milligram", 0.000001m, new[] { "milligram", "milligrams", "mg" }),
                new UnitDefinition(ConversionCategory.Weight, "pound", 0.45359237m, new[] { "pound", "pounds", "lb", "lbs" }),
                new UnitDefinition(ConversionCategory.Weight, "ounce", 0.028349523125m, new[] { "ounce", "ounces", "oz" })
            }
        };
    }

    public bool TryResolveCategory(string category, out ConversionCategory resolvedCategory)
    {
        if (string.IsNullOrWhiteSpace(category))
        {
            resolvedCategory = default;
            return false;
        }

        return _categoryAliases.TryGetValue(category.Trim(), out resolvedCategory);
    }

    public bool TryResolveUnit(ConversionCategory category, string unit, out UnitDefinition? unitDefinition)
    {
        unitDefinition = null;

        if (string.IsNullOrWhiteSpace(unit))
        {
            return false;
        }

        if (!_unitsByCategory.TryGetValue(category, out var units))
        {
            return false;
        }

        unitDefinition = units.FirstOrDefault(x =>
            x.Aliases.Any(alias => alias.Equals(unit.Trim(), StringComparison.OrdinalIgnoreCase)));

        return unitDefinition is not null;
    }

    public IReadOnlyCollection<UnitDefinition> GetUnits(ConversionCategory category)
        => _unitsByCategory.TryGetValue(category, out var units)
            ? units
            : Array.Empty<UnitDefinition>();
}
