using Microsoft.AspNetCore.Mvc;
using UnitConverter.WebApi.Contracts;
using UnitConverter.WebApi.Services.Interfaces;

namespace UnitConverter.WebApi.Controllers;

[ApiController]
[Route("")]
public sealed class ConversionsController : ControllerBase
{
    private readonly IUnitConversionService _conversionService;
    private readonly ILogger<ConversionsController> _logger;

    public ConversionsController(IUnitConversionService conversionService, ILogger<ConversionsController> logger)
    {
        _conversionService = conversionService;
        _logger = logger;
    }

    [HttpGet("convert")]
    [ProducesResponseType(typeof(ConversionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ConversionResponse>> Convert([FromQuery] ConversionRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Received conversion request: {Category} {FromUnit} -> {ToUnit}, Value={Value}",
            request.Category,
            request.FromUnit,
            request.ToUnit,
            request.Value);

        var response = await _conversionService.ConvertAsync(request, cancellationToken);
        return Ok(response);
    }
}
