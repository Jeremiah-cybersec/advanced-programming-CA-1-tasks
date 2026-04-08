using System.Collections.Generic;

namespace ChurrosApp
{
    /// <summary>
    /// Entry point — menu-driven Churros food truck console application.
    /// Data structure used: Queue<Order> to model the FIFO waiting line.
    /// </summary>
    class Program
    {
        // ── Menu items ───────────────────────────────────────────────
        private static readonly List<Churros> Menu = new()
        {
            new Churros("Churros with plain sugar",     6.00),
            new Churros("Churros with cinnamon sugar",  6.00),
            new Churros("Churros with chocolate sauce", 8.00),
            new Churros("Churros with Nutella",         8.00),
        };

        // ── Order queue (FIFO data structure) ────────────────────────
        private static readonly Queue<Order> OrderQueue = new();

        // ────────────────────────────────────────────────────────────
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool running = true;

            while (running)
            {
                PrintMainMenu();
                string input = Console.ReadLine()?.Trim() ?? "";

                switch (input)
                {
                    case "1":
                        PlaceOrder();
                        break;
                    case "2":
                        DeliverOrder();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("\n  Goodbye! Thanks for visiting Delicious Churros! 🎉\n");
                        break;
                    default:
                        Console.WriteLine("\n  ⚠  Invalid option. Please choose 1, 2, or 0.\n");
                        break;
                }
            }
        }

        // ── Print header + main menu ─────────────────────────────────
        private static void PrintMainMenu()
        {
            Console.WriteLine("\n" + new string('-', 60));
            Console.WriteLine("  🍩  Delicious Churros");
            Console.WriteLine(new string('-', 60));
            foreach (var item in Menu)
                Console.WriteLine($"    • {item}");
            Console.WriteLine(new string('-', 60));

            if (OrderQueue.Count > 0)
                Console.WriteLine($"  📋 Orders in queue: {OrderQueue.Count}");

            Console.WriteLine("\n  Choose your option:");
            Console.WriteLine("    1. Place order");
            Console.WriteLine("    2. Deliver order");
            Console.WriteLine("    0. Exit");
            Console.Write("\n  > ");
        }

        // ── Place a new order ────────────────────────────────────────
        private static void PlaceOrder()
        {
            Console.WriteLine("\n  --- Place Order ---");
            Console.WriteLine("  Select item:");
            for (int i = 0; i < Menu.Count; i++)
                Console.WriteLine($"    {i + 1}. {Menu[i]}");
            Console.Write("\n  Item number > ");

            if (!int.TryParse(Console.ReadLine()?.Trim(), out int choice) ||
                choice < 1 || choice > Menu.Count)
            {
                Console.WriteLine("\n  ⚠  Invalid item selection.");
                return;
            }

            Console.Write("  Quantity > ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int qty) || qty <= 0)
            {
                Console.WriteLine("\n  ⚠  Invalid quantity. Must be 1 or more.");
                return;
            }

            Churros selected = Menu[choice - 1];
            Order newOrder = new Order(selected.Name, qty, selected.Price);

            newOrder.PlaceOrder();
            double paid = newOrder.PayBill();
            Console.WriteLine($"  ✅ €{paid:F2} accepted. Your order number is #{newOrder.OrderNo}. Please wait!");

            OrderQueue.Enqueue(newOrder);
        }

        // ── Deliver the next order in the queue ──────────────────────
        private static void DeliverOrder()
        {
            if (OrderQueue.Count == 0)
            {
                Console.WriteLine("\n  ℹ  No orders in the queue at the moment.");
                return;
            }

            Order next = OrderQueue.Dequeue();
            next.CollectOrder();
            Console.WriteLine($"\n  📣 Calling Order #{next.OrderNo} — please collect your Churros!");
            Console.WriteLine($"  Orders remaining in queue: {OrderQueue.Count}");
        }
    }
}
