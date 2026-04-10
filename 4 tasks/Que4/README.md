# Task 4 – Book Scraper (Python)

## Install Dependencies
```bash
pip install requests beautifulsoup4
```

## How to Run
```bash
cd Que4
python Que4.py
```

## What It Does
a. Scrapes book titles, star ratings (1–5), and prices from:
   https://books.toscrape.com/catalogue/category/books/travel_2/index.html

b. Saves results to `travel_books.csv`

c. Reads the CSV back and displays a formatted table in the terminal

## Modules Used
| Module | Purpose |
|---|---|
| `requests` | HTTP GET request to fetch the webpage |
| `beautifulsoup4` | HTML parsing and element selection |
| `csv` | Reading and writing CSV files |
| `os` | Show absolute file path |

## Output Files
- `travel_books.csv` — contains columns: title, rating, price
