using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Orders : Form
    {
        public string conString = "Data Source=MSI;Initial Catalog=MMStart;Integrated Security=True";
        public Orders()
        {
            InitializeComponent();

            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

        }

        private void Orders_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string idd = textBox1.Text.ToString();


                string searchQuery = "SELECT * FROM order2 WHERE id = @id";
                SqlCommand searchCmd = new SqlCommand(searchQuery, con);
                searchCmd.Parameters.AddWithValue("@id", idd);
                SqlDataReader reader = searchCmd.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        string id = reader["id"].ToString();
                        string castname = reader["castname"].ToString();
                        string castadd = reader["castadd"].ToString();
                        string proi = reader["prodid"].ToString();
                        MessageBox.Show($"ID: {id} -Product ID: {proi} -Customer Name: {castname} -Customer Address: {castadd} \nis exist");
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

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }


        private void iconButton2_Click_1(object sender, EventArgs e)
        {
            textBox2.Clear();


            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string searchQuery = "SELECT * FROM order2";
                SqlCommand searchCmd = new SqlCommand(searchQuery, con);
                SqlDataReader reader = searchCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    StringBuilder records = new StringBuilder();

                    while (reader.Read())
                    {
                        string id = reader["id"].ToString();
                        string castname = reader["castname"].ToString();
                        string castadd = reader["castadd"].ToString();
                        string proi = reader["prodid"].ToString();
                        string cost = reader["cost"].ToString();
                        records.AppendLine($"ID: {id} -Product ID: {proi} -Customer Name: {castname} -Customer Address: {castadd} -Cost: {cost}/\n");
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
