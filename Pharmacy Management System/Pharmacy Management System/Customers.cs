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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            ShowCustomer();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Babashov\source\repos\Pharmacy Management System\Pharmacy Management System\PharmacyDB.mdf;Integrated Security=True");
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            this.Hide();
            Obj.Show();
        }

        private void GoDashboard_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            this.Hide();
            Obj.Show();
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

        }

        private void GoCustomers_Click(object sender, EventArgs e)
        {

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

        private void btnSelling_Click(object sender, EventArgs e)
        {
            Selling Obj = new Selling();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void ShowCustomer()
        {
            Con.Open();
            string Query = "Select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGVCustomer.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            txtCustomerName.Text = "";
            txtAddress.Text = "";
            txtMobileNo.Text = "";
            txtGender.SelectedIndex = 0;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text == "" || txtAddress.Text == "" || txtMobileNo.Text == "" || txtGender.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl(CustomerName,CustomerAddress,CustomerMobileNo,CustomerDOB,CustomerGender) values(@CN,@CA,@CMN,@CD,@CG)", Con);
                    cmd.Parameters.AddWithValue("@CN", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@CA", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@CMN", txtMobileNo.Text);
                    cmd.Parameters.AddWithValue("@CD", txtDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", txtGender.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Added Successfully");
                    Con.Close();
                    ShowCustomer();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void DGVCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCustomerName.Text = DGVCustomer.SelectedRows[0].Cells[1].Value.ToString();
            txtAddress.Text = DGVCustomer.SelectedRows[0].Cells[2].Value.ToString();
            txtMobileNo.Text = DGVCustomer.SelectedRows[0].Cells[3].Value.ToString();
            txtDOB.Text = DGVCustomer.SelectedRows[0].Cells[4].Value.ToString();
            txtGender.SelectedItem = DGVCustomer.SelectedRows[0].Cells[5].Value.ToString();
            if (txtCustomerName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(DGVCustomer.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Customer");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from CustomerTbl where CustomerId=@CKey", Con);
                    cmd.Parameters.AddWithValue("@Ckey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted Successfully");
                    Con.Close();
                    ShowCustomer();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text == "" || txtAddress.Text == "" || txtMobileNo.Text == "" || txtGender.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update CustomerTbl Set CustomerName=@CN,CustomerAddress=@CA,CustomerMobileNo=@CMN,CustomerDOB=@CD,SellerGender=@CG,SellerPassword=@CP where SellerId=@CKey", Con);
                    cmd.Parameters.AddWithValue("@CN", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@CA", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@CMN", txtMobileNo.Text);
                    cmd.Parameters.AddWithValue("@CD", txtDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", txtGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated Successfully");
                    Con.Close();
                    ShowCustomer();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
