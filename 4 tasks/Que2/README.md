# Task 2 – Periodic Table Explorer (C#)

## How to Run
```bash
cd Que2
dotnet run
```

## Data Structure Used
`Dictionary<int, Element>` — keyed by atomic number for O(1) lookup.

## Sample Session
```
Hi there! Happy to help!

Provide atomic number of the element (1–30): 26
  ──────────────────────────────────────────────────────
  Atomic Number : 26
  Name          : Iron
  Symbol        : Fe
  Category      : Transition Metal
  Atomic Mass   : 55.845 u
  Info          : Most common element on Earth by mass...
  ──────────────────────────────────────────────────────

Do you want to know more elements [y/n]? n
Thanks!
```

## Sources
- https://en.wikipedia.org/wiki/Periodic_table
- https://pubchem.ncbi.nlm.nih.gov/element/
