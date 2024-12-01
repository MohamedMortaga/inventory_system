using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WindowsFormsApp1
{
    public partial class Customers : Form
    {
        public string conString = "Data Source=MSI;Initial Catalog=MMStart;Integrated Security=True";
        public Customers()
        {
            InitializeComponent();
     
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Customers_Load(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string idd = textBox1.Text.ToString();


                string searchQuery = "SELECT * FROM customer2 WHERE id = @id";
                SqlCommand searchCmd = new SqlCommand(searchQuery, con);
                searchCmd.Parameters.AddWithValue("@id", idd);
                SqlDataReader reader = searchCmd.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        string id = reader["id"].ToString();
                        string name = reader["name"].ToString();
                        string address = reader["address"].ToString();
                        MessageBox.Show($"ID: {id} -Customer Address: {address} -Customer Name: {name} \nis exist");
                    }
                }
                else
                {

                    MessageBox.Show("No record found with the specified ID.");
                }

                reader.Close();
                textBox1.Clear();
            }
        }

       

        private void iconButton2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();


            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string searchQuery = "SELECT * FROM customer2";
                SqlCommand searchCmd = new SqlCommand(searchQuery, con);
                SqlDataReader reader = searchCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    StringBuilder records = new StringBuilder();

                    while (reader.Read())
                    {
                        string id = reader["id"].ToString();
                        string name = reader["name"].ToString();
                        string address = reader["address"].ToString();

                        records.AppendLine($"ID: {id} -Customer Address: {address} -Customer Name: {name} /\n");
                    }

                    textBox2.Text = records.ToString();
                    MessageBox.Show("Records loaded successfully!");
                }
                else
                {
                    MessageBox.Show("No records found.");
                }

                reader.Close();
            }

        }
    }
}
