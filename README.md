# UnitConverter

ASP.NET Core Web API for converting values across common measurement categories.

## Supported categories

- Length
- Temperature
- Weight / Mass

## Design highlights

- Controller-based Web API
- Clean separation between controller, service, catalog, and conversion strategies
- Strategy pattern for category-specific conversion rules
- Hardcoded unit catalog for this version
- Centralized exception handling with consistent API errors
- Unit tests for conversion logic and API behavior

## Prerequisites

- .NET 10 SDK
- Any editor or IDE that supports ASP.NET Core projects

## Run locally

From the repository root:

```bash
dotnet restore
dotnet run --project src/UnitConverter.WebApi/UnitConverter.WebApi.csproj
```

The app runs with Swagger enabled in development.

### Example requests

Length:
```bash
curl "https://localhost:5001/convert?category=length&fromUnit=meter&toUnit=foot&value=1"
```

Temperature:
```bash
curl "https://localhost:5001/convert?category=temperature&fromUnit=celsius&toUnit=fahrenheit&value=100"
```

Weight:
```bash
curl "https://localhost:5001/convert?category=weight&fromUnit=kilogram&toUnit=pound&value=10"
```

## Notes

- Category is required to keep unit resolution unambiguous and scalable.
- Units are hardcoded in this version, but the design isolates unit definitions so new units can be added in one place later.
- The service validates unsupported categories and units and returns structured 400 responses instead of generic server errors.
