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
    public partial class serialnotracking : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();

        public serialnotracking()
        {
            InitializeComponent();
        }

        public serialnotracking(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            lvstockin.Columns.Add("Date", 100, HorizontalAlignment.Left);
            lvstockin.Columns.Add("Type", 60, HorizontalAlignment.Left);
            lvstockin.Columns.Add("Bill No", 80, HorizontalAlignment.Left);
            lvstockin.Columns.Add("Party", 100, HorizontalAlignment.Left);
            lvstockin.Columns.Add("Item", 150, HorizontalAlignment.Left);
            lvstockin.Columns.Add("CO", 100, HorizontalAlignment.Left);
            lvstockin.Columns.Add("Batch", 50, HorizontalAlignment.Left);
            lvstockin.Columns.Add("Price", 100, HorizontalAlignment.Left);
            lvstockin.Columns.Add("Dis%", 60, HorizontalAlignment.Left);
            lvstockin.Columns.Add("billno", 0, HorizontalAlignment.Left);
            lvstockin.Columns.Add("ClientID", 0, HorizontalAlignment.Left);

            lvstockout.Columns.Add("Date", 100, HorizontalAlignment.Left);
            lvstockout.Columns.Add("Type", 60, HorizontalAlignment.Left);
            lvstockout.Columns.Add("Bill No", 80, HorizontalAlignment.Left);
            lvstockout.Columns.Add("Party", 100, HorizontalAlignment.Left);
            lvstockout.Columns.Add("Item", 150, HorizontalAlignment.Left);
            lvstockout.Columns.Add("CO", 100, HorizontalAlignment.Left);
            lvstockout.Columns.Add("Batch", 50, HorizontalAlignment.Left);
            lvstockout.Columns.Add("Price", 100, HorizontalAlignment.Left);
            lvstockout.Columns.Add("Dis%", 60, HorizontalAlignment.Left);
            lvstockout.Columns.Add("billno", 0, HorizontalAlignment.Left);
            lvstockout.Columns.Add("ClientID", 0, HorizontalAlignment.Left);
            this.ActiveControl = txtserialno;
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
        private DataTable changedtclone(DataTable dt)
        {
            DataTable dtClone = dt.Clone(); //just copy structure, no data
            for (int i = 0; i < dtClone.Columns.Count; i++)
            {
                if (dtClone.Columns[i].DataType != typeof(string))
                    dtClone.Columns[i].DataType = typeof(string);
            }

            foreach (DataRow dr in dt.Rows)
            {
                dtClone.ImportRow(dr);
            }
            return dtClone;
        }
        ListViewItem li;
        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                lvstockin.Items.Clear();
                lvstockout.Items.Clear();
                if (!string.IsNullOrEmpty(txtserialno.Text))
                {
                    DataTable dtserial = conn.getdataset("select * from serials where SerialNo='" + txtserialno.Text + "' and isactive=1 and (transactionid='P' or transactionid='S' or transactionid='PR' or transactionid='SR')");
                    #region
                    if (dtserial.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtserial.Rows.Count; i++)
                        {
                            DataTable dtbill = conn.getdataset("select * from BillProductMaster where isactive=1 and serialno like '%" + txtserialno.Text + "%' and billno='" + dtserial.Rows[i]["voucherid"].ToString() + "' and Billtype='" + dtserial.Rows[i]["TransactionID"].ToString() + "'");
                            if (dtbill.Rows.Count > 0)
                            {
                                if (dtserial.Rows[i]["TransactionID"].ToString() == "P")
                                {
                                    li = lvstockin.Items.Add(Convert.ToDateTime(dtbill.Rows[0]["Bill_Run_Date"].ToString()).ToString(Master.dateformate));
                                    li.SubItems.Add(dtserial.Rows[i]["TransactionID"].ToString());
                                    li.SubItems.Add(dtserial.Rows[i]["voucherid"].ToString());
                                    li.SubItems.Add(dtserial.Rows[i]["PartyName"].ToString());
                                    li.SubItems.Add(dtserial.Rows[i]["itemname"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["per"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["batch"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["Rate"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["discountper"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["billno"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["ClientID"].ToString());
                                    txtstatus.Text = "Product Available";
                                }
                                else if (dtserial.Rows[i]["TransactionID"].ToString() == "SR")
                                {
                                    li = lvstockin.Items.Add(Convert.ToDateTime(dtbill.Rows[0]["Bill_Run_Date"].ToString()).ToString(Master.dateformate));
                                    li.SubItems.Add(dtserial.Rows[i]["TransactionID"].ToString());
                                    li.SubItems.Add(dtserial.Rows[i]["voucherid"].ToString());
                                    li.SubItems.Add(dtserial.Rows[i]["PartyName"].ToString());
                                    li.SubItems.Add(dtserial.Rows[i]["itemname"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["per"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["batch"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["Rate"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["discountper"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["billno"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["ClientID"].ToString());
                                    txtstatus.Text = "Product Available";
                                }
                                else if (dtserial.Rows[i]["TransactionID"].ToString() == "S")
                                {
                                    li = lvstockout.Items.Add(Convert.ToDateTime(dtbill.Rows[0]["Bill_Run_Date"].ToString()).ToString(Master.dateformate));
                                    li.SubItems.Add(dtserial.Rows[i]["TransactionID"].ToString());
                                    li.SubItems.Add(dtserial.Rows[i]["voucherid"].ToString());
                                    li.SubItems.Add(dtserial.Rows[i]["PartyName"].ToString());
                                    li.SubItems.Add(dtserial.Rows[i]["itemname"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["per"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["batch"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["Rate"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["discountper"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["billno"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["ClientID"].ToString());
                                    txtstatus.Text = "Product Not Available";
                                }
                                else if (dtserial.Rows[i]["TransactionID"].ToString() == "PR")
                                {
                                    li = lvstockout.Items.Add(Convert.ToDateTime(dtbill.Rows[0]["Bill_Run_Date"].ToString()).ToString(Master.dateformate));
                                    li.SubItems.Add(dtserial.Rows[i]["TransactionID"].ToString());
                                    li.SubItems.Add(dtserial.Rows[i]["voucherid"].ToString());
                                    li.SubItems.Add(dtserial.Rows[i]["PartyName"].ToString());
                                    li.SubItems.Add(dtserial.Rows[i]["itemname"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["per"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["batch"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["Rate"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["discountper"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["billno"].ToString());
                                    li.SubItems.Add(dtbill.Rows[0]["ClientID"].ToString());
                                    txtstatus.Text = "Product Not Available";
                                }
                            }
                        }
                    }
                    #endregion
                    DataTable dtserialC = conn.getdataset("select * from serials where SerialNo='" + txtserialno.Text + "' and isactive=1 and (transactionid='PC' or transactionid='SC')");
                    #region
                    if (dtserialC.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtserialC.Rows.Count; i++)
                        {
                            DataTable dtorder = conn.getdataset("select * from SaleOrderProductMaster sp inner join SaleOrderMaster so on so.billno=sp.billno and so.OrderStatus='Pending' where so.isactive=1 and sp.isactive=1 and sp.serialno like '%" + txtserialno.Text + "%' and sp.billno='" + dtserialC.Rows[i]["voucherid"].ToString() + "' and sp.Billtype='" + dtserialC.Rows[i]["TransactionID"].ToString() + "'");
                            if (dtorder.Rows.Count > 0)
                            {
                                if (dtserialC.Rows[i]["TransactionID"].ToString() == "PC")
                                {
                                    li = lvstockin.Items.Add(Convert.ToDateTime(dtorder.Rows[0]["Bill_Run_Date"].ToString()).ToString(Master.dateformate));
                                    li.SubItems.Add(dtserialC.Rows[i]["TransactionID"].ToString());
                                    li.SubItems.Add(dtserialC.Rows[i]["voucherid"].ToString());
                                    li.SubItems.Add(dtserialC.Rows[i]["PartyName"].ToString());
                                    li.SubItems.Add(dtserialC.Rows[i]["itemname"].ToString());
                                    li.SubItems.Add(dtorder.Rows[0]["per"].ToString());
                                    li.SubItems.Add(dtorder.Rows[0]["batch"].ToString());
                                    li.SubItems.Add(dtorder.Rows[0]["Rate"].ToString());
                                    li.SubItems.Add(dtorder.Rows[0]["discountper"].ToString());
                                    li.SubItems.Add(dtorder.Rows[0]["billno"].ToString());
                                    li.SubItems.Add(dtorder.Rows[0]["ClientID"].ToString());
                                    txtstatus.Text = "Product Available";
                                }
                                else if (dtserialC.Rows[i]["TransactionID"].ToString() == "SC")
                                {
                                    li = lvstockout.Items.Add(Convert.ToDateTime(dtorder.Rows[0]["Bill_Run_Date"].ToString()).ToString(Master.dateformate));
                                    li.SubItems.Add(dtserialC.Rows[i]["TransactionID"].ToString());
                                    li.SubItems.Add(dtserialC.Rows[i]["voucherid"].ToString());
                                    li.SubItems.Add(dtserialC.Rows[i]["PartyName"].ToString());
                                    li.SubItems.Add(dtserialC.Rows[i]["itemname"].ToString());
                                    li.SubItems.Add(dtorder.Rows[0]["per"].ToString());
                                    li.SubItems.Add(dtorder.Rows[0]["batch"].ToString());
                                    li.SubItems.Add(dtorder.Rows[0]["Rate"].ToString());
                                    li.SubItems.Add(dtorder.Rows[0]["discountper"].ToString());
                                    li.SubItems.Add(dtorder.Rows[0]["billno"].ToString());
                                    li.SubItems.Add(dtorder.Rows[0]["ClientID"].ToString());
                                    txtstatus.Text = "Product Not Available";
                                }
                            }
                        }
                    }
                    #endregion
                    //DataTable dtpurchase = conn.getdataset("select * from serials where SerialNo='" + txtserialno.Text + "' and TransactionID='P' or TransactionID='SR' and idactive=1");
                    //if (dtpurchase.Rows.Count > 0)
                    //{
                    //    DataTable dt11 = conn.getdataset("select Bill_Run_Date,Billtype,Bill_No,Productname,per,batch,Rate,discountper,billno from SaleOrderProductMaster where billno='" + dtpurchase.Rows[0]["VoucherID"].ToString() + "' and Billtype='" + dtpurchase.Rows[0]["TransactionID"].ToString() + "' and isactive=1");
                    //    dt11 = changedtclone(dt11);
                    //    DataTable dt = conn.getdataset("select Bill_Run_Date,Billtype,Bill_No,Productname,per,batch,Rate,discountper,billno from BillProductMaster where billno='" + dtpurchase.Rows[0]["VoucherID"].ToString() + "' and Billtype='" + dtpurchase.Rows[0]["TransactionID"].ToString() + "' and isactive=1");
                    //    dt = changedtclone(dt);
                    //    dt.Merge(dt11);
                    //    //dt.DefaultView.Sort = "[bill_Run_Date] DESC";
                    //    dt = dt.DefaultView.ToTable();
                    //    DataTable clientid = conn.getdataset("select ClientID from BillMaster where isactive=1 and billno='" + dt.Rows[0]["billno"].ToString() + "'");
                    //    DataTable partyname = conn.getdataset("select AccountName from ClientMaster where isactive=1 and ClientID='" + clientid.Rows[0]["ClientID"].ToString() + "'");

                    //    li = lvstockin.Items.Add(Convert.ToDateTime(dt.Rows[0].ItemArray[0].ToString()).ToString(Master.dateformate));
                    //    li.SubItems.Add(dt.Rows[0]["Billtype"].ToString());
                    //    li.SubItems.Add(dt.Rows[0]["billno"].ToString());
                    //    li.SubItems.Add(partyname.Rows[0]["AccountName"].ToString());
                    //    li.SubItems.Add(dt.Rows[0]["Productname"].ToString());
                    //    li.SubItems.Add(dt.Rows[0]["per"].ToString());
                    //    li.SubItems.Add(dt.Rows[0]["batch"].ToString());
                    //    li.SubItems.Add(dt.Rows[0]["Rate"].ToString());
                    //    li.SubItems.Add(dt.Rows[0]["discountper"].ToString());
                    //    li.SubItems.Add(dt.Rows[0]["billno"].ToString());
                    //    txtstatus.Text = "Product Available";
                    //}

                  
                    //DataTable dtsale = conn.getdataset("select * from serials where SerialNo='" + txtserialno.Text + "' and TransactionID='S' or TransactionID='PR' or TransactionID='SO' or TransactionID='PC'");
                    //if (dtsale.Rows.Count > 0)
                    //{
                    //    DataTable dt12 = conn.getdataset("select Bill_Run_Date,Billtype,Bill_No,Productname,per,batch,Rate,discountper,billno from SaleOrderProductMaster where billno='" + dtsale.Rows[0]["VoucherID"].ToString() + "' and Billtype='" + dtsale.Rows[0]["TransactionID"].ToString() + "' and isactive=1");
                    //    dt12 = changedtclone(dt12);
                    //    DataTable dt1 = conn.getdataset("select Bill_Run_Date,Billtype,Bill_No,Productname,per,batch,Rate,discountper,billno from BillProductMaster where billno='" + dtsale.Rows[0]["VoucherID"].ToString() + "' and Billtype='" + dtsale.Rows[0]["TransactionID"].ToString() + "' and isactive=1");
                    //    dt1 = changedtclone(dt1);
                    //    dt1.Merge(dt12);
                    //    //dt.DefaultView.Sort = "[bill_Run_Date] DESC";
                    //    dt1 = dt1.DefaultView.ToTable();
                    //    DataTable clientid1 = conn.getdataset("select ClientID from BillMaster where isactive=1 and billno='" + dt1.Rows[0]["billno"].ToString() + "'");
                    //    DataTable partyname1 = conn.getdataset("select AccountName from ClientMaster where isactive=1 and ClientID='" + clientid1.Rows[0]["ClientID"].ToString() + "'");


                    //    li = lvstockout.Items.Add(Convert.ToDateTime(dt1.Rows[0].ItemArray[0].ToString()).ToString(Master.dateformate));
                    //    li.SubItems.Add(dt1.Rows[0]["Billtype"].ToString());
                    //    li.SubItems.Add(dt1.Rows[0]["billno"].ToString());
                    //    li.SubItems.Add(partyname1.Rows[0]["AccountName"].ToString());
                    //    li.SubItems.Add(dt1.Rows[0]["Productname"].ToString());
                    //    li.SubItems.Add(dt1.Rows[0]["per"].ToString());
                    //    li.SubItems.Add(dt1.Rows[0]["batch"].ToString());
                    //    li.SubItems.Add(dt1.Rows[0]["Rate"].ToString());
                    //    li.SubItems.Add(dt1.Rows[0]["discountper"].ToString());
                    //    txtstatus.Text = "Product Not Available";

                    //}
                   
                }
                else
                {
                    MessageBox.Show("Enter Serial No");
                    this.ActiveControl = txtserialno;
                }
            }
            catch
            {
            }
        }
        public void setformstockin()
        {
            //  this.Enabled = false;
            string[] strfinalarray = new string[lvstockin.Items.Count];
            if (lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[1].Text == "P")
            {
                strfinalarray = new string[5] { "P", "C", "Purchase", "P", "" };
            }
            else if (lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[1].Text == "SR")
            {
                strfinalarray = new string[5] { "SR", "C", "SaleReturn", "SR", "" };
            }
            else if (lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[1].Text == "PC")
            {
                strfinalarray = new string[5] { "PC", "C", "Purchase Challan", "PC", "" };
            }
            //else if (lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[1].Text == "PR")
            //{
            //    strfinalarray = new string[5] { "PR", "D", "PurchaseReturn", "PR", "" };
            //}
            String str = lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[2].Text;
            DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
            DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
            DefaultSaleOrder bd1 = new DefaultSaleOrder(this, master, tabControl, strfinalarray);
            //  Sale p = new Sale(this, master, tabControl);
            if (dt1.Rows[0]["formname"].ToString() == bd.Text)
            {
                bd.updatemode(str, lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[2].Text, Convert.ToInt32(lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[10].Text), strfinalarray);
                master.AddNewTab(bd);
            }
            else if (dt1.Rows[0]["formname"].ToString() == bd1.Text)
            {
                bd1.updatemode(str, lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[2].Text, Convert.ToInt32(lvstockin.Items[lvstockin.FocusedItem.Index].SubItems[10].Text), strfinalarray);
                master.AddNewTab(bd1);
            }
        }
        public void setformstockout()
        {
            //this.Enabled = false;
            string[] strfinalarray = new string[lvstockout.Items.Count];
            if (lvstockout.Items[lvstockout.FocusedItem.Index].SubItems[1].Text == "S")
            {
                strfinalarray = new string[5] { "S", "D", "Sale", "S", "" };
            }
            else if (lvstockout.Items[lvstockout.FocusedItem.Index].SubItems[1].Text == "P")
            {
                strfinalarray = new string[5] { "P", "C", "Purchase", "P", "" };
            }
            else if (lvstockout.Items[lvstockout.FocusedItem.Index].SubItems[1].Text == "SC")
            {
                strfinalarray = new string[5] { "SC", "D", "Sale Challan", "SC", "" };
            }
            else if (lvstockout.Items[lvstockout.FocusedItem.Index].SubItems[1].Text == "PR")
            {
                strfinalarray = new string[5] { "PR", "D", "PurchaseReturn", "PR", "" };
            }
            String str = lvstockout.Items[lvstockout.FocusedItem.Index].SubItems[2].Text;
            DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
            DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
            DefaultSaleOrder bd1 = new DefaultSaleOrder(this, master, tabControl, strfinalarray);
            //  Sale p = new Sale(this, master, tabControl);
            if (dt1.Rows[0]["formname"].ToString() == bd.Text)
            {
                bd.updatemode(str, lvstockout.Items[lvstockout.FocusedItem.Index].SubItems[2].Text, Convert.ToInt32(lvstockout.Items[lvstockout.FocusedItem.Index].SubItems[10].Text), strfinalarray);
                master.AddNewTab(bd);
            }
            else if (dt1.Rows[0]["formname"].ToString() == bd1.Text)
            {
                bd1.updatemode(str, lvstockout.Items[lvstockout.FocusedItem.Index].SubItems[2].Text, Convert.ToInt32(lvstockout.Items[lvstockout.FocusedItem.Index].SubItems[10].Text), strfinalarray);
                master.AddNewTab(bd1);
            }
        }

        private void lvstockin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                setformstockin();
            }
            catch
            {
            }
        }

        private void lvstockout_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                setformstockout();
            }
            catch
            {
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnok_MouseEnter(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = false;
            btnok.BackColor = Color.FromArgb(94, 191, 174);
            btnok.ForeColor = Color.White;
        }

        private void btnok_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void btnok_MouseLeave(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = true;
            btnok.BackColor = Color.FromArgb(51, 153, 255);
            btnok.ForeColor = Color.White;
        }

        private void btnok_Enter(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = false;
            btnok.BackColor = Color.FromArgb(94, 191, 174);
            btnok.ForeColor = Color.White;
        }

        private void btnok_Leave(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = true;
            btnok.BackColor = Color.FromArgb(51, 153, 255);
            btnok.ForeColor = Color.White;
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
                //this.Close();
            }
        }

        private void btncancel_Enter(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = false;
            btncancel.BackColor = Color.FromArgb(248, 152, 94);
            btncancel.ForeColor = Color.White;
        }

        private void btncancel_Leave(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = true;
            btncancel.BackColor = Color.FromArgb(51, 153, 255);
            btncancel.ForeColor = Color.White;
        }

        private void btncancel_MouseEnter(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = false;
            btncancel.BackColor = Color.FromArgb(248, 152, 94);
            btncancel.ForeColor = Color.White;
        }

        private void btncancel_MouseLeave(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = true;
            btncancel.BackColor = Color.FromArgb(51, 153, 255);
            btncancel.ForeColor = Color.White;
        }

        private void lvstockin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    setformstockin();
                }
                catch
                {
                }
            }
        }

        private void lvstockout_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    setformstockout();
                }
                catch
                {
                }
            }
        }
    }
}
