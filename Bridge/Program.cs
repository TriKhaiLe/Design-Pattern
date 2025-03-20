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

    public abstract class ControlSystem
    {
        protected IIrrigationSystem _irrigationSystem;

        public ControlSystem(IIrrigationSystem irrigationSystem)
        {
            _irrigationSystem = irrigationSystem;
        }

        public abstract void Control();
    }

    public class ManualControl : ControlSystem
    {
        public ManualControl(IIrrigationSystem irrigationSystem) : base(irrigationSystem)
        {
        }

        public override void Control()
        {
            Console.WriteLine("Manually controlling");
            _irrigationSystem.WaterPlants();
        }
    }

    public class AutomaticControl : ControlSystem
    {
        public AutomaticControl(IIrrigationSystem irrigationSystem) : base(irrigationSystem)
        {
        }

        public override void Control()
        {
            Console.WriteLine("Automatically controlling");
            _irrigationSystem.WaterPlants();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IIrrigationSystem drip = new DripIrrigation();
            IIrrigationSystem sprinkler = new SprinklerIrrigation();
            IIrrigationSystem manual = new ManualIrrigation();

            ControlSystem[] irrigationSystems =
            [
                new ManualControl(manual),
                new AutomaticControl(sprinkler),
                new AutomaticControl(drip)
            ];

            foreach (var system in irrigationSystems)
            {
                system.Control();
                Console.WriteLine();
            }
        }
    }
}