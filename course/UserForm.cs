using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

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
            string q = "SELECT Products.NameProduct, Delivery.Count, Delivery.TheDate, Addresses.Area, Addresses.Street, Addresses.House " +
                "FROM Products INNER JOIN (Courier INNER JOIN (Addresses INNER JOIN Delivery ON Addresses.IdAddress = Delivery.IdAddress) " +
                "ON Courier.IdCourier = Delivery.IdCourier) ON Products.IdProduct = Delivery.IdProduct ";
            string query = q;
            if (comboBox1.SelectedItem == null)
            {
                query += $"WHERE ((Addresses.Area)='{comboBox2.Text}')";
                query += $"AND ((Courier.NameCourier)='{name}');";
            }
            if (comboBox2.SelectedItem == null)
            {
                query += $"WHERE (((Delivery.TheDate)=#{comboBox1.Text}#) ";
                query += $"AND ((Courier.NameCourier)='{name}'));";
            }
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                query += $"WHERE (((Delivery.TheDate)=#{comboBox1.Text}#) ";
                query += $"AND ((Addresses.Area)='{comboBox2.Text}') ";
                query += $"AND ((Courier.NameCourier)='{name}'));";
            }
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

    }
}
