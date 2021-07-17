using System;
using System.Linq;

namespace FakestoreDatabase
{
    class Program
    {
        static void Main(string[] args)
        {

            //inventory

            Person John = new Person("John");

            Console.WriteLine("Hello {0}, Welcome to fakestore.\n", John.name);
            menu();
        }

        public static void inventory()
        {
            using (var db = new StoreContext())
            {
                Console.WriteLine("Updated Inventory");
                db.Add(new Item { Name = "iMac", Price = 799.99 });
                db.Add(new Item { Name = "Mac Mini", Price = 399.99 });
                db.Add(new Item { Name = "MacBook Air", Price = 999.99 });
                db.Add(new Item { Name = "MacBook Pro", Price = 1299.99 });
                db.Add(new Item { Name = "Sanyo TV", Price = 499.99 });
                db.Add(new Item { Name = "LG TV", Price = 399.99 });
                db.Add(new Item { Name = "Sony TV", Price = 649.99 });
                db.SaveChanges();
            }
        }

        public static void showInventory()
        {
            using (var db = new StoreContext())
            {
                Console.WriteLine("Item ID |      Name       |    Price ");

                for(int i = 4; i <= 13; i++)
                {
                    Item item = db.Items.Where<Item>(b => b.ItemId == i).First();
                    Console.WriteLine("   {0}      {1}              {2}", item.ItemId, item.Name, item.Price);
                }
            }
        }

        public static void addItems()
        {
            using ( var db = new StoreContext())
            {
                Console.Write("Item ID: \n");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Quantity: \n");
                int quantity = Convert.ToInt32(Console.ReadLine());
                Item item = db.Items.Where<Item>(b => b.ItemId == id).First();
                double taxes = (item.Price * quantity) * (8.25 / 100);

                db.Add(new Order { Product_Name = item.Name, Quantity = quantity, Subtotal = item.Price * quantity + taxes });
                db.SaveChanges();
                Console.WriteLine("Added Item to Cart\n");

            }
        }

        public static void cart()
        {
            using (var db = new StoreContext())
            {
                Console.WriteLine("Order ID |      Name       |    Quantity      |    Subtotal ");

                for (int i = 3; i <= 5; i++)
                {
                    Order order = db.Orders.Where<Order>(b => b.OrderId == i).First();
                    Console.WriteLine("   {0}      {1}              {2}           {3}", order.OrderId, order.Product_Name, order.Quantity, order.Subtotal);
                }
            }
        }

        public static void total()
        {
            using (var db = new StoreContext())
            {
                double total = 0.0;
                for (int i = 3; i <= 5; i++)
                {
                    Order order = db.Orders.Where<Order>(b => b.OrderId == i).First();
                    total += order.Subtotal;
                }
                Console.WriteLine("Thank you for your purchase.");
                Console.WriteLine("Your Subtotal was: {0}", total);
            }
        }

        public static void menu()
        {
            bool type = true;
            while (type)
            {
                Console.WriteLine("1 = Inventory");
                Console.WriteLine("2 = Add to Cart");
                Console.WriteLine("3 = Cart");
                Console.WriteLine("4 = Exit");
                Console.Write("Choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        showInventory();
                        break;
                    case "2":
                        addItems();
                        break;
                    case "3":
                        cart();
                        break;
                    case "4":
                        total();
                        type = false;
                        break;
                }
            }
        }
    }
}
