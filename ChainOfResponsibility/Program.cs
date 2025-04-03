namespace ChainOfResponsibility
{
    using System;

    public class Patient
    {
        public string PatientId { get; set; }
        public string Name { get; set; }
        public string Symptoms { get; set; }

        public Patient(string patientId, string name, string symptoms)
        {
            PatientId = patientId;
            Name = name;
            Symptoms = symptoms;
        }
    }

    public class Request
    {
        public Patient Patient { get; set; }
        public string HealthStatus { get; set; }

        public Request(Patient patient)
        {
            Patient = patient;
            HealthStatus = "Pending";
        }
    }

    public interface IHandler
    {
        void SetNextHandler(IHandler handler);
        void HandleRequest(Request request);
    }

    public abstract class BaseHandler : IHandler
    {
        private IHandler _nextHandler;

        public void SetNextHandler(IHandler handler)
        {
            _nextHandler = handler;
        }

        public virtual void HandleRequest(Request request)
        {
            if (_nextHandler != null)
            {
                _nextHandler.HandleRequest(request);
            }
        }
    }

    public class ReceptionHandler : BaseHandler
    {
        public override void HandleRequest(Request request)
        {
            Console.WriteLine($"Reception: Processing patient {request.Patient.Name}. Symptoms: {request.Patient.Symptoms}");
            request.HealthStatus = "Received";
            base.HandleRequest(request); 
        }
    }

    public class DiagnosisHandler : BaseHandler
    {
        public override void HandleRequest(Request request)
        {
            Console.WriteLine($"Diagnosis: Diagnosing patient {request.Patient.Name}. Symptoms: {request.Patient.Symptoms}");
            request.HealthStatus = "Diagnosed";
            base.HandleRequest(request); 
        }
    }

    public class TreatmentHandler : BaseHandler
    {
        public override void HandleRequest(Request request)
        {
            Console.WriteLine($"Treatment: Treating patient {request.Patient.Name}. Prescribing treatment based on diagnosis.");
            request.HealthStatus = "Treated";
            base.HandleRequest(request); 
        }
    }

    public class ConsultationHandler : BaseHandler
    {
        public override void HandleRequest(Request request)
        {
            Console.WriteLine($"Consultation: Advising patient {request.Patient.Name} on post-treatment care.");
            request.HealthStatus = "Completed";
        }
    }

    public class HospitalClient
    {
        public void ProcessPatient(Patient patient)
        {
            Request request = new Request(patient);

            IHandler reception = new ReceptionHandler();
            IHandler diagnosis = new DiagnosisHandler();
            IHandler treatment = new TreatmentHandler();
            IHandler consultation = new ConsultationHandler();

            reception.SetNextHandler(diagnosis);
            diagnosis.SetNextHandler(treatment);
            treatment.SetNextHandler(consultation);

            Console.WriteLine("Starting patient processing...\n");
            reception.HandleRequest(request);
            Console.WriteLine($"\nPatient {patient.Name} processing completed. Final status: {request.HealthStatus}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Patient patient = new("P001", "Walt White", "Cancer");

            HospitalClient client = new();
            client.ProcessPatient(patient);
        }
    }
}
