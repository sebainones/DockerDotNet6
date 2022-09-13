namespace Store.DB; 

 public record InMemoryPizza 
 {
   public int Id {get; set;} 
   public string ? Name { get; set; }

    public string? Description { get; set; }
 }

 public class InMemoryPizzaDB
 {
   private static List<InMemoryPizza> _pizzas = new List<InMemoryPizza>()
   {
     new InMemoryPizza{ Id=1, Name="Montemagno, Pizza shaped like a great mountain" },
     new InMemoryPizza{ Id=2, Name="The Galloway, Pizza shaped like a submarine, silent but deadly"},
     new InMemoryPizza{ Id=3, Name="The Noring, Pizza shaped like a Viking helmet, where's the mead"} 
   };

   public static List<InMemoryPizza> GetPizzas() 
   {
     return _pizzas;
   } 

   public static InMemoryPizza ? GetPizza(int id) 
   {
     return _pizzas.SingleOrDefault(pizza => pizza.Id == id);
   } 

   public static InMemoryPizza CreatePizza(InMemoryPizza pizza) 
   {
     _pizzas.Add(pizza);
     return pizza;
   }

   public static InMemoryPizza UpdatePizza(InMemoryPizza update) 
   {
     _pizzas = _pizzas.Select(pizza =>
     {
       if (pizza.Id == update.Id)
       {
         pizza.Name = update.Name;
       }
       return pizza;
     }).ToList();
     return update;
   }

   public static void RemovePizza(int id)
   {
     _pizzas = _pizzas.FindAll(pizza => pizza.Id != id).ToList();
   }
 }