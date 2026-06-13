using UnitConverter.WebApi.Contracts;

namespace UnitConverter.WebApi.Services.Interfaces;

public interface IUnitConversionService
{
    Task<ConversionResponse> ConvertAsync(ConversionRequest request, CancellationToken cancellationToken = default);
}
