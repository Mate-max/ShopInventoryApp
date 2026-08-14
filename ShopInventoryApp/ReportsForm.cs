using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace ShopInventoryApp
{
    public partial class ReportsForm : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=ShopInventoryDB;Trusted_Connection=True;TrustServerCertificate=True;";
        public ReportsForm()
        {
            InitializeComponent();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            // default-ად დავაყენოთ მიმდინარე თვის დასაწყისიდან დღემდე
            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTo.Value = DateTime.Now;

            // მონაცემები ავტომატურად ჩაიტვირთოს
            LoadReportData();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadReportData();
        }

        private void LoadReportData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL მოთხოვნა: იღებს გაყიდვების დეტალებს და ითვლის თვითღირებულებას და სუფთა მოგებას
                    string query = @"
    SELECT 
        s.SaleID AS [ჩეკის #],
        s.SaleDate AS [თარიღი],
        p.ProductName AS [პროდუქტი],
        sd.Quantity AS [რაოდენობა],
        sd.UnitPrice AS [გასაყიდი ფასი],
        sd.TotalPrice AS [სულ ჯამი],
        (sd.Quantity * ISNULL(p.BuyPrice, 0)) AS [სულ თვითღირებულება],
        (sd.TotalPrice - (sd.Quantity * ISNULL(p.BuyPrice, 0))) AS [სუფთა მოგება]
    FROM Sales s
    INNER JOIN SaleDetails sd ON s.SaleID = sd.SaleID
    INNER JOIN Products p ON sd.ProductID = p.ProductID
    WHERE s.SaleDate >= @FromDate AND s.SaleDate <= @ToDate
    ORDER BY s.SaleDate DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // დღის დასაწყისი 00:00:00 და დღის დასასრული 23:59:59
                    cmd.Parameters.AddWithValue("@FromDate", dtpFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@ToDate", dtpTo.Value.Date.AddDays(1).AddTicks(-1));

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // ჩავტვირთოთ DataGridView-ში
                    dgvReports.DataSource = dt;

                    //გამოვთვალოთ ჯამური ინდიკატორები (KPIs)
                    decimal totalRevenue = 0;
                    decimal totalNetProfit = 0;
                    int totalSalesCount = dt.Rows.Count;

                    foreach (DataRow row in dt.Rows)
                    {
                        // ISNULL-ის დაDBNull-ის უსაფრთხო გადაყვანა:
                        if (row["სულ ჯამი"] != DBNull.Value)
                            totalRevenue += Convert.ToDecimal(row["სულ ჯამი"]);

                        if (row["სუფთა მოგება"] != DBNull.Value)
                            totalNetProfit += Convert.ToDecimal(row["სუფთა მოგება"]);
                    }

                    // განვაახლოთ Label-ები ეკრანზე
                    lblTotalRevenue.Text = $"სულ შემოსავალი: {totalRevenue:0.00} ₾";
                    lblTotalSales.Text = $"გაყიდვების რაოდენობა: {totalSalesCount}";
                    lblNetProfit.Text = $"სუფთა მოგება: {totalNetProfit:0.00} ₾";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("რეპორტის ჩატვირთვის შეცდომა: " + ex.Message, "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (dgvReports.Rows.Count == 0)
            {
                MessageBox.Show("ჩამოსატვირთი მონაცემები არ არის!", "გაფრთხილება", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook |*.xlsx", FileName = $"SalesReport_{DateTime.Now:yyyyMMdd}.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            DataTable dt = (DataTable)dgvReports.DataSource;
                            workbook.Worksheets.Add(dt, "Sales Report");
                            workbook.SaveAs(sfd.FileName);
                        }
                        MessageBox.Show("რეპორტი წარმატებით ექსპორტირდა Excel-ში!", "წარმატება", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Excel-ში შენახვის შეცდომა: " + ex.Message, "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
