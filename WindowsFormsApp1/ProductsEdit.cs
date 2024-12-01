using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ProductsEdit : Form
    {
        public string conString = "Data Source=MSI;Initial Catalog=MMStart;Integrated Security=True";
        private Form currentChildForm;
        public ProductsEdit()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        private void OpenChildForm(Form childForm)
        {
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }


        private void iconCurrentChildForm_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Settings());
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panelDesktop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string getMaxIdQuery = "SELECT MAX(id) FROM product2";
                SqlCommand getMaxIdCmd = new SqlCommand(getMaxIdQuery, con);
                object maxIdResult = getMaxIdCmd.ExecuteScalar();

                int id;
                if (maxIdResult != DBNull.Value)
                {
                    id = Convert.ToInt32(maxIdResult) + 1;
                }
                else
                {
                    id = 1;
                }

                string q = "INSERT INTO product2(id, name, cost) VALUES(@id, @name, @cost)";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", id.ToString());
                cmd.Parameters.AddWithValue("@name", textBox1.Text.ToString());
                cmd.Parameters.AddWithValue("@cost", textBox2.Text.ToString());  
                cmd.ExecuteNonQuery();
                MessageBox.Show("Saved!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {

                string idToDelete = textBox3.Text.ToString();


                string deleteQuery = "DELETE FROM product2 WHERE id = @id";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, con);
                deleteCmd.Parameters.AddWithValue("@id", idToDelete);
                int rowsAffected = deleteCmd.ExecuteNonQuery();


                string updateIdsQuery = "UPDATE product2 SET id = id - 1 WHERE id > @idToDelete";
                SqlCommand updateIdsCmd = new SqlCommand(updateIdsQuery, con);
                updateIdsCmd.Parameters.AddWithValue("@idToDelete", idToDelete);
                updateIdsCmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record deleted successfully!");
                }
                else
                {
                    MessageBox.Show("No record found with the specified ID.");
                }
            }

            textBox3.Clear();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            int id = int.Parse(textBox5.Text);


            string selectQuery = "SELECT * FROM product2 WHERE id = @id";
            SqlCommand selectCmd = new SqlCommand(selectQuery, con);
            selectCmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = selectCmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();

                string updateQuery = "UPDATE product2 SET ";
                if (!string.IsNullOrEmpty(textBox7.Text))
                {
                    updateQuery += "name = @name, ";
                }
                if (!string.IsNullOrEmpty(textBox6.Text))
                {
                    updateQuery += "cost = @cost, ";
                }

                updateQuery = updateQuery.TrimEnd(',', ' ');

                updateQuery += " WHERE id = @id";

                SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                updateCmd.Parameters.AddWithValue("@id", id);
                if (!string.IsNullOrEmpty(textBox7.Text))
                {
                    updateCmd.Parameters.AddWithValue("@name", textBox7.Text);
                }
                if (!string.IsNullOrEmpty(textBox6.Text))
                {
                    updateCmd.Parameters.AddWithValue("@cost", textBox6.Text);
                }


                updateCmd.ExecuteNonQuery();
                MessageBox.Show("Record updated successfully!");
            }
            else
            {
                MessageBox.Show("Record not found!");
            }

            reader.Close();

            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }
    }
}