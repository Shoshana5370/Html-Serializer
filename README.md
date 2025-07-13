# 🧩 HTML Serializer

A foundational tool for parsing and querying HTML using C#.  
This project implements two main components:
- **Html Serializer** – parses raw HTML into a structured object tree.
- **Html Query** – supports CSS-like querying over the object tree.

This infrastructure can later be extended into a full-fledged **Crawler**, **Scraper**, or **HTML Analyzer**.

---

## ✨ Features

- Fetch HTML content from a web URL.
- Parse raw HTML into a structured object tree (`HtmlElement`).
- Extract tag names, attributes, classes, IDs, and inner HTML.
- Support for self-closing and standard tags.
- Perform queries using CSS-style selectors (tag, id, class, nested selectors).
- Prevent duplicate matches using `HashSet`.

---

## 📦 Technologies

- C#
- .NET
- HttpClient
- Regex
- System.Text.Json
- Singleton Pattern
- Queue for tree traversal
- HashSet for uniqueness
