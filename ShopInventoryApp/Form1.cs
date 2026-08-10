using Microsoft.Data.SqlClient;
using System.Data;

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
                    string query = "SELECT * FROM vw_ProductList";

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
            if (string.IsNullOrWhiteSpace(txtBarcode.Text) ||
                string.IsNullOrWhiteSpace(txtProductName.Text) ||
                string.IsNullOrWhiteSpace(txtCategoryID.Text) ||
                string.IsNullOrWhiteSpace(txtCostPrice.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("გთხოვთ შეავსოთ ყველა ველი!");
                return;
            }

            // სვეტების სახელები ზუსტად შენი ბაზის შესაბამისია
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
                        cmd.Parameters.AddWithValue("@CategoryID", int.Parse(txtCategoryID.Text));
                        cmd.Parameters.AddWithValue("@BuyPrice", decimal.Parse(txtCostPrice.Text));
                        cmd.Parameters.AddWithValue("@SellPrice", decimal.Parse(txtPrice.Text));
                        cmd.Parameters.AddWithValue("@Quantity", int.Parse(txtStock.Text));

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("პროდუქტი წარმატებით დაემატა!");

                        btnLoadProducts_Click(sender, e);

                        txtBarcode.Clear();
                        txtProductName.Clear();
                        txtCategoryID.Clear();
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
                string query = "DELETE FROM Products WHERE ProductID = @ProductID";

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
                txtCategoryID.Text = "1";

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
                        cmd.Parameters.AddWithValue("@CategoryID", int.Parse(txtCategoryID.Text));
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
                            txtCategoryID.Text = reader["CategoryID"].ToString();
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
    }
}