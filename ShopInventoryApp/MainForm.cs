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

        private void HighlightActiveButton(Button activeButton)
        {
            // 1.სამივე ღილაკს გავუთიშოთ Windows-ის სტანდარტული ფონი
            btnInventory.UseVisualStyleBackColor = false;
            btnSales.UseVisualStyleBackColor = false;
            btnOpenReports.UseVisualStyleBackColor = false;

            // 2.სამივე ღილაკი დავაყენოთ მუქ ნაცრისფერზე
            Color defaultColor = Color.FromArgb(45, 45, 48);
            btnInventory.BackColor = defaultColor;
            btnSales.BackColor = defaultColor;
            btnOpenReports.BackColor = defaultColor;

            // 3. მხოლოდ დაჭერილ (აქტიურ) ღილაკს მივცეთ კაშკაშა ლურჯი ფერი
            activeButton.BackColor = Color.FromArgb(0, 122, 204);
        }

        // 1️⃣ პროგრამის ჩართვისთანავე default-ად გავხსნათ SalesForm (გაყიდვების გვერდი)
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new SalesForm());

            HighlightActiveButton(btnInventory);
        }

        // 2️⃣ "ახალი გაყიდვა" ღილაკზე დაჭერისას:
        private void btnSales_Click(object sender, EventArgs e)
        {
            HighlightActiveButton(btnSales);
            LoadFormIntoPanel(new SalesForm());
        }

        // 3️⃣ "მარაგები" ღილაკზე დაჭერისას:
        private void btnInventory_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new Form1());

            HighlightActiveButton(btnInventory);
        }

        // 4️⃣ "რეპორტები" ღილაკზე დაჭერისას:
        private void btnOpenReports_Click(object sender, EventArgs e)
        {
            // თუ გინდა რეპორტებიც იმაpermission პანელშივე ჩაჯდეს:
            LoadFormIntoPanel(new ReportsForm());

            HighlightActiveButton(btnOpenReports);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("dd.mm.yyyy HH:mm:ss");
        }
    }
}