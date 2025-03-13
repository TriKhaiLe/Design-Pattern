namespace Prototype
{
    using System;

    public interface IEntertainmentPrototype
    {
        IEntertainmentPrototype Clone();
        void DisplayInfo();
    }

    public abstract class Entertainment
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public string EntertainmentType { get; set; }

        public Entertainment(string title, int duration, string type)
        {
            Title = title;
            Duration = duration;
            EntertainmentType = type;
        }
    }

    public class Movie : Entertainment, IEntertainmentPrototype
    {
        public string Director { get; set; }

        public Movie(string title, int duration, string director)
            : base(title, duration, "Movie")
        {
            Director = director;
        }

        public IEntertainmentPrototype Clone()
        {
            return new Movie(this.Title, this.Duration, this.Director);
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Type: {EntertainmentType}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Duration: {Duration} minutes");
            Console.WriteLine($"Director: {Director}");
            Console.WriteLine("------------------------");
        }
    }

    public class TVShow : Entertainment, IEntertainmentPrototype
    {
        public int Season { get; set; }

        public TVShow(string title, int duration, int season)
            : base(title, duration, "TV Show")
        {
            Season = season;
        }

        public IEntertainmentPrototype Clone()
        {
            return new TVShow(this.Title, this.Duration, this.Season);
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Type: {EntertainmentType}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Duration: {Duration} minutes");
            Console.WriteLine($"Season: {Season}");
            Console.WriteLine("------------------------");
        }
    }

    public class MusicEvent : Entertainment, IEntertainmentPrototype
    {
        public string Artist { get; set; }

        public MusicEvent(string title, int duration, string artist)
            : base(title, duration, "Music Event")
        {
            Artist = artist;
        }

        public IEntertainmentPrototype Clone()
        {
            return new MusicEvent(this.Title, this.Duration, this.Artist);
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Type: {EntertainmentType}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Duration: {Duration} minutes");
            Console.WriteLine($"Artist: {Artist}");
            Console.WriteLine("------------------------");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Tạo các prototype gốc
            Movie originalMovie = new Movie("Inception", 148, "Christopher Nolan");
            TVShow originalTVShow = new TVShow("Breaking Bad", 45, 5);
            MusicEvent originalMusicEvent = new MusicEvent("Summer Concert", 120, "Taylor Swift");

            Console.WriteLine("Original Entertainment Programs:");
            originalMovie.DisplayInfo();
            originalTVShow.DisplayInfo();
            originalMusicEvent.DisplayInfo();

            // Tạo bản sao từ các prototype
            Movie clonedMovie = (Movie)originalMovie.Clone();
            TVShow clonedTVShow = (TVShow)originalTVShow.Clone();
            MusicEvent clonedMusicEvent = (MusicEvent)originalMusicEvent.Clone();

            // Tùy chỉnh các bản sao
            clonedMovie.Title = "Inception 2";
            clonedTVShow.Title = "Breaking Bad: The Movie";
            clonedMusicEvent.Duration = 150;

            Console.WriteLine("\nCloned and Modified Entertainment Programs:");
            clonedMovie.DisplayInfo();
            clonedTVShow.DisplayInfo();
            clonedMusicEvent.DisplayInfo();

            // Kiểm tra xem bản gốc có bị ảnh hưởng không
            Console.WriteLine("\nOriginal Programs (Unchanged):");
            originalMovie.DisplayInfo();
            originalTVShow.DisplayInfo();
            originalMusicEvent.DisplayInfo();
        }
    }
}
