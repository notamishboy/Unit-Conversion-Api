namespace UnitConverter.WebApi.Contracts;

public sealed record ApiErrorResponse(
    string Code,
    string Message,
    IReadOnlyDictionary<string, string[]>? Details = null,
    string? TraceId = null);
