using Microsoft.OpenApi.Models;
using Store.DB;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    //the controller-based web API wires up the controllers using the AddControllers method.
    services.AddControllers();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
      {
          c.SwaggerDoc("v1", new OpenApiInfo { Title = "Products API", Description = "Keep track of your tasks", Version = "v1" });
      });
}

var app = builder.Build();

// configure HTTP request pipeline
{
    // With minimal API, you add the route right away on the "app" instance
    app.MapControllers();
} 

app.UseSwagger();
app.UseSwaggerUI(c =>
  {
     c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
  });

app.MapGet("/", () => "Hello World!");

app.MapGet("/catalog", () => "some data");


//Pizzas API

app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));
app.MapGet("/pizzas", () => PizzaDB.GetPizzas());
app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
app.MapDelete("/pizzas/{id}", (int id) => PizzaDB.RemovePizza(id));

//starts your API and makes it listen for requests from the client.
app.Run();
