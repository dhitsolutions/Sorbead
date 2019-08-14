using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace RamdevSales
{
    public partial class SaleItemGroupWise : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        Printing prn = new Printing();
        private Master master;
        private TabControl tabControl;
        DataTable userrights = new DataTable();

        public SaleItemGroupWise()
        {
            InitializeComponent();
        }

        public SaleItemGroupWise(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }
        public void binaagent()
        {
            string qry = "";
            qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=50 order by AccountName";
            SqlCommand cmd1 = new SqlCommand(qry, con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbagentname.ValueMember = "ClientID";
            cmbagentname.DisplayMember = "AccountName";
            cmbagentname.DataSource = dt1;
            cmbagentname.SelectedIndex = -1;
        }
        public void binaagenttype()
        {
            string qry = "";
            qry = "select customertypeid,customertype from ClientMaster where isactive=1 and groupID=50 order by AccountName";
            SqlCommand cmd1 = new SqlCommand(qry, con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbagenttype.ValueMember = "customertypeid";
            cmbagenttype.DisplayMember = "customertype";
            cmbagenttype.DataSource = dt1;
            cmbagenttype.SelectedIndex = -1;
        }
        public void bindagenttypename()
        {
            try
            {
                lvagenttype.CheckBoxes = true;
                lvagenttype.Columns.Add("", 20, HorizontalAlignment.Left);
                lvagenttype.Columns.Add("id", 0, HorizontalAlignment.Left);
                lvagenttype.Columns.Add("Customer Type", 150, HorizontalAlignment.Left);
                string qry = "";
                // qry = "select distinct customertypeid,customertype from ClientMaster where isactive=1 and customertype IS NOT NULL order by customertype";
                //qry = "select Groupname from ClientMaster where isactive=1 and (groupID=99 or GroupID='" + groupid + "') order by AccountName";
                qry = "select id,customertype from AccountCustomerType where isactive=1 order by customertype";
                DataTable group = conn.getdataset(qry);
                if (group.Rows.Count > 0)
                {
                    lvagenttype.Items.Clear();
                    for (int i = 0; i <= group.Rows.Count - 1; i++)
                    {
                        lvagenttype.Items.Add("");
                        lvagenttype.Items[i].SubItems.Add(group.Rows[i]["id"].ToString());
                        lvagenttype.Items[i].SubItems.Add(group.Rows[i]["customertype"].ToString());
                    }
                }

            }
            catch
            {
            }
        }
        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
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

        private void SaleItemGroupWise_Load(object sender, EventArgs e)
        {
            userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[0]["p"].ToString() == "False")
                {
                    btngenrpt.Enabled = false;
                }
            }
            DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            LVledger.Columns.Add("Item Name", 400, HorizontalAlignment.Center);
            LVledger.Columns.Add("Total Qty", 200, HorizontalAlignment.Center);
            DTPFrom.CustomFormat = Master.dateformate;
            DTPTo.CustomFormat = Master.dateformate;
            binaagent();
            //binaagenttype();
            bindagenttypename();
            this.ActiveControl = DTPFrom;
        }
        Double debit = 0;
        Double qty = 0;
        Int32 rowid = -1;
        int i;
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            filelength = 1;
            progressBar1.Value = 0;
            i = 0;
            timer1.Interval = 1000;
            timer1.Start();
            timer1.Tick += new EventHandler(timer1_Tick);
            #region
            //            try
            //{
            //    LVledger.Items.Clear();
            //    if (cmbagentname.SelectedIndex != -1)
            //    {
            //        //  DataTable pos = conn.getdataset("select ItemName,SUM(qty) as qty from BillPOSProductMaster where isactive=1 and BillRunDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillRunDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by itemname");
            //        //  pos = changedtclone(pos);
            //        for (int j = 0; j < lvagenttype.Items.Count; j++)
            //        {
            //            if (Convert.ToBoolean(lvagenttype.Items[j].Checked) == true)
            //            {
            //                DataTable sale = conn.getdataset("select pp.Productname as ItemName,SUM(pp.qty) qty from BillProductMaster pp inner join BillMaster b on b.billno=pp.billno inner join ClientMaster c on c.ClientID=b.ClientID where c.isactive=1 and pp.isactive=1 and pp.Billtype='S'and b.agentID='" + cmbagentname.SelectedValue + "' and c.customertypeid='" + lvagenttype.Items[j].SubItems[1].Text + "' and pp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and pp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by pp.productname");
            //                //  sale = changedtclone(sale);
            //                // sale.Merge(pos);
            //              //  LVledger.Items.Clear();
            //                debit = 0;
            //                qty = 0;
            //                for (int i = 0; i < sale.Rows.Count; i++)
            //                {
            //                    bool exists = false;
            //                    foreach (ListViewItem item in LVledger.Items)
            //                    {
            //                        for (int b = 0; b < item.SubItems.Count; b++)
            //                        {
            //                            string itemname = item.SubItems[0].Text;
            //                            if (sale.Rows[i]["ItemName"].ToString() == itemname)
            //                            {
            //                                exists = true;
            //                                rowid = item.Index;
            //                                qty = Convert.ToDouble(item.SubItems[1].Text);
            //                            }
            //                        }
            //                    }
            //                    if (!exists)
            //                    {
            //                        ListViewItem li;
            //                        li = LVledger.Items.Add(sale.Rows[i]["ItemName"].ToString());
            //                        li.SubItems.Add(sale.Rows[i]["qty"].ToString());
            //                        // debit += Convert.ToDouble(sale.Rows[i]["qty"].ToString());
            //                    }
            //                    else
            //                    {
            //                        Double newqty = qty + Convert.ToDouble(sale.Rows[i]["qty"].ToString());
            //                        string aqty = Convert.ToString(newqty);
            //                        LVledger.Items[rowid].SubItems[1].Text = aqty;
            //                        // debit += Convert.ToDouble(aqty);
            //                    }

            //                }
            //                if (LVledger.Items.Count > 0)
            //                {
            //                    for (int i = 0; i < LVledger.Items.Count; i++)
            //                    {

            //                        debit += Convert.ToDouble(LVledger.Items[i].SubItems[1].Text);
            //                    }
            //                }
            //                txttotalqty.Text = debit.ToString("N2");
            //            }
            //        }
            //    }
            //    else
            //    {
            //        DataTable sale = conn.getdataset("select pp.Productname as ItemName,SUM(pp.qty) qty from BillProductMaster pp inner join BillMaster b on b.billno=pp.billno inner join ClientMaster c on c.ClientID=b.ClientID where c.isactive=1 and pp.isactive=1 and pp.Billtype='S' and pp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and pp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by pp.productname");
            //        //  sale = changedtclone(sale);
            //        // sale.Merge(pos);
            //        //  LVledger.Items.Clear();
            //        debit = 0;
            //        qty = 0;
            //        for (int i = 0; i < sale.Rows.Count; i++)
            //        {
            //            bool exists = false;
            //            foreach (ListViewItem item in LVledger.Items)
            //            {
            //                for (int b = 0; b < item.SubItems.Count; b++)
            //                {
            //                    string itemname = item.SubItems[0].Text;
            //                    if (sale.Rows[i]["ItemName"].ToString() == itemname)
            //                    {
            //                        exists = true;
            //                        rowid = item.Index;
            //                        qty = Convert.ToDouble(item.SubItems[1].Text);
            //                    }
            //                }
            //            }
            //            if (!exists)
            //            {
            //                ListViewItem li;
            //                li = LVledger.Items.Add(sale.Rows[i]["ItemName"].ToString());
            //                li.SubItems.Add(sale.Rows[i]["qty"].ToString());
            //                // debit += Convert.ToDouble(sale.Rows[i]["qty"].ToString());
            //            }
            //            else
            //            {
            //                Double newqty = qty + Convert.ToDouble(sale.Rows[i]["qty"].ToString());
            //                string aqty = Convert.ToString(newqty);
            //                LVledger.Items[rowid].SubItems[1].Text = aqty;
            //                // debit += Convert.ToDouble(aqty);
            //            }

            //        }
            //        if (LVledger.Items.Count > 0)
            //        {
            //            for (int i = 0; i < LVledger.Items.Count; i++)
            //            {

            //                debit += Convert.ToDouble(LVledger.Items[i].SubItems[1].Text);
            //            }
            //        }
            //        txttotalqty.Text = debit.ToString("N2");
            //        //MessageBox.Show("Select Agent Name");
            //        //cmbagentname.Focus();
            //    }
            //}
            //catch
            //{
            //}
            #endregion
        }

        private void btngenrpt_Click(object sender, EventArgs e)
        {
            DialogResult dr1 = MessageBox.Show("Do you want to Print ItemWiseStock?", "ItemWiseStock", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr1 == DialogResult.Yes)
            {
                if (LVledger.Items.Count > 0)
                {
                    prn.execute("delete from printing");
                    DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1");
                    string client = conn.ExecuteScalar("select customertype from clientmaster where isactive=1 and clientID='" + cmbagentname.SelectedValue + "'");
                    for (int i = 0; i < LVledger.Items.Count; i++)
                    {
                        string Qty = "", ItemName = "", ItemFormat = "", serialno = "";
                        ItemName = LVledger.Items[i].SubItems[0].Text;
                        Qty = LVledger.Items[i].SubItems[1].Text;
                        string agenttype = "";
                        for (int j = 0; j < lvagenttype.Items.Count; j++)
                        {
                            if (Convert.ToBoolean(lvagenttype.Items[j].Checked) == true)
                            {
                                agenttype += lvagenttype.Items[j].SubItems[2].Text + ",";
                            }
                        }

                        ItemFormat = "ITEM WISE SALE FROM " + DTPFrom.Text + " TO " + DTPTo.Text;
                        serialno = Convert.ToInt32(i + 1).ToString();
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17)VALUES";
                        qry += "('" + ItemName + "','" + Qty + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + ItemFormat + "','" + txttotalqty.Text + "','" + serialno + "','" + cmbagentname.Text + "','" + client + "','" + agenttype + "')";
                        prn.execute(qry);
                    }
                    string reportName = "ItemWiseSale";
                    Print popup = new Print(reportName);
                    popup.ShowDialog();
                    popup.Dispose();
                }
                else
                {
                    MessageBox.Show("No Records For Print ItemWiseStock", "ItemWiseStock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                cmbagentname.Focus();
            }
        }

        private void cmbagentname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lvagenttype.Focus();
            }
        }

        private void BtnViewReport_Leave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = System.Drawing.Color.White;
        }

        private void BtnViewReport_Enter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
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

        private void btngenrpt_Enter(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = false;
            btngenrpt.BackColor = Color.FromArgb(176, 111, 193);
            btngenrpt.ForeColor = Color.White;
        }

        private void btngenrpt_Leave(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = true;
            btngenrpt.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btngenrpt.ForeColor = System.Drawing.Color.White;
        }

        private void btngenrpt_MouseEnter(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = false;
            btngenrpt.BackColor = Color.FromArgb(176, 111, 193);
            btngenrpt.ForeColor = Color.White;
        }

        private void btngenrpt_MouseLeave(object sender, EventArgs e)
        {
            btngenrpt.UseVisualStyleBackColor = true;
            btngenrpt.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btngenrpt.ForeColor = System.Drawing.Color.White;
        }

        private void btnclose_Enter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_Leave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_MouseEnter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_MouseLeave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = Color.White;
        }

        private void cmbagentname_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbagentname.SelectedIndex = 0;
                cmbagentname.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbagenttype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnViewReport.Focus();
            }
        }

        private void cmbagenttype_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbagenttype.SelectedIndex = 0;
                cmbagenttype.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void lvagenttype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnViewReport.Focus();
            }
        }

        static bool flag = false;
        int filelength = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i == 0)
            {
                _BindSaleItemGroupWiseList();
                if (flag == false)
                {
                    if (progressBar1.Value == filelength)
                    {
                        timer1.Enabled = false;   //Add this line
                        timer1.Stop();
                        i = 1;
                    }
                }
            }
        }

        private void _BindSaleItemGroupWiseList()
        {
            try
            {
                progressBar1.Maximum = 4;
                filelength = 4;

                LVledger.Items.Clear();
                progressBar1.Increment(1);

                if (cmbagentname.SelectedIndex != -1)
                {
                    progressBar1.Increment(1);

                    //  DataTable pos = conn.getdataset("select ItemName,SUM(qty) as qty from BillPOSProductMaster where isactive=1 and BillRunDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillRunDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by itemname");
                    //  pos = changedtclone(pos);
                    for (int j = 0; j < lvagenttype.Items.Count; j++)
                    {
                        if (Convert.ToBoolean(lvagenttype.Items[j].Checked) == true)
                        {
                            DataTable sale = conn.getdataset("select pp.Productname as ItemName,SUM(pp.qty) qty from BillProductMaster pp inner join BillMaster b on b.billno=pp.billno inner join ClientMaster c on c.ClientID=b.ClientID where c.isactive=1 and pp.isactive=1 and pp.Billtype='S'and b.agentID='" + cmbagentname.SelectedValue + "' and c.customertypeid='" + lvagenttype.Items[j].SubItems[1].Text + "' and pp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and pp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by pp.productname");
                            //  sale = changedtclone(sale);
                            // sale.Merge(pos);
                            //  LVledger.Items.Clear();
                            debit = 0;
                            qty = 0;
                            for (int i = 0; i < sale.Rows.Count; i++)
                            {
                                bool exists = false;
                                foreach (ListViewItem item in LVledger.Items)
                                {
                                    for (int b = 0; b < item.SubItems.Count; b++)
                                    {
                                        string itemname = item.SubItems[0].Text;
                                        if (sale.Rows[i]["ItemName"].ToString() == itemname)
                                        {
                                            exists = true;
                                            rowid = item.Index;
                                            qty = Convert.ToDouble(item.SubItems[1].Text);
                                        }
                                    }
                                }
                                if (!exists)
                                {
                                    ListViewItem li;
                                    li = LVledger.Items.Add(sale.Rows[i]["ItemName"].ToString());
                                    li.SubItems.Add(sale.Rows[i]["qty"].ToString());
                                    // debit += Convert.ToDouble(sale.Rows[i]["qty"].ToString());
                                }
                                else
                                {
                                    Double newqty = qty + Convert.ToDouble(sale.Rows[i]["qty"].ToString());
                                    string aqty = Convert.ToString(newqty);
                                    LVledger.Items[rowid].SubItems[1].Text = aqty;
                                    // debit += Convert.ToDouble(aqty);
                                }

                            }
                            if (LVledger.Items.Count > 0)
                            {
                                for (int i = 0; i < LVledger.Items.Count; i++)
                                {

                                    debit += Convert.ToDouble(LVledger.Items[i].SubItems[1].Text);
                                }
                            }
                            txttotalqty.Text = debit.ToString("N2");
                        }
                    }
                }
                else
                {
                    progressBar1.Increment(1);
                    DataTable sale = conn.getdataset("select pp.Productname as ItemName,SUM(pp.qty) qty from BillProductMaster pp inner join BillMaster b on b.billno=pp.billno inner join ClientMaster c on c.ClientID=b.ClientID where c.isactive=1 and pp.isactive=1 and pp.Billtype='S' and pp.Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and pp.Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by pp.productname");
                    //  sale = changedtclone(sale);
                    // sale.Merge(pos);
                    //  LVledger.Items.Clear();
                    debit = 0;
                    qty = 0;
                    for (int i = 0; i < sale.Rows.Count; i++)
                    {
                        bool exists = false;
                        foreach (ListViewItem item in LVledger.Items)
                        {
                            for (int b = 0; b < item.SubItems.Count; b++)
                            {
                                string itemname = item.SubItems[0].Text;
                                if (sale.Rows[i]["ItemName"].ToString() == itemname)
                                {
                                    exists = true;
                                    rowid = item.Index;
                                    qty = Convert.ToDouble(item.SubItems[1].Text);
                                }
                            }
                        }
                        if (!exists)
                        {
                            ListViewItem li;
                            li = LVledger.Items.Add(sale.Rows[i]["ItemName"].ToString());
                            li.SubItems.Add(sale.Rows[i]["qty"].ToString());
                            // debit += Convert.ToDouble(sale.Rows[i]["qty"].ToString());
                        }
                        else
                        {
                            Double newqty = qty + Convert.ToDouble(sale.Rows[i]["qty"].ToString());
                            string aqty = Convert.ToString(newqty);
                            LVledger.Items[rowid].SubItems[1].Text = aqty;
                            // debit += Convert.ToDouble(aqty);
                        }

                    }
                    if (LVledger.Items.Count > 0)
                    {
                        for (int i = 0; i < LVledger.Items.Count; i++)
                        {

                            debit += Convert.ToDouble(LVledger.Items[i].SubItems[1].Text);
                        }
                    }
                    txttotalqty.Text = debit.ToString("N2");
                    //MessageBox.Show("Select Agent Name");
                    //cmbagentname.Focus();
                }
                progressBar1.Increment(1);
            }
            catch
            {
            }
        }

    }
}
