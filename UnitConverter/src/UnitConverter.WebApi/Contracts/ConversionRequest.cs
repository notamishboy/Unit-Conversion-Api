using System.ComponentModel.DataAnnotations;

namespace UnitConverter.WebApi.Contracts;

public sealed class ConversionRequest
{
    [Required]
    public string? Category { get; init; }

    [Required]
    public string? FromUnit { get; init; }

    [Required]
    public string? ToUnit { get; init; }

    [Required]
    public decimal? Value { get; init; }
}
