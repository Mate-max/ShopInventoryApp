namespace ShopInventoryApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadFormIntoPanel(Form formToLoad)
        {
            // გავასუფთავოთ პანელი ძველი გვერდისგან
            pnlContent.Controls.Clear();

            // ფორმას ჩავუხსნათ ჩარჩოები, რომ პანელში ჩაჯდეს
            formToLoad.TopLevel = false;
            formToLoad.FormBorderStyle = FormBorderStyle.None;
            formToLoad.Dock = DockStyle.Fill;

            // ჩავსვათ პანელში და გამოვაჩინოთ
            pnlContent.Controls.Add(formToLoad);
            pnlContent.Tag = formToLoad;
            formToLoad.Show();
        }

        // "მარაგები" ღილაკზე დაჭერისას:
        private void btnInventory_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new Form1());
        }

        private void btnInventory_Click_1(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new Form1());
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new SalesForm());
        }
    }
}