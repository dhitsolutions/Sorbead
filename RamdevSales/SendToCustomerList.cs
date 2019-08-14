﻿using System;
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
    public partial class SendToCustomerList : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        OleDbSettings ods = new OleDbSettings();
        Connection conn = new Connection();
        DataTable dt = new DataTable();
        string complainID, id;
        string CustomerName;
        public SendToCustomerList()
        {
            InitializeComponent();
        }

        public SendToCustomerList(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            this.ActiveControl = btnAdd;
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmSendToCustomer u = new frmSendToCustomer(master, tabControl);
           master.AddNewTab(u);
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        ListViewItem li;
        public void binddata()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                LV.Items.Clear();
                dt = conn.getdataset("select ID,date,customername from tblsendtocustomer where isactive=1 and Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'group by ID,date,customername");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        li = LV.Items.Add(dt.Rows[i]["ID"].ToString());
                        li.SubItems.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add(dt.Rows[i]["customername"].ToString());
                    }
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
        }
        private void SendToCustomerList_Load(object sender, EventArgs e)
        {
            DataSet dtrange = conn.getdata("SELECT * FROM Company where CompanyID='" + Master.companyId + "'");
            DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            LV.Columns.Add("Send To Customer No", 100, HorizontalAlignment.Center);
            LV.Columns.Add("ComplainDate", 200, HorizontalAlignment.Center);
            LV.Columns.Add("Supplier Name", 400, HorizontalAlignment.Center);
            btnAdd.Focus();
            DTPFrom.CustomFormat = Master.dateformate;
            DTPTo.CustomFormat = Master.dateformate;
            binddata();
            this.ActiveControl = btnAdd;
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            binddata();
        }

        private void txtcomplainname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtcomplainname.Text))
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    LV.Items.Clear();
                    dt = conn.getdataset("select id,date,customername from tblsendtocustomer where isactive=1 and id like'%" + txtcomplainname.Text + "%'group by id,date,customername");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            li = LV.Items.Add(dt.Rows[i]["id"].ToString());
                            li.SubItems.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt.Rows[i]["customername"].ToString());
                        }
                    }
                }
                else
                {
                    binddata();
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }

        }

        private void txtdate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtdate.Text))
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    LV.Items.Clear();
                    dt = conn.getdataset("select id,date,customername from tblsendtocustomer where isactive=1 and date like'%" + txtdate.Text + "%'group by id,date,customername");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            li = LV.Items.Add(dt.Rows[i]["id"].ToString());
                            li.SubItems.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt.Rows[i]["customername"].ToString());
                        }
                    }
                }
                else
                {
                    binddata();
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
        }

        private void txtsuppliername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtsuppliername.Text))
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    LV.Items.Clear();
                    dt = conn.getdataset("select id,date,customername from tblsendtocustomer where isactive=1 and customername like'%" + txtsuppliername.Text + "%'group by id,date,customername");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            li = LV.Items.Add(dt.Rows[i]["id"].ToString());
                            li.SubItems.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                            li.SubItems.Add(dt.Rows[i]["customername"].ToString());
                        }
                    }
                }
                else
                {
                    binddata();
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
        }
        public static string iid;
        public void open()
        {
            try
            {
                try
                {
                    this.Enabled = false;
                    iid = LV.Items[LV.FocusedItem.Index].SubItems[0].Text;

                    frmSendToCustomer dlg = new frmSendToCustomer(master, tabControl);


                    dlg.Update(1, iid);
                    master.AddNewTab(dlg);
                    // dlg.Show();
                }
                finally
                {
                    this.Enabled = true;
                }
            }
            catch
            {
            }
        }
        private void LV_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            open();
        }

        private void LV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                open();
            }
        }

        private void BtnViewReport_Enter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_Leave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_MouseEnter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_MouseLeave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
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

        private void btnAdd_Enter(object sender, EventArgs e)
        {
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.BackColor = System.Drawing.Color.FromArgb(9, 106, 3);
            btnAdd.ForeColor = System.Drawing.Color.White;
        }

        private void btnAdd_Leave(object sender, EventArgs e)
        {
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnAdd.ForeColor = System.Drawing.Color.White;
        }

        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.BackColor = System.Drawing.Color.FromArgb(9, 106, 3);
            btnAdd.ForeColor = System.Drawing.Color.White;
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnAdd.ForeColor = System.Drawing.Color.White;
        }
    }
}
