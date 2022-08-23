var builder = WebApplication.CreateBuilder(args);


// var app = builder.Build();
// app.MapGet("/", () => "Hello World!");
// app.Run();


// add services to DI container
{
    var services = builder.Services;
    services.AddControllers();
}

var app = builder.Build();

// configure HTTP request pipeline
{
    app.MapControllers();
}

app.Run();
