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

namespace Pharmacy_Management_System
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountMedicine();
            CountCustomer();
            CountSeller();
            SumAmount();
            GetSeller();
            SumAmountBySellers();
            GetBestCustomer();
            GetBestseller();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Babashov\source\repos\Pharmacy Management System\Pharmacy Management System\PharmacyDB.mdf;Integrated Security=True");
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Selling Obj = new Selling();
            this.Hide();
            Obj.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void CountMedicine()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from MedicineTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            LblMedicines.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountCustomer()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from CustomerTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            LblCustomers.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountSeller()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from SellerTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            LblSellers.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void SumAmount()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(BillAmount) from BillTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            LblSellAmount.Text = "Rs " + dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void SumAmountBySellers()
        {
            if (txtSellsBySeller.SelectedValue != null)
            {
                
                    Con.Open();

                    string query = "SELECT SUM(BillAmount) FROM BillTbl WHERE SellerName=@SellerName";

                    using (SqlCommand cmd = new SqlCommand(query, Con))
                    {
                        cmd.Parameters.AddWithValue("@SellerName", txtSellsBySeller.SelectedValue.ToString());

                        SqlDataAdapter sda = new SqlDataAdapter(cmd);

                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                        {
                            LblSellsBySeller.Text = "Rs " + dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            // Handle the case where no data is returned or the value is DBNull
                            LblSellsBySeller.Text = "No data available for the selected seller.";
                        }
                    }
            }
            else
            {
                // Handle the case where txtSellsBySeller.SelectedValue is null
                // Display an error message, log the issue, or take appropriate action.
            }

        }

        private void GetSeller()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select SellerName from SellerTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SellerName", typeof(string));
            dt.Load(Rdr);
            txtSellsBySeller.ValueMember = "SellerName";
            txtSellsBySeller.DataSource = dt;
            Con.Close();
        }
        private void GetBestCustomer()
        {
            try
            {
                Con.Open();
                string InnerQuery = "select Max(BillAmount) from BillTbl";
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(InnerQuery, Con);
                sda1.Fill(dt1);
                string Query = "select CustomerName from BillTbl where BillAmount = '" + dt1.Rows[0][0].ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                LblBestCustomer.Text = dt.Rows[0][0].ToString();
                Con.Close();
            }
            catch (Exception Ex)
            {
                Con.Close();
            }
        }
        private void GetBestseller()
        {
            try
            {
                Con.Open();
                string InnerQuery = "select Max(BillAmount) from BillTbl";
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(InnerQuery, Con);
                sda1.Fill(dt1);
                string Query = "select SellerName from BillTbl where BillAmount = '" + dt1.Rows[0][0].ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                LblBestSeller.Text = dt.Rows[0][0].ToString();
                Con.Close();
            }
            catch (Exception Ex)
            {
                Con.Close();
            }
        }
        private void btnManufacturer_Click(object sender, EventArgs e)
        {
            Manufacturer Obj = new Manufacturer();
            this.Hide();
            Obj.Show();
        }

        private void GoManufacturer_Click(object sender, EventArgs e)
        {
            Manufacturer Obj = new Manufacturer();
            this.Hide();
            Obj.Show();
        }

        private void btnMedicines_Click(object sender, EventArgs e)
        {
            Medicines Obj = new Medicines();
            this.Hide();
            Obj.Show();
        }

        private void GoMedicines_Click(object sender, EventArgs e)
        {
            Medicines Obj = new Medicines();
            this.Hide();
            Obj.Show();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            this.Hide();
            Obj.Show();
        }

        private void GoCustomers_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            this.Hide();
            Obj.Show();
        }

        private void btnSellers_Click(object sender, EventArgs e)
        {
            Sellers Obj = new Sellers();
            this.Hide();
            Obj.Show();
        }

        private void GoSellers_Click(object sender, EventArgs e)
        {
            Sellers Obj = new Sellers();
            this.Hide();
            Obj.Show();
        }

        private void GoSelling_Click(object sender, EventArgs e)
        {
            Selling Obj = new Selling();
            this.Hide();
            Obj.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            this.Hide();
            Obj.Show();
        }

        private void GoLogout_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            this.Hide();
            Obj.Show();
        }

        private void txtSellsBySeller_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SumAmountBySellers();
        }
    }
}
