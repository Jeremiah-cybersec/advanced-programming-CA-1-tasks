namespace PeriodicTableApp
{
    /// <summary>
    /// Console app to look up the first 30 elements of the periodic table.
    /// Data structure used: Dictionary<int, Element> — O(1) lookup by atomic number.
    /// Data sourced from: https://pubchem.ncbi.nlm.nih.gov/element/ and
    ///                    https://en.wikipedia.org/wiki/Periodic_table
    /// </summary>
    class Program
    {
        // ── Build the dictionary of the first 30 elements ─────────────
        private static readonly Dictionary<int, Element> PeriodicTable = new()
        {
            { 1,  new Element(1,  "Hydrogen",   "H",  "Nonmetal",          1.008,   "Lightest and most abundant element in the universe; used in fuel cells and rocket fuel.") },
            { 2,  new Element(2,  "Helium",     "He", "Noble Gas",          4.003,   "Second lightest element; used in balloons, MRI cooling, and deep-sea diving tanks.") },
            { 3,  new Element(3,  "Lithium",    "Li", "Alkali Metal",       6.941,   "Lightest metal; widely used in rechargeable batteries and mood-stabilising medications.") },
            { 4,  new Element(4,  "Beryllium",  "Be", "Alkaline Earth Metal",9.012,  "Hard, lightweight metal; used in aerospace structures and X-ray windows.") },
            { 5,  new Element(5,  "Boron",      "B",  "Metalloid",         10.811,   "Used in glass, detergents, and as a neutron absorber in nuclear reactors.") },
            { 6,  new Element(6,  "Carbon",     "C",  "Nonmetal",          12.011,   "Basis of all organic life; exists as graphite, diamond, and fullerenes.") },
            { 7,  new Element(7,  "Nitrogen",   "N",  "Nonmetal",          14.007,   "Makes up ~78% of Earth's atmosphere; essential for amino acids and DNA.") },
            { 8,  new Element(8,  "Oxygen",     "O",  "Nonmetal",          15.999,   "Essential for respiration; most abundant element in Earth's crust.") },
            { 9,  new Element(9,  "Fluorine",   "F",  "Halogen",           18.998,   "Most electronegative element; used in toothpaste (fluoride) and Teflon.") },
            { 10, new Element(10, "Neon",       "Ne", "Noble Gas",         20.180,   "Inert gas used in neon lighting and high-voltage indicators.") },
            { 11, new Element(11, "Sodium",     "Na", "Alkali Metal",      22.990,   "Highly reactive metal; essential electrolyte in the human body (table salt).") },
            { 12, new Element(12, "Magnesium",  "Mg", "Alkaline Earth Metal",24.305, "Lightweight structural metal; vital for chlorophyll and hundreds of enzymes.") },
            { 13, new Element(13, "Aluminium",  "Al", "Post-transition Metal",26.982,"Most abundant metal in Earth's crust; widely used in packaging and aircraft.") },
            { 14, new Element(14, "Silicon",    "Si", "Metalloid",         28.086,   "Semiconductor foundation of modern electronics and solar panels.") },
            { 15, new Element(15, "Phosphorus", "P",  "Nonmetal",          30.974,   "Essential in DNA, RNA, and ATP; used in fertilisers and matches.") },
            { 16, new Element(16, "Sulfur",     "S",  "Nonmetal",          32.065,   "Yellow solid used in vulcanising rubber, gunpowder, and fungicides.") },
            { 17, new Element(17, "Chlorine",   "Cl", "Halogen",           35.453,   "Greenish toxic gas; used in water purification and PVC production.") },
            { 18, new Element(18, "Argon",      "Ar", "Noble Gas",         39.948,   "Third most abundant gas in the atmosphere; used in welding and light bulbs.") },
            { 19, new Element(19, "Potassium",  "K",  "Alkali Metal",      39.098,   "Essential mineral for nerve function and heart rhythm; found in bananas.") },
            { 20, new Element(20, "Calcium",    "Ca", "Alkaline Earth Metal",40.078, "Most abundant mineral in the human body; key component of bones and teeth.") },
            { 21, new Element(21, "Scandium",   "Sc", "Transition Metal",  44.956,   "Rare silvery metal; used in aerospace alloys and high-intensity lighting.") },
            { 22, new Element(22, "Titanium",   "Ti", "Transition Metal",  47.867,   "Strong, lightweight, corrosion-resistant; used in aircraft and medical implants.") },
            { 23, new Element(23, "Vanadium",   "V",  "Transition Metal",  50.942,   "Hard bluish-silver metal; used to strengthen steel alloys.") },
            { 24, new Element(24, "Chromium",   "Cr", "Transition Metal",  51.996,   "Shiny, corrosion-resistant metal; key component of stainless steel.") },
            { 25, new Element(25, "Manganese",  "Mn", "Transition Metal",  54.938,   "Brittle metal important in steel production and biological enzyme function.") },
            { 26, new Element(26, "Iron",       "Fe", "Transition Metal",  55.845,   "Most common element on Earth by mass; essential in haemoglobin and structural steel.") },
            { 27, new Element(27, "Cobalt",     "Co", "Transition Metal",  58.933,   "Hard magnetic metal; used in lithium-ion batteries and blue pigments.") },
            { 28, new Element(28, "Nickel",     "Ni", "Transition Metal",  58.693,   "Corrosion-resistant metal used in coins, stainless steel, and batteries.") },
            { 29, new Element(29, "Copper",     "Cu", "Transition Metal",  63.546,   "Excellent electrical conductor; used in wiring, plumbing, and electronics.") },
            { 30, new Element(30, "Zinc",       "Zn", "Transition Metal",  65.38,    "Bluish-white metal used in galvanising steel and as an essential trace element.") },
        };

        // ────────────────────────────────────────────────────────────
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("\n  ======================================");
            Console.WriteLine("    Periodic Table Explorer (Z = 1–30)");
            Console.WriteLine("  ======================================");
            Console.WriteLine("  Hi there! Happy to help!\n");

            bool continueLoop = true;

            while (continueLoop)
            {
                Console.Write("  Provide atomic number of the element (1–30): ");
                string? input = Console.ReadLine()?.Trim();

                if (int.TryParse(input, out int atomicNum) &&
                    PeriodicTable.TryGetValue(atomicNum, out Element? found))
                {
                    found.Display();
                }
                else
                {
                    Console.WriteLine($"\n  ⚠  '{input}' is not a valid atomic number in range 1–30.\n");
                }

                Console.Write("\n  Do you want to know more elements [y/n]? ");
                string? answer = Console.ReadLine()?.Trim().ToLower();

                if (answer == "n")
                {
                    continueLoop = false;
                    Console.WriteLine("\n  Thanks!\n");
                }
                else if (answer != "y")
                {
                    Console.WriteLine("  (Unrecognised input — continuing anyway.)\n");
                }
            }
        }
    }
}
