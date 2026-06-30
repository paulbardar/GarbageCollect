namespace PoemApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Poem!");

            using (Poem poem1 = new Poem("Odysseus", "Homer", "Epic", 700))
            {
                poem1.ShowInfo();
            }
            Console.WriteLine("Object has been deleted successfully.");
            Console.WriteLine("_____________________________________");
            CreatePoem();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        static void CreatePoem()
        {
            Poem poem2 = new Poem("Hamlet", "Shekspiert", "Sci-Fi", 1755);
        }
    }

    public class Poem : IDisposable
    {
        private bool _disposed = false;
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }

        public Poem(string name, string author, string genre, int year)
        {
            Name = name;
            Author = author;
            Genre = genre;
            Year = year;
            Console.WriteLine($"Conructor Object '{Name}' created.");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Name: {Name} | Author: {Author} | Genre: {Genre} | Year: {Year}");
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
                    Console.WriteLine($"[Dispose] Managed resources for the movie '{Name}' sucsess free.");
                }


                _disposed = true;
            }
        }

        ~Poem()
        {
            Dispose(false);
            Console.WriteLine($"Destructor. Poem was deleted '{Name}'.");
        }
    }
}
