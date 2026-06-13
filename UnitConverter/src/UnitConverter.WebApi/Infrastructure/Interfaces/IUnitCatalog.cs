using UnitConverter.WebApi.Domain;

namespace UnitConverter.WebApi.Infrastructure.Interfaces;

public interface IUnitCatalog
{
    bool TryResolveCategory(string category, out ConversionCategory resolvedCategory);

    bool TryResolveUnit(ConversionCategory category, string unit, out UnitDefinition? unitDefinition);

    IReadOnlyCollection<UnitDefinition> GetUnits(ConversionCategory category);
}
