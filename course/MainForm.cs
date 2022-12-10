using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace course
{
    public partial class MainForm : Form
    {
        private static readonly string _connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.mdb;";
        private readonly OleDbConnection _connection;
        public MainForm()
        {
            InitializeComponent();

            _connection = new OleDbConnection(_connectString);

            _connection.Open();
        }

        private void user_Button(object sender, EventArgs e)
        {
            string query = "SELECT NameCourier FROM Courier";
            OleDbCommand command = new OleDbCommand(query, _connection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (textBox1.Text == reader[0].ToString())
                {
                    UserForm userForm = new UserForm(textBox1.Text, _connection);
                    userForm.Show();
                }
            }

            reader.Close();
        }

        private void Admin_Button(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                AdminForm adminForm = new AdminForm(textBox1.Text, _connection);
                adminForm.Show();
            }
        }
    }
}