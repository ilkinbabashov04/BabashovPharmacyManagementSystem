using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmacy_Management_System
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            this.Hide();
            Obj.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtAdminPassword.Text == "")
            {

            }
            else if(txtAdminPassword.Text == "Admin")
            {
                Dashboard Obj = new Dashboard();
                this.Hide();
                Obj.Show();
            }
            else
            {
                MessageBox.Show("You have entered wrong Admin Password");
                txtAdminPassword.Text = "";
            }
        }
    }
}
