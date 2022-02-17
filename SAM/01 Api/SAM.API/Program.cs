using Microsoft.OpenApi.Models;
using SAM.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
        c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo() { Title = "SAM.API", Version = "v1" });
            c.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "SAM.API.xml"));
        }
    );

// configure logging
builder.Services.AddLogging();

// configure mapping
builder.Services.InitializeMapper();

// configure in memory database
ServiceExtensions.InitializeDatabase(builder.Services);

// configuring services injection
ServiceExtensions.AddServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
