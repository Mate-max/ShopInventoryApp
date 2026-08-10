namespace ShopInventoryApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dgvProducts = new DataGridView();
            btnLoadProducts = new Button();
            btnAddProduct = new Button();
            txtBarcode = new TextBox();
            txtCostPrice = new TextBox();
            txtPrice = new TextBox();
            txtStock = new TextBox();
            txtCategoryID = new TextBox();
            txtProductName = new TextBox();
            შტრიხკოდი = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            btnDeleteProduct = new Button();
            btnUpdateProduct = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // dgvProducts
            // 
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(45, 50, 55);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.Location = new Point(792, 58);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersWidth = 62;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(1128, 518);
            dgvProducts.TabIndex = 0;
            dgvProducts.CellClick += dgvProducts_CellClick;
            dgvProducts.CellContentClick += dgvProducts_CellContentClick;
            // 
            // btnLoadProducts
            // 
            btnLoadProducts.BackColor = Color.DarkBlue;
            btnLoadProducts.FlatAppearance.BorderSize = 0;
            btnLoadProducts.FlatStyle = FlatStyle.Flat;
            btnLoadProducts.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLoadProducts.ForeColor = Color.Transparent;
            btnLoadProducts.Location = new Point(399, 647);
            btnLoadProducts.Name = "btnLoadProducts";
            btnLoadProducts.Size = new Size(180, 55);
            btnLoadProducts.TabIndex = 1;
            btnLoadProducts.Text = "მონაცემების ჩატვირთვა";
            btnLoadProducts.UseCompatibleTextRendering = true;
            btnLoadProducts.UseVisualStyleBackColor = false;
            btnLoadProducts.Click += btnLoadProducts_Click;
            // 
            // btnAddProduct
            // 
            btnAddProduct.BackColor = Color.Green;
            btnAddProduct.FlatAppearance.BorderSize = 0;
            btnAddProduct.FlatStyle = FlatStyle.Flat;
            btnAddProduct.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddProduct.ForeColor = Color.Transparent;
            btnAddProduct.Location = new Point(1241, 647);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(180, 55);
            btnAddProduct.TabIndex = 2;
            btnAddProduct.Text = "პროდუქტის დამატება";
            btnAddProduct.UseCompatibleTextRendering = true;
            btnAddProduct.UseVisualStyleBackColor = false;
            btnAddProduct.Click += btnAddProduct_Click;
            // 
            // txtBarcode
            // 
            txtBarcode.Font = new Font("Segoe UI", 10F);
            txtBarcode.Location = new Point(538, 58);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(200, 34);
            txtBarcode.TabIndex = 3;
            txtBarcode.KeyDown += txtBarcode_KeyDown;
            // 
            // txtCostPrice
            // 
            txtCostPrice.Font = new Font("Segoe UI", 10F);
            txtCostPrice.Location = new Point(538, 265);
            txtCostPrice.Name = "txtCostPrice";
            txtCostPrice.Size = new Size(200, 34);
            txtCostPrice.TabIndex = 4;
            // 
            // txtPrice
            // 
            txtPrice.Font = new Font("Segoe UI", 10F);
            txtPrice.Location = new Point(538, 334);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(200, 34);
            txtPrice.TabIndex = 5;
            // 
            // txtStock
            // 
            txtStock.Font = new Font("Segoe UI", 10F);
            txtStock.Location = new Point(538, 403);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(200, 34);
            txtStock.TabIndex = 6;
            // 
            // txtCategoryID
            // 
            txtCategoryID.Font = new Font("Segoe UI", 10F);
            txtCategoryID.Location = new Point(538, 196);
            txtCategoryID.Name = "txtCategoryID";
            txtCategoryID.Size = new Size(200, 34);
            txtCategoryID.TabIndex = 7;
            // 
            // txtProductName
            // 
            txtProductName.Font = new Font("Segoe UI", 10F);
            txtProductName.Location = new Point(538, 127);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(200, 34);
            txtProductName.TabIndex = 8;
            // 
            // შტრიხკოდი
            // 
            შტრიხკოდი.AutoSize = true;
            შტრიხკოდი.Font = new Font("Segoe UI", 10F);
            შტრიხკოდი.Location = new Point(309, 64);
            შტრიხკოდი.Name = "შტრიხკოდი";
            შტრიხკოდი.Size = new Size(135, 28);
            შტრიხკოდი.TabIndex = 9;
            შტრიხკოდი.Text = "შტრიხკოდი";
            შტრიხკოდი.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(309, 133);
            label2.Name = "label2";
            label2.Size = new Size(138, 28);
            label2.TabIndex = 10;
            label2.Text = "დასახელება";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(309, 202);
            label3.Name = "label3";
            label3.Size = new Size(160, 28);
            label3.TabIndex = 11;
            label3.Text = "კატეგორიის ID";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(309, 271);
            label4.Name = "label4";
            label4.Size = new Size(206, 28);
            label4.TabIndex = 12;
            label4.Text = "თვითღირებულება";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F);
            label5.Location = new Point(309, 340);
            label5.Name = "label5";
            label5.Size = new Size(62, 28);
            label5.TabIndex = 13;
            label5.Text = "ფასი";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F);
            label6.Location = new Point(309, 409);
            label6.Name = "label6";
            label6.Size = new Size(129, 28);
            label6.TabIndex = 14;
            label6.Text = "რაოდენობა";
            // 
            // btnDeleteProduct
            // 
            btnDeleteProduct.BackColor = Color.Red;
            btnDeleteProduct.FlatAppearance.BorderSize = 0;
            btnDeleteProduct.FlatStyle = FlatStyle.Flat;
            btnDeleteProduct.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDeleteProduct.ForeColor = Color.Transparent;
            btnDeleteProduct.Location = new Point(1662, 647);
            btnDeleteProduct.Name = "btnDeleteProduct";
            btnDeleteProduct.Size = new Size(180, 55);
            btnDeleteProduct.TabIndex = 15;
            btnDeleteProduct.Text = "წაშლა";
            btnDeleteProduct.UseCompatibleTextRendering = true;
            btnDeleteProduct.UseVisualStyleBackColor = false;
            btnDeleteProduct.Click += btnDeleteProduct_Click;
            // 
            // btnUpdateProduct
            // 
            btnUpdateProduct.BackColor = Color.Olive;
            btnUpdateProduct.FlatAppearance.BorderSize = 0;
            btnUpdateProduct.FlatStyle = FlatStyle.Flat;
            btnUpdateProduct.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUpdateProduct.ForeColor = Color.Transparent;
            btnUpdateProduct.Location = new Point(820, 647);
            btnUpdateProduct.Name = "btnUpdateProduct";
            btnUpdateProduct.Size = new Size(180, 55);
            btnUpdateProduct.TabIndex = 16;
            btnUpdateProduct.Text = "რედაქტირება";
            btnUpdateProduct.UseCompatibleTextRendering = true;
            btnUpdateProduct.UseVisualStyleBackColor = false;
            btnUpdateProduct.Click += btnUpdateProduct_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(1950, 1076);
            Controls.Add(btnUpdateProduct);
            Controls.Add(btnDeleteProduct);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(შტრიხკოდი);
            Controls.Add(txtProductName);
            Controls.Add(txtCategoryID);
            Controls.Add(txtStock);
            Controls.Add(txtPrice);
            Controls.Add(txtCostPrice);
            Controls.Add(txtBarcode);
            Controls.Add(btnAddProduct);
            Controls.Add(btnLoadProducts);
            Controls.Add(dgvProducts);
            Name = "Form1";
            Text = "მაღაზიის მარაგების მართვა";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProducts;
        private Button btnLoadProducts;
        private Button btnAddProduct;
        private TextBox txtBarcode;
        private TextBox txtCostPrice;
        private TextBox txtPrice;
        private TextBox txtStock;
        private TextBox txtCategoryID;
        private TextBox txtProductName;
        private Label შტრიხკოდი;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button btnDeleteProduct;
        private Button btnUpdateProduct;
    }
}
