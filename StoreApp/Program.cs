namespace StoreApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, StoreApp!");
            Console.WriteLine("______________________________________________________");

            Console.WriteLine("--- Create Stores ---");
            Store groceryStore = new Store("Freshness", "1 Centralna St.", "Grocery");
            Store clothingStore = new Store("Style", "Nezalezhnosti Ave.", "Clothing");

            Console.WriteLine($"\nInfo about Store 1: Name - {groceryStore.Name}, Adress - {groceryStore.Address}, Type - {groceryStore.Type}");

            Console.WriteLine("\n--- Calling the Dispose method manually ---");
            groceryStore.Dispose();

            Console.WriteLine("\n--- Block using ---");

            using (Store hardwareStore = new Store("Master", "5 Budivelnykiv St.", "Hardware"))
            {
                Console.WriteLine($"Store Opened: {hardwareStore.Name} ({hardwareStore.Type})");
            }

            Console.WriteLine("\n--- Testing the wrong store type ---");

            try
            {
                Store fakeStore = new Store("Books for You", "3 Franka st.", "Books");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }

            Console.WriteLine("\nThe application closed.");

            CreateStore();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        static void CreateStore()
        {
            Store store2 = new Store("Sport Shoes", "11 Petlyury st.", "Shoes");
        }
    }

    public class Store : IDisposable
    {
        private bool _disposed = false;
        private string _type;

        public static readonly string[] AllowedTypes = { "Grocery", "Hardware", "Clothing", "Shoes" };

        public string Name { get; set; }
        public string Address { get; set; }
        public string Type
        {
            get { return _type; }
            set
            {
                if (Array.Exists(AllowedTypes, t => t.Equals(value, StringComparison.OrdinalIgnoreCase)))
                {
                    _type = value;
                }
                else
                {
                    throw new ArgumentException($"Invalid store type. Allowed types are: {string.Join(", ", AllowedTypes)}");
                }
            }
        }
        public Store(string name, string address, string type)
        {
            Name = name;
            Address = address;
            Type = type;
            Console.WriteLine($"[Constructor] Store '{Name}' OPENED.");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Console.WriteLine($"[Dispose] Releasing managed resources for the store '{Name}'.");
            }

            _disposed = true;
            Console.WriteLine($"[Dispose] Store '{Name}' closed and Garbage Cleaned.\n");
        }

        ~Store()
        {
            Dispose(false);
            Console.WriteLine($"Destructor. Store was deleted '{Name}'.");
        }
    }
}
