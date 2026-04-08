# Task 1 – Delicious Churros (C#)

## How to Run

### Prerequisites
- .NET 8 SDK — download from https://dotnet.microsoft.com/download

### Run the Console App
```bash
cd Que1
dotnet run
```

### Run the Unit Tests
```bash
cd Que1.Tests
dotnet test
```
Or from the solution root:
```bash
dotnet test Que1.sln
```

---

## Project Structure

```
Que1/
├── Que1.sln                  ← Solution file (open in Visual Studio)
├── Que1/
│   ├── Que1.csproj
│   ├── Churros.cs            ← Churros menu item class
│   ├── Order.cs              ← Order class (OOP concepts)
│   └── Program.cs            ← Menu-driven entry point
└── Que1.Tests/
    ├── Que1.Tests.csproj
    └── OrderTests.cs         ← xUnit tests for PayBill()
```

---

## OOP Concepts Demonstrated

| Concept           | Where                                      |
|-------------------|--------------------------------------------|
| Classes & Objects | `Churros`, `Order`, `Program`              |
| Encapsulation     | Private backing fields in `Order`          |
| Properties        | `Name`, `Price`, `OrderNo`, `Bill`, etc.   |
| Access Modifiers  | `public`, `private`, `internal`            |
| Constructor       | Both `Churros(name, price)` and `Order(…)` |

## Data Structure Used
`Queue<Order>` — models the real-world FIFO waiting line at the food truck.
Customers join at the back (`Enqueue`) and are served from the front (`Dequeue`).

## Unit Tests (xUnit)
`OrderTests.cs` contains 5 tests covering:
- Single item total
- Multiple quantity total
- All price points (€6 and €8 items)
- Invalid quantity throws `ArgumentException`
