namespace PeriodicTableApp
{
    /// <summary>
    /// Represents a single element from the periodic table.
    /// </summary>
    public class Element
    {
        // ── Properties ───────────────────────────────────────────────
        public int    AtomicNumber { get; private set; }
        public string Name        { get; private set; }
        public string Symbol      { get; private set; }
        public string Category    { get; private set; }
        public double AtomicMass  { get; private set; }
        public string Description { get; private set; }

        // ── Constructor ──────────────────────────────────────────────
        public Element(int atomicNumber, string name, string symbol,
                       string category, double atomicMass, string description)
        {
            AtomicNumber = atomicNumber;
            Name         = name;
            Symbol       = symbol;
            Category     = category;
            AtomicMass   = atomicMass;
            Description  = description;
        }

        // ── Display ──────────────────────────────────────────────────
        public void Display()
        {
            Console.WriteLine($"\n  {'─',0}{'─',0}" + new string('─', 50));
            Console.WriteLine($"  Atomic Number : {AtomicNumber}");
            Console.WriteLine($"  Name          : {Name}");
            Console.WriteLine($"  Symbol        : {Symbol}");
            Console.WriteLine($"  Category      : {Category}");
            Console.WriteLine($"  Atomic Mass   : {AtomicMass} u");
            Console.WriteLine($"  Info          : {Description}");
            Console.WriteLine("  " + new string('─', 50));
        }
    }
}
