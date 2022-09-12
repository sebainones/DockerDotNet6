var builder = WebApplication.CreateBuilder(args);


// var app = builder.Build();
// app.MapGet("/", () => "Hello World!");
// app.Run();


// add services to DI container
{
    var services = builder.Services;
    //the controller-based web API wires up the controllers using the AddControllers method.
    services.AddControllers();
}

var app = builder.Build();

// configure HTTP request pipeline
{
    // With minimal API, you add the route right away on the "app" instance
    app.MapControllers();
}

//starts your API and makes it listen for requests from the client.
app.Run();
