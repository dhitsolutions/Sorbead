using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RamdevSales
{
    public partial class GSTR2 : Form
    {
        private Master master;
        private TabControl tabControl;
        DataTable userrights = new DataTable();
        Connection conn = new Connection();

        public GSTR2()
        {
            InitializeComponent();
        }

        public GSTR2(Master master, TabControl tabControl)
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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[41]["a"].ToString() == "True")
                    {
                        GST_Register_Bill_Wise1 gst = new GST_Register_Bill_Wise1(master, tabControl);
                        master.AddNewTab(gst);
                    }
                    else
                    {
                        MessageBox.Show("You Don't Have Permission to View");
                        return;
                    }
                }

            }
            catch
            {
            }
        }

        private void GSTR2_Load(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void btnclose_Enter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void btnclose_Leave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void btnclose_MouseEnter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void btnclose_MouseLeave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = System.Drawing.Color.White;
        }

        private void txtbtb_Click(object sender, EventArgs e)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[41]["a"].ToString() == "True")
                    {
                        B2B_GSTR2_ gst = new B2B_GSTR2_(master, tabControl);
                        master.AddNewTab(gst);
                    }
                    else
                    {
                        MessageBox.Show("You Don't have Permission For View");
                        return;
                    }
                }

            }
            catch
            {
            }
        }

        private void btnregdealers_Click(object sender, EventArgs e)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[41]["a"].ToString() == "True")
                    {
                        cdnr_GSTR2_ b2b = new cdnr_GSTR2_(master, tabControl);
                        master.AddNewTab(b2b);
                    }
                    else
                    {
                        MessageBox.Show("You Don't have Permission For View");
                        return;
                    }
                }
            }
            catch
            {
            }
        }

        private void btncndur_Click(object sender, EventArgs e)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[41]["a"].ToString() == "True")
                    {
                        cdnur_GSTR2_ b2b = new cdnur_GSTR2_(master, tabControl);
                        master.AddNewTab(b2b);
                    }
                    else
                    {
                        MessageBox.Show("You Don't have Permission For View");
                        return;
                    }

                }
            }
            catch
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[41]["a"].ToString() == "True")
                    {
                        B2BUR_GSTR2_ gst = new B2BUR_GSTR2_(master, tabControl);
                        master.AddNewTab(gst);
                    }
                    else
                    {
                        MessageBox.Show("You Don't have Permission For View");
                        return;
                    }
                }

            }
            catch
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[41]["a"].ToString() == "True")
                {
                    hsnreport_GSTR2_ gst = new hsnreport_GSTR2_(master, tabControl);
                    master.AddNewTab(gst);
                }
                else
                {
                    MessageBox.Show("You Don't have Permission For View");
                    return;
                }
            }
        }
    }
}
