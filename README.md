# 🛒 ShopInventoryApp — Smart Retail & POS System

![C#](https://img.shields.io/badge/C%23-.NET%2010.0-blue?style=for-the-badge&logo=csharp)
![SQL Server](https://img.shields.io/badge/Database-MS%20SQL%20Server-red?style=for-the-badge&logo=microsoftsqlserver)
![UI](https://img.shields.io/badge/UI-Windows%20Forms-lightgrey?style=for-the-badge)

თანამედროვე, სწრაფი და მრავალფუნქციური **Desktop POS (Point of Sale)** და საწყობის მართვის სისტემა, შექმნილი C# (.NET 10.0) და MS SQL Server-ის ბაზაზე.

---

## 🌟 ძირითადი ფუნქციონალი (Key Features)

- 📦 **მარაგების მართვა (Inventory Management):** პროდუქტების დამატება, რედაქტირება, წაშლა, თვითღირებულებისა და გასაყიდი ფასების კონტროლი, ნაშთების მონიტორინგი.
- 💳 **სწრაფი გაყიდვები (Sales & Barcode Scanning):** შტრიხკოდის სკანერის მხარდაჭერა, კალათის დინამიური დათვლა და ტრანზაქციის უსაფრთხო გატარება (SQL Transactions).
- 🧾 **PDF ჩეკების გენერაცია (PDF Receipts):** ყოველი გაყიდვისას ავტომატურად იქმნება დეტალური PDF ჩეკი.
- 📧 **Gmail SMTP ინტეგრაცია:** მყიდველის ელ-ფოსტის მითითებისას PDF ჩეკი ავტომატურად იგზავნება Gmail-ის საშუალებით.
- 📊 **ფინანსური რეპორტინგი (Financial Dashboard):** 
  - შემოსავლების, გაყიდვების რაოდენობისა და **სუფთა მოგების (Net Profit)** ავტომატური გამოთვლა თარიღების ფილტრით.
- 📥 **Excel Export:** რეპორტების 1 დაჭერით ჩამოტვირთვა `.xlsx` ფორმატში `ClosedXML` ბიბლიოთეკის გამოყენებით.
- 🖥️ **თანამედროვე MDI/Panel UI:** მოქნილი ინტერფეისი დინამიური გვერდების გადართვით (`MainForm` Panel Navigation).
  
- ---

## 🛠️ ტექნოლოგიური სტეკი (Tech Stack)

* **Language:** C# (.NET 10.0 / WinForms)
* **Database:** Microsoft SQL Server
* **Libraries & Packages:**
  * `Microsoft.Data.SqlClient` — SQL Server-თან კავშირისთვის.
  * `iTextSharp` / `PdfSharp` — PDF ჩეკების გენერაციისთვის.
  * `ClosedXML` — Excel რეპორტების ექსპორტისთვის.
  * `System.Net.Mail` — Gmail SMTP ავტომატიზაციისთვის.

## 📸 ეკრანის სქრინშოტები (Screenshots)
<img width="2233" height="996" alt="Screenshot 2026-08-13 204856" src="https://github.com/user-attachments/assets/3af25972-90bc-497b-ab60-297f8cff6500" />
<img width="1970" height="900" alt="Screenshot 2026-08-13 211107" src="https://github.com/user-attachments/assets/b50fb53c-b74e-4086-b1d0-c67caf7410c2" />
<img width="1977" height="863" alt="Screenshot 2026-08-10 141700" src="https://github.com/user-attachments/assets/aac45afc-c411-45a8-be68-bd603e9cd24d" />


