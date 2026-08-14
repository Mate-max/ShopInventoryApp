using System.Net;
using System.Net.Mail;
using Microsoft.Data.SqlClient;
// iText 7 - PDF ბიბლიოთეკები
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;

namespace ShopInventoryApp
{
    public partial class SalesForm : Form
    {
        public SalesForm()
        {
            InitializeComponent();
        }

        private string connectionString = @"Server=.\SQLEXPRESS;Database=ShopInventoryDB;Trusted_Connection=True;TrustServerCertificate=True;";


        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            // როცა სკანერი შტრიხკოდს დაასკანერებს, ის ავტომატურად აჭერს Enter-ს
            if (e.KeyCode == Keys.Enter)
            {
                string barcode = txtBarcode.Text.Trim();

                if (!string.IsNullOrEmpty(barcode))
                {
                    AddProductToCart(barcode);
                    txtBarcode.Clear(); // ველის გასუფთავება შემდეგი სკანირებისთვის
                }

                e.SuppressKeyPress = true; // „ბიპ“ ხმის გათიშვა Enter-ზე
            }
        }

        private void AddProductToCart(string barcode)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // ამოგვაქვს ნაშთიც (Quantity)
                string query = "SELECT ProductID, ProductName, SellPrice, Quantity FROM Products WHERE BarCode = @BarCode";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BarCode", barcode);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int productId = Convert.ToInt32(reader["ProductID"]);
                    string productName = reader["ProductName"]?.ToString() ?? string.Empty;
                    decimal price = Convert.ToDecimal(reader["SellPrice"]);
                    int stockQuantity = Convert.ToInt32(reader["Quantity"]); // არსებული ნაშთი ბაზაში

                    // 1. ვამოწმებთ, უკვე არის თუ არა კალათაში
                    foreach (DataGridViewRow row in dgvCart.Rows)
                    {
                        if (!row.IsNewRow && row.Cells[1].Value?.ToString() == barcode)
                        {
                            int currentCartQty = Convert.ToInt32(row.Cells[3].Value);

                            // ⚠️ შემოწმება: კალათის რაოდენობა + 1 აჭარბებს თუ არა ბაზის ნაშთს?
                            if (currentCartQty + 1 > stockQuantity)
                            {
                                MessageBox.Show($"მარაგში არ არის საკმარისი რაოდენობა! (ხელმისაწვდომია: {stockQuantity} ცალი)",
                                                "მარაგი ამოწურულია", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            row.Cells[3].Value = currentCartQty + 1;
                            row.Cells[5].Value = (currentCartQty + 1) * price;
                            CalculateTotal();
                            return;
                        }
                    }

                    // 2. თუ ახალი ნივთია, ვამოწმებთ საერთოდ არის თუ არა ბაზაში 1 ცალი მაინც
                    if (stockQuantity < 1)
                    {
                        MessageBox.Show("პროდუქტის მარაგი ამოწურულია (0 ცალი)!", "გაფრთხილება", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // თუ მარაგი არის, ვამატებთ კალათაში
                    dgvCart.Rows.Add(productId, barcode, productName, 1, price, price);
                    CalculateTotal();
                }
                else
                {
                    MessageBox.Show("პროდუქტი ამ შტრიხკოდით ბაზაში ვერ მოიძებნა!", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                // მე-6 სვეტი (ინდექსი 5) არის ჯამი
                if (!row.IsNewRow && row.Cells[5].Value != null)
                {
                    if (decimal.TryParse(row.Cells[5].Value?.ToString(), out decimal rowTotal))
                    {
                        total += rowTotal;
                    }
                }
            }

            lblTotal.Text = $"{total:N2} ₾";
        }

        private void dgvCart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // ვამოწმებთ, რომ ცვლილება მოხდა "Quantity" (რაოდენობის) სვეტში
            if (e.RowIndex >= 0 && dgvCart.Columns[e.ColumnIndex].Name == "Quantity")
            {
                DataGridViewRow row = dgvCart.Rows[e.RowIndex];

                if (row.Cells["Quantity"].Value != null && row.Cells["Price"].Value != null)
                {
                    if (int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int qty) && qty > 0)
                    {
                        decimal price = Convert.ToDecimal(row.Cells["Price"].Value);

                        // ახალი ჯამის გამოთვლა სტრიქონისთვის
                        row.Cells["colTotal"].Value = qty * price;

                        // საერთო ჯამის გადათვლა
                        CalculateTotal();
                    }
                    else
                    {
                        MessageBox.Show("გთხოვთ მიუთითოთ ვალიდური რაოდენობა!", "გაფრთხილება", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        row.Cells["Quantity"].Value = 1; // უბრუნებს შეცომა 1-ს
                    }
                }
            }
        }

        private void dgvCart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgvCart.CurrentRow != null && !dgvCart.CurrentRow.IsNewRow)
            {
                dgvCart.Rows.Remove(dgvCart.CurrentRow);
                CalculateTotal();
                txtBarcode.Focus();
            }
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            txtBarcode.Focus();
        }

        private void btnCompleteSale_Click(object sender, EventArgs e)
        {
            // 1. შემოწმება: არის თუ არა რაიმე კალათაში
            bool hasItems = false;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (!row.IsNewRow && row.Cells["ProductID"].Value != null)
                {
                    hasItems = true;
                    break;
                }
            }
            if (!hasItems)
            {
                MessageBox.Show("კალათა ცარიელია! დაამატეთ პროდუქტი გაყიდვამდე.", "გაფრთხილება", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. ბაზაში გაყიდვის გატარება
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // ა) გამოვთვალოთ ჯამური თანხა
                    decimal grandTotal = 0;
                    foreach (DataGridViewRow row in dgvCart.Rows)
                    {
                        if (!row.IsNewRow && row.Cells["colTotal"].Value != null)
                        {
                            grandTotal += Convert.ToDecimal(row.Cells["colTotal"].Value);
                        }
                    }

                    // ბ) Sales ცხრილში ჩაწერა და ახალი SaleID-ის ამოღება
                    string saleQuery = "INSERT INTO Sales (TotalAmount) OUTPUT INSERTED.SaleID VALUES (@TotalAmount)";
                    SqlCommand saleCmd = new SqlCommand(saleQuery, conn, transaction);
                    saleCmd.Parameters.AddWithValue("@TotalAmount", grandTotal);

                    int saleId = (int)saleCmd.ExecuteScalar(); // 👈 ცვლადის სახელი გავასწორეთ saleId-ზე

                    // გ) SaleDetails-ში ჩაწერა
                    foreach (DataGridViewRow row in dgvCart.Rows)
                    {
                        if (row.IsNewRow || row.Cells["ProductID"].Value == null) continue;

                        int productId = Convert.ToInt32(row.Cells["ProductID"].Value);
                        int qty = Convert.ToInt32(row.Cells["Quantity"].Value);
                        decimal unitPrice = Convert.ToDecimal(row.Cells["Price"].Value);
                        decimal totalPrice = Convert.ToDecimal(row.Cells["colTotal"].Value);

                        // დეტალების ჩაწერა
                        string detailQuery = @"INSERT INTO SaleDetails (SaleID, ProductID, Quantity, UnitPrice, TotalPrice) 
                                       VALUES (@SaleID, @ProductID, @Quantity, @UnitPrice, @TotalPrice)";
                        SqlCommand detailCmd = new SqlCommand(detailQuery, conn, transaction);
                        detailCmd.Parameters.AddWithValue("@SaleID", saleId);
                        detailCmd.Parameters.AddWithValue("@ProductID", productId);
                        detailCmd.Parameters.AddWithValue("@Quantity", qty);
                        detailCmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                        detailCmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                        detailCmd.ExecuteNonQuery();
                    }

                    // 3. თუ ყველა პროდუქტი წარმატებით ჩაიწერა, ახლა ვადასტურებთ ტრანზაქციას
                    transaction.Commit();

                    MessageBox.Show("გაყიდვა წარმატებით განხორციელდა!", "წარმატება", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. დავაგენერიროთ PDF ჩეკი (ციკლის გარეთ, სანამ კალათას გავასუფთავებთ)
                    string pdfPath = GenerateReceiptPdf(saleId, dgvCart, grandTotal);

                    if (!string.IsNullOrEmpty(pdfPath))
                    {
                        // 📧 თუ მოლარემ ჩაწერილი აქვს მყიდველის მეილი, გაიგზავნოს Gmail-ზე
                        if (!string.IsNullOrWhiteSpace(txtCustomerEmail.Text))
                        {
                            SendReceiptByEmail(txtCustomerEmail.Text.Trim(), pdfPath);
                        }

                        //  his ეკრანზე გახსნა სანახავად/ამოსაბეჭდად
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(pdfPath) { UseShellExecute = true });
                    }

                    // 6. კალათის გასუფთავება და მომზადება შემდეგი გაყიდვისთვის
                    dgvCart.Rows.Clear();
                    CalculateTotal();
                    txtBarcode.Focus();
                }
                catch (Exception ex)
                {
                    // შეცდომისას უკან ვაბრუნებთ ცვლილებებს
                    transaction.Rollback();
                    MessageBox.Show("შეცდომა გაყიდვისას: " + ex.Message, "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public string GenerateReceiptPdf(int saleId, DataGridView dgvCart, decimal totalAmount)
        {
            string folderPath = System.IO.Path.Combine(Application.StartupPath, "Receipts");
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

            string filePath = System.IO.Path.Combine(folderPath, $"Receipt_{saleId}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

            try
            {
                using (PdfWriter writer = new PdfWriter(filePath))
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    // 80mm ჩეკის ზომა
                    pdf.SetDefaultPageSize(new PageSize(226, 600));
                    Document doc = new Document(pdf);
                    doc.SetMargins(10, 10, 10, 10);

                    // Bold შრიფტის მომზადება
                    PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    // სათაური
                    Paragraph header = new Paragraph("MY SHOP INVENTORY")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(boldFont)
                        .SetFontSize(14);
                    doc.Add(header);

                    Paragraph info = new Paragraph($"Check #: {saleId}\nDate: {DateTime.Now:yyyy-MM-dd HH:mm}\n-----------------------------------")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(9);
                    doc.Add(info);

                    // პროდუქტების ცხრილი
                    Table table = new Table(new float[] { 50, 20, 30 }).UseAllAvailableWidth();

                    table.AddHeaderCell(new Cell().Add(new Paragraph("Product").SetFont(boldFont).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Qty").SetFont(boldFont).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Total").SetFont(boldFont).SetFontSize(9)).SetBorder(Border.NO_BORDER));

                    foreach (DataGridViewRow row in dgvCart.Rows)
                    {
                        if (row.IsNewRow || row.Cells[0].Value == null) continue;

                        string name = row.Cells[2].Value?.ToString() ?? "";
                        string qty = row.Cells[3].Value?.ToString() ?? "0";
                        string total = Convert.ToDecimal(row.Cells[5].Value).ToString("0.00");

                        table.AddCell(new Cell().Add(new Paragraph(name).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                        table.AddCell(new Cell().Add(new Paragraph(qty).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                        table.AddCell(new Cell().Add(new Paragraph(total + " GEL").SetFontSize(9)).SetBorder(Border.NO_BORDER));
                    }

                    doc.Add(table);

                    // ჯამი
                    Paragraph footer = new Paragraph($"-----------------------------------\nTOTAL: {totalAmount:0.00} GEL\n\nThank you for shopping!")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(boldFont)
                        .SetFontSize(9);
                    doc.Add(footer);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDF ჩეკის შექმნის შეცდომა: " + ex.Message);
                return string.Empty;
            }
        }
        public void SendReceiptByEmail(string recipientEmail, string pdfPath)
        {
            try
            {
                string senderEmail = "mateaskilashvili09@gmail.com";
                string appPassword = "aclt xniv alou dols";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail, "My Shop");
                mail.To.Add(recipientEmail);
                mail.Subject = "თქვენი ნასყიდობის ჩეკი";
                mail.Body = "გმადლობთ ჩვენთან შეძენისთვის! იხილეთ მიმაგრებული ჩეკი PDF ფორმატში.";

                // მივაბათ PDF ფაილი
                if (File.Exists(pdfPath))
                {
                    Attachment attachment = new Attachment(pdfPath);
                    mail.Attachments.Add(attachment);
                }

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(senderEmail, appPassword);
                smtp.EnableSsl = true;

                smtp.Send(mail);
                MessageBox.Show("ჩეკი წარმატებით გაიგზავნა ელ-ფოსტაზე!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Email-ის გაგზავნის შეცდომა: " + ex.Message);
            }
        }
    }
}