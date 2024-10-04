using AdventureWorks.Http.Filters.DocumentFilters;
using AdventureWorks.Http.Filters.Exceptions;
using AdventureWorks.Http.Filters.UrlMiddleware;
using AdventureWorks.Service;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdventureWorks REST API", Version = "v1" });
    c.DocumentFilter<LowercaseDocumentFilter>();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

ServiceConfiguration.ConfigureAdventureWorksServices(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseMiddleware<LowerCaseUrlMiddleware>();

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
