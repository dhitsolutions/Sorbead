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
    public partial class DynamicSMSTemplate : Form
    {
        Connection conn = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        private Master master;
        private TabControl tabControl;
        private string p;
        public static string pagename;
        public static string pageid;
        public DynamicSMSTemplate()
        {
            InitializeComponent();
        }

        public DynamicSMSTemplate(Master master, TabControl tabControl, string p, string id)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            this.p = p;
            pagename = p;
            pageid = id;
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

        private void DynamicSMSTemplate_Load(object sender, EventArgs e)
        {
            try
            {
                LVdysms.Columns.Add("", 220, HorizontalAlignment.Center);
                txtheader.Text = "Set SMS Template For "+pagename;
                if (pagename == "Account Master")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='"+pageid+"'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='ClientMaster'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Cash Receipt")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='paymentreceipt'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Bank Receipt")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='Voucher'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Cash Payment")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='paymentreceipt'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Cash Sale")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='BillMaster'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Credit Sale")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='BillMaster'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Message To Agent")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='BillMaster'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Sale Challan")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='SaleOrderMaster'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Sale Order")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='SaleOrderMaster'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Sale Return")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='BillMaster'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Purchase")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='BillMaster'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Purchase Challan")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='SaleOrderMaster'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Purchase Order")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='SaleOrderMaster'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Purchase Return")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='BillMaster'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Debit.Note")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='tbldebitcreditnote'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Credit.Note")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='tbldebitcreditnote'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Outstanding Receivable")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='ClientMaster'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                    DataTable dt2 = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='Ledger'");
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt2.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Outstanding Payble")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='ClientMaster'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                    DataTable dt2 = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='Ledger'");
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt2.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Quick Receipt")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='paymentreceipt'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "Quick Payment")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='paymentreceipt'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }
                else if (pagename == "POS")
                {
                    DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and id='" + pageid + "'");
                    txtpart1.Text = dt1.Rows[0]["message"].ToString();
                    DataTable dt = conn.getdataset("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='BillPOSMaster'");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LVdysms.Items.Add(dt.Rows[i]["column_name"].ToString());

                    }
                }

            }
            catch
            {
            }
        }

        private void txtpart1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblmsgcount.Text = "Appx. Characters   " + txtpart1.Text.Length;
                int msglenth = txtpart1.Text.Length;
                int smscount =0;
                //if (txtpart1.Text.Length <= 1 && txtpart1.Text.Length >= 160)
                //{
                //    smscount = 1;
                //}
                //if (msglenth == 160)
                //{
                //    msglenth = 0;
                //    smscount = smscount + 1;
                //}
                lblmsg.Text = "Appx No. of SMS  " + smscount;
            }
            catch
            {
            }
        }

        private void LVdysms_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtpart1.Text += "{" + LVdysms.Items[LVdysms.FocusedItem.Index].SubItems[0].Text + "}";
        }

        private void LVdysms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpart1.Text += "{" + LVdysms.Items[LVdysms.FocusedItem.Index].SubItems[0].Text + "}";
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(pageid))
                {
                    conn.execute("UPDATE [dbo].[tblsmsmenumaster] SET message='" + txtpart1.Text + "' where id='" + pageid + "'");
                    MessageBox.Show("SMS Template has been saved.");
                    master.RemoveCurrentTab();
                }
            }
            catch
            {
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(pageid))
                {
                    DialogResult dr1 = MessageBox.Show("Do you want to Delete SMS Template?", "SMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr1 == DialogResult.Yes)
                    {
                        conn.execute("UPDATE [dbo].[tblsmsmenumaster] SET message='" + "" + "' where id='" + pageid + "'");
                        MessageBox.Show("SMS Template deleted successfully");
                        master.RemoveCurrentTab();
                    }
                }
            }
            catch
            {
            }
        }
    }
}
