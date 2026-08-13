using DocumentFormat.OpenXml.Office2013.Excel;
using Microsoft.Data.SqlClient;

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

        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
            //1. შემოწმება: არის თუ არა რაიმე კალათაში
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

            //2. ბაზაში გაყიდვის გატარება
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

                    // ბ) Sales ცხრილში ჩაწერა და ახალი SaleId-ის ამოღება
                    string saleQuery = "INSERT INTO Sales (TotalAmount) OUTPUT INSERTED.SaleID VALUES (@TotalAmount)";
                    SqlCommand saleCmd = new SqlCommand(saleQuery, conn, transaction);
                    saleCmd.Parameters.AddWithValue("@TotalAmount", grandTotal);

                    int saled = (int)saleCmd.ExecuteScalar();

                    // გ) SaleDetails-ში ჩაწერა და Products-ში ნაშთის შემცირება
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
                        detailCmd.Parameters.AddWithValue("@SaleID", saled);
                        detailCmd.Parameters.AddWithValue("@ProductID", productId);
                        detailCmd.Parameters.AddWithValue("@Quantity", qty);
                        detailCmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                        detailCmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                        detailCmd.ExecuteNonQuery();

                        // თუ ყველაფერი უხარვეზოდ შესრულდა, დავადასტუროთ ბაზაში
                        transaction.Commit();

                        MessageBox.Show("გაყიდვა წარმატებით განხორციელდა!", "წარმატებები", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // დ) კალათის გასუფთავება და მოემზადე შემდეგი გაყიდვისთვის
                        dgvCart.Rows.Clear();
                        CalculateTotal();
                        txtBarcode.Focus();
                    }
                }
                catch (Exception ex)
                {
                    // შეცდომისას უკან ვაბრუნებთ ცვლილებებს
                    transaction.Rollback();
                    MessageBox.Show("შეცდომა გაყიდვისას: " + ex.Message, "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}