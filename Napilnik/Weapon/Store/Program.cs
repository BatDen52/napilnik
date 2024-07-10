using System.Drawing;

namespace HW2.Store
{
    class Program
    {
        private static void Main()
        {
            Product iPhone12 = new Product("IPhone 12");
            Product iPhone11 = new Product("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Deliver(iPhone12, 10);
            warehouse.Deliver(iPhone11, 1);

            Console.WriteLine("Warehouse Info:");
            warehouse.ShowInfo();

            Cart cart = shop.CreateCart();
            cart.Add(iPhone12, 4);

            try
            {
                cart.Add(iPhone11, 3);
            }
            catch (Exception exeption)
            {
                WriteError(exeption);
            }

            Console.WriteLine("\nCart Info:");
            cart.ShowInfo();

            Console.Write("\nPaylink ");
            Console.WriteLine(cart.Order().Paylink);

            try
            {
                cart.Add(iPhone12, 9);
            }
            catch (Exception exeption)
            {
                WriteError(exeption);
            }
        }

        private static void WriteError(Exception exeption)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(exeption.Message);
            Console.ForegroundColor = defaultColor;
        }
    }

    public class Order
    {
        public Order(IReadOnlyDictionary<Product, Cell> products)
        {
            if (products == null)
                throw new ArgumentNullException();

            Paylink = "purchased: " + products.Count.ToString();
        }

        public string Paylink { get; internal set; }
    }

    public class Shop
    {
        private Warehouse _warehouse;

        public Shop(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException();

            _warehouse = warehouse;
        }

        public Cart CreateCart() => new Cart(_warehouse);
    }

    public class Product
    {
        private string _title;

        public Product(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException();

            _title = title;
        }

        public override string ToString() => _title;
    }

    public class Storage
    {
        private Dictionary<Product, Cell> _products = new();

        protected IReadOnlyDictionary<Product, Cell> Products => _products;

        public bool HasEnoughProduct(Product product, int neededCount)
        {
            if (product == null)
                throw new ArgumentNullException();

            if (neededCount < 0)
                throw new ArgumentOutOfRangeException();

            return _products.TryGetValue(product, out Cell cell) && cell.Count >= neededCount;
        }

        public void TakeProducts(Product product, int neededCount)
        {
            if (HasEnoughProduct(product, neededCount) == false)
                throw new InvalidOperationException();

            _products[product].ReduceCount(neededCount);

            if (_products[product].Count == 0)
                _products.Remove(product);
        }

        public void ShowInfo()
        {
            foreach (Cell cell in _products.Values)
                Console.WriteLine(cell);
        }

        protected void Clear() => _products.Clear();

        protected void Add(Product product, int count)
        {
            if (product == null)
                throw new ArgumentNullException();

            if (count < 0)
                throw new ArgumentOutOfRangeException();

            if (_products.ContainsKey(product))
                _products[product].IncreaseCount(count);
            else
                _products.Add(product, new Cell(product, count));
        }
    }

    public class Cart : Storage
    {
        public Cart(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException();

            Warehouse = warehouse;
        }

        public Warehouse Warehouse { get; }

        public new void Add(Product product, int count)
        {
            if (Warehouse.HasEnoughProduct(product, count) == false)
                throw new InvalidOperationException();

            base.Add(product, count);
        }

        public Order Order() => new Order(Products);

        public void Cancel()
        {
            foreach (Cell cell in Products.Values)
                Warehouse.Deliver(cell.Product, cell.Count);

            Clear();
        }
    }

    public class Warehouse : Storage
    {
        public void Deliver(Product product, int count) => Add(product, count);
    }

    public class Cell
    {
        public Cell(Product product, int count)
        {
            if (product == null)
                throw new ArgumentNullException();

            if (count < 0)
                throw new ArgumentOutOfRangeException();

            Product = product;
            Count = count;
        }

        public Product Product { get; }
        public int Count { get; private set; }

        public void IncreaseCount(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException();

            Count += count;
        }

        public void ReduceCount(int count)
        {
            if (count < 0 || Count < count)
                throw new ArgumentOutOfRangeException();

            Count -= count;
        }

        public override string ToString() => $"{Product} - {Count}";
    }
}