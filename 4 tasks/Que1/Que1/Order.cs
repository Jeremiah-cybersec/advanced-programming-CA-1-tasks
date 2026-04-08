namespace ChurrosApp
{
    /// <summary>
    /// Represents a single customer order.
    /// Demonstrates: Classes & Objects, Encapsulation, Properties,
    ///               Access Modifiers, Constructor.
    /// </summary>
    public class Order
    {
        // ── Private backing fields (Encapsulation) ───────────────────
        private static int _nextOrderNo = 1;   // auto-incrementing counter

        private string  _orderDetails;
        private int     _quantity;
        private double  _bill;
        private int     _orderNo;
        private bool    _isCollected;

        // ── Public Properties ────────────────────────────────────────
        public string OrderDetails
        {
            get => _orderDetails;
            private set => _orderDetails = value;
        }

        public int Quantity
        {
            get => _quantity;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Quantity must be greater than zero.");
                _quantity = value;
            }
        }

        public double Bill
        {
            get => _bill;
            private set => _bill = value;
        }

        public int OrderNo
        {
            get => _orderNo;
            private set => _orderNo = value;
        }

        public bool IsCollected
        {
            get => _isCollected;
            private set => _isCollected = value;
        }

        // ── Constructor ──────────────────────────────────────────────
        public Order(string orderDetails, int quantity, double unitPrice)
        {
            OrderDetails = orderDetails;
            Quantity     = quantity;
            Bill         = unitPrice * quantity;
            OrderNo      = _nextOrderNo++;
            IsCollected  = false;
        }

        // ── Methods ──────────────────────────────────────────────────

        /// <summary>Places the order and confirms it to the console.</summary>
        public void PlaceOrder()
        {
            Console.WriteLine($"\n  ✔  Order #{OrderNo} placed: {Quantity}x {OrderDetails}");
            Console.WriteLine($"     Total bill: €{Bill:F2}");
        }

        /// <summary>Simulates payment and returns the total bill amount.</summary>
        public double PayBill()
        {
            Console.WriteLine($"\n  💳 Payment of €{Bill:F2} received for Order #{OrderNo}. Thank you!");
            return Bill;
        }

        /// <summary>Marks the order as collected.</summary>
        public void CollectOrder()
        {
            IsCollected = true;
            Console.WriteLine($"\n  🛍  Order #{OrderNo} ({OrderDetails}) collected. Enjoy!");
        }

        public override string ToString() =>
            $"Order #{OrderNo} | {Quantity}x {OrderDetails} | €{Bill:F2} | " +
            $"{(IsCollected ? "Collected" : "Waiting")}";

        // ── Reset helper (used by unit tests only) ───────────────────
        internal static void ResetCounter(int value = 1) => _nextOrderNo = value;
    }
}
