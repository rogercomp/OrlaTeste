using Orla.Api.Common;
using Orla.Api.Endpoint.YouTube;

namespace Orla.Api.Endpoint;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("");

        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/health", () => new { message = "OK" }); 

        //endpoints.MapGroup("v1/identity")
        //         .WithTags("Identity")
        //         .MapEndpoint<CreateUserEndpoint>()
        //         .MapEndpoint<LoginEndpoint>()
        //         .MapEndpoint<LogoutEndpoint>()                 
        //         .MapEndpoint<UserInfoEndpoint>()
        //         .MapEndpoint<UpdateUserEndpoint>()
        //         .MapEndpoint<ResetPasswordEndpoint>();            

        endpoints.MapGroup("v1/medicamento")
                .WithTags("Medicamentos")
                .MapEndpoint<CreateYouTubeDetailEndpoint>()
                .MapEndpoint<UpdateYouTubeDetailEndpoint>()
                .MapEndpoint<DeleteYouTubeDetailEndpoint>()
                .MapEndpoint<GetAllYouTubeDetailEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
