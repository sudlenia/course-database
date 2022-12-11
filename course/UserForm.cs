using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace course
{
    public partial class UserForm : Form
    {
        private readonly OleDbConnection _connection;
        public string name;
        public UserForm(string name, OleDbConnection connect)
        {
            InitializeComponent();
            this.name = name;
            _connection = connect;
            updateCouriers();


            OleDbCommand command = new OleDbCommand();
            command.Connection = connect;
            command.CommandText = "SELECT Delivery.TheDate " +
                "FROM Courier INNER JOIN (Products INNER JOIN (Addresses INNER JOIN Delivery ON Addresses.IdAddress = Delivery.IdAddress) " +
                "ON Products.IdProduct = Delivery.IdProduct) ON Courier.IdCourier = Delivery.IdCourier " +
                "WHERE (((Courier.NameCourier)= ";
            command.CommandText += $"'{name}'));";
            OleDbDataReader reader = command.ExecuteReader();
            List<string> list = new List<string>();
            while (reader.Read())
            {
                string[] item = reader[0].ToString().Split(' ')[0].Split('.');
                string date = item[1] + "/" + item[0] + "/" + item[2];
                list.Add(date);
                foreach (string _date in list)
                {
                    if (comboBox1.Items.Contains(_date))
                    {

                    }
                    else
                    {
                        comboBox1.Items.Add(_date);
                    }
                }
            }
            reader.Close();
            list.Clear();

            command.CommandText = "SELECT Addresses.Area " +
                "FROM Products INNER JOIN (Courier INNER JOIN (Addresses INNER JOIN Delivery ON Addresses.IdAddress = Delivery.IdAddress) " +
                "ON Courier.IdCourier = Delivery.IdCourier) ON Products.IdProduct = Delivery.IdProduct " +
                "WHERE (((Courier.NameCourier)= ";
            command.CommandText += $"'{name}'));";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string area = reader[0].ToString();
                list.Add(area);
                foreach (string _area in list)
                {
                    if (comboBox2.Items.Contains(_area))
                    {

                    }
                    else
                    {
                        comboBox2.Items.Add(_area);
                    }
                }
            }
        }



        private void updateCouriers(string q = "SELECT Products.NameProduct, Delivery.Count, Delivery.TheDate, Addresses.Area, Addresses.Street, Addresses.House, Courier.NameCourier " +
                                               "FROM Products INNER JOIN (Courier INNER JOIN (Addresses INNER JOIN Delivery ON Addresses.IdAddress = Delivery.IdAddress) " +
                                               "ON Courier.IdCourier = Delivery.IdCourier) ON Products.IdProduct = Delivery.IdProduct " +
                                               "WHERE (((Courier.NameCourier)= ")
        {
            string query = q + $"'{name}'));";
            OleDbCommand command = new OleDbCommand(query, _connection);
            OleDbDataReader reader = command.ExecuteReader();

            dataGridView1.Rows.Clear();

            while (reader.Read())
            {
                var nameProduct = reader[0];
                var count = reader[1];
                string[] item = reader[2].ToString().Split(' ')[0].Split('.');
                var theDate = item[1] + "/" + item[0] + "/" + item[2];
                var area = reader[3];
                var street = reader[4];
                var house = reader[5];
                dataGridView1.Rows.Add(nameProduct, count, theDate, area, street, house);
            }

            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT Products.NameProduct, Delivery.Count, Delivery.TheDate, Addresses.Area, Addresses.Street, Addresses.House " +
                            "FROM Products INNER JOIN (Courier INNER JOIN (Addresses INNER JOIN Delivery ON Addresses.IdAddress = Delivery.IdAddress) " +
                            "ON Courier.IdCourier = Delivery.IdCourier) ON Products.IdProduct = Delivery.IdProduct " +
                            "WHERE (";
            if (comboBox1.SelectedItem != null)
            {
                query += $"((Delivery.TheDate)=#{comboBox1.Text}#)";
            }
            if (comboBox2.SelectedItem != null)
            {
                if (query.EndsWith(")")) query += " AND";
                query += $"((Addresses.Area)='{comboBox2.Text}')";
            }
            query += $" AND ((Courier.NameCourier)='{name}'));";
            if (comboBox1.SelectedItem != null || comboBox2.SelectedItem != null)
            {
                comboBox1.SelectedItem = null;
                comboBox2.SelectedItem = null;
                OleDbCommand command = new OleDbCommand(query, _connection);
                OleDbDataReader reader = command.ExecuteReader();

                dataGridView1.Rows.Clear();

                while (reader.Read())
                {
                    var nameProduct = reader[0];
                    var count = reader[1];
                    var theDate = reader[2];
                    var area = reader[3];
                    var street = reader[4];
                    var house = reader[5];
                    dataGridView1.Rows.Add(nameProduct, count, theDate, area, street, house);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime Time = DateTime.Now;
            string _Time = Time.ToString().Split(' ')[0].Replace('.', '/');
            string q = "SELECT Products.NameProduct, Delivery.Count, Delivery.TheDate, Addresses.Area, Addresses.Street, Addresses.House, Courier.NameCourier " +
                                               "FROM Products INNER JOIN (Courier INNER JOIN (Addresses INNER JOIN Delivery ON Addresses.IdAddress = Delivery.IdAddress) " +
                                               "ON Courier.IdCourier = Delivery.IdCourier) ON Products.IdProduct = Delivery.IdProduct " +
                                               "WHERE (((Delivery.TheDate)= ";
            string query = q + $"#{_Time}#) ";
            query += $"AND ((Courier.NameCourier)='{name}'));";
            OleDbCommand command = new OleDbCommand(query, _connection);
            OleDbDataReader reader = command.ExecuteReader();

            dataGridView1.Rows.Clear();

            while (reader.Read())
            {
                var nameProduct = reader[0];
                var count = reader[1];
                var theDate = reader[2];
                var area = reader[3];
                var street = reader[4];
                var house = reader[5];
                dataGridView1.Rows.Add(nameProduct, count, theDate, area, street, house);
            }

            reader.Close();
        }

        public List<List<string>> ExecuteQuery(string query)
        {
            List<List<string>> data = new List<List<string>>();

            object[] meta = new object[10];
            bool read;

            OleDbCommand command = new OleDbCommand(query, _connection);
            OleDbDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                do
                {
                    List<string> row = new List<string>();
                    int NumberOfColums = reader.GetValues(meta);
                    for (int i = 0; i < NumberOfColums; i++)
                    {
                        row.Add(meta[i].ToString() ?? "");
                    }
                    data.Add(row);
                    read = reader.Read();
                } while (read == true);
            }
            reader.Close();

            return data;
        }

        private void MapBtn_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку адреса!", "Error!");
                return;
            }
            var row = dataGridView1.SelectedRows[0];

            string area = row.Cells[3].Value?.ToString() ?? "";
            string street = row.Cells[4].Value?.ToString() ?? "";
            string house = row.Cells[5].Value?.ToString() ?? "";

            string query = $"SELECT Addresses.X, Addresses.Y " +
                $"FROM Addresses " +
                $"WHERE (((Addresses.Area)=\"{area}\") AND ((Addresses.Street)=\"{street}\") AND ((Addresses.House)={int.Parse(house)})); ";

            List<string> coords = ExecuteQuery(query)[0];

            string X = coords[0];
            string Y = coords[1];

            if (X.Length == 0 || Y.Length == 0)
            {
                MessageBox.Show("Не получилось найти координаты адреса", "Error!");
                return;
            }

            Map map = new Map(double.Parse(X.Replace(".", ",")), double.Parse(Y.Replace(".", ",")));
            map.ShowDialog();
        }
    }
}
