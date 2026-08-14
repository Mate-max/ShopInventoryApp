namespace ShopInventoryApp
{
    partial class SalesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            txtBarcode = new TextBox();
            dgvCart = new DataGridView();
            ProductID = new DataGridViewTextBoxColumn();
            Barcode = new DataGridViewTextBoxColumn();
            colProductName = new DataGridViewTextBoxColumn();
            Quantity = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            lblTotal = new Label();
            btnCompleteSale = new Button();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            txtCustomerEmail = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            SuspendLayout();
            // 
            // txtBarcode
            // 
            txtBarcode.Location = new Point(85, 262);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(441, 31);
            txtBarcode.TabIndex = 0;
            txtBarcode.KeyDown += txtBarcode_KeyDown;
            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(45, 50, 55);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvCart.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCart.Columns.AddRange(new DataGridViewColumn[] { ProductID, Barcode, colProductName, Quantity, Price, colTotal });
            dgvCart.EnableHeadersVisualStyles = false;
            dgvCart.Location = new Point(764, 103);
            dgvCart.Name = "dgvCart";
            dgvCart.RowHeadersWidth = 62;
            dgvCart.Size = new Size(1131, 448);
            dgvCart.TabIndex = 1;
            dgvCart.CellValueChanged += dgvCart_CellValueChanged;
            dgvCart.KeyDown += dgvCart_KeyDown;
            // 
            // ProductID
            // 
            ProductID.HeaderText = "ProductID";
            ProductID.MinimumWidth = 8;
            ProductID.Name = "ProductID";
            ProductID.Visible = false;
            // 
            // Barcode
            // 
            Barcode.HeaderText = "შტრიხკოდი";
            Barcode.MinimumWidth = 8;
            Barcode.Name = "Barcode";
            // 
            // colProductName
            // 
            colProductName.HeaderText = "დასახელება";
            colProductName.MinimumWidth = 8;
            colProductName.Name = "colProductName";
            // 
            // Quantity
            // 
            Quantity.HeaderText = "რაოდენობა";
            Quantity.MinimumWidth = 8;
            Quantity.Name = "Quantity";
            // 
            // Price
            // 
            Price.HeaderText = "ფასი";
            Price.MinimumWidth = 8;
            Price.Name = "Price";
            // 
            // colTotal
            // 
            colTotal.HeaderText = "ჯამი";
            colTotal.MinimumWidth = 8;
            colTotal.Name = "colTotal";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 20F);
            lblTotal.Location = new Point(1306, 608);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(136, 54);
            lblTotal.TabIndex = 2;
            lblTotal.Text = "0,00 ₾";
            // 
            // btnCompleteSale
            // 
            btnCompleteSale.BackColor = Color.Green;
            btnCompleteSale.Font = new Font("Segoe UI", 12F);
            btnCompleteSale.ForeColor = Color.Transparent;
            btnCompleteSale.Location = new Point(1671, 594);
            btnCompleteSale.Name = "btnCompleteSale";
            btnCompleteSale.Size = new Size(172, 68);
            btnCompleteSale.TabIndex = 3;
            btnCompleteSale.Text = "გაყიდვა";
            btnCompleteSale.UseVisualStyleBackColor = false;
            btnCompleteSale.Click += btnCompleteSale_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(89, 103);
            label2.Name = "label2";
            label2.Size = new Size(441, 54);
            label2.TabIndex = 4;
            label2.Text = "სწრაფი სკანირება";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(89, 176);
            label3.Name = "label3";
            label3.Size = new Size(221, 41);
            label3.TabIndex = 5;
            label3.Text = "შტრიხკოდი";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(89, 327);
            label1.Name = "label1";
            label1.Size = new Size(298, 41);
            label1.TabIndex = 6;
            label1.Text = "მყიდველის Email";
            // 
            // txtCustomerEmail
            // 
            txtCustomerEmail.Location = new Point(89, 410);
            txtCustomerEmail.Name = "txtCustomerEmail";
            txtCustomerEmail.Size = new Size(272, 31);
            txtCustomerEmail.TabIndex = 7;
            // 
            // SalesForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2114, 874);
            Controls.Add(txtCustomerEmail);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnCompleteSale);
            Controls.Add(lblTotal);
            Controls.Add(dgvCart);
            Controls.Add(txtBarcode);
            Name = "SalesForm";
            Text = "SalesForm";
            Load += SalesForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBarcode;
        private DataGridView dgvCart;
        private Label lblTotal;
        private Button btnCompleteSale;
        private Label label2;
        private Label label3;
        private DataGridViewTextBoxColumn ProductID;
        private DataGridViewTextBoxColumn Barcode;
        private DataGridViewTextBoxColumn colProductName;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn colTotal;
        private Label label1;
        private TextBox txtCustomerEmail;
    }
}