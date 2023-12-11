using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmacy_Management_System
{
    public partial class Selling : Form
    {
        public Selling()
        {
            InitializeComponent();
            ShowMedicine();
            ShowBill();
            GetCustomer();
            LblSellerName.Text = Login.User;
            PrintDocument.PrintPage += PrintDocument_PrintPage;
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Babashov\source\repos\Pharmacy Management System\Pharmacy Management System\PharmacyDB.mdf;Integrated Security=True");
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
        private void GetCustomer()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CustomerId from CustomerTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustomerId", typeof(int));
            dt.Load(Rdr);
            txtCustomerId.ValueMember = "CustomerId";
            txtCustomerId.DataSource = dt;
            Con.Close();
        }

        private void GetCustomerName()
        {
            Con.Open();
            string Query = "SELECT * FROM CustomerTbl WHERE CustomerId = '" + txtCustomerId.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtCustomerName.Text = dr["CustomerName"].ToString();
            }
            Con.Close();
        }
            private void UpdateQnty()
        {
            try
            {
                int NewQnty = Stock - Convert.ToInt32(txtQuantity.Text);
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update MedicineTbl Set MedicineQnty=@MQ where MedicineId=@MKey", Con);
                cmd.Parameters.AddWithValue("@MQ", NewQnty);
                cmd.Parameters.AddWithValue("@MKey", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Medicine Update Successfully");
                Con.Close();
                ShowMedicine();
                //Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void ShowBill()
        {
            Con.Open();
            string Query = ("Select * from BillTbl where SellerName = '"+LblSellerName.Text+"'");
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGVTransactions.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ShowMedicine()
        {
            Con.Open();
            string Query = "Select * from MedicineTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGVMedicineLists.DataSource = ds.Tables[0];
            Con.Close();
        }
        int n = 0, GrdTotal = 0;
        private void txtCustomerId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustomerName();
        }
        int Key = 0, Stock;
        String MedName;
        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int Pos = 100; // Initial Y position for drawing
            int GrdTotal = 0; // Grand Total variable
            int n = 0; // Counter variable

            // Header
            e.Graphics.DrawString("Babashov Pharmacy", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80, Pos));
            Pos += 20; // Move down for the next line
            e.Graphics.DrawString("ID Medicine Price Quantity Total", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, Pos));
            Pos += 20; // Move down for the next line

            // Content from DataGridView
            foreach (DataGridViewRow row in DGVBill.Rows)
            {
                int MedId = Convert.ToInt32(row.Cells["Column1"].Value);
                string MedName = "" + row.Cells["Column2"].Value;
                int MedPrice = Convert.ToInt32(row.Cells["Column3"].Value);
                int MedQty = Convert.ToInt32(row.Cells["Column4"].Value);
                int MedTot = Convert.ToInt32(row.Cells["Column5"].Value);

                e.Graphics.DrawString("" + MedId, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, Pos));
                e.Graphics.DrawString("" + MedName, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, Pos));
                e.Graphics.DrawString("" + MedPrice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(128, Pos));
                e.Graphics.DrawString("" + MedQty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, Pos));
                e.Graphics.DrawString("" + MedTot, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, Pos));

                Pos += 20; // Move down for the next line
                GrdTotal += MedTot; // Accumulate Grand Total
                n++;
            }

            // Grand Total
            Pos += 60; // Move down for the next line
            e.Graphics.DrawString("Grand Total: Rs" + GrdTotal, new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(50, Pos));
            Pos += 100; // Move down for the next line

            // Footer
            e.Graphics.DrawString("***Babashov Pharmacy***", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(10, Pos));

            // Clear the DataGridView
            DGVBill.Rows.Clear();
            DGVBill.Refresh();

            // Reset variables
            Pos = 100;
            GrdTotal = 0;
            n = 0;

        }

        private void DGVMedicineLists_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMedicine.Text = DGVMedicineLists.SelectedRows[0].Cells[1].Value.ToString();
            //txtMedicineType.SelectedItem = DGVMedicineLists.SelectedRows[0].Cells[2].Value.Tostring();
            Stock = Convert.ToInt32(DGVMedicineLists.SelectedRows[0].Cells[3].Value.ToString());
            txtPrice.Text = DGVMedicineLists.SelectedRows[0].Cells[4].Value.ToString();
            //txtManufacturer.SelectedValue = DGVMedicineLists.SelectedRows[0].Cells[s].Value.ToString();
            //txtManufacturerName.Text = DGVMedicineLists.SelectedRows[0].Cells[6].Value.ToString();
            if (txtMedicine.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(DGVMedicineLists.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        int Pos = 60;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (DGVBill.Rows.Count > 0)
            {
                // Open the print dialog
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    // Set the PrintDocument object to use the selected printer
                    PrintDocument.PrinterSettings = printDialog.PrinterSettings;

                    // Call the Print method to start printing
                    PrintDocument.Print();
                }
            }
            else
            {
                MessageBox.Show("No data to print.");
            }



        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void DGVBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DGVBill.AutoGenerateColumns = false;
            // Check if columns are already created
            if (DGVBill.Columns.Count == 0)
                {
                    // Assuming your columns are of type DataGridViewTextBoxColumn. Adjust types accordingly.
                    DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
                    DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
                    DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
                    DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
                    DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();

                    // Set column names, headers, and any other properties as needed.
                    column1.Name = "Column1";
                    column1.HeaderText = "Column1 Header";

                    column2.Name = "Column2";
                    column2.HeaderText = "Column2 Header";

                    // Repeat for other columns...

                    // Add the columns to the DataGridView.
                    DGVBill.Columns.AddRange(column1, column2, column3, column4, column5);
                }

                // Example: Adding a new row based on the clicked cell
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(DGVBill);

                // Set values for each cell in the row
                newRow.Cells[0].Value = "Value1";
                newRow.Cells[1].Value = "Value2";
                newRow.Cells[2].Value = "Value3";
                newRow.Cells[3].Value = "Value4";
                newRow.Cells[4].Value = "Value5";

                DGVBill.Rows.Add(newRow);
            


        }

        private void DGVTransactions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Selling_Load(object sender, EventArgs e)
        {

        }

        int MedId, MedPrice, MedQty, MedTot;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtQuantity.Text == "" || !int.TryParse(txtQuantity.Text, out int enteredQuantity) || enteredQuantity > Stock)
            {
                MessageBox.Show("Enter a correct quantity");
            }
            else
            {
                int total = enteredQuantity * Convert.ToInt32(txtPrice.Text);

                // Check if columns are already created
                if (DGVBill.Columns.Count == 0)
                {
                    // Assuming your columns are of type DataGridViewTextBoxColumn. Adjust types accordingly.
                    DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
                    DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
                    DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
                    DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
                    DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();

                    // Set column names, headers, and any other properties as needed.
                    column1.Name = "Column1";
                    column1.HeaderText = "ID";

                    column2.Name = "Column2";
                    column2.HeaderText = "Medicine";

                    column3.Name = "Column3";
                    column3.HeaderText = "Price";

                    column4.Name = "Column4";
                    column4.HeaderText = "Quantity";

                    column5.Name = "Column5";
                    column5.HeaderText = "Total";

                    // Add the columns to the DataGridView.
                    DGVBill.Columns.AddRange(column1, column2, column3, column4, column5);
                }

                // Create a new DataGridViewRow and add cells directly
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = MedId });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = MedName });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = Convert.ToInt32(txtPrice.Text) });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = enteredQuantity });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = total });

                // Add the new row to the DataGridView
                DGVBill.Rows.Add(newRow);

                // Update the total
                GrdTotal += total;
                LblTotal.Text = "Rs " + GrdTotal;
                n++;
                UpdateQnty();
            }

        }
    }
}
