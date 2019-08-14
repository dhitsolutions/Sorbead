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
    public partial class GSTR1Summery : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        Printing prn = new Printing();
        DataTable userrights = new DataTable();

        public GSTR1Summery()
        {
            InitializeComponent();
        }

        public GSTR1Summery(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        private void GSTR1Summery_Load(object sender, EventArgs e)
        {
            userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");

            if (userrights.Rows.Count > 0)
            {
                DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                con.Open();
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(0, 0);

                LVledger.Columns.Add("Perticulars", 160, HorizontalAlignment.Left);
                LVledger.Columns.Add("Count", 70, HorizontalAlignment.Center);
                LVledger.Columns.Add("Taxable Value", 120, HorizontalAlignment.Right);
                LVledger.Columns.Add("IGST Amt", 120, HorizontalAlignment.Right);
                LVledger.Columns.Add("CGST Amt", 120, HorizontalAlignment.Right);
                LVledger.Columns.Add("SGST Amt", 120, HorizontalAlignment.Right);
                LVledger.Columns.Add("Cess Amt", 120, HorizontalAlignment.Right);
                LVledger.Columns.Add("Tax Amt", 120, HorizontalAlignment.Right);
                LVledger.Columns.Add("Invoice Amt", 130, HorizontalAlignment.Right);

                if (userrights.Rows[41]["p"].ToString() == "False")
                {
                    btngenrpt.Enabled = false;
                }
            }
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }


    }
}
