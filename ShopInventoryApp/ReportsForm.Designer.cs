namespace ShopInventoryApp
{
    partial class ReportsForm
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
            dtpFrom = new DateTimePicker();
            dtpTo = new DateTimePicker();
            btnFilter = new Button();
            lblTotalRevenue = new Label();
            lblTotalSales = new Label();
            lblNetProfit = new Label();
            dgvReports = new DataGridView();
            btnExportExcel = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvReports).BeginInit();
            SuspendLayout();
            // 
            // dtpFrom
            // 
            dtpFrom.Location = new Point(63, 30);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(300, 31);
            dtpFrom.TabIndex = 0;
            // 
            // dtpTo
            // 
            dtpTo.Location = new Point(490, 30);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(300, 31);
            dtpTo.TabIndex = 1;
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(973, 30);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(207, 34);
            btnFilter.TabIndex = 2;
            btnFilter.Text = "🔍 განახლება";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            // 
            // lblTotalRevenue
            // 
            lblTotalRevenue.AutoSize = true;
            lblTotalRevenue.Location = new Point(148, 140);
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Size = new Size(235, 25);
            lblTotalRevenue.TabIndex = 3;
            lblTotalRevenue.Text = "სულ შემოსავალი: 0.00 ₾";
            // 
            // lblTotalSales
            // 
            lblTotalSales.AutoSize = true;
            lblTotalSales.Location = new Point(503, 140);
            lblTotalSales.Name = "lblTotalSales";
            lblTotalSales.Size = new Size(252, 25);
            lblTotalSales.TabIndex = 4;
            lblTotalSales.Text = "გაყიდვების რაოდენობა: 0";
            // 
            // lblNetProfit
            // 
            lblNetProfit.AutoSize = true;
            lblNetProfit.Location = new Point(850, 140);
            lblNetProfit.Name = "lblNetProfit";
            lblNetProfit.Size = new Size(208, 25);
            lblNetProfit.TabIndex = 5;
            lblNetProfit.Text = "სუფთა მოგება: 0.00 ₾";
            // 
            // dgvReports
            // 
            dgvReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReports.Location = new Point(178, 210);
            dgvReports.Name = "dgvReports";
            dgvReports.RowHeadersWidth = 62;
            dgvReports.Size = new Size(1562, 445);
            dgvReports.TabIndex = 6;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Location = new Point(791, 661);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(278, 34);
            btnExportExcel.TabIndex = 7;
            btnExportExcel.Text = "📥 Excel-ში ჩამოტვირთვა";
            btnExportExcel.UseVisualStyleBackColor = true;
            btnExportExcel.Click += btnExportExcel_Click;
            // 
            // ReportsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1936, 761);
            Controls.Add(btnExportExcel);
            Controls.Add(dgvReports);
            Controls.Add(lblNetProfit);
            Controls.Add(lblTotalSales);
            Controls.Add(lblTotalRevenue);
            Controls.Add(btnFilter);
            Controls.Add(dtpTo);
            Controls.Add(dtpFrom);
            Name = "ReportsForm";
            Text = "ReportsForm";
            Load += ReportsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvReports).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Button btnFilter;
        private Label lblTotalRevenue;
        private Label lblTotalSales;
        private Label lblNetProfit;
        private DataGridView dgvReports;
        private Button btnExportExcel;
    }
}