namespace ShopInventoryApp
{
    public partial class SalesForm : Form
    {
        public SalesForm()
        {
            InitializeComponent();
        }

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
            bool found = false;

            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.Cells["colBarcode"].Value?.ToString() == barcode)
                {
                    int currentQty = Convert.ToInt32(row.Cells["colQuantity"].Value);
                    decimal price = Convert.ToDecimal(row.Cells["colPrice"].Value);

                    row.Cells["colQuantity"].Value = currentQty + 1;
                    row.Cells["colTotal"].Value = (currentQty + 1) * price;

                    found = true;
                    break;
                }
            }
            if (!found)
            {
                string productName = "ტესტ პროდუქტი";
                decimal price = 2.50m;

                dgvCart.Rows.Add(barcode, productName, 1, price, price);
            }
            CalculateGrandTotal();
        }

        private void CalculateGrandTotal()
        {
            decimal grandTotal = 0;

            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.Cells["colTotal"].Value != null)
                {
                    grandTotal += Convert.ToDecimal(row.Cells["colTotal"].Value);
                }
            }
            lblTotal.Text = $"{grandTotal:N2} ₾";
        }
    }
}