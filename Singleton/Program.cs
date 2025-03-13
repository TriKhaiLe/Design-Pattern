namespace Singleton
{
    using System;
    using System.Collections.Generic;

    public class Patient
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MedicalCondition { get; set; }

        public Patient(string id, string name, DateTime dob, string condition)
        {
            Id = id;
            Name = name;
            DateOfBirth = dob;
            MedicalCondition = condition;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, DOB: {DateOfBirth.ToShortDateString()}, Condition: {MedicalCondition}";
        }
    }

    public class PatientRecordManager
    {
        // Instance duy nhất của lớp (static)
        private static PatientRecordManager _instance;
        private static readonly object _lock = new();

        private Dictionary<string, Patient> patientRecords;

        private PatientRecordManager()
        {
            patientRecords = new Dictionary<string, Patient>();
        }

        // Phương thức static để lấy instance duy nhất
        public static PatientRecordManager Instance
        {
            get
            {
                // Double-check locking để đảm bảo thread-safe
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new PatientRecordManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public void AddPatient(Patient patient)
        {
            if (!patientRecords.ContainsKey(patient.Id))
            {
                patientRecords.Add(patient.Id, patient);
                Console.WriteLine($"Added patient: {patient.Name}");
            }
            else
            {
                Console.WriteLine($"Patient with ID {patient.Id} already exists!");
            }
        }

        public bool UpdatePatient(string id, string newCondition)
        {
            if (patientRecords.ContainsKey(id))
            {
                patientRecords[id].MedicalCondition = newCondition;
                Console.WriteLine($"Updated medical condition for patient ID: {id}");
                return true;
            }
            Console.WriteLine($"Patient with ID {id} not found!");
            return false;
        }

        public Patient GetPatient(string id)
        {
            if (patientRecords.ContainsKey(id))
            {
                return patientRecords[id];
            }
            Console.WriteLine($"Patient with ID {id} not found!");
            return null;
        }

        public void DisplayAllRecords()
        {
            Console.WriteLine("\nAll Patient Records:");
            foreach (var patient in patientRecords.Values)
            {
                Console.WriteLine(patient.ToString());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PatientRecordManager recordManager = PatientRecordManager.Instance;

            // Mô phỏng truy cập từ bác sĩ
            Console.WriteLine("Doctor accessing system:");
            recordManager.AddPatient(new Patient("P001", "John Doe", new DateTime(1980, 5, 15), "Hypertension"));
            recordManager.AddPatient(new Patient("P002", "Jane Smith", new DateTime(1990, 8, 22), "Diabetes"));

            // Mô phỏng truy cập từ y tá
            Console.WriteLine("\nNurse accessing system:");
            PatientRecordManager nurseManager = PatientRecordManager.Instance; // Vẫn là cùng instance
            nurseManager.UpdatePatient("P001", "Hypertension and Asthma");

            // Mô phỏng truy cập từ nhân viên hành chính
            Console.WriteLine("\nAdmin accessing system:");
            PatientRecordManager adminManager = PatientRecordManager.Instance; // Vẫn là cùng instance
            Patient patient = adminManager.GetPatient("P001");
            if (patient != null)
            {
                Console.WriteLine($"Retrieved: {patient}");
            }

            recordManager.DisplayAllRecords();

            // Chứng minh rằng tất cả đều dùng cùng một instance
            Console.WriteLine("\nMemory addresses to prove singleton:");
            Console.WriteLine($"Doctor instance: {recordManager.GetHashCode()}");
            Console.WriteLine($"Nurse instance: {nurseManager.GetHashCode()}");
            Console.WriteLine($"Admin instance: {adminManager.GetHashCode()}");
        }
    }
}
