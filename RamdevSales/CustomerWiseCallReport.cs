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
    public partial class CustomerWiseCallReport : Form
    {
        Connection con = new Connection();
        private Master master;
        private TabControl tabControl;
        public CustomerWiseCallReport()
        {
            InitializeComponent();
            bindcustomer();
            LVcall.Columns.Add("complainitemid", 0, HorizontalAlignment.Left);
            LVcall.Columns.Add("itemid", 0, HorizontalAlignment.Left);
            LVcall.Columns.Add("Complain ID", 120, HorizontalAlignment.Left);
            LVcall.Columns.Add("Date", 120, HorizontalAlignment.Left);
            LVcall.Columns.Add("Item Name", 300, HorizontalAlignment.Left);
            LVcall.Columns.Add("Qty", 200, HorizontalAlignment.Left);
            LVcall.Columns.Add("Serial No", 140, HorizontalAlignment.Left);
            LVcall.Columns.Add("Remarks", 140, HorizontalAlignment.Left);
            LVcall.Columns.Add("Status", 300, HorizontalAlignment.Left);
            LVcall.Columns.Add("Description", 300, HorizontalAlignment.Left);
        }

        public CustomerWiseCallReport(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            this.master = master;
            this.tabControl = tabControl;
            InitializeComponent();
            bindcustomer();
            LVcall.Columns.Add("complainitemid", 0, HorizontalAlignment.Left);
            LVcall.Columns.Add("itemid", 0, HorizontalAlignment.Left);
            LVcall.Columns.Add("Complain ID", 100, HorizontalAlignment.Left);
            LVcall.Columns.Add("Date", 120, HorizontalAlignment.Left);
            LVcall.Columns.Add("Item Name", 300, HorizontalAlignment.Left);
            LVcall.Columns.Add("Qty", 50, HorizontalAlignment.Left);
            LVcall.Columns.Add("Serial No", 100, HorizontalAlignment.Left);
            LVcall.Columns.Add("Remarks", 140, HorizontalAlignment.Left);
            LVcall.Columns.Add("Status", 300, HorizontalAlignment.Left);
            LVcall.Columns.Add("Description", 300, HorizontalAlignment.Left);
        }
        public void bindcustomer()
        {
            DataTable dt1 = new DataTable();
            dt1=con.getdataset("select ClientID,AccountName from ClientMaster where isactive=1 order by AccountName");
            cmbaccname.ValueMember = "ClientID";
            cmbaccname.DisplayMember = "AccountName";
            cmbaccname.DataSource = dt1;
            cmbaccname.SelectedIndex = -1;
        }
        private void cmbaccname_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnok_Click(object sender, EventArgs e)
        {
            ListViewItem li;
            LVcall.Items.Clear();
            DataTable dt = con.getdataset("select cc.id as complainitemid,cc.itemid,c.id as complainid, c.date,cc.itemname,cc.qty, cc.serialno,cc.remarks, cc.status, cc.description from tblcomplainmaster c inner join tblitemcomplainmaster cc on c.id=cc.complainID where c.isactive=1 and cc.isactive=1 and customerid='" + cmbaccname.SelectedValue + "' and c.date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and c.date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by c.date");
             if (dt.Rows.Count > 0)
             {
                 for (int i = 0; i < dt.Rows.Count; i++)
                 {
                     li = LVcall.Items.Add(dt.Rows[i]["complainitemid"].ToString());
                     li.SubItems.Add(dt.Rows[i]["itemid"].ToString());
                     li.SubItems.Add(dt.Rows[i]["complainid"].ToString());
                     li.SubItems.Add(Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                     li.SubItems.Add(dt.Rows[i]["itemname"].ToString());
                     li.SubItems.Add(dt.Rows[i]["qty"].ToString());
                     li.SubItems.Add(dt.Rows[i]["serialno"].ToString());
                     li.SubItems.Add(dt.Rows[i]["remarks"].ToString());
                     li.SubItems.Add(dt.Rows[i]["status"].ToString());
                     li.SubItems.Add(dt.Rows[i]["description"].ToString());
                     
                 }
                 for (int i = 0; i < LVcall.Items.Count; i++)
                 {
                     LVcall.Items[i].UseItemStyleForSubItems = false;
                     LVcall.Items[i].SubItems[8].ForeColor = Color.White;
                     if (LVcall.Items[i].SubItems[8].Text == "Complain Received")
                     {
                         LVcall.Items[i].SubItems[8].BackColor = Color.OrangeRed;
                     }
                     else if (LVcall.Items[i].SubItems[8].Text == "Send To Company")
                     {
                         LVcall.Items[i].SubItems[8].BackColor = Color.Orange;
                     }
                     else if (LVcall.Items[i].SubItems[8].Text == "Product Received From Company")
                     {
                         LVcall.Items[i].SubItems[8].BackColor = Color.LightBlue;
                     }
                     else if (LVcall.Items[i].SubItems[8].Text == "Product Sent To Customer")
                     {
                         LVcall.Items[i].SubItems[8].BackColor = Color.YellowGreen;
                     }
                 }


             }
        }

        private void LVcall_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SendtoSerialno();
        }

        private void LVcall_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendtoSerialno();
            }
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
        private void SendtoSerialno()
        {
            String srno = LVcall.Items[LVcall.FocusedItem.Index].SubItems[6].Text;
            serialnotrackingreport sr = new serialnotrackingreport(this, master, tabControl, srno);
            master.AddNewTab(sr);
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
    }
}
