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
using System.IO;
using ClosedXML.Excel;

namespace RamdevSales
{
    public partial class CustomerAndSupplierList : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        DataTable main = new DataTable();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public CustomerAndSupplierList()
        {
            InitializeComponent();
        }

        public CustomerAndSupplierList(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
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
        DataTable userrights = new DataTable();
        private void CustomerAndSupplierList_Load(object sender, EventArgs e)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[11]["p"].ToString() == "False")
                    {
                        btnprint.Enabled = false;
                        btnCalculator.Enabled = false;
                    }
                }
                LVcs.Columns.Add("Customer/Supplier Name", 300, HorizontalAlignment.Left);
                LVcs.Columns.Add("OP Balance", 100, HorizontalAlignment.Center);
                LVcs.Columns.Add("Address", 150, HorizontalAlignment.Center);
                LVcs.Columns.Add("City", 150, HorizontalAlignment.Center);
                LVcs.Columns.Add("State", 150, HorizontalAlignment.Center);
                LVcs.Columns.Add("State Code", 100, HorizontalAlignment.Center);
                LVcs.Columns.Add("Phone", 150, HorizontalAlignment.Center);
                LVcs.Columns.Add("Mobile", 150, HorizontalAlignment.Center);
                LVcs.Columns.Add("Email", 150, HorizontalAlignment.Center);
                LVcs.Columns.Add("GST No", 150, HorizontalAlignment.Center);
                this.ActiveControl = cmbcors;
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
        public void binddata()
        {
            try
            {
                progressBar1.Maximum = 4;
                filelength = 4;
                LVcs.Items.Clear();
                DataTable dt = new DataTable();
                progressBar1.Increment(1);
                dt = conn.getdataset("select * from ClientMaster where isactive=1 and GroupName='" + cmbcors.Text + "' order by AccountName asc");
                if (dt.Rows.Count > 0)
                {
                    progressBar1.Increment(1);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListViewItem li;
                        li = LVcs.Items.Add(dt.Rows[i]["AccountName"].ToString());
                        li.SubItems.Add(dt.Rows[i]["opbal"].ToString());
                        li.SubItems.Add(dt.Rows[i]["Address"].ToString());
                        li.SubItems.Add(dt.Rows[i]["City"].ToString());
                        li.SubItems.Add(dt.Rows[i]["State"].ToString());
                        li.SubItems.Add(dt.Rows[i]["statecode"].ToString());
                        li.SubItems.Add(dt.Rows[i]["Phone"].ToString());
                        li.SubItems.Add(dt.Rows[i]["Mobile"].ToString());
                        li.SubItems.Add(dt.Rows[i]["Email"].ToString());
                        li.SubItems.Add(dt.Rows[i]["GstNo"].ToString());
                    }
                    progressBar1.Increment(1);
                }
                progressBar1.Increment(1);
            }
            catch
            {
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

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1 = conn.getdataset("select AccountName,opbal,Address,City,State,Statecode,Phone,Mobile,Email,GstNo from ClientMaster where isactive=1 and GroupName='" + cmbcors.Text + "' order by AccountName asc");
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
                            wb.Worksheets.Add(dt1, "Customer Or Supplier");
                            // wb.Worksheets.Add(dt1, "ItemPrice");
                            wb.SaveAs(folderPath + "Customer Or Supplier" + DateTimeName + ".xlsx");
                        }
                        MessageBox.Show("Export Data Sucessfully");
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "Customer Or Supplier", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "Customer Or Supplier" + DateTimeName + ".xlsx");
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

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            try
            {
                Printing prndata = new Printing();
                if (LVcs.Items.Count > 0)
                {
                    prndata.execute("delete from printing");
                    DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1");
                    int j = 1;
                    for (int i = 0; i < LVcs.Items.Count; i++)
                    {

                        string Customername = "", opstock = "", Address = "", City = "", State = "", StateCode = "", PhNo = "", MobileNo = "", Email = "", Gstno = "", FromDateToDate = "";
                        Customername = LVcs.Items[i].SubItems[0].Text;
                        opstock = LVcs.Items[i].SubItems[1].Text;
                        Address = LVcs.Items[i].SubItems[2].Text;
                        City = LVcs.Items[i].SubItems[3].Text;
                        State = LVcs.Items[i].SubItems[4].Text;
                        StateCode = LVcs.Items[i].SubItems[5].Text;
                        PhNo = LVcs.Items[i].SubItems[6].Text;
                        MobileNo = LVcs.Items[i].SubItems[7].Text;
                        Email = LVcs.Items[i].SubItems[8].Text;
                        Gstno = LVcs.Items[i].SubItems[9].Text;

                        FromDateToDate = cmbcors.Text + " List ";
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23)VALUES";
                        qry += "('" + j++ + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["Website"].ToString() + "','" + FromDateToDate + "','" + dt1.Rows[0]["CSTNO"].ToString() + "','" + Customername + "','" + opstock + "','" + Address + "','" + City + "','" + State + "','" + StateCode + "','" + PhNo + "','" + MobileNo + "','" + Email + "','" + Gstno + "')";
                        prndata.execute(qry);
                    }
                    Print popup = new Print("CustomerSupplier");
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
