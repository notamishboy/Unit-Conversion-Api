using UnitConverter.WebApi.Domain;
using UnitConverter.WebApi.Infrastructure;
using Xunit;

namespace UnitConverter.Tests.Infrastructure;

public sealed class UnitCatalogTests
{
    private readonly UnitCatalog _catalog = new();

    [Theory]
    [InlineData("length", ConversionCategory.Length)]
    [InlineData("Temperature", ConversionCategory.Temperature)]
    [InlineData("mass", ConversionCategory.Weight)]
    public void TryResolveCategory_RecognizesAliases(string input, ConversionCategory expected)
    {
        var resolved = _catalog.TryResolveCategory(input, out var category);

        Assert.True(resolved);
        Assert.Equal(expected, category);
    }

    [Theory]
    [InlineData(ConversionCategory.Length, "ft", "foot")]
    [InlineData(ConversionCategory.Temperature, "C", "celsius")]
    [InlineData(ConversionCategory.Weight, "lbs", "pound")]
    public void TryResolveUnit_RecognizesAliases(ConversionCategory category, string input, string expectedCanonical)
    {
        var resolved = _catalog.TryResolveUnit(category, input, out var unit);

        Assert.True(resolved);
        Assert.NotNull(unit);
        Assert.Equal(expectedCanonical, unit!.CanonicalName);
    }
}
