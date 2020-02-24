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
    public partial class SaleReturnRegisterdetailed : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        DataTable main = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public SaleReturnRegisterdetailed()
        {
            InitializeComponent();
        }

        public SaleReturnRegisterdetailed(Master master, TabControl tabControl)
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
        DataTable userrights = new DataTable();
        private void saleregisterdetailed_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[0]["p"].ToString() == "False")
                    {
                        btnprint.Enabled = false;
                        btnCalculator.Enabled = false;
                    }
                }

                //lvsalereg.Columns.Add("Date", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Bill No", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Party", 120, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Item", 120, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Company", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Group", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Salt", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Batch", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Qty", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Alt Qty", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Free", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Price", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Basic Amt", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Dis(%)", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Dis Amt", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Tax(%)", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Tax Amt", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Cess Amt", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Subtotal", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Other", 100, HorizontalAlignment.Left);
                //lvsalereg.Columns.Add("Net Amt", 100, HorizontalAlignment.Left);
                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;
                this.ActiveControl = DTPFrom;
                bindaccountdropdown();
                binditemdropdown();
            }
            catch
            {
            }
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
        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
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

        private void DTPFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DTPTo.Focus();
            }
        }
        string jsonstr = "";
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
                main.Columns.Add("billnocontinus", typeof(string));
                main.Columns.Add("Date", typeof(string));
                main.Columns.Add("Bill No", typeof(string));
                main.Columns.Add("Party", typeof(string));
                main.Columns.Add("Item", typeof(string));
                main.Columns.Add("Company", typeof(string));
                main.Columns.Add("Group", typeof(string));
                main.Columns.Add("Batch", typeof(string));
                main.Columns.Add("Qty", typeof(string));
                main.Columns.Add("Alt Qty", typeof(string));
                main.Columns.Add("Free", typeof(string));
                main.Columns.Add("Price", typeof(string));
                main.Columns.Add("Basic Amt", typeof(string));
                main.Columns.Add("Dis(%)", typeof(string));
                main.Columns.Add("Dis Amt", typeof(string));
                main.Columns.Add("Tax(%)", typeof(string));
                main.Columns.Add("Tax Amt", typeof(string));
                main.Columns.Add("Add Tax(%)", typeof(string));
                main.Columns.Add("Add Tax Amt", typeof(string));
                main.Columns.Add("Cess Amt", typeof(string));
                main.Columns.Add("Subtotal", typeof(string));
                main.Columns.Add("Other", typeof(string));
                main.Columns.Add("Round Off", typeof(string));
                main.Columns.Add("Net Amt", typeof(string));
                main.Columns.Add("Clientid", typeof(string));

                //main.Columns.Add("", typeof(string));
                //main.Columns.Add("", typeof(string));
                //main.Columns.Add("", typeof(string));
                //main.Columns.Add("", typeof(string));
                //main.Columns.Add("", typeof(string));
                //main.Columns.Add("", typeof(string));
                //main.Columns.Add("", typeof(string));
                //main.Columns.Add("", typeof(string));



              //  jsonstr = "{\"version\":\"1.0.0918\",\"billLists\":[";
                DataTable dtt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                
                //  DataTable dt1 = conn.getdataset("select b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.qty,bp.Aqty,bp.free,bp.Rate,bp.Total,bp.discountper,bp.discountamt,sum(bp.cgstper + bp.sgstper + bp.igstper) as taxper,bp.Tax,bp.addtaxper,bp.addtax,bp.Amount,sum(bc.amount) as others,b.totalnet  from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno inner join Billchargesmaster bc on bp.billno=bc.billno where b.isactive=1 and bp.isactive=1 and bc.isactive=1 group by b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.qty,bp.Aqty,bp.free,bp.Rate,bp.Total,bp.discountper,bp.discountamt,bp.Tax,bp.addtaxper,bp.addtax,bp.Amount,b.totalnet");
                DataTable dt1 = conn.getdataset("select * from BillMaster where isactive=1 and BillType='SR' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date asc");
                if (dt1.Rows.Count > 0)
                {
                    
                    progressBar1.Increment(1);

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        //if (i == 0)
                        //{
                        //    jsonstr += "{\"userGstin\":\"" + dtt1.Rows[0]["CSTNo"].ToString() + "\",";
                        //}
                        //else
                        //{
                        //    jsonstr += ",{\"userGstin\":\"" + dtt1.Rows[0]["CSTNo"].ToString() + "\",";
                        //}
                        //string subsupplytype = conn.getsinglevalue("select subtypeid from subtypemaster where id='" + dt1.Rows[i]["ewaysubtypeid"].ToString() + "'");
                        //if (subsupplytype == "")
                        //{
                        //    subsupplytype = "1";
                        //}
                        //string doctype = conn.getsinglevalue("select docid from documenttypemaster where id='" + dt1.Rows[i]["ewaydoctypeid"].ToString() + "'");
                        //jsonstr += "\"supplyType\":\"O\",\"subSupplyType\":" + subsupplytype + ",\"docType\":\"" + doctype + "\",\"docNo\":\"" + dt1.Rows[0]["billno"].ToString() + "\",\"docDate\":\"" + Convert.ToDateTime(dt1.Rows[i]["Bill_Date"].ToString()).ToString("dd/MM/yyyy") + "\",\"fromGstin\":\"" + dtt1.Rows[0]["CSTNo"].ToString() + "\",\"fromTrdName\":\"" + dtt1.Rows[0]["CompanyName"].ToString() + "\",\"fromAddr1\":\"" + dtt1.Rows[0]["Address"].ToString() + "\",\"fromAddr2\":\"" + dtt1.Rows[0]["Address2"].ToString() + "\",\"fromPlace\":\"" + dtt1.Rows[0]["City"].ToString() + "\",\"fromPincode\":" + dtt1.Rows[0]["pincode"].ToString() + ",\"fromStateCode\":" + dtt1.Rows[0]["Statecode"].ToString() + ",\"actualFromStateCode\":" + dtt1.Rows[0]["Statecode"].ToString() + ",";
                        ////  DataTable dt = conn.getdataset("select b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.qty,bp.Aqty,bp.free,bp.Rate,bp.Total,bp.discountper,bp.discountamt,sum(bp.cgstper + bp.sgstper + bp.igstper) as taxper,bp.Tax,bp.addtaxper,bp.addtax,bp.cess,bp.Amount,b.totalnet,bp.Productid,b.roudoff  from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno where b.isactive=1 and bp.isactive=1  and b.BillType='S' and bp.BillType='S' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.billno='" + dt1.Rows[i]["billno"].ToString() + "' group by b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.qty,bp.Aqty,bp.free,bp.Rate,bp.Total,bp.discountper,bp.discountamt,bp.Tax,bp.addtaxper,bp.addtax,bp.cess,bp.Amount,b.totalnet,bp.Productid,b.roudoff");
                        //DataTable client = conn.getdataset("select * from clientmaster where isactive=1 and clientID='" + dt1.Rows[i]["ClientID"].ToString() + "'");
                        //jsonstr += "\"toGstin\":\"" + client.Rows[0]["GstNo"].ToString() + "\",\"toTrdName\":\"" + client.Rows[0]["PrintName"].ToString() + "\",\"toAddr1\":\"" + client.Rows[0]["Address"].ToString() + "\",\"toPlace\":\"" + client.Rows[0]["City"].ToString() + "\",\"toPincode\":\"" + client.Rows[0]["pincode"].ToString() + "\",\"toStateCode\":" + client.Rows[0]["Statecode"].ToString() + ",\"actualToStateCode\":" + client.Rows[0]["Statecode"].ToString() + ",";
                        //jsonstr += "\"totalValue\":" + (Convert.ToDouble(dt1.Rows[i]["totalbasic"].ToString()) - Convert.ToDouble(dt1.Rows[i]["totaldiscount"].ToString()) - Convert.ToDouble(dt1.Rows[i]["totaladddiscount"].ToString())) + ",\"cgstvalue\":" + Convert.ToDouble(dt1.Rows[i]["cgatamt"].ToString()) + ",\"sgstvalue\":" + Convert.ToDouble(dt1.Rows[i]["sgstamt"].ToString()) + ",\"igstvalue\":" + Convert.ToDouble(dt1.Rows[i]["igstamt"].ToString()) + ",\"cessvalue\":" + Convert.ToDouble(dt1.Rows[i]["totalcess"].ToString()) + ",\"OthValue\":" + Convert.ToDouble(dt1.Rows[i]["totalcharges"].ToString()) + ",\"totInvValue\":" + Convert.ToDouble(dt1.Rows[i]["totalnet"].ToString()) + ",";
                        //string mot = conn.getsinglevalue("Select transportid from modeoftransport where id='" + dt1.Rows[i]["ewaytransportid"].ToString() + "'");


                        //jsonstr += "\"transMode\":\"" + mot + "\",\"transDistance\":\"" + dt1.Rows[i]["ewaydistance"].ToString() + "\",\"transporterName\":\"" + dt1.Rows[i]["ewaytransportername"].ToString() + "\",\"transporterId\":\"" + dt1.Rows[i]["ewaytransportlicid"].ToString() + "\",\"transDocNo\":\"" + dt1.Rows[i]["ewaydocno"].ToString() + "\",";
                        //if(dt1.Rows[i]["ewaydocdate"].ToString()!="")
                        //jsonstr += "\"transDocDate\":\"" + Convert.ToDateTime(dt1.Rows[i]["ewaydocdate"].ToString()).ToString("dd/MM/yyyy") + "\",\"vehicleNo\":\"" + dt1.Rows[i]["ewayvehicleno"].ToString() + "\",\"vehicleType\":\"R\",";
                        //else
                        //    jsonstr += "\"vehicleNo\":\"" + dt1.Rows[i]["ewayvehicleno"].ToString() + "\",\"vehicleType\":\"R\",";
                        //jsonstr += "\"itemList\":[";
                        DataTable dt = conn.getdataset("select b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,sum(bp.qty) as qty,sum(cast (bp.Aqty as float)) as aqty,sum(bp.free) as free,bp.Rate,sum(bp.Total) as total,bp.discountper,sum(bp.discountamt) as discountamt,(bp.cgstper + bp.sgstper + bp.igstper) as taxper,sum(bp.Tax) as tax,bp.addtaxper,sum(bp.addtax) as addtax,sum(bp.cess) as cess,sum(bp.Amount) as Amount,b.totalnet,bp.Productid,b.roudoff,bp.Per,bp.cgstamt,bp.sgstamt,bp.igdtamt from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno where b.isactive=1 and bp.isactive=1  and b.BillType='SR' and bp.BillType='SR' and b.ClientID='" + dt1.Rows[i]["ClientID"].ToString() + "' and bp.ClientID='" + dt1.Rows[i]["ClientID"].ToString() + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.billno='" + dt1.Rows[i]["billno"].ToString() + "' group by b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.Rate,bp.discountper,bp.addtaxper,bp.Productid,b.roudoff,b.totalnet,bp.cgstper,bp.sgstper,bp.igstper,bp.Per,bp.cgstamt,bp.sgstamt,bp.igdtamt");
                          for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            
                            DataTable productid = conn.getdataset("select CompanyID,GroupName from ProductMaster where isactive=1 and ProductID='" + dt.Rows[j]["Productid"].ToString() + "'");
                            string companyname = conn.ExecuteScalar("select companyname from CompanyMaster where CompanyID='" + productid.Rows[0]["CompanyID"].ToString() + "'");
                            string party = conn.ExecuteScalar("select AccountName from ClientMaster where isactive=1 and ClientID='" + dt.Rows[j]["ClientID"].ToString() + "'");
                            string other = conn.ExecuteScalar("select sum(amount) as others from Billchargesmaster where isactive=1 and BillType='SR' and billno='" + dt1.Rows[i]["billno"].ToString() + "'");
                            if (string.IsNullOrEmpty(other))
                            {
                                other = "0";
                            }
                            DataTable hsn = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + dt.Rows[j]["Productid"].ToString() + "'");
                            //if (j == 0)
                            //    jsonstr += "{\"itemNo\":" + (j + 1) + ",\"productName\":\"" + dt.Rows[j]["Productname"].ToString() + "\",\"productDesc\":\"" + dt.Rows[j]["Productname"].ToString() + "\",\"hsnCode\":" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + ",\"quantity\":" + dt.Rows[j]["qty"].ToString() + ",\"qtyUnits\":\"" + dt.Rows[j]["Per"].ToString() + "\",\"taxableAmount\":" + (Convert.ToDouble(dt.Rows[j]["Total"].ToString()) - Convert.ToDouble(dt.Rows[j]["discountamt"].ToString())) + ",\"sgstRate\":" + dt.Rows[j]["sgstamt"].ToString() + ",\"cgstRate\":" + dt.Rows[j]["cgstamt"].ToString() + ",\"igstRate\":" + dt.Rows[j]["igdtamt"].ToString() + ",\"cessRate\":" + dt.Rows[j]["cess"].ToString() + ",\"cessNonAdvol\":" + dt.Rows[j]["addtax"].ToString() + "}";
                            //else
                            //    jsonstr += ",{\"itemNo\":" + (j + 1) + ",\"productName\":\"" + dt.Rows[j]["Productname"].ToString() + "\",\"productDesc\":\"" + dt.Rows[j]["Productname"].ToString() + "\",\"hsnCode\":" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + ",\"quantity\":" + dt.Rows[j]["qty"].ToString() + ",\"qtyUnits\":\"" + dt.Rows[j]["Per"].ToString() + "\",\"taxableAmount\":" + (Convert.ToDouble(dt.Rows[j]["Total"].ToString()) - Convert.ToDouble(dt.Rows[j]["discountamt"].ToString())) + ",\"sgstRate\":" + dt.Rows[j]["sgstamt"].ToString() + ",\"cgstRate\":" + dt.Rows[j]["cgstamt"].ToString() + ",\"igstRate\":" + dt.Rows[j]["igdtamt"].ToString() + ",\"cessRate\":" + dt.Rows[j]["cess"].ToString() + ",\"cessNonAdvol\":" + dt.Rows[j]["addtax"].ToString() + "}";
		
                            if (j == 0)
                            {

                                DataRow dr = main.NewRow();
                                string d = Convert.ToDateTime(dt.Rows[j]["Bill_Date"]).ToString(Master.dateformate);
                                dr["billnocontinus"] = dt.Rows[j]["billno"].ToString();
                                dr["Date"] = d;
                                dr["Bill No"] = dt.Rows[j]["billno"].ToString();
                                dr["Party"] = party;
                                dr["Item"] = dt.Rows[j]["Productname"].ToString();
                                dr["Company"] = companyname;
                                dr["Group"] = productid.Rows[0]["GroupName"].ToString();
                                dr["Batch"] = dt.Rows[j]["batch"].ToString();
                                dr["Qty"] = dt.Rows[j]["qty"].ToString();
                                dr["Alt Qty"] = dt.Rows[j]["aqty"].ToString();
                                dr["Free"] = dt.Rows[j]["free"].ToString();
                                dr["Price"] = dt.Rows[j]["rate"].ToString();
                                dr["Basic Amt"] = dt.Rows[j]["Total"].ToString();
                                dr["Dis(%)"] = dt.Rows[j]["discountper"].ToString();
                                dr["Dis Amt"] = dt.Rows[j]["discountamt"].ToString();
                                dr["Tax(%)"] = dt.Rows[j]["taxper"].ToString();
                                dr["Tax Amt"] = dt.Rows[j]["Tax"].ToString();
                                dr["Add Tax(%)"] = dt.Rows[j]["addtaxper"].ToString();
                                dr["Add Tax Amt"] = dt.Rows[j]["addtax"].ToString();
                                dr["Cess Amt"] = dt.Rows[j]["cess"].ToString();
                                dr["Subtotal"] = dt.Rows[j]["Amount"].ToString();
                                dr["Other"] = other;
                                dr["Round Off"] = dt.Rows[j]["roudoff"].ToString();
                                // Double roundoff = 0;
                                // Double nettotal = 0;
                                // if (Convert.ToDouble(dt.Rows[j]["roudoff"].ToString()) < 0.00)
                                // {
                                //     roundoff = Convert.ToDouble(dt.Rows[j]["roudoff"].ToString()) * -1;
                                //   //  nettotal = Convert.ToDouble(dt.Rows[j]["totalnet"].ToString()) - roundoff;
                                // }
                                // else
                                // {
                                //     roundoff = Convert.ToDouble(dt.Rows[j]["roudoff"].ToString());
                                //   //  nettotal = Convert.ToDouble(dt.Rows[j]["totalnet"].ToString()) + roundoff;
                                // }
                                //// Double nettotal = Convert.ToDouble(dt.Rows[j]["totalnet"].ToString()) + roundoff;
                                dr["Net Amt"] = dt.Rows[j]["totalnet"].ToString();
                                dr["Clientid"] = dt.Rows[j]["ClientID"].ToString();
                                main.Rows.Add(dr);
                            }
                            else
                            {
                                DataRow dr = main.NewRow();
                                dr["billnocontinus"] = dt.Rows[j]["billno"].ToString();
                                dr["Date"] = "";
                                dr["Bill No"] = "";
                                dr["Party"] = party;
                                dr["Item"] = dt.Rows[j]["Productname"].ToString();
                                dr["Company"] = companyname;
                                dr["Group"] = productid.Rows[0]["GroupName"].ToString();
                                dr["Batch"] = dt.Rows[j]["batch"].ToString();
                                dr["Qty"] = dt.Rows[j]["qty"].ToString();
                                dr["Alt Qty"] = dt.Rows[j]["aqty"].ToString();
                                dr["Free"] = dt.Rows[j]["free"].ToString();
                                dr["Price"] = dt.Rows[j]["rate"].ToString();
                                dr["Basic Amt"] = dt.Rows[j]["Total"].ToString();
                                dr["Dis(%)"] = dt.Rows[j]["discountper"].ToString();
                                dr["Dis Amt"] = dt.Rows[j]["discountamt"].ToString();
                                dr["Tax(%)"] = dt.Rows[j]["taxper"].ToString();
                                dr["Tax Amt"] = dt.Rows[j]["Tax"].ToString();
                                dr["Add Tax(%)"] = dt.Rows[j]["addtaxper"].ToString();
                                dr["Add Tax Amt"] = dt.Rows[j]["addtax"].ToString();
                                dr["Cess Amt"] = dt.Rows[j]["cess"].ToString();
                                dr["Subtotal"] = dt.Rows[j]["Amount"].ToString();
                                dr["Other"] = "";
                                dr["Round Off"] = "";
                                dr["Net Amt"] = "";
                                dr["Clientid"] = dt.Rows[j]["ClientID"].ToString();

                                main.Rows.Add(dr);
                            }

                            
                        }
                       //   jsonstr += "]}";	
                        progressBar1.Increment(1);
                    }
                 //   jsonstr += "]}";
                    Double[] tot = new Double[main.Columns.Count];
                    for (int i = 0; i < main.Rows.Count; i++)
                    {
                        for (int j = 8; j < main.Columns.Count; j++)
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
                    lastdr[5] = "";
                    lastdr[6] = "";
                    lastdr[7] = "";
                    for (int i = 8; i < main.Columns.Count; i++)
                    {
                        if (i == 8)
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
                    lvsalereg.Columns.Clear();
                    lvsalereg.Items.Clear();
                    int ColCount = main.Columns.Count;
                    //Add columns
                    for (int k = 0; k < ColCount; k++)
                    {
                        if (k == 0)
                        {
                            lvsalereg.Columns.Add(main.Columns[k].ColumnName, 0);
                        }
                        else
                        {
                            if (k == 24)
                            {
                                lvsalereg.Columns.Add(main.Columns[k].ColumnName, 0);
                            }
                            else
                            {
                                lvsalereg.Columns.Add(main.Columns[k].ColumnName, 120);
                            }
                            //lvsalereg.Columns.Add(main.Columns[k].ColumnName, 120);
                        }
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
                            lvsalereg.Items.Add(lvi);
                        }
                    }
                    progressBar1.Increment(1);
                }
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

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvsalereg.Items.Count > 0)
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
                                wb.Worksheets.Add(main, "SALE Register");
                                // wb.Worksheets.Add(dt1, "ItemPrice");
                                wb.SaveAs(folderPath + "SALE Register(BillWise)" + DateTimeName + ".xlsx");
                            }
                            DialogResult dr = MessageBox.Show("Do you want to Open Document?", "SALE Register(BillWise)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start(folderPath + "SALE Register(BillWise)" + DateTimeName + ".xlsx");
                                String pathToExecutable = "AcroRd32.exe";
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Records for Download Excel..");
                }

                // MessageBox.Show("Export Data Sucessfully");
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void open()
        {
            try
            {
                string[] strfinalarray = new string[5] { "SR", "C", "SaleReturn", "SR", "" };
                this.Enabled = false;
                String str = lvsalereg.Items[lvsalereg.FocusedItem.Index].SubItems[0].Text;
                int clientid = Convert.ToInt32(lvsalereg.Items[lvsalereg.FocusedItem.Index].SubItems[24].Text);
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                //  Sale p = new Sale(this, master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    bd.updatemode(str, lvsalereg.Items[lvsalereg.FocusedItem.Index].SubItems[0].Text, clientid, strfinalarray);
                    master.AddNewTab(bd);
                }
                //else if (dt1.Rows[0]["formname"].ToString() == p.Text)
                //{
                //    p.updatemode(str, LVDayBook.Items[LVDayBook.FocusedItem.Index].SubItems[0].Text, 1);
                //    master.AddNewTab(p);
                //}
            }
            catch
            {
            }
            finally
            {
                this.Enabled = true;
            }
        }
        private void lvsalereg_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[0]["u"].ToString() == "True")
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

        private void lvsalereg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[0]["u"].ToString() == "True")
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                bindsearch();
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
        }

        private void bindsearch()
        {
            // if (txtaccount.Text != "" || txtitems.Text != "")
            //  {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            main = new DataTable();
            main.Columns.Add("billnocontinus", typeof(string));
            main.Columns.Add("Date", typeof(string));
            main.Columns.Add("Bill No", typeof(string));
            main.Columns.Add("Party", typeof(string));
            main.Columns.Add("Item", typeof(string));
            main.Columns.Add("Company", typeof(string));
            main.Columns.Add("Group", typeof(string));
            main.Columns.Add("Batch", typeof(string));
            main.Columns.Add("Qty", typeof(string));
            main.Columns.Add("Alt Qty", typeof(string));
            main.Columns.Add("Free", typeof(string));
            main.Columns.Add("Price", typeof(string));
            main.Columns.Add("Basic Amt", typeof(string));
            main.Columns.Add("Dis(%)", typeof(string));
            main.Columns.Add("Dis Amt", typeof(string));
            main.Columns.Add("Tax(%)", typeof(string));
            main.Columns.Add("Tax Amt", typeof(string));
            main.Columns.Add("Add Tax(%)", typeof(string));
            main.Columns.Add("Add Tax Amt", typeof(string));
            main.Columns.Add("Cess Amt", typeof(string));
            main.Columns.Add("Subtotal", typeof(string));
            main.Columns.Add("Other", typeof(string));
            main.Columns.Add("Round Off", typeof(string));
            main.Columns.Add("Net Amt", typeof(string));
            DataTable dt1 = new DataTable();
            if (drpaccount.Text == "AccountName" || drpaccount.Text == "PrintName" || drpaccount.Text == "GroupName" || drpaccount.Text == "Address" || drpaccount.Text == "City" || drpaccount.Text == "State" || drpaccount.Text == "statecode")
            {
                //clientid = conn.getsinglevalue("select clientid from clientmaster where isactive=1 and (AccountName like '%" + txtaccount.Text + "%' and PrintName like '%" + txtaccount.Text + "%' and GroupName like '%" + txtaccount.Text + "%' and Address like '%" + txtaccount.Text + "%' and City like '%" + txtaccount.Text + "%'and State like '%" + txtaccount.Text + "%' and statecode like '%" + txtaccount.Text + "%')");
                dt1 = conn.getdataset("select * from BillMaster where clientid in (select clientid from clientmaster where isactive=1 and (" + drpaccount.Text + " like '%" + txtaccount.Text + "%')) and isactive=1 and BillType='SR' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Bill_Date");
            }
            else
            {
                dt1 = conn.getdataset("select * from BillMaster where clientid like '%" + txtaccount.Text + "%' and isactive=1 and BillType='SR' and Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
            }
            //  DataTable dt1 = conn.getdataset("select b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.qty,bp.Aqty,bp.free,bp.Rate,bp.Total,bp.discountper,bp.discountamt,sum(bp.cgstper + bp.sgstper + bp.igstper) as taxper,bp.Tax,bp.addtaxper,bp.addtax,bp.Amount,sum(bc.amount) as others,b.totalnet  from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno inner join Billchargesmaster bc on bp.billno=bc.billno where b.isactive=1 and bp.isactive=1 and bc.isactive=1 group by b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.qty,bp.Aqty,bp.free,bp.Rate,bp.Total,bp.discountper,bp.discountamt,bp.Tax,bp.addtaxper,bp.addtax,bp.Amount,b.totalnet");
            // lvsalereg.Columns.Clear();
            lvsalereg.Items.Clear();
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    DataTable dt = new DataTable();
                    if (drpitems.Text == "GroupName" || drpitems.Text == "Packing" || drpitems.Text == "Hsn_Sac_Code" || drpitems.Text == "itemnumber" || drpitems.Text == "ProductID")
                    {
                        //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' and Packing like '%" + txtitems.Text + "%' and Hsn_Sac_Code like '%" + txtitems.Text + "%' and itemnumber like '%" + txtitems.Text + "%')");
                        //items = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' or Packing like '%" + txtitems.Text + "%' or Hsn_Sac_Code like '%" + txtitems.Text + "%' or ProductID like '%" + txtitems.Text + "%' or itemnumber like '%" + txtitems.Text + "%')) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                        dt = conn.getdataset("select b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,sum(bp.qty) as qty,sum(cast (bp.Aqty as float)) as aqty,sum(bp.free) as free,bp.Rate,sum(bp.Total) as total,bp.discountper,sum(bp.discountamt) as discountamt,sum(bp.cgstper + bp.sgstper + bp.igstper) as taxper,sum(bp.Tax) as tax,bp.addtaxper,sum(bp.addtax) as addtax,sum(bp.cess) as cess,sum(bp.Amount) as Amount,b.totalnet,bp.Productid,b.roudoff  from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno where b.isactive=1 and bp.isactive=1 and bp.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and b.BillType='SR' and bp.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.billno='" + dt1.Rows[i]["billno"].ToString() + "' group by b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.Rate,bp.discountper,bp.addtaxper,bp.Productid,b.roudoff,b.totalnet");
                    }
                    else if (drpitems.Text == "companyname")
                    {
                        //itemname = conn.getsinglevalue("select product_name from productmaster where isactive=1 and companyid =(select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))");
                        //items = conn.getdataset("select distinct Productname as ItemName from SaleOrderProductMaster where productname in (select product_name from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and isactive=1 and Billtype='SO' and Bill_Run_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by Productname");
                        dt = conn.getdataset("select b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,sum(bp.qty) as qty,sum(cast (bp.Aqty as float)) as aqty,sum(bp.free) as free,bp.Rate,sum(bp.Total) as total,bp.discountper,sum(bp.discountamt) as discountamt,sum(bp.cgstper + bp.sgstper + bp.igstper) as taxper,sum(bp.Tax) as tax,bp.addtaxper,sum(bp.addtax) as addtax,sum(bp.cess) as cess,sum(bp.Amount) as Amount,b.totalnet,bp.Productid,b.roudoff  from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno where b.isactive=1 and bp.isactive=1 and bp.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%')))  and b.BillType='SR' and bp.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.billno='" + dt1.Rows[i]["billno"].ToString() + "' group by b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.Rate,bp.discountper,bp.addtaxper,bp.Productid,b.roudoff,b.totalnet");
                    }
                    else
                    {
                        dt = conn.getdataset("select b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,sum(bp.qty) as qty,sum(cast (bp.Aqty as float)) as aqty,sum(bp.free) as free,bp.Rate,sum(bp.Total) as total,bp.discountper,sum(bp.discountamt) as discountamt,sum(bp.cgstper + bp.sgstper + bp.igstper) as taxper,sum(bp.Tax) as tax,bp.addtaxper,sum(bp.addtax) as addtax,sum(bp.cess) as cess,sum(bp.Amount) as Amount,b.totalnet,bp.Productid,b.roudoff  from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno where b.isactive=1 and bp.isactive=1  and b.BillType='SR' and bp.BillType='SR' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.billno='" + dt1.Rows[i]["billno"].ToString() + "' group by b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.Rate,bp.discountper,bp.addtaxper,bp.Productid,b.roudoff,b.totalnet");
                    }

                    //  DataTable dt = conn.getdataset("select b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.qty,bp.Aqty,bp.free,bp.Rate,bp.Total,bp.discountper,bp.discountamt,sum(bp.cgstper + bp.sgstper + bp.igstper) as taxper,bp.Tax,bp.addtaxper,bp.addtax,bp.cess,bp.Amount,b.totalnet,bp.Productid  from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno where b.isactive=1 and bp.isactive=1  and b.BillType='S' and bp.BillType='S' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and b.billno='" + dt1.Rows[i]["billno"].ToString() + "' group by b.Bill_Date,b.billno,b.ClientID,bp.Productname,bp.batch,bp.qty,bp.Aqty,bp.free,bp.Rate,bp.Total,bp.discountper,bp.discountamt,bp.Tax,bp.addtaxper,bp.addtax,bp.cess,bp.Amount,b.totalnet,bp.Productid");
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        DataTable productid = conn.getdataset("select CompanyID,GroupName from ProductMaster where isactive=1 and ProductID='" + dt.Rows[j]["Productid"].ToString() + "'");
                        string companyname = conn.ExecuteScalar("select companyname from CompanyMaster where CompanyID='" + productid.Rows[0]["CompanyID"].ToString() + "'");
                        string party = conn.ExecuteScalar("select AccountName from ClientMaster where isactive=1 and ClientID='" + dt.Rows[j]["ClientID"].ToString() + "'");
                        string other = conn.ExecuteScalar("select sum(amount) as others from Billchargesmaster where isactive=1 and BillType='SR' and billno='" + dt1.Rows[i]["billno"].ToString() + "'");
                        if (string.IsNullOrEmpty(other))
                        {
                            other = "0";
                        }
                        if (j == 0)
                        {
                            DataRow dr = main.NewRow();
                            string d = Convert.ToDateTime(dt.Rows[j]["Bill_Date"]).ToString(Master.dateformate);
                            dr["billnocontinus"] = dt.Rows[j]["billno"].ToString();
                            dr["Date"] = d;
                            dr["Bill No"] = dt.Rows[j]["billno"].ToString();
                            dr["Party"] = party;
                            dr["Item"] = dt.Rows[j]["Productname"].ToString();
                            dr["Company"] = companyname;
                            dr["Group"] = productid.Rows[0]["GroupName"].ToString();
                            dr["Batch"] = dt.Rows[j]["batch"].ToString();
                            dr["Qty"] = dt.Rows[j]["qty"].ToString();
                            dr["Alt Qty"] = dt.Rows[j]["aqty"].ToString();
                            dr["Free"] = dt.Rows[j]["free"].ToString();
                            dr["Price"] = dt.Rows[j]["rate"].ToString();
                            dr["Basic Amt"] = dt.Rows[j]["Total"].ToString();
                            dr["Dis(%)"] = dt.Rows[j]["discountper"].ToString();
                            dr["Dis Amt"] = dt.Rows[j]["discountamt"].ToString();
                            dr["Tax(%)"] = dt.Rows[j]["taxper"].ToString();
                            dr["Tax Amt"] = dt.Rows[j]["Tax"].ToString();
                            dr["Add Tax(%)"] = dt.Rows[j]["addtaxper"].ToString();
                            dr["Add Tax Amt"] = dt.Rows[j]["addtax"].ToString();
                            dr["Cess Amt"] = dt.Rows[j]["cess"].ToString();
                            dr["Subtotal"] = dt.Rows[j]["Amount"].ToString();
                            dr["Other"] = other;
                            dr["Round Off"] = dt.Rows[j]["roudoff"].ToString();
                            dr["Net Amt"] = dt.Rows[j]["totalnet"].ToString();

                            main.Rows.Add(dr);
                        }
                        else
                        {
                            DataRow dr = main.NewRow();
                            dr["billnocontinus"] = dt.Rows[j]["billno"].ToString();
                            dr["Date"] = "";
                            dr["Bill No"] = "";
                            dr["Party"] = party;
                            dr["Item"] = dt.Rows[j]["Productname"].ToString();
                            dr["Company"] = companyname;
                            dr["Group"] = productid.Rows[0]["GroupName"].ToString();
                            dr["Batch"] = dt.Rows[j]["batch"].ToString();
                            dr["Qty"] = dt.Rows[j]["qty"].ToString();
                            dr["Alt Qty"] = dt.Rows[j]["aqty"].ToString();
                            dr["Free"] = dt.Rows[j]["free"].ToString();
                            dr["Price"] = dt.Rows[j]["rate"].ToString();
                            dr["Basic Amt"] = dt.Rows[j]["Total"].ToString();
                            dr["Dis(%)"] = dt.Rows[j]["discountper"].ToString();
                            dr["Dis Amt"] = dt.Rows[j]["discountamt"].ToString();
                            dr["Tax(%)"] = dt.Rows[j]["taxper"].ToString();
                            dr["Tax Amt"] = dt.Rows[j]["Tax"].ToString();
                            dr["Add Tax(%)"] = dt.Rows[j]["addtaxper"].ToString();
                            dr["Add Tax Amt"] = dt.Rows[j]["addtax"].ToString();
                            dr["Cess Amt"] = dt.Rows[j]["cess"].ToString();
                            dr["Subtotal"] = dt.Rows[j]["Amount"].ToString();
                            dr["Other"] = "0";
                            dr["Round Off"] = "";
                            dr["Net Amt"] = "";

                            main.Rows.Add(dr);
                        }
                    }

                }
                Double[] tot = new Double[main.Columns.Count];
                for (int i = 0; i < main.Rows.Count; i++)
                {
                    for (int j = 8; j < main.Columns.Count; j++)
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
                lastdr[5] = "";
                lastdr[6] = "";
                lastdr[7] = "";
                for (int i = 8; i < main.Columns.Count; i++)
                {
                    if (i == 8)
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
                lvsalereg.Columns.Clear();
                lvsalereg.Items.Clear();
                int ColCount = main.Columns.Count;
                //Add columns
                for (int k = 0; k < ColCount; k++)
                {
                    if (k == 0)
                    {
                        lvsalereg.Columns.Add(main.Columns[k].ColumnName, 0);
                    }
                    else
                    {
                        lvsalereg.Columns.Add(main.Columns[k].ColumnName, 120);
                    }
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
                        lvsalereg.Items.Add(lvi);
                        //  }
                    }
                }
            }
        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            try
            {
                Printing prndata = new Printing();
                if (lvsalereg.Items.Count > 0)
                {
                    prndata.execute("delete from printing");
                    DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1");
                    int j = 1;
                    for (int i = 0; i < lvsalereg.Items.Count; i++)
                    {

                        string date = "", Billno = "", PartyName = "", Itemname = "", Company = "", Group = "", batch = "", Qty = "", AltQty = "", Free = "", Price = "", BasicAmount = "", DisPer = "", DisAmount = "", TaxPer = "", TaxAmount = "", AdditionalTaxPer = "", AddTaxAmount = "", CessAmount = "", SubTotal = "", Other = "", NetAmount = "", FromDateToDate = "";
                        date = lvsalereg.Items[i].SubItems[1].Text;
                        Billno = lvsalereg.Items[i].SubItems[2].Text;
                        PartyName = lvsalereg.Items[i].SubItems[3].Text;
                        Itemname = lvsalereg.Items[i].SubItems[4].Text;
                        Company = lvsalereg.Items[i].SubItems[5].Text;
                        Group = lvsalereg.Items[i].SubItems[6].Text;
                        batch = lvsalereg.Items[i].SubItems[7].Text;
                        Qty = lvsalereg.Items[i].SubItems[8].Text;
                        AltQty = lvsalereg.Items[i].SubItems[9].Text;
                        Free = lvsalereg.Items[i].SubItems[10].Text;
                        Price = lvsalereg.Items[i].SubItems[11].Text;
                        BasicAmount = lvsalereg.Items[i].SubItems[12].Text;
                        DisPer = lvsalereg.Items[i].SubItems[13].Text;
                        DisAmount = lvsalereg.Items[i].SubItems[14].Text;
                        TaxPer = lvsalereg.Items[i].SubItems[15].Text;
                        TaxAmount = lvsalereg.Items[i].SubItems[16].Text;
                        AdditionalTaxPer = lvsalereg.Items[i].SubItems[17].Text;
                        AddTaxAmount = lvsalereg.Items[i].SubItems[18].Text;
                        CessAmount = lvsalereg.Items[i].SubItems[19].Text;
                        SubTotal = lvsalereg.Items[i].SubItems[20].Text;
                        Other = lvsalereg.Items[i].SubItems[21].Text;
                        NetAmount = lvsalereg.Items[i].SubItems[22].Text;
                        FromDateToDate = "SALE REGISTER FROM " + DTPFrom.Text + " TO " + DTPTo.Text;
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34)VALUES";
                        qry += "('" + date + "','" + Billno + "','" + PartyName + "','" + Itemname + "','" + Company + "','" + Group + "','" + batch + "','" + Qty + "','" + AltQty + "','" + Free + "','" + Price + "','" + BasicAmount + "','" + DisPer + "','" + DisAmount + "','" + TaxPer + "','" + TaxAmount + "','" + AdditionalTaxPer + "','" + AddTaxAmount + "','" + CessAmount + "','" + SubTotal + "','" + Other + "','" + NetAmount + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["Website"].ToString() + "','" + FromDateToDate + "','" + dt1.Rows[0]["CSTNO"].ToString() + "')";
                        prndata.execute(qry);
                    }
                    Print popup = new Print("SaleRegister");
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
        DataTable path = new DataTable();
        private void lnkewayjson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataSet ds2 = ods.getdata("select * from Path");
            path = ds2.Tables[0];

            string appPath = path.Rows[0]["DefaultPath"].ToString();
            string path1 = appPath + @"\JSON\EWBBulk_" + DTPFrom.Text + "_" + DTPTo.Text + "_" + DateTime.Now + ".json";
            string filename = "EWBBulk_" + DTPFrom.Text + "_" + DTPTo.Text + "_" + DateTime.Now.ToString("ddMMyyyyHHMMSS") + ".json";
            var dir = appPath + @"\JSON\"; // folder location

            if (!Directory.Exists(dir))  // if it doesn't exist, create
                Directory.CreateDirectory(dir);

            // use Path.Combine to combine 2 strings to a path
            File.WriteAllText(Path.Combine(dir, filename), jsonstr);
            MessageBox.Show("Json File has been created on following path " + path1);
        }
    }
}
