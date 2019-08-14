using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using ClosedXML.Excel;

namespace RamdevSales
{
    public partial class SalePurchaseDetailsReport : Form
    {
       // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection cs = new Connection();
        private Master master;
        private TabControl tabControl;
        public SalePurchaseDetailsReport()
        {
            InitializeComponent();
        }

        public SalePurchaseDetailsReport(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            this.master = master;
            this.tabControl = tabControl;
            InitializeComponent();
            DTPFrom.CustomFormat = Master.dateformate;
            DTPTo.CustomFormat = Master.dateformate;
            DataSet dtrange = cs.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
           // DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            cmbFilter1.SelectedIndex = 0;
            cmbFilter2.SelectedIndex = 0;
            cmbFilter3.SelectedIndex = 0;
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                if (cmbFilter1.SelectedIndex == 0 && cmbFilter2.SelectedIndex == 0 && cmbFilter3.SelectedIndex == 0)
                {
                    dt = cs.getdataset("select b.Bill_Date,b.billno,b.ClientID,cm.AccountName,bp.Productname,bp.batch,bp.Rate,bp.Bags,bp.Aqty,bp.Pqty,b.totalqty,b.totalnet,bp.Amount,bp.productid from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno inner join ClientMaster cm on cm.ClientID=b.ClientID where b.isactive=1 and bp.isactive=1 and ((b.BillType='S' and bp.Billtype='S') or (b.BillType='P' and bp.Billtype='P'))  and Bill_Date>='" + DTPFrom.Text + "' and Bill_Date<='" + DTPTo.Text + "' order by Bill_Date");
                    if (dt.Rows.Count > 0)
                    {
                        grdData.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No Records Found For this Criteria..");
                        grdData.DataSource = null;
                    }

                    // grdData.Columns[1].Visible = false;
                }
                else
                {
                    if (cmbFilter1.Text == "Customer" || cmbFilter2.Text == "Customer" || cmbFilter3.Text == "Customer")
                    {
                        string qry = "";
                        if (cmbFilter1.SelectedIndex != 0)
                        {
                            if (cmbFilter1.Text == "Customer")
                            {
                                qry += "and AccountName like '%" + txtFilter1.Text + "%'";
                            }
                            else
                            {
                                qry += "and " + cmbFilter1.Text + " like '%" + txtFilter1.Text + "%'";
                            }
                        }
                        if (cmbFilter2.SelectedIndex != 0)
                        {
                            if (cmbFilter2.Text == "Customer")
                            {
                                qry += "and AccountName like '%" + txtFilter2.Text + "%'";
                            }
                            else
                            {
                                qry += "and " + cmbFilter2.Text + " like '%" + txtFilter2.Text + "%'";
                            }
                        }
                        if (cmbFilter3.SelectedIndex != 0)
                        {
                            if (cmbFilter3.Text == "Customer")
                            {
                                qry += "and AccountName like '%" + txtFilter3.Text + "%'";
                            }
                            else
                            {
                                qry += "and " + cmbFilter3.Text + " like '%" + txtFilter3.Text + "%'";
                            }
                        }

                        dt = cs.getdataset("select b.Bill_Date,b.billno,b.ClientID,cm.AccountName,bp.Productname,bp.batch,bp.Rate,bp.Bags,bp.Aqty,bp.Pqty,b.totalqty,b.totalnet,bp.Amount,bp.productid from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno inner join ClientMaster cm on cm.ClientID=b.ClientID where b.isactive=1 and bp.isactive=1 and b.BillType='S' and bp.Billtype='S' and Bill_Date>='" + DTPFrom.Text + "' and Bill_Date<='" + DTPTo.Text + "'" + qry + "");
                        if (dt.Rows.Count > 0)
                        {
                            grdData.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("No Records Found For this Criteria..");
                            grdData.DataSource = null;
                        }
                        // grdData.Columns[1].Visible = false;
                    }
                    else if (cmbFilter1.Text == "Supplier" || cmbFilter2.Text == "Supplier" || cmbFilter3.Text == "Supplier")
                    {
                        string qry = "";
                        if (cmbFilter1.SelectedIndex != 0)
                        {
                            if (cmbFilter1.Text == "Supplier")
                            {
                                qry += "and AccountName like '%" + txtFilter1.Text + "%'";
                            }
                            else
                            {
                                if (cmbFilter1.Text == "billno")
                                {
                                    qry += "and b." + cmbFilter1.Text + " like '%" + txtFilter1.Text + "%'";
                                }
                                else
                                {
                                    qry += "and " + cmbFilter1.Text + " like '%" + txtFilter1.Text + "%'";
                                }
                            }
                        }
                        if (cmbFilter2.SelectedIndex != 0)
                        {
                            if (cmbFilter2.Text == "Supplier")
                            {
                                qry += "and AccountName like '%" + txtFilter2.Text + "%'";
                            }
                            else
                            {
                                if (cmbFilter2.Text == "billno")
                                {
                                    qry += "and b." + cmbFilter2.Text + " like '%" + txtFilter2.Text + "%'";
                                }
                                else
                                {
                                    qry += "and " + cmbFilter2.Text + " like '%" + txtFilter2.Text + "%'";
                                }
                            }
                        }
                        if (cmbFilter3.SelectedIndex != 0)
                        {
                            if (cmbFilter3.Text == "Supplier")
                            {
                                qry += "and AccountName like '%" + txtFilter3.Text + "%'";
                            }
                            else
                            {
                                if (cmbFilter3.Text == "billno")
                                {
                                    qry += "and b." + cmbFilter3.Text + " like '%" + txtFilter3.Text + "%'";
                                }
                                else
                                {
                                    qry += "and " + cmbFilter3.Text + " like '%" + txtFilter3.Text + "%'";
                                }
                            }
                        }
                        dt = cs.getdataset("select b.Bill_Date,b.billno,b.ClientID,cm.AccountName,bp.Productname,bp.batch,bp.Rate,bp.Bags,bp.Aqty,bp.Pqty,b.totalqty,b.totalnet,bp.Amount,bp.productid from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno inner join ClientMaster cm on cm.ClientID=b.ClientID where b.isactive=1 and bp.isactive=1 and cm.isactive=1 and b.BillType='P' and bp.Billtype='P' and Bill_Date>='" + DTPFrom.Text + "' and Bill_Date<='" + DTPTo.Text + "'" + qry + "");
                        if (dt.Rows.Count > 0)
                        {
                            grdData.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("No Records Found For this Criteria..");
                            grdData.DataSource = null;
                        }
                        // grdData.Columns[1].Visible = false;
                    }
                    else
                    {
                        string qry = "";
                        if (cmbFilter1.SelectedIndex != 0)
                        {
                            if (cmbFilter1.Text == "billno")
                            {
                                qry += "and b." + cmbFilter1.Text + " like '%" + txtFilter1.Text + "%'";
                            }
                            else
                            {
                                qry += "and " + cmbFilter1.Text + " like '%" + txtFilter1.Text + "%'";
                            }
                        }
                        if (cmbFilter2.SelectedIndex != 0)
                        {
                            if (cmbFilter2.Text == "billno")
                            {
                                qry += "and b." + cmbFilter2.Text + " like '%" + txtFilter2.Text + "%'";
                            }
                            else
                            {
                                qry += "and " + cmbFilter2.Text + " like '%" + txtFilter2.Text + "%'";
                            }
                        }
                        if (cmbFilter3.SelectedIndex != 0)
                        {
                            if (cmbFilter3.Text == "billno")
                            {
                                qry += "and b." + cmbFilter3.Text + " like '%" + txtFilter3.Text + "%'";
                            }
                            else
                            {
                                qry += "and " + cmbFilter3.Text + " like '%" + txtFilter3.Text + "%'";
                            }
                        }
                        dt = cs.getdataset("select b.Bill_Date,b.billno,b.ClientID,cm.AccountName,bp.Productname,bp.batch,bp.Rate,bp.Bags,bp.Aqty,bp.Pqty,b.totalqty,b.totalnet,bp.Amount,bp.productid from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno inner join ClientMaster cm on cm.ClientID=b.ClientID where b.isactive=1 and bp.isactive=1 and ((b.BillType='S' and bp.Billtype='S') or (b.BillType='P' and bp.Billtype='P'))  and Bill_Date>='" + DTPFrom.Text + "' and Bill_Date<='" + DTPTo.Text + "'"+ qry +" order by Bill_Date");
                        if (dt.Rows.Count > 0)
                        {
                            grdData.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("No Records Found For this Criteria..");
                            grdData.DataSource = null;
                        }
                    }

 
                }
                if (dt.Rows.Count > 0)
                {
                   // int rate = 0;
                    double rate = 0.0, qty = 0.0,amount = 0.0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rate += double.Parse(dt.Rows[i]["Rate"].ToString());
                        qty += double.Parse(dt.Rows[i]["Pqty"].ToString());
                        amount += double.Parse(dt.Rows[i]["Amount"].ToString());

                    }
                    txtTotalRate.Text = rate.ToString();
                    txtTotalQty.Text = qty.ToString();
                    txtTotalAmount.Text = amount.ToString();
                }
            }
            catch (Exception Excp)
            {

            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                // tabControl.TabPages.Remove(tabControl.SelectedTab);
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    master.RemoveCurrentTab();
                }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
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

        private void txtFilter2_TextChanged(object sender, EventArgs e)
        {

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
                cmbFilter1.Focus();
            }
        }

        private void cmbFilter1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFilter1.Focus();
            }
        }

        private void txtFilter1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbFilter2.Focus();
            }
        }

        private void cmbFilter2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFilter2.Focus();
            }
        }

        private void txtFilter2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbFilter3.Focus();
            }
        }

        private void cmbFilter3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFilter3.Focus();
            }
        }

        private void txtFilter3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnViewReport.Focus();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)grdData.DataSource;
                if (dt.Rows.Count > 0)
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
                                wb.Worksheets.Add(dt, "SalePurchaseReport");
                                wb.SaveAs(folderPath + "Report" + DateTimeName + ".xlsx");
                            }
                            MessageBox.Show("Export Data Sucessfully");
                            DialogResult dr = MessageBox.Show("Do you want to Open Document?", "Report", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start(folderPath + "Report" + DateTimeName + ".xlsx");
                                String pathToExecutable = "AcroRd32.exe";
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Records Found To Export Data..");
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)grdData.DataSource;
            try
            {
                Printing prndata = new Printing();
                if (dt.Rows.Count > 0)
                {
                    prndata.execute("delete from printing");
                    DataTable dt1 = cs.getdataset("select * from company WHERE isactive=1");
                    int j = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Billdate = "", billno = "", AccountName = "", ProductName = "", Batch = "", Rate = "";
                        Billdate = dt.Rows[i]["Bill_Date"].ToString();
                        billno = dt.Rows[i]["billno"].ToString();
                        AccountName = dt.Rows[i]["AccountName"].ToString();
                        ProductName = dt.Rows[i]["Productname"].ToString();
                        Batch = dt.Rows[i]["batch"].ToString();
                        Rate = dt.Rows[i]["Rate"].ToString();
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24)VALUES";
                        qry += "('" + j++ + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + Billdate + "','" + billno + "','" + AccountName + "','" + ProductName + "','" + Batch + "','" + Rate + "','"+txtTotalRate.Text+"','"+txtTotalQty.Text+"','"+txtTotalAmount.Text+"')";
                        prndata.execute(qry);
                    }
                    Print popup = new Print("SalePurchaseDetailsReport");
                    popup.ShowDialog();
                    popup.Dispose();
                }
                else
                {
                    MessageBox.Show("No Records For Printing..");
                }
            }
            catch (Exception excp)
            {

            }

        }

    }
}
