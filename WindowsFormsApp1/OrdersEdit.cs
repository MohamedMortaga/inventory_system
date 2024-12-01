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
    public partial class OrdersEdit : Form
    {
        public string conString = "Data Source=MSI;Initial Catalog=MMStart;Integrated Security=True";
        private Form currentChildForm;
        public OrdersEdit()
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



        private void iconCurrentChildForm_Click(object sender, EventArgs e)
        {

            OpenChildForm(new Settings());
        }

        private void panelDesktop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string getMaxIdQuery = "SELECT MAX(id) FROM order2";
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

                string q = "INSERT INTO order2(id, castname, castadd,prodid,cost) VALUES(@id,@cname, @cadd, @proid,@cost)";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", id.ToString());
                cmd.Parameters.AddWithValue("@cname", textBox2.Text.ToString());
                cmd.Parameters.AddWithValue("@cadd", textBox3.Text.ToString());
                cmd.Parameters.AddWithValue("@proid", textBox6.Text.ToString());
                cmd.Parameters.AddWithValue("@cost", textBox5.Text.ToString());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Saved!");
            }
            textBox2.Clear();
            textBox3.Clear();
            textBox6.Clear();
            textBox5.Clear();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {

                string idToDelete = textBox1.Text.ToString();
                string deleteQuery = "DELETE FROM order2 WHERE id = @id";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, con);
                deleteCmd.Parameters.AddWithValue("@id", idToDelete);
                int rowsAffected = deleteCmd.ExecuteNonQuery();


                string updateIdsQuery = "UPDATE order2 SET id = id - 1 WHERE id > @idToDelete";
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
                textBox1.Clear();
            }


        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                int id = int.Parse(textBox4.Text); 

           
                string selectQuery = "SELECT * FROM order2 WHERE id = @id";
                SqlCommand selectCmd = new SqlCommand(selectQuery, con);
                selectCmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = selectCmd.ExecuteReader();

                if (reader.Read()) 
                {
                    reader.Close(); 

                    string updateQuery = "UPDATE order2 SET ";
                    if (!string.IsNullOrEmpty(textBox10.Text))
                    {
                        updateQuery += "castname = @cname, ";
                    }
                    if (!string.IsNullOrEmpty(textBox9.Text))
                    {
                        updateQuery += "castadd = @cadd, ";
                    }
                    if (!string.IsNullOrEmpty(textBox8.Text))
                    {
                        updateQuery += "prodid = @proid, ";
                    }
                    if (!string.IsNullOrEmpty(textBox5.Text))
                    {
                        updateQuery += "cost = @cost, ";
                    }
                    updateQuery = updateQuery.TrimEnd(',', ' '); 

                    updateQuery += " WHERE id = @id";

                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@id", id);
                    if (!string.IsNullOrEmpty(textBox10.Text))
                    {
                        updateCmd.Parameters.AddWithValue("@cname", textBox10.Text);
                    }
                    if (!string.IsNullOrEmpty(textBox9.Text))
                    {
                        updateCmd.Parameters.AddWithValue("@cadd", textBox9.Text);
                    }
                    if (!string.IsNullOrEmpty(textBox8.Text))
                    {
                        updateQuery += "prodid = @proid, ";
                    }
                    if (!string.IsNullOrEmpty(textBox5.Text))
                    {
                        updateQuery += "cost = @cost, ";
                    }

                    updateCmd.ExecuteNonQuery();
                    MessageBox.Show("Record updated successfully!");
                }
                else
                {
                    MessageBox.Show("Record not found!");
                }

                reader.Close(); 

                textBox4.Clear();
                textBox10.Clear();
                textBox9.Clear();
                textBox8.Clear();
                textBox5.Clear();


            }
        }
    }
}
