using UnitConverter.WebApi.Infrastructure.Interfaces;
using UnitConverter.WebApi.Infrastructure;
using UnitConverter.WebApi.Infrastructure.Strategies;
using UnitConverter.WebApi.Middleware;
using UnitConverter.WebApi.Services.Interfaces;
using UnitConverter.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUnitCatalog, UnitCatalog>();
builder.Services.AddSingleton<IUnitConversionStrategy, LengthConversionStrategy>();
builder.Services.AddSingleton<IUnitConversionStrategy, TemperatureConversionStrategy>();
builder.Services.AddSingleton<IUnitConversionStrategy, WeightConversionStrategy>();
builder.Services.AddSingleton<IUnitConversionService, UnitConversionService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program { }
