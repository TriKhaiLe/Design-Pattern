namespace DesignPatternBTVN
{
    internal class Program
    {
        public interface ICar
        {
            void ShowDetails();
        }

        public class EuropeanSedan : ICar
        {
            public void ShowDetails() => Console.WriteLine("European Sedan - Engine: Turbo, Seats: Leather, Size: Medium");
        }

        public class EuropeanSUV : ICar
        {
            public void ShowDetails() => Console.WriteLine("European SUV - Engine: V6, Seats: Luxury, Size: Large");
        }

        public class EuropeanElectric : ICar
        {
            public void ShowDetails() => Console.WriteLine("European Electric Car - Engine: Electric, Seats: Premium, Size: Compact");
        }

        public class AsianSedan : ICar
        {
            public void ShowDetails() => Console.WriteLine("Asian Sedan - Engine: Hybrid, Seats: Fabric, Size: Medium");
        }

        public class AsianSUV : ICar
        {
            public void ShowDetails() => Console.WriteLine("Asian SUV - Engine: V8, Seats: Standard, Size: Large");
        }

        public class AsianElectric : ICar
        {
            public void ShowDetails() => Console.WriteLine("Asian Electric Car - Engine: Battery, Seats: Eco-friendly, Size: Compact");
        }

        // Abstract Factory
        public interface ICarFactory
        {
            ICar CreateSedan();
            ICar CreateSUV();
            ICar CreateElectricCar();
        }

        // Concrete Factory cho Châu Âu
        public class EuropeanCarFactory : ICarFactory
        {
            public ICar CreateSedan() => new EuropeanSedan();
            public ICar CreateSUV() => new EuropeanSUV();
            public ICar CreateElectricCar() => new EuropeanElectric();
        }

        // Concrete Factory cho Châu Á
        public class AsianCarFactory : ICarFactory
        {
            public ICar CreateSedan() => new AsianSedan();
            public ICar CreateSUV() => new AsianSUV();
            public ICar CreateElectricCar() => new AsianElectric();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                ICarFactory? factory = null;
                while (factory == null)
                {
                    Console.Write("Chon khu vuc (European / Asian): ");
                    string region = Console.ReadLine().Trim();

                    factory = region switch
                    {
                        "European" => new EuropeanCarFactory(),
                        "Asian" => new AsianCarFactory(),
                        _ => null
                    };

                    if (factory == null)
                    {
                        Console.WriteLine("Khu vuc khong hop le, hay nhap lai.");
                    }
                }

                Console.Write("Chon loai xe (Sedan / SUV / Electric): ");
                string carType = Console.ReadLine().Trim();

                ICar? car = carType switch
                {
                    "Sedan" => factory.CreateSedan(),
                    "SUV" => factory.CreateSUV(),
                    "Electric" => factory.CreateElectricCar(),
                    _ => null
                };

                if (car == null)
                {
                    Console.WriteLine("Loai xe khong dung, hay chon lai.");
                    continue;
                }

                car.ShowDetails();
                Console.WriteLine("================================");
            }
        }
    }
}
