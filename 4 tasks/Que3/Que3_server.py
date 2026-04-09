"""
Que3_server.py  –  EasyDrive Registration Server
--------------------------------------------------
• Listens for TCP connections from the client.
• Receives customer registration data (JSON).
• Stores it in a local SQLite database (easydrive.db).
• Generates a unique registration number and sends it back.

Run first:  python Que3_server.py
"""

import socket
import json
import sqlite3
import uuid
import datetime
import os

# ── Configuration ─────────────────────────────────────────────────────
HOST = '127.0.0.1'
PORT = 65432
DB_FILE = 'easydrive.db'

# ── Database setup ─────────────────────────────────────────────────────
def init_db():
    """Create the customers table if it doesn't already exist."""
    conn = sqlite3.connect(DB_FILE)
    cursor = conn.cursor()
    cursor.execute('''
        CREATE TABLE IF NOT EXISTS customers (
            id               INTEGER PRIMARY KEY AUTOINCREMENT,
            registration_no  TEXT    NOT NULL UNIQUE,
            name             TEXT    NOT NULL,
            address          TEXT    NOT NULL,
            pps_number       TEXT    NOT NULL,
            license_document TEXT    NOT NULL,
            registered_at    TEXT    NOT NULL
        )
    ''')
    conn.commit()
    conn.close()
    print(f"[DB] Database initialised → {os.path.abspath(DB_FILE)}")


def save_customer(data: dict) -> str:
    """
    Insert a customer record into the database.
    Returns the generated registration number.
    """
    reg_no = "ED-" + str(uuid.uuid4()).upper()[:8]   # e.g. ED-3F7A1B2C
    timestamp = datetime.datetime.now().isoformat(sep=' ', timespec='seconds')

    conn = sqlite3.connect(DB_FILE)
    cursor = conn.cursor()
    cursor.execute('''
        INSERT INTO customers
            (registration_no, name, address, pps_number, license_document, registered_at)
        VALUES (?, ?, ?, ?, ?, ?)
    ''', (
        reg_no,
        data['name'],
        data['address'],
        data['pps_number'],
        data['license_document'],
        timestamp
    ))
    conn.commit()
    conn.close()
    return reg_no


# ── Server main ────────────────────────────────────────────────────────
def start_server():
    init_db()

    # AF_INET = IPv4, SOCK_STREAM = TCP (connection-oriented protocol)
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as server_socket:
        server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        server_socket.bind((HOST, PORT))
        server_socket.listen(5)
        print(f"[SERVER] EasyDrive Server listening on {HOST}:{PORT}")
        print("[SERVER] Waiting for client connections...\n")

        while True:
            conn, addr = server_socket.accept()
            with conn:
                print(f"[SERVER] Connected by {addr}")

                # Receive data in chunks
                raw_data = b""
                while True:
                    chunk = conn.recv(4096)
                    if not chunk:
                        break
                    raw_data += chunk
                    if b"<END>" in raw_data:        # sentinel sent by client
                        raw_data = raw_data.replace(b"<END>", b"")
                        break

                if not raw_data:
                    print("[SERVER] Empty payload received. Ignoring.")
                    continue

                try:
                    customer_data = json.loads(raw_data.decode('utf-8'))
                    print(f"[SERVER] Received registration for: {customer_data.get('name', 'Unknown')}")

                    reg_no = save_customer(customer_data)
                    print(f"[SERVER] Saved to DB. Registration No: {reg_no}")

                    # Send registration number back to client
                    response = json.dumps({"status": "success", "registration_no": reg_no})
                    conn.sendall(response.encode('utf-8'))

                except (json.JSONDecodeError, KeyError) as e:
                    print(f"[SERVER] Error processing data: {e}")
                    error_resp = json.dumps({"status": "error", "message": str(e)})
                    conn.sendall(error_resp.encode('utf-8'))


if __name__ == "__main__":
    start_server()
