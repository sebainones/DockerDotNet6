using Microsoft.OpenApi.Models;
using Store.DB;
using Microsoft.EntityFrameworkCore;
using DockerDotNet;
using DockerDotNet.Models;

var builder = WebApplication.CreateBuilder(args);


// add services to DI container
{
    var services = builder.Services;
    //the controller-based web API wires up the controllers using the AddControllers method.
    services.AddControllers();

    //Replace the in-memory database with a persistent database.
    services.AddDbContext<PizzaDbContext>(options => options.UseInMemoryDatabase("items"));

    // services.AddDbContext<PizzaDbContext>(opt =>
    //         opt.UseNpgsql(builder.Configuration.GetConnectionString("ProductsDatabase")));

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
      {
          c.SwaggerDoc("v1", new OpenApiInfo { Title = "Products/Pizzas API", Description = "Some experimental API", Version = "v1" });
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


//Pizzas Minimal API using real UseInMemoryDatabase
//GET 

app.MapGet("/pizzas", async (PizzaDbContext db) => await db.Pizzas.ToListAsync());

//POST
app.MapPost("/pizza", async (PizzaDbContext db, Pizza pizza) =>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});

//PUT

app.MapPut("/pizza/{id}", async (PizzaDbContext db, Pizza updatepizza, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();
    pizza.Name = updatepizza.Name;
    pizza.Description = updatepizza.Description;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

//DELETE
app.MapDelete("/pizza/{id}", async (PizzaDbContext db, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null)
    {
        return Results.NotFound();
    }
    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});


app.MapGet("/", () => "☁️ Hello MultiCloud World ⛅");

//Pizzas Minimal API using custom Class doing something like InMemory
app.MapGet("/inmemory/pizzas/{id}", (int id) => InMemoryPizzaDB.GetPizza(id));
app.MapGet("/inmemory/pizzas", () => InMemoryPizzaDB.GetPizzas());
app.MapPost("/inmemory/pizzas", (InMemoryPizza pizza) => InMemoryPizzaDB.CreatePizza(pizza));
app.MapPut("/inmemory/pizzas", (InMemoryPizza pizza) => InMemoryPizzaDB.UpdatePizza(pizza));
app.MapDelete("/inmemory/pizzas/{id}", (int id) => InMemoryPizzaDB.RemovePizza(id));

//starts your API and makes it listen for requests from the client.
app.Run();
