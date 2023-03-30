using System.Data.SQLite;

namespace LearningSQL_2022_2023
{
    public partial class Form1 : Form
    {
        private string _dbPath = Path.Combine(@"C:\Users\Elchin\source\repos\LearningSQL-2022-2023", "Data", "MyDatabase.db");
        private SQLiteConnection _dbConnection = null;


        public Form1()
        {
            InitializeComponent();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(_dbPath))
            {
                SQLiteConnection.CreateFile(_dbPath);
            }

            _dbConnection = new SQLiteConnection($"Data Source={_dbPath};Version=3;");
            _dbConnection.Open();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string surname = txtSurname.Text;
            string age = txtAge.Text;

            string[] row = { name, surname, age };
            var listViewItem = new ListViewItem(row);
            lstData.Items.Add(listViewItem);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string surname = txtSurname.Text;
            string age = txtAge.Text;

            string sql = "CREATE TABLE IF NOT EXISTS People (Name TEXT, Surname TEXT, Age INT)";
            using (SQLiteCommand command = new SQLiteCommand(sql, _dbConnection))
            {
                command.ExecuteNonQuery();
            }

            sql = "INSERT INTO People (Name, Surname, Age) VALUES (@Name, @Surname, @Age)";
            using (SQLiteCommand command = new SQLiteCommand(sql, _dbConnection))
            {
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Surname", surname);
                command.Parameters.AddWithValue("@Age", age);
                command.ExecuteNonQuery();
            }
        }
    }
}