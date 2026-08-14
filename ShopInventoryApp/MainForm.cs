namespace ShopInventoryApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // 💡 ეს დამხმარე ფუნქცია ჩასვამს ნებისმიერ ფორმას pnlContent პანელში
        private void LoadFormIntoPanel(Form formToLoad)
        {
            pnlContent.Controls.Clear(); // ვასუფთავებთ პანელს

            formToLoad.TopLevel = false;
            formToLoad.FormBorderStyle = FormBorderStyle.None;
            formToLoad.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(formToLoad);
            pnlContent.Tag = formToLoad;
            formToLoad.Show();
        }

        // 1️⃣ პროგრამის ჩართვისთანავე default-ად გავხსნათ SalesForm (გაყიდვების გვერდი)
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new SalesForm());
        }

        // 2️⃣ "ახალი გაყიდვა" ღილაკზე დაჭერისას:
        private void btnSales_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new SalesForm());
        }

        // 3️⃣ "მარაგები" ღილაკზე დაჭერისას:
        private void btnInventory_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new Form1());
        }

        // 4️⃣ "რეპორტები" ღილაკზე დაჭერისას:
        private void btnOpenReports_Click(object sender, EventArgs e)
        {
            // თუ გინდა რეპორტებიც იმაpermission პანელშივე ჩაჯდეს:
            LoadFormIntoPanel(new ReportsForm());

            // ან თუ გინდა ცალკე ამოხტეს (Pop-up):
            // ReportsForm reports = new ReportsForm();
            // reports.ShowDialog();

        }
    }
}