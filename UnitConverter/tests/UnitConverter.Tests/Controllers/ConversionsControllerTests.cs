using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using UnitConverter.WebApi.Contracts;
using UnitConverter.WebApi.Controllers;
using UnitConverter.WebApi.Services.Interfaces;
using Xunit;

namespace UnitConverter.Tests.Controllers;

public sealed class ConversionsControllerTests
{
    [Fact]
    public async Task Convert_ReturnsOkResult()
    {
        var service = new StubConversionService(
            new ConversionResponse("length", "meter", "foot", 1m, 3.28m));

        var controller = new ConversionsController(service, NullLogger<ConversionsController>.Instance);

        var result = await controller.Convert(new ConversionRequest
        {
            Category = "length",
            FromUnit = "meter",
            ToUnit = "foot",
            Value = 1m
        }, CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<ConversionResponse>(ok.Value);
        Assert.Equal(3.28m, response.ConvertedValue);
    }

    private sealed class StubConversionService : IUnitConversionService
    {
        private readonly ConversionResponse _response;

        public StubConversionService(ConversionResponse response)
        {
            _response = response;
        }

        public Task<ConversionResponse> ConvertAsync(ConversionRequest request, CancellationToken cancellationToken = default)
            => Task.FromResult(_response);
    }
}
