using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using ClosedXML.Excel;
using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace ShopInventoryApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string connectionString = @"Server=.\SQLEXPRESS;Database=ShopInventoryDB;Trusted_Connection=True;TrustServerCertificate=True;";
        private void btnLoadProducts_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // გამოვიძახოთ ჩვენი შექმნილი VIEW
                    string query = "SELECT * FROM vw_ProductList where IsActive = 1";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // ჩავსვათ მონაცემები DataGridView-ში
                    dgvProducts.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("შეცდომა ბაზასთან დაკავშირებისას: " + ex.Message);
                }
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            // 1. ვალიდაცია: ვამოწმებთ ტექსტბოქსებს და ასევე ComboBox-ს (არჩეულია თუ არა კატეგორია)
            if (string.IsNullOrWhiteSpace(txtBarcode.Text) ||
                string.IsNullOrWhiteSpace(txtProductName.Text) ||
                cmbCategories.SelectedValue == null || // <--- ამოწმებს, არჩეულია თუ არა კატეგორია
                string.IsNullOrWhiteSpace(txtCostPrice.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("გთხოვთ შეავსოთ ყველა ველი და აირჩიოთ კატეგორია!");
                return;
            }

            string query = @"INSERT INTO Products (BarCode, ProductName, CategoryID, BuyPrice, SellPrice, Quantity) 
                     VALUES (@BarCode, @ProductName, @CategoryID, @BuyPrice, @SellPrice, @Quantity)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BarCode", txtBarcode.Text);
                        cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);

                        // 2. ComboBox-იდან იღებს ფარულ ID-ს (ValueMember-ს)
                        cmd.Parameters.AddWithValue("@CategoryID", Convert.ToInt32(cmbCategories.SelectedValue));

                        cmd.Parameters.AddWithValue("@BuyPrice", decimal.Parse(txtCostPrice.Text));
                        cmd.Parameters.AddWithValue("@SellPrice", decimal.Parse(txtPrice.Text));
                        cmd.Parameters.AddWithValue("@Quantity", int.Parse(txtStock.Text));

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("პროდუქტი წარმატებით დაემატა!");

                        btnLoadProducts_Click(sender, e);

                        // 3. ველების გასუფთავება
                        txtBarcode.Clear();
                        txtProductName.Clear();
                        cmbCategories.SelectedIndex = -1; // <--- ComboBox-ის დაბრუნება ცარიელ მდგომარეობაში
                        txtCostPrice.Clear();
                        txtPrice.Clear();
                        txtStock.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("შეცდომა დამატებისას: " + ex.Message);
                }
            }
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void LoadCategories()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryID, CategoryName FROM Categories";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // რას დაინახავს მომხმარებელი ეკრანზე:
                cmbCategories.DisplayMember = "CategoryName";

                // რა მნიშვნელობა ექნება ამ არჩევანს ფონურ რეჟიმში (ID):
                cmbCategories.ValueMember = "CategoryID";

                cmbCategories.DataSource = dt;
                cmbCategories.SelectedIndex = -1; // თავიდან არაფერი იყოს არჩეული
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            // 1. შევამოწმოთ, არის თუ არა არჩეული რომელიმე სტრიქონი ცხრილში
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("გთხოვთ, ჯერ აირჩიოთ წასაშლელი პროდუქტი ცხრილში!");
                return;
            }

            // 2. წავიკითხოთ არჩეული სტრიქონის ProductID
            int selectedProductId = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["ProductID"].Value);

            // 3. ვკითხოთ მომხმარებელს დადასტურება
            DialogResult confirm = MessageBox.Show(
                "ნამდვილად გსურთ ამ პროდუქტის წაშლა?",
                "დადასტურება",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
            {
                string connectionString = @"Server=.\SQLEXPRESS;Database=ShopInventoryDB;Trusted_Connection=True;TrustServerCertificate=True;";
                string query = "UPDATE Products SET IsActive = 0 WHERE ProductID = @ProductID";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", selectedProductId);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("პროდუქტი წარმატებით წაიშალა!");

                            // ცხრილის განახლება
                            btnLoadProducts_Click(sender, e);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("შეცდომა წაშლისას: " + ex.Message);
                    }
                }
            }
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // შევამოწმოთ, რომ ნამდვილად მონაცემების სტრიქონს დააჭირა და არა სათაურს
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];

                txtBarcode.Text = row.Cells["შტრიხკოდი"].Value?.ToString();
                txtProductName.Text = row.Cells["პროდუქტი"].Value?.ToString();

                // ვინაიდან ცხრილში ID-ს ნაცვლად ტექსტი "ტკბილეული" წერია,
                // ველში ჩავსვათ 1 და საჭიროებისამებრ ხელით შეცვალე შესაბამისი ID ციფრით
                cmbCategories.SelectedValue = 1;

                txtCostPrice.Text = row.Cells["თვითღირებულება"].Value?.ToString();
                txtPrice.Text = row.Cells["გასაყიდი ფასი"].Value?.ToString();

                // რაოდენობა ბოლო სვეტშია
                txtStock.Text = row.Cells[7].Value?.ToString();
            }
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("გთხოვთ, ჯერ აირჩიოთ პროდუქტი ცხრილში განახლებისთვის!");
                return;
            }

            // ვიღებთ ProductID-ს პირველი სვეტიდან (ინდექსი 0)
            int selectedProductId = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["ProductID"].Value);

            string query = @"UPDATE Products
                            SET BarCode = @BarCode,
                               ProductName = @ProductName,
                                CategoryID = @CategoryID,
                                BuyPrice = @BuyPrice,
                                SellPrice = @SellPrice,
                                Quantity = @Quantity
                            WHERE ProductID = @ProductID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BarCode", txtBarcode.Text);
                        cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                        cmd.Parameters.AddWithValue("@CategoryID", Convert.ToInt32(cmbCategories.SelectedValue));
                        cmd.Parameters.AddWithValue("@BuyPrice", decimal.Parse(txtCostPrice.Text));
                        cmd.Parameters.AddWithValue("@SellPrice", decimal.Parse(txtPrice.Text));
                        cmd.Parameters.AddWithValue("@Quantity", int.Parse(txtStock.Text));
                        cmd.Parameters.AddWithValue("@ProductID", selectedProductId);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("მონაცემები წარმატებით განახლდა!");

                        btnLoadProducts_Click(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("შეცდომა განახლებისას: " + ex.Message);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ცხრილის პარამეტრების გასწორება
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersVisible = false;

            // მონაცემების ავტომატური ჩატვირთვა ჩართვისთანავე
            btnLoadProducts_Click(sender, e);

            LoadCategories();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Enter-ზე დაჭერისას Windows-ის უსიამოვნო "Beep" ხმის გათიშვა
                e.SuppressKeyPress = true;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Products WHERE Barcode = @Barcode";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Barcode", txtBarcode.Text.Trim());

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            // თუ პროდუქტი მოიძებნა, მონაცემების ველებში გადატანა
                            txtProductName.Text = reader["ProductName"].ToString();
                            cmbCategories.SelectedValue = reader["CategoryID"];
                            txtCostPrice.Text = reader["BuyPrice"].ToString();
                            txtPrice.Text = reader["SellPrice"].ToString();
                            txtStock.Text = reader["Quantity"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("პროდუქტი ვერ მოიძებნა შტრიხკოდის მიხედვით.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("შეცდომა მოხდა ძებნისას: " + ex.Message);
                    }
                }
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        DataTable dt = new DataTable();

                        // 1. სვეტების სათაურების წამოღება DataGridView-დან
                        foreach (DataGridViewColumn column in dgvProducts.Columns)
                        {
                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }

                        // 2. მონაცემების წამოღება თითოეული სტრიქონიდან
                        foreach (DataGridViewRow row in dgvProducts.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                DataRow dr = dt.NewRow();
                                for (int i = 0; i < dgvProducts.Columns.Count; i++)
                                {
                                    dr[i] = row.Cells[i].Value?.ToString() ?? "";
                                }
                                dt.Rows.Add(dr);
                            }
                        }

                        // 3. Excel ფაილის შექმნა და შენახვა
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var worksheet = wb.Worksheets.Add(dt, "Products");
                            worksheet.Columns().AdjustToContents(); // სვეტების ზომების ავტომატური გასწორება
                            wb.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("მონაცემები წარმატებით ექსპორტირდა Excel-ში!", "წარმატება", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"შეცდომა ექსპორტისას: {ex.Message}", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF Document|*.pdf" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (PdfWriter writer = new PdfWriter(sfd.FileName))
                        using (PdfDocument pdf = new PdfDocument(writer))
                        using (Document document = new Document(pdf))
                        {
                            Table table = new Table(dgvProducts.Columns.Count);

                            // 1. სვეტების სათაურები
                            foreach (DataGridViewColumn column in dgvProducts.Columns)
                            {
                                table.AddHeaderCell(column.HeaderText);
                            }

                            // 2. მონაცემები
                            foreach (DataGridViewRow row in dgvProducts.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        table.AddCell(cell.Value?.ToString() ?? "");
                                    }
                                }
                            }

                            document.Add(table);
                        }

                        MessageBox.Show("მონაცემები წარმატებით ექსპორტირდა PDF-ში!", "წარმატება", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"შეცდომა ექსპორტისას: {ex.Message}", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}