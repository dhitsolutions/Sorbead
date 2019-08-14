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
using ClosedXML.Excel;
using System.IO;

namespace RamdevSales
{
    public partial class quickreceptregister : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        DataTable main = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public quickreceptregister()
        {
            InitializeComponent();
        }

        public quickreceptregister(Master master, TabControl tabControl)
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

        private void quickreceptregister_Load(object sender, EventArgs e)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[2]["p"].ToString() == "False")
                    {
                        btnprint.Enabled = false;
                        btnCalculator.Enabled = false;
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
            }
            catch
            {
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

        private void btnprint_Enter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_Leave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_MouseEnter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_MouseLeave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = Color.White;
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
                BtnViewReport.Focus();
            }
        }
        public void binddata()
        {
            try
            {
                progressBar1.Maximum = 4;
                filelength = 4;

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                progressBar1.Increment(1);

                main = new DataTable();
                main.Columns.Add("Date", typeof(string));
                main.Columns.Add("Receipt No", typeof(string));
                main.Columns.Add("Payment Mode", typeof(string));
                main.Columns.Add("Account Name", typeof(string));
                main.Columns.Add("Remarks", typeof(string));
                main.Columns.Add("Total Amount", typeof(string));


                DataTable dt1 = conn.getdataset("select * from Ledger where isactive=1 and TranType='Rect' and Date1>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Date1<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                if (dt1.Rows.Count > 0)
                {
                    progressBar1.Increment(1);

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DataRow dr = main.NewRow();
                        string d = Convert.ToDateTime(dt1.Rows[i]["Date1"]).ToString(Master.dateformate);
                        dr["Date"] = d;
                        dr["Receipt No"] = dt1.Rows[i]["VoucherID"].ToString();
                        dr["Payment Mode"] = dt1.Rows[i]["OT1"].ToString();
                        dr["Account Name"] = dt1.Rows[i]["AccountName"].ToString();
                        dr["Remarks"] = dt1.Rows[i]["ShortNarration"].ToString();
                        dr["Total Amount"] = dt1.Rows[i]["Amount"].ToString();

                        main.Rows.Add(dr);
                    }
                    Double[] tot = new Double[main.Columns.Count];
                    for (int i = 0; i < main.Rows.Count; i++)
                    {
                        for (int j = 5; j < main.Columns.Count; j++)
                        {
                            if (main.Rows[i][j].ToString() == "")
                            {
                                tot[j] += Convert.ToDouble("0");
                            }
                            else
                            {
                                tot[j] += Convert.ToDouble(main.Rows[i][j].ToString());
                            }
                        }
                    }
                    DataRow lastdr = main.NewRow();
                    lastdr[0] = "";
                    lastdr[1] = "";
                    lastdr[2] = "";
                    lastdr[3] = "";
                    lastdr[4] = "";
                    for (int i = 5; i < main.Columns.Count; i++)
                    {
                        if (i == 5)
                        {
                            lastdr[i] = tot[i].ToString("N2");
                        }
                        else
                        {
                            lastdr[i] = tot[i].ToString("N2");
                        }
                    }
                    main.Rows.Add();
                    main.Rows.Add(lastdr);
                    lvqp.Columns.Clear();
                    lvqp.Items.Clear();
                    int ColCount = main.Columns.Count;

                    progressBar1.Increment(1);

                    //Add columns
                    for (int k = 0; k < ColCount; k++)
                    {
                        lvqp.Columns.Add(main.Columns[k].ColumnName, 120);
                    }
                    // Display items in the ListView control
                    for (int i = 0; i < main.Rows.Count; i++)
                    {
                        DataRow drow = main.Rows[i];

                        // Only row that have not been deleted
                        if (drow.RowState != DataRowState.Deleted)
                        {
                            // Define the list items
                            ListViewItem lvi = new ListViewItem(drow[0].ToString());
                            for (int j = 1; j < ColCount; j++)
                            {
                                lvi.SubItems.Add(drow[j].ToString());
                            }
                            // Add the list items to the ListView
                            lvqp.Items.Add(lvi);
                        }
                    }
                }
                progressBar1.Increment(1);
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
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
            //binddata();
        }

        public void open()
        {
            String str = lvqp.Items[lvqp.FocusedItem.Index].SubItems[2].Text;

            QReceipt bd = new QReceipt(master, tabControl);
            bd.updatemode(str, lvqp.Items[lvqp.FocusedItem.Index].SubItems[1].Text, 1, lvqp.Items[lvqp.FocusedItem.Index].SubItems[0].Text);
            master.AddNewTab(bd);
            //String str = lvqp.Items[lvqp.FocusedItem.Index].SubItems[2].Text;

            //QPayment bd = new QPayment(master, tabControl);
            //bd.updatemode(str, lvqp.Items[lvqp.FocusedItem.Index].SubItems[1].Text, 1, lvqp.Items[lvqp.FocusedItem.Index].SubItems[0].Text);
            //master.AddNewTab(bd);
        }
        private void lvqp_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[2]["u"].ToString() == "True")
                {
                    open();
                }
                else
                {
                    MessageBox.Show("You don't have Permission To View");
                    return;
                }
            }

        }
        DataTable userrights = new DataTable();
        private void lvqp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[2]["u"].ToString() == "True")
                    {
                        open();
                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To View");
                        return;
                    }
                }
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
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
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(main, "Quick Receipt Register");
                            // wb.Worksheets.Add(dt1, "ItemPrice");
                            wb.SaveAs(folderPath + "Quick Receipt Register(BillWise)" + DateTimeName + ".xlsx");
                        }
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "Quick Receipt Register(BillWise)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "Quick Receipt Register(BillWise)" + DateTimeName + ".xlsx");
                            String pathToExecutable = "AcroRd32.exe";
                        }
                    }
                }

                // MessageBox.Show("Export Data Sucessfully");
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            try
            {
                Printing prndata = new Printing();
                if (lvqp.Items.Count > 0)
                {
                    prndata.execute("delete from printing");
                    DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1");
                    int j = 1;
                    for (int i = 0; i < lvqp.Items.Count; i++)
                    {

                        string date = "", Reciptno = "", PaymentMode = "", AccountName = "", Remarks = "", TotalAmount = "", FromDateToDate = "";
                        date = lvqp.Items[i].SubItems[0].Text;
                        Reciptno = lvqp.Items[i].SubItems[1].Text;
                        PaymentMode = lvqp.Items[i].SubItems[2].Text;
                        AccountName = lvqp.Items[i].SubItems[3].Text;
                        Remarks = lvqp.Items[i].SubItems[4].Text;
                        TotalAmount = lvqp.Items[i].SubItems[5].Text;
                        // batch = lvqp.Items[i].SubItems[6].Text;
                        //  Qty = lvqp.Items[i].SubItems[7].Text;
                        //  AltQty = lvqp.Items[i].SubItems[8].Text;

                        FromDateToDate = "QUICK RECEIPT REGISTER FROM " + DTPFrom.Text + " TO " + DTPTo.Text;
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18)VALUES";
                        qry += "('" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["Website"].ToString() + "','" + FromDateToDate + "','" + dt1.Rows[0]["CSTNO"].ToString() + "','" + date + "','" + Reciptno + "','" + PaymentMode + "','" + AccountName + "','" + Remarks + "','" + TotalAmount + "')";
                        prndata.execute(qry);
                    }
                    Print popup = new Print("QuickReceiptRegister");
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

        static bool flag = false;
        int filelength = 1;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i == 0)
            {
                binddata();
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
    }
}
