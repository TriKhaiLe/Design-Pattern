namespace Composite
{
    using System;
    using System.Collections.Generic;

    public interface MedicalService
    {
        string GetDescription();
        double GetCost();
    }

    public class Consultation : MedicalService
    {
        public string GetDescription()
        {
            return "Kham benh";
        }

        public double GetCost()
        {
            return 50.0;
        }
    }

    public class XRay : MedicalService
    {
        public string GetDescription()
        {
            return "Chup X-quang";
        }

        public double GetCost()
        {
            return 100.0;
        }
    }

    public class Surgery : MedicalService
    {
        public string GetDescription()
        {
            return "Phau thuat";
        }

        public double GetCost()
        {
            return 1000.0;
        }
    }

    public class CompositeMedicalService : MedicalService
    {
        private readonly List<MedicalService> services = [];
        private string packageName;

        public CompositeMedicalService(string packageName)
        {
            this.packageName = packageName;
        }

        public void AddService(MedicalService service)
        {
            services.Add(service);
        }

        public void RemoveService(MedicalService service)
        {
            services.Remove(service);
        }

        public string GetDescription()
        {
            string description = $"Goi dich vu {packageName} bao gom: ";
            foreach (var service in services)
            {
                description += service.GetDescription() + ", ";
            }
            return description.TrimEnd(',', ' ');
        }

        public double GetCost()
        {
            double totalCost = 0;
            foreach (var service in services)
            {
                totalCost += service.GetCost();
            }
            return totalCost;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Tao cac dich vu don le
            MedicalService consultation = new Consultation();
            MedicalService xray = new XRay();
            MedicalService surgery = new Surgery();

            Console.WriteLine("Dich vu don le:");
            Console.WriteLine($"{consultation.GetDescription()} - Chi phi: ${consultation.GetCost()}");
            Console.WriteLine($"{xray.GetDescription()} - Chi phi: ${xray.GetCost()}");
            Console.WriteLine($"{surgery.GetDescription()} - Chi phi: ${surgery.GetCost()}");
            Console.WriteLine();

            // Tao cac goi dich vu
            CompositeMedicalService generalHealthPackage = new("Goi Kham Tong Quat");
            generalHealthPackage.AddService(consultation);
            generalHealthPackage.AddService(xray);

            CompositeMedicalService advancedHealthPackage = new("Goi Kham Hoang Gia");
            advancedHealthPackage.AddService(consultation);
            advancedHealthPackage.AddService(xray);
            advancedHealthPackage.AddService(surgery);

            Console.WriteLine($"{generalHealthPackage.GetDescription()} - Tong chi phi: ${generalHealthPackage.GetCost()}");
            Console.WriteLine($"{advancedHealthPackage.GetDescription()} - Tong chi phi: ${advancedHealthPackage.GetCost()}");
        }
    }
}
