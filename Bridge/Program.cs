namespace Bridge
{
    using System;

    public interface IIrrigationSystem
    {
        void WaterPlants();
    }

    public class DripIrrigation : IIrrigationSystem
    {
        public void WaterPlants()
        {
            Console.WriteLine("Using drip");
        }
    }

    public class SprinklerIrrigation : IIrrigationSystem
    {
        public void WaterPlants()
        {
            Console.WriteLine("Using sprinkler");
        }
    }

    public class ManualIrrigation : IIrrigationSystem
    {
        public void WaterPlants()
        {
            Console.WriteLine("Watering plants manually");
        }
    }

    public interface IControlSystem
    {
        void Control();
    }

    public class ManualControl : IControlSystem
    {
        private IIrrigationSystem _irrigationSystem;

        public ManualControl(IIrrigationSystem irrigationSystem)
        {
            _irrigationSystem = irrigationSystem;
        }

        public void Control()
        {
            Console.WriteLine("Manually controlling");
            _irrigationSystem.WaterPlants();
        }
    }

    public class AutomaticControl : IControlSystem
    {
        private IIrrigationSystem _irrigationSystem;

        public AutomaticControl(IIrrigationSystem irrigationSystem)
        {
            _irrigationSystem = irrigationSystem;
        }

        public void Control()
        {
            Console.WriteLine("Automatically controlling");
            _irrigationSystem.WaterPlants();
        }
    }

    public abstract class IrrigationController
    {
        protected IControlSystem controlSystem;

        protected IrrigationController(IControlSystem controlSystem)
        {
            this.controlSystem = controlSystem;
        }

        public abstract void Operate();
    }

    public class FarmIrrigation : IrrigationController
    {
        private string _farmName;

        public FarmIrrigation(string farmName, IControlSystem controlSystem)
            : base(controlSystem) => _farmName = farmName;

        public override void Operate()
        {
            Console.WriteLine($"{_farmName}:");
            controlSystem.Control();
            Console.WriteLine("Completed\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IIrrigationSystem drip = new DripIrrigation();
            IIrrigationSystem sprinkler = new SprinklerIrrigation();
            IIrrigationSystem manual = new ManualIrrigation();

            IrrigationController[] irrigationSystems =
            [
            new FarmIrrigation("North Farm", new ManualControl(drip)),
            new FarmIrrigation("East Farm", new AutomaticControl(sprinkler)),
            new FarmIrrigation("South Farm", new ManualControl(manual)),
            new FarmIrrigation("West Farm", new AutomaticControl(drip))
            ];

            foreach (var system in irrigationSystems)
            {
                system.Operate();
            }
        }
    }
}
