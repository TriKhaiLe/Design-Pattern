namespace Iterator
{
    using System;
    using System.Collections.Generic;

    public class Vehicle
    {
        public string VehicleId { get; set; }
        public string Type { get; set; }
        public string LicensePlate { get; set; }
        public int Capacity { get; set; }
        public string Manufacturer { get; set; }

        public Vehicle(string vehicleId, string type, string licensePlate, int capacity, string manufacturer)
        {
            VehicleId = vehicleId;
            Type = type;
            LicensePlate = licensePlate;
            Capacity = capacity;
            Manufacturer = manufacturer;
        }

        public override string ToString()
        {
            return $"ID: {VehicleId}, Loai: {Type}, Bien so: {LicensePlate}, Tai trong: {Capacity}, Hang: {Manufacturer}";
        }
    }

    public interface VehicleIterator
    {
        bool HasNext();
        Vehicle Next();
    }

    public interface VehicleCollection
    {
        VehicleIterator CreateIterator();
    }

    public class BusCollection : VehicleCollection
    {
        private List<Vehicle> buses;

        public BusCollection()
        {
            buses = new List<Vehicle>();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            buses.Add(vehicle);
        }

        public VehicleIterator CreateIterator()
        {
            return new BusIterator(buses);
        }
    }

    public class BusIterator : VehicleIterator
    {
        private List<Vehicle> buses;
        private int position = 0;

        public BusIterator(List<Vehicle> buses)
        {
            this.buses = buses;
        }

        public bool HasNext()
        {
            return position < buses.Count;
        }

        public Vehicle Next()
        {
            if (HasNext())
            {
                Vehicle vehicle = buses[position];
                position++;
                return vehicle;
            }
            return null;
        }
    }

    public class TruckCollection : VehicleCollection
    {
        private List<Vehicle> trucks;

        public TruckCollection()
        {
            trucks = new List<Vehicle>();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            trucks.Add(vehicle);
        }

        public VehicleIterator CreateIterator()
        {
            return new TruckIterator(trucks);
        }
    }

    public class TruckIterator : VehicleIterator
    {
        private List<Vehicle> trucks;
        private int position = 0;

        public TruckIterator(List<Vehicle> trucks)
        {
            this.trucks = trucks;
        }

        public bool HasNext()
        {
            while (position < trucks.Count && trucks[position].Capacity <= 10)
            {
                position++;
            }
            return position < trucks.Count;
        }

        public Vehicle Next()
        {
            if (HasNext())
            {
                Vehicle vehicle = trucks[position];
                position++;
                return vehicle;
            }
            return null;
        }
    }

    public class CarCollection : VehicleCollection
    {
        private List<Vehicle> cars;

        public CarCollection()
        {
            cars = new List<Vehicle>();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            cars.Add(vehicle);
        }

        public VehicleIterator CreateIterator()
        {
            return new CarIterator(cars);
        }
    }

    public class CarIterator : VehicleIterator
    {
        private List<Vehicle> cars;
        private int position = 0;

        public CarIterator(List<Vehicle> cars)
        {
            this.cars = cars;
        }

        public bool HasNext()
        {
            while (position < cars.Count && cars[position].Manufacturer != "Toyota")
            {
                position++;
            }
            return position < cars.Count;
        }

        public Vehicle Next()
        {
            if (HasNext())
            {
                Vehicle vehicle = cars[position];
                position++;
                return vehicle;
            }
            return null;
        }
    }

    public class TrafficManager
    {
        public void DisplayVehicles(VehicleCollection collection)
        {
            VehicleIterator iterator = collection.CreateIterator();
            Console.WriteLine("Danh sach phuong tien:");
            while (iterator.HasNext())
            {
                Vehicle vehicle = iterator.Next();
                Console.WriteLine(vehicle.ToString());
            }
            Console.WriteLine("-------------------");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BusCollection busCollection = new BusCollection();
            TruckCollection truckCollection = new TruckCollection();
            CarCollection carCollection = new CarCollection();

            busCollection.AddVehicle(new Vehicle("B001", "Xe buyt", "51B-123", 40, "Hyundai"));
            busCollection.AddVehicle(new Vehicle("B002", "Xe buyt", "51B-124", 50, "Kia"));

            truckCollection.AddVehicle(new Vehicle("T001", "Xe tai", "51C-456", 5, "Isuzu"));
            truckCollection.AddVehicle(new Vehicle("T002", "Xe tai", "51C-457", 15, "Hino"));

            carCollection.AddVehicle(new Vehicle("C001", "Xe hoi", "51D-789", 4, "Honda"));
            carCollection.AddVehicle(new Vehicle("C002", "Xe hoi", "51D-790", 5, "Toyota"));

            TrafficManager manager = new TrafficManager();

            Console.WriteLine("Duyet xe buyt:");
            manager.DisplayVehicles(busCollection);

            Console.WriteLine("Duyet xe tai (tai trong > 10 tan):");
            manager.DisplayVehicles(truckCollection);

            Console.WriteLine("Duyet xe hoi (hang Toyota):");
            manager.DisplayVehicles(carCollection);
        }
    }
}
