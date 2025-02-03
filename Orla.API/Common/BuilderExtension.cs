using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Orla.Api.Data;
using Orla.Api.Handlers;
using Orla.Api.Models;
using Orla.Core;
using Orla.Core.Handlers;


namespace Orla.Api.Common;

public static class BuilderExtension
{
    public static void AddConfiguration(
        this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;        
        Configuration.ApiGoogleKey = builder.Configuration.GetValue<string>("ApiGoogleKey") ?? string.Empty;                
        Configuration.Issuer = builder.Configuration["Jwt:Issuer"] ?? string.Empty;
        Configuration.Audience = builder.Configuration["Jwt:Audience"] ?? string.Empty;
        Configuration.Key = builder.Configuration["Jwt:Key"] ?? string.Empty;
        Configuration.TokenExpireSeconds = builder.Configuration["Jwt:TokenExpireSeconds"] ?? string.Empty;

    }

    public static void AddSecurity(this WebApplicationBuilder builder)
    {

        builder.Services.AddAuthentication(options =>
          {
              options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;            
          })
            
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidIssuer = Configuration.Issuer,
                    ValidAudience = Configuration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.Key)),
                    ClockSkew = TimeSpan.FromSeconds(double.Parse(Configuration.TokenExpireSeconds))
                };
            });

        builder.Services.AddAuthorization();
    }

    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder.Services
               .AddDbContext<AppDbContext>(
                   options => { options.UseSqlite(Configuration.ConnectionString); })
               .AddEndpointsApiExplorer();

        builder.Services
          .AddIdentityCore<User>()
          .AddRoles<IdentityRole<long>>()
          .AddSignInManager()
          .AddErrorDescriber<IdentityPortugueseMessages>()
          .AddEntityFrameworkStores<AppDbContext>()
          .AddApiEndpoints();

    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
        options => options.AddPolicy(
                ApiConfiguration.CorsPolicyName,
                policy => policy
                    .WithOrigins([
                        Configuration.BackendUrl                        
                    ])
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            ));
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {
            x.CustomSchemaIds(n => n.FullName);
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API Orla",
                Version = "v1"
            });
            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            x.AddSecurityRequirement(new OpenApiSecurityRequirement {
                                            {
                                                new OpenApiSecurityScheme {
                                                    Reference = new OpenApiReference {
                                                        Type = ReferenceType.SecurityScheme,
                                                            Id = "Bearer"
                                                    }
                                                },
                                                new string[] {}
                                            }
                                        });
        });

    }

    public static void AddServices(this WebApplicationBuilder builder)
    {

        builder
            .Services
            .AddTransient<IAccountHandler, AccountHandler>();             

        builder
            .Services
            .AddTransient<IYouTubeHandler, YouTubeHandler>();

    }
}
