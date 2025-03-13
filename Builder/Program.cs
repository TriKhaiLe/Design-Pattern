namespace Builder
{
    class DietPlan
    {
        public string Name { get; set; }
        public List<string> Proteins { get; set; } = new List<string>();
        public List<string> Carbohydrates { get; set; } = new List<string>();
        public List<string> Vegetables { get; set; } = new List<string>();
        public string Drink { get; set; }

        public void ShowPlan()
        {
            Console.WriteLine($"Che do an: {Name}");
            Console.WriteLine("Protein: " + string.Join(", ", Proteins));
            Console.WriteLine("Carbohydrate: " + string.Join(", ", Carbohydrates));
            Console.WriteLine("Rau cu qua: " + string.Join(", ", Vegetables));
            Console.WriteLine($"Do uong: {Drink}\n");
        }
    }

    interface IDietPlanBuilder
    {
        IDietPlanBuilder SetName(string name);
        IDietPlanBuilder AddProtein(string protein);
        IDietPlanBuilder AddCarbohydrate(string carbohydrate);
        IDietPlanBuilder AddVegetable(string vegetable);
        IDietPlanBuilder SetDrink(string drink);
        DietPlan GetPlan();
    }

    class DietPlanBuilder : IDietPlanBuilder
    {
        private DietPlan _dietPlan = new DietPlan();

        public IDietPlanBuilder SetName(string name)
        {
            _dietPlan.Name = name;
            return this;
        }

        public IDietPlanBuilder AddProtein(string protein)
        {
            _dietPlan.Proteins.Add(protein);
            return this;
        }
        public IDietPlanBuilder AddCarbohydrate(string carbohydrate)
        {
            _dietPlan.Carbohydrates.Add(carbohydrate);
            return this;
        }
        public IDietPlanBuilder AddVegetable(string vegetable)
        {
            _dietPlan.Vegetables.Add(vegetable);
            return this;
        }
        public IDietPlanBuilder SetDrink(string drink)
        {
            _dietPlan.Drink = drink;
            return this;
        }

        public DietPlan GetPlan() => _dietPlan;
    }

    class DietPlanDirector
    {
        public void ConstructMediterraneanDiet(IDietPlanBuilder builder)
        {
            builder.SetName("Che do an Dia Trung Hai")
                .AddProtein("Thit ca")
                .AddCarbohydrate("Dau o liu")
                .AddVegetable("Rau qua tuoi")
                .SetDrink("Ruou vang do");
        }

        public void ConstructDashDiet(IDietPlanBuilder builder)
        {
            builder.SetName("Che do an DASH")
                .AddProtein("Thit ga")
                .AddCarbohydrate("Gao lut")
                .AddVegetable("Rau cai xanh")
                .SetDrink("Nuoc loc");
        }
        public void ConstructVegetarianDiet(IDietPlanBuilder builder)
        {
            builder.SetName("Che do an Chay")
                .AddProtein("Dau nhan")
                .AddCarbohydrate("Bun")
                .AddVegetable("Rau cai thao")
                .SetDrink("Nuoc ep ca rot");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var builder = new DietPlanBuilder();
                var director = new DietPlanDirector();

                director.ConstructMediterraneanDiet(builder);
                var mediterraneanDiet = builder.GetPlan();
                mediterraneanDiet.ShowPlan();

                builder = new DietPlanBuilder();
                director.ConstructDashDiet(builder);
                var dashDiet = builder.GetPlan();
                dashDiet.ShowPlan();

                builder = new DietPlanBuilder();
                director.ConstructVegetarianDiet(builder);
                var vegetarianDiet = builder.GetPlan();
                vegetarianDiet.ShowPlan();

                Console.WriteLine("Nhan phim bat ky de xem lai, nhan 'q' de thoat");
                if (Console.ReadKey().KeyChar == 'q')
                    break;
                Console.Clear();
            }
        }
    }
}
