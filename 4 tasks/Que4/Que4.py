"""
Que4.py  –  Book Scraper: books.toscrape.com (Travel Category)
--------------------------------------------------------------
a. Scrapes book names, ratings and prices from the travel category.
b. Stores the data in a CSV file (travel_books.csv).
c. Reads the CSV back and displays all records in the terminal.

Required modules (install if needed):
    pip install requests beautifulsoup4
"""

import requests
from bs4 import BeautifulSoup
import csv
import os

# ── Configuration ──────────────────────────────────────────────────────
URL      = "https://books.toscrape.com/catalogue/category/books/travel_2/index.html"
CSV_FILE = "travel_books.csv"

# Map word ratings to numbers
RATING_MAP = {
    "One": 1, "Two": 2, "Three": 3, "Four": 4, "Five": 5
}

# ── Part a: Scrape the webpage ─────────────────────────────────────────
def scrape_books(url: str) -> list[dict]:
    """
    Scrape book name, rating, and price from the given URL.
    Returns a list of dicts: [{"title": ..., "rating": ..., "price": ...}]
    """
    print(f"\n[SCRAPER] Fetching: {url}")
    headers = {"User-Agent": "Mozilla/5.0"}
    response = requests.get(url, headers=headers, timeout=10)
    response.raise_for_status()

    soup = BeautifulSoup(response.text, "html.parser")
    books_data = []

    articles = soup.select("article.product_pod")
    print(f"[SCRAPER] Found {len(articles)} books on the page.\n")

    for article in articles:
        # Title is in the <a> tag inside <h3>; full title is in the 'title' attribute
        title_tag = article.select_one("h3 > a")
        title = title_tag["title"] if title_tag else "N/A"

        # Rating: the <p> with class "star-rating" has a second class like "Three"
        rating_tag = article.select_one("p.star-rating")
        rating_word = rating_tag["class"][1] if rating_tag else "Zero"
        rating = RATING_MAP.get(rating_word, 0)

        # Price: inside <p class="price_color">
        price_tag = article.select_one("p.price_color")
        price = price_tag.get_text(strip=True) if price_tag else "N/A"

        books_data.append({
            "title":  title,
            "rating": rating,
            "price":  price
        })

    return books_data


# ── Part b: Save to CSV ────────────────────────────────────────────────
def save_to_csv(books: list[dict], filename: str):
    """Write book records to a CSV file."""
    with open(filename, mode='w', newline='', encoding='utf-8') as f:
        fieldnames = ["title", "rating", "price"]
        writer = csv.DictWriter(f, fieldnames=fieldnames)
        writer.writeheader()
        writer.writerows(books)

    print(f"[CSV] Data saved to: {os.path.abspath(filename)}")


# ── Part c: Read CSV and display in terminal ───────────────────────────
def display_from_csv(filename: str):
    """Read all records from the CSV and print them in a formatted table."""
    print(f"\n[CSV] Reading data from: {filename}")
    print("\n" + "=" * 70)
    print(f"  {'#':<4} {'Title':<42} {'Rating':<8} {'Price'}")
    print("=" * 70)

    with open(filename, mode='r', encoding='utf-8') as f:
        reader = csv.DictReader(f)
        for idx, row in enumerate(reader, start=1):
            stars = "★" * int(row["rating"]) + "☆" * (5 - int(row["rating"]))
            print(f"  {idx:<4} {row['title']:<42} {stars}  {row['price']}")

    print("=" * 70 + "\n")


# ── Entry point ────────────────────────────────────────────────────────
if __name__ == "__main__":
    # a. Scrape
    books = scrape_books(URL)

    # b. Save to CSV
    save_to_csv(books, CSV_FILE)

    # c. Display from CSV
    display_from_csv(CSV_FILE)

    print(f"[DONE] {len(books)} books processed. CSV file: {CSV_FILE}\n")
