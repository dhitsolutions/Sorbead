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
    public partial class lockproduct : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        OleDbSettings ods = new OleDbSettings();
        Connection conn = new Connection();
        Printing prn = new Printing();
        private Master master;
        private TabControl tabControl;
        DataTable userrights = new DataTable();

        public lockproduct()
        {
            InitializeComponent();
        }

        public lockproduct(Master master, TabControl tabControl)
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
        int cnt = 0;
        private void lockproduct_Load(object sender, EventArgs e)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");

                DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;
                if (cnt == 0)
                {
                    lvproduct.Columns.Add("Order No", 100, HorizontalAlignment.Left);
                    lvproduct.Columns.Add("Date", 100, HorizontalAlignment.Left);
                    lvproduct.Columns.Add("Remarks", 250, HorizontalAlignment.Left);
                    lvproduct.Columns.Add("Item Name", 250, HorizontalAlignment.Left);
                    lvproduct.Columns.Add("Item Qty", 100, HorizontalAlignment.Left);
                    lvproduct.Columns.Add("Unit", 80, HorizontalAlignment.Left);
                    this.ActiveControl = DTPFrom;

                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[19]["p"].ToString() == "False")
                        {
                            btnprint.Enabled = false;
                        }
                    }
                }
                else
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[19]["p"].ToString() == "False")
                        {
                            btnprint.Enabled = false;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void DTPFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DTPTo.Focus();
            }
        }

        private void DTPTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsearch.Focus();
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[19]["a"].ToString() == "True")
                    {
                        lvproduct.Items.Clear();
                        DataTable dt = conn.getdataset("select * from tbllockordermaster where isactive=1 and Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lvproduct.Items.Add(dt.Rows[i]["orderno"].ToString());
                                lvproduct.Items[i].SubItems.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                                lvproduct.Items[i].SubItems.Add(dt.Rows[i]["remarks"].ToString());
                                lvproduct.Items[i].SubItems.Add(dt.Rows[i]["itemname"].ToString());
                                lvproduct.Items[i].SubItems.Add(dt.Rows[i]["qtyreq"].ToString());
                                lvproduct.Items[i].SubItems.Add(dt.Rows[i]["unit"].ToString());
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("You Don't Have Permission To View");
                        return;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print Lock Order?", "Lock Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    prn.execute("delete from printing");
                    string status;
                    status = "Lock Order";
                    for (int i = 0; i < lvproduct.Items.Count; i++)
                    {
                        DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19)VALUES";
                        qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + status + "','" + lvproduct.Items[i].SubItems[0].Text + "','" + lvproduct.Items[i].SubItems[1].Text + "','" + lvproduct.Items[i].SubItems[2].Text + "','" + lvproduct.Items[i].SubItems[3].Text + "','" + lvproduct.Items[i].SubItems[4].Text + "','" + lvproduct.Items[i].SubItems[5].Text + "','" + dt1.Rows[0]["WebSite"].ToString() + "')";
                        prn.execute(qry);
                    }
                    //  string reportName = "ProductionUtilization";
                    //  Print popup = new Print(reportName);
                    //  popup.ShowDialog();
                    //   popup.Dispose();
                }
            }
            catch
            {
            }
        }

        private void btnsearch_Enter(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = false;
            btnsearch.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            btnsearch.ForeColor = System.Drawing.Color.White;
        }

        private void btnsearch_Leave(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = true;
            btnsearch.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnsearch.ForeColor = System.Drawing.Color.White;
        }

        private void btnsearch_MouseEnter(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = false;
            btnsearch.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            btnsearch.ForeColor = System.Drawing.Color.White;
        }

        private void btnsearch_MouseLeave(object sender, EventArgs e)
        {
            btnsearch.UseVisualStyleBackColor = true;
            btnsearch.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnsearch.ForeColor = System.Drawing.Color.White;
        }

        private void btnClose_Enter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = System.Drawing.Color.White;
        }

        private void btnClose_Leave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = System.Drawing.Color.White;
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = System.Drawing.Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = System.Drawing.Color.White;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_Enter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = System.Drawing.Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_Leave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_MouseEnter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = System.Drawing.Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = System.Drawing.Color.White;
        }

        private void btnprint_MouseLeave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = System.Drawing.Color.White;
        }
    }
}
