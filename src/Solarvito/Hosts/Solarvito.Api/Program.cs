using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.File;
using Solarvito.Api.Middlewares;
using Solarvito.Api.Modules;
using Solarvito.DataAccess;
using Solarvito.Domain;
using Solarvito.Registrar;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, services, configuration) =>
configuration.ReadFrom.Configuration(context.Configuration)
.Enrich.FromLogContext()
.WriteTo.Console()
.WriteTo.Seq("http://localhost:5341"));

builder.Services.AddServiceRegistrationModule();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerModule();

builder.Services.AddAuthenticationModule(builder.Configuration);

builder.Services.AddIdentityModule();

builder.Services.AddRedisModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
