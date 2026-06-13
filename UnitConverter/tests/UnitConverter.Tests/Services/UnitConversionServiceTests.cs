using UnitConverter.WebApi.Contracts;
using UnitConverter.WebApi.Exceptions;
using UnitConverter.WebApi.Infrastructure;
using UnitConverter.WebApi.Infrastructure.Interfaces;
using UnitConverter.WebApi.Infrastructure.Strategies;
using UnitConverter.WebApi.Services;
using UnitConverter.WebApi.Services.Interfaces;
using Xunit;

namespace UnitConverter.Tests.Services;

public sealed class UnitConversionServiceTests
{
    private readonly IUnitConversionService _service;

    public UnitConversionServiceTests()
    {
        var catalog = new UnitCatalog();
        var strategies = new IUnitConversionStrategy[]
        {
            new LengthConversionStrategy(),
            new TemperatureConversionStrategy(),
            new WeightConversionStrategy()
        };

        _service = new UnitConversionService(strategies, catalog);
    }

    [Fact]
    public async Task Convert_Length_MeterToFoot_ReturnsExpectedValue()
    {
        var result = await _service.ConvertAsync(new ConversionRequest
        {
            Category = "length",
            FromUnit = "meter",
            ToUnit = "foot",
            Value = 1m
        });

        Assert.Equal("length", result.Category);
        Assert.Equal("meter", result.FromUnit);
        Assert.Equal("foot", result.ToUnit);
        Assert.Equal(3.2808398950131233595800524934m, result.ConvertedValue);
    }

    [Fact]
    public async Task Convert_Temperature_CelsiusToFahrenheit_ReturnsExpectedValue()
    {
        var result = await _service.ConvertAsync(new ConversionRequest
        {
            Category = "temperature",
            FromUnit = "celsius",
            ToUnit = "fahrenheit",
            Value = 100m
        });

        Assert.Equal(212m, result.ConvertedValue);
    }

    [Fact]
    public async Task Convert_Weight_KilogramToPound_ReturnsExpectedValue()
    {
        var result = await _service.ConvertAsync(new ConversionRequest
        {
            Category = "weight",
            FromUnit = "kilogram",
            ToUnit = "pound",
            Value = 10m
        });

        Assert.Equal(22.04622621848775714699670600m, result.ConvertedValue);
    }

    [Fact]
    public async Task Convert_SameUnit_ReturnsSameValue()
    {
        var result = await _service.ConvertAsync(new ConversionRequest
        {
            Category = "length",
            FromUnit = "km",
            ToUnit = "kilometer",
            Value = 7.5m
        });

        Assert.Equal(7.5m, result.ConvertedValue);
    }

    [Fact]
    public async Task Convert_UnsupportedCategory_Throws()
    {
        await Assert.ThrowsAsync<UnsupportedCategoryException>(() => _service.ConvertAsync(new ConversionRequest
        {
            Category = "currency",
            FromUnit = "usd",
            ToUnit = "inr",
            Value = 10m
        }));
    }

    [Fact]
    public async Task Convert_UnsupportedUnit_Throws()
    {
        await Assert.ThrowsAsync<UnsupportedUnitException>(() => _service.ConvertAsync(new ConversionRequest
        {
            Category = "length",
            FromUnit = "meter",
            ToUnit = "lightyear",
            Value = 10m
        }));
    }
}
