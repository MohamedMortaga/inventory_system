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
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace WindowsFormsApp1
{
    public partial class CustomersEdit : Form
    {
        public string conString = "Data Source=MSI;Initial Catalog=MMStart;Integrated Security=True";
        private Form currentChildForm;
        public CustomersEdit()
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

        private void iconCurrentChildForm_Click_2(object sender, EventArgs e)
        {
            OpenChildForm(new Settings());
        }

        private void panelDesktop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string getMaxIdQuery = "SELECT MAX(id) FROM customer2";
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

                string q = "INSERT INTO customer2(id, name, address) VALUES(@id, @name, @address)";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", id.ToString());
                cmd.Parameters.AddWithValue("@name", textBox1.Text.ToString());
                cmd.Parameters.AddWithValue("@address", textBox2.Text.ToString());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Saved!");
            }
            textBox1.Clear();
            textBox2.Clear();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {

                string idToDelete = textBox3.Text.ToString();


                string deleteQuery = "DELETE FROM customer2 WHERE id = @id";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, con);
                deleteCmd.Parameters.AddWithValue("@id", idToDelete);
                int rowsAffected = deleteCmd.ExecuteNonQuery();


                string updateIdsQuery = "UPDATE customer2 SET id = id - 1 WHERE id > @idToDelete";
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
                textBox3.Clear();
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            int id = int.Parse(textBox5.Text);


            string selectQuery = "SELECT * FROM customer2 WHERE id = @id";
            SqlCommand selectCmd = new SqlCommand(selectQuery, con);
            selectCmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = selectCmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();

                string updateQuery = "UPDATE customer2 SET ";
                if (!string.IsNullOrEmpty(textBox8.Text))
                {
                    updateQuery += "name = @name, ";
                }
                if (!string.IsNullOrEmpty(textBox7.Text))
                {
                    updateQuery += "address = @address, ";
                }
  
                updateQuery = updateQuery.TrimEnd(',', ' ');

                updateQuery += " WHERE id = @id";

                SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                updateCmd.Parameters.AddWithValue("@id", id);
                if (!string.IsNullOrEmpty(textBox8.Text))
                {
                    updateCmd.Parameters.AddWithValue("@name", textBox8.Text);
                }
                if (!string.IsNullOrEmpty(textBox7.Text))
                {
                    updateCmd.Parameters.AddWithValue("@address", textBox7.Text);
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
            textBox8.Clear();
            textBox7.Clear();

        }
    }
}
