using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, GC!");
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            using (Movie movie1 = new Movie("Inception", "Warner Bros.", "Sci-Fi", 148, 2010))
            {
                movie1.DisplayInfo();
            }

            Movie movie2 = new Movie("Interstellar", "Paramount", "Sci-Fi", 169, 2014);
            movie2.DisplayInfo();
            movie2.Dispose();

            try
            {
                movie2.DisplayInfo();
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            //CreateFilm();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();

            //Console.WriteLine("\nProgramm over.");
        }

        static void CreateFilm()
        {
            Movie movie2 = new Movie("Interstellar333", "Paramount", "Sci-Fi", 169, 2025);
        }
    }

    class Movie : IDisposable
    {
        private bool _disposed = false;
        public string Title { get; set; }
        public string Studio { get; set; }
        public string Genre { get; set; }
        public int DurationMinutes { get; set; }
        public int ReleaseYear { get; set; }

        public Movie(string title, string studio, string genre, int durationMinutes, int releaseYear)
        {
            Title = title;
            Studio = studio;
            Genre = genre;
            DurationMinutes = durationMinutes;
            ReleaseYear = releaseYear;
        }

        public void DisplayInfo()
        {
 
            if (_disposed) throw new ObjectDisposedException(nameof(Movie), "Object is deleted now.");

            Console.WriteLine($"Name: {Title}");
            Console.WriteLine($"Studio: {Studio}");
            Console.WriteLine($"Genre: {Genre}");
            Console.WriteLine($"Durationь: {DurationMinutes} хв");
            Console.WriteLine($"Year: {ReleaseYear}");
            Console.WriteLine("-----------------------------------");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            Console.WriteLine("Object has been deleted successfully.");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Console.WriteLine($"[Dispose] Managed resources for the movie '{Title}' sucsess free.");
                }


                _disposed = true;
            }
        }

        ~Movie()
        {
            Dispose(false);
            Console.WriteLine($"Destructor. Movie was deleted '{Title}'.");
        }
    }

        

}
