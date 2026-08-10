namespace ShopInventoryApp
{
    partial class MainForm
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
            pnlSidebar = new Panel();
            btnSales = new Button();
            btnInventory = new Button();
            pnlContent = new Panel();
            pnlSidebar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.DimGray;
            pnlSidebar.Controls.Add(btnSales);
            pnlSidebar.Controls.Add(btnInventory);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(200, 717);
            pnlSidebar.TabIndex = 0;
            // 
            // btnSales
            // 
            btnSales.Dock = DockStyle.Top;
            btnSales.FlatAppearance.BorderSize = 0;
            btnSales.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 60, 65);
            btnSales.FlatStyle = FlatStyle.Flat;
            btnSales.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSales.ForeColor = Color.Transparent;
            btnSales.Location = new Point(0, 34);
            btnSales.Name = "btnSales";
            btnSales.Size = new Size(200, 59);
            btnSales.TabIndex = 1;
            btnSales.Text = "ახალი გაყიდვა";
            btnSales.UseVisualStyleBackColor = true;
            btnSales.Click += btnSales_Click;
            // 
            // btnInventory
            // 
            btnInventory.BackColor = Color.DimGray;
            btnInventory.Dock = DockStyle.Top;
            btnInventory.FlatAppearance.BorderSize = 0;
            btnInventory.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 60, 65);
            btnInventory.FlatStyle = FlatStyle.Flat;
            btnInventory.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInventory.ForeColor = Color.Transparent;
            btnInventory.Location = new Point(0, 0);
            btnInventory.Name = "btnInventory";
            btnInventory.Size = new Size(200, 34);
            btnInventory.TabIndex = 0;
            btnInventory.Text = "მარაგები";
            btnInventory.UseVisualStyleBackColor = false;
            btnInventory.Click += btnInventory_Click_1;
            // 
            // pnlContent
            // 
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(200, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1771, 717);
            pnlContent.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1971, 717);
            Controls.Add(pnlContent);
            Controls.Add(pnlSidebar);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            pnlSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSidebar;
        private Panel pnlContent;
        private Button btnInventory;
        private Button btnSales;
    }
}