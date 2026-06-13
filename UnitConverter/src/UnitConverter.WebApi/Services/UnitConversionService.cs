using UnitConverter.WebApi.Contracts;
using UnitConverter.WebApi.Domain;
using UnitConverter.WebApi.Exceptions;
using UnitConverter.WebApi.Infrastructure.Interfaces;
using UnitConverter.WebApi.Services.Interfaces;

namespace UnitConverter.WebApi.Services;

public sealed class UnitConversionService : IUnitConversionService
{
    private readonly IUnitCatalog _catalog;
    private readonly IReadOnlyDictionary<ConversionCategory, IUnitConversionStrategy> _strategies;

    public UnitConversionService(IEnumerable<IUnitConversionStrategy> strategies, IUnitCatalog catalog)
    {
        _catalog = catalog;
        _strategies = strategies.ToDictionary(strategy => strategy.Category);
    }

    public Task<ConversionResponse> ConvertAsync(ConversionRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.Value is null)
        {
            throw new InvalidConversionValueException();
        }

        if (!_catalog.TryResolveCategory(request.Category!, out var category))
        {
            throw new UnsupportedCategoryException(request.Category!);
        }

        if (!_strategies.TryGetValue(category, out var strategy))
        {
            throw new ConversionNotSupportedException(request.Category!, request.FromUnit!, request.ToUnit!);
        }

        if (!_catalog.TryResolveUnit(category, request.FromUnit!, out var fromUnit))
        {
            throw new UnsupportedUnitException(request.Category!, request.FromUnit!);
        }

        if (!_catalog.TryResolveUnit(category, request.ToUnit!, out var toUnit))
        {
            throw new UnsupportedUnitException(request.Category!, request.ToUnit!);
        }

        var convertedValue = strategy.Convert(request.Value.Value, fromUnit!, toUnit!);

        var response = new ConversionResponse(
            Category: category.ToString().ToLowerInvariant(),
            FromUnit: fromUnit!.CanonicalName,
            ToUnit: toUnit!.CanonicalName,
            InputValue: request.Value.Value,
            ConvertedValue: convertedValue);

        return Task.FromResult(response);
    }

}
