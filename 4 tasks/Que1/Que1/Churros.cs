namespace ChurrosApp
{
    /// <summary>
    /// Represents a Churros menu item with a name and price.
    /// </summary>
    public class Churros
    {
        // ── Properties ──────────────────────────────────────────────
        public string Name { get; private set; }
        public double Price { get; private set; }

        // ── Constructor ─────────────────────────────────────────────
        public Churros(string name, double price)
        {
            Name  = name;
            Price = price;
        }

        // ── Methods ─────────────────────────────────────────────────
        public override string ToString() => $"{Name,-35} €{Price:F2}";
    }
}
