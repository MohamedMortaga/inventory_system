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
    public partial class Dashboard : Form
    {
        public string conString = "Data Source=MSI;Initial Catalog=MMStart;Integrated Security=True";
        public Dashboard()
        {
            InitializeComponent();
            
        
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string getMaxIdQuery = "SELECT MAX(id) FROM customer2";
                SqlCommand getMaxIdCmd = new SqlCommand(getMaxIdQuery, con);
                object maxIdResult = getMaxIdCmd.ExecuteScalar();

         
                if (maxIdResult != DBNull.Value)
                {
                    label6.Text = Convert.ToString(Convert.ToInt32(maxIdResult));
                }
                else
                {
                    label6.Text = "0";
                }



                string getMaxIdQuery1 = "SELECT MAX(id) FROM order2";
                SqlCommand getMaxIdCmd1 = new SqlCommand(getMaxIdQuery1, con);
                object maxIdResult1 = getMaxIdCmd1.ExecuteScalar();


                if (maxIdResult1 != DBNull.Value)
                {
                    label7.Text = Convert.ToString(Convert.ToInt32(maxIdResult1));
                }
                else
                {
                    label7.Text = "0";
                }



                string getMaxIdQuery2 = "SELECT MAX(id) FROM product2";
                SqlCommand getMaxIdCmd2 = new SqlCommand(getMaxIdQuery2, con);
                object maxIdResult2 = getMaxIdCmd2.ExecuteScalar();


                if (maxIdResult2 != DBNull.Value)
                {
                    label5.Text = Convert.ToString(Convert.ToInt32(maxIdResult2));
                }
                else
                {
                    label5.Text = "0";
                }


                string getTotalCostQuery = "SELECT SUM(cost) FROM order2";
                SqlCommand getTotalCostCmd = new SqlCommand(getTotalCostQuery, con);
                object totalCostResult = getTotalCostCmd.ExecuteScalar();

                
                if (totalCostResult != DBNull.Value)
                {
                    decimal totalCost = Convert.ToDecimal(totalCostResult);
                    label4.Text = totalCost.ToString();
                }
                else
                {
                    label4.Text = "0";
                }
            }
        }
    }
}
