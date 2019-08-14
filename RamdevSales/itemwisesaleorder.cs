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
using System.IO;
using ClosedXML.Excel;

namespace RamdevSales
{
    public partial class itemwisesaleorder : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        Printing prn = new Printing();
        private Master master;
        private TabControl tabControl;
        DataTable userrights = new DataTable();

        public itemwisesaleorder()
        {
            InitializeComponent();
        }

        public itemwisesaleorder(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        DataTable maindt = new DataTable();
        Double debit = 0;
        Double qty = 0;
        Int32 rowid = -1;
        private void itemwisesaleorder_Load(object sender, EventArgs e)
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
            DTPFrom.CustomFormat = Master.dateformate;
            DTPTo.CustomFormat = Master.dateformate;
            this.ActiveControl = DTPFrom;
            bindaccountdropdown();
            binditemdropdown();
        }

        private void bindaccountdropdown()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='ClientMaster' and (column_name like '%AccountName%' or column_name like '%PrintName%' or column_name like '%Groupname%' or column_name like '%Address%' or column_name like '%City%' or column_name like '%State%')", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                // dt = sql.getdataset("select * from psm");
                DataRow dr;
                dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);
                dr["column_name"] = "--Select--";
                if (dt.Rows.Count != 0)
                {
                    // cmbname.DataSource = dt.DefaultView;
                    // cmbname.ValueMember = "sp_id";
                    // cmbname.DisplayMember = "p_name";
                    // btnclr.Enabled = true;
                    // cmbname.SelectedIndex = -1;
                    drpaccount.DataSource = dt;
                    drpaccount.DisplayMember = "column_name";
                    drpaccount.ValueMember = "ClientID";
                }
            }
            catch
            {
            }
        }

        private void binditemdropdown()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where (TABLE_NAME='ProductMaster' or TABLE_NAME='Companymaster') and (column_name like '%ProductID%' or column_name like '%Product_Name%' or column_name like '%GroupName%' or column_name like '%Packing%' or column_name like '%HSN_Sac_Code%' or column_name like '%itemnumber%' or column_name like '%companyname%' )", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                // dt = sql.getdataset("select * from psm");
                DataRow dr;
                dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);
                dr["column_name"] = "--Select Column Name--";
                if (dt.Rows.Count != 0)
                {
                    // cmbname.DataSource = dt.DefaultView;
                    // cmbname.ValueMember = "sp_id";
                    // cmbname.DisplayMember = "p_name";
                    // btnclr.Enabled = true;
                    // cmbname.SelectedIndex = -1;
                    drpitems.DataSource = dt;
                    drpitems.DisplayMember = "column_name";
                    drpitems.ValueMember = "ClientID";
                }

            }
            catch
            {
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
            //try
            //{
            //    grdview.DataSource = null;
            //    DataTable bills = conn.getdataset("select * from SaleOrderMaster where isactive=1 and BillType='SO' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date");
            //    bills = changedtclone(bills);

            //    DataTable items = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
            //    items = changedtclone(items);

            //    maindt = new DataTable();
            //    maindt.Columns.Add("Items name");
            //    for (int i = 0; i < bills.Rows.Count; i++)
            //    {
            //        bool isexist = false;
            //        bool iscolexist = false;
            //        for (int j = 0; j < items.Rows.Count; j++)
            //        {
            //            string qty = conn.ExecuteScalar("select isnull(sum(qty),0) as qty from SaleOrderProductMaster where isactive=1 and Billtype='SO' and billno='" + bills.Rows[i]["billno"].ToString() + "' and productname='" + items.Rows[j]["ItemName"].ToString() + "'");

            //            if (Convert.ToDouble(qty) > 0)
            //            {
            //                isexist = true;
            //                if (iscolexist == false)
            //                {
            //                    maindt.Columns.Add(bills.Rows[i]["billno"].ToString() + Environment.NewLine + Convert.ToDateTime(bills.Rows[i]["Bill_Date"].ToString()).ToString("dd-MMM-yyyy"));
            //                    iscolexist = true;
            //                }
            //            }
            //            if (isexist == true)
            //            {
            //                break;
            //            }

            //        }
            //    }
            //    maindt.Columns.Add("Total");
            //    DataRow dr;

            //    for (int i = 0; i < items.Rows.Count; i++)
            //    {
            //        bool isexist = false;
            //        dr = maindt.NewRow();
            //        dr["Items name"] = items.Rows[i]["ItemName"].ToString();
            //        double rowstot = 0;
            //        for (int j = 0; j < bills.Rows.Count; j++)
            //        {
            //            string qty = conn.ExecuteScalar("select isnull(sum(qty),0) as qty from SaleOrderProductMaster where isactive=1 and Billtype='SO' and billno='" + bills.Rows[j]["billno"].ToString() + "' and productname='" + items.Rows[i]["ItemName"].ToString() + "'");
            //            dr[bills.Rows[j]["billno"].ToString() + Environment.NewLine + Convert.ToDateTime(bills.Rows[j]["Bill_Date"].ToString()).ToString("dd-MMM-yyyy")] = qty;
            //            rowstot += Convert.ToDouble(qty);
            //        }
            //        dr["Total"] = rowstot;
            //        maindt.Rows.Add(dr);
            //    }
            //    dr = maindt.NewRow();
            //    dr["items Name"] = "Total";
            //    for (int i = 1; i < maindt.Columns.Count; i++)
            //    {
            //        double colstot = 0;
            //        for (int j = 0; j < maindt.Rows.Count; j++)
            //        {
            //            colstot += Convert.ToDouble(maindt.Rows[j][i].ToString());
            //            dr[maindt.Columns[i].ColumnName] = colstot;
            //        }
            //    }
            //    maindt.Rows.Add(dr);

            //    grdview.DataSource = maindt;
            //}
            //catch
            //{
            //}
            #endregion
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void btngenrpt_Click(object sender, EventArgs e)
        {
            try
            {

                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        string[] files = Directory.GetFiles(fbd.SelectedPath);
                        string folderPath = fbd.SelectedPath + "\\";
                        String DateTimeName = DateTime.Now.ToString("dd_MMM_yyyy hh_mm_ss");
                        // string folderPath = "C:\\Excel\\";
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(maindt, "Sale Order");
                            // wb.Worksheets.Add(dt1, "ItemPrice");
                            wb.SaveAs(folderPath + "SaleOrder_Management" + DateTimeName + ".xlsx");
                        }
                        MessageBox.Show("Export Data Sucessfully");
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "SaleOrder Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "SaleOrder_Management" + DateTimeName + ".xlsx");
                            String pathToExecutable = "AcroRd32.exe";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bindserch();
        }
        public void bindserch()
        {
            try
            {
                if (txtaccount.Text != "" || txtitems.Text != "")
                {
                    grdview.DataSource = null;
                    string clientid = "";
                    DataTable bills = new DataTable();
                    if (drpaccount.Text == "AccountName" || drpaccount.Text == "PrintName" || drpaccount.Text == "GroupName" || drpaccount.Text == "Address" || drpaccount.Text == "City" || drpaccount.Text == "State" || drpaccount.Text == "statecode")
                    {
                        //clientid = conn.getsinglevalue("select clientid from clientmaster where isactive=1 and (AccountName like '%" + txtaccount.Text + "%' and PrintName like '%" + txtaccount.Text + "%' and GroupName like '%" + txtaccount.Text + "%' and Address like '%" + txtaccount.Text + "%' and City like '%" + txtaccount.Text + "%'and State like '%" + txtaccount.Text + "%' and statecode like '%" + txtaccount.Text + "%')");
                        bills = conn.getdataset("select * from SaleOrderMaster where clientid in (select clientid from clientmaster where isactive=1 and (AccountName like '%" + txtaccount.Text + "%' or PrintName like '%" + txtaccount.Text + "%' or GroupName like '%" + txtaccount.Text + "%' or Address like '%" + txtaccount.Text + "%' or City like '%" + txtaccount.Text + "%'or State like '%" + txtaccount.Text + "%' or statecode like '%" + txtaccount.Text + "%')) and isactive=1 and BillType='SO' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date");
                    }
                    else
                    {
                        clientid = txtaccount.Text;
                        bills = conn.getdataset("select * from SaleOrderMaster where clientid like '%" + clientid + "%' and isactive=1 and BillType='SO' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date");
                    }

                    bills = changedtclone(bills);

                    DataTable items = new DataTable();
                    string itemname = "";
                    if (drpitems.Text == "GroupName" || drpitems.Text == "Packing" || drpitems.Text == "Hsn_Sac_Code" || drpitems.Text == "itemnumber" || drpitems.Text == "ProductID")
                    {
                        //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' and Packing like '%" + txtitems.Text + "%' and Hsn_Sac_Code like '%" + txtitems.Text + "%' and itemnumber like '%" + txtitems.Text + "%')");
                        items = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' or Packing like '%" + txtitems.Text + "%' or Hsn_Sac_Code like '%" + txtitems.Text + "%' or ProductID like '%" + txtitems.Text + "%' or itemnumber like '%" + txtitems.Text + "%')) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                    }
                    else if (drpitems.Text == "companyname")
                    {
                        //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and companyid =(select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))");
                        items = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                    }
                    else
                    {
                        itemname = txtitems.Text;
                        items = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname like '%" + itemname + "%'  and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                    }

                    items = changedtclone(items);

                    maindt = new DataTable();
                    maindt.Columns.Add("Items name");
                    for (int i = 0; i < bills.Rows.Count; i++)
                    {
                        bool isexist = false;
                        bool iscolexist = false;
                        for (int j = 0; j < items.Rows.Count; j++)
                        {
                            string qty = conn.ExecuteScalar("select isnull(sum(qty),0) as qty from SaleOrderProductMaster where isactive=1 and Billtype='SO' and billno='" + bills.Rows[i]["billno"].ToString() + "' and productname='" + items.Rows[j]["ItemName"].ToString() + "'");

                            if (Convert.ToDouble(qty) > 0)
                            {
                                isexist = true;
                                if (iscolexist == false)
                                {
                                    maindt.Columns.Add(bills.Rows[i]["billno"].ToString() + Environment.NewLine + Convert.ToDateTime(bills.Rows[i]["Bill_Date"].ToString()).ToString("dd-MMM-yyyy"));
                                    iscolexist = true;
                                }
                            }
                            if (isexist == true)
                            {
                                break;
                            }

                        }
                    }
                    maindt.Columns.Add("Total");
                    DataRow dr;

                    for (int i = 0; i < items.Rows.Count; i++)
                    {
                        bool isexist = false;
                        dr = maindt.NewRow();
                        dr["Items name"] = items.Rows[i]["ItemName"].ToString();
                        double rowstot = 0;
                        for (int j = 0; j < bills.Rows.Count; j++)
                        {
                            try
                            {
                                string qty = conn.ExecuteScalar("select isnull(sum(qty),0) as qty from SaleOrderProductMaster where isactive=1 and Billtype='SO' and billno='" + bills.Rows[j]["billno"].ToString() + "' and productname='" + items.Rows[i]["ItemName"].ToString() + "'");
                                dr[bills.Rows[j]["billno"].ToString() + Environment.NewLine + Convert.ToDateTime(bills.Rows[j]["Bill_Date"].ToString()).ToString("dd-MMM-yyyy")] = qty;
                                rowstot += Convert.ToDouble(qty);
                            }
                            catch
                            {
                            }
                        }
                        dr["Total"] = rowstot;
                        maindt.Rows.Add(dr);
                    }
                    dr = maindt.NewRow();
                    dr["items Name"] = "Total";
                    for (int i = 1; i < maindt.Columns.Count; i++)
                    {
                        double colstot = 0;
                        for (int j = 0; j < maindt.Rows.Count; j++)
                        {
                            colstot += Convert.ToDouble(maindt.Rows[j][i].ToString());
                            dr[maindt.Columns[i].ColumnName] = colstot;
                        }
                    }
                    maindt.Rows.Add(dr);

                    grdview.DataSource = maindt;
                }
            }
            catch
            {
            }
        }

        static bool flag = false;
        int filelength = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i == 0)
            {
                _BindItemWiseSaleOrderList();
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

        private void _BindItemWiseSaleOrderList()
        {
            try
            {
                progressBar1.Maximum = 4;
                filelength = 4;
                progressBar1.Increment(1);

                grdview.DataSource = null;
                DataTable bills = conn.getdataset("select * from SaleOrderMaster where isactive=1 and BillType='SO' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date");
                bills = changedtclone(bills);

                DataTable items = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                items = changedtclone(items);

                maindt = new DataTable();
                maindt.Columns.Add("Items name");
                progressBar1.Increment(1);
                for (int i = 0; i < bills.Rows.Count; i++)
                {
                    bool isexist = false;
                    bool iscolexist = false;
                    for (int j = 0; j < items.Rows.Count; j++)
                    {
                        string qty = conn.ExecuteScalar("select isnull(sum(qty),0) as qty from SaleOrderProductMaster where isactive=1 and Billtype='SO' and billno='" + bills.Rows[i]["billno"].ToString() + "' and productname='" + items.Rows[j]["ItemName"].ToString() + "'");

                        if (Convert.ToDouble(qty) > 0)
                        {
                            isexist = true;
                            if (iscolexist == false)
                            {
                                maindt.Columns.Add(bills.Rows[i]["billno"].ToString() + Environment.NewLine + Convert.ToDateTime(bills.Rows[i]["Bill_Date"].ToString()).ToString("dd-MMM-yyyy"));
                                iscolexist = true;
                            }
                        }
                        if (isexist == true)
                        {
                            break;
                        }

                    }
                }
                progressBar1.Increment(1);
                maindt.Columns.Add("Total");
                DataRow dr;

                for (int i = 0; i < items.Rows.Count; i++)
                {
                    bool isexist = false;
                    dr = maindt.NewRow();
                    dr["Items name"] = items.Rows[i]["ItemName"].ToString();
                    double rowstot = 0;
                    for (int j = 0; j < bills.Rows.Count; j++)
                    {
                        string qty = conn.ExecuteScalar("select isnull(sum(qty),0) as qty from SaleOrderProductMaster where isactive=1 and Billtype='SO' and billno='" + bills.Rows[j]["billno"].ToString() + "' and productname='" + items.Rows[i]["ItemName"].ToString() + "'");
                        dr[bills.Rows[j]["billno"].ToString() + Environment.NewLine + Convert.ToDateTime(bills.Rows[j]["Bill_Date"].ToString()).ToString("dd-MMM-yyyy")] = qty;
                        rowstot += Convert.ToDouble(qty);
                    }
                    dr["Total"] = rowstot;
                    maindt.Rows.Add(dr);
                }
                dr = maindt.NewRow();
                dr["items Name"] = "Total";
                for (int i = 1; i < maindt.Columns.Count; i++)
                {
                    double colstot = 0;
                    for (int j = 0; j < maindt.Rows.Count; j++)
                    {
                        colstot += Convert.ToDouble(maindt.Rows[j][i].ToString());
                        dr[maindt.Columns[i].ColumnName] = colstot;
                    }
                }
                maindt.Rows.Add(dr);

                grdview.DataSource = maindt;
                progressBar1.Increment(1);
            }
            catch
            {
            }
        }

    }
}
