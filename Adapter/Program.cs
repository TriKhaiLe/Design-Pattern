namespace Adapter
{
    using System;

    public interface ICourse
    {
        void Start();
        string GetDetails();
    }

    public class InPersonCourse : ICourse
    {
        private string _courseName;
        private string _schedule;

        public InPersonCourse(string courseName, string schedule)
        {
            _courseName = courseName;
            _schedule = schedule;
        }

        public void AttendClass()
        {
            Console.WriteLine($"Attending in-person class for {_courseName}");
        }

        public string GetSchedule()
        {
            return _schedule;
        }

        public void Start()
        {
            AttendClass();
        }

        public string GetDetails()
        {
            return $"In-Person Course: {_courseName}, Schedule: {GetSchedule()}";
        }
    }

    public class OnlineCourse
    {
        private string _courseName;
        private string _timetable;

        public OnlineCourse(string courseName, string timetable)
        {
            _courseName = courseName;
            _timetable = timetable;
        }

        public void JoinSession()
        {
            Console.WriteLine($"Joining online session for {_courseName}");
        }

        public string ViewTimetable()
        {
            return _timetable;
        }
    }

    public class SelfStudyCourse
    {
        private string _courseName;
        private string _deadline;

        public SelfStudyCourse(string courseName, string deadline)
        {
            _courseName = courseName;
            _deadline = deadline;
        }

        public void AccessMaterial()
        {
            Console.WriteLine($"Accessing study materials for {_courseName}");
        }

        public string GetDeadline()
        {
            return _deadline;
        }
    }

    public class OnlineCourseAdapter : ICourse
    {
        private OnlineCourse _onlineCourse;

        public OnlineCourseAdapter(OnlineCourse onlineCourse)
        {
            _onlineCourse = onlineCourse;
        }

        public void Start()
        {
            _onlineCourse.JoinSession();
        }

        public string GetDetails()
        {
            return $"Online Course: {_onlineCourse.ViewTimetable()}";
        }
    }

    public class SelfStudyCourseAdapter : ICourse
    {
        private SelfStudyCourse _selfStudyCourse;

        public SelfStudyCourseAdapter(SelfStudyCourse selfStudyCourse)
        {
            _selfStudyCourse = selfStudyCourse;
        }

        public void Start()
        {
            _selfStudyCourse.AccessMaterial();
        }

        public string GetDetails()
        {
            return $"Self-Study Course: Deadline {_selfStudyCourse.GetDeadline()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ICourse[] courses =
            [
            new InPersonCourse("Programming 101", "Mon-Wed-Fri 9:00-11:00"),
            new OnlineCourseAdapter(new OnlineCourse("Web Development", "Tue-Thu 14:00-16:00")),
            new SelfStudyCourseAdapter(new SelfStudyCourse("Data Science", "2025-06-30"))
            ];

            Console.WriteLine("Education Management System Demo:");
            Console.WriteLine("=================================");

            foreach (var course in courses)
            {
                Console.WriteLine("\nCourse Details: " + course.GetDetails());
                Console.Write("Starting course: ");
                course.Start();
            }
        }
    }
}
