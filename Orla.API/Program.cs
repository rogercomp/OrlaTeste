using Microsoft.AspNetCore.Identity;
using Serilog;
using Orla.Api;
using Orla.Api.Common;
using Orla.Api.Endpoint;
using Orla.Core;


var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

builder.Services.Configure<IdentityOptions>(options =>
{    
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
});

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


builder.Services
       .AddHttpClient(Configuration.HttpClientName, opt => { opt.BaseAddress = new Uri(Configuration.BackendUrl); });

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.ConfigureDevEnvironment();
}

app.UseSerilogRequestLogging();

app.UseCors(ApiConfiguration.CorsPolicyName);

app.UseSecurity();
app.MapEndpoints();

app.Run();