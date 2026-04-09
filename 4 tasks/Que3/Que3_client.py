"""
Que3_client.py  –  EasyDrive Registration Client
--------------------------------------------------
• Collects customer registration information from the console.
• Sends it to the EasyDrive server over TCP (connection-oriented).
• Displays the registration number returned by the server.

Run second (after server is running):  python Que3_client.py
"""

import socket
import json
import os

# ── Configuration (must match server) ─────────────────────────────────
HOST = '127.0.0.1'
PORT = 65432

# ── Input helpers ──────────────────────────────────────────────────────
def prompt(label: str, required: bool = True) -> str:
    """Prompt the user and enforce non-empty input for required fields."""
    while True:
        value = input(f"  {label}: ").strip()
        if value:
            return value
        if not required:
            return ""
        print("  ⚠  This field is required. Please enter a value.\n")


def get_license_path() -> str:
    """Ask for a driving licence document filename/path."""
    print("\n  Note: Enter the filename of your driving licence document")
    print("        (e.g. license.pdf or D:\\Documents\\license.jpg)")
    while True:
        path = input("  Driving Licence Document: ").strip()
        if path:
            return path
        print("  ⚠  Driving licence document is required.\n")


# ── Collect customer info ──────────────────────────────────────────────
def collect_customer_info() -> dict:
    print("\n" + "=" * 55)
    print("   🚗  EasyDrive – Customer Registration")
    print("=" * 55)
    print("   'Drive by the hour, any day, any time'")
    print("=" * 55)
    print("\n  Please provide the following details:\n")

    name             = prompt("Full Name")
    address          = prompt("Address")
    pps_number       = prompt("PPS Number")
    license_document = get_license_path()

    return {
        "name":             name,
        "address":          address,
        "pps_number":       pps_number,
        "license_document": license_document
    }


# ── Send data to server ────────────────────────────────────────────────
def register_customer(data: dict):
    print(f"\n  [CLIENT] Connecting to EasyDrive Server at {HOST}:{PORT}...")

    try:
        # AF_INET = IPv4, SOCK_STREAM = TCP
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as client_socket:
            client_socket.connect((HOST, PORT))
            print("  [CLIENT] Connection established successfully.\n")

            # Serialise and send with sentinel
            payload = json.dumps(data).encode('utf-8') + b"<END>"
            client_socket.sendall(payload)
            print("  [CLIENT] Registration data sent to server. Awaiting response...")

            # Receive response
            response_raw = client_socket.recv(1024)
            response = json.loads(response_raw.decode('utf-8'))

            if response.get("status") == "success":
                reg_no = response.get("registration_no")
                print("\n" + "=" * 55)
                print("  ✅  Registration Successful!")
                print(f"  Your Registration Number: {reg_no}")
                print("  Please keep this number for future rentals.")
                print("=" * 55 + "\n")
            else:
                print(f"\n  ❌ Registration failed: {response.get('message')}\n")

    except ConnectionRefusedError:
        print("\n  ❌ Could not connect to the server.")
        print("     Make sure Que3_server.py is running first.\n")
    except Exception as e:
        print(f"\n  ❌ An error occurred: {e}\n")


# ── Entry point ────────────────────────────────────────────────────────
if __name__ == "__main__":
    customer_data = collect_customer_info()

    print("\n  --- Confirm your details ---")
    for key, value in customer_data.items():
        print(f"  {key.replace('_', ' ').title():<25}: {value}")

    confirm = input("\n  Submit registration? [y/n]: ").strip().lower()
    if confirm == 'y':
        register_customer(customer_data)
    else:
        print("\n  Registration cancelled.\n")
