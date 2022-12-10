using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace course
{
    public partial class AdminForm : Form
    {
        private readonly OleDbConnection _connection;
        public AdminForm(string name, OleDbConnection connect)
        {
            InitializeComponent();
            _connection = connect;
            updateTotalPrice();
            updateTable();

            OleDbCommand command = new OleDbCommand();
            command.Connection = connect;
            List<string> list = new List<string>();

            //AreaComboBox1
            command.CommandText = "SELECT Addresses.Area " +
                                  "FROM Addresses;";
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string area = reader[0].ToString();
                list.Add(area);
                foreach (string _area in list)
                {
                    if (comboBox1.Items.Contains(_area))
                    {

                    }
                    else
                    {
                        comboBox1.Items.Add(_area);
                    }
                }
            }
            reader.Close();
            list.Clear();

            //StreetComboBox2
            command.CommandText = "SELECT Addresses.Street " +
                                  "FROM Addresses;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string street = reader[0].ToString();
                list.Add(street);
                foreach (string _street in list)
                {
                    if (comboBox2.Items.Contains(_street))
                    {

                    }
                    else
                    {
                        comboBox2.Items.Add(_street);
                    }
                }
            }
            reader.Close();
            list.Clear();

            //NameCourierComboBox3
            command.CommandText = "SELECT Courier.NameCourier " +
                                  "FROM Courier";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string nameCourier = reader[0].ToString();
                list.Add(nameCourier);
                foreach (string _nameCourier in list)
                {
                    if (comboBox3.Items.Contains(_nameCourier))
                    {

                    }
                    else
                    {
                        comboBox3.Items.Add(_nameCourier);
                    }
                }
            }
            reader.Close();
            list.Clear();

            //DateComboBox4
            command.CommandText = "SELECT Delivery.TheDate " +
                                  "FROM Delivery;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string[] item = reader[0].ToString().Split(' ')[0].Split('.');
                string date = item[1] + "/" + item[0] + "/" + item[2];
                list.Add(date);
                foreach (string _date in list)
                {
                    if (comboBox4.Items.Contains(_date))
                    {

                    }
                    else
                    {
                        comboBox4.Items.Add(_date);
                    }
                }
            }
            reader.Close();
            list.Clear();

            //AddressComboBox5
            command.CommandText = "SELECT Addresses.Area, Addresses.Street, Addresses.House " +
                                  "FROM Addresses;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string area = reader[0].ToString();
                string street = reader[1].ToString();
                string house = reader[2].ToString();
                string address = area + " " + street + " " + house;
                list.Add(address);
                foreach (string _address in list)
                {
                    if (comboBox5.Items.Contains(_address))
                    {

                    }
                    else
                    {
                        comboBox5.Items.Add(_address);
                    }
                }
            }
            reader.Close();
            list.Clear();

            //NameProductComboBox6
            command.CommandText = "SELECT Products.NameProduct " +
                                  "FROM Products;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string nameProduct = reader[0].ToString();
                list.Add(nameProduct);
                foreach (string _nameProduct in list)
                {
                    if (comboBox6.Items.Contains(_nameProduct))
                    {

                    }
                    else
                    {
                        comboBox6.Items.Add(_nameProduct);
                    }
                }
            }
            reader.Close();
            list.Clear();

            //NameCourierComboBox7
            command.CommandText = "SELECT Courier.NameCourier " +
                                  "FROM Courier;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string nameCourier = reader[0].ToString();
                list.Add(nameCourier);
                foreach (string _nameCourier in list)
                {
                    if (comboBox7.Items.Contains(_nameCourier))
                    {

                    }
                    else
                    {
                        comboBox7.Items.Add(_nameCourier);
                    }
                }
            }
            reader.Close();
            list.Clear();


        }
        private void updateTotalPrice()
        {
            string query = $"UPDATE Products INNER JOIN Delivery ON Products.IdProduct = Delivery.IdProduct SET Delivery.TotalPrice = [Delivery.Count]*[Products.PriceForOne];";
            OleDbCommand command = new OleDbCommand(query, _connection);
            command.ExecuteNonQuery();
        }

        private void updateTable()
        {
            string query = "SELECT Delivery.IdDelivery, Addresses.Area, Addresses.Street, Addresses.House, Products.NameProduct, Delivery.Count, " +
            "Products.PriceForOne, Delivery.TotalPrice, Courier.NameCourier, Delivery.TheDate " +
            "FROM Products INNER JOIN (Courier INNER JOIN (Addresses INNER JOIN Delivery ON Addresses.IdAddress = Delivery.IdAddress) " +
            "ON Courier.IdCourier = Delivery.IdCourier) ON Products.IdProduct = Delivery.IdProduct;";
            OleDbCommand command = new OleDbCommand(query, _connection);
            OleDbDataReader reader = command.ExecuteReader();

            dataGridView1.Rows.Clear();

            while (reader.Read())
            {
                var id = reader[0];
                var area = reader[1];
                var street = reader[2];
                var house = reader[3];
                var nameProduct = reader[4];
                var count = reader[5];
                var priceForOne = reader[6];
                var totalPrice = reader[7];
                var nameCourier = reader[8];
                string[] item = reader[9].ToString().Split(' ')[0].Split('.');
                var theDate = item[1] + "/" + item[0] + "/" + item[2];
                dataGridView1.Rows.Add(id, area, street, house, nameProduct, count, priceForOne, totalPrice, nameCourier, theDate);
            }

            reader.Close();
        }

        private void resetButton(object sender, System.EventArgs e)
        {
            updateTable();
        }

        private void applyButton(object sender, System.EventArgs e)
        {
            string query = "SELECT Delivery.IdDelivery, Addresses.Area, Addresses.Street, Addresses.House, Products.NameProduct, Delivery.Count, " +
                "Products.PriceForOne, Delivery.TotalPrice, Courier.NameCourier, Delivery.TheDate " +
                "FROM Products INNER JOIN (Courier INNER JOIN (Addresses INNER JOIN Delivery ON Addresses.IdAddress = Delivery.IdAddress) " +
                "ON Courier.IdCourier = Delivery.IdCourier) ON Products.IdProduct = Delivery.IdProduct " +
                $"WHERE (";
            if (comboBox1.SelectedItem != null)
            {
                query += $"((Addresses.Area)='{comboBox1.Text}')";
            }
            if (comboBox2.SelectedItem != null)
            {
                if (query.EndsWith(")")) query += " AND";
                query += $"((Addresses.Street)='{comboBox2.Text}')";
            }
            if (comboBox3.SelectedItem != null)
            {
                if (query.EndsWith(")")) query += " AND";
                query += $"((Courier.NameCourier)='{comboBox3.Text}')";
            }
            if (comboBox4.SelectedItem != null)
            {
                if (query.EndsWith(")")) query += " AND";
                query += $"((Delivery.TheDate)=#{comboBox4.Text}#)";
            }
            query += ");";
            
            if (comboBox1.SelectedItem != null || comboBox2.SelectedItem != null || comboBox3.SelectedItem != null || comboBox4.SelectedItem != null)
            {
                OleDbCommand command = new OleDbCommand(query, _connection);
                OleDbDataReader reader = command.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (reader.Read())
                {
                    var id = reader[0];
                    var area = reader[1];
                    var street = reader[2];
                    var house = reader[3];
                    var nameProduct = reader[4];
                    var count = reader[5];
                    var priceForOne = reader[6];
                    var totalPrice = reader[7];
                    var nameCourier = reader[8];
                    string[] item = reader[9].ToString().Split(' ')[0].Split('.');
                    var theDate = item[1] + "/" + item[0] + "/" + item[2];
                    dataGridView1.Rows.Add(id, area, street, house, nameProduct, count, priceForOne, totalPrice, nameCourier, theDate);
                }

                reader.Close();
            }
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            comboBox3.SelectedItem = null;
            comboBox4.SelectedItem = null;
        }

        private void addButton(object sender, System.EventArgs e)
        {
            if(comboBox7.SelectedItem != null && comboBox5.SelectedItem != null && comboBox6.SelectedItem != null 
                && textBox1.Text != null && dateTimePicker1.Value != null)
            {
                string query = "INSERT INTO Delivery " +
                        $"VALUES('{GetIdCourier(comboBox7.Text)}', '{GetIdAddress(comboBox5.Text)}', '{GetIdProduct(comboBox6.Text)}', " +
                        $"'{textBox1.Text}', '0', '{dateTimePicker1.Value.ToString("dd/MM/yyyy")}', '{Convert.ToInt32(MaxId()) + 1}');";
                OleDbCommand command = new OleDbCommand(query, _connection);
                command.ExecuteNonQuery();
                comboBox5.SelectedItem = null;
                comboBox6.SelectedItem = null;
                comboBox7.SelectedItem = null;
                textBox1.Text = null;
                updateTotalPrice();
                updateTable();
            }
        }

        private void deleteButton(object sender, System.EventArgs e)
        {
            if (double.TryParse(textBox2.Text, out var parsedNumber))
            {
                string query = $"DELETE Delivery.IdDelivery FROM Delivery WHERE (((Delivery.IdDelivery)={parsedNumber}));";
                OleDbCommand command = new OleDbCommand(query, _connection);
                command.ExecuteNonQuery();
                updateTable();
                textBox2.Text = null;
            }
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

        public string GetIdCourier(string name)
        {
            string query = $"SELECT Courier.IdCourier FROM Courier WHERE (((Courier.NameCourier)='{name}'));";
            return ExecuteQuery(query)[0][0];
        }

        public string GetIdAddress(string _address)
        {
            string address = _address.ToString().Split(' ')[2];
            string query = $"SELECT Addresses.IdAddress FROM Addresses WHERE (((Addresses.House)={address}));";
            return ExecuteQuery(query)[0][0];
        }

        public string GetIdProduct(string name)
        {
            string query = $"SELECT Products.IdProduct FROM Products WHERE (((Products.NameProduct)='{name}'));";
            return ExecuteQuery(query)[0][0];
        }

        public string MaxId()
        {
            string query = "SELECT Max(Delivery.IdDelivery) AS [Max-IdDelivery] FROM Delivery;";
            return ExecuteQuery(query)[0][0];
        }

        private void changeButton(object sender, EventArgs e)
        {
            if (double.TryParse(textBox3.Text, out var parsedNumber))
            {
                string query = "UPDATE Delivery SET ";

                if (comboBox5.SelectedItem != null)
                {
                    query += $"Delivery.IdAddress = {GetIdAddress(comboBox5.Text)},";
                }
                if (comboBox6.SelectedItem != null)
                {
                    query += $"Delivery.IdProduct = {GetIdProduct(comboBox6.Text)},";
                }
                if (textBox1.Text != "")
                {
                    query += $"Delivery.[Count] = {textBox1.Text},";
                }
                if (comboBox7.SelectedItem != null)
                {
                    query += $"Delivery.IdCourier = {GetIdCourier(comboBox7.Text)},";
                }
                if (dateTimePicker1.Value != null)
                {
                    query += $"Delivery.TheDate = #{dateTimePicker1.Value.ToString("MM/dd/yyyy").Replace('.', '/')}#";
                }
                if (query.EndsWith(","))
                {
                    query = query.TrimEnd(',');
                }
                query += $" WHERE (((Delivery.IdDelivery)={parsedNumber}));";

                comboBox5.SelectedItem = null;
                comboBox6.SelectedItem = null;
                comboBox7.SelectedItem = null;
                textBox1.Text = null;

                OleDbCommand command = new OleDbCommand(query, _connection);
                command.ExecuteNonQuery();
                updateTotalPrice();
                updateTable();
            }
        }
    }
}
