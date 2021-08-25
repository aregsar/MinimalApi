using Example;


var app = WebApplication.CreateBuilder(args).Build();

//use the existing service provieder to create a separate scope
//outside the web request scope to access scoped services
//once the scope exists the services within the scope are released
// using (var scope = app.Services.CreateAsyncScope())
// {
//     //get a service provider for the new scope
//     var serviceProvider = scope.ServiceProvider;

//     //resolve services within the new scope
//     var hostEnvironment = serviceProvider.GetRequiredService<IHostEnvironment>();
// }

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.MapGet("/", () => "Hello World!");
app.MapGet("/", Sample.Index);
app.MapGet("/get", Sample.Get);

app.Run();
