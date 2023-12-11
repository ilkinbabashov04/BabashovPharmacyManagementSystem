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
    public partial class Manufacturer : Form
    {
        public Manufacturer()
        {
            InitializeComponent();
            ShowManufacturer(); 
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Babashov\source\repos\Pharmacy Management System\Pharmacy Management System\PharmacyDB.mdf;Integrated Security=True");
        private void label3_Click(object sender, EventArgs e)
        {

        }

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
        private void ShowManufacturer()
        {
            Con.Open();
            string Query = "Select * from ManufacturerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGVManufacturer.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtManufacturerName.Text == "" || txtAddress.Text == "" || txtMobileNo.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ManufacturerTbl(ManufacturerName,ManufacturerAddress,ManufacturerMobileNo,ManufacturerDate) values(@MN,@MA,@MMN,@MD)", Con);
                    cmd.Parameters.AddWithValue("@MN", txtManufacturerName.Text);
                    cmd.Parameters.AddWithValue("@MA", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@MMN", txtMobileNo.Text);
                    cmd.Parameters.AddWithValue("@MD", txtJoinDate.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Added Successfully");
                    Con.Close();
                    ShowManufacturer();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void DGVManufacturer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtManufacturerName.Text = DGVManufacturer.SelectedRows[0].Cells[1].Value.ToString();
            txtAddress.Text = DGVManufacturer.SelectedRows[0].Cells[2].Value.ToString();
            txtMobileNo.Text = DGVManufacturer.SelectedRows[0].Cells[3].Value.ToString();
            txtJoinDate.Text = DGVManufacturer.SelectedRows[0].Cells[4].Value.ToString();
            if (txtManufacturerName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(DGVManufacturer.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Manufacturer");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from ManufacturerTbl where ManufacturerId=@MKey", Con);
                    cmd.Parameters.AddWithValue("@Mkey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Deleted Successfully");
                    Con.Close();
                    ShowManufacturer();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Reset()
        {
            txtManufacturerName.Text = "";
            txtAddress.Text = "";
            txtMobileNo.Text = "";
            Key = 0;    
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtManufacturerName.Text == "" || txtAddress.Text == "" || txtMobileNo.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update ManufacturerTbl Set ManufacturerName=@MN,ManufacturerAddress=@MA,ManufacturerMobileNo=@MMN,ManufacturerDate=@MD where ManufacturerId=@MKey", Con);
                    cmd.Parameters.AddWithValue("@MN", txtManufacturerName.Text);
                    cmd.Parameters.AddWithValue("@MA", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@MMN", txtMobileNo.Text);
                    cmd.Parameters.AddWithValue("@MD", txtJoinDate.Value.Date);
                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Updated Successfully");
                    Con.Close();
                    ShowManufacturer();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Manufacturer_Load(object sender, EventArgs e)
        {

        }
    }
}
