using Example;
using Microsoft.AspNetCore.Routing.Patterns;

var app = WebApplication.CreateBuilder(args).Build();

ScopedServices(app);

UseDeveloperExceptionPageInDevelopmentEnvironment(app);

MapRoutes(app);

app.Run();


void UseDeveloperExceptionPageInDevelopmentEnvironment(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
}

void MapRoutes(WebApplication app)
{
    app.MapGet("/", Sample.Index);
    app.MapGet("/get", Sample.Get);
    app.MapPost("/x", () => { });
    app.MapPut("/x", () => { });
    app.MapDelete("/x", () => { });

    //var pattern = RoutePatternFactory.Pattern();

    app.Map("", () => { });

    //app.MapMethods("/x", new[] { "PATCH" }, () => { });
    app.MapPatch("/x", () => { });
}


void ScopedServices(WebApplication app)
{

    //use the existing service provieder to create a separate scope
    //outside the web request scope to access scoped services
    //once the scope exists the services within the scope are released
    using var scope = app.Services.CreateAsyncScope();

    //get a service provider for the new scope
    var serviceProvider = scope.ServiceProvider;

    //resolve services within the new scope
    var hostEnvironment = serviceProvider.GetRequiredService<IHostEnvironment>();

}


//https://github.com/dotnet/aspnetcore/blob/main/src/Http/Routing/src/Builder/EndpointRouteBuilderExtensions.cs
//https://github.com/dotnet/aspnetcore/blob/abdc2a261146c90647f3af2380bc9168d55c6896/src/Http/Routing/src/Builder/DelegateEndpointRouteBuilderExtensions.cs
public static class HttpPatchVerbExtension
{
    private static readonly string[] PatchVerb = new[] { "PATCH" };
    public static void MapPatch(this IEndpointRouteBuilder endpoints,
            string pattern,
            Delegate handler)
    {
        endpoints.MapMethods(pattern, PatchVerb, handler);
    }
}
