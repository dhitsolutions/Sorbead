﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;
using LoggingFramework;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;

namespace RamdevSales
{
    public partial class DefaultSale : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        Printing prn = new Printing();
        OleDbSettings ods = new OleDbSettings();
        // private DateWisePurchaseReport dateWisePurchaseReport;
        public static string productid = "";
        public static string saletype = "";
        public static string taxvalue = "";
        public static string taxforprice = "";
        public static string ataxforprice = "";
        public static string itemname = "";
        public static string activecontroal;
        public static string[] updateitem;
        DataTable options, dt, dt1, dt2, dt3, dt5, dt6, dt7 = new DataTable();
        DataTable dt8 = new DataTable();
        DataTable updateitem1 = new DataTable();
        public static DataTable temptable = new DataTable();
        static string Dprice, taxper, additaxper;
        public static string refnoupdate;
        public DefaultSale()
        {
            InitializeComponent();
            options = conn.getdataset("select * from options");
            loadcurrency();
        }
        private void loadcurrency()
        {
            try
            {
                lblrate.Text = "Rate" + Master.currency;
                lblamount.Text = "Amount" + Master.currency;
            }
            catch
            {
            }
        }
        //public DefaultSale(DateWisePurchaseReport dateWisePurchaseReport)
        //{

        //    InitializeComponent();
        //    this.dateWisePurchaseReport = dateWisePurchaseReport;
        //    options = conn.getdataset("select * from options");
        //    loadcurrency();
        //}

        public DefaultSale(Ledger ledger)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.ledger = ledger;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSale(DateWiseReport dateWiseReport)
        {
            InitializeComponent();
            this.dateWiseReport = dateWiseReport;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSale(CashBook cashBook)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.cashBook = cashBook;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSale(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            options = conn.getdataset("select * from options");
            loadcurrency();
            this.master = master;
            this.tabControl = tabControl;
        }

        public DefaultSale(DateWiseReport dateWiseReport, Master master, TabControl tabControl)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.dateWiseReport = dateWiseReport;
            this.master = master;
            this.tabControl = tabControl;
            options = conn.getdataset("select * from options");
            loadcurrency();
            
        }
      
        public DefaultSale(Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSale(DateWiseReport dateWiseReport, Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.dateWiseReport = dateWiseReport;
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSale(Ledger ledger, Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.ledger = ledger;
            this.master = master;
            this.tabControl = tabControl;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSale(Ledger ledger, Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.ledger = ledger;
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSale(ItemWiseStock itemWiseStock, Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.itemWiseStock = itemWiseStock;
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSale(SaleOrder saleOrder, Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.saleOrder = saleOrder;
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSale(SaleOrder saleOrder, Master master, string p_2)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.saleOrder = saleOrder;
            this.master = master;
            //this.tabControl = tabControl;
            this.p_2 = p_2;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSale(serialnotracking serialnotracking, Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.serialnotracking = serialnotracking;
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }
        public DefaultSale(SaleReturnRegisterdetailed saleReturnRegisterdetailed, Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.SaleReturnRegisterdetailed = saleReturnRegisterdetailed;
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }
        public DefaultSale(saleregisterdetailed saleregisterdetailed, Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.saleregisterdetailed = saleregisterdetailed;
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }

        public DefaultSale(purchaseregisterdetailed purchaseregisterdetailed, Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.purchaseregisterdetailed = purchaseregisterdetailed;
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
            options = conn.getdataset("select * from options");
            loadcurrency();
        }
        DataTable userrights = new DataTable();
        private void DefaultSale_Load(object sender, EventArgs e)
        {
            try
            {
                if (cnt == 0)
                {
                    userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");

                    if (strfinalarray[0] == "S")
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[0]["a"].ToString() == "False")
                            {
                                BtnPayment.Enabled = false;
                            }
                            if (userrights.Rows[0]["d"].ToString() == "False")
                            {
                                btndelete.Enabled = false;
                            }
                            if (userrights.Rows[0]["p"].ToString() == "False")
                            {
                                btnCalculator.Enabled = false;
                            }
                        }
                        txtheader.Text = "OUT WARD";
                        txttype.Text = "OUT WARD Type:";
                        this.Text = "OUT WARD";
                    }
                    else if (strfinalarray[0] == "SR")
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[13]["a"].ToString() == "False")
                            {
                                BtnPayment.Enabled = false;
                            }
                            if (userrights.Rows[13]["d"].ToString() == "False")
                            {
                                btndelete.Enabled = false;
                            }
                            if (userrights.Rows[13]["p"].ToString() == "False")
                            {
                                btnCalculator.Enabled = false;
                            }
                        }
                        txtheader.Text = "SALE RETURN";
                        txttype.Text = "Sale Type:";
                        this.Text = "Sale Return";
                    }
                    else if (strfinalarray[0] == "P")
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[3]["a"].ToString() == "False")
                            {
                                BtnPayment.Enabled = false;
                            }
                            if (userrights.Rows[3]["d"].ToString() == "False")
                            {
                                btndelete.Enabled = false;
                            }
                            if (userrights.Rows[3]["p"].ToString() == "False")
                            {
                                btnCalculator.Enabled = false;
                            }
                        }
                        txtheader.Text = "IN WARD";
                        txttype.Text = "IN WARD Type:";
                        this.Text = "IN WARD";
                    }

                    else if (strfinalarray[0] == "PR")
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[16]["a"].ToString() == "False")
                            {
                                BtnPayment.Enabled = false;
                            }
                            if (userrights.Rows[16]["d"].ToString() == "False")
                            {
                                btndelete.Enabled = false;
                            }
                            if (userrights.Rows[16]["p"].ToString() == "False")
                            {
                                btnCalculator.Enabled = false;
                            }
                        }
                        txtheader.Text = "PURCHASE RETURN";
                        txttype.Text = "Purchase Type:";
                        this.Text = "Purchase Return";
                    }
                    options = conn.getdataset("select * from options");
                    loadpage();
                    bindperticular();
                    TxtBillNo.ReadOnly = true;
                    bindallitem();
                    //  TxtRundate.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                }
                if (userrights.Rows.Count > 0)
                {
                    if (strfinalarray[0] == "S")
                    {
                        if (userrights.Rows[0]["p"].ToString() == "False")
                        {
                            btnCalculator.Enabled = false;
                        }
                    }
                    else if (strfinalarray[0] == "SR")
                    {
                        if (userrights.Rows[13]["p"].ToString() == "False")
                        {
                            btnCalculator.Enabled = false;
                        }
                    }
                    else if (strfinalarray[0] == "P")
                    {
                        if (userrights.Rows[3]["p"].ToString() == "False")
                        {
                            btnCalculator.Enabled = false;
                        }
                    }
                    else if (strfinalarray[0] == "PR")
                    {
                        if (userrights.Rows[16]["p"].ToString() == "False")
                        {
                            btnCalculator.Enabled = false;
                        }
                    }
                }

                TxtRundate.CustomFormat = Master.dateformate;
                txtduedate.CustomFormat = Master.dateformate;
                //set the interval  and start the timer
                //  timer1.Interval = 1000;
                //  timer1.Start();


            }
            catch
            {

            }
        }
        private void loadpage()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            charges = 1;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            //   TxtRundate.Text = DateTime.Now.ToString(Master.dateformate);
            this.ActiveControl = TxtRundate;
            LVFO.Columns.Add("Description of Goods", 196, HorizontalAlignment.Left);
            LVFO.Columns.Add("Packing", 69, HorizontalAlignment.Center);
            LVFO.Columns.Add("Batch", 51, HorizontalAlignment.Center);
            LVFO.Columns.Add("P.Qty", 53, HorizontalAlignment.Center);
            LVFO.Columns.Add("A.Qty", 47, HorizontalAlignment.Center);
            LVFO.Columns.Add("Total Qty", 70, HorizontalAlignment.Right);
            LVFO.Columns.Add("Free", 53, HorizontalAlignment.Center);
            LVFO.Columns.Add(lblrate.Text, 74, HorizontalAlignment.Right);
            LVFO.Columns.Add("Per", 42, HorizontalAlignment.Center);
            LVFO.Columns.Add(lblamount.Text, 95, HorizontalAlignment.Center);
            LVFO.Columns.Add("Dis(%)", 50, HorizontalAlignment.Center);
            LVFO.Columns.Add("Dis Per", 63, HorizontalAlignment.Center);
            LVFO.Columns.Add("TAX", 63, HorizontalAlignment.Center);
            LVFO.Columns.Add("Add Tax", 65, HorizontalAlignment.Center);
            LVFO.Columns.Add("Total", 118, HorizontalAlignment.Center);
            LVFO.Columns.Add("Sgstper", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("Sgstamt", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("cgstper", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("cgstamt", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("igstper", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("igstamt", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("addtaxper", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("serial", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("cess", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("productid", 0, HorizontalAlignment.Center);
            LVFO.Columns.Add("boxsrno", 0, HorizontalAlignment.Center);


            LVCHARGES.Columns.Add("Perticulars", 237, HorizontalAlignment.Left);
            LVCHARGES.Columns.Add("Remarks", 215, HorizontalAlignment.Left);
            LVCHARGES.Columns.Add("Value", 167, HorizontalAlignment.Left);
            LVCHARGES.Columns.Add("@", 122, HorizontalAlignment.Left);
            LVCHARGES.Columns.Add("+/-", 91, HorizontalAlignment.Left);
            LVCHARGES.Columns.Add("Amount", 117, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("valueofexp", 0, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("tax", 0, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("sgst", 0, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("cgst", 0, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("igst", 0, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("additax", 0, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("additaxamt", 0, HorizontalAlignment.Right);
            LVCHARGES.Columns.Add("chargeID", 0, HorizontalAlignment.Right);
            lvserial.Columns.Add("", 1000, HorizontalAlignment.Right);
            lvallitem.Columns.Add("Product Name", 400, HorizontalAlignment.Left);
            temptable = new DataTable();
            temptable.Columns.Add("Itemname", typeof(string));
            temptable.Columns.Add("SERIAL", typeof(string));
            bindsaletype();
            bindcustomer();
            DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            TxtRundate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            TxtRundate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());

            
            //  autoreaderbind();
            //DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
            //TxtRundate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            //TxtRundate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            //txtduedate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            //txtduedate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            //TxtRundate.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            //  DataSet dtrange = conn.getdata("SELECT * FROM Company where CompanyID='" + Master.companyId + "'");
            //  TxtRundate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            //  TxtRundate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            ////  txtduedate.MinDate = Convert.ToDateTime(TxtRundate.Value);
            //  txtduedate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            //  TxtRundate.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
            if (options.Rows[0]["salevoucherno"].ToString() == "Manual")
            {
                TxtBillNo.ReadOnly = false;
            }
            else if (options.Rows[0]["salervoucherno"].ToString() == "Manual")
            {
                TxtBillNo.ReadOnly = false;
            }
            else if (options.Rows[0]["purchasevoucherno"].ToString() == "Manual")
            {
                TxtBillNo.ReadOnly = false;
            }
            else if (options.Rows[0]["purchaservoucherno"].ToString() == "Manual")
            {
                TxtBillNo.ReadOnly = false;
            }
            //  grditem.Rows[0].Selected = false;
            con.Close();
        }
        public void bindbatch()
        {
            try
            {
                cmbbatch.Enabled = true;
                dt8 = new DataTable();
                SqlCommand cmd1 = new SqlCommand("select Productid,Batchno from ProductPriceMaster where Productid='" + dt1.Rows[0]["ProductID"].ToString() + "' and isactive='1'", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                sda1.Fill(dt8);
                cmbbatch.ValueMember = "Productid";
                cmbbatch.DisplayMember = "Batchno";
                cmbbatch.DataSource = dt8;
                cmbbatch.Focus();
            }
            catch
            {
            }


        }
        public void bindsaletype()
        {
            SqlCommand cmd = new SqlCommand("select Purchasetypeid,Purchasetypename from PurchasetypeMaster where FormType='" + strfinalarray[0] + "' and isactive='1'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            cmbsaletype.ValueMember = "Purchasetypeid";
            cmbsaletype.DisplayMember = "Purchasetypename";
            cmbsaletype.DataSource = dt;
            cmbsaletype.SelectedIndex = -1;

            //  autobind(dt, cmbsaletype);
        }
        public void bindallitem()
        {
            try
            {
                DataTable allitem = new DataTable();
                lvallitem.Items.Clear();
                allitem = conn.getdataset("Getitemname");
                grditem.DataSource = allitem;
                grditem.Rows[0].Selected = false;
                GridDesign();
                grditem.Columns[0].Width = 400;
                //for (int i = 0; i < allitem.Rows.Count; i++)
                //{
                //    ListViewItem li;
                //    li = lvallitem.Items.Add(allitem.Rows[i]["Product_Name"].ToString());
                //}
            }
            catch
            {
            }
        }
        public void bindcustomer()
        {
            string qry = "";
            if (Convert.ToBoolean(options.Rows[0]["showcustomersupplierseperate"].ToString()) == true)
            {
                if (strfinalarray[0] == "S")
                {
                    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
                    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or GroupID='" + groupid + "') order by AccountName";
                }
                else if (strfinalarray[0] == "SR")
                {
                    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
                    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or GroupID='" + groupid + "') order by AccountName";
                }
                else if (strfinalarray[0] == "P")
                {
                    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or GroupID='" + groupid + "') order by AccountName";
                    //   qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=100 order by AccountName";
                }
                else if (strfinalarray[0] == "PR")
                {
                    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or GroupID='" + groupid + "') order by AccountName";
                    //qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=100 order by AccountName";
                }
            }
            else
            {
                DataTable Accountgroup = conn.getdataset("select * from AccountGroup where (id='99' or id='100')");
                string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                string groupid1 = Accountgroup.Rows[1]["UnderGroupID"].ToString();
                qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or GroupID=100 or GroupID='" + groupid + "' or GroupID='" + groupid1 + "') order by AccountName";
                //  qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or groupid=99) order by AccountName";
            }


            SqlCommand cmd1 = new SqlCommand(qry, con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbcustname.ValueMember = "ClientID";
            cmbcustname.DisplayMember = "AccountName";
            cmbcustname.DataSource = dt1;
            cmbcustname.SelectedIndex = -1;


            //  autobind(dt1, cmbcustname);
        }
        public void binaagent()
        {
            string qry = "";
            if (strfinalarray[0] == "S" || strfinalarray[0] == "SR")
            {
                qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=50 order by AccountName";
            }
            SqlCommand cmd1 = new SqlCommand(qry, con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbagentname.ValueMember = "ClientID";
            cmbagentname.DisplayMember = "AccountName";
            cmbagentname.DataSource = dt1;
            cmbagentname.SelectedIndex = -1;
        }

        private void autobind(DataTable dt1, ComboBox cmbcustname)
        {
            string[] arr = new string[dt1.Rows.Count];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                arr[i] = dt1.Rows[i][1].ToString();
            }

            cmbcustname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbcustname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbcustname.AutoCompleteCustomSource.AddRange(arr);
        }

        public void btnCalculator_Click(object sender, EventArgs e)
        {
            databind();
            print();
        }
        string num = "";
        string inword = "";
        public void GenerateWordsinRs()
        {
            decimal numberrs = Convert.ToDecimal(num);
            CultureInfo ci = new CultureInfo("en-IN");
            string aaa = String.Format("{0:#,##0.##}", numberrs);
            aaa = aaa + " " + ci.NumberFormat.CurrencySymbol.ToString();
            inword = aaa;


            string input = num;
            string a = "";
            string b = "";

            // take decimal part of input. convert it to word. add it at the end of method.
            string decimals = "";

            if (input.Contains("."))
            {
                decimals = input.Substring(input.IndexOf(".") + 1);
                // remove decimal part from input
                input = input.Remove(input.IndexOf("."));

            }
            string strWords = NumbersToWords(Convert.ToInt32(input));

            if (!num.Contains("."))
            {
                a = "In words :" + strWords + " Rupees Only";
            }
            else
            {
                a = "In words :" + strWords + " Rupees";
            }

            if (decimals.Length > 0)
            {
                // if there is any decimal part convert it to words and add it to strWords.
                string strwords2 = NumbersToWords(Convert.ToInt32(decimals));
                if (strwords2 != "Zero")
                {
                    b = " and " + strwords2 + " Paisa Only ";
                }
            }

            inword = a + b;
        }
        public static string NumbersToWords(int inputNumber)
        {
            int inputNo = inputNumber;

            if (inputNo == 0)
                return "Zero";

            int[] numbers = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (inputNo < 0)
            {
                sb.Append("Minus ");
                inputNo = -inputNo;
            }

            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            numbers[0] = inputNo % 1000; // units
            numbers[1] = inputNo / 1000;
            numbers[2] = inputNo / 100000;
            numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
            numbers[3] = inputNo / 10000000; // crores
            numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

            for (int i = 3; i > 0; i--)
            {
                if (numbers[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (numbers[i] == 0) continue;
                u = numbers[i] % 10; // ones
                t = numbers[i] / 10;
                h = numbers[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
        Double scessamt = 0;
        string creditdays = "";
        private void print()
        {
            //if (strfinalarray[0] != "P")
            //{
                if (Rewritrbooksofaccount.rewritedata != "True")
                {
                    string save = conn.ExecuteScalar("select billno from billmaster where isactive=1 and billno='" + TxtBillNo.Text + "' and BillType='" + strfinalarray[0] + "'");
                    if (!string.IsNullOrEmpty(save))
                    {
                        if (txtheader.Text == "OUT WARD")
                        {
                            if (userrights.Rows.Count > 0)
                            {
                                if (userrights.Rows[0]["p"].ToString() == "False")
                                {
                                    // MessageBox.Show("You don't have Permission To Print");
                                    return;
                                }
                            }
                        }
                        else if (txtheader.Text == "SALE RETURN")
                        {
                            if (userrights.Rows.Count > 0)
                            {
                                if (userrights.Rows[13]["p"].ToString() == "False")
                                {
                                    // MessageBox.Show("You don't have Permission To Print");
                                    return;
                                }
                            }
                        }
                        else if (txtheader.Text == "IN WARD")
                        {
                            if (userrights.Rows.Count > 0)
                            {
                                if (userrights.Rows[3]["p"].ToString() == "False")
                                {
                                    //MessageBox.Show("You don't have Permission To Print");
                                    return;
                                }
                            }
                        }
                        else if (txtheader.Text == "PURCHASE RETURN")
                        {
                            if (userrights.Rows.Count > 0)
                            {
                                if (userrights.Rows[16]["p"].ToString() == "False")
                                {
                                    // MessageBox.Show("You don't have Permission To Print");
                                    return;
                                }
                            }
                        }
                        String s1 = Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00");
                        if (s1 == ".00")
                        {
                            s1 = "0";
                            num = s1;
                        }
                        else
                        {
                            num = s1;
                        }
                        GenerateWordsinRs();

                        //ChangeNumbersToWords sh = new ChangeNumbersToWords();
                        //String s1 = Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00");
                        //string[] words = s1.Split('.');


                        //string str = sh.changeToWords(words[0]);
                        //string str1 = sh.changeToWords(words[1]);
                        //if (str1 == " " || str1 == null || words[1] == "00")
                        //{
                        //    str1 = "Zero ";
                        //}
                        //String inword = "In words : Rupees " + str + "and " + str1 + "Paise Only";
                        if (DateWiseReport.autoprint == "True")
                        {
                            DataTable client = conn.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbcustname.SelectedValue + "'");
                            DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                            prn.execute("delete from printing");
                            int j = 1;
                            string perticulars = "", remarks = "", value = "", at = "", plusminus = "", amount = "", valuelvf = "", taxlvf = "", sgstlvf = "", cgstlvf = "", igstlvf = "", addtaxlvf = "";
                            for (int i = 0; i < LVCHARGES.Items.Count; i++)
                            {
                                perticulars += LVCHARGES.Items[i].SubItems[0].Text + Environment.NewLine;
                                remarks += LVCHARGES.Items[i].SubItems[1].Text + Environment.NewLine;
                                value += LVCHARGES.Items[i].SubItems[2].Text + Environment.NewLine;
                                at += LVCHARGES.Items[i].SubItems[3].Text + Environment.NewLine;
                                plusminus += LVCHARGES.Items[i].SubItems[4].Text + Environment.NewLine;
                                amount += LVCHARGES.Items[i].SubItems[5].Text + Environment.NewLine;
                                valuelvf += LVCHARGES.Items[i].SubItems[6].Text + Environment.NewLine;
                                taxlvf += LVCHARGES.Items[i].SubItems[7].Text + Environment.NewLine;
                                sgstlvf += LVCHARGES.Items[i].SubItems[8].Text + Environment.NewLine;
                                cgstlvf += LVCHARGES.Items[i].SubItems[9].Text + Environment.NewLine;
                                igstlvf += LVCHARGES.Items[i].SubItems[10].Text + Environment.NewLine;
                                addtaxlvf += LVCHARGES.Items[i].SubItems[11].Text + Environment.NewLine;

                            }
                            string printrefno;
                            if (BtnPayment.Text == "Update")
                            {
                                printrefno = refnoupdate;
                            }
                            else
                            {
                                printrefno = refno;
                            }
                            for (int i = 0; i < LVFO.Items.Count; i++)
                            {
                                try
                                {
                                    DataTable hsn = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + LVFO.Items[i].SubItems[24].Text + "'");
                                    
                                    string h = "select * from ProductMaster where isactive=1 and ProductID='" + LVFO.Items[i].SubItems[24].Text + "'";
                                    LogGenerator.Info(h);
                                    DataTable item = conn.getdataset("select * from ProductPriceMaster where isactive=1 and ProductID='" + hsn.Rows[0]["ProductID"].ToString() + "' and Batchno='" + LVFO.Items[i].SubItems[2].Text + "'");
                                    string u = "select * from ProductPriceMaster where isactive=1 and ProductID='" + hsn.Rows[0]["ProductID"].ToString() + "' and Batchno='" + LVFO.Items[i].SubItems[2].Text + "'";
                                    LogGenerator.Info(u);
                                   
                                    //DataTable item1 = conn.getdataset("select * from ItemTaxMaster where isactive=1 and ProductID='" + hsn.Rows[0]["ProductID"].ToString() + "' and saletypeid='"+cmbsaletype.Text+"'");
                                    //old query  DataTable taxgroup = conn.getdataset("select sum(amount)-sum(discountamt) as basicamount,sum(sgstamt) as sgst,sum(cgstamt) as cgst,sum(amount)+sum(sgstamt)+sum(cgstamt)-sum(discountamt) as total, igdtamt ,sum(addtax) as addtax,sum(addtaxper) as addtaxper from BillProductMaster WHERE isactive=1 and billno='" + TxtBillNo.Text + "' group by igdtamt");
                                    //new query created by sir ==select p.Hsn_Sac_Code,b.Productname,sum(b.Total),b.sgstper,sum( b.sgstamt),b.cgstper, sum(b.cgstamt),b.igstper,sum(b.igdtamt),b.addtaxper,sum(b.addtax),sum(b.Amount) from billproductmaster b inner join ProductMaster p on p.Product_Name=b.Productname  where  b.isactive=1 and p.isactive=1 group by p.Hsn_Sac_Code,b.Productname,b.sgstper,b.cgstper,b.igstper,b.addtaxper
                                    DataTable saletype = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbsaletype.Text + "'");
                                    string o = "select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbsaletype.Text + "'";
                                    LogGenerator.Info(o);
                                    DataTable taxgroup = conn.getdataset("select p.Hsn_Sac_Code,sum(b.Total)-sum(b.discountamt) as basicamount,b.sgstper as sgstper,sum( b.sgstamt) as sgst,b.cgstper as cgstper, sum(b.cgstamt) as cgst,sgstper + cgstper + igstper as igstper ,sum(b.igdtamt) as igdtamt,b.addtaxper,sum(b.addtax) as addtax,sum(b.Total)+sum(b.sgstamt)+sum(b.cgstamt)+(b.igdtamt)-sum(b.discountamt) as total from billproductmaster b inner join ProductMaster p on p.ProductID=b.ProductID where  b.billno='" + TxtBillNo.Text + "' and b.billtype='" + strfinalarray[0] + "' and b.isactive=1 and p.isactive=1 group by p.Hsn_Sac_Code,b.sgstper,b.cgstper,b.igstper,b.addtaxper,b.igdtamt");
                                    string z = "select p.Hsn_Sac_Code,sum(b.Total)-sum(b.discountamt) as basicamount,b.sgstper as sgstper,sum( b.sgstamt) as sgst,b.cgstper as cgstper, sum(b.cgstamt) as cgst,sum(b.sgstper + b.cgstper + b.igstper) as igstper ,sum(b.igdtamt) as igdtamt,b.addtaxper,sum(b.addtax) as addtax,sum(b.Total)+sum(b.sgstamt)+sum(b.cgstamt)+(b.igdtamt)-sum(b.discountamt) as total from billproductmaster b inner join ProductMaster p on p.ProductID=b.ProductID where  b.billno='" + TxtBillNo.Text + "' and b.isactive=1 and p.isactive=1 group by p.Hsn_Sac_Code,b.sgstper,b.cgstper,b.igstper,b.addtaxper,b.igdtamt";
                                    LogGenerator.Info(z);
                                    string HSNCODE = "HSN/SAC", taxable = "Taxable", cgstper = "CGST % ", cgstamt = "AMT.", sgstper = "SGST %", sgstamt = "AMT.", totalamt = "Total", addper = "AddTax%", addamt = "Amt.", igstper = "Igst %", igstamt = "Amt.";
                                    double cgst = 0, sgst = 0, basicamt = 0, nettotal = 0, Addtax = 0;
                                    for (int t = 0; t < taxgroup.Rows.Count; t++)
                                    {
                                        HSNCODE += Environment.NewLine + taxgroup.Rows[t]["Hsn_Sac_Code"].ToString();
                                        taxable += Environment.NewLine + taxgroup.Rows[t]["basicamount"].ToString();
                                        basicamt += Convert.ToDouble(taxgroup.Rows[t]["basicamount"].ToString());

                                        cgstper += Environment.NewLine + (Convert.ToDouble(taxgroup.Rows[t]["igstper"].ToString()) / 2).ToString();
                                        cgstamt += Environment.NewLine + taxgroup.Rows[t]["cgst"].ToString();
                                        cgst += Convert.ToDouble(taxgroup.Rows[t]["cgst"].ToString());

                                        sgstper += Environment.NewLine + (Convert.ToDouble(taxgroup.Rows[t]["igstper"].ToString()) / 2).ToString();
                                        sgstamt += Environment.NewLine + taxgroup.Rows[t]["sgst"].ToString();
                                        sgst += Convert.ToDouble(taxgroup.Rows[t]["sgst"].ToString());

                                        igstper += Environment.NewLine + (Convert.ToDouble(taxgroup.Rows[t]["igstper"].ToString())).ToString();
                                        igstamt += Environment.NewLine + taxgroup.Rows[t]["igdtamt"].ToString();

                                        addper += Environment.NewLine + (Convert.ToDouble(taxgroup.Rows[t]["addtaxper"].ToString()) / 2).ToString();
                                        addamt += Environment.NewLine + taxgroup.Rows[t]["addtax"].ToString();
                                        Addtax += Convert.ToDouble(taxgroup.Rows[t]["addtax"].ToString());

                                        totalamt += Environment.NewLine + taxgroup.Rows[t]["total"].ToString();
                                        nettotal += Convert.ToDouble(taxgroup.Rows[t]["total"].ToString());
                                    }
                                    Double a = Convert.ToDouble(TxtBillTotal.Text);
                                    Double b = 0;
                                    try
                                    {
                                        b = Convert.ToDouble(privousbalance);
                                    }
                                    catch
                                    {
                                    }
                                    Double c = a + b;
                                    //string serialno = Regex.Replace(LVFO.Items[i].SubItems[22].Text, @"\s+", string.Empty);
                                    string serialno = LVFO.Items[i].SubItems[22].Text.Trim();
                                    string customername = "";
                                    string address = "";
                                    string city = "";
                                    string phone = "";
                                    string mobile = "";
                                    if (strfinalarray[0] == "S" || strfinalarray[0] == "P")
                                    {
                                        string account = cmbcustname.Text.ToUpper();
                                        if (cmbterms.Text == "Cash" && account == "CASH")
                                        {
                                            customername = txtcustomername.Text;
                                            address = txtaddress.Text;
                                            city = txtcity.Text;
                                            phone = txtphone.Text;
                                            mobile = txtmobile.Text;

                                        }
                                        else
                                        {
                                            customername = client.Rows[0][2].ToString();
                                            address = client.Rows[0][6].ToString();
                                            city = client.Rows[0][7].ToString();
                                            phone = client.Rows[0][9].ToString();
                                            mobile = client.Rows[0][10].ToString();
                                        }
                                    }
                                    else
                                    {
                                        customername = client.Rows[0][2].ToString();
                                        address = client.Rows[0][6].ToString();
                                        city = client.Rows[0][7].ToString();
                                        phone = client.Rows[0][9].ToString();
                                        mobile = client.Rows[0][10].ToString();
                                    }
                                    if (strfinalarray[0] == "S")
                                    {
                                        creditdays = client.Rows[0]["credaysale"].ToString();
                                    }
                                    else if (strfinalarray[0] == "P")
                                    {
                                        creditdays = client.Rows[0]["credaypurchase"].ToString();
                                    }
                                    else
                                    {
                                        creditdays = "";
                                    }
                                    scessamt = Convert.ToDouble(hsn.Rows[0]["cessamt"].ToString()) * Convert.ToDouble(LVFO.Items[i].SubItems[5].Text);
                                    string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80,T81,T82,T83,T84,T85,T86,T87,T88,T89,T90,T91,T92,T93,T94,T95,T96,T97,T98,T99,T100,P1,P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14,P15,P16,P17,P18,P19,P20,P21,P22,P23,P24,P25,P26,P27,P28,P29,P30,P31,P32,P33,P34,P35,P36,P37,P38,P39,P40,P41,P42,P43,P44,P45,P46,P47,P48,P49,P50,P51,P52,P53,P54,P55,P56)VALUES";
                                    qry += "('" + j++ + "','" + TxtBillNo.Text + "','" + TxtRundate.Text + "','" + cmbterms.Text + "','" + txtduedate.Text + "','" + cmbcustname.Text + "','" + txtpono.Text + "','" + cmbsaletype.Text + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[3].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text + "','" + LVFO.Items[i].SubItems[10].Text + "','" + LVFO.Items[i].SubItems[11].Text + "','" + LVFO.Items[i].SubItems[12].Text + "','" + LVFO.Items[i].SubItems[13].Text + "','" + LVFO.Items[i].SubItems[14].Text + "','" + lbltotcount.Text + "','" + lbltotpqty.Text + "','" + txttotaqty.Text + "','" + txttotfree.Text + "','" + lblbasictot.Text + "','" + txttotdiscount.Text + "','" + txttotadis.Text + "','" + txttottax.Text + "','" + txttotaddvat.Text + "','" + txtamt.Text + "','" + txttotservice.Text + "','" + txttotalcharges.Text + "','" + txtroundoff.Text + "','" + TxtBillTotal.Text + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + inword + "','" + txttransport.Text + "','" + txtdelieveryat.Text + "','" + txtfraight.Text + "','" + txtvehicleno.Text + "','" + txtgrrrno.Text + "','" + txtremarks.Text + "','" + txtweight.Text + "','" + txtskids.Text + "','" + perticulars + "','" + remarks + "','" + value + "','" + at + "','" + plusminus + "','" + amount + "','" + client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + customername + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + address + "','" + city + "','" + client.Rows[0][8].ToString() + "','" + phone + "','" + mobile + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString() + "','" + LVFO.Items[i].SubItems[15].Text + "','" + LVFO.Items[i].SubItems[16].Text + "','" + LVFO.Items[i].SubItems[17].Text + "','" + LVFO.Items[i].SubItems[18].Text + "','" + LVFO.Items[i].SubItems[19].Text + "','" + LVFO.Items[i].SubItems[20].Text + "','" + LVFO.Items[i].SubItems[21].Text + "','" + lblsgsttotsl.Text + "','" + lblcgattotal.Text + "','" + lbligsttotal.Text + "','" + valuelvf + "','" + taxlvf + "','" + sgstlvf + "','" + cgstlvf + "','" + igstlvf + "','" + addtaxlvf + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + client.Rows[0][17].ToString() + "','" + serialno + "','" + printrefno + "','" + item.Rows[0]["BasicPrice"].ToString() + "','" + item.Rows[0]["SalePrice"].ToString() + "','" + item.Rows[0]["MRP"].ToString() + "','" + item.Rows[0]["PurchasePrice"].ToString() + "','" + item.Rows[0]["Barcode"].ToString() + "','" + item.Rows[0]["ExpDt"].ToString() + "','" + item.Rows[0]["mfgdt"].ToString() + "','" + item.Rows[0]["Expdays"].ToString() + "','" + taxable + "','" + cgstper + "','" + cgstamt + "','" + sgstper + "','" + sgstamt + "','" + totalamt + "','" + basicamt + "','" + cgst + "','" + sgst + "','" + nettotal.ToString("N2") + "','" + addper + "','" + addamt + "','" + Addtax + "','" + HSNCODE + "','" + client.Rows[0][19].ToString() + "','" + saletype.Rows[0]["InvoiceHeading"].ToString() + "','" + igstper + "','" + igstamt + "','" + dt1.Rows[0]["PANNo"].ToString() + "','" + client.Rows[0]["Vatno"].ToString() + "','" + client.Rows[0]["accountnumber"].ToString() + "','" + privousbalance + "','" + c + "','" + txtf1.Text + "','" + txtf2.Text + "','" + txtf3.Text + "','" + txtf4.Text + "','" + txtf5.Text + "','" + txtf6.Text + "','" + txtf7.Text + "','" + txtf8.Text + "','" + txtf9.Text + "','" + txtf10.Text + "','" + txtf11.Text + "','" + txtf12.Text + "','" + txtf13.Text + "','" + txtf14.Text + "','" + txtf15.Text + "','" + client.Rows[0]["noteorremarks"].ToString() + "','" + hsn.Rows[0]["cessper"].ToString() + "','" + LVFO.Items[i].SubItems[23].Text + "','" + scessamt + "','" + creditdays + "','" + LVFO.Items[i].SubItems[25].Text + "','" + client.Rows[0]["Vatno"].ToString() + "','" + client.Rows[0]["credaysale"].ToString() + "','" + hsn.Rows[0]["itemnumber"].ToString() + "','" + item.Rows[0]["batchPartCode"].ToString() + "')";
                                    prn.execute(qry);
                                    LogGenerator.Info(qry);
                                    scessamt = 0;
                                }
                                catch
                                {
                                }
                            }
                            Print popup = new Print(strfinalarray[2]);
                            //popup.ShowDialog();
                            //popup.Dispose();
                        }
                        else
                        {
                            DialogResult dr1 = MessageBox.Show("Do you want to Print Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr1 == DialogResult.Yes)
                            {
                                DataTable client = conn.getdataset("select * from clientmaster where isactive=1 and clientID='" + cmbcustname.SelectedValue + "'");
                                DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                                prn.execute("delete from printing");
                                int j = 1;
                                string perticulars = "", remarks = "", value = "", at = "", plusminus = "", amount = "", valuelvf = "", taxlvf = "", sgstlvf = "", cgstlvf = "", igstlvf = "", addtaxlvf = "";
                                for (int i = 0; i < LVCHARGES.Items.Count; i++)
                                {
                                    perticulars += LVCHARGES.Items[i].SubItems[0].Text + Environment.NewLine;
                                    remarks += LVCHARGES.Items[i].SubItems[1].Text + Environment.NewLine;
                                    value += LVCHARGES.Items[i].SubItems[2].Text + Environment.NewLine;
                                    at += LVCHARGES.Items[i].SubItems[3].Text + Environment.NewLine;
                                    plusminus += LVCHARGES.Items[i].SubItems[4].Text + Environment.NewLine;
                                    amount += LVCHARGES.Items[i].SubItems[5].Text + Environment.NewLine;
                                    valuelvf += LVCHARGES.Items[i].SubItems[6].Text + Environment.NewLine;
                                    taxlvf += LVCHARGES.Items[i].SubItems[7].Text + Environment.NewLine;
                                    sgstlvf += LVCHARGES.Items[i].SubItems[8].Text + Environment.NewLine;
                                    cgstlvf += LVCHARGES.Items[i].SubItems[9].Text + Environment.NewLine;
                                    igstlvf += LVCHARGES.Items[i].SubItems[10].Text + Environment.NewLine;
                                    addtaxlvf += LVCHARGES.Items[i].SubItems[11].Text + Environment.NewLine;

                                }
                                string printrefno;
                                if (BtnPayment.Text == "Update")
                                {
                                    printrefno = refnoupdate;
                                }
                                else
                                {
                                    printrefno = refno;
                                }
                                for (int i = 0; i < LVFO.Items.Count; i++)
                                {
                                    try
                                    {
                                        DataTable hsn = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + LVFO.Items[i].SubItems[24].Text + "'");
                                        string h = "select * from ProductMaster where isactive=1 and ProductID='" + LVFO.Items[i].SubItems[24].Text + "'";
                                        LogGenerator.Info(h);
                                        DataTable item = conn.getdataset("select * from ProductPriceMaster where isactive=1 and ProductID='" + hsn.Rows[0]["ProductID"].ToString() + "' and Batchno='" + LVFO.Items[i].SubItems[2].Text + "'");
                                        string u = "select * from ProductPriceMaster where isactive=1 and ProductID='" + hsn.Rows[0]["ProductID"].ToString() + "' and Batchno='" + LVFO.Items[i].SubItems[2].Text + "'";
                                        LogGenerator.Info(u);
                                        //DataTable item1 = conn.getdataset("select * from ItemTaxMaster where isactive=1 and ProductID='" + hsn.Rows[0]["ProductID"].ToString() + "' and saletypeid='"+cmbsaletype.Text+"'");
                                        //old query  DataTable taxgroup = conn.getdataset("select sum(amount)-sum(discountamt) as basicamount,sum(sgstamt) as sgst,sum(cgstamt) as cgst,sum(amount)+sum(sgstamt)+sum(cgstamt)-sum(discountamt) as total, igdtamt ,sum(addtax) as addtax,sum(addtaxper) as addtaxper from BillProductMaster WHERE isactive=1 and billno='" + TxtBillNo.Text + "' group by igdtamt");
                                        //new query created by sir ==select p.Hsn_Sac_Code,b.Productname,sum(b.Total),b.sgstper,sum( b.sgstamt),b.cgstper, sum(b.cgstamt),b.igstper,sum(b.igdtamt),b.addtaxper,sum(b.addtax),sum(b.Amount) from billproductmaster b inner join ProductMaster p on p.Product_Name=b.Productname  where  b.isactive=1 and p.isactive=1 group by p.Hsn_Sac_Code,b.Productname,b.sgstper,b.cgstper,b.igstper,b.addtaxper
                                        DataTable saletype = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbsaletype.Text + "'");
                                        string o = "select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbsaletype.Text + "'";
                                        LogGenerator.Info(o);
                                        DataTable taxgroup = conn.getdataset("select p.Hsn_Sac_Code,sum(b.Total)-sum(b.discountamt) as basicamount,b.sgstper as sgstper,sum( b.sgstamt) as sgst,b.cgstper as cgstper, sum(b.cgstamt) as cgst,sgstper + cgstper + igstper as igstper ,sum(b.igdtamt) as igdtamt,b.addtaxper,sum(b.addtax) as addtax,sum(b.Total)+sum(b.sgstamt)+sum(b.cgstamt)+(b.igdtamt)-sum(b.discountamt) as total from billproductmaster b inner join ProductMaster p on p.Product_Name=b.Productname where  b.billno='" + TxtBillNo.Text + "' and b.billtype='" + strfinalarray[0] + "' and b.isactive=1 and p.isactive=1 group by p.Hsn_Sac_Code,b.sgstper,b.cgstper,b.igstper,b.addtaxper,b.igdtamt");
                                        string z = "select p.Hsn_Sac_Code,sum(b.Total)-sum(b.discountamt) as basicamount,b.sgstper as sgstper,sum( b.sgstamt) as sgst,b.cgstper as cgstper, sum(b.cgstamt) as cgst,sum(b.sgstper + b.cgstper + b.igstper) as igstper ,sum(b.igdtamt) as igdtamt,b.addtaxper,sum(b.addtax) as addtax,sum(b.Total)+sum(b.sgstamt)+sum(b.cgstamt)+(b.igdtamt)-sum(b.discountamt) as total from billproductmaster b inner join ProductMaster p on p.Product_Name=b.Productname where  b.billno='" + TxtBillNo.Text + "' and b.isactive=1 and p.isactive=1 group by p.Hsn_Sac_Code,b.sgstper,b.cgstper,b.igstper,b.addtaxper,b.igdtamt";
                                        LogGenerator.Info(z);
                                        string HSNCODE = "HSN/SAC", taxable = "Taxable", cgstper = "CGST % ", cgstamt = "AMT.", sgstper = "SGST %", sgstamt = "AMT.", totalamt = "Total", addper = "AddTax%", addamt = "Amt.", igstper = "Igst %", igstamt = "Amt.";
                                        double cgst = 0, sgst = 0, basicamt = 0, nettotal = 0, Addtax = 0;
                                        for (int t = 0; t < taxgroup.Rows.Count; t++)
                                        {
                                            HSNCODE += Environment.NewLine + taxgroup.Rows[t]["Hsn_Sac_Code"].ToString();
                                            taxable += Environment.NewLine + taxgroup.Rows[t]["basicamount"].ToString();
                                            basicamt += Convert.ToDouble(taxgroup.Rows[t]["basicamount"].ToString());

                                            cgstper += Environment.NewLine + (Convert.ToDouble(taxgroup.Rows[t]["igstper"].ToString()) / 2).ToString();
                                            cgstamt += Environment.NewLine + taxgroup.Rows[t]["cgst"].ToString();
                                            cgst += Convert.ToDouble(taxgroup.Rows[t]["cgst"].ToString());

                                            sgstper += Environment.NewLine + (Convert.ToDouble(taxgroup.Rows[t]["igstper"].ToString()) / 2).ToString();
                                            sgstamt += Environment.NewLine + taxgroup.Rows[t]["sgst"].ToString();
                                            sgst += Convert.ToDouble(taxgroup.Rows[t]["sgst"].ToString());

                                            igstper += Environment.NewLine + (Convert.ToDouble(taxgroup.Rows[t]["igstper"].ToString())).ToString();
                                            igstamt += Environment.NewLine + taxgroup.Rows[t]["igdtamt"].ToString();

                                            addper += Environment.NewLine + (Convert.ToDouble(taxgroup.Rows[t]["addtaxper"].ToString()) / 2).ToString();
                                            addamt += Environment.NewLine + taxgroup.Rows[t]["addtax"].ToString();
                                            Addtax += Convert.ToDouble(taxgroup.Rows[t]["addtax"].ToString());

                                            totalamt += Environment.NewLine + taxgroup.Rows[t]["total"].ToString();
                                            nettotal += Convert.ToDouble(taxgroup.Rows[t]["total"].ToString());
                                        }
                                        Double a = Convert.ToDouble(TxtBillTotal.Text);
                                        Double b = 0;
                                        try
                                        {
                                            b = Convert.ToDouble(privousbalance);
                                        }
                                        catch
                                        {
                                        }
                                        Double c = a + b;
                                        //string serialno = Regex.Replace(LVFO.Items[i].SubItems[22].Text, @"\s+", string.Empty);
                                        string serialno = LVFO.Items[i].SubItems[22].Text.Trim();
                                        string customername = "";
                                        string address = "";
                                        string city = "";
                                        string phone = "";
                                        string mobile = "";
                                        if (strfinalarray[0] == "S" || strfinalarray[0] == "P")
                                        {
                                            string account = cmbcustname.Text.ToUpper();
                                            if (cmbterms.Text == "Cash" && account == "CASH")
                                            {
                                                customername = txtcustomername.Text;
                                                address = txtaddress.Text;
                                                city = txtcity.Text;
                                                phone = txtphone.Text;
                                                mobile = txtmobile.Text;

                                            }
                                            else
                                            {
                                                customername = client.Rows[0][2].ToString();
                                                address = client.Rows[0][6].ToString();
                                                city = client.Rows[0][7].ToString();
                                                phone = client.Rows[0][9].ToString();
                                                mobile = client.Rows[0][10].ToString();
                                            }
                                        }
                                        else
                                        {
                                            customername = client.Rows[0][2].ToString();
                                            address = client.Rows[0][6].ToString();
                                            city = client.Rows[0][7].ToString();
                                            phone = client.Rows[0][9].ToString();
                                            mobile = client.Rows[0][10].ToString();
                                        }
                                        if (strfinalarray[0] == "S")
                                        {
                                            creditdays = client.Rows[0]["credaysale"].ToString();
                                        }
                                        else if (strfinalarray[0] == "P")
                                        {
                                            creditdays = client.Rows[0]["credaypurchase"].ToString();
                                        }
                                        else
                                        {
                                            creditdays = "";
                                        }
                                        scessamt = Convert.ToDouble(hsn.Rows[0]["cessamt"].ToString()) * Convert.ToDouble(LVFO.Items[i].SubItems[5].Text);
                                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58,T59,T60,T61,T62,T63,T64,T65,T66,T67,T68,T69,T70,T71,T72,T73,T74,T75,T76,T77,T78,T79,T80,T81,T82,T83,T84,T85,T86,T87,T88,T89,T90,T91,T92,T93,T94,T95,T96,T97,T98,T99,T100,P1,P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14,P15,P16,P17,P18,P19,P20,P21,P22,P23,P24,P25,P26,P27,P28,P29,P30,P31,P32,P33,P34,P35,P36,P37,P38,P39,P40,P41,P42,P43,P44,P45,P46,P47,P48,P49,P50,P51,P52,P53,P54,P55,P56)VALUES";
                                        qry += "('" + j++ + "','" + TxtBillNo.Text + "','" + TxtRundate.Text + "','" + cmbterms.Text + "','" + txtduedate.Text + "','" + cmbcustname.Text + "','" + txtpono.Text + "','" + cmbsaletype.Text + "','" + LVFO.Items[i].SubItems[0].Text + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[3].Text + "','" + LVFO.Items[i].SubItems[4].Text + "','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text + "','" + LVFO.Items[i].SubItems[10].Text + "','" + LVFO.Items[i].SubItems[11].Text + "','" + LVFO.Items[i].SubItems[12].Text + "','" + LVFO.Items[i].SubItems[13].Text + "','" + LVFO.Items[i].SubItems[14].Text + "','" + lbltotcount.Text + "','" + lbltotpqty.Text + "','" + txttotaqty.Text + "','" + txttotfree.Text + "','" + lblbasictot.Text + "','" + txttotdiscount.Text + "','" + txttotadis.Text + "','" + txttottax.Text + "','" + txttotaddvat.Text + "','" + txtamt.Text + "','" + txttotservice.Text + "','" + txttotalcharges.Text + "','" + txtroundoff.Text + "','" + TxtBillTotal.Text + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + inword + "','" + txttransport.Text + "','" + txtdelieveryat.Text + "','" + txtfraight.Text + "','" + txtvehicleno.Text + "','" + txtgrrrno.Text + "','" + txtremarks.Text + "','" + txtweight.Text + "','" + txtskids.Text + "','" + perticulars + "','" + remarks + "','" + value + "','" + at + "','" + plusminus + "','" + amount + "','" + client.Rows[0][0].ToString() + "','" + client.Rows[0][1].ToString() + "','" + customername + "','" + client.Rows[0][3].ToString() + "','" + client.Rows[0][4].ToString() + "','" + client.Rows[0][5].ToString() + "','" + address + "','" + city + "','" + client.Rows[0][8].ToString() + "','" + phone + "','" + mobile + "','" + client.Rows[0][11].ToString() + "','" + client.Rows[0][12].ToString() + "','" + client.Rows[0][13].ToString() + "','" + LVFO.Items[i].SubItems[15].Text + "','" + LVFO.Items[i].SubItems[16].Text + "','" + LVFO.Items[i].SubItems[17].Text + "','" + LVFO.Items[i].SubItems[18].Text + "','" + LVFO.Items[i].SubItems[19].Text + "','" + LVFO.Items[i].SubItems[20].Text + "','" + LVFO.Items[i].SubItems[21].Text + "','" + lblsgsttotsl.Text + "','" + lblcgattotal.Text + "','" + lbligsttotal.Text + "','" + valuelvf + "','" + taxlvf + "','" + sgstlvf + "','" + cgstlvf + "','" + igstlvf + "','" + addtaxlvf + "','" + hsn.Rows[0]["Hsn_Sac_Code"].ToString() + "','" + client.Rows[0][17].ToString() + "','" + serialno + "','" + printrefno + "','" + item.Rows[0]["BasicPrice"].ToString() + "','" + item.Rows[0]["SalePrice"].ToString() + "','" + item.Rows[0]["MRP"].ToString() + "','" + item.Rows[0]["PurchasePrice"].ToString() + "','" + item.Rows[0]["Barcode"].ToString() + "','" + item.Rows[0]["ExpDt"].ToString() + "','" + item.Rows[0]["mfgdt"].ToString() + "','" + item.Rows[0]["Expdays"].ToString() + "','" + taxable + "','" + cgstper + "','" + cgstamt + "','" + sgstper + "','" + sgstamt + "','" + totalamt + "','" + basicamt + "','" + cgst + "','" + sgst + "','" + nettotal.ToString("N2") + "','" + addper + "','" + addamt + "','" + Addtax + "','" + HSNCODE + "','" + client.Rows[0][19].ToString() + "','" + saletype.Rows[0]["InvoiceHeading"].ToString() + "','" + igstper + "','" + igstamt + "','" + dt1.Rows[0]["PANNo"].ToString() + "','" + client.Rows[0]["Vatno"].ToString() + "','" + client.Rows[0]["accountnumber"].ToString() + "','" + privousbalance + "','" + c + "','" + txtf1.Text + "','" + txtf2.Text + "','" + txtf3.Text + "','" + txtf4.Text + "','" + txtf5.Text + "','" + txtf6.Text + "','" + txtf7.Text + "','" + txtf8.Text + "','" + txtf9.Text + "','" + txtf10.Text + "','" + txtf11.Text + "','" + txtf12.Text + "','" + txtf13.Text + "','" + txtf14.Text + "','" + txtf15.Text + "','" + client.Rows[0]["noteorremarks"].ToString() + "','" + hsn.Rows[0]["cessper"].ToString() + "','" + LVFO.Items[i].SubItems[23].Text + "','" + scessamt + "','" + creditdays + "','" + LVFO.Items[i].SubItems[25].Text + "','" + client.Rows[0]["Vatno"].ToString() + "','" + client.Rows[0]["credaysale"].ToString() + "','" + hsn.Rows[0]["itemnumber"].ToString() + "','" + item.Rows[0]["batchPartCode"].ToString() + "')";
                                        prn.execute(qry);
                                        LogGenerator.Info(qry);
                                        scessamt = 0;
                                    }
                                    catch
                                    {
                                    }
                                }
                                Print popup = new Print(strfinalarray[2]);
                                popup.ShowDialog();
                                popup.Dispose();
                            }
                        }



                    }
                    else
                    {
                        MessageBox.Show("You don't have Permission To Print Bill First Save Bill");
                        BtnPayment.Focus();
                    }
                }
        //   }
        }

        public void clearitem()
        {

            txtboxsrno.Text = string.Empty;
            txtitemname.Text = string.Empty;
            txtpacking.Text = string.Empty;
            txtbags.Text = string.Empty;
            qtyflag = 1;
            txtqty.Text = string.Empty;
            qtyflag = 1;
            txtpqty.Text = string.Empty;
            txtrate.Text = string.Empty;
            txtper.Text = string.Empty;
            txttotal.Text = string.Empty;
            txttax.Text = string.Empty;
            txtaddtax.Text = string.Empty;
            txtamount.Text = string.Empty;
            lbladdtax1.Text = "[]";
            lbltax1.Text = "[]";
            lblbagqty.Text = "[]";
            lblaltqty.Text = "[]";
            txtfree.Text = "";
            txtdisamt.Text = "";
            txtdisper.Text = "";
            cmbbatch.DataSource = null;
            cmbbatch.Items.Clear();
            lvserial.Items.Clear();
            //temptable = new DataTable();
            //temptable.Clear();
            //cmbbatch.Items.Clear();
            // cmbbatch.SelectedIndex = -1;
            //  cmbbatch.ResetText();




        }
        void getsr()
        {
            try
            {

                String str = conn.ExecuteScalar("select max(Bill_No) from BillMaster where isactive='1' and billtype='" + strfinalarray[0] + "' and saletype='" + cmbsaletype.SelectedValue + "' and id=(select max(id) from billmaster where isactive=1 and BillType='" + strfinalarray[0] + "' and SaleType='" + cmbsaletype.SelectedValue + "' )");
                var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-" };
                foreach (var c in charsToRemove)
                {
                    str = str.Replace(c, string.Empty);
                }
                DataTable dt = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypeid='" + cmbsaletype.SelectedValue + "'");
                Int64 id, count;
                //     Object data = dr[1];

                if (str == "")
                {

                    id = Convert.ToInt64(dt.Rows[0]["startingno"].ToString());
                    count = Convert.ToInt64(dt.Rows[0]["startingno"].ToString());
                }
                else
                {
                    id = Convert.ToInt64(str) + 1;
                    count = Convert.ToInt64(str) + 1;
                }
                lblbill_no.Text = count.ToString();
                TxtBillNo.Text = dt.Rows[0]["prefix"].ToString() + count.ToString();

            }
            catch
            {
            }
            finally
            {

            }

        }
        public void clearall()
        {
            //getsr();
            // TxtRundate.Text = DateTime.Now.ToShortDateString();
            txtf1.Text = "";
            txtf2.Text = "";
            txtf3.Text = "";
            txtf4.Text = "";
            txtf5.Text = "";
            txtf6.Text = "";
            txtf7.Text = "";
            txtf8.Text = "";
            txtf9.Text = "";
            txtf10.Text = "";
            txtf11.Text = "";
            txtf12.Text = "";
            txtf13.Text = "";
            txtf14.Text = "";
            txtf15.Text = "";
            txtcustomername.Text = string.Empty;
            txtaddress.Text = string.Empty;
            txtcity.Text = string.Empty;
            txtphone.Text = string.Empty;
            txtmobile.Text = string.Empty;
            txtpanno.Text = string.Empty;
            txtadharno.Text = string.Empty;
            cmbterms.SelectedIndex = -1;
            cmbcustname.SelectedIndex = -1;
            cmbsaletype.SelectedIndex = -1;
            txtpono.Text = string.Empty;
            lbltotpqty.Text = string.Empty;
            txttottax.Text = string.Empty;
            TxtBillTotal.Text = string.Empty;
            lblbasictot.Text = "0";
            txttottax.Text = "0";
            lbltotcount.Text = "0";
            lbltotpqty.Text = "0";
            txtweight.Text = string.Empty;
            txttransport.Text = string.Empty;
            txtremarks.Text = string.Empty;
            TxtBillNo.Text = "";
            TxtRundate.Focus();
            this.ActiveControl = TxtRundate;
            txtdelieveryat.Text = "";
            TxtBillNo.ReadOnly = true;
            temptable.Rows.Clear();
            cmbagentname.SelectedIndex = -1;
            pnlagent.Visible = false;
            txtfraight.Text = string.Empty;
            txtgrrrno.Text = string.Empty;
            txtskids.Text = string.Empty;
            txtvehicleno.Text = string.Empty;
        }
        public static string statusreg = string.Empty;
        public static string Decrypstatus(string cipherText)
        {
            string EncryptionKey = "00";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    statusreg = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static string privousbalance = string.Empty;
        DataTable SPdt = new DataTable();
        public void databind()
        {
            try
            {
                if ((cmbcustname.Text).ToUpper() == "CASH" || cmbcustname.SelectedValue == "101")
                {
                    #region
                    //  #region
                    ////  string totaldebit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and OT1='" + cmbaccname.Text + "' and dc='D' and isactive=1");
                    ////  string totalcredit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and OT1='" + cmbaccname.Text + "' and dc='C' and isactive=1");
                    //  string totaldebit = "";
                    //  string totalcredit = "";
                    //  if (totaldebit == "" || totaldebit == "NULL")
                    //  {
                    //      totaldebit = "0.00";
                    //  }
                    //  if (totalcredit == "" || totalcredit == "NULL")
                    //  {
                    //      totalcredit = "0.00";
                    //  }
                    //  Double opbal;
                    //  string DC = "";
                    //  DataTable opbalance = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID=" + cmbcustname.SelectedValue);
                    //  string stropbal = opbalance.Rows[0]["opbal"].ToString();
                    //  string strDC = opbalance.Rows[0]["Dr_cr"].ToString();
                    //  if (strDC == "Dr.")
                    //  {
                    //      double ttdebit = Convert.ToDouble(totaldebit) + Convert.ToDouble(stropbal);
                    //      totaldebit = ttdebit.ToString();
                    //  }
                    //  else if (strDC == "Cr.")
                    //  {
                    //      double ttcredit = Convert.ToDouble(totalcredit) + Convert.ToDouble(stropbal);
                    //      totalcredit = ttcredit.ToString();
                    //  }

                    //  if (Convert.ToDouble(totaldebit) >= Convert.ToDouble(totalcredit))
                    //  {
                    //      opbal = Convert.ToDouble(totaldebit) - Convert.ToDouble(totalcredit);
                    //   //   txtopbal.Text = opbal.ToString("N2") + " Dr.";
                    //      DC = "D";
                    //  }
                    //  else
                    //  {
                    //      opbal = Convert.ToDouble(totalcredit) - Convert.ToDouble(totaldebit);
                    //    //  txtopbal.Text = opbal.ToString("N2") + " Cr.";
                    //      DC = "C";
                    //  }

                    //  #endregion

                    //  //for create ledger
                    ////  mouseclickid.Rows.Clear();
                    ////  LVledger.Items.Clear();
                    //  #region
                    //  DataTable SPdt = conn.getdataset("select * from Ledger where isactive=1 and  OT1='" + cmbcustname.Text + "' order by Date1");
                    //  string balance = "0.00";
                    //  balance = Convert.ToString(opbal);
                    //  Double debit = 0, credit = 0;
                    //  for (int i = 0; i < SPdt.Rows.Count; i++)
                    //  {
                    //      if (SPdt.Rows[i]["TranType"].ToString() == "Sale")
                    //      {
                    //          //ListViewItem li;
                    //          //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          //li.SubItems.Add("Sale");
                    //          //li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                    //          //li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                    //          //debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //          //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //li.SubItems.Add("");
                    //          //mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();

                    //              opbal = Convert.ToDouble(str[0]);
                    //          }

                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                    //        //  li.SubItems.Add(balance);
                    //      }
                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "Cash Recept")
                    //      {
                    //          //ListViewItem li;
                    //          //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          //li.SubItems.Add("Cash Recept");
                    //          //li.SubItems.Add("Sales");
                    //          //li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                    //          //li.SubItems.Add("");
                    //          //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //          //mouseclickid.Rows.Add("Cash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();
                    //              opbal = Convert.ToDouble(str[0]);
                    //          }
                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                    //        //  li.SubItems.Add(balance);
                    //      }
                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "Rect")
                    //      {
                    //          //ListViewItem li;
                    //          //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          //li.SubItems.Add("Rect");
                    //          //li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                    //          //li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                    //          //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //li.SubItems.Add("");
                    //          //debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //          //mouseclickid.Rows.Add("Rect", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();
                    //              opbal = Convert.ToDouble(str[0]);
                    //          }
                    //          string CD = SPdt.Rows[i]["dc"].ToString();
                    //          if (CD == "C")
                    //          {
                    //              CD = "D";
                    //          }
                    //          else
                    //          {
                    //              CD = "C";
                    //          }
                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);

                    //        //  li.SubItems.Add(balance);
                    //      }
                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "SaleReturn")
                    //      {
                    //          //ListViewItem li;
                    //          //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          //li.SubItems.Add("Sale Return");
                    //          //li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                    //          //li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                    //          //li.SubItems.Add("");
                    //          //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //          //mouseclickid.Rows.Add("Sale Return", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();

                    //              opbal = Convert.ToDouble(str[0]);
                    //          }
                    //          if (DC == "C")
                    //          {
                    //              DC = "D";
                    //          }
                    //          else
                    //          {
                    //              DC = "C";
                    //          }
                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                    //        //  li.SubItems.Add(balance);
                    //      }
                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "Purchase")
                    //      {
                    //          //ListViewItem li;
                    //          //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          //li.SubItems.Add("Purchase");
                    //          //li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                    //          //li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                    //          //li.SubItems.Add("");
                    //          //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //          //mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();

                    //              opbal = Convert.ToDouble(str[0]);
                    //          }

                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                    //        //  li.SubItems.Add(balance);
                    //      }
                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "PurchaseReturn")
                    //      {
                    //          //ListViewItem li;
                    //          //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          //li.SubItems.Add("Purchase Return");
                    //          //li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                    //          //li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                    //          //li.SubItems.Add("");
                    //          //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //          //mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();

                    //              opbal = Convert.ToDouble(str[0]);
                    //          }
                    //          //if (DC == "C")
                    //          //{
                    //          //    DC = "D";
                    //          //}
                    //          //else
                    //          //{
                    //          //    DC = "C";
                    //          //}
                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                    //         // li.SubItems.Add(balance);
                    //      }
                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "Pmnt")
                    //      {
                    //          //ListViewItem li;
                    //          //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          //li.SubItems.Add("Pmnt");
                    //          //li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                    //          //li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                    //          //li.SubItems.Add("");
                    //          //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));

                    //          //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //          //mouseclickid.Rows.Add("Pmnt", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();

                    //              opbal = Convert.ToDouble(str[0]);
                    //          }
                    //          string CD = SPdt.Rows[i]["dc"].ToString();
                    //          if (CD == "C")
                    //          {
                    //              CD = "D";
                    //          }
                    //          else
                    //          {
                    //              CD = "C";
                    //          }
                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                    //        //  li.SubItems.Add(balance);
                    //      }
                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "Cash Invoice")
                    //      {
                    //          //ListViewItem li;
                    //          //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          //li.SubItems.Add("Cash Invoice");
                    //          //li.SubItems.Add("Purchases");
                    //          //li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                    //          //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //          //li.SubItems.Add("");
                    //          //mouseclickid.Rows.Add("Cash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();
                    //              opbal = Convert.ToDouble(str[0]);
                    //          }
                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                    //         // li.SubItems.Add(balance);
                    //      }
                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque Issued")
                    //      {
                    //          //ListViewItem li;
                    //          //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          //li.SubItems.Add("Cheque Issued");
                    //          //li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                    //          //string CD = SPdt.Rows[i]["dc"].ToString();
                    //          //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          //if (CD == "D")
                    //          //{
                    //          //    //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          //    li.SubItems.Add("");
                    //          //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //}
                    //          //else
                    //          //{
                    //          //    li.SubItems.Add("");
                    //          //    //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //}
                    //          //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //          //mouseclickid.Rows.Add("Cheque Issued", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();

                    //              opbal = Convert.ToDouble(str[0]);
                    //          }
                    //          //CD = SPdt.Rows[i]["dc"].ToString();
                    //          //if (CD == "C")
                    //          //{
                    //          //    CD = "D";
                    //          //}
                    //          //else
                    //          //{
                    //          //    CD = "C";
                    //          //}
                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                    //        //  li.SubItems.Add(balance);
                    //       //   li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                    //      }
                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "Draft Issued")
                    //      {
                    //          //ListViewItem li;
                    //          //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          //li.SubItems.Add("Draft Issued");
                    //          //li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                    //          //string CD = SPdt.Rows[i]["dc"].ToString();
                    //          //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          //if (CD == "D")
                    //          //{
                    //          //    //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          //    li.SubItems.Add("");
                    //          //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //}
                    //          //else
                    //          //{
                    //          //    li.SubItems.Add("");
                    //          //    //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //}
                    //          //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //          //mouseclickid.Rows.Add("Draft Issued", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();

                    //              opbal = Convert.ToDouble(str[0]);
                    //          }
                    //          //CD = SPdt.Rows[i]["dc"].ToString();
                    //          //if (CD == "C")
                    //          //{
                    //          //    CD = "D";
                    //          //}
                    //          //else
                    //          //{
                    //          //    CD = "C";
                    //          //}
                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                    //         // li.SubItems.Add(balance);
                    //         // li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                    //      }
                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque/Draft/Rtgs Received")
                    //      {
                    //          //ListViewItem li;
                    //          //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          //li.SubItems.Add("Cheque/Draft/Rtgs Received");
                    //          //li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                    //          //string CD = SPdt.Rows[i]["dc"].ToString();
                    //          //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          //if (CD == "D")
                    //          //{
                    //          //    // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          //    li.SubItems.Add("");
                    //          //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //}
                    //          //else
                    //          //{
                    //          //    li.SubItems.Add("");
                    //          //    // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //}
                    //          //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //          //mouseclickid.Rows.Add("Cheque/Draft/Rtgs Received", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();

                    //              opbal = Convert.ToDouble(str[0]);
                    //          }
                    //          //CD = SPdt.Rows[i]["dc"].ToString();
                    //          //if (CD == "C")
                    //          //{
                    //          //    CD = "D";
                    //          //}
                    //          //else
                    //          //{
                    //          //    CD = "C";
                    //          //}
                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                    //        //  li.SubItems.Add(balance);
                    //        //  li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                    //      }

                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "Deposit Cash Into Bank")
                    //      {
                    //          //ListViewItem li;
                    //          //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          //li.SubItems.Add("Deposit Cash Into Bank");
                    //          //li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                    //          string CD = SPdt.Rows[i]["dc"].ToString();
                    //          //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          //if (CD == "D")
                    //          //{
                    //          //    //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          //    li.SubItems.Add("");
                    //          //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //}
                    //          //else
                    //          //{
                    //          //    li.SubItems.Add("");
                    //          //    // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //}
                    //          //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //          //mouseclickid.Rows.Add("Deposit Cash Into Bank", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();

                    //              opbal = Convert.ToDouble(str[0]);
                    //          }
                    //          //CD = SPdt.Rows[i]["dc"].ToString();
                    //          //if (CD == "C")
                    //          //{
                    //          //    CD = "D";
                    //          //}
                    //          //else
                    //          //{
                    //          //    CD = "C";
                    //          //}
                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                    //       //   li.SubItems.Add(balance);
                    //        //  li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                    //      }

                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "Withdraw Cash from Bank")
                    //      {
                    //          //ListViewItem li;
                    //          //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          //li.SubItems.Add("Withdraw Cash from Bank");
                    //          //li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                    //          string CD = SPdt.Rows[i]["dc"].ToString();
                    //          //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          //if (CD == "D")
                    //          //{
                    //          //    //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          //    li.SubItems.Add("");
                    //          //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //}
                    //          //else
                    //          //{
                    //          //    li.SubItems.Add("");
                    //          //    //  li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          //}
                    //          //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //          //mouseclickid.Rows.Add("Withdraw Cash from Bank", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();

                    //              opbal = Convert.ToDouble(str[0]);
                    //          }
                    //          //CD = SPdt.Rows[i]["dc"].ToString();
                    //          //if (CD == "C")
                    //          //{
                    //          //    CD = "D";
                    //          //}
                    //          //else
                    //          //{
                    //          //    CD = "C";
                    //          //}
                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                    //      //    li.SubItems.Add(balance);
                    //        //  li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                    //      }

                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "Bank Expenses")
                    //      {
                    //          ListViewItem li;
                    //          li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          li.SubItems.Add("Bank Expenses");
                    //          li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                    //          string CD = SPdt.Rows[i]["dc"].ToString();
                    //          li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          if (CD == "D")
                    //          {
                    //              // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //              li.SubItems.Add("");
                    //              li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          }
                    //          else
                    //          {
                    //              li.SubItems.Add("");
                    //              //   li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //              li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          }
                    //          credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //          mouseclickid.Rows.Add("Bank Expenses", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();

                    //              opbal = Convert.ToDouble(str[0]);
                    //          }
                    //          CD = SPdt.Rows[i]["dc"].ToString();
                    //          if (CD == "C")
                    //          {
                    //              CD = "D";
                    //          }
                    //          else
                    //          {
                    //              CD = "C";
                    //          }
                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                    //          li.SubItems.Add(balance);
                    //          li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                    //      }

                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "Online Transfer")
                    //      {
                    //          ListViewItem li;
                    //          li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          li.SubItems.Add("Online Transfer");
                    //          li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                    //          string CD = SPdt.Rows[i]["dc"].ToString();
                    //          li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //          if (CD == "D")
                    //          {
                    //              //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //              li.SubItems.Add("");
                    //              li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          }
                    //          else
                    //          {
                    //              li.SubItems.Add("");
                    //              // li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                    //              li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //          }
                    //          credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //          mouseclickid.Rows.Add("Online Transfer", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();

                    //              opbal = Convert.ToDouble(str[0]);
                    //          }
                    //          CD = SPdt.Rows[i]["dc"].ToString();
                    //          if (CD == "C")
                    //          {
                    //              CD = "D";
                    //          }
                    //          else
                    //          {
                    //              CD = "C";
                    //          }
                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, CD, i);
                    //          li.SubItems.Add(balance);
                    //          li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                    //      }
                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "DEBIT NOTE")
                    //      {
                    //          ListViewItem li;
                    //          li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          li.SubItems.Add("DEBIT NOTE");
                    //          li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                    //          li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                    //          string CD = SPdt.Rows[i]["dc"].ToString();
                    //          if (CD == "D")
                    //          {
                    //              li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //              debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //              li.SubItems.Add("");
                    //          }
                    //          else
                    //          {
                    //              li.SubItems.Add("");
                    //              li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //              credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                    //          }

                    //          mouseclickid.Rows.Add("DEBIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();
                    //              opbal = Convert.ToDouble(str[0]);
                    //          }
                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                    //          li.SubItems.Add(balance);
                    //          li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                    //      }
                    //      else if (SPdt.Rows[i]["TranType"].ToString() == "CREDIT NOTE")
                    //      {
                    //          ListViewItem li;
                    //          li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                    //          li.SubItems.Add("CREDIT NOTE");
                    //          li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                    //          li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                    //          string CD = SPdt.Rows[i]["dc"].ToString();
                    //          if (CD == "D")
                    //          {
                    //              li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //              debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                    //              li.SubItems.Add("");
                    //          }
                    //          else
                    //          {
                    //              li.SubItems.Add("");
                    //              li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                    //              credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                    //          }

                    //          mouseclickid.Rows.Add("CREDIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
                    //          if (i != 0)
                    //          {
                    //              string[] str = balance.Split(' ');
                    //              char temp = str[1][0];
                    //              DC = temp.ToString();
                    //              opbal = Convert.ToDouble(str[0]);
                    //          }
                    //          balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                    //          li.SubItems.Add(balance);
                    //          li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                    //      }

                    //  }
                    //  #endregion

                    // // txttotdebit.Text = debit.ToString("N2");
                    // // txttotcredit.Text = credit.ToString("N2");
                    // privousbalance = balance;
                    #endregion
                }
                else
                {
                    //for calculate OpBalance
                    #region
                    //  string totaldebit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and Accountid='" + cmbcustname.SelectedValue + "' and dc='D' and isactive=1 ");
                    //  string totalcredit = conn.ExecuteScalar("select sum(amount) from Ledger where Date1 < '" + DTPFrom.Text + "' and Accountid='" + cmbcustname.SelectedValue + "' and dc='C' and isactive=1 ");
                    string totaldebit = "";
                    string totalcredit = "";
                    if (totaldebit == "" || totaldebit == "NULL")
                    {
                        totaldebit = "0.00";
                    }
                    if (totalcredit == "" || totalcredit == "NULL")
                    {
                        totalcredit = "0.00";
                    }
                    Double opbal;
                    string DC = "";
                    DataTable opbalance = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID=" + cmbcustname.SelectedValue);
                    string stropbal = opbalance.Rows[0]["opbal"].ToString();
                    string strDC = opbalance.Rows[0]["Dr_cr"].ToString();
                    if (strDC == "Dr.")
                    {
                        double ttdebit = Convert.ToDouble(totaldebit) + Convert.ToDouble(stropbal);
                        totaldebit = ttdebit.ToString();
                    }
                    else if (strDC == "Cr.")
                    {
                        double ttcredit = Convert.ToDouble(totalcredit) + Convert.ToDouble(stropbal);
                        totalcredit = ttcredit.ToString();
                    }

                    if (Convert.ToDouble(totaldebit) >= Convert.ToDouble(totalcredit))
                    {
                        opbal = Convert.ToDouble(totaldebit) - Convert.ToDouble(totalcredit);
                        // txtopbal.Text = opbal.ToString("N2") + " Dr.";
                        DC = "D";
                    }
                    else
                    {
                        opbal = Convert.ToDouble(totalcredit) - Convert.ToDouble(totaldebit);
                        //  txtopbal.Text = opbal.ToString("N2") + " Cr.";
                        DC = "C";
                    }
                    #endregion
                    //for create ledger
                    //     mouseclickid.Rows.Clear();
                    //    LVledger.Items.Clear();
                    #region
                    if (BtnPayment.Text == "Update")
                    {
                        string bno = conn.ExecuteScalar("select id from Ledger where isactive=1 and Voucherid='" + TxtBillNo.Text + "' order by Date1");
                        int nbno = Convert.ToInt32(bno) - 1;
                        SPdt = conn.getdataset("select * from Ledger where isactive=1 and ID<'" + nbno + "' and Accountid='" + cmbcustname.SelectedValue + "' order by Date1");
                    }
                    else
                    {
                        SPdt = conn.getdataset("select * from Ledger where isactive=1 and Accountid='" + cmbcustname.SelectedValue + "' order by Date1");
                    }
                    string balance = "0.00";
                    balance = Convert.ToString(opbal);
                    Double debit = 0, credit = 0;
                    for (int i = 0; i < SPdt.Rows.Count; i++)
                    {
                        if (SPdt.Rows[i]["TranType"].ToString() == "Sale")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("Sale");
                            //li.SubItems.Add("Sales");
                            //li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                            //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //li.SubItems.Add("");
                            //mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            //  li.SubItems.Add(balance);
                        }
                        else if (SPdt.Rows[i]["TranType"].ToString() == "Cash Recept")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("Cash Recept");
                            //li.SubItems.Add("Sales");
                            //li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                            //li.SubItems.Add("");
                            //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //mouseclickid.Rows.Add("Cash Recept", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            //  li.SubItems.Add(balance);
                        }
                        else if (SPdt.Rows[i]["TranType"].ToString() == "Rect")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("Rect");
                            //li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                            //li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                            //li.SubItems.Add("");
                            //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //mouseclickid.Rows.Add("Rect", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            //  li.SubItems.Add(balance);
                        }
                        else if (SPdt.Rows[i]["TranType"].ToString() == "SaleReturn")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("Sale Return");
                            //li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                            //li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());
                            //li.SubItems.Add("");
                            //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //mouseclickid.Rows.Add("Sale Return", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            //  li.SubItems.Add(balance);
                        }
                        else if (SPdt.Rows[i]["TranType"].ToString() == "Purchase")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("Purchase");
                            //li.SubItems.Add("Purchases");
                            //li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                            //li.SubItems.Add("");
                            //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            //   li.SubItems.Add(balance);
                        }

                        else if (SPdt.Rows[i]["TranType"].ToString() == "PurchaseReturn")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("Purchase Return");
                            //li.SubItems.Add(SPdt.Rows[i]["AccountName"].ToString());
                            //li.SubItems.Add("Return Bill No: " + SPdt.Rows[i]["VoucherID"].ToString());

                            //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //li.SubItems.Add("");
                            //debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();

                                opbal = Convert.ToDouble(str[0]);
                            }
                            //if (DC == "C")
                            //{
                            //    DC = "D";
                            //}
                            //else
                            //{
                            //    DC = "C";
                            //}
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            //  li.SubItems.Add(balance);
                        }
                        else if (SPdt.Rows[i]["TranType"].ToString() == "Pmnt")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("Pmnt");
                            //li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                            //li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["ShortNarration"].ToString());
                            //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //li.SubItems.Add("");
                            //mouseclickid.Rows.Add("Pmnt", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            // li.SubItems.Add(balance);
                        }
                        else if (SPdt.Rows[i]["TranType"].ToString() == "Cash Invoice")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("Cash Invoice");
                            //li.SubItems.Add("Purchases");
                            //li.SubItems.Add("Bill No. " + SPdt.Rows[i]["VoucherID"].ToString());
                            //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //li.SubItems.Add("");
                            //mouseclickid.Rows.Add("Cash Invoice", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            // li.SubItems.Add(balance);
                        }
                        else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque Issued")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("Cheque Issued");
                            //li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                            //li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["OT5"].ToString() + "; " + SPdt.Rows[i]["OT6"].ToString());
                            //string CD = SPdt.Rows[i]["dc"].ToString();
                            //if (CD == "D")
                            //{
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //    li.SubItems.Add("");
                            //}
                            //else
                            //{
                            //    li.SubItems.Add("");
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                            //}

                            //mouseclickid.Rows.Add("Cheque Issued", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            // li.SubItems.Add(balance);
                            // li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        }
                        else if (SPdt.Rows[i]["TranType"].ToString() == "Draft Issued")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("Draft Issued");
                            //li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                            //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            //string CD = SPdt.Rows[i]["dc"].ToString();
                            //if (CD == "D")
                            //{
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString()), 2).ToString("N2"));
                            //    debit += Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString());
                            //    li.SubItems.Add("");
                            //}
                            //else
                            //{
                            //    li.SubItems.Add("");
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString()), 2).ToString("N2"));
                            //    credit += Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString());


                            //}

                            //mouseclickid.Rows.Add("Draft Issued", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["OT3"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            //  li.SubItems.Add(balance);
                            // li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        }
                        else if (SPdt.Rows[i]["TranType"].ToString() == "Cheque/Draft/Rtgs Received")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("Cheque/Draft/Rtgs Received");
                            //li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                            //li.SubItems.Add("By Rcpt. No.: " + SPdt.Rows[i]["VoucherID"].ToString() + "; " + SPdt.Rows[i]["OT5"].ToString() + "; " + SPdt.Rows[i]["OT6"].ToString());
                            //string CD = SPdt.Rows[i]["dc"].ToString();
                            //if (CD == "D")
                            //{
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //    li.SubItems.Add("");
                            //}
                            //else
                            //{
                            //    li.SubItems.Add("");
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                            //}

                            //mouseclickid.Rows.Add("Cheque/Draft/Rtgs Received", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            //  li.SubItems.Add(balance);
                            //  li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        }
                        else if (SPdt.Rows[i]["TranType"].ToString() == "Deposit Cash Into Bank")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("Deposit Cash Into Bank");
                            //li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                            //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            //string CD = SPdt.Rows[i]["dc"].ToString();
                            //if (CD == "D")
                            //{
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //    li.SubItems.Add("");
                            //}
                            //else
                            //{
                            //    li.SubItems.Add("");
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                            //}

                            //mouseclickid.Rows.Add("Deposit Cash Into Bank", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            //   li.SubItems.Add(balance);
                            //  li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        }
                        else if (SPdt.Rows[i]["TranType"].ToString() == "Withdraw Cash from Bank")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("Withdraw Cash from Bank");
                            //li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                            //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            //string CD = SPdt.Rows[i]["dc"].ToString();
                            //if (CD == "D")
                            //{
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //    li.SubItems.Add("");
                            //}
                            //else
                            //{
                            //    li.SubItems.Add("");
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                            //}

                            //mouseclickid.Rows.Add("Withdraw Cash from Bank", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            //    li.SubItems.Add(balance);
                            //   li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        }
                        else if (SPdt.Rows[i]["TranType"].ToString() == "Bank Expenses")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("Bank Expenses");
                            //li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                            //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            //string CD = SPdt.Rows[i]["dc"].ToString();
                            //if (CD == "D")
                            //{
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //    li.SubItems.Add("");
                            //}
                            //else
                            //{
                            //    li.SubItems.Add("");
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                            //}

                            //mouseclickid.Rows.Add("Bank Expenses", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            //  li.SubItems.Add(balance);
                            // li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        }
                        else if (SPdt.Rows[i]["TranType"].ToString() == "Online Transfer")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("Online Transfer");
                            //li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                            //li.SubItems.Add(SPdt.Rows[i]["OT6"].ToString());
                            //string CD = SPdt.Rows[i]["dc"].ToString();
                            //if (CD == "D")
                            //{
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //    li.SubItems.Add("");
                            //}
                            //else
                            //{
                            //    li.SubItems.Add("");
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                            //}

                            //mouseclickid.Rows.Add("Online Transfer", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            //    li.SubItems.Add(balance);
                            //   li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        }
                        else if (SPdt.Rows[i]["TranType"].ToString() == "DEBIT NOTE")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("DEBIT NOTE");
                            //li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                            //li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                            //string CD = SPdt.Rows[i]["dc"].ToString();
                            //if (CD == "D")
                            //{
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //    li.SubItems.Add("");
                            //}
                            //else
                            //{
                            //    li.SubItems.Add("");
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                            //}

                            //mouseclickid.Rows.Add("DEBIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            //  li.SubItems.Add(balance);
                            //  li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        }
                        else if (SPdt.Rows[i]["TranType"].ToString() == "CREDIT NOTE")
                        {
                            //ListViewItem li;
                            //li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date1"].ToString()).ToString(Master.dateformate));
                            //li.SubItems.Add("CREDIT NOTE");
                            //li.SubItems.Add(SPdt.Rows[i]["OT1"].ToString());
                            //li.SubItems.Add(SPdt.Rows[i]["ShortNarration"].ToString());
                            //string CD = SPdt.Rows[i]["dc"].ToString();
                            //if (CD == "D")
                            //{
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    debit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());
                            //    li.SubItems.Add("");
                            //}
                            //else
                            //{
                            //    li.SubItems.Add("");
                            //    li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                            //    credit += Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString());


                            //}

                            //mouseclickid.Rows.Add("CREDIT NOTE", SPdt.Rows[i]["VoucherID"].ToString());
                            if (i != 0)
                            {
                                string[] str = balance.Split(' ');
                                char temp = str[1][0];
                                DC = temp.ToString();
                                opbal = Convert.ToDouble(str[0]);
                            }
                            balance = getprviousbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), DC, SPdt.Rows[i]["dc"].ToString(), i);
                            //  li.SubItems.Add(balance);
                            //  li.SubItems.Add(SPdt.Rows[i]["VoucherID"].ToString());
                        }

                    }
                    #endregion

                    // txttotdebit.Text = debit.ToString("N2");
                    //   txttotcredit.Text = credit.ToString("N2");
                    if (balance == "0")
                    {
                        privousbalance = balance;
                    }
                    else
                    {
                        String withoutLast11 = balance.Substring(0, (balance.Length - 3));
                        privousbalance = withoutLast11;
                    }
                }
            }
            catch
            {
                privousbalance = "0";
            }
        }
        public void BtnPayment_Click(object sender, EventArgs e)
        {
            BtnPayment.Enabled = false;
            if (BtnPayment.Text != "Update")
            {
                if (strfinalarray[0] == "S" || strfinalarray[0] == "SR")
                {
                    string isexit = conn.ExecuteScalar("select billno from BillMaster where isactive=1 and BillType='" + strfinalarray[0] + "' and billno='" + TxtBillNo.Text + "'");
                    if (!string.IsNullOrEmpty(isexit))
                    {
                        MessageBox.Show("Bill No. already Avalable Add another");
                        TxtBillNo.Focus();
                        BtnPayment.Enabled = true;
                        return;
                    }
                }
                else
                {
                    string isexit = conn.ExecuteScalar("select billno from BillMaster where isactive=1 and BillType='" + strfinalarray[0] + "' and billno='" + TxtBillNo.Text + "' and ClientID='" + cmbcustname.SelectedValue + "'");
                    if (!string.IsNullOrEmpty(isexit))
                    {
                        MessageBox.Show("Bill No. already Avalable Add another");
                        TxtBillNo.Focus();
                        BtnPayment.Enabled = true;
                        return;
                    }
                }
            }
            databind();
            if (BtnPayment.Text != "Update")
            {
                DataSet ds = ods.getdata("Select * from tblreg");
                string reg = Convert.ToString(ds.Tables[0].Rows[0]["d16"].ToString());
                Decrypstatus(reg);
                if (strfinalarray[0] == "S")
                {
                    if (statusreg == "Edu")
                    {
                        string sale = conn.ExecuteScalar("select count(id) from BillMaster where isactive=1 and BillType='S'");
                        if (sale == "5")
                        {
                            MessageBox.Show("You Are Not Authorized to Add More Then 5 Bill");
                            return;
                        }
                    }
                }
                else if (strfinalarray[0] == "SR")
                {
                    if (statusreg == "Edu")
                    {
                        string sale = conn.ExecuteScalar("select count(id) from BillMaster where isactive=1 and BillType='SR'");
                        if (sale == "5")
                        {
                            MessageBox.Show("You Are Not Authorized to Add More Then 5 Bill");
                            return;
                        }
                    }
                }
                else if (strfinalarray[0] == "P")
                {
                    if (statusreg == "Edu")
                    {
                        string sale = conn.ExecuteScalar("select count(id) from BillMaster where isactive=1 and BillType='P'");
                        if (sale == "5")
                        {
                            MessageBox.Show("You Are Not Authorized to Add More Then 5 Bill");
                            return;
                        }
                    }
                }
                else if (strfinalarray[0] == "PR")
                {
                    if (statusreg == "Edu")
                    {
                        string sale = conn.ExecuteScalar("select count(id) from BillMaster where isactive=1 and BillType='PR'");
                        if (sale == "5")
                        {
                            MessageBox.Show("You Are Not Authorized to Add More Then 5 Bill");
                            return;
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(TxtBillNo.Text))
            {
                btnsubmit();
            }
            else
            {
                MessageBox.Show("Enter Bill No");
                TxtBillNo.Focus();
            }
            BtnPayment.Enabled = true;
            this.ActiveControl = TxtRundate;
        }
        string oldbillno = "";
        int flag = 0;
        string newrefno = "";
        public void submindata()
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtBillNo.Text))
                {
                    if (options.Rows[0]["salevoucherno"].ToString() == "Manual")
                    {
                        if (strfinalarray[0] == "S")
                        {
                            string manual = TxtBillNo.Text;
                            var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-", "/", "!", "#", "$", "%", "&", "(", ")", "*", "+", ",", ".", ";", ":", "<", "=", ">", "?", "@", "[", "]", "~", "^", "_", "{", "}", "|" };
                            foreach (var c in charsToRemove)
                            {
                                manual = manual.Replace(c, string.Empty);
                            }
                            lblbill_no.Text = manual;
                        }
                    }
                    if (options.Rows[0]["salervoucherno"].ToString() == "Manual")
                    {
                        if (strfinalarray[0] == "SR")
                        {
                            string manual = TxtBillNo.Text;
                            var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-", "/", "!", "#", "$", "%", "&", "(", ")", "*", "+", ",", ".", ";", ":", "<", "=", ">", "?", "@", "[", "]", "~", "^", "_", "{", "}", "|" };
                            foreach (var c in charsToRemove)
                            {
                                manual = manual.Replace(c, string.Empty);
                            }
                            lblbill_no.Text = manual;
                        }
                    }
                    if (options.Rows[0]["purchasevoucherno"].ToString() == "Manual")
                    {
                        if (strfinalarray[0] == "P")
                        {
                            string manual = TxtBillNo.Text;
                            var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-", "/", "!", "#", "$", "%", "&", "(", ")", "*", "+", ",", ".", ";", ":", "<", "=", ">", "?", "@", "[", "]", "~", "^", "_", "{", "}", "|" };
                            foreach (var c in charsToRemove)
                            {
                                manual = manual.Replace(c, string.Empty);
                            }

                            lblbill_no.Text = manual;
                        }
                    }
                    if (options.Rows[0]["purchaservoucherno"].ToString() == "Manual")
                    {
                        if (strfinalarray[0] == "PR")
                        {
                            string manual = TxtBillNo.Text;
                            var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-", "/", "!", "#", "$", "%", "&", "(", ")", "*", "+", ",", ".", ";", ":", "<", "=", ">", "?", "@", "[", "]", "~", "^", "_", "{", "}", "|" };
                            foreach (var c in charsToRemove)
                            {
                                manual = manual.Replace(c, string.Empty);
                            }
                            lblbill_no.Text = manual;
                        }
                    }
                    this.Enabled = false;
                    if (BtnPayment.Text == "Update")
                    {
                        if (txtheader.Text == "OUT WARD")
                        {
                            if (userrights.Rows.Count > 0)
                            {
                                if (userrights.Rows[0]["u"].ToString() == "False")
                                {
                                    MessageBox.Show("You don't have Permission To Update");
                                    return;
                                }
                            }
                        }
                        else if (txtheader.Text == "SALE RETURN")
                        {
                            if (userrights.Rows.Count > 0)
                            {
                                if (userrights.Rows[13]["u"].ToString() == "False")
                                {
                                    MessageBox.Show("You don't have Permission To Update");
                                    return;
                                }
                            }
                        }
                        else if (txtheader.Text == "IN WARD")
                        {
                            if (userrights.Rows.Count > 0)
                            {
                                if (userrights.Rows[3]["u"].ToString() == "False")
                                {
                                    MessageBox.Show("You don't have Permission To Update");
                                    return;
                                }
                            }
                        }
                        else if (txtheader.Text == "PURCHASE RETURN")
                        {
                            if (userrights.Rows.Count > 0)
                            {
                                if (userrights.Rows[16]["u"].ToString() == "False")
                                {
                                    MessageBox.Show("You don't have Permission To Update");
                                    return;
                                }
                            }
                        }
                        string clientid = conn.ExecuteScalar("select ClientID from billproductmaster where billno=(select billno from billmaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "') and billtype='" + strfinalarray[0] + "' and isactive=1");
                        if (string.IsNullOrEmpty(clientid))
                        {
                            SqlCommand cmd2 = new SqlCommand("Update billproductmaster set ClientID='" + clientidupdate + "',Userid='"+ master.CurrentUserid +"' where isactive=1 and BillType='" + strfinalarray[0] + "'and billno=(select billno from billmaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "')", con);
                            cmd2.ExecuteNonQuery();
                        }
                        
                        string charges = conn.ExecuteScalar("select ClientID from Billchargesmaster where billno=(select billno from billmaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "') and billtype='" + strfinalarray[0] + "' and isactive=1");
                        if (string.IsNullOrEmpty(charges))
                        {
                            conn.execute("update Billchargesmaster set ClientID='" + clientidupdate + "',Userid='" + master.CurrentUserid + "' where  isactive=1 and billno=(select billno from billmaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "') and billtype='" + strfinalarray[0] + "'");
                        }
                        SqlCommand cmd21 = new SqlCommand("Update billproductmaster set isactive='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='" + master.CurrentUserid + "' where ClientID='" + clientidupdate + "' and isactive=1 and BillType='" + strfinalarray[0] + "'and billno=(select billno from billmaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "')", con);
                        cmd21.ExecuteNonQuery();
                        conn.execute("update Billchargesmaster set isactive='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='" + master.CurrentUserid + "' where ClientID='" + clientidupdate + "' and isactive=1 and billno=(select billno from billmaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "') and billtype='" + strfinalarray[0] + "'");
                        conn.execute("Update Serials set isactive='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='" + master.CurrentUserid + "' where PartyID='" + clientidupdate + "' and isactive=1 and VoucherID=(select billno from billmaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "')");
                        for (int i = 0; i < LVFO.Items.Count; i++)
                        {
                            Guid guid;
                            guid = Guid.NewGuid();
                            string productid = conn.ExecuteScalar("select Productid from productmaster where isactive=1 and ProductID='" + LVFO.Items[i].SubItems[24].Text + "'");
                            // conn.execute("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid])VALUES('"+lblbill_no.Text+"','" + Convert.ToDateTime(TxtRundate.Text).ToString("dd/MMM/yyyy") + "','" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[3].Text.Replace(",", "") + "','','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[12].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[14].Text.Replace(",", "") + "','1','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','S','" + TxtBillNo.Text + "','" + LVFO.Items[i].SubItems[13].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[11].Text.Replace(",", "") + "','"+productid+"')");
                            conn.execute("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime],[ClientID],[fkid],[boxsrno],[Userid])VALUES('" + lblbill_no.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[3].Text.Replace(",", "") + "','','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[12].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[14].Text.Replace(",", "") + "','1','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','" + strfinalarray[0] + "','" + TxtBillNo.Text + "','" + LVFO.Items[i].SubItems[13].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[11].Text.Replace(",", "") + "','" + productid + "','" + LVFO.Items[i].SubItems[15].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[16].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[17].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[18].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[19].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[20].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[21].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[22].Text + "','" + refnoupdate + "','" + LVFO.Items[i].SubItems[23].Text.Replace(",", "") + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + cmbcustname.SelectedValue + "','" + lblid.Text + "','" + LVFO.Items[i].SubItems[25].Text.Replace(",", "") + "','"+master.CurrentUserid+"')");
                            //   if (temptable.Rows.Count == 0)
                            //  {
                            string serial = LVFO.Items[i].SubItems[22].Text;
                            //serial = Regex.Replace(LVFO.Items[i].SubItems[22].Text, @"\s+", string.Empty);
                            serial = LVFO.Items[i].SubItems[22].Text.Trim();
                            if (!string.IsNullOrEmpty(serial))
                            {
                                string[] values = serial.Split(',');
                                //  temptable = new DataTable();
                                // temptable.Columns.Add("Itemname", typeof(string));
                                // temptable.Columns.Add("SERIAL", typeof(string));
                                for (int j = 0; j < values.Length; j++)
                                {
                                    bool exists = false;
                                    for (int b = 0; b < temptable.Rows.Count; b++)
                                    {
                                        //   string srno = item.SubItems[22].Text;
                                        //string srno = Regex.Replace(temptable.Rows[b]["SERIAL"].ToString(), @"\s+", string.Empty);
                                        string srno = temptable.Rows[b]["SERIAL"].ToString().Trim();
                                        string itemname = temptable.Rows[b]["Itemname"].ToString();
                                        if (values[j] == srno && LVFO.Items[i].SubItems[0].Text == itemname)
                                        {
                                            exists = true;
                                        }
                                    }
                                    if (exists != true)
                                    {
                                        temptable.Rows.Add(LVFO.Items[i].SubItems[0].Text, values[j]);
                                    }
                                }
                            }
                            //    }

                        }
                        for (int o = 0; o < temptable.Rows.Count; o++)
                        {
                            Guid guid;
                            guid = Guid.NewGuid();
                            string ItemID = conn.ExecuteScalar("select Productid from productmaster where isactive=1 and Product_Name='" + temptable.Rows[o]["Itemname"].ToString() + "'");
                            conn.execute("INSERT INTO [dbo].[Serials]([Date],[VoucherID],[TransactionID],[SerialNo],[TransactionType],[VchNo],[PurchaseTypeID],[PurchaseTypeName],[SaleTypeID],[SaleTypeName],[PartyID],[PartyName],[ItemCompanyID],[SyncID],[SyncDatetime],[isactive],[ItemName],[ItemID],[Userid])VALUES('" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + TxtBillNo.Text + "','" + strfinalarray[0] + "','" + temptable.Rows[o]["SERIAL"].ToString() + "','" + strfinalarray[2] + "','" + txtpono.Text + "','" + cmbsaletype.SelectedValue + "','" + cmbsaletype.Text + "','" + cmbsaletype.SelectedValue + "','" + cmbsaletype.Text + "','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Master.companyId + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1,'" + temptable.Rows[o]["Itemname"].ToString() + "','" + ItemID + "','"+master.CurrentUserid+"')");
                        }
                        for (int i = 0; i < LVCHARGES.Items.Count; i++)
                        {
                            Guid guid;
                            guid = Guid.NewGuid();
                            conn.execute("INSERT INTO [Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billtype],[billsundryid],[SyncID],[SyncDatetime],[ClientID],[fkid],[isactive],[Userid])VALUES('" + TxtBillNo.Text + "','" + LVCHARGES.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[3].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[11].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[12].Text.Replace(",", "") + "','" + strfinalarray[0] + "','" + LVCHARGES.Items[i].SubItems[13].Text.Replace(",", "") + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + cmbcustname.SelectedValue + "','" + lblid.Text + "',1,'" + master.CurrentUserid + "')");
                        }
                        conn.execute("UPDATE [dbo].[BillMaster]SET [cusname]='" + txtcustomername.Text + "',[cusadd]='" + txtaddress.Text + "',[cuscity]='" + txtcity.Text + "',[cusphone]='" + txtphone.Text + "',[cusmobile]='" + txtmobile.Text + "',[cuspancard]='" + txtpanno.Text + "',[cusadhar]='" + txtadharno.Text + "', [Bill_No] = '" + lblbill_no.Text + "',[Bill_Date] = '" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[Terms] = '" + cmbterms.Text + "',[ClientID] = '" + cmbcustname.SelectedValue + "',[PO_No] = '" + txtpono.Text.Replace(",", "") + "',[SaleType] = '" + cmbsaletype.SelectedValue + "',[count] = '" + lbltotcount.Text.Replace(",", "") + "',[totalqty] = '" + lbltotpqty.Text.Replace(",", "") + "',[totalbasic] = '" + lblbasictot.Text.Replace(",", "") + "',[totaltax] = '" + txttottax.Text.Replace(",", "") + "',[totalnet] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[isactive] = '1',[apprweight] = '" + txtweight.Text.Replace(",", "") + "',[dispatchdetails] = '" + txttransport.Text + "',[remarks] = '" + txtremarks.Text + "',[BillType] = '" + strfinalarray[0] + "',[billno] = '" + TxtBillNo.Text + "',[totaladdtax] = '" + txttotaddvat.Text + "',[roudoff] = '" + txtroundoff.Text + "',[Duedate] = '" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "',[totalaqty] = '" + txttotaqty.Text.Replace(",", "") + "',[totalfree] = '" + txttotfree.Text.Replace(",", "") + "',[totaldiscount] ='" + txttotdiscount.Text.Replace(",", "") + "',[totaladddiscount] = '" + txttotadis.Text.Replace(",", "") + "',[totalamount] = '" + txtamt.Text.Replace(",", "") + "',[totalservicejob] = '" + txttotservice.Text.Replace(",", "") + "',[totalcharges] = '" + txttotalcharges.Text.Replace(",", "") + "',[Delieveryat] = '" + txtdelieveryat.Text + "',[fraight] = '" + txtfraight.Text + "',[vehicleno] = '" + txtvehicleno.Text + "',[grrrno] = '" + txtgrrrno.Text + "',[noofskids] = '" + txtskids.Text + "',[sgstamt]='" + lblsgsttotsl.Text + "',[cgatamt]='" + lblcgattotal.Text + "',[igstamt]='" + lbligsttotal.Text + "',[refno]='" + refnoupdate + "',[totalcess]='" + txttotalcess.Text.Replace(",", "") + "',[agentID]='" + cmbagentname.SelectedValue + "',[originalbillno]='" + txtorgbillno.Text + "',[originalbilldate]='" + txtorgbilldate.Text + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',[OT1]='" + txtf1.Text + "',[OT2]='" + txtf2.Text + "',[OT3]='" + txtf3.Text + "',[OT4]='" + txtf4.Text + "',[OT5]='" + txtf5.Text + "',[OT6]='" + txtf6.Text + "',[OT7]='" + txtf7.Text + "',[OT8]='" + txtf8.Text + "',[OT9]='" + txtf9.Text + "',[OT10]='" + txtf10.Text + "',[OT11]='" + txtf11.Text + "',[OT12]='" + txtf12.Text + "',[OT13]='" + txtf13.Text + "',[OT14]='" + txtf14.Text + "',[OT15]='" + txtf15.Text + "',Userid='"+master.CurrentUserid+"' where isactive=1 and id='" + lblid.Text + "' and [billtype]='" + strfinalarray[0] + "'");

                        string vno = conn.ExecuteScalar("select voucherid from ledger where isactive=1 and AccountID='" + clientidupdate + "' and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + strfinalarray[2] + "'");
                        if (vno != "0")
                        {
                            conn.execute("UPDATE [dbo].[Ledger] SET [OT1]='" + cmbterms.Text + "', [voucherid]='" + TxtBillNo.Text + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[TranType] = '" + strfinalarray[2] + "',[AccountID] = '" + cmbcustname.SelectedValue + "',[AccountName]='" + cmbcustname.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = '" + strfinalarray[1] + "',[OD1]='" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+ master.CurrentUserid +"' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + strfinalarray[2] + "' and AccountID='" + clientidupdate + "'");
                        }
                        else
                        {
                            Guid guid;
                            guid = Guid.NewGuid();
                            conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime],[Userid]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + strfinalarray[2] + "','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + strfinalarray[1] + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','"+master.CurrentUserid+"')");
                        }
                        if (strfinalarray[0] == "S")
                        {
                            if (cmbterms.Text == "Credit")
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [isactive]='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "Cash Recept" + "' and AccountID='" + clientidupdate + "'");
                                conn.execute("Update billmaster set OrderStatus='" + "Pending" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and id='" + lblid.Text + "'");
                            }
                            else
                            {
                                string bil = conn.ExecuteScalar("select voucherID from Ledger where isactive=1 and AccountID='" + clientidupdate + "' and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "Cash Recept" + "' ");
                                if (string.IsNullOrEmpty(bil))
                                {
                                    conn.execute("Update billmaster set OrderStatus='" + "Clear" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and id='" + lblid.Text + "'");
                                    conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[ShortNarration],[OT1],[OD1],[SyncDatetime],[isactive],[Userid]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','Cash Recept','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','C','" + "Cash Recept" + "','" + "Cash" + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1,'"+master.CurrentUserid+"')");
                                }
                                else
                                {
                                    conn.execute("Update billmaster set OrderStatus='" + "Clear" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and id='" + lblid.Text + "'");
                                    conn.execute("UPDATE [dbo].[Ledger] SET [OT1]='" + "Cash" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "', [voucherid]='" + TxtBillNo.Text + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[TranType] = '" + "Cash Recept" + "',[AccountID] = '" + cmbcustname.SelectedValue + "',[AccountName]='" + cmbcustname.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = '" + "C" + "',[OD1]='" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "',Userid='"+master.CurrentUserid+"' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "Cash Recept" + "' and AccountID='" + clientidupdate + "'");
                                }
                            }
                            //   conn.execute("UPDATE [dbo].[Ledger] SET [Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[AccountID] = '" + cmbcustname.SelectedValue + "',[AccountName]='" + cmbcustname.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = 'C',[OT1]='" + cmbterms.Text + "',[ShortNarration]='" + "Cash Payment" + "' where [VoucherID]= '" + TxtBillNo.Text + "' and [TranType] = 'Rect'");
                        }
                        else if (strfinalarray[0] == "P")
                        {
                            if (cmbterms.Text == "Credit")
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [isactive]='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "Cash Invoice" + "' and AccountID='" + clientidupdate + "'");
                                conn.execute("Update billmaster set OrderStatus='" + "Pending" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and id='" + lblid.Text + "'");
                            }
                            else
                            {
                                string bil = conn.ExecuteScalar("select voucherID from Ledger where isactive=1 and AccountID='" + clientidupdate + "' and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "Cash Invoice" + "' ");
                                if (string.IsNullOrEmpty(bil))
                                {
                                    conn.execute("Update billmaster set OrderStatus='" + "Clear" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and id='" + lblid.Text + "'");
                                    conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[ShortNarration],[OT1],[OD1],[SyncDatetime],[isactive],[Userid]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','Cash Invoice','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','D','" + "Cash Payment" + "','" + "Cash" + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1,'"+master.CurrentUserid+"')");
                                }
                                else
                                {
                                    conn.execute("Update billmaster set OrderStatus='" + "Clear" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and billno='" + lblid.Text + "'");
                                    conn.execute("UPDATE [dbo].[Ledger] SET [OT1]='" + "Cash" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "', [voucherid]='" + TxtBillNo.Text + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[TranType] = '" + "Cash Invoice" + "',[AccountID] = '" + cmbcustname.SelectedValue + "',[AccountName]='" + cmbcustname.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = '" + "D" + "',[OD1]='" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "',Userid='"+master.CurrentUserid+"' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "Cash Invoice" + "' and AccountID='" + clientidupdate + "'");
                                }
                            }
                            //conn.execute("UPDATE [dbo].[Ledger] SET [Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[AccountID] = '" + cmbcustname.SelectedValue + "',[AccountName]='" + cmbcustname.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = 'D',[OT1]='" + cmbterms.Text + "',[ShortNarration]='" + "Cash Payment" + "' where [VoucherID]= '" + TxtBillNo.Text + "' and [TranType] = 'Pmnt'");
                        }

                        //for (int k = 0; k < dtsaleorder.Rows.Count; k++)
                        //{
                        bool ispending = false;

                        for (int i = 0; i < dtsaleorder.Rows.Count; i++)
                        {
                            for (int j = 0; j < LVFO.Items.Count; j++)
                            {

                                if (LVFO.Items[j].SubItems[0].Text.Replace(",", "") == dtsaleorder.Rows[i]["productname"].ToString().Replace(",", ""))
                                {
                                    DataTable isbill = conn.getdataset("select sum(pqty) from BillProductMaster where  productname= '" + LVFO.Items[j].SubItems[0].Text.Replace(",", "") + "' and isactive=1");
                                    if (isbill.Rows.Count > 0)
                                    {

                                        if (Convert.ToDouble(isbill.Rows[0][0].ToString()) >= Convert.ToDouble(dtsaleorder.Rows[i]["qty"].ToString().Replace(",", "")))
                                        {
                                            if (ispending == false)
                                            {
                                                conn.execute("Update SaleOrderMaster SET OrderStatus='Clear',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and ClientID='" + cmbcustname.SelectedValue + "' and billno='" + dtsaleorder.Rows[i]["billno"].ToString() + "'");
                                            }
                                        }
                                        else
                                        {
                                            conn.execute("Update SaleOrderMaster SET OrderStatus='Pending',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and ClientID='" + cmbcustname.SelectedValue + "' and billno='" + dtsaleorder.Rows[i]["billno"].ToString() + "'");
                                            ispending = true;
                                        }
                                    }
                                }
                                else
                                {

                                    bool isavail = false;
                                    for (int b = 0; b < dtsaleorder.Rows.Count; b++)
                                    {
                                        if (dtsaleorder.Rows[b]["productname"].ToString().Replace(",", "") == dtsaleorder.Rows[i]["productname"].ToString().Replace(",", ""))
                                        {
                                            for (int a = 0; a < LVFO.Items.Count; a++)
                                            {
                                                if (dtsaleorder.Rows[b]["productname"].ToString().Replace(",", "") == LVFO.Items[a].SubItems[0].Text.Replace(",", ""))
                                                {
                                                    isavail = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (isavail == true)
                                            break;

                                    }


                                    if (isavail == false)
                                    {
                                        conn.execute("Update SaleOrderMaster SET OrderStatus='Pending',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and ClientID='" + cmbcustname.SelectedValue + "' and billno='" + dtsaleorder.Rows[i]["billno"].ToString() + "'");
                                        ispending = true;
                                    }
                                }
                            }
                        }
                        //}
                        #region
                        //for (int i = 0; i < dtsaleorder.Rows.Count; i++)
                        //{
                        //    for (int j = 0; j < LVFO.Items.Count; j++)
                        //    {
                        //        if (flag == 0)
                        //        {
                        //            if (LVFO.Items[j].SubItems[0].Text.Replace(",", "") == dtsaleorder.Rows[i]["productname"].ToString().Replace(",", ""))
                        //            {
                        //                DataTable isbill = conn.getdataset("select sum(pqty) from BillProductMaster where  productname like '%" + LVFO.Items[j].SubItems[0].Text.Replace(",", "") + "%' and isactive=1");

                        //                if (Convert.ToDouble(isbill.Rows[0][0].ToString()) >= Convert.ToDouble(dtsaleorder.Rows[i]["qty"].ToString().Replace(",", "")))
                        //                {
                        //                    conn.execute("Update SaleOrderMaster SET OrderStatus='Clear' where ClientID='" + cmbcustname.SelectedValue + "' and billno='" + dtsaleorder.Rows[i]["refno"].ToString() + "'");

                        //                }
                        //                else
                        //                {
                        //                    conn.execute("Update SaleOrderMaster SET OrderStatus='Pending' where ClientID='" + cmbcustname.SelectedValue + "' and billno='" + dtsaleorder.Rows[i]["refno"].ToString() + "'");
                        //                    flag = 1;
                        //                }

                        //            }
                        //            else
                        //            {
                        //                conn.execute("Update SaleOrderMaster SET OrderStatus='Pending' where ClientID='" + cmbcustname.SelectedValue + "' and billno='" + dtsaleorder.Rows[i]["refno"].ToString() + "'");
                        //                flag = 1;
                        //            }
                        //        }
                        //    }
                        //}
                        #endregion
                        if (Rewritrbooksofaccount.rewritedata != "True")
                        {
                            MessageBox.Show("Data Updated Successfully.");
                        }
                        print();
                        clearitem();
                        clearall();
                        clearfooter();
                        LVFO.Items.Clear();
                        LVCHARGES.Items.Clear();


                        BtnPayment.Text = "&Submit";
                    }
                    else
                    {
                        // getsr();
                        //  bool[] ispending = new bool[updateitem.Length];
                        bindbillno();
                        Guid guid1;
                        guid1 = Guid.NewGuid();
                        string s = "INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess],[agentID],[originalbillno],[originalbilldate],[SyncID],[SyncDatetime],[cusname],[cusadd],[cuscity],[cusphone],[cusmobile],[cuspancard],[cusadhar],[OT1],[OT2],[OT3],[OT4],[OT5],[OT6],[OT7],[OT8],[OT9],[OT10],[OT11],[OT12],[OT13],[OT14],[OT15],[Userid])VALUES('" + lblbill_no.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbterms.Text + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text.Replace(",", "") + "','" + cmbsaletype.SelectedValue + "'," + lbltotcount.Text.Replace(",", "") + "," + lbltotpqty.Text.Replace(",", "") + "," + lblbasictot.Text.Replace(",", "") + "," + txttottax.Text.Replace(",", "") + "," + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",'1','" + txtweight.Text.Replace(",", "") + "','" + txttransport.Text + "','" + txtremarks.Text + "','" + strfinalarray[0] + "','" + TxtBillNo.Text + "','" + txttotaddvat.Text + "','" + txtroundoff.Text + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "'," + txttotaqty.Text.Replace(",", "") + "," + txttotfree.Text.Replace(",", "") + "," + txttotdiscount.Text.Replace(",", "") + "," + txttotadis.Text.Replace(",", "") + "," + txtamt.Text.Replace(",", "") + "," + txttotservice.Text.Replace(",", "") + "," + txttotalcharges.Text.Replace(",", "") + ",'" + txtdelieveryat.Text + "','" + txtfraight.Text + "','" + txtvehicleno.Text + "','" + txtgrrrno.Text + "','" + txtskids.Text + "','" + lblsgsttotsl.Text + "','" + lblcgattotal.Text + "','" + lbligsttotal.Text + "','" + "Pending" + "','" + refno + "','" + txttotalcess.Text.Replace(",", "") + "','" + cmbagentname.SelectedValue + "','" + txtorgbillno.Text + "','" + txtorgbilldate.Text + "','" + guid1 + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + txtcustomername.Text + "','" + txtaddress.Text + "','" + txtcity.Text + "','" + txtphone.Text + "','" + txtmobile.Text + "','" + txtpanno.Text + "','" + txtadharno.Text + "','" + txtf1.Text + "','" + txtf2.Text + "','" + txtf3.Text + "','" + txtf4.Text + "','" + txtf5.Text + "','" + txtf6.Text + "','" + txtf7.Text + "','" + txtf8.Text + "','" + txtf9.Text + "','" + txtf10.Text + "','" + txtf11.Text + "','" + txtf12.Text + "','" + txtf13.Text + "','" + txtf14.Text + "','" + txtf15.Text + "','"+master.CurrentUserid+"')";
                        // conn.execute("INSERT INTO [dbo].[BillMaster]([Bill_No],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType],[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[BillType],[billno],[totaladdtax],[roudoff],[Duedate],[totalaqty],[totalfree],[totaldiscount],[totaladddiscount],[totalamount],[totalservicejob],[totalcharges],[Delieveryat],[fraight],[vehicleno],[grrrno],[noofskids],[sgstamt],[cgatamt],[igstamt],[OrderStatus],[refno],[totalcess])VALUES('" + lblbill_no.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbterms.Text + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text.Replace(",", "") + "','" + cmbsaletype.SelectedValue + "'," + lbltotcount.Text.Replace(",", "") + "," + lbltotpqty.Text.Replace(",", "") + "," + lblbasictot.Text.Replace(",", "") + "," + txttottax.Text.Replace(",", "") + "," + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",'1','" + txtweight.Text.Replace(",", "") + "','" + txttransport.Text + "','" + txtremarks.Text + "','" + strfinalarray[0] + "','" + TxtBillNo.Text + "','" + txttotaddvat.Text + "','" + txtroundoff.Text + "','" + Convert.ToDateTime(txtduedate.Text).ToString() + "'," + txttotaqty.Text.Replace(",", "") + "," + txttotfree.Text.Replace(",", "") + "," + txttotdiscount.Text.Replace(",", "") + "," + txttotadis.Text.Replace(",", "") + "," + txtamt.Text.Replace(",", "") + "," + txttotservice.Text.Replace(",", "") + "," + txttotalcharges.Text.Replace(",", "") + ",'" + txtdelieveryat.Text + "','" + txtfraight.Text + "','" + txtvehicleno.Text + "','" + txtgrrrno.Text + "','" + txtskids.Text + "','" + lblsgsttotsl.Text + "','" + lblcgattotal.Text + "','" + lbligsttotal.Text + "','" + "Pending" + "','" + refno + "','" + txttotalcess.Text.Replace(",", "") + "')");
                        conn.execute(s);
                        LogGenerator.Info(s);
                        string fkid = conn.ExecuteScalar("select max(id) from BillMaster where isactive=1");
                        if (string.IsNullOrEmpty(fkid))
                        {
                            fkid = "1";
                        }
                        for (int i = 0; i < LVFO.Items.Count; i++)
                        {



                            Guid guid;
                            guid = Guid.NewGuid();
                            string productid = conn.ExecuteScalar("select Productid from productmaster where isactive=1 and ProductID='" + LVFO.Items[i].SubItems[24].Text + "'");
                            conn.execute("INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime],[ClientID],[fkid],[boxsrno],[Userid])VALUES('" + lblbill_no.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[3].Text.Replace(",", "") + "','','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[12].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[14].Text.Replace(",", "") + "','1','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','" + strfinalarray[0] + "','" + TxtBillNo.Text + "','" + LVFO.Items[i].SubItems[13].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[11].Text.Replace(",", "") + "','" + productid + "','" + LVFO.Items[i].SubItems[15].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[16].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[17].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[18].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[19].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[20].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[21].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[22].Text + "','" + refno + "','" + LVFO.Items[i].SubItems[23].Text.Replace(",", "") + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + cmbcustname.SelectedValue + "','" + fkid + "','" + LVFO.Items[i].SubItems[25].Text.Replace(",", "") + "','"+master.CurrentUserid+"')");
                            string z = "INSERT INTO [dbo].[BillProductMaster]([Bill_No],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[Amount],[isactive],[qty],[Billtype],[billno],[addtax],[batch],[free],[discountper],[discountamt],[productid],[sgstper],[sgstamt],[cgstper],[cgstamt],[igstper],[igdtamt],[addtaxper],[serialno],[refno],[cess],[SyncID],[SyncDatetime],[ClientID],[fkid],[boxsrno],[Userid])VALUES('" + lblbill_no.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + LVFO.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[3].Text.Replace(",", "") + "','','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[12].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[14].Text.Replace(",", "") + "','1','" + LVFO.Items[i].SubItems[5].Text.Replace(",", "") + "','" + strfinalarray[0] + "','" + TxtBillNo.Text + "','" + LVFO.Items[i].SubItems[13].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[11].Text.Replace(",", "") + "','" + productid + "','" + LVFO.Items[i].SubItems[15].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[16].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[17].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[18].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[19].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[20].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[21].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[22].Text + "','" + refno + "','" + LVFO.Items[i].SubItems[23].Text.Replace(",", "") + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + cmbcustname.SelectedValue + "','" + fkid + "','" + LVFO.Items[i].SubItems[25].Text.Replace(",", "") + "','"+master.CurrentUserid+"')";
                            LogGenerator.Info(z);
                            //SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[BillProductMaster]([BillNo],[Bill_Run_Date],[Productname],[Packing],[Bags],[MRP],[Pqty],[Aqty],[Rate],[Per],[Total],[Tax],[addtax],[Amount],[isactive],[qty],[BillType],[Bill_no],[batch],[free],[discountper],[discountamt])VALUES('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("dd/MMM/yyyy") + "','" + LVFO.Items[i].SubItems[1].Text + "','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[0].Text + "','','" + LVFO.Items[i].SubItems[4].Text + "','','" + LVFO.Items[i].SubItems[5].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','" + LVFO.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVFO.Items[i].SubItems[8].Text + "','" + LVFO.Items[i].SubItems[9].Text + "','" + LVFO.Items[i].SubItems[10].Text.Replace(",", "") + "',1,'" + LVFO.Items[i].SubItems[3].Text + "','S','0','" + LVFO.Items[i].SubItems[2].Text + "','" + LVFO.Items[i].SubItems[6].Text + "','','')", con);
                            //cmd1.ExecuteNonQuery();
                            //  if (temptable.Rows.Count == 0)
                            //  {
                            string serial = LVFO.Items[i].SubItems[22].Text;
                            //serial = Regex.Replace(LVFO.Items[i].SubItems[22].Text, @"\s+", string.Empty);
                            serial = LVFO.Items[i].SubItems[22].Text.Trim();
                            if (!string.IsNullOrEmpty(serial))
                            {
                                string[] values = serial.Split(',');
                                //temptable = new DataTable();
                                //temptable.Columns.Add("Itemname", typeof(string));
                                //temptable.Columns.Add("SERIAL", typeof(string));
                                for (int j = 0; j < values.Length; j++)
                                {
                                    bool exists = false;
                                    for (int b = 0; b < temptable.Rows.Count; b++)
                                    {
                                        //   string srno = item.SubItems[22].Text;
                                        //string srno = Regex.Replace(temptable.Rows[b]["SERIAL"].ToString(), @"\s+", string.Empty);
                                        string srno = temptable.Rows[b]["SERIAL"].ToString().Trim();
                                        string itemname = temptable.Rows[b]["Itemname"].ToString();
                                        if (values[j] == srno && LVFO.Items[i].SubItems[0].Text == itemname)
                                        {
                                            exists = true;
                                        }
                                    }
                                    if (exists != true)
                                    {
                                        temptable.Rows.Add(LVFO.Items[i].SubItems[0].Text, values[j]);
                                    }
                                    // temptable.Rows.Add(LVFO.Items[i].SubItems[0].Text, values[j]);
                                }
                            }
                            // }

                        }

                        for (int o = 0; o < temptable.Rows.Count; o++)
                        {
                            Guid guid;
                            guid = Guid.NewGuid();
                            string ItemID = conn.ExecuteScalar("select Productid from productmaster where isactive=1 and Product_Name='" + temptable.Rows[o]["Itemname"].ToString() + "'");
                            conn.execute("INSERT INTO [dbo].[Serials]([Date],[VoucherID],[TransactionID],[SerialNo],[TransactionType],[VchNo],[PurchaseTypeID],[PurchaseTypeName],[SaleTypeID],[SaleTypeName],[PartyID],[PartyName],[ItemCompanyID],[SyncID],[SyncDatetime],[isactive],[ItemName],[ItemID],[Userid])VALUES('" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + TxtBillNo.Text + "','" + strfinalarray[0] + "','" + temptable.Rows[o]["SERIAL"].ToString() + "','" + strfinalarray[2] + "','" + txtpono.Text + "','" + cmbsaletype.SelectedValue + "','" + cmbsaletype.Text + "','" + cmbsaletype.SelectedValue + "','" + cmbsaletype.Text + "','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Master.companyId + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1,'" + temptable.Rows[o]["Itemname"].ToString() + "','" + ItemID + "','"+master.CurrentUserid+"')");
                            string y = "INSERT INTO [dbo].[Serials]([Date],[VoucherID],[TransactionID],[SerialNo],[TransactionType],[VchNo],[PurchaseTypeID],[PurchaseTypeName],[SaleTypeID],[SaleTypeName],[PartyID],[PartyName],[ItemCompanyID],[SyncID],[SyncDatetime],[isactive],[ItemName],[ItemID],[Userid])VALUES('" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + TxtBillNo.Text + "','" + strfinalarray[0] + "','" + temptable.Rows[o]["SERIAL"].ToString() + "','" + strfinalarray[2] + "','" + txtpono.Text + "','" + cmbsaletype.SelectedValue + "','" + cmbsaletype.Text + "','" + cmbsaletype.SelectedValue + "','" + cmbsaletype.Text + "','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Master.companyId + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1,'" + temptable.Rows[o]["Itemname"].ToString() + "','" + ItemID + "','"+master.CurrentUserid+"')";
                            LogGenerator.Info(y);
                        }
                        for (int i = 0; i < LVCHARGES.Items.Count; i++)
                        {
                            Guid guid;
                            guid = Guid.NewGuid();
                            conn.execute("INSERT INTO [Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billtype],[billsundryid],[SyncID],[SyncDatetime],[ClientID],[fkid],[isactive],[Userid])VALUES('" + TxtBillNo.Text + "','" + LVCHARGES.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[3].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[11].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[12].Text.Replace(",", "") + "','" + strfinalarray[0] + "','" + LVCHARGES.Items[i].SubItems[13].Text.Replace(",", "") + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + cmbcustname.SelectedValue + "','" + fkid + "',1,'"+master.CurrentUserid+"')");
                            string x = "INSERT INTO [Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billtype],[billsundryid],[SyncID],[SyncDatetime],[ClientID],[fkid],[isactive],[Userid])VALUES('" + TxtBillNo.Text + "','" + LVCHARGES.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[3].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[11].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[12].Text.Replace(",", "") + "','" + strfinalarray[0] + "','" + LVCHARGES.Items[i].SubItems[13].Text.Replace(",", "") + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + cmbcustname.SelectedValue + "','" + fkid + "',1,'"+master.CurrentUserid+"')";
                            LogGenerator.Info(x);
                            //conn.execute("INSERT INTO [Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[billtype],[isactive])VALUES('" + TxtBillNo.Text + "','" + LVCHARGES.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[3].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[11].Text.Replace(",", "") + "','" + strfinalarray[0] + "',1)");
                        }

                        // SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[BillMaster]([BillNo],[Bill_Date],[Terms],[ClientID],[PO_No],[SaleType] ,[count],[totalqty],[totalbasic],[totaltax],[totalnet],[isactive],[apprweight],[dispatchdetails],[remarks],[billtype],[Bill_no],[totaladdtax],[roudoff])VALUES('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("dd/MMM/yyyy") + "','" + cmbterms.Text + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text + "','" + cmbsaletype.SelectedValue + "'," + lbltotcount.Text + "," + lbltotpqty.Text + "," + lblbasictot.Text + "," + txttottax.Text + "," + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + ",1,'" + txtweight.Text + "','" + txttransport.Text + "','" + txtremarks.Text + "','S','0','" + txttotaddvat.Text + "','" + txtroundoff.Text + "')", con);
                        //cmd.ExecuteNonQuery();
                        Guid guid2;
                        guid2 = Guid.NewGuid();
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime],[Userid]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + strfinalarray[2] + "','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + strfinalarray[1] + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "','" + guid2 + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','"+master.CurrentUserid+"')");
                        string h = "INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime],[Userid]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + strfinalarray[2] + "','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + strfinalarray[1] + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "','" + guid2 + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','"+master.CurrentUserid+"')";
                        LogGenerator.Info(h);
                        if (strfinalarray[0] == "S" && cmbterms.Text == "Cash")
                        {
                            Guid guid;
                            guid = Guid.NewGuid();
                            conn.execute("Update billmaster set OrderStatus='" + "Clear" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and billno='" + TxtBillNo.Text + "'");
                            conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[ShortNarration],[OT1],[OD1],[SyncID],[SyncDatetime],[isactive],[Userid]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','Cash Recept','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','C','" + "Cash Recept" + "','" + "Cash" + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1,'"+master.CurrentUserid+"')");
                        }
                        else if (strfinalarray[0] == "P" && cmbterms.Text == "Cash")
                        {
                            Guid guid;
                            guid = Guid.NewGuid();
                            conn.execute("Update billmaster set OrderStatus='" + "Clear" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and billno='" + TxtBillNo.Text + "'");
                            conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[ShortNarration],[OT1],[OD1],[SyncID],[SyncDatetime],[isactive],[Userid]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','Cash Invoice','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','D','" + "Cash Payment" + "','" + "Cash" + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1,'"+master.CurrentUserid+"')");
                        }

                        //if (ispending[i] == false)
                        //{
                        //    
                        //}
                        //DataTable tempdt = new DataTable();
                        //tempdt = dtsaleorder;
                        if (updateitem != null)
                        {
                            for (int k = 0; k < updateitem.Length; k++)
                            {
                                bool ispending = false;
                                DataTable saleordrdt = new DataTable();
                                saleordrdt = dtsaleorder.Clone();
                                for (int i = 0; i < dtsaleorder.Rows.Count; i++)
                                {
                                    if (dtsaleorder.Rows[i]["billno"].ToString() == updateitem[k].ToString())
                                    {
                                        DataRow dr1 = saleordrdt.NewRow();
                                        dr1.ItemArray = dtsaleorder.Rows[i].ItemArray;
                                        saleordrdt.Rows.Add(dr1);
                                    }
                                }

                                for (int i = 0; i < saleordrdt.Rows.Count; i++)
                                {
                                    for (int j = 0; j < LVFO.Items.Count; j++)
                                    {

                                        if (LVFO.Items[j].SubItems[0].Text.Replace(",", "") == saleordrdt.Rows[i]["productname"].ToString().Replace(",", ""))
                                        {
                                            DataTable isbill = conn.getdataset("select sum(pqty) from BillProductMaster where  productname='" + LVFO.Items[j].SubItems[0].Text.Replace(",", "") + "' and isactive=1");
                                            if (isbill.Rows.Count > 0)
                                            {

                                                if (Convert.ToDouble(isbill.Rows[0][0].ToString()) >= Convert.ToDouble(saleordrdt.Rows[i]["qty"].ToString().Replace(",", "")))
                                                {
                                                    if (ispending == false)
                                                    {
                                                        conn.execute("Update SaleOrderMaster SET OrderStatus='Clear',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and ClientID='" + cmbcustname.SelectedValue + "' and billno='" + saleordrdt.Rows[i]["billno"].ToString() + "'");
                                                    }
                                                }
                                                else
                                                {
                                                    conn.execute("Update SaleOrderMaster SET OrderStatus='Pending',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and ClientID='" + cmbcustname.SelectedValue + "' and billno='" + saleordrdt.Rows[i]["billno"].ToString() + "'");
                                                    ispending = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //if (ispending == false)
                                            //{
                                            //    conn.execute("Update SaleOrderMaster SET OrderStatus='Pending' where ClientID='" + cmbcustname.SelectedValue + "' and billno='" + saleordrdt.Rows[i]["billno"].ToString() + "'");
                                            //}

                                            bool isavail = false;
                                            for (int b = 0; b < dtsaleorder.Rows.Count; b++)
                                            {
                                                if (dtsaleorder.Rows[b]["productname"].ToString().Replace(",", "") == saleordrdt.Rows[i]["productname"].ToString().Replace(",", ""))
                                                {
                                                    for (int a = 0; a < LVFO.Items.Count; a++)
                                                    {
                                                        if (dtsaleorder.Rows[b]["productname"].ToString().Replace(",", "") == LVFO.Items[a].SubItems[0].Text.Replace(",", ""))
                                                        {
                                                            isavail = true;
                                                            break;
                                                        }
                                                    }
                                                }
                                                if (isavail == true)
                                                    break;

                                            }


                                            if (isavail == false)
                                            {
                                                conn.execute("Update SaleOrderMaster SET OrderStatus='Pending',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',Userid='"+master.CurrentUserid+"' where isactive=1 and ClientID='" + cmbcustname.SelectedValue + "' and billno='" + saleordrdt.Rows[i]["billno"].ToString() + "'");
                                                ispending = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #region
                        //for (int i = 0; i < dtsaleorder.Rows.Count; i++)
                        //{
                        //    for (int j = 0; j < LVFO.Items.Count; j++)
                        //    {

                        //        if (LVFO.Items[j].SubItems[0].Text.Replace(",", "") == dtsaleorder.Rows[i]["productname"].ToString().Replace(",", ""))
                        //        {
                        //            DataTable isbill = conn.getdataset("select sum(pqty) from BillProductMaster where  productname like '%" + LVFO.Items[j].SubItems[0].Text.Replace(",", "") + "%' and isactive=1");
                        //            if (isbill.Rows.Count > 0)
                        //            {

                        //                if (Convert.ToDouble(isbill.Rows[0][0].ToString()) >= Convert.ToDouble(dtsaleorder.Rows[i]["qty"].ToString().Replace(",", "")))
                        //                {
                        //                    if (ispending == false)
                        //                    {
                        //                        conn.execute("Update SaleOrderMaster SET OrderStatus='Clear' where ClientID='" + cmbcustname.SelectedValue + "' and billno='" + dtsaleorder.Rows[i]["billno"].ToString() + "'");
                        //                    }
                        //                }
                        //                else
                        //                {
                        //                    conn.execute("Update SaleOrderMaster SET OrderStatus='Pending' where ClientID='" + cmbcustname.SelectedValue + "' and billno='" + dtsaleorder.Rows[i]["billno"].ToString() + "'");
                        //                    ispending = true;
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            conn.execute("Update SaleOrderMaster SET OrderStatus='Pending' where ClientID='" + cmbcustname.SelectedValue + "' and billno='" + dtsaleorder.Rows[i]["billno"].ToString() + "'");
                        //        }
                        //    }
                        //}
                        #endregion

                        MessageBox.Show("Data Inserted Successfully.");
                        print();
                        clearitem();
                        clearall();
                        clearfooter();
                        LVFO.Items.Clear();
                        LVCHARGES.Items.Clear();



                    }

                }
                else
                {
                    MessageBox.Show("please fill all information");
                }
            }
            catch
            {
            }
        }
        private void btnsubmit()
        {
            try
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                if (Rewritrbooksofaccount.rewritedata == "True")
                {
                    submindata();
                }
                else
                {

                    DialogResult dr = MessageBox.Show("Do you want to Save?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        submindata();
                    }
                    else
                    {
                        MessageBox.Show("please fill all information");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                this.Enabled = true;
                con.Close();
            }
        }

        private void clearfooter()
        {
            lbltotcount.Text = "0";
            lbltotpqty.Text = "0";
            txttotaqty.Text = "0";
            txttotfree.Text = "0";
            lblbasictot.Text = "0";
            txttotdiscount.Text = "0";
            txttotadis.Text = "0";
            txttottax.Text = "0";
            txttotaddvat.Text = "0";
            txtamt.Text = "0";
            txttotservice.Text = "0";
            txttotalcharges.Text = "0";
            txtroundoff.Text = "0";
            TxtBillTotal.Text = "0";
            lblsgsttotsl.Text = "0";
            lblcgattotal.Text = "0";
            lbligsttotal.Text = "0";
            txtcess.Text = "0";

        }

        double addtax;
        double taxid, addtaxid;
        private void txtamt_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    ListViewItem li;
                    lbliteminfo.Visible = false;
                    li = LVFO.Items.Add(txtitemname.Text);
                    li.SubItems.Add(txtpacking.Text);
                    li.SubItems.Add("");
                    li.SubItems.Add(txtbags.Text);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtqty.Text), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtpqty.Text), 2).ToString()));
                    li.SubItems.Add(txtfree.Text);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtrate.Text), 2).ToString()));
                    li.SubItems.Add(txtper.Text);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txttotal.Text), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtdisper.Text), 2).ToString()));
                    li.SubItems.Add((Math.Round((Convert.ToDouble(txtdisamt.Text)), 2).ToString()));
                    li.SubItems.Add((Math.Round((Convert.ToDouble(txttax.Text)), 2).ToString()));
                    li.SubItems.Add((Math.Round((Convert.ToDouble(txtaddtax.Text)), 2).ToString()));
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtamount.Text), 2).ToString()));


                    totalcalculation();
                    clearitem();
                    txtitemname.Focus();
                }
            }
        }
        public void clearallitem()
        {
            txtpacking.Text = "0";
            // cmbbatch.SelectedIndex = 0;
            txtbags.Text = "1";
            // txtqty.Text = "0";
            txtpqty.Text = "1";
            //  txtfree.Text = "0";
            txtrate.Text = "0";
            // txtper.Text = "0";
            txttotal.Text = "0";
            txtdisper.Text = "0";
            txtdisamt.Text = "0";
            txttax.Text = "0";
            txtaddtax.Text = "0";
            txtamount.Text = "0";
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
        decimal debit1 = 0;
        decimal credit1 = 0;
        private string getprviousbalance(double opbal, double p, String DC, String actualdc, int i)
        {
            double balance;
            string bal = "";
            if (DC == "D")
            {
                if (actualdc == "D")
                {
                    balance = opbal + p;
                    bal = balance.ToString("N2") + " Dr.";
                }

                else
                {
                    if (opbal > p)
                    {
                        balance = opbal - p;
                        bal = balance.ToString("N2") + " Dr.";
                    }
                    else
                    {
                        balance = p - opbal;
                        bal = balance.ToString("N2") + " Cr.";
                    }
                }
            }
            if (DC == "C")
            {
                if (actualdc == "C")
                {
                    balance = opbal + p;
                    bal = balance.ToString("N2") + " Cr.";
                }
                else
                {
                    if (opbal > p)
                    {
                        balance = opbal - p;
                        bal = balance.ToString("N2") + " Cr.";
                    }
                    else
                    {
                        balance = p - opbal;
                        bal = balance.ToString("N2") + " Dr.";
                    }
                }
            }

            return bal;
        }
        private string getbalance(double opbal, double p, String DC, String actualdc, int i)
        {
            double balance;
            string bal = "";

            if (actualdc == "C")
            {
                balance = opbal + p;
                bal = balance.ToString("N2") + "";
            }

            else
            {
                balance = opbal - p;
                bal = balance.ToString("N2") + "";
            }

            return bal;
        }
        private void getstock(string p, string batch)
        {
            #region
            string proid = conn.ExecuteScalar("select Productid from ProductMaster where isactive=1 and Product_Name='" + p + "'");
            string openingstockfromitem = conn.ExecuteScalar("select opstock from productpricemaster where isactive=1 and productid= '" + proid + "' and BatchNo='" + batch + "'");
            string totalPurchase = "";
            string totalSale = "";
            if (totalPurchase == "" || totalPurchase == "NULL")
            {
                totalPurchase = "0.00";
            }
            if (totalSale == "" || totalSale == "NULL")
            {
                totalSale = "0.00";
            }
            Double opbal;
            string DC = "";
            if (Convert.ToDouble(totalPurchase) >= Convert.ToDouble(totalSale))
            {
                opbal = Convert.ToDouble(totalPurchase) - Convert.ToDouble(totalSale) + Convert.ToDouble(openingstockfromitem);
                // txtopbal.Text = opbal.ToString("N2");
                DC = "D";
            }
            else
            {
                opbal = Convert.ToDouble(totalSale) - Convert.ToDouble(totalPurchase) + Convert.ToDouble(openingstockfromitem);
                // txtopbal.Text = opbal.ToString("N2");
                DC = "C";
            }
            #endregion

            #region
            DataTable pos = conn.getdataset("select 'POS' as Billtype,BillDate as Bill_Run_Date,billno,totalnet as Rate,totalqty as pqty,BillId as Bill_No  from BillPOSMaster where isactive=1 order by BillDate asc");
            pos = changedtclone(pos);

            DataTable SPdt = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No from billproductmaster where isactive=1 and productid='" + proid + "' order by bill_Run_Date");
            string balance = "0.00";
            balance = Convert.ToString(opbal);
            Double debit = 0, credit = 0;
            SPdt = changedtclone(SPdt);
            SPdt.Merge(pos);
            SPdt.DefaultView.Sort = "[bill_Run_Date] DESC";
            SPdt = SPdt.DefaultView.ToTable();
            debit = 0;
            credit = 0;
            debit1 = 0;
            credit1 = 0;
            for (int i = 0; i < SPdt.Rows.Count; i++)
            {
                if (SPdt.Rows[i]["Billtype"].ToString() == "S")
                {
                    if (i != 0)
                    {
                        string[] str = balance.Split(' ');
                        char temp = str[0][0];
                        DC = temp.ToString();
                        opbal = Convert.ToDouble(str[0]);
                    }
                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                }
                else if (SPdt.Rows[i]["Billtype"].ToString() == "POS")
                {
                    if (i != 0)
                    {
                        string[] str = balance.Split(' ');
                        char temp = str[0][0];
                        DC = temp.ToString();
                        opbal = Convert.ToDouble(str[0]);
                    }
                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                }
                else if (SPdt.Rows[i]["Billtype"].ToString() == "SR")
                {
                    if (i != 0)
                    {
                        string[] str = balance.Split(' ');
                        char temp = str[0][0];
                        DC = temp.ToString();
                        opbal = Convert.ToDouble(str[0]);
                    }
                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                }
                else if (SPdt.Rows[i]["Billtype"].ToString() == "P")
                {
                    if (i != 0)
                    {
                        string[] str = balance.Split(' ');
                        char temp = str[0][0];
                        DC = temp.ToString();
                        opbal = Convert.ToDouble(str[0]);
                    }
                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                }
                else if (SPdt.Rows[i]["Billtype"].ToString() == "PR")
                {
                    if (i != 0)
                    {
                        string[] str = balance.Split(' ');
                        char temp = str[0][0];
                        DC = temp.ToString();
                        opbal = Convert.ToDouble(str[0]);
                    }
                    balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                }

            }
            DataTable stockadjustment = conn.getdataset("select sm.*,sim.* from stockadujestmentmaster sm inner join stockadujestmentitemmaster sim on sm.id=sim.stockid where sm.isactive=1 and sim.isactive=1 and sim.itemid='" + proid + "' order by sm.stockdate");
            for (int i = 0; i < stockadjustment.Rows.Count; i++)
            {

                if (Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString()) > 0)
                {
                    credit += Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                }
                else
                {
                    Double d = Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                    Double a1 = d * -1;
                    debit += Convert.ToDouble(a1);
                }
                Double bal = Convert.ToDouble(balance);
                Double astock = Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                Double fbalance = bal + astock;
                balance = Convert.ToString(fbalance);

            }
            currentstock = balance;
            if (options.Rows[0]["requirstockinfo"].ToString() == "True")
            {
                string minstock = conn.ExecuteScalar("select minstock from ProductMaster where isactive=1 and ProductID='" + proid + "'");
                Double a = Convert.ToDouble(minstock);
                Double b = Convert.ToDouble(balance);
                if (a >= b)
                {
                    MessageBox.Show(balance, "Min Stock level");
                }
            }
            #endregion
        }
        public static string currentstock;
        public static string lastprice;
        GetCurrentStock gc = new GetCurrentStock();
        public void enteritem()
        {
            pnlallitem.Visible = false;
            #region
            //x = 0;
            //SqlCommand cmd5 = new SqlCommand("select p.*,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.product_name='" + txtitemname.Text + "'", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd5);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //lbliteminfo.Visible = true;
            //qtyflag = 0;
            //txtpacking.Text = dt.Rows[0]["Packing"].ToString();
            //DataTable dis= conn.getdataset("select specialrate,discount from partyrates where (partyid=0 or partyid='" + cmbcustname.SelectedValue + "') and itemid=" + dt.Rows[0]["productid"].ToString());
            //if (dis.Rows.Count > 0)
            //{
            //    if (Convert.ToDouble(dis.Rows[0]["specialrate"].ToString()) > 0)
            //    {
            //        txtrate.Text = dis.Rows[0]["specialrate"].ToString();
            //    }
            //    else
            //    {
            //        txtrate.Text = dt.Rows[0]["SalePrice"].ToString();
            //    }
            //    if (Convert.ToDouble(dis.Rows[0]["discount"].ToString()) > 0)
            //    {
            //        txtdisper.Text = dis.Rows[0]["discount"].ToString();
            //    }
            //    else
            //    {
            //        txtdisper.Text = dt.Rows[0]["discount"].ToString();
            //    }
            //}
            //else
            //{

            //    txtdisper.Text = "0.00";
            //}

            //txtper.Text = dt.Rows[0]["Unit"].ToString();
            //lblbagqty.Text = "[" + dt.Rows[0]["Unit"].ToString() + "]";
            //lblaltqty.Text = "[" + dt.Rows[0]["Altunit"].ToString() + "]";
            //txtqty.Text = dt.Rows[0]["Convfactor"].ToString();
            //txtdisamt.Text = "0.00";

            //txtfree.Text = "0";
            //lbliteminfo.Text = "Sale Price=" + dt.Rows[0]["SalePrice"].ToString() + "  MRP=" + dt.Rows[0]["MRP"].ToString() + "  Basic=" + dt.Rows[0]["BasicPrice"].ToString() + "  Prch Price=" + dt.Rows[0]["SalePrice"].ToString();
            //SqlCommand cmd6 = new SqlCommand("select * from itemtaxmaster i inner join productmaster p on i.productid=p.productid where p.product_name like'%" + txtitemname.Text + "%' and i.saletypeid like '%" + cmbsaletype.Text + "%'", con);
            //SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
            //DataTable dt1 = new DataTable();
            //sda6.Fill(dt1);
            //string istax = conn.ExecuteScalar("Select taxtypename from Purchasetypemaster where isactive=1 and type='S' and Purchasetypename='" + cmbsaletype.Text + "'");
            //if (dt1.Rows.Count > 0)
            //{
            //    txttax.Text = "0";
            //    txtaddtax.Text = "0";
            //    if (istax != "Tax Free")
            //    {
            //        lbltax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["Vat"].ToString()), 2).ToString() + "]";
            //        lbladdtax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["AddVat"].ToString()), 2).ToString() + "]";
            //    }
            //    else
            //    {
            //        lbltax1.Text = "[0]";
            //        lbladdtax1.Text = "[0]";
            //    }

            //    txtpacking.Focus();
            //    taxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["Vat"].ToString()), 2);
            //    addtaxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["AddVat"].ToString()), 2);

            //}
            //else
            //{

            //    MessageBox.Show("Not any Tax Available For This Sale Type");
            //    txttax.Text = "0";
            //    txtaddtax.Text = "0";
            //    lbltax1.Text = "[0]";
            //    lbladdtax1.Text = "[0]";
            //    taxid = 0;
            //    addtaxid = 0;
            //    txtpacking.Focus();
            //}
            #endregion
            string v = txtitemname.Text.Replace("*", "");
            txtitemname.Text = v;
            if (txtitemname.Text == "")
            {
                MessageBox.Show("Please Select Item ");
                this.ActiveControl = txtitemname;
                pnlallitem.Visible = true;
                bindallitem();
                //return;
            }
            clearallitem();

            DataTable getitemid = conn.getdataset("select * from ProductPriceMaster where isactive=1 and Barcode='" + txtitemname.Text + "'");
            if (getitemid.Rows.Count > 0)
            {
                DataTable getitemname = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + getitemid.Rows[0]["ProductID"].ToString() + "'");
                txtitemname.Text = getitemname.Rows[0]["Product_Name"].ToString();
                //  cmbbatch.Text = getitemid.Rows[0]["BatchNo"].ToString();

            }
            dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbsaletype.Text + "'");
            dt1 = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + txtitemname.Text + "'  or itemnumber='" + txtitemname.Text + "'");
            txtitemname.Text = dt1.Rows[0]["Product_Name"].ToString();
            itemname = txtitemname.Text;
            if (dt1.Rows.Count <= 0)
            {
                MessageBox.Show("Item Not Available");
                pnlallitem.Visible = true;
                return;
            }
            dt2 = conn.getdataset("select * from TaxSlabMaster i inner join productmaster p on i.Taxslabname=p.taxslab where p.isactive=1 and i.isactive=1 and p.product_name='" + txtitemname.Text + "' and i.saletypename='" + cmbsaletype.Text + "'");
            dt3 = conn.getdataset("select * from ProductPriceMaster where isactive=1 and Productid='" + dt1.Rows[0]["ProductID"].ToString() + "'");
            //getstock(txtitemname.Text, dt3.Rows[0]["BatchNo"].ToString());
            currentstock = gc.getstock(txtitemname.Text, dt3.Rows[0]["BatchNo"].ToString());
            productid = dt1.Rows[0]["ProductID"].ToString();
            txtpacking.Text = dt1.Rows[0]["Packing"].ToString();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Please Select Type", "Select", MessageBoxButtons.OK);
                cmbsaletype.Focus();
            }
            else
            {
                try
                {
                    string rateprice = conn.ExecuteScalar("select bp.Rate from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno where b.isactive=1 and bp.isactive=1 and b.BillType='" + strfinalarray[0] + "' and b.ClientID='" + cmbcustname.SelectedValue + "' and bp.productid='" + dt1.Rows[0]["ProductID"].ToString() + "' order by b.Bill_No desc");
                    if (string.IsNullOrEmpty(rateprice))
                    {
                        lastprice = "0";
                    }
                    else
                    {
                        lastprice = rateprice;
                    }
                }
                catch
                {
                }
                if (dt.Rows[0]["Region"].ToString() == "Local")
                {
                    if (dt2.Rows.Count > 0 && dt3.Rows.Count > 0)
                    {
                        lbliteminfo.Text = "Tax=" + dt2.Rows[0]["Sgst"].ToString() + "% + " + dt2.Rows[0]["cgst"].ToString() + "%,AddTax=" + dt2.Rows[0]["additax"].ToString() + ",Sale Price=" + dt3.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt3.Rows[0]["MRP"].ToString() + ",Basic=" + dt3.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt3.Rows[0]["PurchasePrice"].ToString() + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                    }
                    else if (dt2.Rows.Count > 0 && dt3.Rows.Count <= 0)
                    {
                        lbliteminfo.Text = "Tax=" + dt2.Rows[0]["Sgst"].ToString() + "% + " + dt2.Rows[0]["cgst"].ToString() + "%,AddTax=" + dt2.Rows[0]["additax"].ToString() + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                    }
                    else if (dt2.Rows.Count <= 0 && dt3.Rows.Count > 0)
                    {
                        lbliteminfo.Text = "Tax=" + 0 + "% + " + 0 + "%,AddTax=" + 0 + ",Sale Price=" + dt3.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt3.Rows[0]["MRP"].ToString() + ",Basic=" + dt3.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt3.Rows[0]["PurchasePrice"].ToString() + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                    }
                    else if (dt2.Rows.Count <= 0 && dt3.Rows.Count <= 0)
                    {
                        lbliteminfo.Text = "Tax=" + 0 + "% + " + 0 + "%,AddTax=" + 0 + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                    }
                }
                else
                {
                    if (dt2.Rows.Count > 0 && dt3.Rows.Count > 0)
                    {
                        lbliteminfo.Text = "Tax=" + dt2.Rows[0]["Igst"].ToString() + "%,AddTax=" + dt2.Rows[0]["additax"].ToString() + ",Sale Price=" + dt3.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt3.Rows[0]["MRP"].ToString() + ",Basic=" + dt3.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt3.Rows[0]["PurchasePrice"].ToString() + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                    }
                    else if (dt2.Rows.Count > 0 && dt3.Rows.Count <= 0)
                    {
                        lbliteminfo.Text = "Tax=" + dt2.Rows[0]["Igst"].ToString() + "%,AddTax=" + dt2.Rows[0]["additax"].ToString() + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                    }
                    else if (dt2.Rows.Count <= 0 && dt3.Rows.Count > 0)
                    {
                        lbliteminfo.Text = "Tax=" + 0 + "%,AddTax=" + 0 + ",Sale Price=" + dt3.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt3.Rows[0]["MRP"].ToString() + ",Basic=" + dt3.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt3.Rows[0]["PurchasePrice"].ToString() + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                    }
                    else if (dt2.Rows.Count <= 0 && dt3.Rows.Count <= 0)
                    {
                        lbliteminfo.Text = "Tax=" + 0 + "%,AddTax=" + 0 + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                    }
                    // lbliteminfo.Text = "Tax=" + dt2.Rows[0]["Igst"].ToString() + "%,AddTax=" + dt2.Rows[0]["additax"].ToString() + ",Sale Price=" + dt3.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt3.Rows[0]["MRP"].ToString() + ",Basic=" + dt3.Rows[0]["BasicPrice"].ToString() + "";
                }
            }
            if (Convert.ToBoolean(dt1.Rows[0]["IsBatch"].ToString()) == true)
            {
                bindbatch();
                int count = cmbbatch.Items.Count - 1;
                cmbbatch.SelectedIndex = count;
                cmbbatch.DroppedDown = true;
            }
            else
            {

                cmbbatch.SelectedIndex = -1;
                cmbbatch.Enabled = false;
                //txtbags.Focus();
                x = 0;
                DataTable dprice = new DataTable();
                dprice = conn.getdataset("select PickupPrice from PurchasetypeMaster where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypename='" + cmbsaletype.Text + "'");
                Dprice = dprice.Rows[0]["PickupPrice"].ToString();
                SqlCommand cmd5 = new SqlCommand("select p.*,b.*,b." + Dprice + " as Dprice from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.isactive=1 and b.isactive=1 and p.product_name='" + txtitemname.Text + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                DataTable dt9 = new DataTable();
                sda.Fill(dt9);
                lbliteminfo.Visible = true;
                qtyflag = 0;
                double[] dis = new double[2];
                dis = calculatediscount(dt9.Rows[0]["Product_Name"].ToString(), cmbcustname.Text, Convert.ToDouble(dt9.Rows[0]["Dprice"].ToString()));
                //     DataTable dis = conn.getdataset("select specialrate,discount from partyrates where (partyid=0 or partyid='" + cmbcustname.SelectedValue + "') and itemid=" + dt9.Rows[0]["productid"].ToString());
                if (Convert.ToDouble(dis[1].ToString()) != 0)
                {
                    txtrate.Text = dis[1].ToString();
                }
                else
                {
                    txtrate.Text = dt9.Rows[0]["Dprice"].ToString();
                }
                txtdisper.Text = dis[0].ToString();
                //if (dis.Rows.Count > 0)
                //{
                //    if (Convert.ToDouble(dis.Rows[0]["specialrate"].ToString()) > 0)
                //    {
                //        txtrate.Text = dis.Rows[0]["specialrate"].ToString();
                //    }
                //    else
                //    {
                //        txtrate.Text = dt9.Rows[0]["Dprice"].ToString();
                //    }
                //    if (Convert.ToDouble(dis.Rows[0]["discount"].ToString()) > 0)
                //    {

                //        string disc = Convert.ToString(dis.Rows[0]["discount"].ToString());
                //        txtdisper.Text = disc;


                //        txtdisper.Text = dis.Rows[0]["discount"].ToString();
                //        txtdisper.Text = Convert.ToDouble(dis.Rows[0]["discount"].ToString()).ToString();
                //    }
                //    else
                //    {
                //        txtdisper.Text = dis.Rows[0]["discount"].ToString();
                //    }
                //}

                //else
                //{
                //    txtrate.Text = dt9.Rows[0]["Dprice"].ToString();
                //    txtdisper.Text = "0.00";
                //}
                if (options.Rows[0]["requirdlastpriceinbill"].ToString() == "True")
                {
                    try
                    {
                        string rateprice = conn.ExecuteScalar("select bp.Rate from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno where b.isactive=1 and bp.isactive=1 and b.BillType='" + strfinalarray[0] + "' and b.ClientID='" + cmbcustname.SelectedValue + "' and bp.productid='" + dt1.Rows[0]["ProductID"].ToString() + "' order by b.Bill_No desc");
                        if (!string.IsNullOrEmpty(rateprice))
                        {
                            txtrate.Text = rateprice;
                        }
                    }
                    catch
                    {
                    }
                }
                txtper.Text = dt9.Rows[0]["Unit"].ToString();
                lblbagqty.Text = "[" + dt9.Rows[0]["Unit"].ToString() + "]";
                lblaltqty.Text = "[" + dt9.Rows[0]["Altunit"].ToString() + "]";
                txtqty.Text = dt9.Rows[0]["Convfactor"].ToString();
                txtdisamt.Text = "0.00";

                txtfree.Text = "0";
                SqlCommand cmd6 = new SqlCommand("select * from TaxSlabMaster i inner join productmaster p on i.Taxslabname=p.taxslab where p.isactive=1 and i.isactive=1 and p.product_name='" + txtitemname.Text + "' and i.saletypename='" + cmbsaletype.Text + "'", con);
                SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                DataTable dt10 = new DataTable();
                sda6.Fill(dt10);
                string istax = conn.ExecuteScalar("Select taxtypename from Purchasetypemaster where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypename='" + cmbsaletype.Text + "'");
                if (dt10.Rows.Count > 0)
                {
                    txttax.Text = "0";
                    txtaddtax.Text = "0";
                    if (istax != "Tax Free")
                    {
                        lbltax1.Text = "[" + Math.Round(Convert.ToDouble(dt10.Rows[0]["igst"].ToString()), 2).ToString() + "]";
                        taxforprice = lbltax1.Text;
                        taxper = dt10.Rows[0]["igst"].ToString();
                        lbladdtax1.Text = "[" + Math.Round(Convert.ToDouble(dt10.Rows[0]["additax"].ToString()), 2).ToString() + "]";
                        ataxforprice = lbladdtax1.Text;
                        additaxper = dt10.Rows[0]["additax"].ToString();
                        taxid = Math.Round(Convert.ToDouble(dt10.Rows[0]["igst"].ToString()), 2);
                        addtaxid = Math.Round(Convert.ToDouble(dt10.Rows[0]["additax"].ToString()), 2);
                    }
                    else
                    {
                        lbltax1.Text = "[0]";
                        taxforprice = lbltax1.Text;
                        lbladdtax1.Text = "[0]";
                        ataxforprice = lbladdtax1.Text;
                        taxid = 0;
                        addtaxid = 0;

                    }

                    txtpacking.Focus();


                }
                else
                {

                    //MessageBox.Show("Not any Tax Available For This Sale Type");
                    txttax.Text = "0";
                    txtaddtax.Text = "0";
                    lbltax1.Text = "[0]";
                    taxforprice = lbltax1.Text;
                    lbladdtax1.Text = "[0]";
                    ataxforprice = lbladdtax1.Text;
                    taxid = 0;
                    addtaxid = 0;
                    txtpacking.Focus();
                }
                txtbags.Focus();
            }



        }
        public double[] calculatediscount(string itemname, string partyname, double basicprice)
        {
            bool isadd = false;
            double itemper = 0, itemamt = 0, discount = 0, disamt = 0, dispercompany = 0, disamtcompant = 0;
            string discountval = "";
            DataTable dt12 = conn.getdataset("select * from PartyRates where itemname='" + itemname + "' and (PartyName='" + partyname + "' OR PartyID='0')");

            if (dt12.Rows.Count > 0 && isadd == false && (Convert.ToDouble(dt12.Rows[0]["Discount"].ToString()) != 0 || Convert.ToDouble(dt12.Rows[0]["SpecialRate"].ToString()) != 0))
            {
                itemper = Convert.ToDouble(dt12.Rows[0]["Discount"].ToString());
                itemamt = Convert.ToDouble(dt12.Rows[0]["SpecialRate"].ToString());
                isadd = true;
            }
            DataTable dtgroup = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + itemname + "'");
            string groupid = dtgroup.Rows[0]["GroupName"].ToString();
            string companyid = dtgroup.Rows[0]["CompanyID"].ToString();
            DataTable dt123 = conn.getdataset("select * from PartyGroupDiscount where ItemGroupName='" + groupid + "'and(PartyName='" + partyname + "'OR PartyID=' ')");


            if (dt123.Rows.Count > 0 && isadd == false && (Convert.ToDouble(dt123.Rows[0]["Discount"].ToString()) != 0 || Convert.ToDouble(dt123.Rows[0]["SpecialRate"].ToString()) != 0))
            {
                itemper = Convert.ToDouble(dt123.Rows[0]["Discount"].ToString());
                itemamt = Convert.ToDouble(dt123.Rows[0]["SpecialRate"].ToString());
                isadd = true;
            }

            DataTable dt1234 = conn.getdataset("select * from PartyCompanyDiscount where ItemCompanyID='" + companyid + "'and(PartyName='" + partyname + "'OR PartyID=' ')");
            if (dt1234.Rows.Count > 0 && isadd == false && (Convert.ToDouble(dt1234.Rows[0]["Discount"].ToString()) != 0 || Convert.ToDouble(dt1234.Rows[0]["SpecialRate"].ToString()) != 0))
            {
                itemper = Convert.ToDouble(dt1234.Rows[0]["Discount"].ToString());
                itemamt = Convert.ToDouble(dt1234.Rows[0]["SpecialRate"].ToString());
                isadd = true;
            }

            //double disper = 0, disa = 0;
            //if (itemper > 0)
            //{
            //    disper = itemper;
            //    disa = 0;
            //    if (disper != 0)
            //    {
            //        disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
            //    }
            //    else
            //    {
            //        disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
            //    }
            //    disper = Math.Round(disper, 2);
            //    disamt = Math.Round(disa, 2);

            //}
            //else if (itemamt > 0)
            //{
            //    //disper = 0;
            //    //disa = itemamt;
            //    //if (disper != 0)
            //    //{
            //    //    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
            //    //}
            //    //else
            //    //{
            //    //    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
            //    //}
            //    //disper = Math.Round(disper, 2);
            //    //disamt = Math.Round(disa, 2);

            //}
            //else if (discount > 0)
            //{
            //    disper = discount;
            //    disa = 0;
            //    if (disper != 0)
            //    {
            //        disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
            //    }
            //    else
            //    {
            //        disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
            //    }
            //    disper = Math.Round(disper, 2);
            //    disamt = Math.Round(disa, 2);
            //}
            //else if (disamt > 0)
            //{
            //    //disper = 0;
            //    //disa = disamt;
            //    //if (disper != 0)
            //    //{
            //    //    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
            //    //}
            //    //else
            //    //{
            //    //    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
            //    //}
            //    //disper = Math.Round(disper, 2);
            //    //disamt = Math.Round(disa, 2);
            //}
            //else if (dispercompany > 0)
            //{
            //    disper = dispercompany;
            //    disa = 0;
            //    if (disper != 0)
            //    {
            //        disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
            //    }
            //    else
            //    {
            //        disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
            //    }
            //    disper = Math.Round(disper, 2);
            //    disamt = Math.Round(disa, 2);
            //}
            //else if (disamtcompant > 0)
            //{
            //    //disper = 0;
            //    //disa = disamtcompant;
            //    //if (disper != 0)
            //    //{
            //    //    disa = (Convert.ToDouble(basicprice) * Convert.ToDouble(disper)) / 100;
            //    //}
            //    //else
            //    //{
            //    //    disper = (100 * Convert.ToDouble(disa)) / Convert.ToDouble(basicprice);
            //    //}
            //    //disper = Math.Round(disper, 2);
            //    //disamt = Math.Round(disa, 2);
            //}
            double[] dis = new double[2];
            dis[0] = itemper;
            dis[1] = itemamt;
            return dis;
        }
        private void txtitemname_KeyDown(object sender, KeyEventArgs e)
        {

            {
                try
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        try
                        {
                            // lvallitem.Focus();
                            // lvallitem.Items[0].Selected = true;
                            // lvallitem.Select();
                            grditem.Rows[0].Selected = true;
                            grditem.Focus();




                        }
                        catch
                        {
                        }
                    }
                    if (e.KeyCode == Keys.Enter)
                    {
                        grditem.Rows[0].Selected = false;
                        enteritem();
                        cessflag = 0;
                    }
                    if (e.KeyCode == Keys.F3)
                    {
                        var privouscontroal = txtitemname;
                        activecontroal = privouscontroal.Name;
                        Itementry client = new Itementry(this, master, tabControl, activecontroal);

                        client.Passed(1);
                        //client.Show();

                        master.AddNewTab(client);

                        // autoreaderbind();
                        txtitemname.Focus();

                    }
                    if (e.KeyCode == Keys.F2)
                    {
                        if (txtitemname.Text != "")
                        {
                            var privouscontroal = txtitemname;
                            activecontroal = privouscontroal.Name;
                            Itementry client = new Itementry(this, master, tabControl, activecontroal);
                            client.Updatefromsale(txtitemname.Text);
                            client.Passed(1);
                            //client.Show();
                            master.AddNewTab(client);
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void itemcalculation(String qty)
        {
            try
            {
                SqlCommand cmd5 = new SqlCommand("select convfactor from ProductMaster where isactive=1 and product_name='" + txtitemname.Text + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                // Double convfactor = Convert.ToDouble(dt.Rows[0]["convfactor"].ToString());
                Double convfactor = Convert.ToDouble(txtqty.Text);

                double total = Convert.ToDouble(qty) * Convert.ToDouble(convfactor);
                double finaltotal = 0;
                if (!string.IsNullOrEmpty(txtrate.Text))
                {
                    finaltotal = Convert.ToDouble(qty) * Convert.ToDouble(txtrate.Text);
                }
                txttotal.Text = Math.Round(finaltotal, 2).ToString();
                if (!txtdisamt.Focused)
                {
                    if (!string.IsNullOrEmpty(txtdisper.Text))
                    {
                        txtdisamt.Text = (Math.Round((Convert.ToDouble(txtdisper.Text) * Convert.ToDouble(txttotal.Text)) / 100, 2)).ToString();
                    }
                }
                double discount = 0;
                if (!string.IsNullOrEmpty(txtdisamt.Text))
                {
                    discount = Convert.ToDouble(txttotal.Text) - Convert.ToDouble(txtdisamt.Text);
                }

                string istax = conn.ExecuteScalar("Select taxtypename from Purchasetypemaster where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypename='" + cmbsaletype.Text + "'");
                if (istax != "Tax Free")
                {
                    double tax = Math.Round(discount * (Convert.ToDouble(taxid.ToString())) / 100, 2);
                    double addtax = Math.Round(discount * (Convert.ToDouble(addtaxid.ToString())) / 100, 2);
                    txttax.Text = tax.ToString();
                    txtaddtax.Text = addtax.ToString();

                }
                else
                {
                    txttax.Text = "0";
                    txtaddtax.Text = "0";
                    taxid = 0;
                    addtaxid = 0;
                }
                double amount = Math.Round(discount + ((discount * (Convert.ToDouble(taxid.ToString()) + Convert.ToDouble(addtaxid.ToString()))) / 100), 2);
                txtamount.Text = Math.Round(amount, 2).ToString();
            }
            catch
            {
            }
        }
        private void totalcalculation()
        {
            try
            {

                Int32 count = 0;
                Double total = 0;
                Double vat = 0, basic = 0, discount = 0;
                Double addvat = 0;
                Double sgsttotal = 0;
                Double cgsttotal = 0;
                Double igsttotal = 0;
                Double totalcess = 0;
                Double pqty = 0, aqty = 0, free = 0;
                for (int i = 0; i < LVFO.Items.Count; i++)
                {
                    count++;

                    aqty = aqty + Convert.ToDouble(LVFO.Items[i].SubItems[4].Text);
                    pqty = pqty + Convert.ToDouble(LVFO.Items[i].SubItems[5].Text);
                    free = free + Convert.ToDouble(LVFO.Items[i].SubItems[6].Text);
                    basic = basic + Convert.ToDouble(LVFO.Items[i].SubItems[9].Text);
                    discount += Convert.ToDouble(LVFO.Items[i].SubItems[11].Text);
                    vat += Convert.ToDouble(LVFO.Items[i].SubItems[12].Text);
                    addvat += Convert.ToDouble(LVFO.Items[i].SubItems[13].Text);
                    total = total + Convert.ToDouble(LVFO.Items[i].SubItems[14].Text);
                    Double multi = 0, add = 0;
                    sgsttotal = sgsttotal + Convert.ToDouble(LVFO.Items[i].SubItems[16].Text);
                    cgsttotal = cgsttotal + Convert.ToDouble(LVFO.Items[i].SubItems[18].Text);
                    igsttotal = igsttotal + Convert.ToDouble(LVFO.Items[i].SubItems[20].Text);
                    totalcess = totalcess + Convert.ToDouble(LVFO.Items[i].SubItems[23].Text);
                    //multi = (Convert.ToDouble(LVFO.Items[i].SubItems[7].Text) * (Convert.ToDouble(LVFO.Items[i].SubItems[8].Text) / 100));
                    //vat = vat + multi;
                    //add = (Convert.ToDouble(LVFO.Items[i].SubItems[7].Text) * (Convert.ToDouble(LVFO.Items[i].SubItems[9].Text) / 100));
                    //addvat += add;

                }
                try
                {
                    for (int j = 0; j < LVCHARGES.Items.Count; j++)
                    {
                        sgsttotal = sgsttotal + Convert.ToDouble(LVCHARGES.Items[j].SubItems[8].Text);
                        cgsttotal = cgsttotal + Convert.ToDouble(LVCHARGES.Items[j].SubItems[9].Text);
                        igsttotal = igsttotal + Convert.ToDouble(LVCHARGES.Items[j].SubItems[10].Text);
                        addvat = addvat + Convert.ToDouble(LVCHARGES.Items[j].SubItems[11].Text);
                    }
                }
                catch
                {
                    sgsttotal += 0;
                    cgsttotal += 0;
                    igsttotal += 0;
                    addvat += 0;
                }
                lbltotcount.Text = count.ToString("");
                lbltotpqty.Text = pqty.ToString("");
                txttotfree.Text = free.ToString("");
                txttotaqty.Text = aqty.ToString();
                lblbasictot.Text = basic.ToString();
                txttotdiscount.Text = discount.ToString("N2");
                txttottax.Text = Math.Round(vat, 2).ToString("N2");
                txttotaddvat.Text = Math.Round(addvat, 2).ToString("N2");
                txtamt.Text = Math.Round(total, 2).ToString("N2");
                lblsgsttotsl.Text = sgsttotal.ToString("");
                lblcgattotal.Text = cgsttotal.ToString("");
                lbligsttotal.Text = igsttotal.ToString("");
                txttotalcess.Text = totalcess.ToString("");
                getOptions(Math.Round(total, 2));



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Messagebox:" + ex.Message);
            }
        }

        private void getOptions(Double total)
        {

            DataTable dt = options;
            if (dt.Rows.Count > 0)
            {
                if (strfinalarray[0] == "S")
                {
                    if (Convert.ToBoolean(dt.Rows[0]["autoroundoffsale"].ToString()) == true)
                    {

                        double charges = Convert.ToDouble(txttotalcharges.Text);
                        // double cess = Convert.ToDouble(txttotalcess.Text);
                        TxtBillTotal.Text = Math.Round(total + charges, 0).ToString("N2");
                        txtroundoff.Text = Math.Round((Math.Round(Convert.ToDouble(TxtBillTotal.Text), 0) - Convert.ToDouble(total + charges)), 2).ToString();


                    }
                    else
                    {
                        double charges = Convert.ToDouble(txttotalcharges.Text);
                        //  double cess = Convert.ToDouble(txttotalcess.Text);
                        TxtBillTotal.Text = Math.Round(total + charges, 2).ToString("N2");
                        txtroundoff.Text = "0";

                    }
                }
                else if (strfinalarray[0] == "SR")
                {
                    if (Convert.ToBoolean(dt.Rows[0]["autoroundoffsale"].ToString()) == true)
                    {

                        double charges = Convert.ToDouble(txttotalcharges.Text);
                        // double cess = Convert.ToDouble(txttotalcess.Text);
                        TxtBillTotal.Text = Math.Round(total + charges, 0).ToString("N2");
                        txtroundoff.Text = Math.Round((Math.Round(Convert.ToDouble(TxtBillTotal.Text), 0) - Convert.ToDouble(total + charges)), 2).ToString();


                    }
                    else
                    {
                        double charges = Convert.ToDouble(txttotalcharges.Text);
                        //    double cess = Convert.ToDouble(txttotalcess.Text);
                        TxtBillTotal.Text = Math.Round(total + charges, 2).ToString("N2");
                        txtroundoff.Text = "0";

                    }
                }
                else if (strfinalarray[0] == "P")
                {
                    if (Convert.ToBoolean(dt.Rows[0]["autoroundoffpurchase"].ToString()) == true)
                    {

                        double charges = Convert.ToDouble(txttotalcharges.Text);
                        //   double cess = Convert.ToDouble(txttotalcess.Text);
                        TxtBillTotal.Text = Math.Round(total + charges, 0).ToString("N2");
                        txtroundoff.Text = Math.Round((Math.Round(Convert.ToDouble(TxtBillTotal.Text), 0) - Convert.ToDouble(total + charges)), 2).ToString();


                    }
                    else
                    {
                        double charges = Convert.ToDouble(txttotalcharges.Text);
                        //  double cess = Convert.ToDouble(txttotalcess.Text);
                        TxtBillTotal.Text = Math.Round(total + charges, 2).ToString("N2");
                        txtroundoff.Text = "0";

                    }
                }

                else if (strfinalarray[0] == "PR")
                {
                    if (Convert.ToBoolean(dt.Rows[0]["autoroundoffpurchase"].ToString()) == true)
                    {

                        double charges = Convert.ToDouble(txttotalcharges.Text);
                        //  double cess = Convert.ToDouble(txttotalcess.Text);
                        TxtBillTotal.Text = Math.Round(total + charges, 0).ToString("N2");
                        txtroundoff.Text = Math.Round((Math.Round(Convert.ToDouble(TxtBillTotal.Text), 0) - Convert.ToDouble(total + charges)), 2).ToString();


                    }
                    else
                    {
                        double charges = Convert.ToDouble(txttotalcharges.Text);
                        //   double cess = Convert.ToDouble(txttotalcess.Text);
                        TxtBillTotal.Text = Math.Round(total + charges, 2).ToString("N2");
                        txtroundoff.Text = "0";

                    }
                }

            }

        }
        int qtyflag;
        public static string itemproductid, newitemproductid = "";
        private void txtpqty_TextChanged(object sender, EventArgs e)
        {

            {
                try
                {
                    if (!txtbags.Focused)
                    {
                        //if (qtyflag == 0)
                        //{
                        //    qtyflag = 1;
                        if (txtpqty.Text != "")
                        {
                            if (txtqty.Text != "")
                            {
                                Double qty = Convert.ToDouble(Convert.ToDouble(txtpqty.Text) / Convert.ToDouble(txtqty.Text));

                                txtbags.Text = qty.ToString();

                            }
                            else
                            {
                                Double qty = Convert.ToDouble(Convert.ToDouble(txtpqty.Text) / 1);
                                txtbags.Text = qty.ToString();

                            }
                        }
                        else
                        {
                            if (txtqty.Text != "")
                            {
                                Double qty = Convert.ToDouble(1 * Convert.ToDouble(txtqty.Text));
                                txtbags.Text = qty.ToString();

                            }
                            else
                            {
                                txtpqty.Text = "0.00";
                            }

                        }
                        if (txtpqty.Text != "" && txtpqty.Text != "0.00")
                        {
                            itemcalculation(txtpqty.Text);

                        }
                        else
                        {
                            itemcalculation("0");
                        }
                        //  qtyflag = 0;
                    }
                }
                catch
                {
                    // qtyflag = 0;
                }


                try
                {
                    if (txtpqty.Text != "")
                    {


                    }
                    else
                    {
                        itemcalculation("1");
                    }
                }
                catch
                {
                    qtyflag = 0;
                }
            }
        }
        Int32 rowid = -1;
        private void LVFO_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            {
                if (LVFO.SelectedItems.Count > 0)
                {

                    rowid = LVFO.FocusedItem.Index;
                    lbliteminfo.Visible = true;

                    string dt11 = conn.ExecuteScalar("select Product_Name from ProductMaster where isactive=1 and ProductID='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "'");
                    txtitemname.Text = dt11;
                    //bindbatch();
                    cmbbatch.Enabled = true;

                    dt8 = new DataTable();
                    SqlCommand cmd1 = new SqlCommand("select Productid,Batchno from ProductPriceMaster where Productid='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "' and isactive='1'", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    sda1.Fill(dt8);
                    cmbbatch.ValueMember = "Productid";
                    cmbbatch.DisplayMember = "Batchno";
                    cmbbatch.DataSource = dt8;
                    cmbbatch.Focus();
                    cmbbatch.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text;
                    if (LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text == "")
                    {
                        cmbbatch.Enabled = false;
                        cmbbatch.SelectedIndex = -1;
                    }
                    else
                    {
                        cmbbatch.Enabled = true;

                    }

                    txtpacking.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text;
                    txtbags.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[3].Text;
                    txtqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[4].Text;
                    txtpqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text;
                    txtfree.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[6].Text;
                    txtrate.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[7].Text;
                    txtper.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[8].Text;
                    txttotal.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[9].Text;
                    txtdisper.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[10].Text;
                    txtdisamt.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[11].Text;
                    txttax.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[12].Text;
                    txtaddtax.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[13].Text;
                    txtamount.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[14].Text;
                    string serial = LVFO.Items[LVFO.FocusedItem.Index].SubItems[22].Text;
                    itemproductid = LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text;
                    txtboxsrno.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[25].Text;
                    LVFO.Items[LVFO.FocusedItem.Index].SubItems[22].Text = "";
                    if (!string.IsNullOrEmpty(serial))
                    {
                        string[] values = serial.Split(',');
                        //  temptable = new DataTable();
                        // temptable.Columns.Add("Itemname", typeof(string));
                        //  temptable.Columns.Add("SERIAL", typeof(string));
                        for (int i = 0; i < values.Length; i++)
                        {
                            ListViewItem li = new ListViewItem();
                            li = lvserial.Items.Add(values[i] + Environment.NewLine);
                            temptable.Rows.Add(txtitemname.Text, values[i]);
                        }
                    }
                    SqlCommand cmd5 = new SqlCommand("select p.*,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.isactive=1 and b.isactive=1 and p.ProductID='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    lblbagqty.Text = "[" + dt.Rows[0]["Unit"].ToString() + "]";
                    lblaltqty.Text = "[" + dt.Rows[0]["Altunit"].ToString() + "]";

                    SqlCommand cmd6 = new SqlCommand("select * from TaxSlabMaster i inner join productmaster p on i.Taxslabname=p.taxslab where p.isactive=1 and i.isactive=1 and  p.ProductID='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "' and i.saletypename='" + cmbsaletype.Text + "'", con);
                    SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                    DataTable dt1 = new DataTable();
                    sda6.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {

                        lbltax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["igst"].ToString()), 2).ToString() + "]";
                        taxforprice = lbltax1.Text;
                        lbladdtax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["additax"].ToString()), 2).ToString() + "]";
                        ataxforprice = lbladdtax1.Text;
                        txtbags.Focus();
                        taxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["igst"].ToString()), 2);
                        addtaxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["additax"].ToString()), 2);

                    }
                    else
                    {

                        //  MessageBox.Show("Not any Tax Available For This Sale Type");

                        lbltax1.Text = "[0]";
                        taxforprice = lbltax1.Text;
                        lbladdtax1.Text = "[0]";
                        ataxforprice = lbladdtax1.Text;
                        taxid = 0;
                        addtaxid = 0;
                        txtbags.Focus();
                    }

                    Double total = 0;
                    try
                    {
                        total = Math.Round((Convert.ToDouble(TxtBillTotal.Text) - Convert.ToDouble(LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text)), 2);
                    }
                    catch
                    {
                    }
                    TxtBillTotal.Text = total.ToString();
                    //      LVFO.Items[LVFO.FocusedItem.Index].Remove();
                    totalcalculation();

                }
            }
        }

        private void txtbags_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txtbags.Text))
                    {
                        txtbags.Text = "1";
                    }
                    txtpqty.Focus();
                    qtyflag = 0;
                    try
                    {
                        if (!txtpqty.Focused)
                        {
                            //if (qtyflag == 0)
                            //{
                            //    qtyflag = 1;
                            if (txtbags.Text != "")
                            {
                                if (txtqty.Text != "")
                                {
                                    Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * Convert.ToDouble(txtqty.Text));
                                    txtpqty.Text = qty.ToString();
                                }
                                else
                                {
                                    Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * 1);
                                    txtpqty.Text = qty.ToString();
                                }
                            }
                            else
                            {
                                if (txtqty.Text != "")
                                {
                                    Double qty = Convert.ToDouble(1 * Convert.ToDouble(txtqty.Text));
                                    txtpqty.Text = qty.ToString();
                                }
                                else
                                {
                                    txtpqty.Text = "0.00";
                                }

                            }
                            if (txtpqty.Text != "" && txtpqty.Text != "0.00")
                            {
                                itemcalculation(txtpqty.Text);

                            }
                            else
                            {
                                itemcalculation("0");
                            }
                        }
                        txtqty.Focus();
                    }
                    catch
                    {
                    }

                }

            }
        }

        private void txtpqty_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    //if (strfinalarray[0] == "S")
                    //{
                    //    if (Convert.ToBoolean(options.Rows[0]["reqsrno"].ToString()) == true)
                    //    {
                    //        pnlboxsrno.Visible = true;
                    //        if (pnlboxsrno.Visible == true)
                    //        {
                    //            txtboxsrno.Focus();
                    //        }
                    //        else
                    //        {
                    //            txtfree.Focus();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        txtfree.Focus();
                    //    }

                    //}
                    //else
                    //{
                    txtfree.Focus();
                    //}

                    if (string.IsNullOrEmpty(txtpqty.Text))
                    {
                        txtpqty.Text = "1";
                    }
                    try
                    {
                        if (!txtbags.Focused)
                        {
                            //if (qtyflag == 0)
                            //{
                            //    qtyflag = 1;
                            if (txtpqty.Text != "")
                            {
                                if (txtqty.Text != "")
                                {
                                    Double qty = Convert.ToDouble(Convert.ToDouble(txtpqty.Text) / Convert.ToDouble(txtqty.Text));

                                    txtbags.Text = qty.ToString();

                                }
                                else
                                {
                                    Double qty = Convert.ToDouble(Convert.ToDouble(txtpqty.Text) / 1);
                                    txtbags.Text = qty.ToString();

                                }
                            }
                            else
                            {
                                if (txtqty.Text != "")
                                {
                                    Double qty = Convert.ToDouble(1 * Convert.ToDouble(txtqty.Text));
                                    txtbags.Text = qty.ToString();

                                }
                                else
                                {
                                    txtpqty.Text = "0.00";
                                }

                            }
                            if (txtpqty.Text != "" && txtpqty.Text != "0.00")
                            {
                                itemcalculation(txtpqty.Text);

                            }
                            else
                            {
                                itemcalculation("0");
                            }
                            //  qtyflag = 0;
                        }
                    }
                    catch
                    {
                        // qtyflag = 0;
                    }
                    if (options.Rows[0]["itemspeed"].ToString() == "Qty +Free Only")
                    {
                        txtfree.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Rate +Discount(%) Only")
                    {
                        txtrate.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate + Discount(%) Only")
                    {
                        txtfree.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate Only")
                    {
                        txtfree.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty +Rate Only")
                    {
                        txtrate.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty Only")
                    {
                        enteramount();
                    }
                }
            }
        }

        private void txtrate_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txtrate.Text))
                    {
                        txtrate.Text = "0";
                    }
                    //DataTable cess = conn.getdataset("select cessper,cessamt from ProductMaster where isactive=1 and Product_Name='" + txtitemname.Text + "'");
                    //if (cess.Rows.Count > 0)
                    //{
                    //    if (Convert.ToDouble(cess.Rows[0]["cessper"].ToString()) > 0 && Convert.ToDouble(cess.Rows[0]["cessamt"].ToString()) > 0)
                    //    {
                    //        cess c = new cess(cess.Rows[0]["cessper"].ToString(), cess.Rows[0]["cessamt"].ToString(),txtrate,txtcess);
                    //        c.Show();
                    //    }
                    //    else
                    //    {
                    //        txttotal.Focus();
                    //    }
                    //}
                    //else
                    //{
                    txttotal.Focus();
                    //}
                    if (options.Rows[0]["itemspeed"].ToString() == "Qty +Free Only")
                    {
                        //   enteramount();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Rate +Discount(%) Only")
                    {
                        txtdisper.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate + Discount(%) Only ")
                    {
                        txtdisper.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate Only")
                    {
                        enteramount();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty +Rate Only")
                    {
                        enteramount();
                    }

                }
                if (e.KeyCode == Keys.F3)
                {
                    if (txtrate.Text == "")
                    {
                        txtrate.Text = "0";
                    }
                    Price p = new Price(txtrate, txtamount, strfinalarray);
                    p.ShowDialog();
                }
            }
        }
        int x, y;

        private void txtrate_TextChanged(object sender, EventArgs e)
        {

            {
                try
                {
                    if (Convert.ToDouble(txtrate.Text) > 0)
                    {
                        if (x == 0)
                        {

                            itemcalculation(txtpqty.Text);
                        }
                    }
                }
                catch
                {
                }
            }
        }
        public static string s;
        public void caseterms()
        {
            try
            {
                if (cmbterms.Text == "Cash")
                {
                    string qry = "";
                    if (Convert.ToBoolean(options.Rows[0]["showcustomersupplierseperate"].ToString()) == true)
                    {
                        if (strfinalarray[0] == "S")
                        {
                            DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
                            string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                            qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or groupID=11 or GroupID='" + groupid + "') order by AccountName";
                        }
                        else if (strfinalarray[0] == "SR")
                        {
                            DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
                            string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                            qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or groupID=11 or GroupID='" + groupid + "') order by AccountName";
                        }
                        else if (strfinalarray[0] == "P")
                        {
                            DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                            string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                            qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or groupID=11 or GroupID='" + groupid + "') order by AccountName";
                            //   qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=100 order by AccountName";
                        }
                        else if (strfinalarray[0] == "PR")
                        {
                            DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                            string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                            qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or groupID=11 or GroupID='" + groupid + "') order by AccountName";
                            //qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=100 order by AccountName";
                        }
                    }
                    else
                    {
                        DataTable Accountgroup = conn.getdataset("select * from AccountGroup where (id='99' or id='100')");
                        string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                        string groupid1 = Accountgroup.Rows[1]["UnderGroupID"].ToString();
                        qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or groupID=11 or GroupID=100 or GroupID='" + groupid + "' or GroupID='" + groupid1 + "') order by AccountName";
                        //  qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or groupid=99) order by AccountName";
                    }


                    SqlCommand cmd1 = new SqlCommand(qry, con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    cmbcustname.ValueMember = "ClientID";
                    cmbcustname.DisplayMember = "AccountName";
                    cmbcustname.DataSource = dt1;
                    cmbcustname.SelectedIndex = -1;
                }
            }
            catch
            {
            }
        }
        private void cmbterms_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // This will eliminate the beeping
                    bool inList = false;
                    for (int i = 0; i < cmbterms.Items.Count; i++)
                    {
                        s = cmbterms.GetItemText(cmbterms.Items[i]);
                        if (s == cmbterms.Text)
                        {
                            inList = true;
                            cmbterms.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbterms.Text = "";
                    }
                    caseterms();
                    txtduedate.Focus();
                }
            }
        }
        public void getduedate()
        {
            try
            {
                DataTable due = conn.getdataset("select * from ClientMaster where isactive=1 and AccountName='" + cmbcustname.Text + "'");
                if (due.Rows.Count > 0)
                {
                    if (strfinalarray[0] == "S")
                    {
                        DateTime billdate = TxtRundate.Value;
                        string creditdays = due.Rows[0]["credaysale"].ToString();
                        if (string.IsNullOrEmpty(creditdays))
                        {
                            creditdays = "0";
                        }
                        DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                        txtduedate.Value = duedate;
                    }
                    else if (strfinalarray[0] == "SR")
                    {
                        DateTime billdate = TxtRundate.Value;
                        string creditdays = due.Rows[0]["credaysale"].ToString();
                        if (string.IsNullOrEmpty(creditdays))
                        {
                            creditdays = "0";
                        }
                        DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                        txtduedate.Value = duedate;
                    }
                    else if (strfinalarray[0] == "P")
                    {
                        DateTime billdate = TxtRundate.Value;
                        string creditdays = due.Rows[0]["credaypurchase"].ToString();
                        if (string.IsNullOrEmpty(creditdays))
                        {
                            creditdays = "0";
                        }
                        DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                        txtduedate.Value = duedate;
                    }
                    else if (strfinalarray[0] == "PR")
                    {
                        DateTime billdate = TxtRundate.Value;
                        string creditdays = due.Rows[0]["credaypurchase"].ToString();
                        if (string.IsNullOrEmpty(creditdays))
                        {
                            creditdays = "0";
                        }
                        DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                        txtduedate.Value = duedate;
                    }
                }
            }
            catch
            {
            }
        }
        private void cmbcustname_KeyDown(object sender, KeyEventArgs e)
        {

            {

                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // This will eliminate the beeping
                    bool inList = false;
                    for (int i = 0; i < cmbcustname.Items.Count; i++)
                    {
                        s = cmbcustname.GetItemText(cmbcustname.Items[i]);
                        if (s == cmbcustname.Text)
                        {
                            inList = true;
                            cmbcustname.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbcustname.Text = "";
                    }
                    getduedate();
                    cmbsaletype.Focus();
                    if (cmbcustname.Text != "")
                    {
                        DataTable dtClientAddress = conn.getdataset("select * from ClientMaster where isactive=1 and AccountName='" + cmbcustname.Text + "'");
                        //if (dtClientAddress.Rows.Count > 0)
                        //{
                        //    txttransport.Text = dtClientAddress.Rows[0]["Address"].ToString() + " " + dtClientAddress.Rows[0]["City"].ToString() + " " + dtClientAddress.Rows[0]["State"].ToString() + " " + "Ph: " + dtClientAddress.Rows[0]["Phone"].ToString() + "Mob: " + dtClientAddress.Rows[0]["Mobile"].ToString() + "";
                        //}
                        // getsr();
                        if (strfinalarray[0] == "S" || strfinalarray[0] == "P")
                        {
                            string oldrefno = conn.ExecuteScalar("select refno from BillMaster where isactive=1 and billno='" + TxtBillNo.Text + "' and billtype='" + strfinalarray[0] + "'");
                            if (string.IsNullOrEmpty(oldrefno))
                            {
                                DataTable dtcn = new DataTable();
                                if (strfinalarray[0] == "P")
                                {
                                    dtcn = conn.getdataset("Select distinct ClientId from SaleOrderMaster where isactive=1 and OrderStatus='Pending' and ClientId='" + cmbcustname.SelectedValue + "' and (BillType like '%" + strfinalarray[0] + "%' or BillType like '%" + "STI" + "%')");
                                }
                                else
                                {
                                    dtcn = conn.getdataset("Select distinct ClientId from SaleOrderMaster where isactive=1 and OrderStatus='Pending' and ClientId='" + cmbcustname.SelectedValue + "' and BillType like '%" + strfinalarray[0] + "%'");
                                }
                                //dtcn = conn.getdataset("Select distinct ClientId from SaleOrderMaster where isactive=1 and OrderStatus='Pending' and ClientId='" + cmbcustname.SelectedValue + "' and BillType like '%" + strfinalarray[0] + "%'");
                                if (dtcn != null && dtcn.Rows.Count > 0)
                                {

                                    SaleOrder frm = new SaleOrder(dtcn.Rows[0][0].ToString(), this, strfinalarray);

                                    frm.ShowDialog();

                                    //string userEnteredText = frm.EnteredText;
                                    //frm.Dispose();

                                }
                            }
                        }
                        // //txtpono.Focus();
                        cmbsaletype.Focus();
                        // txtpono.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Please Select Customer");
                        cmbcustname.Focus();
                    }
                    //if (cmbcustname.Text != "")
                    //{
                    //    cmbsaletype.Focus();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Please Select Customer");
                    //    cmbcustname.Focus();
                    //}
                }
                if (e.KeyCode == Keys.F3)
                {
                    var privouscontroal = cmbcustname;
                    activecontroal = privouscontroal.Name;
                    Accountentry client = new Accountentry(this, master, tabControl, activecontroal, strfinalarray[0]);

                    client.Passed(1);
                    //   client.Show();
                    master.AddNewTab(client);
                }
                if (e.KeyCode == Keys.F2)
                {
                    var privouscontroal = cmbcustname;
                    activecontroal = privouscontroal.Name;
                    string iid = cmbcustname.SelectedValue.ToString();

                    Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                    client.Update(1, iid);
                    client.Passed(1);
                    //  client.Show();
                    master.AddNewTab(client);
                }

            }
        }
        public void bindbillno()
        {
            if (cmbsaletype.Text != "")
            {


                if (BtnPayment.Text == "&Submit")
                {
                    if (strfinalarray[0] == "S")
                    {
                        if (options.Rows[0]["salevoucherno"].ToString() == "Continuous")
                        {

                            String str = conn.ExecuteScalar("select max(Bill_No) from BillMaster where isactive='1' and BillType='S' and id=(select max(id) from billmaster where isactive=1 and BillType='S')");
                            var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-" };
                            foreach (var c in charsToRemove)
                            {
                                str = str.Replace(c, string.Empty);
                            }

                            DataTable dt = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypeid='" + cmbsaletype.SelectedValue + "'");
                            Int64 id, count;
                            if (str == "")
                            {

                                id = Convert.ToInt64(1);
                                count = Convert.ToInt64(1);
                            }
                            else
                            {
                                id = Convert.ToInt64(str) + 1;
                                count = Convert.ToInt64(str) + 1;
                            }
                            lblbill_no.Text = count.ToString();
                            TxtBillNo.Text = dt.Rows[0]["prefix"].ToString() + count.ToString();
                        }
                        else if (options.Rows[0]["salevoucherno"].ToString().Trim() == "Type_Wise")
                        {
                            getsr();
                        }
                        else
                        {
                            //   TxtBillNo.Text = "";
                            TxtBillNo.ReadOnly = false;
                        }
                    }
                    else if (strfinalarray[0] == "SR")
                    {
                        if (options.Rows[0]["salervoucherno"].ToString() == "Continuous")
                        {
                            String str = conn.ExecuteScalar("select max(Bill_No) from BillMaster where isactive='1' and BillType='SR' and id=(select max(id) from billmaster where isactive=1 and BillType='SR')");
                            var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-" };
                            foreach (var c in charsToRemove)
                            {
                                str = str.Replace(c, string.Empty);
                            }
                            DataTable dt = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypeid='" + cmbsaletype.SelectedValue + "'");
                            Int64 id, count;
                            if (str == "")
                            {

                                id = Convert.ToInt64(1);
                                count = Convert.ToInt64(1);
                            }
                            else
                            {
                                id = Convert.ToInt64(str) + 1;
                                count = Convert.ToInt64(str) + 1;
                            }
                            lblbill_no.Text = count.ToString();
                            TxtBillNo.Text = dt.Rows[0]["prefix"].ToString() + count.ToString();
                        }
                        else if (options.Rows[0]["salervoucherno"].ToString() == "Type_Wise")
                        {
                            getsr();
                        }
                        else
                        {
                            //  TxtBillNo.Text = "";
                            TxtBillNo.ReadOnly = false;
                        }
                    }
                    else if (strfinalarray[0] == "P")
                    {
                        if (options.Rows[0]["purchasevoucherno"].ToString() == "Continuous")
                        {
                            String str = conn.ExecuteScalar("select max(Bill_No) from BillMaster where isactive='1' and BillType='P' and id=(select max(id) from billmaster where isactive=1 and BillType='P')");
                            var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-" };
                            foreach (var c in charsToRemove)
                            {
                                str = str.Replace(c, string.Empty);
                            }
                            DataTable dt = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypeid='" + cmbsaletype.SelectedValue + "'");
                            Int64 id, count;
                            if (str == "")
                            {

                                id = Convert.ToInt64(1);
                                count = Convert.ToInt64(1);
                            }
                            else
                            {
                                id = Convert.ToInt64(str) + 1;
                                count = Convert.ToInt64(str) + 1;
                            }
                            lblbill_no.Text = count.ToString();
                            TxtBillNo.Text = dt.Rows[0]["prefix"].ToString() + count.ToString();
                        }
                        else if (options.Rows[0]["purchasevoucherno"].ToString() == "Type_Wise")
                        {
                            getsr();
                        }
                        else
                        {
                            // TxtBillNo.Text = "";
                            TxtBillNo.ReadOnly = false;
                        }
                    }
                    else if (strfinalarray[0] == "PR")
                    {
                        if (options.Rows[0]["purchaservoucherno"].ToString() == "Continuous")
                        {
                            String str = conn.ExecuteScalar("select max(Bill_No) from BillMaster where isactive='1' and BillType='PR' and id=(select max(id) from billmaster where isactive=1 and BillType='PR')");
                            var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-" };
                            foreach (var c in charsToRemove)
                            {
                                str = str.Replace(c, string.Empty);
                            }
                            DataTable dt = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypeid='" + cmbsaletype.SelectedValue + "'");
                            Int64 id, count;
                            if (str == "")
                            {

                                id = Convert.ToInt64(1);
                                count = Convert.ToInt64(1);
                            }
                            else
                            {
                                id = Convert.ToInt64(str) + 1;
                                count = Convert.ToInt64(str) + 1;
                            }
                            lblbill_no.Text = count.ToString();
                            TxtBillNo.Text = dt.Rows[0]["prefix"].ToString() + count.ToString();
                        }
                        else if (options.Rows[0]["purchaservoucherno"].ToString() == "Type_Wise")
                        {
                            getsr();
                        }
                        else
                        {
                            //  TxtBillNo.Text = "";
                            TxtBillNo.ReadOnly = false;
                        }
                    }
                }
                else
                {
                    if (options.Rows[0]["purchaservoucherno"].ToString() == "Manual")
                    {
                        TxtBillNo.ReadOnly = false;
                    }
                    else if (options.Rows[0]["purchasevoucherno"].ToString() == "Manual")
                    {
                        TxtBillNo.ReadOnly = false;
                    }
                    else if (options.Rows[0]["salervoucherno"].ToString() == "Manual")
                    {
                        TxtBillNo.ReadOnly = false;
                    }
                    else if (options.Rows[0]["salevoucherno"].ToString() == "Manual")
                    {
                        TxtBillNo.ReadOnly = false;
                    }
                }
                //   txtpono.Focus();
            }
            else
            {
                MessageBox.Show("Please Select Sale type");
                cmbsaletype.Focus();
            }
        }
        DataTable qp = new DataTable();
        public void addisnal()
        {
            try
            {
                #region
                if (strfinalarray[0] == "S")
                {
                    qp = conn.getdataset("select * from Additional where MasterType='Sale'");
                }
                else if (strfinalarray[0] == "P")
                {
                    qp = conn.getdataset("select * from Additional where MasterType='Purchase'");
                }
                else if (strfinalarray[0] == "SR")
                {
                    qp = conn.getdataset("select * from Additional where MasterType='Sale Return'");
                }
                else if (strfinalarray[0] == "PR")
                {
                    qp = conn.getdataset("select * from Additional where MasterType='Purchase Return'");
                }
                if (qp.Rows.Count > 0)
                {
                    pnladditional.Visible = true;
                    if (qp.Rows[0]["nooffields"].ToString() == "1")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "2")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "3")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "4")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "5")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "6")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "7")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "8")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "9")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "10")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "11")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "12")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "13")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        lblf13.Visible = true;
                        txtf13.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                            lblf13.Text = qp.Rows[0]["field13"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "14")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        lblf13.Visible = true;
                        txtf13.Visible = true;
                        lblf14.Visible = true;
                        txtf14.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                            lblf13.Text = qp.Rows[0]["field13"].ToString();
                            lblf14.Text = qp.Rows[0]["field14"].ToString();
                        }
                    }
                    else if (qp.Rows[0]["nooffields"].ToString() == "15")
                    {
                        lblf1.Visible = true;
                        txtf1.Visible = true;
                        lblf2.Visible = true;
                        txtf2.Visible = true;
                        lblf3.Visible = true;
                        txtf3.Visible = true;
                        lblf4.Visible = true;
                        txtf4.Visible = true;
                        lblf5.Visible = true;
                        txtf5.Visible = true;
                        lblf6.Visible = true;
                        txtf6.Visible = true;
                        lblf7.Visible = true;
                        txtf7.Visible = true;
                        lblf8.Visible = true;
                        txtf8.Visible = true;
                        lblf9.Visible = true;
                        txtf9.Visible = true;
                        lblf10.Visible = true;
                        txtf10.Visible = true;
                        lblf11.Visible = true;
                        txtf11.Visible = true;
                        lblf12.Visible = true;
                        txtf12.Visible = true;
                        lblf13.Visible = true;
                        txtf13.Visible = true;
                        lblf14.Visible = true;
                        txtf14.Visible = true;
                        lblf15.Visible = true;
                        txtf15.Visible = true;
                        if (qp.Rows.Count > 0)
                        {
                            lblf1.Text = qp.Rows[0]["field1"].ToString();
                            lblf2.Text = qp.Rows[0]["field2"].ToString();
                            lblf3.Text = qp.Rows[0]["field3"].ToString();
                            lblf4.Text = qp.Rows[0]["field4"].ToString();
                            lblf5.Text = qp.Rows[0]["field5"].ToString();
                            lblf6.Text = qp.Rows[0]["field6"].ToString();
                            lblf7.Text = qp.Rows[0]["field7"].ToString();
                            lblf8.Text = qp.Rows[0]["field8"].ToString();
                            lblf9.Text = qp.Rows[0]["field9"].ToString();
                            lblf10.Text = qp.Rows[0]["field10"].ToString();
                            lblf11.Text = qp.Rows[0]["field11"].ToString();
                            lblf12.Text = qp.Rows[0]["field12"].ToString();
                            lblf13.Text = qp.Rows[0]["field13"].ToString();
                            lblf14.Text = qp.Rows[0]["field14"].ToString();
                            lblf15.Text = qp.Rows[0]["field15"].ToString();
                        }
                    }
                    txtf1.Focus();
                }
                else
                {
                    txtpono.Focus();
                }
                #endregion
            }
            catch
            {
            }
        }
        private void cmbsaletype_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true; // This will eliminate the beeping
                    bool inList = false;
                    for (int i = 0; i < cmbsaletype.Items.Count; i++)
                    {
                        s = cmbsaletype.GetItemText(cmbsaletype.Items[i]);
                        if (s == cmbsaletype.Text)
                        {
                            inList = true;
                            cmbsaletype.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbsaletype.Text = "";
                    }

                    bindbillno();


                    DataTable reqgst = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypeid='" + cmbsaletype.SelectedValue + "'");
                    DataTable gstno = conn.getdataset("select * from clientmaster where isactive=1 and ClientID='" + cmbcustname.SelectedValue + "'");
                    if (reqgst.Rows[0]["TaxCalculation"].ToString() == "Tax Invoice")
                    {
                        if (gstno.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(gstno.Rows[0]["statecode"].ToString()))
                            {
                                string companystatecode = conn.ExecuteScalar("select Statecode from Company where isactive=1 and CompanyID='" + Master.companyId + "'");
                                if (!string.IsNullOrEmpty(companystatecode))
                                {
                                    if (reqgst.Rows[0]["Region"].ToString() == "Local")
                                    {
                                        if (gstno.Rows[0]["statecode"].ToString().Trim() == companystatecode.Trim())
                                        {
                                        }
                                        else
                                        {
                                            MessageBox.Show("This Party is Out of State So Please Select Inter state Type ");
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (gstno.Rows[0]["statecode"].ToString().Trim() != companystatecode.Trim())
                                        {
                                        }
                                        else
                                        {
                                            MessageBox.Show("Your Party Is Local Please Create Local GST Bill");
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Enter Company State Code");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Enter Client State Code");
                                return;
                            }
                            if (gstno.Rows[0]["GstNo"].ToString() == "")
                            {
                                MessageBox.Show("You are Issuing Tax Invoice to a party whose GST NO is not Specified Kindly Check");
                                return;
                            }
                            //else
                            //{
                            //    DialogResult dr = MessageBox.Show("Do you want to Save?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            //    if (dr == DialogResult.Yes)
                            //    {
                            //        txtpono.Focus();
                            //    }
                            //}
                            txtpono.Focus();
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(gstno.Rows[0]["statecode"].ToString()))
                        {
                            string companystatecode = conn.ExecuteScalar("select Statecode from Company where isactive=1 and CompanyID='" + Master.companyId + "'");
                            if (!string.IsNullOrEmpty(companystatecode))
                            {
                                if (reqgst.Rows[0]["Region"].ToString() == "Local")
                                {
                                    if (gstno.Rows[0]["statecode"].ToString().Trim() == companystatecode.Trim())
                                    {
                                    }
                                    else
                                    {
                                        MessageBox.Show("This Party is Out of State So Please Select Inter state Type ");
                                        return;
                                    }
                                }
                                else
                                {
                                    if (gstno.Rows[0]["statecode"].ToString().Trim() != companystatecode.Trim())
                                    {
                                    }
                                    else
                                    {
                                        MessageBox.Show("Your Party Is Local Please Create Local GST Bill");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Enter Company State Code");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter Client State Code");
                            return;
                        }
                        if (gstno.Rows[0]["GstNo"].ToString() != "")
                        {
                            DialogResult dr = MessageBox.Show("You are Issuing Retail Invoice to Taxable person whose GST No.is specified Are you sure to continue?", "GST No.is specified", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                            {
                                txtpono.Focus();
                            }
                        }
                        else
                        {
                            txtpono.Focus();
                        }


                    }
                    addisnal();
                    saletype = cmbsaletype.Text;
                }
                if (e.KeyCode == Keys.F3)
                {
                    var privouscontroal = cmbsaletype;
                    activecontroal = privouscontroal.Name;
                    if (strfinalarray[0] == "S")
                    {
                        SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                        //pt.Show();
                        master.AddNewTab(pt);
                    }
                    else if (strfinalarray[0] == "P")
                    {
                        PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                        master.AddNewTab(p);
                    }
                    else if (strfinalarray[0] == "SR")
                    {
                        SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                        //pt.Show();
                        master.AddNewTab(pt);
                    }
                    else if (strfinalarray[0] == "PR")
                    {
                        PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                        master.AddNewTab(p);
                    }

                }
                if (e.KeyCode == Keys.F2)
                {
                    var privouscontroal = cmbsaletype;
                    activecontroal = privouscontroal.Name;
                    if (strfinalarray[0] == "S")
                    {
                        SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                        pt.updatemode("Sale", cmbsaletype.Text, "S");
                        // pt.Show();
                        master.AddNewTab(pt);
                    }
                    else if (strfinalarray[0] == "SR")
                    {
                        SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                        pt.updatemode("Sale", cmbsaletype.Text, "SR");
                        // pt.Show();
                        master.AddNewTab(pt);
                    }
                    else if (strfinalarray[0] == "P")
                    {
                        PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                        p.updatemode("Purchase", cmbsaletype.Text, "P");
                        // pt.Show();
                        master.AddNewTab(p);
                    }
                    else if (strfinalarray[0] == "PR")
                    {
                        PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                        p.updatemode("Purchase", cmbsaletype.Text, "PR");
                        // pt.Show();
                        master.AddNewTab(p);
                    }

                }

            }
        }

        private void txtpono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (strfinalarray[0] == "S" || strfinalarray[0] == "SR")
                {
                    if (Convert.ToBoolean(options.Rows[0]["showagentnameinsale"].ToString()) == true)
                    {
                        binaagent();
                        pnlagent.Visible = true;
                        cmbagentname.Focus();

                    }
                    else
                    {
                        this.ActiveControl = TxtBillNo;
                    }
                }
                else
                {
                    this.ActiveControl = TxtBillNo;
                }
            }
        }

        private void DefaultSale_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }

                if (e.Alt == true && e.KeyCode == Keys.S)
                {
                    BtnPayment.PerformClick();
                }
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (pnlcusdetails.Visible == true)
                {
                    pnlcusdetails.Visible = false;
                    txtitemname.Focus();
                    return true;
                }
                if (pnlserial.Visible == true)
                {
                    if (lvserial.Items.Count > Convert.ToInt64(txtbags.Text) && serialno != "NA")
                    {
                        MessageBox.Show("Serial no is More than Qty Please Remove");
                    }
                    else
                    {
                        pnlserial.Visible = false;
                        txtrate.Focus();
                    }
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        master.RemoveCurrentTab();
                    }
                }
                return true;
            }
            if (keyData == (Keys.Control | Keys.D1))
            {
                TAB1.SelectedIndex = 0;
            }
            if (keyData == (Keys.F9))
            {
                this.ActiveControl = txtitemname;
            }
            if (keyData == (Keys.Control | Keys.D2))
            {
                TAB1.SelectedIndex = 1;
            }
            if (keyData == (Keys.Alt | Keys.U))
            {
                //DialogResult dr = MessageBox.Show("Do you want to Update?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dr == DialogResult.Yes)
                //{
                btnsubmit();
                //}
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        int cnt = 0;
        private Ledger ledger;
        private Batch batch;
        string refno;
        DataTable dtsaleorder = new DataTable();
        public void getdata(string[] str, string[] p, int p_2, string[] strfinalarray, string[] str1)
        {
            try
            {
                //set the interval  and start the timer
                //   timer1.Interval = 1000;
                //   timer1.Start();
                int count = str1.Length;
                if (count == 1)
                {
                    txtpono.Text = str1[0];
                }
                cnt = 1;
                updateitem = str;
                #region
                //if (con.State == ConnectionState.Open)
                //{
                //    con.Close();
                //}
                //con.Open();
                //charges = 1;
                //this.StartPosition = FormStartPosition.Manual;
                //this.Location = new Point(0, 0);
                //TxtRundate.Text = DateTime.Now.ToString(Master.dateformate);
                //this.ActiveControl = TxtRundate;
                //LVFO.Columns.Add("Description of Goods", 196, HorizontalAlignment.Left);
                //LVFO.Columns.Add("Packing", 69, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Batch", 51, HorizontalAlignment.Center);
                //LVFO.Columns.Add("P.Qty", 53, HorizontalAlignment.Center);
                //LVFO.Columns.Add("A.Qty", 47, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Total Qty", 53, HorizontalAlignment.Right);
                //LVFO.Columns.Add("Free", 53, HorizontalAlignment.Center);
                //LVFO.Columns.Add(lblrate.Text, 64, HorizontalAlignment.Right);
                //LVFO.Columns.Add("Per", 42, HorizontalAlignment.Center);
                //LVFO.Columns.Add(lblamount.Text, 84, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Dis(%)", 44, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Dis Per", 63, HorizontalAlignment.Center);
                //LVFO.Columns.Add("TAX", 63, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Add Tax", 59, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Total", 108, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Sgstper", 0, HorizontalAlignment.Center);
                //LVFO.Columns.Add("Sgstamt", 0, HorizontalAlignment.Center);
                //LVFO.Columns.Add("cgstper", 0, HorizontalAlignment.Center);
                //LVFO.Columns.Add("cgstamt", 0, HorizontalAlignment.Center);
                //LVFO.Columns.Add("igstper", 0, HorizontalAlignment.Center);
                //LVFO.Columns.Add("igstamt", 0, HorizontalAlignment.Center);
                //LVFO.Columns.Add("addtaxper", 0, HorizontalAlignment.Center);
                //LVFO.Columns.Add("serial", 100, HorizontalAlignment.Center);


                //LVCHARGES.Columns.Add("Perticulars", 237, HorizontalAlignment.Left);
                //LVCHARGES.Columns.Add("Remarks", 215, HorizontalAlignment.Left);
                //LVCHARGES.Columns.Add("Value", 167, HorizontalAlignment.Left);
                //LVCHARGES.Columns.Add("@", 122, HorizontalAlignment.Left);
                //LVCHARGES.Columns.Add("+/-", 91, HorizontalAlignment.Left);
                //LVCHARGES.Columns.Add("Amount", 117, HorizontalAlignment.Right);
                //LVCHARGES.Columns.Add("valueofexp", 0, HorizontalAlignment.Right);
                //LVCHARGES.Columns.Add("tax", 0, HorizontalAlignment.Right);
                //LVCHARGES.Columns.Add("sgst", 0, HorizontalAlignment.Right);
                //LVCHARGES.Columns.Add("cgst", 0, HorizontalAlignment.Right);
                //LVCHARGES.Columns.Add("igst", 0, HorizontalAlignment.Right);
                //LVCHARGES.Columns.Add("additax", 0, HorizontalAlignment.Right);
                //lvserial.Columns.Add("", 1000, HorizontalAlignment.Right);
                //temptable = new DataTable();
                //temptable.Columns.Add("Itemname", typeof(string));
                //temptable.Columns.Add("SERIAL", typeof(string));


                //con.Close();
                #endregion
                dtsaleorder = new DataTable();
                refno = "";
                for (int u = 0; u < str.Count(); u++)
                {
                    refno += p[u] + ",";

                    SqlCommand cmd = new SqlCommand("select * from SaleOrderMaster where ClientID='" + p_2 + "' and billno='" + p[u] + "' and isactive=1 and billtype='" + strfinalarray[u] + "'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    string clientid = conn.ExecuteScalar("select ClientID from SaleOrderProductMaster where billno='" + p[u] + "' and billtype='" + strfinalarray[0] + "' and isactive=1");
                    if (string.IsNullOrEmpty(clientid))
                    {
                        SqlCommand cmd1 = new SqlCommand("select * from SaleOrderProductMaster where billno='" + p[u] + "' and billtype='" + strfinalarray[u] + "' and isactive=1", con);
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                        billorder = new DataTable();
                        sda1.Fill(billorder);
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("select * from SaleOrderProductMaster where ClientID='" + p_2 + "' and billno='" + p[u] + "' and billtype='" + strfinalarray[u] + "' and isactive=1", con);
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                        billorder = new DataTable();
                        sda1.Fill(billorder);
                    }
                    dt1 = billorder;
                    string charges = conn.ExecuteScalar("select ClientID from SaleOrderchargesmaster where billno='" + p[u] + "' and billtype='" + strfinalarray[0] + "' and isactive=1");
                    if (string.IsNullOrEmpty(charges))
                    {
                        billocharges = conn.getdataset("select * from SaleOrderchargesmaster where billno='" + p[u] + "' and billtype='" + strfinalarray[u] + "' and isactive=1");
                    }
                    else
                    {
                        billocharges = conn.getdataset("select * from SaleOrderchargesmaster where ClientID='" + p_2 + "' and billno='" + p[u] + "' and billtype='" + strfinalarray[u] + "' and isactive=1");
                    }
                    dt2 = billocharges;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    //  updateitem1 = conn.getdataset("Select * from SaleOrderProductMaster where  billno='" + str[u] + "'");
                    DataTable isorder = conn.getdataset("select * from BillProductMaster where isactive=1 and  refno like '%" + str[u] + "%' and isactive=1");
                    if (isorder.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            for (int j = 0; j < isorder.Rows.Count; j++)
                            {
                                if (dt1.Rows[i]["productname"].ToString().Replace(",", "") == isorder.Rows[j]["productname"].ToString().Replace(",", ""))
                                {
                                    dt1.Rows[i]["pqty"] = (Convert.ToDouble(dt1.Rows[i]["pqty"].ToString()) - Convert.ToDouble(isorder.Rows[j]["pqty"].ToString())).ToString();
                                    dt1.Rows[i]["qty"] = (Convert.ToDouble(dt1.Rows[i]["qty"].ToString()) - Convert.ToDouble(isorder.Rows[j]["qty"].ToString())).ToString();
                                    dt1.Rows[i]["bags"] = (Convert.ToDouble(dt1.Rows[i]["bags"].ToString()) - Convert.ToDouble(isorder.Rows[j]["bags"].ToString())).ToString();
                                    double total = (Convert.ToDouble(dt1.Rows[i]["pqty"].ToString()) * Convert.ToDouble(dt1.Rows[i]["rate"].ToString()));
                                    double tax = (total * Convert.ToDouble(dt1.Rows[i]["tax"].ToString())) / Convert.ToDouble(dt1.Rows[i]["total"].ToString());
                                    double addtax = (total * Convert.ToDouble(dt1.Rows[i]["addtax"].ToString())) / Convert.ToDouble(dt1.Rows[i]["total"].ToString());
                                    dt1.Rows[i]["total"] = total;
                                    dt1.Rows[i]["tax"] = tax;
                                    dt1.Rows[i]["amount"] = (Convert.ToDouble(dt1.Rows[i]["total"].ToString()) + Convert.ToDouble(isorder.Rows[j]["tax"].ToString())).ToString();
                                    dt1.Rows[i]["addtax"] = addtax;
                                    dt1.Rows[i]["sgstamt"] = tax / 2;
                                    dt1.Rows[i]["cgstamt"] = tax / 2;
                                    dt1.Rows[i]["igdtamt"] = tax;
                                }
                            }

                        }
                    }
                    if (u == 0)
                    {
                        dtsaleorder = dt1.Clone();
                    }

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(dt1.Rows[i]["pqty"].ToString()) > 0)
                        {
                            dtsaleorder.ImportRow(dt1.Rows[i]);
                            ListViewItem li;
                            string productname = conn.ExecuteScalar("select product_name from productmaster where isactive=1 and productid='" + dt1.Rows[i]["productid"].ToString() + "'");
                            //  li.SubItems.Add(productname);
                            li = LVFO.Items.Add(dt1.Rows[i]["Productname"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Packing"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["batch"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Bags"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Aqty"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Pqty"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["free"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Rate"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Per"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Total"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["discountper"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["discountamt"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Tax"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["addtax"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["Amount"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["sgstper"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["sgstamt"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["cgstper"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["cgstamt"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["igstper"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["igdtamt"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["addtaxper"].ToString());
                            string srno12 = dt1.Rows[i]["serialno"].ToString();
                            string[] array = srno12.Split(',');
                            string newserial = "";
                            if (strfinalarray[0] == "PC")
                            {
                                foreach (string s in array)
                                {
                                    //string srno = Regex.Replace(s, @"\s+", string.Empty);
                                    //srno.Trim();
                                    string srno = s.Trim();
                                    string checkP = conn.ExecuteScalar("select VoucherID from Serials where isactive=1 and ItemID='" + dt1.Rows[i]["productid"].ToString() + "' and SerialNo='" + srno + "' and TransactionID='P'");
                                    if (string.IsNullOrEmpty(checkP))
                                    {
                                        newserial += srno + ",";
                                        temptable.Rows.Add(dt1.Rows[i]["Productname"].ToString(), srno);
                                    }
                                }
                            }
                            else if (strfinalarray[0] == "SC")
                            {
                                foreach (string s in array)
                                {
                                    //string srno = Regex.Replace(s, @"\s+", string.Empty);
                                    string srno = s.Trim();
                                    string checkS = conn.ExecuteScalar("select VoucherID from Serials where isactive=1 and ItemID='" + dt1.Rows[i]["productid"].ToString() + "' and SerialNo='" + srno + "' and TransactionID='S'");
                                    if (string.IsNullOrEmpty(checkS))
                                    {
                                        newserial += srno + ",";
                                        temptable.Rows.Add(dt1.Rows[i]["Productname"].ToString(), srno);
                                    }
                                }
                            }
                            newserial = newserial.TrimEnd(',');
                            li.SubItems.Add(newserial);
                            li.SubItems.Add(dt1.Rows[i]["cess"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["productid"].ToString());
                            li.SubItems.Add(dt1.Rows[i]["boxsrno"].ToString());
                        }
                    }
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        ListViewItem li;
                        li = LVCHARGES.Items.Add(dt2.Rows[i]["perticulars"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["remarks"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["value"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["at"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["plusminus"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["amount"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["valueofexp"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["tax"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["sgst"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["cgst"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["igst"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["additax"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["addtaxamt"].ToString());
                        li.SubItems.Add(dt2.Rows[i]["billsundryid"].ToString());
                    }
                    if (strfinalarray[0] == "S")
                    {
                        if (Convert.ToBoolean(options.Rows[0]["itemsinasedingorderinsale"].ToString()) == true)
                        {
                            this.LVFO.ListViewItemSorter = new ListViewItemComparer(0);
                        }
                    }

                }
                refno = refno.TrimEnd(',');
                calculatetotalcharges();
                totalcalculation();
                clearitem();
                bindperticular();
                cmbsaletype.Focus();
                con.Close();
            }
            catch
            {
            }

        }
        DataTable billpro, billcharges, billorder, billocharges = new DataTable();
        public static string clientidupdate;
        internal void updatemode(string str, string p, int p_2, string[] strfinalarray)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (strfinalarray[0] == "S")
                {
                    txtheader.Text = "OUT WARD";
                    txttype.Text = "OUT WARD Type:";
                    this.Text = "OUT WARD";
                    if (Convert.ToBoolean(options.Rows[0]["showagentnameinsale"].ToString()) == true)
                    {
                        binaagent();
                        pnlagent.Visible = true;
                    }
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[0]["a"].ToString() == "False")
                        {
                            BtnPayment.Enabled = false;
                        }
                        if (userrights.Rows[0]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                }
                else if (strfinalarray[0] == "SR")
                {
                    txtheader.Text = "SALE RETURN";
                    txttype.Text = "Sale Type:";
                    this.Text = "Sale Return";
                    if (Convert.ToBoolean(options.Rows[0]["showagentnameinsale"].ToString()) == true)
                    {
                        binaagent();
                        pnlagent.Visible = true;
                    }
                    pnlorgbillno.Visible = true;
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[13]["a"].ToString() == "False")
                        {
                            BtnPayment.Enabled = false;
                        }
                        if (userrights.Rows[13]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                }
                else if (strfinalarray[0] == "P")
                {
                    txtheader.Text = "IN WARD";
                    txttype.Text = "IN WARD Type:";
                    this.Text = "IN WARD";
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[3]["a"].ToString() == "False")
                        {
                            BtnPayment.Enabled = false;
                        }
                        if (userrights.Rows[3]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                }

                else if (strfinalarray[0] == "PR")
                {
                    txtheader.Text = "PURCHASE RETURN";
                    txttype.Text = "Purchase Type:";
                    this.Text = "Purchase Return";
                    pnlorgbillno.Visible = true;
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[16]["a"].ToString() == "False")
                        {
                            BtnPayment.Enabled = false;
                        }
                        if (userrights.Rows[16]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                }
                //set the interval  and start the timer
                //timer1.Interval = 1000;
                //timer1.Start();
                if (isretrivesale == false)
                {
                    loadpage();
                }


                cnt = 1;
                if (isretrivesale != false)
                {
                    if (strfinalarray[0] == "SR")
                    {
                        strfinalarray[0] = "S";
                    }
                    else if (strfinalarray[0] == "PR")
                    {
                        strfinalarray[0] = "P";
                    }
                }
                SqlCommand cmd = new SqlCommand("select * from billmaster where ClientID='" + p_2 + "' and billno='" + p + "' and isactive=1 and billtype='" + strfinalarray[0] + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                txtorgbilldate.Text = Convert.ToDateTime(dt.Rows[0]["Bill_Date"].ToString()).ToString(Master.dateformate);
                string clientid = conn.ExecuteScalar("select ClientID from billproductmaster where billno='" + p + "' and billtype='" + strfinalarray[0] + "' and isactive=1");
                if (string.IsNullOrEmpty(clientid))
                {
                    SqlCommand cmd1 = new SqlCommand("select * from billproductmaster where billno='" + p + "' and billtype='" + strfinalarray[0] + "' and isactive=1", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    billpro = new DataTable();
                    sda1.Fill(billpro);
                    dtsaleorder.Merge(billpro);
                }
                else
                {
                    SqlCommand cmd1 = new SqlCommand("select * from billproductmaster where ClientID='" + p_2 + "' and billno='" + p + "' and billtype='" + strfinalarray[0] + "' and isactive=1", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    billpro = new DataTable();
                    sda1.Fill(billpro);
                    dtsaleorder.Merge(billpro);
                }
                dt1 = billpro;
                DataTable isorder = conn.getdataset("select * from BillProductMaster where  refno like '%" + str + "%' and isactive=1");
                if (isorder.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        for (int j = 0; j < isorder.Rows.Count; j++)
                        {
                            if (dt1.Rows[i]["productname"].ToString().Replace(",", "") == isorder.Rows[j]["productname"].ToString().Replace(",", ""))
                            {
                                dt1.Rows[i]["pqty"] = (Convert.ToDouble(dt1.Rows[i]["pqty"].ToString()) - Convert.ToDouble(isorder.Rows[j]["pqty"].ToString())).ToString();
                                dt1.Rows[i]["qty"] = (Convert.ToDouble(dt1.Rows[i]["qty"].ToString()) - Convert.ToDouble(isorder.Rows[j]["qty"].ToString())).ToString();
                                dt1.Rows[i]["bags"] = (Convert.ToDouble(dt1.Rows[i]["bags"].ToString()) - Convert.ToDouble(isorder.Rows[j]["bags"].ToString())).ToString();
                                double total = (Convert.ToDouble(dt1.Rows[i]["pqty"].ToString()) * Convert.ToDouble(dt1.Rows[i]["rate"].ToString()));
                                double tax = (total * Convert.ToDouble(dt1.Rows[i]["tax"].ToString())) / Convert.ToDouble(dt1.Rows[i]["total"].ToString());
                                double addtax = (total * Convert.ToDouble(dt1.Rows[i]["addtax"].ToString())) / Convert.ToDouble(dt1.Rows[i]["total"].ToString());
                                dt1.Rows[i]["total"] = total;
                                dt1.Rows[i]["tax"] = tax;
                                dt1.Rows[i]["amount"] = (Convert.ToDouble(dt1.Rows[i]["total"].ToString()) + Convert.ToDouble(isorder.Rows[j]["tax"].ToString())).ToString();
                                dt1.Rows[i]["addtax"] = addtax;
                                dt1.Rows[i]["sgstamt"] = tax / 2;
                                dt1.Rows[i]["cgstamt"] = tax / 2;
                                dt1.Rows[i]["igdtamt"] = tax;
                            }
                        }

                    }

                }
                refnoupdate = dt.Rows[0]["refno"].ToString();
                string charges = conn.ExecuteScalar("select ClientID from Billchargesmaster where billno='" + p + "' and billtype='" + strfinalarray[0] + "' and isactive=1");
                if (string.IsNullOrEmpty(charges))
                {
                    billcharges = conn.getdataset("select * from Billchargesmaster where billno='" + p + "' and billtype='" + strfinalarray[0] + "' and isactive=1");
                }
                else
                {
                    billcharges = conn.getdataset("select * from Billchargesmaster where ClientID='" + p_2 + "' and billno='" + p + "' and billtype='" + strfinalarray[0] + "' and isactive=1");
                }
                DataTable dt2 = billcharges;
                if (isretrivesale == false)
                {
                    // DataTable dt2 = conn.getdataset("select * from Billchargesmaster where ClientID='" + p_2 + "' and billno='" + p + "' and billtype='" + strfinalarray[0] + "' and isactive=1");
                    clientidupdate = dt.Rows[0]["ClientID"].ToString();
                    oldbillno = dt.Rows[0]["billno"].ToString();
                    lblid.Text = dt.Rows[0]["id"].ToString();
                    lblbill_no.Text = dt.Rows[0]["bill_no"].ToString();
                    TxtBillNo.Text = dt.Rows[0]["billno"].ToString();
                    try
                    {
                        TxtRundate.Text = Convert.ToDateTime(dt.Rows[0]["Bill_Date"].ToString()).ToString(Master.dateformate);
                        txtduedate.Text = Convert.ToDateTime(dt.Rows[0]["Duedate"].ToString()).ToString(Master.dateformate);
                    }
                    catch
                    {
                    }
                    cmbterms.Text = dt.Rows[0]["Terms"].ToString();
                    caseterms();
                    txtcustomername.Text = dt.Rows[0]["cusname"].ToString();
                    txtaddress.Text = dt.Rows[0]["cusadd"].ToString();
                    txtcity.Text = dt.Rows[0]["cuscity"].ToString();
                    txtphone.Text = dt.Rows[0]["cusphone"].ToString();
                    txtmobile.Text = dt.Rows[0]["cusmobile"].ToString();
                    txtpanno.Text = dt.Rows[0]["cuspancard"].ToString();
                    txtadharno.Text = dt.Rows[0]["cusadhar"].ToString();
                    txtf1.Text = dt.Rows[0]["OT1"].ToString();
                    txtf2.Text = dt.Rows[0]["OT2"].ToString();
                    txtf3.Text = dt.Rows[0]["OT3"].ToString();
                    txtf4.Text = dt.Rows[0]["OT4"].ToString();
                    txtf5.Text = dt.Rows[0]["OT5"].ToString();
                    txtf6.Text = dt.Rows[0]["OT6"].ToString();
                    txtf7.Text = dt.Rows[0]["OT7"].ToString();
                    txtf8.Text = dt.Rows[0]["OT8"].ToString();
                    txtf9.Text = dt.Rows[0]["OT9"].ToString();
                    txtf10.Text = dt.Rows[0]["OT10"].ToString();
                    txtf11.Text = dt.Rows[0]["OT11"].ToString();
                    txtf12.Text = dt.Rows[0]["OT12"].ToString();
                    txtf13.Text = dt.Rows[0]["OT13"].ToString();
                    txtf14.Text = dt.Rows[0]["OT14"].ToString();
                    txtf15.Text = dt.Rows[0]["OT15"].ToString();

                    cmd = new SqlCommand("select accountname from clientmaster where isactive=1 and clientid='" + dt.Rows[0]["ClientID"].ToString() + "' and isactive=1", con);
                    con.Open();
                    string clientname = cmd.ExecuteScalar().ToString();
                    //  cmbcustname.SelectedIndex = cmbcustname.Items.IndexOf(clientname);
                    cmbcustname.Text = clientname;
                }
                if (strfinalarray[0] == "S" || strfinalarray[0] == "SR")
                {
                    if (Convert.ToBoolean(options.Rows[0]["showagentnameinsale"].ToString()) == true)
                    {
                        DataTable agent = new DataTable();
                        agent = conn.getdataset("select accountname from clientmaster where isactive=1 and clientid='" + dt.Rows[0]["agentID"].ToString() + "' and isactive=1");
                        if (agent.Rows.Count > 0)
                        {
                            //  cmd = new SqlCommand("select accountname from clientmaster where isactive=1 and clientid='" + dt.Rows[0]["agentID"].ToString() + "' and isactive=1", con);
                            string agentname = agent.Rows[0]["accountname"].ToString();
                            cmbagentname.Text = agentname;
                            pnlagent.Visible = true;
                        }
                    }
                }
                if (isretrivesale == false)
                {
                    if (strfinalarray[0] == "SR" || strfinalarray[0] == "PR")
                    {
                        txtorgbilldate.Text = dt.Rows[0]["originalbilldate"].ToString();
                        txtorgbillno.Text = dt.Rows[0]["originalbillno"].ToString();
                    }
                    txtpono.Text = dt.Rows[0]["PO_No"].ToString();

                    cmd = new SqlCommand("select Purchasetypename from Purchasetypemaster where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypeid='" + dt.Rows[0]["SaleType"].ToString() + "'", con);
                    string saletypename = cmd.ExecuteScalar().ToString();
                    cmbsaletype.Text = saletypename;
                    saletype = cmbsaletype.Text;

                    //lbltotcount.Text = dt.Rows[0]["count"].ToString();
                    //lbltotpqty.Text = dt.Rows[0]["totalqty"].ToString();
                    //lblbasictot.Text = dt.Rows[0]["totalbasic"].ToString();
                    //txttottax.Text = dt.Rows[0]["totaltax"].ToString();
                    //TxtBillTotal.Text = dt.Rows[0]["totaltax"].ToString();

                    txtweight.Text = dt.Rows[0]["apprweight"].ToString();
                    txttransport.Text = dt.Rows[0]["dispatchdetails"].ToString();
                    if (txttransport.Text == "" || txttransport.Text.Length == 0)
                    {
                        DataTable dtClientAddress = conn.getdataset("select Address,City,State,Phone,Mobile from clientmaster where isactive=1 and clientid='" + clientidupdate + "' and isactive=1");
                        if (dtClientAddress.Rows.Count > 0)
                        {
                            txttransport.Text = dtClientAddress.Rows[0][0].ToString() + " " + dtClientAddress.Rows[0][1].ToString() + " " + dtClientAddress.Rows[0][2].ToString() + " " + "Ph:" + dtClientAddress.Rows[0][3].ToString() + "Mob:" + dtClientAddress.Rows[0][4].ToString()+"";
                        }
                       // clientidupdate;
                    }
                    else
                    {
                        txttransport.Text = dt.Rows[0]["dispatchdetails"].ToString();
                    }
                    txtremarks.Text = dt.Rows[0]["remarks"].ToString();
                    txtduedate.Text = Convert.ToDateTime(dt.Rows[0]["Duedate"].ToString()).ToString(Master.dateformate);
                    txtdelieveryat.Text = dt.Rows[0]["Delieveryat"].ToString();
                    txtfraight.Text = dt.Rows[0]["fraight"].ToString();
                    txtvehicleno.Text = dt.Rows[0]["vehicleno"].ToString();
                    txtgrrrno.Text = dt.Rows[0]["grrrno"].ToString();
                    txtskids.Text = dt.Rows[0]["noofskids"].ToString();
                    //   txtcess.Text = dt.Rows[0]["totalcess"].ToString();


                }

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li;
                    string productname = conn.ExecuteScalar("select product_name from productmaster where isactive=1 and productid='" + dt1.Rows[i]["productid"].ToString() + "'");
                    //  li.SubItems.Add(productname);
                    li = LVFO.Items.Add(dt1.Rows[i]["Productname"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Packing"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["batch"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Bags"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Aqty"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Pqty"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["free"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Rate"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Per"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Total"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["discountper"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["discountamt"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Tax"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["addtax"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["Amount"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["sgstper"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["sgstamt"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["cgstper"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["cgstamt"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["igstper"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["igdtamt"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["addtaxper"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["serialno"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["cess"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["productid"].ToString());
                    li.SubItems.Add(dt1.Rows[i]["boxsrno"].ToString());
                }
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = LVCHARGES.Items.Add(dt2.Rows[i]["perticulars"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["remarks"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["value"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["at"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["plusminus"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["amount"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["valueofexp"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["tax"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["sgst"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["cgst"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["igst"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["additax"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["addtaxamt"].ToString());
                    li.SubItems.Add(dt2.Rows[i]["billsundryid"].ToString());
                }
                if (strfinalarray[0] == "S")
                {
                    if (Convert.ToBoolean(options.Rows[0]["itemsinasedingorderinsale"].ToString()) == true)
                    {
                        this.LVFO.ListViewItemSorter = new ListViewItemComparer(0);
                    }
                }
                calculatetotalcharges();
                totalcalculation();
                clearitem();

                bindperticular();
                BtnPayment.Text = "Update";
                txtitemname.Focus();
                //  this.ActiveControl = txtitemname;
                con.Close();
            }
            catch
            {
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearall();
            clearitem();
            LVFO.Items.Clear();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {

            {
                try
                {
                    DialogResult dr1 = MessageBox.Show("Do you want to Delete Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr1 == DialogResult.Yes)
                    {
                        this.Enabled = false;
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        con.Open();
                        //pqty='" + updateitem1.Rows[0]["qty"].ToString() + "',qty='" + updateitem1.Rows[0]["qty"].ToString() + "'
                        SqlCommand cmd = new SqlCommand("update billmaster set isactive=0,Userid='"+master.CurrentUserid+"' where ClientID='" + clientidupdate + "' and billno='" + TxtBillNo.Text + "' and billtype='" + strfinalarray[0] + "'", con);
                        cmd.ExecuteNonQuery();
                        if (dtsaleorder.Rows.Count > 0)
                        {
                            string[] refno = dtsaleorder.Rows[0]["refno"].ToString().Split(',');
                            for (int i = 0; i < refno.Length; i++)
                            {
                                conn.execute("Update SaleOrderMaster SET OrderStatus='Pending',Userid='"+master.CurrentUserid+"' where isactive=1 and billno='" + refno[i] + "'");
                            }
                        }
                        //for (int i = 0; i < dtsaleorder.Rows.Count; i++)
                        //{
                        //    for (int j = 0; j < LVFO.Items.Count; j++)
                        //    {
                        //        if (LVFO.Items[j].SubItems[0].Text.Replace(",", "") == dtsaleorder.Rows[i]["productname"].ToString().Replace(",", ""))
                        //        {
                        //            DataTable isbill = conn.getdataset("select sum(pqty) from BillProductMaster where  productname like '%" + LVFO.Items[j].SubItems[0].Text.Replace(",", "") + "%' and isactive=1");
                        //            if (Convert.ToDouble(isbill.Rows[0][0].ToString()) >= Convert.ToDouble(dtsaleorder.Rows[i]["qty"].ToString().Replace(",", "")))
                        //            {
                        //                conn.execute("Update SaleOrderMaster SET OrderStatus='Pending' where ClientID='" + cmbcustname.SelectedValue + "' and billno'" + dtsaleorder.Rows[i]["refno"].ToString() + "'");
                        //            }
                        //            else
                        //            {
                        //                conn.execute("Update SaleOrderMaster SET OrderStatus='Clear' where ClientID='" + cmbcustname.SelectedValue + "' and billno='" + dtsaleorder.Rows[i]["refno"].ToString() + "'");
                        //            }
                        //        }
                        //    }
                        //}
                        SqlCommand cmd2 = new SqlCommand("update billproductmaster set isactive=0,Userid='"+master.CurrentUserid+"' where ClientID='" + clientidupdate + "' and billno='" + TxtBillNo.Text + "' and BillType='" + strfinalarray[0] + "'", con);
                        cmd2.ExecuteNonQuery();
                        conn.execute("update Billchargesmaster set isactive='0',Userid='"+master.CurrentUserid+"' where ClientID='" + clientidupdate + "' and  billno='" + TxtBillNo.Text + "' and billtype='" + strfinalarray[0] + "'");
                        conn.execute("update Serials set isactive='0',Userid='"+master.CurrentUserid+"' where PartyID='" + clientidupdate + "' and VoucherID='" + TxtBillNo.Text + "' and TransactionID='" + strfinalarray[0] + "'");
                        //clientidupdate
                        //SqlCommand cmd3 = new SqlCommand("update ledger set isactive=0 where voucherid='" + TxtBillNo.Text + "' and trantype='" + strfinalarray[2] + "'", con);
                        SqlCommand cmd3 = new SqlCommand("update ledger set isactive=0,Userid='"+master.CurrentUserid+"' where voucherid='" + TxtBillNo.Text + "' and AccountID='" + clientidupdate + "'", con);
                        cmd3.ExecuteNonQuery();
                        MessageBox.Show("Delete Successfully");
                        master.RemoveCurrentTab();
                    }
                }
                finally
                {
                    this.Enabled = true;
                }
            }
        }

        private void txtpacking_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txtpacking.Text))
                    {
                        txtpacking.Text = "1";
                    }
                    txtbags.Focus();
                }
            }
        }

        private void txtpacking_TextChanged(object sender, EventArgs e)
        {

            {
                if (txtpacking.Text != "")
                {

                }
            }
        }

        private void txtqty_TextChanged(object sender, EventArgs e)
        {

            {
                try
                {
                    //  if (qtyflag == 0)
                    // {
                    if (txtqty.Text != "")
                    {
                        if (txtbags.Text != "")
                        {
                            Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * Convert.ToDouble(txtqty.Text));
                            txtpqty.Text = qty.ToString();
                        }
                        else
                        {
                            Double qty = Convert.ToDouble(1 * Convert.ToDouble(txtqty.Text));
                            txtpqty.Text = qty.ToString();
                        }
                    }
                    else
                    {
                        if (txtbags.Text != "")
                        {
                            Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * 1);
                            txtpqty.Text = qty.ToString();
                        }
                        else
                        {
                            txtpqty.Text = "0.00";
                        }

                    }
                    if (txtpqty.Text != "" && txtpqty.Text != "0.00")
                    {
                        itemcalculation(txtpqty.Text);

                    }
                    else
                    {
                        itemcalculation("0");
                    }
                    //    qtyflag = 1;
                    //  }

                }
                catch
                {
                }
            }
        }

        private void txtqty_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtpqty.Focus();
                    try
                    {
                        if (qtyflag == 0)
                        {
                            if (txtqty.Text != "")
                            {
                                if (txtbags.Text != "")
                                {
                                    Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * Convert.ToDouble(txtqty.Text));
                                    txtpqty.Text = qty.ToString();
                                }
                                else
                                {
                                    Double qty = Convert.ToDouble(1 * Convert.ToDouble(txtqty.Text));
                                    txtpqty.Text = qty.ToString();
                                }
                            }
                            else
                            {
                                if (txtbags.Text != "")
                                {
                                    Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * 1);
                                    txtpqty.Text = qty.ToString();
                                }
                                else
                                {
                                    txtpqty.Text = "0.00";
                                }

                            }
                            if (txtpqty.Text != "" && txtpqty.Text != "0.00")
                            {
                                itemcalculation(txtpqty.Text);

                            }
                            else
                            {
                                itemcalculation("0");
                            }
                            qtyflag = 1;
                        }

                    }
                    catch
                    {
                    }
                }
            }
        }

        private void txtweight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtskids.Focus();
            }
        }
        private void txtdispatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtremarks.Focus();
            }
        }

        private void txtremarks_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtweight.Focus();
            }
        }

        private void txtbags_TextChanged(object sender, EventArgs e)
        {

            {
                try
                {
                    if (!txtpqty.Focused)
                    {
                        //if (qtyflag == 0)
                        //{
                        //    qtyflag = 1;
                        if (txtbags.Text != "")
                        {
                            if (txtqty.Text != "")
                            {
                                Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * Convert.ToDouble(txtqty.Text));
                                txtpqty.Text = qty.ToString();
                            }
                            else
                            {
                                Double qty = Convert.ToDouble(Convert.ToDouble(txtbags.Text) * 1);
                                txtpqty.Text = qty.ToString();
                            }
                        }
                        else
                        {
                            if (txtqty.Text != "")
                            {
                                Double qty = Convert.ToDouble(1 * Convert.ToDouble(txtqty.Text));
                                txtpqty.Text = qty.ToString();
                            }
                            else
                            {
                                txtpqty.Text = "0.00";
                            }

                        }
                        if (txtpqty.Text != "" && txtpqty.Text != "0.00")
                        {
                            itemcalculation(txtpqty.Text);

                        }
                        else
                        {
                            itemcalculation("0");
                        }
                    }
                    //qtyflag = 0;
                }
                catch
                {
                }
            }
        }

        private void txtbags_KeyPress(object sender, KeyPressEventArgs e)
        {

            {

                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {

            {
                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtpqty_KeyPress(object sender, KeyPressEventArgs e)
        {

            {
                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {

            {
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    master.RemoveCurrentTab();
                }
            }
        }

        private void TxtBillNo_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!string.IsNullOrEmpty(TxtBillNo.Text))
                    {
                        string account = cmbcustname.Text.ToUpper();
                        if (cmbterms.Text == "Cash" && account == "CASH")
                        {
                            if (strfinalarray[0] == "S" || strfinalarray[0] == "P")
                            {
                                pnlcusdetails.Visible = true;
                                txtcustomername.Focus();
                                this.ActiveControl = txtcustomername;
                            }
                            else
                            {
                                if (strfinalarray[0] == "SR" || strfinalarray[0] == "PR")
                                {
                                    pnlorgbillno.Visible = true;
                                    txtorgbillno.Focus();
                                }
                                else
                                {
                                    txtitemname.Focus();
                                    this.ActiveControl = txtitemname;

                                }
                            }
                        }
                        else
                        {
                            if (strfinalarray[0] == "SR" || strfinalarray[0] == "PR")
                            {
                                pnlorgbillno.Visible = true;
                                txtorgbillno.Focus();
                            }
                            else
                            {
                                txtitemname.Focus();
                                this.ActiveControl = txtitemname;

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter Bill No");
                        TxtBillNo.Focus();
                    }
                    
                }
            }
        }

        private void TxtRundate_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbterms.Focus();
                    // this.cmbterms.BackColor = System.Drawing.Color.Red;
                }
            }
        }

        private void LVFO_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    {
                        if (LVFO.SelectedItems.Count > 0)
                        {

                            rowid = LVFO.FocusedItem.Index;
                            lbliteminfo.Visible = true;

                            string dt11 = conn.ExecuteScalar("select Product_Name from ProductMaster where isactive=1 and ProductID='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "'");
                            txtitemname.Text = dt11;
                            //bindbatch();
                            cmbbatch.Enabled = true;
                            dt8 = new DataTable();
                            SqlCommand cmd1 = new SqlCommand("select Productid,Batchno from ProductPriceMaster where Productid='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "' and isactive='1'", con);
                            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                            sda1.Fill(dt8);
                            cmbbatch.ValueMember = "Productid";
                            cmbbatch.DisplayMember = "Batchno";
                            cmbbatch.DataSource = dt8;
                            cmbbatch.Focus();
                            cmbbatch.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text;
                            if (LVFO.Items[LVFO.FocusedItem.Index].SubItems[2].Text == "")
                            {
                                cmbbatch.Enabled = false;
                                cmbbatch.SelectedIndex = -1;
                            }
                            else
                            {
                                cmbbatch.Enabled = true;

                            }

                            txtpacking.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[1].Text;
                            txtbags.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[3].Text;
                            txtqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[4].Text;
                            txtpqty.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text;
                            txtfree.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[6].Text;
                            txtrate.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[7].Text;
                            txtper.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[8].Text;
                            txttotal.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[9].Text;
                            txtdisper.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[10].Text;
                            txtdisamt.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[11].Text;
                            txttax.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[12].Text;
                            txtaddtax.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[13].Text;
                            txtamount.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[14].Text;
                            string serial = LVFO.Items[LVFO.FocusedItem.Index].SubItems[22].Text;
                            itemproductid = LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text;
                            txtboxsrno.Text = LVFO.Items[LVFO.FocusedItem.Index].SubItems[25].Text;
                            LVFO.Items[LVFO.FocusedItem.Index].SubItems[22].Text = "";
                            if (!string.IsNullOrEmpty(serial))
                            {
                                string[] values = serial.Split(',');
                                //  temptable = new DataTable();
                                // temptable.Columns.Add("Itemname", typeof(string));
                                //  temptable.Columns.Add("SERIAL", typeof(string));
                                for (int i = 0; i < values.Length; i++)
                                {
                                    ListViewItem li = new ListViewItem();
                                    li = lvserial.Items.Add(values[i] + Environment.NewLine);
                                    temptable.Rows.Add(txtitemname.Text, values[i]);
                                }
                            }
                            SqlCommand cmd5 = new SqlCommand("select p.*,b.* from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.isactive=1 and b.isactive=1 and p.ProductID='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "'", con);
                            SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            lblbagqty.Text = "[" + dt.Rows[0]["Unit"].ToString() + "]";
                            lblaltqty.Text = "[" + dt.Rows[0]["Altunit"].ToString() + "]";

                            SqlCommand cmd6 = new SqlCommand("select * from TaxSlabMaster i inner join productmaster p on i.Taxslabname=p.taxslab where i.isactive=1 and p.isactive=1 and p.ProductID='" + LVFO.Items[LVFO.FocusedItem.Index].SubItems[24].Text + "' and i.saletypename='" + cmbsaletype.Text + "'", con);
                            SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                            DataTable dt1 = new DataTable();
                            sda6.Fill(dt1);
                            if (dt1.Rows.Count > 0)
                            {

                                lbltax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["igst"].ToString()), 2).ToString() + "]";
                                taxforprice = lbltax1.Text;
                                lbladdtax1.Text = "[" + Math.Round(Convert.ToDouble(dt1.Rows[0]["additax"].ToString()), 2).ToString() + "]";
                                ataxforprice = lbladdtax1.Text;
                                txtbags.Focus();
                                taxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["igst"].ToString()), 2);
                                addtaxid = Math.Round(Convert.ToDouble(dt1.Rows[0]["additax"].ToString()), 2);

                            }
                            else
                            {

                                //  MessageBox.Show("Not any Tax Available For This Sale Type");

                                lbltax1.Text = "[0]";
                                taxforprice = lbltax1.Text;
                                lbladdtax1.Text = "[0]";
                                ataxforprice = lbladdtax1.Text;
                                taxid = 0;
                                addtaxid = 0;
                                txtbags.Focus();
                            }

                            Double total = 0;
                            try
                            {
                                total = Math.Round((Convert.ToDouble(TxtBillTotal.Text) - Convert.ToDouble(LVFO.Items[LVFO.FocusedItem.Index].SubItems[5].Text)), 2);
                            }
                            catch
                            {
                            }
                            TxtBillTotal.Text = total.ToString();
                            //      LVFO.Items[LVFO.FocusedItem.Index].Remove();
                            totalcalculation();

                        }
                    }
                }
                if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr1 = MessageBox.Show("Do you want to Remove Item?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr1 == DialogResult.Yes)
                    {

                        string serial = LVFO.Items[LVFO.FocusedItem.Index].SubItems[22].Text;
                        LVFO.Items[LVFO.FocusedItem.Index].SubItems[22].Text = "";
                        if (!string.IsNullOrEmpty(serial))
                        {
                            string[] values = serial.Split(',');
                            //  temptable = new DataTable();
                            // temptable.Columns.Add("Itemname", typeof(string));
                            //  temptable.Columns.Add("SERIAL", typeof(string));
                            for (int i = 0; i < values.Length; i++)
                            {
                                for (int j = 0; j < temptable.Rows.Count; j++)
                                {
                                    if (temptable.Rows[j]["SERIAL"].ToString() == values[i])
                                    {
                                        temptable.Rows[j].Delete();
                                    }
                                }
                            }
                        }
                        LVFO.Items[LVFO.FocusedItem.Index].Remove();
                        temptable.AcceptChanges();
                        totalcalculation();
                    }
                }

            }
        }

        private void cmbbatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //if (cmbbatch.SelectedIndex == 0)
                //{
                //    MessageBox.Show("Pls Select Batch");
                //    return;
                //}
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbbatch.Items.Count; i++)
                {
                    s = cmbbatch.GetItemText(cmbbatch.Items[i]);
                    if (s == cmbbatch.Text)
                    {
                        inList = true;
                        cmbbatch.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbbatch.Text = "";
                }

                txtbags.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbbatch;
                activecontroal = privouscontroal.Name;
                Batch b = new Batch(strfinalarray, this, activecontroal);
                b.ShowDialog();




            }
        }
        int cessflag = 0;
        private void txttotal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txttotal.Text))
                {
                    txttotal.Text = "0";
                }
                //DataTable cess = conn.getdataset("select cessper,cessamt from ProductMaster where isactive=1 and Product_Name='" + txtitemname.Text + "'");
                //if (cess.Rows.Count > 0)
                //{

                //    if (Convert.ToDouble(cess.Rows[0]["cessper"].ToString()) > 0 && Convert.ToDouble(cess.Rows[0]["cessamt"].ToString()) > 0 && cessflag==0)
                //    {
                //        cess c = new cess(cess.Rows[0]["cessper"].ToString(), cess.Rows[0]["cessamt"].ToString(), txttotal, txtcess,txtbags,txtdisamt);
                //        c.ShowDialog();
                //        cessflag = 1;
                //    }
                //    else
                //    {
                //        txtdisper.Focus();
                //    }
                //}
                //else
                //{
                txtdisper.Focus();
                //}
            }
        }

        private void txttotal_TextChanged(object sender, EventArgs e)
        {

            {
                if (txttotal.Text != "" && y == 0)
                {
                    try
                    {
                        double discount = 0;
                        double amt = Convert.ToDouble(txttotal.Text) / Convert.ToDouble(txtpqty.Text);
                        txtrate.Text = Math.Round(amt, 2).ToString();
                        if (!string.IsNullOrEmpty(txtdisamt.Text))
                        {
                            discount = Convert.ToDouble(txttotal.Text) - Convert.ToDouble(txtdisamt.Text);
                        }
                        double amount = Math.Round(discount + ((discount * (Convert.ToDouble(taxid.ToString()) + Convert.ToDouble(addtaxid.ToString()))) / 100), 2);
                        txtamount.Text = Math.Round(amount, 2).ToString();
                        string istax = conn.ExecuteScalar("Select taxtypename from Purchasetypemaster where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypename='" + cmbsaletype.Text + "'");
                        if (istax != "Tax Free")
                        {
                            double tax = Math.Round(discount * (Convert.ToDouble(taxid.ToString())) / 100, 2);
                            double addtax = Math.Round(discount * (Convert.ToDouble(addtaxid.ToString())) / 100, 2);
                            txttax.Text = tax.ToString();
                            txtaddtax.Text = addtax.ToString();
                        }
                        else
                        {
                            txttax.Text = "0";
                            txtaddtax.Text = "0";
                            taxid = 0;
                            addtaxid = 0;

                        }
                        //txttax.Text = tax.ToString();
                        //txtaddtax.Text = addtax.ToString();
                    }
                    catch
                    {
                    }

                }
            }
        }

        private void txtdisper_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtdisper.Text != "" && p == 0 && Convert.ToDouble(txtdisper.Text) > 0)
                {
                    double amt = (Convert.ToDouble(txttotal.Text) * Convert.ToDouble(txtdisper.Text)) / 100;
                    txtdisamt.Text = Math.Round(amt, 2).ToString();
                    itemcalculation(txtpqty.Text);
                }
            }
            catch
            {
            }
        }

        private void txtdisper_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txtdisper.Text))
                    {
                        txtdisper.Text = "0";
                    }
                    txtdisamt.Focus();
                    if (options.Rows[0]["itemspeed"].ToString() == "Qty +Free Only")
                    {
                        // enteramount();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Rate +Discount(%) Only")
                    {
                        enteramount();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate + Discount(%) Only ")
                    {
                        enteramount();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate Only")
                    {
                        // enteramount();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty +Rate Only")
                    {
                        // txtrate.Focus();
                    }
                    else if (options.Rows[0]["itemspeed"].ToString() == "Qty Only")
                    {
                        // enteramount();
                    }
                }
            }
        }
        int p, q;
        private void txtdisper_KeyPress(object sender, KeyPressEventArgs e)
        {
            {

                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            {
                p = 0;
                q = 1;
            }
        }

        private void txtfree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtfree.Text))
                {
                    txtfree.Text = "0";
                }

                if (options.Rows[0]["itemspeed"].ToString() == "Qty +Free Only")
                {
                    enteramount();
                    return;
                }
                else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Rate +Discount(%) Only")
                {
                    txtrate.Focus();
                }
                else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate + Discount(%) Only")
                {
                    txtrate.Focus();
                }
                else if (options.Rows[0]["itemspeed"].ToString() == "Qty + Free + Rate Only")
                {
                    txtrate.Focus();
                }
                else if (options.Rows[0]["itemspeed"].ToString() == "Qty +Rate Only")
                {
                    txtrate.Focus();
                }
                DataTable serial = new DataTable();
                serial = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + txtitemname.Text + "'");
                if (serial.Rows[0]["isserial"].ToString() == "True")
                {
                    pnlserial.Visible = true;
                    txtserial.Focus();
                }
                else
                {
                    x = 0;
                    txtrate.Focus();
                }
                DataTable dt = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypeid='" + cmbsaletype.SelectedValue + "'");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["TaxTypename"].ToString() == "Tax Inclusive")
                    {
                        if (txtrate.Text == "")
                        {
                            txtrate.Text = "0";
                        }
                        Price p = new Price(txtrate, txtamount, strfinalarray);
                        p.ShowDialog();
                    }
                }

            }
        }

        private void txtfree_KeyPress(object sender, KeyPressEventArgs e)
        {
            {

                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtdisamt_TextChanged(object sender, EventArgs e)
        {

            {
                if ((txtdisamt.Text != "" && q == 0) && Convert.ToDouble(txtdisamt.Text) > 0)
                {
                    if (Convert.ToDouble(txttotal.Text) == 0)
                    {
                        txtdisper.Text = Math.Round(0.00, 2).ToString();
                    }
                    else
                    {
                        double amt = (100 * Convert.ToDouble(txtdisamt.Text)) / Convert.ToDouble(txttotal.Text);
                        txtdisper.Text = Math.Round(amt, 2).ToString();
                    }

                    itemcalculation(txtpqty.Text);
                }
            }
        }

        private void txtdisamt_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txtdisamt.Text))
                    {
                        txtdisamt.Text = "0";
                    }
                    DataTable cess = conn.getdataset("select cessper,cessamt from ProductMaster where isactive=1 and Product_Name='" + txtitemname.Text + "'");
                    if (cess.Rows.Count > 0)
                    {

                        if (Convert.ToDouble(cess.Rows[0]["cessper"].ToString()) >= 0 && Convert.ToDouble(cess.Rows[0]["cessamt"].ToString()) >= 0 && cessflag == 0)
                        {
                            if (Convert.ToBoolean(options.Rows[0]["cess"].ToString()) == true)
                            {
                                cess c = new cess(cess.Rows[0]["cessper"].ToString(), cess.Rows[0]["cessamt"].ToString(), txttotal, txtcess, txtbags, txtdisamt);
                                c.ShowDialog();
                                cessflag = 1;
                            }
                            else
                            {
                                txtamount.Focus();
                            }
                        }
                        else
                        {
                            double tamt = Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtcess.Text);
                            txtamount.Text = Convert.ToString(tamt);
                            txtamount.Focus();

                        }
                    }
                    else
                    {
                        txtamount.Focus();
                    }
                }
            }
        }

        private void txtdisamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            {

                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            {
                p = 1;
                q = 0;
            }
        }

        private void txtrate_KeyPress(object sender, KeyPressEventArgs e)
        {

            {
                x = 0;
                y = 1;
            }
            {

                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txttotal_Validated(object sender, EventArgs e)
        {

        }

        private void txttotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            {

                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            {
                x = 1;
                y = 0;
            }
        }

        private void txtcharremark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                DataTable dt = conn.getdataset("select * from billsundry where isactive=1 and billsundryid='" + cmbcharper.SelectedValue + "'");
                if (dt.Rows[0]["symbol"].ToString() == "%")
                {
                    txtcharval.Focus();
                }
                else
                {
                    if (dt.Rows[0]["OT3"].ToString() == "1")
                    {
                        ChargesPnl cp = new ChargesPnl(txtcharamt, this, strfinalarray);
                        cp.ShowDialog();
                        txtcharamt.Focus();
                    }
                    else
                    {
                        if (txtcharval.Text != "0")
                        {
                            txtcharval.Focus();
                        }
                        else
                        {
                            txtcharamt.Focus();
                        }
                    }
                }

            }
        }

        private void cmbcharper_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {
                        e.SuppressKeyPress = true; // This will eliminate the beeping
                        bool inList = false;
                        for (int i = 0; i < cmbcharper.Items.Count; i++)
                        {
                            s = cmbcharper.GetItemText(cmbcharper.Items[i]);
                            if (s == cmbcharper.Text)
                            {
                                inList = true;
                                cmbcharper.Text = s;
                                break;
                            }
                        }
                        if (!inList)
                        {
                            cmbcharper.Text = "";
                        }

                        if (charges != 1)
                        {
                            if (cmbcharper.Text != "")
                            {

                                DataTable dt = conn.getdataset("select * from billsundry where isactive=1 and billsundryid='" + cmbcharper.SelectedValue + "'");
                                if (dt.Rows[0]["symbol"].ToString() == "%")
                                {

                                    if (dt.Rows[0]["applyon"].ToString() == "Net")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(TxtBillTotal.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                    else if (dt.Rows[0]["applyon"].ToString() == "Basic Amount")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(lblbasictot.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                    else if (dt.Rows[0]["applyon"].ToString() == "Items Total")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(lbltotpqty.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                    else if (dt.Rows[0]["applyon"].ToString() == "Taxable Amount")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(lblbasictot.Text) - Convert.ToDouble(txttotdiscount.Text) - Convert.ToDouble(txttotadis.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                    else if (dt.Rows[0]["applyon"].ToString() == "Tax Amount")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(txttottax.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                    else if (dt.Rows[0]["applyon"].ToString() == "Tax + AddTax")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(txttottax.Text) + Convert.ToDouble(txttotaddvat.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                    else if (dt.Rows[0]["applyon"].ToString() == "Service Charges")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(txttotservice.Text) + Convert.ToDouble(txttotalcharges.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                    else if (dt.Rows[0]["applyon"].ToString() == "Auto Round Off")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(txtroundoff.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                    else if (dt.Rows[0]["applyon"].ToString() == "Amount")
                                    {
                                        txtcharval.Enabled = true;
                                        txtcharat.Enabled = true;
                                        double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(txtamt.Text)) / 100;
                                        txtcharval.Text = Math.Round(value, 2).ToString();
                                        txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                    }
                                }
                                else
                                {
                                    txtcharval.Text = "0";
                                    //  txtcharval.Focus();
                                    // txtcharval.Enabled = false;
                                    // txtcharat.Enabled = false;
                                }

                                txtcharplusminus.Text = dt.Rows[0]["BillSundryType"].ToString();
                                txtcharamt.Text = Math.Round(Convert.ToDouble(txtcharval.Text) * (Convert.ToDouble(txtcharat.Text) / 100), 2).ToString();
                                //txtcharremark.Focus();
                            }
                        }

                    }
                    catch
                    {
                    }
                    txtcharremark.Focus();
                }
                if (e.KeyCode == Keys.F3)
                {
                    var privouscontroal = cmbcharper;
                    activecontroal = privouscontroal.Name;
                    ChargesHead popup = new ChargesHead(this, cmbcharper.Text, master, tabControl, activecontroal);
                    master.AddNewTab(popup);
                    // popup.ShowDialog();

                    //    string userEnteredText = popup.EnteredText;

                    //  popup.Dispose();

                }
                if (e.KeyCode == Keys.F2)
                {
                    var privouscontroal = cmbcharper;
                    activecontroal = privouscontroal.Name;
                    ChargesHead popup = new ChargesHead(this, cmbcharper.Text, master, tabControl, activecontroal);
                    popup.Update(1, cmbcharper.Text);
                    master.AddNewTab(popup);
                }

            }
        }
        public void bindperticular()
        {
            try
            {
                DataTable dt = conn.getdataset("select billsundryid,billsundryname from billsundry where isactive=1");


                cmbcharper.ValueMember = "billsundryid";
                cmbcharper.DisplayMember = "billsundryname";
                cmbcharper.DataSource = dt;
                cmbcharper.SelectedIndex = -1;
                charges = 0;
                //  autobind(dt, cmbcharper);
            }
            catch
            {
            }

        }
        int charges;
        private DateWiseReport dateWiseReport;
        private CashBook cashBook;
        private Master master;
        private TabControl tabControl;
        private void cmbcharper_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbcharper.Items.Count; i++)
                {
                    s = cmbcharper.GetItemText(cmbcharper.Items[i]);
                    if (s == cmbcharper.Text)
                    {
                        inList = true;
                        cmbcharper.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcharper.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }
        private void addchargesdatadata()
        {
            DataTable dt = conn.getdataset("select * from billsundry where isactive=1 and billsundryid='" + cmbcharper.SelectedValue + "'");


        }

        private void txtcharval_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtcharat.Focus();
                }
            }
        }

        private void txtcharval_TextChanged(object sender, EventArgs e)
        {

            {
                if (txtcharval.Text != "")
                {
                    chargescalculator();
                }
            }
        }
        private void chargescalculator()
        {
            if (txtcharat.Text == "")
            {
                txtcharat.Text = "0";
            }
            txtcharamt.Text = Math.Round(Convert.ToDouble(txtcharval.Text) * (Convert.ToDouble(txtcharat.Text) / 100), 2).ToString();
        }

        private void txtcharat_TextChanged(object sender, EventArgs e)
        {

            {
                if (txtcharat.Text != "")
                {
                    chargescalculator();
                }
            }
        }

        private void txtcharplusminus_TextChanged(object sender, EventArgs e)
        {

            {

            }
        }

        private void txtcharamt_TextChanged(object sender, EventArgs e)
        {

            {

            }
        }

        private void txtcharamt_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    btncharadditem.Focus();
                }
            }
        }

        private void txtcharat_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataTable dt = conn.getdataset("select * from billsundry where isactive=1 and billsundryid='" + cmbcharper.SelectedValue + "'");
                    if (dt.Rows[0]["OT3"].ToString() == "1")
                    {
                        double val = Convert.ToDouble(txtcharval.Text);
                        double per = Convert.ToDouble(txtcharat.Text);
                        double totalval = val * per / 100;
                        taxvalue = Convert.ToString(totalval);
                        ChargesPnl cp = new ChargesPnl(txtcharamt, this, strfinalarray);
                        cp.ShowDialog();
                        txtcharamt.Focus();
                    }
                    else
                    {
                        txtcharamt.Focus();
                    }
                }
            }
        }

        private void btncharadditem_Click(object sender, EventArgs e)
        {

            {
                try
                {
                    ListViewItem li;
                    lbliteminfo.Visible = false;
                    li = LVCHARGES.Items.Add(cmbcharper.Text);
                    li.SubItems.Add(txtcharremark.Text);
                    li.SubItems.Add(txtcharval.Text);
                    li.SubItems.Add(txtcharat.Text);
                    li.SubItems.Add(txtcharplusminus.Text);
                    li.SubItems.Add((Math.Round(Convert.ToDouble(txtcharamt.Text), 2).ToString()));
                    DataTable dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbsaletype.Text + "'");
                    string charge = conn.ExecuteScalar("select BillSundryID from BillSundry where isactive=1 and BillSundryName='" + cmbcharper.Text + "'");
                    if (dt.Rows[0]["Region"].ToString() == "Local")
                    {
                        if (ChargesPnl.valofexp == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.valofexp);
                        }
                        if (ChargesPnl.tax1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.tax1);
                        }
                        if (ChargesPnl.sgst1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.sgst1);
                        }
                        if (ChargesPnl.cgst1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.cgst1);
                        }
                        if (ChargesPnl.igst1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.igst1);
                        }
                        if (ChargesPnl.additax == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.additax);
                        }
                        if (ChargesPnl.additaxamt == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.additaxamt);
                        }
                        li.SubItems.Add(charge);
                    }
                    else
                    {
                        if (ChargesPnl.valofexp == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.valofexp);
                        }
                        if (ChargesPnl.tax1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.tax1);
                        }
                        if (ChargesPnl.sgst1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.sgst1);
                        }
                        if (ChargesPnl.cgst1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.cgst1);
                        }
                        if (ChargesPnl.igst1 == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.igst1);
                        }
                        if (ChargesPnl.additax == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.additax);
                        }
                        if (ChargesPnl.additaxamt == "")
                        {
                            li.SubItems.Add("0");
                        }
                        else
                        {
                            li.SubItems.Add(ChargesPnl.additaxamt);
                        }
                        li.SubItems.Add(charge);
                    }



                    calculatetotalcharges();

                    totalcalculation();
                    clearcharitem();
                    cmbcharper.Focus();
                }
                catch
                {
                }
            }
        }
        private void calculatetotalcharges()
        {
            Double charges = 0;
            for (int i = 0; i < LVCHARGES.Items.Count; i++)
            {
                if (LVCHARGES.Items[i].SubItems[4].Text == "+")
                {
                    string str = LVCHARGES.Items[i].SubItems[5].Text;
                    //  totalcalculation();

                    charges += Convert.ToDouble(LVCHARGES.Items[i].SubItems[5].Text);
                }
                if (LVCHARGES.Items[i].SubItems[4].Text == "-")
                {
                    string str = LVCHARGES.Items[i].SubItems[5].Text;
                    charges -= Convert.ToDouble(LVCHARGES.Items[i].SubItems[5].Text);
                }

            }
            txttotalcharges.Text = Math.Round(charges, 2).ToString();
        }

        private void clearcharitem()
        {
            cmbcharper.Text = "";
            txtcharamt.Text = "";
            txtcharat.Text = "";
            txtcharremark.Text = "";
            txtcharval.Text = "";
            txtcharplusminus.Text = "";
            ChargesPnl.valofexp = "";
            ChargesPnl.tax1 = "";
            ChargesPnl.sgst1 = "";
            ChargesPnl.cgst1 = "";
            ChargesPnl.igst1 = "";
            ChargesPnl.additax = "";
            lvfvalue = "";

        }

        private void txtcharval_KeyPress(object sender, KeyPressEventArgs e)
        {

            {

                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtcharat_KeyPress(object sender, KeyPressEventArgs e)
        {

            {

                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtcharamt_KeyPress(object sender, KeyPressEventArgs e)
        {

            {

                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
        public static string lvfvalue, lvftax, lvfsgst, lvfcgst, lvfigst, lvfaddtax, lvfaddtaxamt;
        public void open()
        {
            try
            {
                if (LVCHARGES.SelectedItems.Count > 0)
                {
                    cmbcharper.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[0].Text;
                    txtcharremark.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[1].Text;
                    txtcharval.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[2].Text;
                    txtcharat.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[3].Text;
                    txtcharplusminus.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[4].Text;
                    txtcharamt.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[5].Text;
                    lvfvalue = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[6].Text;
                    lvftax = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[7].Text;
                    lvfsgst = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[8].Text;
                    lvfcgst = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[9].Text;
                    lvfigst = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[10].Text;
                    lvfaddtax = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[11].Text;
                    lvfaddtaxamt = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[12].Text;
                    LVCHARGES.Items[LVCHARGES.FocusedItem.Index].Remove();
                    calculatetotalcharges();
                    totalcalculation();
                    cmbcharper.Focus();

                }
            }
            catch
            {
            }
        }
        private void LVCHARGES_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            open();
        }

        private void LVCHARGES_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    open();
                }
                if (e.KeyCode == Keys.Delete)
                {
                    LVCHARGES.Items[LVCHARGES.FocusedItem.Index].Remove();
                    calculatetotalcharges();
                    totalcalculation();
                }
            }
        }

        private void txttransport_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtdelieveryat.Focus();
                }
            }
        }

        private void txtdelieveryat_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtfraight.Focus();
                }
            }
        }

        private void txtfraight_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtvehicleno.Focus();
                }
            }
        }

        private void txtvehicleno_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtgrrrno.Focus();
                }
            }
        }

        private void txtgrrrno_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtremarks.Focus();
            }
        }

        private void txtskids_KeyDown(object sender, KeyEventArgs e)
        {

            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnPayment.Focus();
                }
            }
        }

        private void txtduedate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbcustname.Focus();
            }
        }
        double sgstper, sgstamt, cgstper, cgstamt, igstper, igstamt, tax, addtaxper;
        private string[] strfinalarray;
        private ItemWiseStock itemWiseStock;
        string serialno;
        private SaleOrder saleOrder;
        private string p_2;
        private serialnotracking serialnotracking;

        public void enteramount()
        {
            try
            {

                if (Convert.ToBoolean(options.Rows[0]["chkRestrictstockinsalewhennostock"].ToString()) == true && strfinalarray[0] == "S")
                {
                    if (Convert.ToDouble(currentstock) > 0)
                    {
                        enteramt();
                    }
                    else
                    {
                        MessageBox.Show("There is no stock in this item");
                    }
                }
                else
                {
                    enteramt();
                }
            }
            catch
            {
            }
        }

        private void enteramt()
        {
            DataTable dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbsaletype.Text + "'");
            dt1 = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + txtitemname.Text + "'");
            if (dt.Rows[0]["Region"].ToString() == "Local")
            {
                tax = Convert.ToDouble(txttax.Text);
                sgstamt = tax / 2;
                cgstamt = tax / 2;
                if (string.IsNullOrEmpty(taxper))
                {
                    string a = lbltax1.Text;
                    a = a.Remove(a.Length - 1, 1);
                    var sub = a;
                    sub = sub.Substring(1);
                    taxper = sub;
                }
                sgstper = Convert.ToDouble(taxper) / 2;
                cgstper = Convert.ToDouble(taxper) / 2;
                addtaxper = Convert.ToDouble(additaxper);
                igstper = 0;
                igstamt = 0.00;


            }
            else
            {
                if (string.IsNullOrEmpty(taxper))
                {
                    string a = lbltax1.Text;
                    a = a.Remove(a.Length - 1, 1);
                    var sub = a;
                    sub = sub.Substring(1);
                    taxper = sub;
                }
                tax = Convert.ToDouble(txttax.Text);
                igstper = Convert.ToDouble(taxper);
                igstamt = tax;
                sgstper = 0;
                sgstamt = 0.00;
                cgstper = 0;
                cgstamt = 0.00;
                addtaxper = Convert.ToDouble(additaxper);
            }
            if (rowid >= 0)
            {
                lbliteminfo.Visible = false;
                LVFO.Items[rowid].SubItems[0].Text = txtitemname.Text;
                LVFO.Items[rowid].SubItems[1].Text = txtpacking.Text;
                if (cmbbatch.Text == "")
                {
                    DataTable batch = conn.getdataset("select Productid,Batchno from ProductPriceMaster where Productid='" + dt1.Rows[0]["ProductID"].ToString() + "' and isactive=1");
                    LVFO.Items[rowid].SubItems[2].Text = batch.Rows[0]["Batchno"].ToString();
                }
                else
                {
                    LVFO.Items[rowid].SubItems[2].Text = cmbbatch.Text;
                }

                LVFO.Items[rowid].SubItems[3].Text = txtbags.Text;
                LVFO.Items[rowid].SubItems[4].Text = (Math.Round(Convert.ToDouble(txtqty.Text), 2).ToString());
                LVFO.Items[rowid].SubItems[5].Text = (Math.Round(Convert.ToDouble(txtpqty.Text), 2).ToString());
                LVFO.Items[rowid].SubItems[6].Text = txtfree.Text;
                LVFO.Items[rowid].SubItems[7].Text = (Math.Round(Convert.ToDouble(txtrate.Text), 2).ToString());
                LVFO.Items[rowid].SubItems[8].Text = txtper.Text;
                LVFO.Items[rowid].SubItems[9].Text = (Math.Round(Convert.ToDouble(txttotal.Text), 2).ToString());
                LVFO.Items[rowid].SubItems[10].Text = (Math.Round(Convert.ToDouble(txtdisper.Text), 2).ToString());
                LVFO.Items[rowid].SubItems[11].Text = (Math.Round((Convert.ToDouble(txtdisamt.Text)), 2).ToString());
                LVFO.Items[rowid].SubItems[12].Text = (Math.Round((Convert.ToDouble(txttax.Text)), 2).ToString());
                LVFO.Items[rowid].SubItems[13].Text = (Math.Round((Convert.ToDouble(txtaddtax.Text)), 2).ToString());
                // double tamt = Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtcess.Text);
                // txtamount.Text = Convert.ToString(tamt);
                LVFO.Items[rowid].SubItems[14].Text = (Math.Round((Convert.ToDouble(txtamount.Text)), 2).ToString());
                LVFO.Items[rowid].SubItems[15].Text = (Math.Round((Convert.ToDouble(sgstper)), 2).ToString());
                LVFO.Items[rowid].SubItems[16].Text = (Math.Round((Convert.ToDouble(sgstamt)), 2).ToString());
                LVFO.Items[rowid].SubItems[17].Text = (Math.Round((Convert.ToDouble(cgstper)), 2).ToString());
                LVFO.Items[rowid].SubItems[18].Text = (Math.Round((Convert.ToDouble(cgstamt)), 2).ToString());
                LVFO.Items[rowid].SubItems[19].Text = (Math.Round((Convert.ToDouble(igstper)), 2).ToString());
                LVFO.Items[rowid].SubItems[20].Text = (Math.Round((Convert.ToDouble(igstamt)), 2).ToString());
                LVFO.Items[rowid].SubItems[21].Text = (Math.Round((Convert.ToDouble(addtaxper)), 2).ToString());

                for (int i = 0; i < lvserial.Items.Count; i++)
                {

                    //string srno = Regex.Replace(lvserial.Items[i].SubItems[0].Text, @"\s+", string.Empty);
                    string srno = lvserial.Items[i].SubItems[0].Text.Trim();
                    serialno += srno + ",";


                }
                if (serialno != null)
                {
                    //serialno = "NA";
                    serialno = serialno.TrimEnd(',');
                }
                LVFO.Items[rowid].SubItems[22].Text = (serialno);
                LVFO.Items[rowid].SubItems[23].Text = (txtcess.Text);
                LVFO.Items[rowid].SubItems[24].Text = (dt1.Rows[0]["ProductID"].ToString());
                LVFO.Items[rowid].SubItems[25].Text = (txtboxsrno.Text);
                if (BtnPayment.Text == "Update")
                {
                    if (itemproductid != dt1.Rows[0]["ProductID"].ToString())
                    {
                        if (!string.IsNullOrEmpty(serialno))
                        {
                            string[] values = serialno.Split(',');
                            //  temptable = new DataTable();
                            // temptable.Columns.Add("Itemname", typeof(string));
                            //  temptable.Columns.Add("SERIAL", typeof(string));
                            for (int i = 0; i < values.Length; i++)
                            {
                                for (int j = 0; j < temptable.Rows.Count; j++)
                                {
                                    if (temptable.Rows[j]["SERIAL"].ToString() == values[i])
                                    {
                                        temptable.Rows[j].Delete();
                                        temptable.AcceptChanges();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    // if (itemproductid == dt1.Rows[0]["ProductID"].ToString())
                    //  {
                    if (!string.IsNullOrEmpty(serialno))
                    {
                        string[] values = serialno.Split(',');
                        //  temptable = new DataTable();
                        // temptable.Columns.Add("Itemname", typeof(string));
                        //  temptable.Columns.Add("SERIAL", typeof(string));
                        for (int i = 0; i < values.Length; i++)
                        {
                            for (int j = 0; j < temptable.Rows.Count; j++)
                            {
                                if (temptable.Rows[j]["SERIAL"].ToString() == values[i])
                                {
                                    temptable.Rows[j].Delete();
                                    temptable.AcceptChanges();
                                }
                            }
                        }
                    }
                    //}
                }
                serialno = "";
                txtcess.Text = "0";
                rowid = -1;
                cessflag = 0;
            }
            else
            {
                ListViewItem li;
                lbliteminfo.Visible = false;
                li = LVFO.Items.Add(txtitemname.Text);
                li.SubItems.Add(txtpacking.Text);
                if (cmbbatch.Text == "")
                {
                    DataTable batch1 = conn.getdataset("select Productid,Batchno from ProductPriceMaster where Productid='" + dt1.Rows[0]["ProductID"].ToString() + "' and isactive='1'");
                    li.SubItems.Add(batch1.Rows[0]["Batchno"].ToString());
                }
                else
                {
                    li.SubItems.Add(cmbbatch.Text);
                }

                li.SubItems.Add(txtbags.Text);
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtqty.Text), 2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtpqty.Text), 2).ToString()));
                li.SubItems.Add(txtfree.Text);
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtrate.Text), 2).ToString()));
                li.SubItems.Add(txtper.Text);
                li.SubItems.Add((Math.Round(Convert.ToDouble(txttotal.Text), 2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtdisper.Text), 2).ToString()));
                li.SubItems.Add((Math.Round((Convert.ToDouble(txtdisamt.Text)), 2).ToString()));
                li.SubItems.Add((Math.Round((Convert.ToDouble(txttax.Text)), 2).ToString()));
                li.SubItems.Add((Math.Round((Convert.ToDouble(txtaddtax.Text)), 2).ToString()));
                //  double tamt = Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtcess.Text);
                // txtamount.Text = Convert.ToString(tamt);
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtamount.Text), 2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(sgstper), 2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(sgstamt), 2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(cgstper), 2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(cgstamt), 2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(igstper), 2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(igstamt), 2).ToString()));
                li.SubItems.Add((Math.Round(Convert.ToDouble(addtaxper), 2).ToString()));
                for (int i = 0; i < lvserial.Items.Count; i++)
                {
                    //string srno = Regex.Replace(lvserial.Items[i].SubItems[0].Text, @"\s+", string.Empty);
                    string srno = lvserial.Items[i].SubItems[0].Text.Trim();
                    serialno += srno + ",";


                }
                if (serialno != null)
                {
                    //serialno = "NA";
                    serialno = serialno.TrimEnd(',');
                }
                li.SubItems.Add(serialno);
                if (itemproductid != dt1.Rows[0]["ProductID"].ToString())
                {
                    if (!string.IsNullOrEmpty(serialno))
                    {
                        string[] values = serialno.Split(',');
                        //  temptable = new DataTable();
                        // temptable.Columns.Add("Itemname", typeof(string));
                        //  temptable.Columns.Add("SERIAL", typeof(string));
                        for (int i = 0; i < values.Length; i++)
                        {
                            for (int j = 0; j < temptable.Rows.Count; j++)
                            {
                                if (temptable.Rows[j]["SERIAL"].ToString() == values[i])
                                {
                                    temptable.Rows[j].Delete();
                                    temptable.AcceptChanges();
                                }
                            }
                        }
                    }
                }
                li.SubItems.Add(txtcess.Text);
                li.SubItems.Add(dt1.Rows[0]["ProductID"].ToString());
                li.SubItems.Add(txtboxsrno.Text);
                txtcess.Text = "0";
                serialno = "";
                // LVFO.Items[rowid].SubItems[22].Text = (serialno);
            }
            if (Convert.ToDouble(txtamount.Text) > 0)
            {
                if (strfinalarray[0] == "S")
                {
                    if (Convert.ToBoolean(options.Rows[0]["itemsinasedingorderinsale"].ToString()) == true)
                    {
                        this.LVFO.ListViewItemSorter = new ListViewItemComparer(0);
                    }
                }
            }

            totalcalculation();
            clearitem();
            txtitemname.Focus();
            cessflag = 0;
            bindallitem();
        }
        private void txtamount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enteramount();
            }
        }

        private void txtamount_TextChanged(object sender, EventArgs e)
        {

        }



        private void cmbbatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbbatch.SelectedIndex == -1 && cmbbatch.Focused)
                {
                    lbliteminfo.Text = "iteminfo";
                    return;

                }
                else
                {
                    dt5 = conn.getdataset("select * from ProductMaster where isactive=1 and Product_Name='" + txtitemname.Text + "'");
                    dt6 = conn.getdataset("select * from TaxSlabMaster i inner join productmaster p on i.Taxslabname=p.taxslab where p.isactive=1 and i.isactive=1 and p.product_name='" + txtitemname.Text + "' and i.saletypename='" + cmbsaletype.Text + "'");
                    dt7 = conn.getdataset("select * from ProductPriceMaster where isactive=1 and Productid='" + dt5.Rows[0]["ProductID"].ToString() + "' and Batchno='" + cmbbatch.Text + "'");
                 //   getstock(txtitemname.Text, cmbbatch.Text);
                    currentstock = gc.getstock(txtitemname.Text, cmbbatch.Text);
                    try
                    {
                        string rateprice = conn.ExecuteScalar("select bp.Rate from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno where b.isactive=1 and bp.isactive=1 and b.BillType='" + strfinalarray[0] + "' and b.ClientID='" + cmbcustname.SelectedValue + "' and bp.productid='" + dt5.Rows[0]["ProductID"].ToString() + "' order by b.Bill_No desc");
                        if (string.IsNullOrEmpty(rateprice))
                        {
                            lastprice = "0";
                        }
                        else
                        {
                            lastprice = rateprice;
                        }
                    }
                    catch
                    {
                    }
                    dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbsaletype.Text + "'");
                    if (dt.Rows[0]["Region"].ToString() == "Local")
                    {
                        if (dt6.Rows.Count > 0 && dt7.Rows.Count > 0)
                        {
                            lbliteminfo.Text = "Tax=" + dt6.Rows[0]["Sgst"].ToString() + "% + " + dt6.Rows[0]["cgst"].ToString() + "%,AddTax=" + dt6.Rows[0]["additax"].ToString() + ",Sale Price=" + dt7.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt7.Rows[0]["MRP"].ToString() + ",Basic=" + dt7.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt7.Rows[0]["BasicPrice"].ToString() + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                        }
                        else if (dt6.Rows.Count > 0 && dt7.Rows.Count <= 0)
                        {
                            lbliteminfo.Text = "Tax=" + dt6.Rows[0]["Sgst"].ToString() + "% + " + dt6.Rows[0]["cgst"].ToString() + "%,AddTax=" + dt6.Rows[0]["additax"].ToString() + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                        }
                        else if (dt6.Rows.Count <= 0 && dt7.Rows.Count > 0)
                        {
                            lbliteminfo.Text = "Tax=" + 0 + "% + " + 0 + "%,AddTax=" + 0 + ",Sale Price=" + dt7.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt7.Rows[0]["MRP"].ToString() + ",Basic=" + dt7.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt7.Rows[0]["BasicPrice"].ToString() + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                        }
                        else if (dt6.Rows.Count <= 0 && dt7.Rows.Count <= 0)
                        {
                            lbliteminfo.Text = "Tax=" + 0 + "% + " + 0 + "%,AddTax=" + 0 + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                        }
                        // lbliteminfo.Text = "Tax=" + dt2.Rows[0]["Sgst"].ToString() + "% + " + dt2.Rows[0]["cgst"].ToString() + "%,AddTax=" + dt2.Rows[0]["additax"].ToString() + ",Sale Price=" + dt7.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt7.Rows[0]["MRP"].ToString() + ",Basic=" + dt7.Rows[0]["BasicPrice"].ToString() + "";
                    }
                    else
                    {
                        if (dt6.Rows.Count > 0 && dt7.Rows.Count > 0)
                        {
                            lbliteminfo.Text = "Tax=" + dt6.Rows[0]["Igst"].ToString() + "%,AddTax=" + dt6.Rows[0]["additax"].ToString() + ",Sale Price=" + dt7.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt7.Rows[0]["MRP"].ToString() + ",Basic=" + dt7.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt7.Rows[0]["BasicPrice"].ToString() + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                        }
                        else if (dt6.Rows.Count > 0 && dt7.Rows.Count <= 0)
                        {
                            lbliteminfo.Text = "Tax=" + dt6.Rows[0]["Igst"].ToString() + "%,AddTax=" + dt6.Rows[0]["additax"].ToString() + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                        }
                        else if (dt6.Rows.Count <= 0 && dt7.Rows.Count > 0)
                        {
                            lbliteminfo.Text = "Tax=" + 0 + "%,AddTax=" + 0 + ",Sale Price=" + dt7.Rows[0]["SalePrice"].ToString() + ",MRP=" + dt7.Rows[0]["MRP"].ToString() + ",Basic=" + dt7.Rows[0]["BasicPrice"].ToString() + ",PurchasePrice=" + dt7.Rows[0]["BasicPrice"].ToString() + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                        }
                        else if (dt6.Rows.Count <= 0 && dt7.Rows.Count <= 0)
                        {
                            lbliteminfo.Text = "Tax=" + 0 + "%,AddTax=" + 0 + ",Sale Price=" + 0 + ",MRP=" + 0 + ",Basic=" + 0 + ",PurchasePrice=" + 0 + ",Current Stock=" + currentstock + ",Last Price=" + lastprice + "";
                        }

                    }
                    x = 0;
                    DataTable dprice = new DataTable();
                    dprice = conn.getdataset("select PickupPrice from PurchasetypeMaster where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypename='" + cmbsaletype.Text + "'");
                    Dprice = dprice.Rows[0]["PickupPrice"].ToString();
                    SqlCommand cmd5 = new SqlCommand("select p.*,b.*,b." + Dprice + " as Dprice from productmaster p inner join ProductPriceMaster b on p.ProductID=b.ProductID where p.isactive=1 and b.isactive=1 and p.product_name='" + txtitemname.Text + "' and b.Batchno='" + cmbbatch.Text + "'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                    DataTable dt9 = new DataTable();
                    sda.Fill(dt9);
                    lbliteminfo.Visible = true;
                    qtyflag = 0;
                    double[] dis = new double[2];
                    dis = calculatediscount(dt9.Rows[0]["Product_Name"].ToString(), cmbcustname.Text, Convert.ToDouble(dt9.Rows[0]["Dprice"].ToString()));
                    if (Convert.ToDouble(dis[1].ToString()) != 0)
                    {
                        txtrate.Text = dis[1].ToString();
                    }
                    else
                    {
                        txtrate.Text = dt9.Rows[0]["Dprice"].ToString();
                    }
                    txtdisper.Text = dis[0].ToString();
                    // DataTable dis = conn.getdataset("select specialrate,discount from partyrates where (partyid=0 or partyid='" + cmbcustname.SelectedValue + "') and itemid=" + dt9.Rows[0]["productid"].ToString());
                    //if (dis.Rows.Count > 0)
                    //{
                    //    if (Convert.ToDouble(dis.Rows[0]["specialrate"].ToString()) > 0)
                    //    {
                    //        txtrate.Text = dis.Rows[0]["specialrate"].ToString();
                    //    }
                    //    else
                    //    {
                    //        txtrate.Text = dt9.Rows[0]["Dprice"].ToString();
                    //    }
                    //    if (Convert.ToDouble(dis.Rows[0]["discount"].ToString()) > 0)
                    //    {
                    //        txtdisper.Text = dis.Rows[0]["discount"].ToString();
                    //    }
                    //    else
                    //    {
                    //        txtdisper.Text = dis.Rows[0]["discount"].ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    txtrate.Text = dt9.Rows[0]["Dprice"].ToString();
                    //    txtdisper.Text = "0.00";
                    //}
                    if (options.Rows[0]["requirdlastpriceinbill"].ToString() == "True")
                    {
                        try
                        {
                            string rateprice = conn.ExecuteScalar("select bp.Rate from BillMaster b inner join BillProductMaster bp on b.billno=bp.billno where b.isactive=1 and bp.isactive=1 and b.BillType='" + strfinalarray[0] + "' and b.ClientID='" + cmbcustname.SelectedValue + "' and bp.productid='" + dt1.Rows[0]["ProductID"].ToString() + "' order by b.Bill_No desc");
                            if (!string.IsNullOrEmpty(rateprice))
                            {
                                txtrate.Text = rateprice;
                            }
                        }
                        catch
                        {
                        }
                    }
                    txtper.Text = dt9.Rows[0]["Unit"].ToString();
                    lblbagqty.Text = "[" + dt9.Rows[0]["Unit"].ToString() + "]";
                    lblaltqty.Text = "[" + dt9.Rows[0]["Altunit"].ToString() + "]";
                    txtqty.Text = dt9.Rows[0]["Convfactor"].ToString();
                    txtdisamt.Text = "0.00";

                    txtfree.Text = "0";
                    SqlCommand cmd6 = new SqlCommand("select * from TaxSlabMaster i inner join productmaster p on i.Taxslabname=p.taxslab where p.isactive=1 and i.isactive=1 and p.product_name='" + txtitemname.Text + "' and i.saletypename='" + cmbsaletype.Text + "'", con);
                    SqlDataAdapter sda6 = new SqlDataAdapter(cmd6);
                    DataTable dt10 = new DataTable();
                    sda6.Fill(dt10);
                    string istax = conn.ExecuteScalar("Select taxtypename from Purchasetypemaster where isactive=1 and FormType='" + strfinalarray[0] + "' and Purchasetypename='" + cmbsaletype.Text + "'");
                    if (dt10.Rows.Count > 0)
                    {
                        txttax.Text = "0";
                        txtaddtax.Text = "0";
                        if (istax != "Tax Free")
                        {
                            lbltax1.Text = "[" + Math.Round(Convert.ToDouble(dt10.Rows[0]["igst"].ToString()), 2).ToString() + "]";
                            taxforprice = lbltax1.Text;
                            taxper = dt10.Rows[0]["igst"].ToString();
                            lbladdtax1.Text = "[" + Math.Round(Convert.ToDouble(dt10.Rows[0]["additax"].ToString()), 2).ToString() + "]";
                            ataxforprice = lbladdtax1.Text;
                            additaxper = dt10.Rows[0]["additax"].ToString();
                            taxid = Math.Round(Convert.ToDouble(dt10.Rows[0]["igst"].ToString()), 2);
                            addtaxid = Math.Round(Convert.ToDouble(dt10.Rows[0]["additax"].ToString()), 2);
                        }
                        else
                        {
                            lbltax1.Text = "[0]";
                            taxforprice = lbltax1.Text;
                            lbladdtax1.Text = "[0]";
                            ataxforprice = lbladdtax1.Text;
                            taxid = 0;
                            addtaxid = 0;
                        }




                    }
                    else
                    {

                        //  MessageBox.Show("Not any Tax Available For This Sale Type");
                        txttax.Text = "0";
                        txtaddtax.Text = "0";
                        lbltax1.Text = "[0]";
                        taxforprice = lbltax1.Text;
                        lbladdtax1.Text = "[0]";
                        ataxforprice = lbladdtax1.Text;
                        taxid = 0;
                        addtaxid = 0;
                        //txtpacking.Focus();
                    }

                }

            }
            catch
            {
            }
        }

        private void cmbterms_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbterms.SelectedIndex = 0;
                cmbterms.DroppedDown = true;
            }
            catch
            {
            }


        }

        private void cmbcustname_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbcustname.SelectedIndex = 0;
                cmbcustname.DroppedDown = true;
            }
            catch
            {
            }

        }

        private void cmbsaletype_Enter(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    cmbsaletype.SelectedIndex = 0;
                    cmbsaletype.DroppedDown = true;
                }
                catch
                {
                }
            }
            catch
            {
            }

        }

        //private void cmbbatch_Enter(object sender, EventArgs e)
        //{
        //    if (cmbbatch.Enabled == true)
        //    {
        //      //  cmbbatch.SelectedIndex = 0;
        //        int count = cmbbatch.Items.Count - 1;
        //        cmbbatch.SelectedIndex = count;
        //        //int count = cmbbatch.Items.Count;
        //        //for (int i = 0; i < count; i++)
        //        //{
        //        //    cmbbatch.SelectedIndex = i;
        //        //}
        //        cmbbatch.DroppedDown = true;
        //    }

        //}

        private void cmbcharper_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbcharper.SelectedIndex = 0;
                cmbcharper.DroppedDown = true;
            }
            catch
            {
            }

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
        int flagserial = 0;
        string serialitem = "";
        int serialflag = 0;
        private void txtserial_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    #region
                    //string ItemID = conn.ExecuteScalar("select Productid from productmaster where Product_Name='" + txtitemname.Text + "'");
                    //if (strfinalarray[0] == "S")
                    //{
                    //    string srno = Regex.Replace(txtserial.Text, @"\s+", string.Empty);
                    //    DataTable serialitem = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='S'");
                    //    if (serialitem.Rows.Count > 0)
                    //    {
                    //        string avalable = conn.ExecuteScalar("select so.Bill_No from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and billtype='SC' and billno='" + serialitem.Rows[0]["VoucherID"].ToString() + "' order by so.bill_Run_Date");
                    //        if (!string.IsNullOrEmpty(avalable))
                    //        {
                    //            MessageBox.Show("Serial No is exists");
                    //            return;
                    //        }
                    //        string avalable1 = conn.ExecuteScalar("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No from billproductmaster where isactive=1 and billtype='S' and billno='" + serialitem.Rows[0]["VoucherID"].ToString() + "' order by bill_Run_Date");
                    //        if (!string.IsNullOrEmpty(avalable1))
                    //        {
                    //            MessageBox.Show("Serial No is exists");
                    //            return;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        DataTable serialitem1 = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='PC'");
                    //        if (serialitem1.Rows.Count > 0)
                    //        {
                    //            string avalable2 = conn.ExecuteScalar("select so.Bill_No from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and billtype='PC' and billno='" + serialitem1.Rows[0]["VoucherID"].ToString() + "' order by so.bill_Run_Date");
                    //            if (string.IsNullOrEmpty(avalable2))
                    //            {
                    //                DialogResult dr1 = MessageBox.Show("This Serial No dose not exist " + Environment.NewLine + "Purchase Enttry may not have been Posted or item may sold earlier" + Environment.NewLine + "Do you still wish to bill this Sr.No. ?", "Invalid Sr.No", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //                if (dr1 == DialogResult.Yes)
                    //                {
                    //                    serialflag = 1;
                    //                }
                    //                else
                    //                {
                    //                    txtserial.Text = "";
                    //                    txtserial.Focus();
                    //                    return;
                    //                }
                    //            }
                    //        }
                    //        //else
                    //        //{
                    //        //    DialogResult dr1 = MessageBox.Show("This Serial No dose not exist " + Environment.NewLine + "Purchase Enttry may not have been Posted or item may sold earlier" + Environment.NewLine + "Do you still wish to bill this Sr.No. ?", "Invalid Sr.No", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //        //    if (dr1 == DialogResult.Yes)
                    //        //    {
                    //        //    }
                    //        //    else
                    //        //    {
                    //        //        txtserial.Text = "";
                    //        //        txtserial.Focus();
                    //        //        return;
                    //        //    }
                    //        //}
                    //        if (serialflag == 0)
                    //        {
                    //            DataTable serialitem2 = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='P'");
                    //            if (serialitem1.Rows.Count > 0)
                    //            {
                    //                string avalable3 = conn.ExecuteScalar("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No from billproductmaster where isactive=1 and billtype='P' and billno='" + serialitem2.Rows[0]["VoucherID"].ToString() + "' order by bill_Run_Date");
                    //                if (string.IsNullOrEmpty(avalable3))
                    //                {
                    //                    DialogResult dr1 = MessageBox.Show("This Serial No dose not exist " + Environment.NewLine + "Purchase Enttry may not have been Posted or item may sold earlier" + Environment.NewLine + "Do you still wish to bill this Sr.No. ?", "Invalid Sr.No", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //                    if (dr1 == DialogResult.Yes)
                    //                    {

                    //                    }
                    //                    else
                    //                    {
                    //                        txtserial.Text = "";
                    //                        txtserial.Focus();
                    //                        return;
                    //                    }
                    //                }
                    //            }
                    //            //else
                    //            //{
                    //            //    DialogResult dr1 = MessageBox.Show("This Serial No dose not exist " + Environment.NewLine + "Purchase Enttry may not have been Posted or item may sold earlier" + Environment.NewLine + "Do you still wish to bill this Sr.No. ?", "Invalid Sr.No", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //            //    if (dr1 == DialogResult.Yes)
                    //            //    {

                    //            //    }
                    //            //    else
                    //            //    {
                    //            //        txtserial.Text = "";
                    //            //        txtserial.Focus();
                    //            //        return;
                    //            //    }
                    //            //}
                    //        }
                    //    }
                    //}
                    //else if (strfinalarray[0] == "SR")
                    //{
                    //    string srno = Regex.Replace(txtserial.Text, @"\s+", string.Empty);
                    //    DataTable serialitem = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='SR'");
                    //    if (serialitem.Rows.Count > 0)
                    //    {
                    //        string avalable = conn.ExecuteScalar("select so.Bill_No from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and billtype='SC' and billno='" + serialitem.Rows[0]["VoucherID"].ToString() + "' order by so.bill_Run_Date");
                    //        if (!string.IsNullOrEmpty(avalable))
                    //        {
                    //            MessageBox.Show("Serial No is exists");
                    //            return;
                    //        }
                    //        string avalable1 = conn.ExecuteScalar("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No from billproductmaster where isactive=1 and billtype='S' and billno='" + serialitem.Rows[0]["VoucherID"].ToString() + "' order by bill_Run_Date");
                    //        if (!string.IsNullOrEmpty(avalable1))
                    //        {
                    //            MessageBox.Show("Serial No is exists");
                    //            return;
                    //        }
                    //    }


                    //}
                    //else if (strfinalarray[0] == "P")
                    //{
                    //    string srno = Regex.Replace(txtserial.Text, @"\s+", string.Empty);
                    //    DataTable serialitem = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='P'");
                    //    if (serialitem.Rows.Count > 0)
                    //    {
                    //        string avalable = conn.ExecuteScalar("select so.Bill_No from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and billtype='PC' and billno='" + serialitem.Rows[0]["VoucherID"].ToString() + "' order by so.bill_Run_Date");
                    //        if (!string.IsNullOrEmpty(avalable))
                    //        {
                    //            MessageBox.Show("Serial No is exists");
                    //            return;
                    //        }
                    //        string avalable1 = conn.ExecuteScalar("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No from billproductmaster where isactive=1 and billtype='P' and billno='" + serialitem.Rows[0]["VoucherID"].ToString() + "' order by bill_Run_Date");
                    //        if (!string.IsNullOrEmpty(avalable1))
                    //        {
                    //            MessageBox.Show("Serial No is exists");
                    //            return;
                    //        }
                    //    }
                    //    DataTable serialitem1 = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='PC'");
                    //    if (serialitem1.Rows.Count > 0)
                    //    {
                    //        string avalable = conn.ExecuteScalar("select so.Bill_No from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and billtype='PC' and billno='" + serialitem1.Rows[0]["VoucherID"].ToString() + "' order by so.bill_Run_Date");
                    //        if (!string.IsNullOrEmpty(avalable))
                    //        {
                    //            MessageBox.Show("Serial No is exists");
                    //            return;
                    //        }
                    //        string avalable1 = conn.ExecuteScalar("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No from billproductmaster where isactive=1 and billtype='P' and billno='" + serialitem1.Rows[0]["VoucherID"].ToString() + "' order by bill_Run_Date");
                    //        if (!string.IsNullOrEmpty(avalable1))
                    //        {
                    //            MessageBox.Show("Serial No is exists");
                    //            return;
                    //        }
                    //    }
                    //}
                    //else if (strfinalarray[0] == "PR")
                    //{
                    //    string srno = Regex.Replace(txtserial.Text, @"\s+", string.Empty);
                    //    DataTable serialitem = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='PR'");
                    //    if (serialitem.Rows.Count > 0)
                    //    {
                    //        string avalable = conn.ExecuteScalar("select so.Bill_No from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and billtype='PC' and billno='" + serialitem.Rows[0]["VoucherID"].ToString() + "' order by so.bill_Run_Date");
                    //        if (!string.IsNullOrEmpty(avalable))
                    //        {
                    //            MessageBox.Show("Serial No is exists");
                    //            return;
                    //        }
                    //        string avalable1 = conn.ExecuteScalar("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No from billproductmaster where isactive=1 and billtype='P' and billno='" + serialitem.Rows[0]["VoucherID"].ToString() + "' order by bill_Run_Date");
                    //        if (!string.IsNullOrEmpty(avalable1))
                    //        {
                    //            MessageBox.Show("Serial No is exists");
                    //            return;
                    //        }
                    //    }
                    //}
                    #endregion
                    if (txtserial.Text != "")
                    {
                        string ItemID = conn.ExecuteScalar("select Productid from productmaster where isactive=1 and Product_Name='" + txtitemname.Text + "'");
                        //string srno = Regex.Replace(txtserial.Text, @"\s+", string.Empty);
                        string srno = txtserial.Text.Trim();
                        if (strfinalarray[0] == "S")
                        {
                            DataTable checkS = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='S'");
                            if (checkS.Rows.Count > 0)
                            {
                                DataTable checkSR = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='SR'");
                                if (checkSR.Rows.Count > 0)
                                {

                                }
                                else
                                {
                                    MessageBox.Show("Serial No. already added into the Sale. please Remove first then add again");
                                    return;
                                }
                            }
                            else
                            {
                                DataTable checkPC = conn.getdataset("select * from Serials s inner join SaleOrderMaster so on so.billno=s.VoucherID and so.isactive=1 where s.isactive=1 and s.ItemID='" + ItemID + "' and s.SerialNo='" + srno + "' and s.TransactionID='PC' and so.OrderStatus='Pending'");
                                if (checkPC.Rows.Count > 0)
                                {

                                }
                                else
                                {
                                    DataTable checkP = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='P'");
                                    if (checkP.Rows.Count > 0)
                                    {
                                        DataTable checkPR = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='PR'");
                                        if (checkPR.Rows.Count > 0)
                                        {
                                            MessageBox.Show("Serial No. already Return From the Purchase. please Remove first then add again");
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        DialogResult dr1 = MessageBox.Show("Serial No. not available into Purchase Do you want to still add?", "Serial", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (dr1 == DialogResult.Yes)
                                        {

                                        }
                                        else
                                        {
                                            srno = "";
                                            return;
                                        }

                                    }

                                }

                            }
                        }
                        else if (strfinalarray[0] == "SR")
                        {
                            DataTable checkS = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='S'");
                            if (checkS.Rows.Count > 0)
                            {
                                DataTable checkSR = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='SR'");
                                if (checkSR.Rows.Count > 0)
                                {
                                    MessageBox.Show("Serial No. already added into the Sale Return. please Remove first then add again");
                                    return;
                                }
                            }
                        }
                        else if (strfinalarray[0] == "P")
                        {
                            DataTable checkP = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='P'");
                            if (checkP.Rows.Count > 0)
                            {
                                DataTable checkPR = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='PR'");
                                if (checkPR.Rows.Count > 0)
                                {

                                }
                                else
                                {
                                    MessageBox.Show("Serial No. already added into the Purchase. please Remove first then add again");
                                    return;
                                }
                            }
                        }
                        else if (strfinalarray[0] == "PR")
                        {
                            DataTable checkP = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='P'");
                            if (checkP.Rows.Count > 0)
                            {
                                DataTable checkPR = conn.getdataset("select * from Serials where isactive=1 and ItemID='" + ItemID + "' and SerialNo='" + srno + "' and TransactionID='PR'");
                                if (checkPR.Rows.Count > 0)
                                {
                                    MessageBox.Show("Serial No. already added into the Purchase Return. please Remove first then add again");
                                    return;
                                }
                            }
                        }
                        bool exists = false;
                        foreach (ListViewItem item in LVFO.Items)
                        {
                            for (int b = 0; b < item.SubItems.Count; b++)
                            {
                                //   string srno = item.SubItems[22].Text;
                                //string newsrno = Regex.Replace(item.SubItems[22].Text, @"\s+", string.Empty);
                                string newsrno = item.SubItems[22].Text.Trim();
                                if (string.IsNullOrEmpty(newsrno))
                                {
                                    string[] values = newsrno.Split(',');
                                    for (int i = 0; i < values.Length; i++)
                                    {
                                        string itemname = item.SubItems[0].Text;
                                        if (txtserial.Text == values[i] && txtitemname.Text == itemname)
                                        {
                                            exists = true;
                                        }
                                    }
                                }
                            }
                        }
                        if (exists == true)
                        {
                            MessageBox.Show("Serial No is exists");
                            return;
                        }
                        double qty11 = Convert.ToDouble(txtfree.Text) + Convert.ToDouble(txtbags.Text);
                        if (lvserial.Items.Count >= Convert.ToInt64(qty11))
                        {
                            txtserial.Text = "";
                            pnlserial.Visible = false;
                            txtrate.Focus();
                            flagserial = 1;
                        }
                        if (lvserial.Items.Count > Convert.ToInt64(txtbags.Text) && serialno != "NA")
                        {
                            MessageBox.Show("Serial no is More than Qty Please Remove");
                            return;
                        }
                        if (txtserial.Text == "" && flagserial == 0)
                        {
                            MessageBox.Show("Enter Serial No");
                            return;
                        }
                        string qty1 = txtbags.Text;
                        string free = txtfree.Text;
                        double qty = Convert.ToDouble(txtfree.Text) + Convert.ToDouble(txtbags.Text);
                        ListViewItem li = new ListViewItem();
                        foreach (ListViewItem item in lvserial.Items)
                        {
                            String withoutLast = item.Text.Substring(0, (item.Text.Length - 2));
                            if (withoutLast == txtserial.Text)
                            {
                                MessageBox.Show("Serial No is exists");
                                return;
                            }
                        }

                        if (lvserial.Items.Count >= Convert.ToInt64(qty))
                        {

                            txtserial.Text = "";
                            pnlserial.Visible = false;
                            txtrate.Focus();
                        }
                        else
                        {
                            li = lvserial.Items.Add(txtserial.Text + Environment.NewLine);
                            temptable.Rows.Add(txtitemname.Text, txtserial.Text);
                            txtserial.Text = "";
                            if (lvserial.Items.Count >= Convert.ToInt64(qty))
                            {
                                txtserial.Text = "";
                                pnlserial.Visible = false;
                                txtrate.Focus();
                            }
                        }
                        txtserial.Text = "";
                    }
                    else
                    {
                        double qty = Convert.ToDouble(txtfree.Text) + Convert.ToDouble(txtbags.Text);
                        if (lvserial.Items.Count >= Convert.ToInt64(qty))
                        {

                            txtserial.Text = "";
                            pnlserial.Visible = false;
                            txtrate.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Enter Serial No");
                        }
                    }
                    flagserial = 0;
                }

            }
            catch
            {
            }
        }

        private void lvserial_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                //string srno = Regex.Replace(lvserial.Items[lvserial.FocusedItem.Index].SubItems[0].Text, @"\s+", string.Empty);
                string srno = lvserial.Items[lvserial.FocusedItem.Index].SubItems[0].Text.Trim();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {

                    if (temptable.Rows[i]["Itemname"].ToString().Trim().Contains(txtitemname.Text) && temptable.Rows[i]["SERIAL"].ToString().Trim().Contains(srno))
                    {
                        temptable.Rows[i].Delete();

                    }
                }
                temptable.AcceptChanges();
                txtserial.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[0].Text;
                lvserial.Items[lvserial.FocusedItem.Index].Remove();
                txtserial.Focus();
            }
            catch
            {
            }
        }

        private void lvserial_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    //string srno = Regex.Replace(lvserial.Items[lvserial.FocusedItem.Index].SubItems[0].Text, @"\s+", string.Empty);
                    string srno = lvserial.Items[lvserial.FocusedItem.Index].SubItems[0].Text.Trim();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {

                        if (temptable.Rows[i]["Itemname"].ToString().Trim().Contains(txtitemname.Text) && temptable.Rows[i]["SERIAL"].ToString().Trim().Contains(srno))
                        {
                            temptable.Rows[i].Delete();
                        }
                    }
                    temptable.AcceptChanges();
                    lvserial.Items[lvserial.FocusedItem.Index].Remove();
                }
            }
            catch
            {
            }
        }

        private void txtpono_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            //{
            //    e.Handled = false;
            //}
            //else
            //{
            //    e.Handled = true;
            //}
        }

        private void txttax_KeyPress(object sender, KeyPressEventArgs e)
        {
            {

                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtaddtax_KeyPress(object sender, KeyPressEventArgs e)
        {
            {

                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            {

                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtserial_KeyPress(object sender, KeyPressEventArgs e)
        {
            {

                //if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
                //{
                //    e.Handled = false;
                //}
                //else
                //{
                //    e.Handled = true;
                //}
            }
        }

        private void TxtRundate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //DataSet dtrange = conn.getdata("SELECT * FROM Company where CompanyID='" + Master.companyId + "'");
                //TxtRundate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                //TxtRundate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                //txtduedate.MinDate = Convert.ToDateTime(TxtRundate.Value);
                //txtduedate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                //DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                //TxtRundate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                //TxtRundate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                //txtduedate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                //txtduedate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
            }
            catch
            {
            }
        }

        private void txtpono_Enter(object sender, EventArgs e)
        {
            txtpono.BackColor = Color.LightYellow;
        }

        private void txtpono_Leave(object sender, EventArgs e)
        {
            txtpono.BackColor = Color.White;
        }

        private void TxtBillNo_Enter(object sender, EventArgs e)
        {
            TxtBillNo.BackColor = Color.LightYellow;
        }

        private void TxtBillNo_Leave(object sender, EventArgs e)
        {
            TxtBillNo.BackColor = Color.White;
        }

        private void txtitemname_Enter(object sender, EventArgs e)
        {
            try
            {
                //        if (txtitemname.Focused)
                //      {
                txtitemname.BackColor = Color.LightYellow;
                pnlallitem.Visible = true;
                bindallitem();
                grditem.Rows[0].Selected = false;
                //    }
                //  lvallitem.Select();
                //lvallitem.Items[0].Selected = true;
            }
            catch
            {
            }

        }

        private void txtitemname_Leave(object sender, EventArgs e)
        {
            txtitemname.BackColor = Color.White;
            try
            {
                if (grditem.Rows[0].Selected == true)
                {
                    pnlallitem.Visible = true;
                }
                else
                {
                    pnlallitem.Visible = false;
                }
            }
            catch
            {
            }
        }

        private void txtpacking_Enter(object sender, EventArgs e)
        {
            txtpacking.BackColor = Color.LightYellow;
        }

        private void txtpacking_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpacking.Text))
            {
                txtpacking.Text = "1";
            }
            txtpacking.BackColor = Color.White;
        }

        private void txtbags_Enter(object sender, EventArgs e)
        {
            txtbags.BackColor = Color.LightYellow;
        }

        private void txtbags_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbags.Text))
            {
                txtbags.Text = "1";
            }
            txtbags.BackColor = Color.White;
        }

        private void txtqty_Enter(object sender, EventArgs e)
        {
            txtqty.BackColor = Color.LightYellow;
        }

        private void txtqty_Leave(object sender, EventArgs e)
        {
            txtqty.BackColor = Color.White;
        }

        private void txtpqty_Enter(object sender, EventArgs e)
        {
            txtpqty.BackColor = Color.LightYellow;
        }

        private void txtpqty_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpqty.Text))
            {
                txtpqty.Text = "1";
            }
            txtpqty.BackColor = Color.White;
        }

        private void txtfree_Enter(object sender, EventArgs e)
        {
            txtfree.BackColor = Color.LightYellow;
        }

        private void txtfree_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtfree.Text))
            {
                txtfree.Text = "0";
            }
            txtfree.BackColor = Color.White;
        }

        private void txtrate_Enter(object sender, EventArgs e)
        {
            txtrate.BackColor = Color.LightYellow;
        }

        private void txtrate_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtrate.Text))
            {
                txtrate.Text = "0";
            }
            txtrate.BackColor = Color.White;
        }

        private void txtper_Enter(object sender, EventArgs e)
        {
            txtper.BackColor = Color.LightYellow;
        }

        private void txtper_Leave(object sender, EventArgs e)
        {
            txtper.BackColor = Color.White;
        }

        private void txttotal_Enter(object sender, EventArgs e)
        {
            txttotal.BackColor = Color.LightYellow;
        }

        private void txttotal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttotal.Text))
            {
                txttotal.Text = "0";
            }
            txttotal.BackColor = Color.White;
        }

        private void txtdisper_Enter(object sender, EventArgs e)
        {
            txtdisper.BackColor = Color.LightYellow;
        }

        private void txtdisper_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtdisper.Text))
            {
                txtdisper.Text = "0";
            }
            txtdisper.BackColor = Color.White;
        }

        private void txtdisamt_Enter(object sender, EventArgs e)
        {
            txtdisamt.BackColor = Color.LightYellow;
        }

        private void txtdisamt_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtdisamt.Text))
            {
                txtdisamt.Text = "0";
            }
            txtdisamt.BackColor = Color.White;
        }

        private void txttax_Enter(object sender, EventArgs e)
        {
            txttax.BackColor = Color.LightYellow;
        }

        private void txttax_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttax.Text))
            {
                txttax.Text = "0";
            }
            txttax.BackColor = Color.White;
        }

        private void txtaddtax_Enter(object sender, EventArgs e)
        {
            txtaddtax.BackColor = Color.LightYellow;
        }

        private void txtaddtax_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtaddtax.Text))
            {
                txtaddtax.Text = "0";
            }
            txtaddtax.BackColor = Color.White;
        }

        private void txtamount_Enter(object sender, EventArgs e)
        {
            txtamount.BackColor = Color.LightYellow;
        }

        private void txtamount_Leave(object sender, EventArgs e)
        {
            txtamount.BackColor = Color.White;
        }

        private void txtcharremark_Enter(object sender, EventArgs e)
        {
            txtcharremark.BackColor = Color.LightYellow;
        }

        private void txtcharremark_Leave(object sender, EventArgs e)
        {
            txtcharremark.BackColor = Color.White;
        }

        private void txtcharval_Enter(object sender, EventArgs e)
        {
            txtcharval.BackColor = Color.LightYellow;
        }

        private void txtcharval_Leave(object sender, EventArgs e)
        {
            txtcharval.BackColor = Color.White;
        }

        private void txtcharat_Enter(object sender, EventArgs e)
        {
            txtcharat.BackColor = Color.LightYellow;
        }

        private void txtcharat_Leave(object sender, EventArgs e)
        {
            txtcharat.BackColor = Color.White;
        }

        private void txtcharplusminus_Enter(object sender, EventArgs e)
        {
            txtcharplusminus.BackColor = Color.LightYellow;
        }

        private void txtcharplusminus_Leave(object sender, EventArgs e)
        {
            txtcharplusminus.BackColor = Color.White;
        }
        string searchstr;
        private SaleReturnRegisterdetailed SaleReturnRegisterdetailed;
        private saleregisterdetailed saleregisterdetailed;
        private purchaseregisterdetailed purchaseregisterdetailed;
        private void timer1_Tick(object sender, EventArgs e)
        {
            // searchstr = "";
        }

        private void cmbcustname_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbcustname.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbcustname.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbsaletype_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbsaletype.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbsaletype.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void BtnPayment_Enter(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = false;
            BtnPayment.BackColor = Color.YellowGreen;
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_Leave(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = true;
            BtnPayment.BackColor = Color.FromArgb(51, 153, 255);
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_MouseEnter(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = false;
            BtnPayment.BackColor = Color.YellowGreen;
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_MouseLeave(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = true;
            BtnPayment.BackColor = Color.FromArgb(51, 153, 255);
            BtnPayment.ForeColor = Color.White;
        }

        private void btnReset_Enter(object sender, EventArgs e)
        {
            btnReset.UseVisualStyleBackColor = false;
            btnReset.BackColor = Color.FromArgb(250, 185, 34);
            btnReset.ForeColor = Color.White;
        }

        private void btnReset_Leave(object sender, EventArgs e)
        {
            btnReset.UseVisualStyleBackColor = true;
            btnReset.BackColor = Color.FromArgb(51, 153, 255);
            btnReset.ForeColor = Color.White;
        }

        private void btnReset_MouseEnter(object sender, EventArgs e)
        {
            btnReset.UseVisualStyleBackColor = false;
            btnReset.BackColor = Color.FromArgb(250, 185, 34);
            btnReset.ForeColor = Color.White;
        }

        private void btnReset_MouseLeave(object sender, EventArgs e)
        {
            btnReset.UseVisualStyleBackColor = true;
            btnReset.BackColor = Color.FromArgb(51, 153, 255);
            btnReset.ForeColor = Color.White;
        }

        private void btndelete_Enter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(255, 77, 77);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_Leave(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = true;
            btndelete.BackColor = Color.FromArgb(51, 153, 255);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_MouseEnter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(255, 77, 77);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_MouseLeave(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = true;
            btndelete.BackColor = Color.FromArgb(51, 153, 255);
            btndelete.ForeColor = Color.White;
        }

        private void btnCalculator_Enter(object sender, EventArgs e)
        {
            btnCalculator.UseVisualStyleBackColor = false;
            btnCalculator.BackColor = Color.FromArgb(176, 111, 193);
            btnCalculator.ForeColor = Color.White;
        }

        private void btnCalculator_Leave(object sender, EventArgs e)
        {
            btnCalculator.UseVisualStyleBackColor = true;
            btnCalculator.BackColor = Color.FromArgb(51, 153, 255);
            btnCalculator.ForeColor = Color.White;
        }

        private void btnCalculator_MouseEnter(object sender, EventArgs e)
        {
            btnCalculator.UseVisualStyleBackColor = false;
            btnCalculator.BackColor = Color.FromArgb(176, 111, 193);
            btnCalculator.ForeColor = Color.White;
        }

        private void btnCalculator_MouseLeave(object sender, EventArgs e)
        {
            btnCalculator.UseVisualStyleBackColor = true;
            btnCalculator.BackColor = Color.FromArgb(51, 153, 255);
            btnCalculator.ForeColor = Color.White;
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

        private void cmbterms_KeyUp(object sender, KeyEventArgs e)
        {
            //searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //// If the Search string is greater than 1 then use custom logic
            //if (searchstr.Length > 1)
            //{
            //    int index;
            //    // Search the Item that matches the string typed
            //    index = cmbterms.FindString(searchstr);
            //    // Select the Item in the Combo
            //    cmbterms.SelectedIndex = index;
            //}
        }

        private void cmbbatch_KeyUp(object sender, KeyEventArgs e)
        {
            //searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //// If the Search string is greater than 1 then use custom logic
            //if (searchstr.Length > 1)
            //{
            //    int index;
            //    // Search the Item that matches the string typed
            //    index = cmbbatch.FindString(searchstr);
            //    // Select the Item in the Combo
            //    cmbbatch.SelectedIndex = index;
            //}
        }

        private void cmbcharper_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbcharper.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbcharper.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var privouscontroal = txtitemname;
            activecontroal = privouscontroal.Name;
            Itementry client = new Itementry(this, master, tabControl, activecontroal);
            client.Passed(1);
            //client.Show();
            master.AddNewTab(client);
            // autoreaderbind();
            txtitemname.Focus();
        }

        private void btnGroupEdit_Click(object sender, EventArgs e)
        {
            if (txtitemname.Text != "")
            {
                // SqlDataAdapter da = new SqlDataAdapter();
                // DataTable dtitem = new DataTable();
                SqlCommand cmd = new SqlCommand("select productid from productmaster where isactive=1 and Product_Name='" + txtitemname.Text + "' and isactive=1", con);
                SqlDataAdapter sda6 = new SqlDataAdapter(cmd);
                DataTable dtitem = new DataTable();
                sda6.Fill(dtitem);
                if (dtitem.Rows.Count > 0)
                {
                    var privouscontroal = txtitemname;
                    activecontroal = privouscontroal.Name;
                    Itementry client = new Itementry(this, master, tabControl, activecontroal);
                    client.Updatefromsale(txtitemname.Text);
                    client.Passed(1);
                    //client.Show();
                    master.AddNewTab(client);
                }
                else
                {
                    MessageBox.Show("Please Enter/Select Valid Item.");
                }
            }
            else
            {
                MessageBox.Show("Please Enter/Select Item.");
            }
        }

        private void btnAddPartyName_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbcustname;
            activecontroal = privouscontroal.Name;
            Accountentry client = new Accountentry(this, master, tabControl, activecontroal, strfinalarray[0]);
            client.Passed(1);
            master.AddNewTab(client);
        }

        private void btnEditPartyName_Click(object sender, EventArgs e)
        {
            if (cmbcustname.Text != "" && cmbcustname.Text != null)
            {
                var privouscontroal = cmbcustname;
                activecontroal = privouscontroal.Name;
                string iid = cmbcustname.SelectedValue.ToString();
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
            else
            {
                MessageBox.Show("Please Select PartyName.");
            }
        }

        private void btnAddSaleType_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbsaletype;
            activecontroal = privouscontroal.Name;
            if (strfinalarray[0] == "S")
            {
                SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                //pt.Show();
                master.AddNewTab(pt);
            }
            else if (strfinalarray[0] == "P")
            {
                PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                master.AddNewTab(p);
            }
            else if (strfinalarray[0] == "SR")
            {
                SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                //pt.Show();
                master.AddNewTab(pt);
            }
            else if (strfinalarray[0] == "PR")
            {
                PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                master.AddNewTab(p);
            }
        }

        private void btnEditSaleType_Click(object sender, EventArgs e)
        {
            if (cmbsaletype.Text != "" && cmbsaletype.Text != null)
            {
                var privouscontroal = cmbsaletype;
                activecontroal = privouscontroal.Name;
                if (strfinalarray[0] == "S")
                {
                    SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                    pt.updatemode("Sale", cmbsaletype.Text, "S");
                    // pt.Show();
                    master.AddNewTab(pt);
                }
                else if (strfinalarray[0] == "SR")
                {
                    SaletypeEntry pt = new SaletypeEntry(this, master, tabControl, activecontroal);
                    pt.updatemode("Sale", cmbsaletype.Text, "SR");
                    // pt.Show();
                    master.AddNewTab(pt);
                }
                else if (strfinalarray[0] == "P")
                {
                    PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                    p.updatemode("Purchase", cmbsaletype.Text, "P");
                    // pt.Show();
                    master.AddNewTab(p);
                }
                else if (strfinalarray[0] == "PR")
                {
                    PurchaseTypeEntry p = new PurchaseTypeEntry(this, master, tabControl, activecontroal);
                    p.updatemode("Purchase", cmbsaletype.Text, "PR");
                    // pt.Show();
                    master.AddNewTab(p);
                }
            }
            else
            {
                MessageBox.Show("Please Select Type");
            }
        }

        private void btnAddPerticular_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbcharper;
            activecontroal = privouscontroal.Name;
            ChargesHead popup = new ChargesHead(this, cmbcharper.Text, master, tabControl, activecontroal);
            master.AddNewTab(popup);
        }

        private void btnEditPerticular_Click(object sender, EventArgs e)
        {
            if (cmbcharper.Text != "" && cmbcharper.Text != null)
            {
                var privouscontroal = cmbcharper;
                activecontroal = privouscontroal.Name;
                ChargesHead popup = new ChargesHead(this, cmbcharper.Text, master, tabControl, activecontroal);
                popup.Update(1, cmbcharper.Text);
                master.AddNewTab(popup);
            }
            else
            {
                MessageBox.Show("Please Select Perticular Charges");
            }
        }


        private void txtitemname_KeyUp(object sender, KeyEventArgs e)
        {

        }



        private void lvallitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
                //   lvallitem.Items[0].Selected = false;
                txtitemname.Text = str;
                txtitemname.Focus();
                enteritem();
            }
        }

        private void txtitemname_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //   // lvallitem.Items[0].Selected = true;
            //    SqlCommand cmd = new SqlCommand("select Product_Name from productmaster where Product_Name like'%" + txtitemname.Text + "%' and isactive=1", con);
            //    SqlDataAdapter sda6 = new SqlDataAdapter(cmd);
            //    DataTable dtitem = new DataTable();
            //    sda6.Fill(dtitem);
            //    lvallitem.Items.Clear();
            //    for (int i = 0; i < dtitem.Rows.Count; i++)
            //    {
            //        ListViewItem li;
            //        li = lvallitem.Items.Add(dtitem.Rows[i]["Product_Name"].ToString());
            //    }
            //    //  lvallitem.Focus();
            //    if (txtitemname.Text == "" && txtitemname.Text == null)
            //    {
            //        bindallitem();
            //    }
            //}
            //catch
            //{
            //}
        }
        public void GridDesign()
        {

            this.grditem.DefaultCellStyle.Font = new Font("Calibri", 11, FontStyle.Regular);
            grditem.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 11.25f, FontStyle.Bold);
            DataGridViewCellStyle fooCellStyle = new DataGridViewCellStyle();
            DataGridViewHeaderCell f = new DataGridViewHeaderCell();

            grditem.ColumnHeadersDefaultCellStyle.BackColor = Color.Maroon;
            grditem.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grditem.DefaultCellStyle.ForeColor = Color.White;
            grditem.EnableHeadersVisualStyles = false;

            //DataGridViewColumn dataGridViewColumn = dgvitem.Columns[2];
            //dataGridViewColumn.HeaderCell.Style.BackColor = Color.Green;


            foreach (DataGridViewRow row in grditem.Rows)
            {
                row.Cells[0].Style.ForeColor = Color.Maroon;
            }
        }
        private void txtitemname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // lvallitem.Items[0].Selected = true;
                SqlCommand cmd = new SqlCommand("select Product_Name from productmaster where Product_Name like'%" + txtitemname.Text + "%' and isactive=1", con);
                SqlDataAdapter sda6 = new SqlDataAdapter(cmd);
                DataTable dtitem = new DataTable();
                sda6.Fill(dtitem);
                lvallitem.Items.Clear();
                for (int i = 0; i < dtitem.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = lvallitem.Items.Add(dtitem.Rows[i]["Product_Name"].ToString());
                    grditem.DataSource = dtitem;
                }
                //  lvallitem.Focus();
                if (txtitemname.Text == "" && txtitemname.Text == null)
                {
                    bindallitem();
                }
                //  grditem.Focus();
                grditem.Rows[0].Selected = false;
                GridDesign();
            }
            catch
            {
            }
        }

        private void lvallitem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
            //lvallitem.Items[0].Selected = false;
            txtitemname.Text = str;
            txtitemname.Focus();
            enteritem();
        }

        private void cmbterms_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbterms.Text = s;
            }
            catch
            {
            }
        }

        private void cmbcustname_Leave(object sender, EventArgs e)
        {
            cmbcustname.Text = s;
        }

        private void cmbsaletype_Leave(object sender, EventArgs e)
        {
            cmbsaletype.Text = s;
        }

        private void cmbbatch_Leave(object sender, EventArgs e)
        {
            cmbbatch.Text = s;
        }

        private void cmbcharper_Leave(object sender, EventArgs e)
        {
            cmbcharper.Text = s;
        }
        internal void bindbatchfromform(string batchno)
        {
            this.ActiveControl = cmbbatch;
            cmbbatch.Text = batchno;
        }

        private void tabcharges_Enter(object sender, EventArgs e)
        {
            if (cmbcharper.Items.Count > 0)
            {
                // cmbcharper.Focus();
                this.ActiveControl = cmbcharper;
                cmbcharper.SelectedIndex = 0;
                cmbcharper.DroppedDown = true;
            }
        }

        private void cmbterms_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbterms.Items.Count; i++)
                {
                    s = cmbterms.GetItemText(cmbterms.Items[i]);
                    if (s == cmbterms.Text)
                    {
                        inList = true;
                        cmbterms.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbterms.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbcustname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbcustname.Items.Count; i++)
                {
                    s = cmbcustname.GetItemText(cmbcustname.Items[i]);
                    if (s == cmbcustname.Text)
                    {
                        inList = true;
                        cmbcustname.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbcustname.Text = "";
                }
                // getduedate();
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbsaletype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbsaletype.Items.Count; i++)
                {
                    s = cmbsaletype.GetItemText(cmbsaletype.Items[i]);
                    if (s == cmbsaletype.Text)
                    {
                        inList = true;
                        cmbsaletype.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbsaletype.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
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

        private void cmbagentname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbagentname.Items.Count; i++)
                {
                    s = cmbagentname.GetItemText(cmbagentname.Items[i]);
                    if (s == cmbagentname.Text)
                    {
                        inList = true;
                        cmbagentname.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbagentname.Text = "";
                }
                pnlagent.Visible = false;
                TxtBillNo.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbagentname;
                activecontroal = privouscontroal.Name;
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal, strfinalarray[0]);

                client.Passed(1);
                //   client.Show();
                master.AddNewTab(client);
            }
            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = cmbagentname;
                activecontroal = privouscontroal.Name;
                string iid = cmbagentname.SelectedValue.ToString();

                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
        }


        private void cmbagentname_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbagentname.Text = s;
            }
            catch
            {
            }
        }

        private void btncharadditem_Enter(object sender, EventArgs e)
        {
            btncharadditem.UseVisualStyleBackColor = false;
            btncharadditem.BackColor = Color.FromArgb(9, 106, 3);
            btncharadditem.ForeColor = Color.White;
        }

        private void btncharadditem_MouseEnter(object sender, EventArgs e)
        {
            btncharadditem.UseVisualStyleBackColor = false;
            btncharadditem.BackColor = Color.FromArgb(9, 106, 3);
            btncharadditem.ForeColor = Color.White;
        }

        private void btncharadditem_Leave(object sender, EventArgs e)
        {
            btncharadditem.UseVisualStyleBackColor = true;
            btncharadditem.BackColor = Color.FromArgb(51, 153, 255);
            btncharadditem.ForeColor = Color.White;
        }

        private void btncharadditem_MouseLeave(object sender, EventArgs e)
        {
            btncharadditem.UseVisualStyleBackColor = true;
            btncharadditem.BackColor = Color.FromArgb(51, 153, 255);
            btncharadditem.ForeColor = Color.White;
        }
        public bool isretrivesale = false;
        private void txtorgbillno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Convert.ToBoolean(options.Rows[0]["retrivesalepurchasereturn"].ToString()) == true)
                {
                    // if (Convert.ToBoolean(options.Rows[0]["retrivesalepurchasereturn"].ToString()) == true)
                    // {
                    if (txtorgbillno.Text != "")
                    {
                        isretrivesale = true;
                        updatemode(txtorgbillno.Text, txtorgbillno.Text, Convert.ToInt32(cmbcustname.SelectedValue), strfinalarray);
                    }
                    else
                    {
                        MessageBox.Show("Please enter billno");
                    }
                }
                txtorgbilldate.Focus();
                //  }
                //  txtorgbilldate.Focus();
            }
        }

        private void txtorgbilldate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtitemname.Focus();
                pnlorgbillno.Visible = false;
            }
        }

        private void lvallitem_MouseClick(object sender, MouseEventArgs e)
        {
            String str = lvallitem.Items[lvallitem.FocusedItem.Index].SubItems[0].Text;
            //lvallitem.Items[0].Selected = false;
            txtitemname.Text = str;
            txtitemname.Focus();
            enteritem();
        }

        private void grditem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtitemname.Text = ((DataGridView)sender).CurrentRow.Cells[0].Value.ToString();
                txtitemname.Focus();
                grditem.Rows[0].Selected = false;
                enteritem();
            }
        }

        private void grditem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtitemname.Text = ((DataGridView)sender).CurrentRow.Cells[0].Value.ToString();
            txtitemname.Focus();
            grditem.Rows[0].Selected = false;
            enteritem();
        }

        private void grditem_MouseClick(object sender, MouseEventArgs e)
        {
            txtitemname.Text = ((DataGridView)sender).CurrentRow.Cells[0].Value.ToString();
            txtitemname.Focus();
            grditem.Rows[0].Selected = false;
            enteritem();
        }

        private void txtcustomername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtaddress.Focus();
            }
        }

        private void txtaddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcity.Focus();
            }
        }

        private void txtcity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtphone.Focus();
            }
        }

        private void txtphone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtmobile.Focus();
            }
        }

        private void txtmobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpanno.Focus();
            }
        }

        private void txtpanno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtadharno.Focus();
            }
        }

        private void txtadharno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pnlcusdetails.Visible = false;
                txtitemname.Focus();
            }
        }

        private void txtmobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtf1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "1")
                {
                    txtpono.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf2.Focus();
                }
            }
        }

        private void txtf2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "2")
                {
                    txtpono.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf3.Focus();
                }
            }
        }

        private void txtf3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "3")
                {
                    txtpono.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf4.Focus();
                }
            }
        }

        private void txtf4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "4")
                {
                    txtpono.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf5.Focus();
                }
            }
        }

        private void txtf5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "5")
                {
                    txtpono.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf6.Focus();
                }
            }
        }

        private void txtf6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "6")
                {
                    txtpono.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf7.Focus();
                }
            }
        }

        private void txtf7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "7")
                {
                    txtpono.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf8.Focus();
                }
            }
        }

        private void txtf8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "8")
                {
                    txtpono.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf9.Focus();
                }
            }
        }

        private void txtf9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "9")
                {
                    txtpono.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf10.Focus();
                }
            }
        }

        private void txtf10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "10")
                {
                    txtpono.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf11.Focus();
                }
            }
        }

        private void txtf11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "11")
                {
                    txtpono.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf12.Focus();
                }
            }
        }

        private void txtf12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "12")
                {
                    txtpono.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf13.Focus();
                }
            }
        }

        private void txtf13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "13")
                {
                    txtpono.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf14.Focus();
                }
            }
        }

        private void txtf14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (qp.Rows[0]["nooffields"].ToString() == "14")
                {
                    txtpono.Focus();
                    pnladditional.Visible = false;
                }
                else
                {
                    txtf15.Focus();
                }
            }
        }

        private void txtf15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpono.Focus();
                pnladditional.Visible = false;
            }
        }

        private void txtboxsrno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pnlboxsrno.Visible = false;
                txtfree.Focus();
            }
        }
    }

}
