using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RamdevSales
{
    public partial class Update : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();
        ServerConnection con = new ServerConnection();
        SqlConnection constring = new SqlConnection("Data Source=184.168.47.17;Initial Catalog=BusinessPlus;User ID=Businessplus;Password=Businessplus1!");
        public Update()
        {
            InitializeComponent();
        }

        public Update(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    master.RemoveCurrentTab();
                }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtup.Text == "allthebest")
                {
                    if (!string.IsNullOrEmpty(txtdate.Text))
                    {
                        conn.getdataset("Update updatedatabase set updatecode='" + txtdate.Text + "' where id='" + "1" + "'");
                        MessageBox.Show("Data Updated Successfully.");
                        master.RemoveCurrentTab();
                    }
                    else if (!string.IsNullOrEmpty(txtamcday.Text) && !string.IsNullOrEmpty(txtsrno.Text))
                    {
                        conn.getdataset("Update updatedatabase set amcday='" + txtamcday.Text + "' where id='" + "1" + "'");
                        con.execute("Update tblRegistrationMaster set AMCDay='" + txtamcday.Text + "' where isactive=1 and Srno='" + txtsrno.Text + "'",constring);
                        con.execute("Update Renewal set AMCDay='" + txtamcday.Text + "' where isactive=1 and SRNo='" + txtsrno.Text + "'", constring);
                        MessageBox.Show("Data Updated Successfully.");
                        master.RemoveCurrentTab();
                    }
                    else
                    {
                        MessageBox.Show("Enter AmcDay and SerialNo Or Enter Version Control");
                    }
                }
                else
                {
                    MessageBox.Show("You Are Not Authorized to Access");
                }
            }
            catch
            {
            }
        }

        private void txtdate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtamcday_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
