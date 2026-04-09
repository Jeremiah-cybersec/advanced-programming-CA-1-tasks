# Task 3 – EasyDrive Car Rental (Python TCP Client-Server)

## How to Run

### Step 1 — Start the server (Terminal 1)
```bash
cd Que3
python Que3_server.py
```

### Step 2 — Run the client (Terminal 2)
```bash
cd Que3
python Que3_client.py
```

## What Happens
1. Client collects: Name, Address, PPS Number, Driving Licence filename
2. Client sends data as JSON over TCP to the server
3. Server receives, stores in SQLite (`easydrive.db`), generates a unique reg number (e.g. `ED-3F7A1B2C`)
4. Server sends reg number back to client
5. Client displays the registration number to the user

## Protocol
- **TCP** (connection-oriented) via Python `socket` module
- `socket.AF_INET` + `socket.SOCK_STREAM`
- Sentinel `<END>` marks end of transmission

## Database
- SQLite (`easydrive.db`) — disk-persistent relational database
- Table: `customers` (id, registration_no, name, address, pps_number, license_document, registered_at)

## No external dependencies — uses Python standard library only.
