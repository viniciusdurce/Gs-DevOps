using System.Text.Json;
using EnergyConsumptionAPI.Controllers;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using EnergyConsumptionAPI.Services;
using EnergyConsumptionAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EnergyConsumptionAPI", Version = "v1" });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("DbConnection") 
                           ?? throw new InvalidOperationException("A string de conexão com o MongoDB está ausente.");
    return new MongoClient(connectionString);
});

builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("Plusoft");
});


builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddScoped<ConsumptionAnalysisService>();
builder.Services.AddScoped<EnergyConsumptionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
