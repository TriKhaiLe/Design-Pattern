namespace FactoryMethod
{
    internal class Program
    {
        interface IDish
        {
            void Prepare();
            void Cook();
            void Serve();
        }

        class Pizza : IDish
        {
            public void Prepare() => Console.WriteLine("Preparing Pizza");
            public void Cook() => Console.WriteLine("Cooking Pizza");
            public void Serve() => Console.WriteLine("Serving Pizza");
        }

        class Burger : IDish
        {
            public void Prepare() => Console.WriteLine("Preparing Burger");
            public void Cook() => Console.WriteLine("Cooking Burger");
            public void Serve() => Console.WriteLine("Serving Burger");
        }

        class Pasta : IDish
        {
            public void Prepare() => Console.WriteLine("Preparing Pasta");
            public void Cook() => Console.WriteLine("Cooking Pasta");
            public void Serve() => Console.WriteLine("Serving Pasta");
        }

        interface IDishFactory
        {
            IDish CreateDish();
        }

        class PizzaFactory : IDishFactory
        {
            public IDish CreateDish() => new Pizza();
        }

        class BurgerFactory : IDishFactory
        {
            public IDish CreateDish() => new Burger();
        }

        class PastaFactory : IDishFactory
        {
            public IDish CreateDish() => new Pasta();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Chon mon an (Pizza / Burger / Pasta): ");
                string dish = Console.ReadLine().Trim();
                IDishFactory? factory = dish switch
                {
                    "Pizza" => new PizzaFactory(),
                    "Burger" => new BurgerFactory(),
                    "Pasta" => new PastaFactory(),
                    _ => null
                };
                if (factory == null)
                {
                    Console.WriteLine("Mon an khong ton tai");
                    continue;
                }

                IDish food = factory.CreateDish();
                food.Prepare();
                food.Cook();
                food.Serve();
            }
        }
    }
}
