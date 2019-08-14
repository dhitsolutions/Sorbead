using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace RamdevSales
{
    public partial class IntgrateApi : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public IntgrateApi()
        {
            InitializeComponent();
        }

        public IntgrateApi(Master master, TabControl tabControl)
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
                DataTable dt = conn.getdataset("select * from tblsmsapi where isactive=1");
                if (dt.Rows.Count > 0)
                {
                    conn.execute("UPDATE [dbo].[tblsmsapi] SET part1='" + txtpart1.Text + "',part2='" + txtpart2.Text + "',part3='" + txtpart3.Text + "',defaultmobileno='" + txtmobile.Text + "'");
                }
                else
                {
                    conn.execute("INSERT INTO [dbo].[tblsmsapi]([part1],[part2],[part3],[defaultmobileno],[isactive])VALUES('" + txtpart1.Text + "','" + txtpart2.Text + "','" + txtpart3.Text + "','" + txtmobile.Text + "','" + "1" + "')");
                }
                txtpart1.Text = "";
                txtpart2.Text = "";
                txtpart3.Text = "";
                txtmobile.Text = "";
            }
            catch
            {
            }
        }

        private void IntgrateApi_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = conn.getdataset("select * from tblsmsapi where isactive=1");
                if (dt.Rows.Count > 0)
                {
                    txtpart1.Text = dt.Rows[0]["part1"].ToString();
                    txtpart2.Text = dt.Rows[0]["part2"].ToString();
                    txtpart3.Text = dt.Rows[0]["part3"].ToString();
                    txtmobile.Text = dt.Rows[0]["defaultmobileno"].ToString();
                }
                txtpart1.Focus();
            }
            catch
            {
            }
        }

        private void txtmobile_KeyPress(object sender, KeyPressEventArgs e)
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

        private void lblconfiguresmsapi_Click(object sender, EventArgs e)
        {
            smsapi sa = new smsapi();
            sa.Show();
        }
    }
}
