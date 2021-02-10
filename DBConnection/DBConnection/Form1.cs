﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace DBConnection
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection();
        // string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=true";

        public Form1()
        {
            InitializeComponent();
            this.connection.StateChange += new StateChangeEventHandler(this.connection_StateChange);
        }

        static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            ConnectionStringSettings settings =
            ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
                returnValue = settings.ConnectionString;
            return returnValue;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void connection_StateChange(object sender, StateChangeEventArgs e)
        {
            подключитьсяКБДToolStripMenuItem.Enabled =
            (e.CurrentState == ConnectionState.Closed);
            асинхронноеПодключениеКБДToolStripMenuItem.Enabled =
            (e.CurrentState == ConnectionState.Closed);
            отключитьсяОтБДToolStripMenuItem.Enabled =
            (e.CurrentState == ConnectionState.Open);
        }

        private void подключитьсяКБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    MessageBox.Show("Соединение с базой данных " +
                   connection.Database + " выполнено успешно " + "\nСервер: " +
                   connection.DataSource);
                }
                else
                    MessageBox.Show("Соединение с базой данных уже установлено");
            }
            // catch
            // {
            //   MessageBox.Show("Ошибка соединения с базой данных");
            // }
            catch (SqlException XcpSQL)
            {
                foreach (SqlError se in XcpSQL.Errors)
                {
                    MessageBox.Show(se.Message, "Источник ошибки: " + se.Source,
                    MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            {
                MessageBox.Show(Xcp.Message, "Unexpected Exception",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void отключитьсяОтБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                MessageBox.Show("Соединение с базой данных закрыто");
            }
            else
                MessageBox.Show("Соединение с базой данных уже закрыто");
        }

        private async void асинхронноеПодключениеКБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = connectionString;
                    await connection.OpenAsync();
                    MessageBox.Show("Соединение с базой данных " +
                   connection.Database + " выполнено успешно " + "\nСервер: " +
                   connection.DataSource);
                }

                else
                    MessageBox.Show("Соединение с базой данных уже установлено");
            }
            catch
            {
                MessageBox.Show("Ошибка соединения с базой данных");
            }
        }

        private void списокПодключенийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    string str = String.Format("Name ={0}\nProviderName = {1}\nConnectionString = {2} ", cs.Name,
               cs.ProviderName, cs.ConnectionString);
                    MessageBox.Show(str, "Параметры подключений");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (connection)
            {


                if (connection.State == ConnectionState.Closed)
                {
                    MessageBox.Show("Сначала подключитесь к базе");
                    return;
                }
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) FROM Products";
                try
                {
                    int number = (int)command.ExecuteScalar();
                    label1.Text = number.ToString();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int number =
               WorkWithDataBase.ExecuteScalarMetod(connectionString, "SELECT COUNT(*) FROM Products");

                label2.Text = number.ToString();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //SqlCommand command = new SqlCommand("SELECT ProductName FROM Products", connection);
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT ProductName, UnitPrice, QuantityPerUnit FROM Products";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //listView1.Items.Add(reader["ProductName"].ToString());
                        ListViewItem newItem = listView1.Items.Add(reader["ProductName"].ToString());
                        //newItem.SubItems.Add(reader["UnitPrice"].ToString());
                        newItem.SubItems.Add(reader.GetDecimal(1).ToString());
                        newItem.SubItems.Add(reader["QuantityPerUnit"].ToString());
                    }
                }
                {
                    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }

            }
        }

    }
}

