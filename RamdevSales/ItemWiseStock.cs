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
    public partial class ItemWiseStock : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        Printing prn = new Printing();
        public ItemWiseStock()
        {
            InitializeComponent();
        }
        int cid = 0;
        public ItemWiseStock(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        DataTable userrights = new DataTable();
        private void ItemWiseStock_Load(object sender, EventArgs e)
        {
            userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");

            if (cid == 0)
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[10]["p"].ToString() == "False")
                    {
                        btngenrpt.Enabled = false;
                    }
                }
                //DataSet dtrange = ods.getdata("SELECT SQLSetting.* FROM SQLSetting where OT6='" + Master.companyId + "'");
                //DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0][8].ToString());
                //DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0][9].ToString());
                //DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0][8].ToString());
                //DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0][9].ToString());
                //DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0][8].ToString());
                DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                con.Open();
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(0, 0);
                LVledger.Columns.Add("Date", 100, HorizontalAlignment.Center);
                LVledger.Columns.Add("Type", 100, HorizontalAlignment.Center);
                LVledger.Columns.Add("Bill No", 70, HorizontalAlignment.Left);
                LVledger.Columns.Add("Name", 370, HorizontalAlignment.Left);
                LVledger.Columns.Add("Price", 100, HorizontalAlignment.Right);
                LVledger.Columns.Add("Qty In", 100, HorizontalAlignment.Right);
                LVledger.Columns.Add("Oty Out", 100, HorizontalAlignment.Right);
                LVledger.Columns.Add("balance", 140, HorizontalAlignment.Right);
                LVledger.Columns.Add("ClientID", 140, HorizontalAlignment.Right);
                LVledger.Columns.Add("Batch", 140, HorizontalAlignment.Right);

                //  listviewbind();
                //  binditems();
                con.Close();
                mouseclickid.Columns.Add("type", typeof(string));
                mouseclickid.Columns.Add("id", typeof(string));
                // autoreaderbind();
                bindallitem();
                DTPFrom.CustomFormat = Master.dateformate;
                DTPTo.CustomFormat = Master.dateformate;

                //set the interval  and start the timer
                timer1.Interval = 1000;
                timer1.Start();
                this.ActiveControl = txtitem;
            }
            else
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[10]["p"].ToString() == "False")
                    {
                        btngenrpt.Enabled = false;
                    }
                }
            }
        }
        DataTable mouseclickid = new DataTable();
        public void binditems()
        {
            SqlCommand cmd1 = new SqlCommand("select ProductID,Product_Name from ProductMaster where isactive=1 order by Product_Name", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbaccname.ValueMember = "ProductID";
            cmbaccname.DisplayMember = "Product_Name";
            cmbaccname.DataSource = dt1;
            cmbaccname.SelectedIndex = -1;


            //  autobind(dt1, cmbaccname);
        }
        private void autobind(DataTable dt1, ComboBox cmbcustname)
        {
            string[] arr = new string[dt1.Rows.Count];
            //  string list="";
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                arr[i] = dt1.Rows[i][1].ToString();
            }

            //    var stringArr = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();

            cmbcustname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbcustname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbcustname.AutoCompleteCustomSource.AddRange(arr);
        }
        public event EventHandler<EventArgs> Canceled;
        private Master master;
        private TabControl tabControl;
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
        decimal debit1 = 0;
        decimal credit1 = 0;

        int i;
        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            LVledger.Items.Clear();
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[10]["a"].ToString() == "True")
                    {
                        if (txtitem.Text != "")
                        {
                            if (chkshowall.Checked == true)
                            {
                                EventHandler<EventArgs> ea = Canceled;
                                if (ea != null)
                                    ea(this, e);
                                filelength = 1;
                                progressBar1.Value = 0;
                                i = 0;
                                timer1.Interval = 1000;
                                timer1.Start();
                                timer1.Tick += new EventHandler(timer2_Tick);
                                #region
                                //for calculate OpBalance
                                //#region
                                //string proid = conn.ExecuteScalar("select Productid from ProductMaster where isactive=1 and Product_Name='" + txtitem.Text + "'");
                                //string openingstockfromitem = conn.ExecuteScalar("select opstock from productpricemaster where isactive=1 and productid= '" + proid + "'");
                                //string totalPurchase = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='P'");
                                //string totalPR = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='PR'");
                                //string totalSR = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='SR'");
                                //string totalPC = conn.ExecuteScalar("select sum(pqty) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='PC' and s.OrderStatus='Pending'");
                                //string totalSC = conn.ExecuteScalar("select sum(pqty) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='SC' and s.OrderStatus='Pending'");
                                //string totalSale = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='S'");
                                //if (totalPurchase == "" || totalPurchase == "NULL")
                                //{
                                //    totalPurchase = "0.00";
                                //}
                                //if (totalSale == "" || totalSale == "NULL")
                                //{
                                //    totalSale = "0.00";
                                //}
                                //if (totalPR == "" || totalPR == "NULL")
                                //{
                                //    totalPR = "0.00";
                                //}
                                //if (totalSR == "" || totalSR == "NULL")
                                //{
                                //    totalSR = "0.00";
                                //}
                                //if (totalPC == "" || totalPC == "NULL")
                                //{
                                //    totalPC = "0.00";
                                //}
                                //if (totalSC == "" || totalSC == "NULL")
                                //{
                                //    totalSC = "0.00";
                                //}
                                //totalPurchase = (Convert.ToDouble(totalPurchase) + Convert.ToDouble(totalSR) + Convert.ToDouble(totalPC)).ToString();
                                //totalSale = (Convert.ToDouble(totalSale) + Convert.ToDouble(totalPR) + Convert.ToDouble(totalSC)).ToString();

                                //Double opbal;
                                //string DC = "";
                                //if (Convert.ToDouble(totalPurchase) >= Convert.ToDouble(totalSale))
                                //{
                                //    opbal = Convert.ToDouble(openingstockfromitem) + Convert.ToDouble(totalPurchase) - Convert.ToDouble(totalSale);
                                //    txtopbal.Text = opbal.ToString("N2");
                                //    DC = "D";
                                //}
                                //else
                                //{
                                //    opbal = Convert.ToDouble(openingstockfromitem) - Convert.ToDouble(totalSale) + Convert.ToDouble(totalPurchase);
                                //    txtopbal.Text = opbal.ToString("N2");
                                //    DC = "C";
                                //}

                                ////for (int i = 0; i < OPdt.Rows.Count; i++)
                                ////{
                                ////    Double opbal = 0;
                                ////    if (OPdt.Rows[i]["DC"].ToString() == "D")
                                ////    {

                                ////    }
                                ////    else if (OPdt.Rows[i]["TranType"].ToString() == "Rect")
                                ////    {
                                ////        ListViewItem li;
                                ////        li = LVledger.Items.Add(Convert.ToDateTime(OPdt.Rows[i]["Date1"].ToString()).ToString("dd-MMM-yyyy"));
                                ////        li.SubItems.Add("Rect");
                                ////        li.SubItems.Add(OPdt.Rows[i]["OT1"].ToString());
                                ////        li.SubItems.Add("By Rcpt. No.: " + OPdt.Rows[i]["VoucherID"].ToString() + "; " + OPdt.Rows[i]["ShortNarration"].ToString());
                                ////        li.SubItems.Add("");
                                ////        li.SubItems.Add(Math.Round(Convert.ToDouble(OPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                ////        li.SubItems.Add("Rect");
                                ////    }
                                ////}
                                //#endregion

                                //for create ledger
                                //mouseclickid.Rows.Clear();
                                //LVledger.Items.Clear();
                                //#region
                                //DataTable pos = conn.getdataset("select 'POS' as Billtype,b.BillDate as Bill_Run_Date,b.billno,b.totalnet as Rate,bp.qty as pqty,b.BillId as Bill_No  from BillPOSMaster b inner join BillPOSProductMaster bp on b.billid=bp.billid where b.isactive=1 and bp.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and bp.itemname='" + txtitem.Text + "' order by BillDate asc");
                                //pos.Columns.Add("ClientID", typeof(String));
                                ////DataTable pos = conn.getdataset("select 'POS' as Billtype,BillDate as Bill_Run_Date,billno,totalnet as Rate,totalqty as pqty,BillId as Bill_No  from BillPOSMaster where isactive=1 and BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by BillDate asc");
                                //pos = changedtclone(pos);

                                ////DataTable SPdt1 = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No from SaleOrderProductMaster where isactive=1 and OrderStatus='Pending' and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' order by bill_Run_Date");
                                //DataTable SPdt1 = conn.getdataset("select so.Billtype,so.Bill_Run_Date,so.billno,so.Rate,so.Pqty,so.Bill_No,so.ClientID from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and s.Billtype='PC' and so.bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and so.productid='" + proid + "' order by so.bill_Run_Date");
                                //SPdt1 = changedtclone(SPdt1);
                                //DataTable SPdt2 = conn.getdataset("select so.Billtype,so.Bill_Run_Date,so.billno,so.Rate,so.Pqty,so.Bill_No,so.ClientID from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and s.Billtype='SC' and so.bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and so.productid='" + proid + "' order by so.bill_Run_Date");
                                //SPdt2 = changedtclone(SPdt2);
                                //DataTable SPdtsr = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No,ClientID from billproductmaster where isactive=1 and billtype='SR' and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' order by bill_Run_Date");
                                //SPdtsr = changedtclone(SPdtsr);
                                //DataTable SPdtpr = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No,ClientID from billproductmaster where isactive=1 and billtype='PR' and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' order by bill_Run_Date");
                                //SPdtpr = changedtclone(SPdtpr);
                                //DataTable SPdtp = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No,ClientID from billproductmaster where isactive=1 and billtype='P' and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' order by bill_Run_Date");
                                //SPdtp = changedtclone(SPdtp);
                                //DataTable SPdt = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No,ClientID from billproductmaster where isactive=1 and billtype='S' and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' order by bill_Run_Date");
                                //string balance = "0.00";
                                //Double a = Convert.ToDouble(balance) + opbal;
                                //balance = Convert.ToString(a);
                                //Double debit = 0, credit = 0;
                                //SPdt = changedtclone(SPdt);
                                //SPdt.Merge(pos);
                                //SPdt.Merge(SPdt1);
                                //SPdt.Merge(SPdt2);
                                //SPdt.Merge(SPdtsr);
                                //SPdt.Merge(SPdtpr);
                                //SPdt.Merge(SPdtp);
                                //SPdt.DefaultView.Sort = "[bill_Run_Date] asc";
                                //SPdt = SPdt.DefaultView.ToTable();
                                //debit = 0;
                                //credit = 0;
                                //debit1 = 0;
                                //credit1 = 0;
                                //for (int i = 0; i < SPdt.Rows.Count; i++)
                                //{
                                //    if (SPdt.Rows[i]["Billtype"].ToString() == "S")
                                //    {
                                //        ListViewItem li;
                                //        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                                //        li.SubItems.Add("Sale");
                                //        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                                //        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                                //        li.SubItems.Add(clientname);
                                //        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));

                                //        li.SubItems.Add("");
                                //        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                                //        debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());

                                //        mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["Bill_no"].ToString());
                                //        if (i != 0)
                                //        {
                                //            string[] str = balance.Split(' ');
                                //            char temp = str[0][0];
                                //            DC = temp.ToString();
                                //            opbal = Convert.ToDouble(str[0]);
                                //        }
                                //        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                                //        li.SubItems.Add(balance);
                                //        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                                //    }
                                //    else if (SPdt.Rows[i]["Billtype"].ToString() == "SC")
                                //    {
                                //        ListViewItem li;
                                //        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                                //        li.SubItems.Add("Sale Challan");
                                //        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                                //        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from SaleOrderMaster where billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                                //        li.SubItems.Add(clientname);
                                //        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));

                                //        li.SubItems.Add("");
                                //        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                                //        debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());

                                //        mouseclickid.Rows.Add("Sale Challan", SPdt.Rows[i]["Bill_no"].ToString());
                                //        if (i != 0)
                                //        {
                                //            string[] str = balance.Split(' ');
                                //            char temp = str[0][0];
                                //            DC = temp.ToString();
                                //            opbal = Convert.ToDouble(str[0]);
                                //        }
                                //        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                                //        li.SubItems.Add(balance);
                                //        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                                //    }
                                //    else if (SPdt.Rows[i]["Billtype"].ToString() == "POS")
                                //    {
                                //        ListViewItem li;
                                //        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                                //        li.SubItems.Add("POS Sale");
                                //        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                                //        string clientname = conn.ExecuteScalar("select customername from BillPOSMaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "'");
                                //        li.SubItems.Add(clientname);
                                //        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));

                                //        li.SubItems.Add("");
                                //        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                                //        debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());

                                //        mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["Bill_no"].ToString());
                                //        if (i != 0)
                                //        {
                                //            string[] str = balance.Split(' ');
                                //            char temp = str[0][0];
                                //            DC = temp.ToString();
                                //            opbal = Convert.ToDouble(str[0]);
                                //        }
                                //        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                                //        li.SubItems.Add(balance);
                                //        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                                //    }
                                //    else if (SPdt.Rows[i]["Billtype"].ToString() == "SR")
                                //    {
                                //        ListViewItem li;
                                //        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                                //        li.SubItems.Add("Sale Return");
                                //        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                                //        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                                //        li.SubItems.Add(clientname);
                                //        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                                //        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                                //        li.SubItems.Add("");
                                //        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                //        credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                                //        mouseclickid.Rows.Add("Sale Return", SPdt.Rows[i]["Pqty"].ToString());
                                //        if (i != 0)
                                //        {
                                //            string[] str = balance.Split(' ');
                                //            char temp = str[0][0];
                                //            DC = temp.ToString();
                                //            opbal = Convert.ToDouble(str[0]);
                                //        }
                                //        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                                //        li.SubItems.Add(balance);
                                //        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                                //    }
                                //    else if (SPdt.Rows[i]["Billtype"].ToString() == "P")
                                //    {
                                //        ListViewItem li;
                                //        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                                //        li.SubItems.Add("Purchase");
                                //        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                                //        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                                //        li.SubItems.Add(clientname);
                                //        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                                //        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                                //        li.SubItems.Add("");
                                //        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                //        credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                                //        mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["Pqty"].ToString());
                                //        if (i != 0)
                                //        {
                                //            string[] str = balance.Split(' ');
                                //            char temp = str[0][0];
                                //            DC = temp.ToString();
                                //            opbal = Convert.ToDouble(str[0]);
                                //        }
                                //        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                                //        li.SubItems.Add(balance);
                                //        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                                //    }
                                //    else if (SPdt.Rows[i]["Billtype"].ToString() == "PC")
                                //    {
                                //        ListViewItem li;
                                //        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                                //        li.SubItems.Add("Purchase Challan");
                                //        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                                //        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from SaleOrderMaster where billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                                //        li.SubItems.Add(clientname);
                                //        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                                //        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                                //        li.SubItems.Add("");
                                //        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                //        credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                                //        mouseclickid.Rows.Add("Purchase Challan", SPdt.Rows[i]["Pqty"].ToString());
                                //        if (i != 0)
                                //        {
                                //            string[] str = balance.Split(' ');
                                //            char temp = str[0][0];
                                //            DC = temp.ToString();
                                //            opbal = Convert.ToDouble(str[0]);
                                //        }
                                //        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                                //        li.SubItems.Add(balance);
                                //        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                                //    }
                                //    else if (SPdt.Rows[i]["Billtype"].ToString() == "PR")
                                //    {
                                //        ListViewItem li;
                                //        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                                //        li.SubItems.Add("Purchase Return");
                                //        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                                //        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                                //        li.SubItems.Add(clientname);
                                //        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                                //        li.SubItems.Add("");
                                //        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                                //        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                                //        debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                                //        mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["Pqty"].ToString());
                                //        if (i != 0)
                                //        {
                                //            string[] str = balance.Split(' ');
                                //            char temp = str[0][0];
                                //            DC = temp.ToString();
                                //            opbal = Convert.ToDouble(str[0]);
                                //        }
                                //        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                                //        li.SubItems.Add(balance);
                                //        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                                //    }

                                //}
                                //DataTable stockadjustment = conn.getdataset("select sm.*,sim.* from stockadujestmentmaster sm inner join stockadujestmentitemmaster sim on sm.id=sim.stockid where sm.isactive=1 and sim.isactive=1 and sim.itemid='" + proid + "' and sm.stockdate between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' order by sm.stockdate");
                                //for (int i = 0; i < stockadjustment.Rows.Count; i++)
                                //{
                                //    ListViewItem li;
                                //    li = LVledger.Items.Add(Convert.ToDateTime(stockadjustment.Rows[i]["stockdate"].ToString()).ToString("dd-MMM-yyyy"));
                                //    li.SubItems.Add("Stock Adjustment");
                                //    li.SubItems.Add("");
                                //    li.SubItems.Add(stockadjustment.Rows[i]["mainremark"].ToString());
                                //    li.SubItems.Add(Math.Round(Convert.ToDouble("0"), 2).ToString("N2"));
                                //    if (Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString()) > 0)
                                //    {
                                //        li.SubItems.Add((Math.Round(Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString()), 2)).ToString());
                                //        li.SubItems.Add("");
                                //        credit += Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                                //    }
                                //    else
                                //    {
                                //        li.SubItems.Add("");
                                //        li.SubItems.Add((Math.Round(Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString()), 2)).ToString());
                                //        Double d = Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                                //        Double a1 = d * -1;
                                //        debit += Convert.ToDouble(a1);
                                //    }
                                //    Double bal = Convert.ToDouble(balance);
                                //    Double astock = Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                                //    Double fbalance = bal + astock;
                                //    balance = Convert.ToString(fbalance);
                                //    li.SubItems.Add(balance);
                                //    li.SubItems.Add("1");
                                //}
                                //#endregion

                                //txttotdebit.Text = debit.ToString("N2");
                                //txttotcredit.Text = credit.ToString("N2");
                                //txtbalance.Text = balance;

                                #endregion
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(cmbbatch.Text))
                                {
                                    EventHandler<EventArgs> ea = Canceled;
                                    if (ea != null)
                                        ea(this, e);
                                    filelength = 1;
                                    progressBar1.Value = 0;
                                    i = 0;
                                    timer1.Interval = 1000;
                                    timer1.Start();
                                    timer1.Tick += new EventHandler(timer2_Tick);
                                }
                                else
                                {
                                    MessageBox.Show("Please Select Item Batch");
                                    txtitem.Focus();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please Select Item Name");
                            txtitem.Focus();
                        }
                    }
                }
            }
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
        internal void getitemname(int p, string iid)
        {
            try
            {
                if (iid != "")
                {
                    cid = 1;
                    DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                    DTPFrom.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                    DTPFrom.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                    DTPTo.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                    DTPTo.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                    DTPFrom.Value = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                    LVledger.Columns.Add("Date", 100, HorizontalAlignment.Center);
                    LVledger.Columns.Add("Type", 100, HorizontalAlignment.Center);
                    LVledger.Columns.Add("Bill No", 70, HorizontalAlignment.Left);
                    LVledger.Columns.Add("Name", 370, HorizontalAlignment.Left);
                    LVledger.Columns.Add("Price", 100, HorizontalAlignment.Right);
                    LVledger.Columns.Add("Qty In", 100, HorizontalAlignment.Right);
                    LVledger.Columns.Add("Qty Out", 100, HorizontalAlignment.Right);
                    LVledger.Columns.Add("balance", 140, HorizontalAlignment.Right);

                    //  listviewbind();
                    binditems();
                    mouseclickid.Columns.Add("type", typeof(string));
                    mouseclickid.Columns.Add("id", typeof(string));
                    txtitem.Text = iid;
                    DTPFrom.CustomFormat = Master.dateformate;
                    DTPTo.CustomFormat = Master.dateformate;
                    //set the interval  and start the timer
                    //timer1.Interval = 1000;
                    //timer1.Start();
                }
            }
            catch
            {
            }
        }
        public static string s;
        private void cmbaccname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbaccname.Items.Count; i++)
                {
                    s = cmbaccname.GetItemText(cmbaccname.Items[i]);
                    if (s == cmbaccname.Text)
                    {
                        inList = true;
                        cmbaccname.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbaccname.Text = "";
                }


                DataTable dt = new DataTable();
                dt = conn.getdataset("select Product_Name from ProductMaster order by Product_Name");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (txtitem.Text == dt.Rows[i]["Product_Name"].ToString())
                    {
                        DTPFrom.Focus();
                    }
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
                BtnViewReport.Focus();
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


        public void open()
        {
            if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Sale")
            {
                string[] strfinalarray = new string[5] { "S", "D", "Sale", "", "" };
                string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text.Remove(0, 9);

                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                DefaultSaleOrder bd1 = new DefaultSaleOrder(this, master, tabControl, strfinalarray);
                //   Sale p = new Sale(this, master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                }
                else if (dt1.Rows[0]["formname"].ToString() == bd1.Text)
                {
                    bd1.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd1);
                }




                //  bd.Show();
            }
            else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Sale Return")
            {
                string[] strfinalarray = new string[5] { "SR", "C", "Sale Return", "SR", "" };
                string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text.Remove(0, 9);
                int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                DefaultSaleOrder bd1 = new DefaultSaleOrder(this, master, tabControl, strfinalarray);
                //    Sale p = new Sale(this, master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                }
                else if (dt1.Rows[0]["formname"].ToString() == bd1.Text)
                {
                    bd1.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd1);
                }




                //  bd.Show();
            }
            else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Sale Challan")
            {
                string[] strfinalarray = new string[5] { "SC", "D", "Sale Challan", "SC", "" };
                string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text.Remove(0, 9);
                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                //DefaultPurchase frm = new DefaultPurchase(this);
                DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                DefaultSaleOrder bd1 = new DefaultSaleOrder(this, master, tabControl, strfinalarray);
                //Purchase p = new Purchase(this, master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                }
                else if (dt1.Rows[0]["formname"].ToString() == bd1.Text)
                {
                    bd1.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd1);
                }
            }
            else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Stock Out")
            {
             /*   string[] strfinalarray = new string[5] { "STO", "D", "Stock Out", "STO", "" };
                string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text.Remove(0, 9);
                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                //DefaultPurchase frm = new DefaultPurchase(this);
                //  DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                Stockinout bd = new Stockinout(this, master, tabControl, strfinalarray);
                DefaultSaleOrder bd1 = new DefaultSaleOrder(this, master, tabControl, strfinalarray);
                //Purchase p = new Purchase(this, master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                } */
                //else if (dt1.Rows[0]["formname"].ToString() == bd1.Text)
                //{
                //    bd1.updatemode(str, billno, clientid, strfinalarray);
                //    //bd.MdiParent = this.MdiParent;
                //    //bd.StartPosition = FormStartPosition.CenterScreen;
                //    //bd.Show();
                //    master.AddNewTab(bd1);
                //}
            }
            else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Purchase")
            {
                string[] strfinalarray = new string[5] { "P", "C", "Purchase", "", "" };
                string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text.Remove(0, 9);
                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                //DefaultPurchase frm = new DefaultPurchase(this);
                DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                DefaultSaleOrder bd1 = new DefaultSaleOrder(this, master, tabControl, strfinalarray);
                //   Purchase p = new Purchase(this, master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                }
                else if (dt1.Rows[0]["formname"].ToString() == bd1.Text)
                {
                    bd1.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd1);
                }

                //    DefaultPurchase bd = new DefaultPurchase(this);

                //  bd.Show();
            }
            else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Purchase Challan")
            {
                string[] strfinalarray = new string[5] { "PC", "C", "Purchase Challan", "PC", "" };
                string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text.Remove(0, 9);
                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                //DefaultPurchase frm = new DefaultPurchase(this);
                DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                DefaultSaleOrder bd1 = new DefaultSaleOrder(this, master, tabControl, strfinalarray);
                //Purchase p = new Purchase(this, master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                }
                else if (dt1.Rows[0]["formname"].ToString() == bd1.Text)
                {
                    bd1.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd1);
                }
            }
            else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Stock In")
            {
              /*  string[] strfinalarray = new string[5] { "STI", "C", "Stock In", "STI", "" };
                string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text.Remove(0, 9);
                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                //DefaultPurchase frm = new DefaultPurchase(this);
                //  DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                Stockinout bd = new Stockinout(this, master, tabControl, strfinalarray);
                DefaultSaleOrder bd1 = new DefaultSaleOrder(this, master, tabControl, strfinalarray);
                //Purchase p = new Purchase(this, master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                } */
                //else if (dt1.Rows[0]["formname"].ToString() == bd1.Text)
                //{
                //    bd1.updatemode(str, billno, clientid, strfinalarray);
                //    //bd.MdiParent = this.MdiParent;
                //    //bd.StartPosition = FormStartPosition.CenterScreen;
                //    //bd.Show();
                //    master.AddNewTab(bd1);
                //}
            }
            else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Purchase Return")
            {

                string[] strfinalarray = new string[5] { "PR", "D", "Purchase Return", "PR", "" };
                string billno = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text.Remove(0, 9);
                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text;
                int clientid = Convert.ToInt32(LVledger.Items[LVledger.FocusedItem.Index].SubItems[8].Text);
                DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='" + strfinalarray[0] + "' and setdefault=1");
                //DefaultPurchase frm = new DefaultPurchase(this);
                DefaultSale bd = new DefaultSale(this, master, tabControl, strfinalarray);
                DefaultSaleOrder bd1 = new DefaultSaleOrder(this, master, tabControl, strfinalarray);
                //Purchase p = new Purchase(this, master, tabControl);
                if (dt1.Rows[0]["formname"].ToString() == bd.Text)
                {
                    bd.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd);
                }
                else if (dt1.Rows[0]["formname"].ToString() == bd1.Text)
                {
                    bd1.updatemode(str, billno, clientid, strfinalarray);
                    //bd.MdiParent = this.MdiParent;
                    //bd.StartPosition = FormStartPosition.CenterScreen;
                    //bd.Show();
                    master.AddNewTab(bd1);
                }

            }
            else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Rect")
            {
                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text;

                QReceipt bd = new QReceipt();
                bd.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1, LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text);
                bd.Show();
            }
            else if (LVledger.Items[LVledger.FocusedItem.Index].SubItems[1].Text == "Pmnt")
            {
                String str = LVledger.Items[LVledger.FocusedItem.Index].SubItems[2].Text;

                QPayment bd = new QPayment();
                bd.updatemode(str, mouseclickid.Rows[LVledger.FocusedItem.Index][1].ToString(), 1, LVledger.Items[LVledger.FocusedItem.Index].SubItems[0].Text);
                bd.Show();
            }
        }
        private void LVledger_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (userrights.Rows[10]["v"].ToString() == "True" || userrights.Rows[10]["u"].ToString() == "True")
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[10]["u"].ToString() == "True")
                {
                    open();
                }
            }
            //else
            //{
            //    MessageBox.Show("You don't have Permission To View");
            //    return;
            //}
        }

        public void autoreaderbind()
        {
            try
            {
                AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();


                SqlDataReader dReader;
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandType = CommandType.Text;

                //start string

                String qry = "select ProductMaster.Product_Name from ProductMaster";
                //  con.Open();
                int count = 0;

                con.Close();
                qry = qry + " order by ProductMaster.Product_Name";

                if (count == 0)
                {
                    //end string
                    cmd1.CommandText = qry;


                    con.Open();
                    dReader = cmd1.ExecuteReader();

                    if (dReader.HasRows == true)
                    {
                        while (dReader.Read())
                            namesCollection.Add(dReader["Product_Name"].ToString());

                    }
                    else
                    {
                        //  MessageBox.Show("Data not found");
                    }
                    dReader.Close();

                    cmbaccname.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cmbaccname.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    cmbaccname.AutoCompleteCustomSource = namesCollection;
                }
                else
                {

                    //end string
                    cmd1.CommandText = qry;


                    //    con.Open();
                    dReader = cmd1.ExecuteReader();

                    if (dReader.HasRows == true)
                    {
                        while (dReader.Read())
                            namesCollection.Add(dReader["Product_Name"].ToString());

                    }
                    else
                    {
                        //MessageBox.Show("Data not found");
                    }
                    dReader.Close();

                    cmbaccname.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cmbaccname.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    cmbaccname.AutoCompleteCustomSource = namesCollection;
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

        private void LVledger_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtopbal_Enter(object sender, EventArgs e)
        {
            txtopbal.BackColor = Color.LightYellow;
        }

        private void txtopbal_Leave(object sender, EventArgs e)
        {
            txtopbal.BackColor = Color.White;
        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void BtnViewReport_MouseEnter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = Color.White;
        }

        private void BtnViewReport_MouseLeave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = Color.White;
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
            btngenrpt.BackColor = Color.FromArgb(51, 153, 255);
            btngenrpt.ForeColor = Color.White;
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

        private void BtnViewReport_Enter(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = false;
            BtnViewReport.BackColor = Color.FromArgb(94, 191, 174);
            BtnViewReport.ForeColor = Color.White;
        }

        private void BtnViewReport_Leave(object sender, EventArgs e)
        {
            BtnViewReport.UseVisualStyleBackColor = true;
            BtnViewReport.BackColor = Color.FromArgb(51, 153, 255);
            BtnViewReport.ForeColor = Color.White;
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
            btngenrpt.BackColor = Color.FromArgb(51, 153, 255);
            btngenrpt.ForeColor = Color.White;
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

        private void cmbaccname_Enter(object sender, EventArgs e)
        {
            if (cmbaccname.Items.Count > 0)
            {
                // cmbaccname.SelectedIndex = 0;
                // cmbaccname.DroppedDown = true;
            }
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

                    for (int i = 0; i < LVledger.Items.Count; i++)
                    {
                        string date = "", Name = "", Price = "", QtyIN = "", QtyOut = "", balance = "", ItemName = "", ItemFormat = "";
                        date = LVledger.Items[i].SubItems[0].Text;
                        Name = LVledger.Items[i].SubItems[3].Text;
                        Price = LVledger.Items[i].SubItems[4].Text;
                        QtyIN = LVledger.Items[i].SubItems[5].Text;
                        QtyOut = LVledger.Items[i].SubItems[6].Text;
                        balance = LVledger.Items[i].SubItems[7].Text;
                        ItemName = txtitem.Text;
                        ItemFormat = "ITEM WISE STOCK OF " + ItemName + " FROM " + DTPFrom.Text + " TO " + DTPTo.Text;
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19)VALUES";
                        qry += "('" + date + "','" + Name + "','" + Price + "','" + QtyIN + "','" + QtyOut + "','" + balance + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + ItemFormat + "','" + txttotcredit.Text + "','" + txttotdebit.Text + "','" + txtbalance.Text + "')";
                        prn.execute(qry);
                    }
                    string reportName = "ItemWiseStock";
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
        string searchstr;
        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    //empty the string for every 1 seconds
        //    // searchstr = "";
        //}

        private void cmbaccname_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbaccname.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbaccname.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbaccname_Leave(object sender, EventArgs e)
        {
            cmbaccname.Text = s;
        }

        private void cmbaccname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbaccname.Items.Count; i++)
                {
                    s = cmbaccname.GetItemText(cmbaccname.Items[i]);
                    if (s == cmbaccname.Text)
                    {
                        inList = true;
                        cmbaccname.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbaccname.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void LVledger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[10]["v"].ToString() == "True" || userrights.Rows[10]["u"].ToString() == "True")
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
        private void txtitem_Enter(object sender, EventArgs e)
        {
            try
            {
                //        if (txtitemname.Focused)
                //      {
                txtitem.BackColor = Color.LightYellow;
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
        public void bindallitem()
        {
            try
            {
                DataTable allitem = new DataTable();
                lvallitem.Items.Clear();
                allitem = conn.getdataset("Getitemname");
                grditem.DataSource = allitem;
                GridDesign();
                grditem.Rows[0].Selected = false;
                grditem.Columns[0].Width = 400;
                //allitem = conn.getdataset("select ProductMaster.Product_Name from ProductMaster where isactive=1 order by ProductMaster.Product_Name asc");
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
        int leaveflag = 0;
        private void txtitem_Leave(object sender, EventArgs e)
        {
            txtitem.BackColor = Color.White;
            try
            {

                if (leaveflag == 1)
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
                leaveflag = 0;
            }
            catch
            {
            }
        }

        private void txtitem_KeyDown(object sender, KeyEventArgs e)
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
                    leaveflag = 1;
                    bindbatch();
                    DTPFrom.Focus();

                }
            }
            catch
            {
            }
        }

        private void grditem_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                txtitem.Text = ((DataGridView)sender).CurrentRow.Cells[0].Value.ToString();
                txtitem.Focus();
                grditem.Rows[0].Selected = false;
            }
            catch
            {
            }
        }

        private void grditem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                txtitem.Text = ((DataGridView)sender).CurrentRow.Cells[0].Value.ToString();
                txtitem.Focus();
                grditem.Rows[0].Selected = false;
            }
            catch
            {
            }
        }

        private void grditem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtitem.Text = ((DataGridView)sender).CurrentRow.Cells[0].Value.ToString();
                    txtitem.Focus();
                    grditem.Rows[0].Selected = false;
                }
            }
            catch
            {
            }

        }

        private void txtitem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // lvallitem.Items[0].Selected = true;
                SqlCommand cmd = new SqlCommand("select Product_Name from productmaster where Product_Name like'%" + txtitem.Text + "%' and isactive=1", con);
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
                if (txtitem.Text == "" && txtitem.Text == null)
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

        private void DTPFrom_Enter(object sender, EventArgs e)
        {
            try
            {
                if (leaveflag == 0)
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
            }
            catch
            {
            }
        }

        static bool flag = false;
        int filelength = 1;
        Double debit = 0, credit = 0;
        string balance = "0.00";
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void _BindItemWiseStockList()
        {
            progressBar1.Maximum = 4;
            filelength = 4;
            balance = "0.00";
            //for calculate OpBalance
            if (chkshowall.Checked == true)
            {
                //calculate opening balance
                #region
                progressBar1.Increment(1);
                string proid = conn.ExecuteScalar("select Productid from ProductMaster where isactive=1 and Product_Name='" + txtitem.Text + "'");
                string openingstockfromitem = conn.ExecuteScalar("select sum(cast(opstock as float)) as opstock from productpricemaster where isactive=1 and productid= '" + proid + "'");
                string totalPurchase = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and Billtype='P' and isactive=1");
                string totalPR = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='PR'");
                string totalSR = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='SR'");
                string totalPC = conn.ExecuteScalar("select sum(pqty) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='PC' and s.OrderStatus='Pending'");
                string totalSC = conn.ExecuteScalar("select sum(pqty) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='SC' and s.OrderStatus='Pending'");
                string totalSTI = conn.ExecuteScalar("select sum(pqty) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='STI' and s.OrderStatus='Pending'");
                string totalSTO = conn.ExecuteScalar("select sum(pqty) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='STO' and s.OrderStatus='Pending'");
                string totalSale = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='S'");
                string totalGIN = conn.ExecuteScalar("select sum(qty) from tblgoodissuereturnnoteitemmaster where Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='GIN'");
                string totalGRN = conn.ExecuteScalar("select sum(qty) from tblgoodissuereturnnoteitemmaster where Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='GRN'");
                if (totalPurchase == "" || totalPurchase == "NULL")
                {
                    totalPurchase = "0.00";
                }
                if (totalSale == "" || totalSale == "NULL")
                {
                    totalSale = "0.00";
                }
                if (totalPR == "" || totalPR == "NULL")
                {
                    totalPR = "0.00";
                }
                if (totalSR == "" || totalSR == "NULL")
                {
                    totalSR = "0.00";
                }
                if (totalPC == "" || totalPC == "NULL")
                {
                    totalPC = "0.00";
                }
                if (totalSC == "" || totalSC == "NULL")
                {
                    totalSC = "0.00";
                }
                if (totalSC == "" || totalSC == "NULL")
                {
                    totalSC = "0.00";
                }
                if (totalSTI == "" || totalSTI == "NULL")
                {
                    totalSTI = "0.00";
                }
                if (totalSTO == "" || totalSTO == "NULL")
                {
                    totalSTO = "0.00";
                }
                if (totalGIN == "" || totalGIN == "NULL")
                {
                    totalGIN = "0.00";
                }
                if (totalGRN == "" || totalGRN == "NULL")
                {
                    totalGRN = "0.00";
                }
                totalPurchase = (Convert.ToDouble(totalPurchase) + Convert.ToDouble(totalSR) + Convert.ToDouble(totalPC).ToString() + Convert.ToDouble(totalSTI).ToString() + Convert.ToDouble(totalGRN)).ToString();
                totalSale = (Convert.ToDouble(totalSale) + Convert.ToDouble(totalPR) + Convert.ToDouble(totalSC).ToString() + Convert.ToDouble(totalSTO).ToString() + Convert.ToDouble(totalGIN)).ToString();

                Double opbal;
                string DC = "";
                if (Convert.ToDouble(totalPurchase) >= Convert.ToDouble(totalSale))
                {
                    opbal = Convert.ToDouble(openingstockfromitem) + Convert.ToDouble(totalPurchase) - Convert.ToDouble(totalSale);
                    txtopbal.Text = opbal.ToString("N2");
                    DC = "D";
                }
                else
                {
                    opbal = Convert.ToDouble(openingstockfromitem) - Convert.ToDouble(totalSale) + Convert.ToDouble(totalPurchase);
                    txtopbal.Text = opbal.ToString("N2");
                    DC = "C";
                }
                #endregion
                mouseclickid.Rows.Clear();
                LVledger.Items.Clear();

                #region

                progressBar1.Increment(1);
                DataTable pos = conn.getdataset("select 'POS' as Billtype,b.BillDate as Bill_Run_Date,b.billno,b.totalnet as Rate,bp.qty as pqty,b.BillId as Bill_No, bp.Batchno as Batch  from BillPOSMaster b inner join BillPOSProductMaster bp on b.billid=bp.billid where b.isactive=1 and bp.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and bp.itemname='" + txtitem.Text + "' order by BillDate asc");
                pos.Columns.Add("ClientID", typeof(String));
                //DataTable pos = conn.getdataset("select 'POS' as Billtype,BillDate as Bill_Run_Date,billno,totalnet as Rate,totalqty as pqty,BillId as Bill_No  from BillPOSMaster where isactive=1 and BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by BillDate asc");
                pos = changedtclone(pos);

                //DataTable SPdt1 = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No from SaleOrderProductMaster where isactive=1 and OrderStatus='Pending' and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' order by bill_Run_Date");
         //       DataTable SPdtSTI = conn.getdataset("select so.Billtype,so.Bill_Run_Date,so.billno,so.Rate,so.Pqty,so.Bill_No,so.ClientID from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and s.Billtype='STI' and so.bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and so.productid='" + proid + "' order by so.bill_Run_Date");
         //       SPdtSTI = changedtclone(SPdtSTI);
         //       DataTable SPdtSTO = conn.getdataset("select so.Billtype,so.Bill_Run_Date,so.billno,so.Rate,so.Pqty,so.Bill_No,so.ClientID from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and s.Billtype='STO' and so.bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and so.productid='" + proid + "' order by so.bill_Run_Date");
        //        SPdtSTO = changedtclone(SPdtSTO);
                DataTable SPdt1 = conn.getdataset("select so.Billtype,so.Bill_Run_Date,so.billno,so.Rate,so.Pqty,so.Bill_No,so.ClientID,so.Batch as Batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and s.Billtype='PC' and so.bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and so.productid='" + proid + "' order by so.bill_Run_Date");
                SPdt1 = changedtclone(SPdt1);
                DataTable SPdt2 = conn.getdataset("select so.Billtype,so.Bill_Run_Date,so.billno,so.Rate,so.Pqty,so.Bill_No,so.ClientID,so.Batch as Batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and s.Billtype='SC' and so.bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and so.productid='" + proid + "' order by so.bill_Run_Date");
                SPdt2 = changedtclone(SPdt2);
                DataTable SPdtsr = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No,ClientID,Batch as Batch from billproductmaster where isactive=1 and billtype='SR' and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' order by bill_Run_Date");
                SPdtsr = changedtclone(SPdtsr);
                DataTable SPdtpr = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No,ClientID,Batch as Batch from billproductmaster where isactive=1 and billtype='PR' and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' order by bill_Run_Date");
                SPdtpr = changedtclone(SPdtpr);
                DataTable SPdtp = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No,ClientID,Batch as Batch from billproductmaster where isactive=1 and billtype='P' and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' order by bill_Run_Date");
                SPdtp = changedtclone(SPdtp);
             //   DataTable SPdtGIN = conn.getdataset("select Billtype,Date,billno,0 as Rate,qty as Pqty,0 as Bill_No,ClientID from tblgoodissuereturnnoteitemmaster where isactive=1 and billtype='GIN' and Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' order by Date");
             //   SPdtGIN = changedtclone(SPdtGIN);
            //    DataTable SPdtGRN = conn.getdataset("select Billtype,Date,billno,0 as Rate,qty as Pqty,0 as Bill_No,ClientID from tblgoodissuereturnnoteitemmaster where isactive=1 and billtype='GRN' and Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' order by Date");
           //     SPdtGRN = changedtclone(SPdtGRN);
                DataTable SPdt = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No,ClientID,Batch as Batch from billproductmaster where isactive=1 and billtype='S' and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' order by bill_Run_Date");

                Double a = Convert.ToDouble(balance) + opbal;
                balance = Convert.ToString(a);

                SPdt = changedtclone(SPdt);
                SPdt.Merge(pos);
                SPdt.Merge(SPdt1);
                SPdt.Merge(SPdt2);
                SPdt.Merge(SPdtsr);
                SPdt.Merge(SPdtpr);
                SPdt.Merge(SPdtp);
           //     SPdt.Merge(SPdtSTI);
           //     SPdt.Merge(SPdtSTO);
           //     SPdt.Merge(SPdtGIN);
           //     SPdt.Merge(SPdtGRN);
                SPdt.AcceptChanges();



                DataTable dtCloned = SPdt.Clone();
                dtCloned.Columns[1].DataType = typeof(DateTime);
                foreach (DataRow row in SPdt.Rows)
                {
                    dtCloned.ImportRow(row);
                }
                SPdt = null;
                SPdt = dtCloned;
                SPdt.DefaultView.Sort = "[bill_Run_Date] asc";
                SPdt = SPdt.DefaultView.ToTable();
                debit = 0;
                credit = 0;
                debit1 = 0;
                credit1 = 0;
                for (int i = 0; i < SPdt.Rows.Count; i++)
                {
                    if (SPdt.Rows[i]["Billtype"].ToString() == "GIN")
                    {
                      /*  ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GIN");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select employeetype from tbluser_employeetype where isactive=1 and id=(select deptid from tblgoodissuereturnnotemaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(0), 2).ToString("N2"));

                        li.SubItems.Add("");
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["pqty"].ToString()), 2)).ToString());
                        debit += Convert.ToDouble(SPdt.Rows[i]["pqty"].ToString());

                        mouseclickid.Rows.Add("GIN", SPdt.Rows[i]["billno"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["pqty"].ToString()), DC, "D", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());  */
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "GRN")
                    {
                     /*   ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GRN");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select employeetype from tbluser_employeetype where isactive=1 and id=(select deptid from tblgoodissuereturnnotemaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(0), 2).ToString("N2"));
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        li.SubItems.Add("");
                        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                        mouseclickid.Rows.Add("GRN", SPdt.Rows[i]["Pqty"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());   */
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "S")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Sale");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));

                        li.SubItems.Add("");
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());

                        mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["Bill_no"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Batch"].ToString());
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "SC")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Sale Challan");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from SaleOrderMaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));

                        li.SubItems.Add("");
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());

                        mouseclickid.Rows.Add("Sale Challan", SPdt.Rows[i]["Bill_no"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Batch"].ToString());
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "STO")
                    {
                    /*    ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Stock Out");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from SaleOrderMaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));

                        li.SubItems.Add("");
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());

                        mouseclickid.Rows.Add("Stock Out", SPdt.Rows[i]["Bill_no"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());  */
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "POS")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("POS Sale");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select customername from BillPOSMaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "'");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));

                        li.SubItems.Add("");
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());

                        mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["Bill_no"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Batch"].ToString());
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "SR")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Sale Return");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        li.SubItems.Add("");
                        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                        mouseclickid.Rows.Add("Sale Return", SPdt.Rows[i]["Pqty"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Batch"].ToString());
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "P")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Purchase");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        li.SubItems.Add("");
                        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                        mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["Pqty"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Batch"].ToString());
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "PC")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Purchase Challan");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from SaleOrderMaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        li.SubItems.Add("");
                        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                        mouseclickid.Rows.Add("Purchase Challan", SPdt.Rows[i]["Pqty"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Batch"].ToString());
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "STI")
                    {
                      /*  ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Stock In");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from SaleOrderMaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        li.SubItems.Add("");
                        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                        mouseclickid.Rows.Add("Stock In", SPdt.Rows[i]["Pqty"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString()); */
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "PR")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Purchase Return");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                        li.SubItems.Add("");
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                        mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["Pqty"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Batch"].ToString());
                    }

                }
                //Production
                //  DataTable production = conn.getdataset("select * from tblproductionrawmaterialmaster where isactive=1 and productid='" + proid + "'");
                /*     DataTable production = conn.getdataset("select pm.*,p.date from tblproductionrawmaterialmaster pm inner join tblproductionmaster p on p.id=pm.productionid where p.isactive=1 and pm.isactive=1 and pm.productid='" + proid + "'");
                    for (int i = 0; i < production.Rows.Count; i++)
                    {
                       ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(production.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Production");
                        li.SubItems.Add("");
                        li.SubItems.Add("");
                        li.SubItems.Add(Math.Round(Convert.ToDouble("0"), 2).ToString("N2"));
                        //if (Convert.ToDouble(production.Rows[i]["rawqty"].ToString()) > 0)
                        //{
                        li.SubItems.Add("");
                        li.SubItems.Add((Math.Round(Convert.ToDouble(production.Rows[i]["rawqty"].ToString()), 2)).ToString());
                        debit += Convert.ToDouble(production.Rows[i]["rawqty"].ToString());
                        //}
                        //else
                        //{
                        //    li.SubItems.Add("");
                        //    li.SubItems.Add((Math.Round(Convert.ToDouble(production.Rows[i]["rawqty"].ToString()), 2)).ToString());
                        //    Double d = Convert.ToDouble(production.Rows[i]["rawqty"].ToString());
                        //    Double a1 = d * -1;
                        //    debit += Convert.ToDouble(a1);
                        //}
                        Double bal = Convert.ToDouble(balance);
                        Double astock = Convert.ToDouble(production.Rows[i]["rawqty"].ToString());
                        Double fbalance = bal - astock;
                        balance = Convert.ToString(fbalance);
                        li.SubItems.Add(balance);
                        li.SubItems.Add("1");  
                    }*/

                //fineshqty
           /*     DataTable fineshqty = conn.getdataset("select * from tblfinishedgoodsqty where isactive=1 and productid='" + proid + "'");
                for (int i = 0; i < fineshqty.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = LVledger.Items.Add(Convert.ToDateTime(fineshqty.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                    li.SubItems.Add("Finished Qty");
                    li.SubItems.Add("");
                    li.SubItems.Add("");
                    li.SubItems.Add(Math.Round(Convert.ToDouble("0"), 2).ToString("N2"));
                    //if (Convert.ToDouble(fineshqty.Rows[i]["fqty"].ToString()) > 0)
                    //{
                    li.SubItems.Add((Math.Round(Convert.ToDouble(fineshqty.Rows[i]["fqty"].ToString()), 2)).ToString());
                    li.SubItems.Add("");
                    credit += Convert.ToDouble(fineshqty.Rows[i]["fqty"].ToString());
                    //}
                    //else
                    //{
                    //    li.SubItems.Add("");
                    //    li.SubItems.Add((Math.Round(Convert.ToDouble(fineshqty.Rows[i]["fqty"].ToString()), 2)).ToString());
                    //    Double d = Convert.ToDouble(fineshqty.Rows[i]["fqty"].ToString());
                    //    Double a1 = d * -1;
                    //    debit += Convert.ToDouble(a1);
                    //}
                    Double bal = Convert.ToDouble(balance);
                    Double astock = Convert.ToDouble(fineshqty.Rows[i]["fqty"].ToString());
                    Double fbalance = bal + astock;
                    balance = Convert.ToString(fbalance);
                    li.SubItems.Add(balance);
                    li.SubItems.Add("1"); 
                }*/
                progressBar1.Increment(1);
                DataTable stockadjustment = conn.getdataset("select sm.*,sim.* from stockadujestmentmaster sm inner join stockadujestmentitemmaster sim on sm.id=sim.stockid where sm.isactive=1 and sim.isactive=1 and sim.itemid='" + proid + "' and sm.stockdate between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' order by sm.stockdate");
                for (int i = 0; i < stockadjustment.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = LVledger.Items.Add(Convert.ToDateTime(stockadjustment.Rows[i]["stockdate"].ToString()).ToString("dd-MMM-yyyy"));
                    li.SubItems.Add("Stock Adjustment");
                    li.SubItems.Add("");
                    li.SubItems.Add(stockadjustment.Rows[i]["mainremark"].ToString());
                    li.SubItems.Add(Math.Round(Convert.ToDouble("0"), 2).ToString("N2"));
                    if (Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString()) > 0)
                    {
                        li.SubItems.Add((Math.Round(Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString()), 2)).ToString());
                        li.SubItems.Add("");
                        credit += Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                    }
                    else
                    {
                        li.SubItems.Add("");
                        li.SubItems.Add((Math.Round(Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString()), 2)).ToString());
                        Double d = Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                        Double a1 = d * -1;
                        debit += Convert.ToDouble(a1);
                    }
                    Double bal = Convert.ToDouble(balance);
                    Double astock = Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                    Double fbalance = bal + astock;
                    balance = Convert.ToString(fbalance);
                    li.SubItems.Add(balance);
                    li.SubItems.Add("1");
                    li.SubItems.Add(stockadjustment.Rows[i]["Batch"].ToString());
                }
                #endregion
            }
            else
            {
                #region
                progressBar1.Increment(1);
                string proid = conn.ExecuteScalar("select PM.Productid from ProductMaster PM inner join ProductPriceMaster PPM on PM.ProductID=PPM.Productid where PM.isactive=1 and PPM.isactive=1 and Product_Name='" + txtitem.Text + "' and PropriceID='" + cmbbatch.SelectedValue.ToString() + "'");
                //select PM.Productid from ProductMaster PM inner join ProductPriceMaster PPM on PM.ProductID=PPM.Productid where PM.isactive=1 and PPM.isactive=1 and Product_Name like'%Micropin%' and PropriceID='6485'
                string openingstockfromitem = conn.ExecuteScalar("select opstock from productpricemaster where isactive=1 and productid= '" + proid + "' and ProPriceID='" + cmbbatch.SelectedValue.ToString() + "'");

                string totalPurchase = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='P' and Batchid='" + cmbbatch.SelectedValue.ToString() + "'");
                string totalPR = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='PR' and Batchid='" + cmbbatch.SelectedValue.ToString() + "'");
                string totalSR = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='SR' and Batchid='" + cmbbatch.SelectedValue.ToString() + "'");
                string totalPC = conn.ExecuteScalar("select sum(pqty) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='PC' and s.OrderStatus='Pending' and Batchid='" + cmbbatch.SelectedValue.ToString() + "'");
                string totalSC = conn.ExecuteScalar("select sum(pqty) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='SC' and s.OrderStatus='Pending' and Batchid='" + cmbbatch.SelectedValue.ToString() + "'");
            //    string totalSTI = conn.ExecuteScalar("select sum(pqty) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='STI' and s.OrderStatus='Pending' and Batch='" + cmbbatch.Text + "'");
            //    string totalSTO = conn.ExecuteScalar("select sum(pqty) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='STO' and s.OrderStatus='Pending' and Batch='" + cmbbatch.Text + "'");
                string totalSale = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='S' and Batchid='" + cmbbatch.SelectedValue.ToString() + "'");
          //      string totalGIN = conn.ExecuteScalar("select sum(qty) from tblgoodissuereturnnoteitemmaster where Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='GIN' and Batch='" + cmbbatch.Text + "'");
                //      string totalGRN = conn.ExecuteScalar("select sum(qty) from tblgoodissuereturnnoteitemmaster where Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='GRN' and Batch='" + cmbbatch.Text + "'");

                #region MyComment
              /*  string proid = conn.ExecuteScalar("select PM.Productid from ProductMaster PM inner join ProductPriceMaster PPM on PM.ProductID=PPM.Productid where PM.isactive=1 and PPM.isactive=1 and Product_Name='" + txtitem.Text + "' and PropriceID='" + cmbbatch.SelectedValue.ToString() + "'");
                //select PM.Productid from ProductMaster PM inner join ProductPriceMaster PPM on PM.ProductID=PPM.Productid where PM.isactive=1 and PPM.isactive=1 and Product_Name like'%Micropin%' and PropriceID='6485'
                string openingstockfromitem = conn.ExecuteScalar("select opstock from productpricemaster where isactive=1 and productid= '" + proid + "' and ProPriceID='" + cmbbatch.SelectedValue.ToString() + "'");

                string totalPurchase = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date >= '" + DTPFrom.Text + "' and bill_Run_Date<='" + DTPTo.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='P' and Batchid='" + cmbbatch.SelectedValue.ToString() + "'");
                string totalPR = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date >= '" + DTPFrom.Text + "' and bill_Run_Date<='" + DTPTo.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='PR' and Batchid='" + cmbbatch.SelectedValue.ToString() + "'");
                string totalSR = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date >= '" + DTPFrom.Text + "' and bill_Run_Date<='" + DTPTo.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='SR' and Batchid='" + cmbbatch.SelectedValue.ToString() + "'");
                string totalPC = conn.ExecuteScalar("select sum(pqty) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date >= '" + DTPFrom.Text + "' and bill_Run_Date<='" + DTPTo.Text + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='PC' and s.OrderStatus='Pending' and Batchid='" + cmbbatch.SelectedValue.ToString() + "'");
                string totalSC = conn.ExecuteScalar("select sum(pqty) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date >= '" + DTPFrom.Text + "' and bill_Run_Date<='" + DTPTo.Text + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='SC' and s.OrderStatus='Pending' and Batchid='" + cmbbatch.SelectedValue.ToString() + "'");
                //    string totalSTI = conn.ExecuteScalar("select sum(pqty) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='STI' and s.OrderStatus='Pending' and Batch='" + cmbbatch.Text + "'");
                //    string totalSTO = conn.ExecuteScalar("select sum(pqty) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='STO' and s.OrderStatus='Pending' and Batch='" + cmbbatch.Text + "'");
                string totalSale = conn.ExecuteScalar("select sum(pqty) from billproductmaster where bill_Run_Date >= '" + DTPFrom.Text + "' and bill_Run_Date<='" + DTPTo.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='S' and Batchid='" + cmbbatch.SelectedValue.ToString() + "'");
                //      string totalGIN = conn.ExecuteScalar("select sum(qty) from tblgoodissuereturnnoteitemmaster where Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='GIN' and Batch='" + cmbbatch.Text + "'");
                //      string totalGRN = conn.ExecuteScalar("select sum(qty) from tblgoodissuereturnnoteitemmaster where Date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and Billtype='GRN' and Batch='" + cmbbatch.Text + "'");  */
                #endregion MyComment
                if (totalPurchase == "" || totalPurchase == "NULL")
                {
                    totalPurchase = "0.00";
                }
                if (totalSale == "" || totalSale == "NULL")
                {
                    totalSale = "0.00";
                }
                if (totalPR == "" || totalPR == "NULL")
                {
                    totalPR = "0.00";
                }
                if (totalSR == "" || totalSR == "NULL")
                {
                    totalSR = "0.00";
                }
                if (totalPC == "" || totalPC == "NULL")
                {
                    totalPC = "0.00";
                }
                if (totalSC == "" || totalSC == "NULL")
                {
                    totalSC = "0.00";
                }
                if (totalSC == "" || totalSC == "NULL")
                {
                    totalSC = "0.00";
                }
              /*  if (totalSTI == "" || totalSTI == "NULL")
                {
                    totalSTI = "0.00";
                }
                if (totalSTO == "" || totalSTO == "NULL")
                {
                    totalSTO = "0.00";
                }
                if (totalGIN == "" || totalGIN == "NULL")
                {
                    totalGIN = "0.00";
                }
                if (totalGRN == "" || totalGRN == "NULL")
                {
                    totalGRN = "0.00";
                } */
               Double totalPurchase1 = Convert.ToDouble(totalPurchase) + Convert.ToDouble(totalSR) + Convert.ToDouble(totalPC);
               Double totalSale1 = Convert.ToDouble(totalSale) + Convert.ToDouble(totalPR) + Convert.ToDouble(totalSC);

                Double opbal;
                string DC = "";
                if (Convert.ToDouble(totalPurchase1) >= Convert.ToDouble(totalSale1))
                {
                    opbal = Convert.ToDouble(openingstockfromitem) + Convert.ToDouble(totalPurchase1) - Convert.ToDouble(totalSale1);
                    txtopbal.Text = opbal.ToString("N2");
                    DC = "D";
                }
                else
                {
                    opbal = Convert.ToDouble(openingstockfromitem) - Convert.ToDouble(totalSale1) + Convert.ToDouble(totalPurchase1);
                    txtopbal.Text = opbal.ToString("N2");
                    DC = "C";
                }

                //for (int i = 0; i < OPdt.Rows.Count; i++)
                //{
                //    Double opbal = 0;
                //    if (OPdt.Rows[i]["DC"].ToString() == "D")
                //    {

                //    }
                //    else if (OPdt.Rows[i]["TranType"].ToString() == "Rect")
                //    {
                //        ListViewItem li;
                //        li = LVledger.Items.Add(Convert.ToDateTime(OPdt.Rows[i]["Date1"].ToString()).ToString("dd-MMM-yyyy"));
                //        li.SubItems.Add("Rect");
                //        li.SubItems.Add(OPdt.Rows[i]["OT1"].ToString());
                //        li.SubItems.Add("By Rcpt. No.: " + OPdt.Rows[i]["VoucherID"].ToString() + "; " + OPdt.Rows[i]["ShortNarration"].ToString());
                //        li.SubItems.Add("");
                //        li.SubItems.Add(Math.Round(Convert.ToDouble(OPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                //        li.SubItems.Add("Rect");
                //    }
                //}
                #endregion

                mouseclickid.Rows.Clear();
                LVledger.Items.Clear();

                #region

                progressBar1.Increment(1);
                DataTable pos = conn.getdataset("select 'POS' as Billtype,b.BillDate as Bill_Run_Date,b.billno,b.totalnet as Rate,bp.qty as pqty,b.BillId as Bill_No,bp.Batchno as Batch  from BillPOSMaster b inner join BillPOSProductMaster bp on b.billid=bp.billid where b.isactive=1 and bp.isactive=1 and b.BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and b.BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and bp.itemname='" + txtitem.Text + "' order by BillDate asc");
                pos.Columns.Add("ClientID", typeof(String));
                //DataTable pos = conn.getdataset("select 'POS' as Billtype,BillDate as Bill_Run_Date,billno,totalnet as Rate,totalqty as pqty,BillId as Bill_No  from BillPOSMaster where isactive=1 and BillDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' order by BillDate asc");
                pos = changedtclone(pos);

                //DataTable SPdt1 = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No from SaleOrderProductMaster where isactive=1 and OrderStatus='Pending' and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' order by bill_Run_Date");
          //      DataTable SPdtSTI = conn.getdataset("select so.Billtype,so.Bill_Run_Date,so.billno,so.Rate,so.Pqty,so.Bill_No,so.ClientID from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and s.Billtype='STI' and so.bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and so.productid='" + proid + "' and Batch='" + cmbbatch.Text + "' order by so.bill_Run_Date");
          //      SPdtSTI = changedtclone(SPdtSTI);
          //      DataTable SPdtSTO = conn.getdataset("select so.Billtype,so.Bill_Run_Date,so.billno,so.Rate,so.Pqty,so.Bill_No,so.ClientID from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and s.Billtype='STO' and so.bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and so.productid='" + proid + "' and Batch='" + cmbbatch.Text + "' order by so.bill_Run_Date");
         //       SPdtSTO = changedtclone(SPdtSTO);
                DataTable SPdt1 = conn.getdataset("select so.Billtype,so.Bill_Run_Date,so.billno,so.Rate,so.Pqty,so.Bill_No,so.ClientID,so.Batch as Batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and s.Billtype='PC' and so.bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and so.productid='" + proid + "' and Batchid='" + cmbbatch.SelectedValue.ToString() + "' order by so.bill_Run_Date");
                SPdt1 = changedtclone(SPdt1);
                DataTable SPdt2 = conn.getdataset("select so.Billtype,so.Bill_Run_Date,so.billno,so.Rate,so.Pqty,so.Bill_No,so.ClientID,so.Batch as Batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.isactive=1 and s.isactive=1 and s.OrderStatus='Pending' and s.Billtype='SC' and so.bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and so.productid='" + proid + "' and Batchid='" + cmbbatch.SelectedValue.ToString() + "' order by so.bill_Run_Date");
                SPdt2 = changedtclone(SPdt2);
                DataTable SPdtsr = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No,ClientID,Batch as Batch from billproductmaster where isactive=1 and billtype='SR' and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' and Batchid='" + cmbbatch.SelectedValue.ToString() + "' order by bill_Run_Date");
                SPdtsr = changedtclone(SPdtsr);
                DataTable SPdtpr = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No,ClientID,Batch as Batch from billproductmaster where isactive=1 and billtype='PR' and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' and Batchid='" + cmbbatch.SelectedValue.ToString() + "' order by bill_Run_Date");
                SPdtpr = changedtclone(SPdtpr);
                DataTable SPdtp = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No,ClientID,Batch as Batch from billproductmaster where isactive=1 and billtype='P' and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' and Batchid='" + cmbbatch.SelectedValue.ToString() + "' order by bill_Run_Date");
                SPdtp = changedtclone(SPdtp);
             //   DataTable SPdtGIN = conn.getdataset("select Billtype,Date,billno,0 as Rate,qty as Pqty,0 as Bill_No,ClientID from tblgoodissuereturnnoteitemmaster where isactive=1 and billtype='GIN' and Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' and Batch='" + cmbbatch.Text + "' order by Date");
             //   SPdtGIN = changedtclone(SPdtGIN);
            //    DataTable SPdtGRN = conn.getdataset("select Billtype,Date,billno,0 as Rate,qty as Pqty,0 as Bill_No,ClientID from tblgoodissuereturnnoteitemmaster where isactive=1 and billtype='GRN' and Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' and Batch='" + cmbbatch.Text + "' order by Date");
            //    SPdtGRN = changedtclone(SPdtGRN);
                DataTable SPdt = conn.getdataset("select Billtype,Bill_Run_Date,billno,Rate,Pqty,Bill_No,ClientID,Batch as Batch from billproductmaster where isactive=1 and billtype='S' and bill_Run_Date between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' and productid='" + proid + "' and Batchid='" + cmbbatch.SelectedValue.ToString() + "' order by bill_Run_Date");

                Double a = Convert.ToDouble(balance) + opbal;
                balance = Convert.ToString(a);

                SPdt = changedtclone(SPdt);
                SPdt.Merge(pos);
                SPdt.Merge(SPdt1);
                SPdt.Merge(SPdt2);
                SPdt.Merge(SPdtsr);
                SPdt.Merge(SPdtpr);
                SPdt.Merge(SPdtp);
           //     SPdt.Merge(SPdtSTI);
           //     SPdt.Merge(SPdtSTO);
              //  SPdt.Merge(SPdtGIN);
              //  SPdt.Merge(SPdtGRN);
                SPdt.AcceptChanges();



                DataTable dtCloned = SPdt.Clone();
                dtCloned.Columns[1].DataType = typeof(DateTime);
                foreach (DataRow row in SPdt.Rows)
                {
                    dtCloned.ImportRow(row);
                }
                SPdt = null;
                SPdt = dtCloned;
                SPdt.DefaultView.Sort = "[bill_Run_Date] asc";
                SPdt = SPdt.DefaultView.ToTable();
                debit = 0;
                credit = 0;
                debit1 = 0;
                credit1 = 0;
                for (int i = 0; i < SPdt.Rows.Count; i++)
                {
                    if (SPdt.Rows[i]["Billtype"].ToString() == "GIN")
                    {
                      /*  ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GIN");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select employeetype from tbluser_employeetype where isactive=1 and id=(select deptid from tblgoodissuereturnnotemaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(0), 2).ToString("N2"));

                        li.SubItems.Add("");
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["pqty"].ToString()), 2)).ToString());
                        debit += Convert.ToDouble(SPdt.Rows[i]["pqty"].ToString());

                        mouseclickid.Rows.Add("GIN", SPdt.Rows[i]["billno"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["pqty"].ToString()), DC, "D", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());   */
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "GRN")
                    {
                      /*   ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("GRN");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select employeetype from tbluser_employeetype where isactive=1 and id=(select deptid from tblgoodissuereturnnotemaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(0), 2).ToString("N2"));
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        li.SubItems.Add("");
                        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                        mouseclickid.Rows.Add("GRN", SPdt.Rows[i]["Pqty"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());    */
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "S")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Sale");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));

                        li.SubItems.Add("");
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());

                        mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["Bill_no"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Batch"].ToString());
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "SC")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Sale Challan");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from SaleOrderMaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));

                        li.SubItems.Add("");
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());

                        mouseclickid.Rows.Add("Sale Challan", SPdt.Rows[i]["Bill_no"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Batch"].ToString());
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "STO")
                    {
                     /*   ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Stock Out");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from SaleOrderMaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));

                        li.SubItems.Add("");
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());

                        mouseclickid.Rows.Add("Stock Out", SPdt.Rows[i]["Bill_no"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());   */
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "POS")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("POS Sale");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select customername from BillPOSMaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "'");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));

                        li.SubItems.Add("");
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());

                        mouseclickid.Rows.Add("Sale", SPdt.Rows[i]["Bill_no"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Batch"].ToString());
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "SR")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Sale Return");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        li.SubItems.Add("");
                        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                        mouseclickid.Rows.Add("Sale Return", SPdt.Rows[i]["Pqty"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Batch"].ToString());
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "P")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Purchase");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        li.SubItems.Add("");
                        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                        mouseclickid.Rows.Add("Purchase", SPdt.Rows[i]["Pqty"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Batch"].ToString());
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "PC")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Purchase Challan");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from SaleOrderMaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        li.SubItems.Add("");
                        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                        mouseclickid.Rows.Add("Purchase Challan", SPdt.Rows[i]["Pqty"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Batch"].ToString());
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "STI")
                    {
                      /*  ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Stock In");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from SaleOrderMaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        li.SubItems.Add("");
                        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        credit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                        mouseclickid.Rows.Add("Stock In", SPdt.Rows[i]["Pqty"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "C", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString()); */
                    }
                    else if (SPdt.Rows[i]["Billtype"].ToString() == "PR")
                    {
                        ListViewItem li;
                        li = LVledger.Items.Add(Convert.ToDateTime(SPdt.Rows[i]["bill_Run_Date"].ToString()).ToString(Master.dateformate));
                        li.SubItems.Add("Purchase Return");
                        li.SubItems.Add("Bill No. " + SPdt.Rows[i]["Billno"].ToString());
                        string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid=(select clientid from billmaster where isactive=1 and billno='" + SPdt.Rows[i]["Billno"].ToString() + "')");
                        li.SubItems.Add(clientname);
                        li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Rate"].ToString()), 2).ToString("N2"));
                        li.SubItems.Add("");
                        li.SubItems.Add((Math.Round(Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), 2)).ToString());
                        //li.SubItems.Add(Math.Round(Convert.ToDouble(SPdt.Rows[i]["Amount"].ToString()), 2).ToString("N2"));
                        debit += Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString());
                        mouseclickid.Rows.Add("Purchase Return", SPdt.Rows[i]["Pqty"].ToString());
                        if (i != 0)
                        {
                            string[] str = balance.Split(' ');
                            char temp = str[0][0];
                            DC = temp.ToString();
                            opbal = Convert.ToDouble(str[0]);
                        }
                        balance = getbalance(opbal, Convert.ToDouble(SPdt.Rows[i]["Pqty"].ToString()), DC, "D", i);
                        li.SubItems.Add(balance);
                        li.SubItems.Add(SPdt.Rows[i]["ClientID"].ToString());
                        li.SubItems.Add(SPdt.Rows[i]["Batch"].ToString());
                    }

                }
                //Production
            /*    DataTable production = conn.getdataset("select pm.*,p.date from tblproductionrawmaterialmaster pm inner join tblproductionmaster p on p.id=pm.productionid where p.isactive=1 and pm.isactive=1 and pm.productid='" + proid + "' and pm.batchno='" + cmbbatch.Text + "'");
                for (int i = 0; i < production.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = LVledger.Items.Add(Convert.ToDateTime(production.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                    li.SubItems.Add("Production");
                    li.SubItems.Add("");
                    li.SubItems.Add("");
                    li.SubItems.Add(Math.Round(Convert.ToDouble("0"), 2).ToString("N2"));
                    //  if (Convert.ToDouble(production.Rows[i]["rawqty"].ToString()) > 0)
                    // {
                    li.SubItems.Add("");
                    li.SubItems.Add((Math.Round(Convert.ToDouble(production.Rows[i]["rawqty"].ToString()), 2)).ToString());
                    debit += Convert.ToDouble(production.Rows[i]["rawqty"].ToString());
                    //}
                    //else
                    //{
                    //    li.SubItems.Add("");
                    //    li.SubItems.Add((Math.Round(Convert.ToDouble(production.Rows[i]["rawqty"].ToString()), 2)).ToString());
                    //    Double d = Convert.ToDouble(production.Rows[i]["rawqty"].ToString());
                    //    Double a1 = d * -1;
                    //    debit += Convert.ToDouble(a1);
                    //}
                    Double bal = Convert.ToDouble(balance);
                    Double astock = Convert.ToDouble(production.Rows[i]["rawqty"].ToString());
                    Double fbalance = bal - astock;
                    balance = Convert.ToString(fbalance);
                    li.SubItems.Add(balance);
                    li.SubItems.Add("1");
                }
                //fineshqty
                DataTable fineshqty = conn.getdataset("select * from tblfinishedgoodsqty where isactive=1 and productid='" + proid + "' and batchno='" + cmbbatch.Text + "'");
                for (int i = 0; i < fineshqty.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = LVledger.Items.Add(Convert.ToDateTime(fineshqty.Rows[i]["date"].ToString()).ToString(Master.dateformate));
                    li.SubItems.Add("Finished Qty");
                    li.SubItems.Add("");
                    li.SubItems.Add("");
                    li.SubItems.Add(Math.Round(Convert.ToDouble("0"), 2).ToString("N2"));
                    //if (Convert.ToDouble(fineshqty.Rows[i]["fqty"].ToString()) > 0)
                    //{
                    li.SubItems.Add((Math.Round(Convert.ToDouble(fineshqty.Rows[i]["fqty"].ToString()), 2)).ToString());
                    li.SubItems.Add("");
                    credit += Convert.ToDouble(fineshqty.Rows[i]["fqty"].ToString());
                    //}
                    //else
                    //{
                    //    li.SubItems.Add("");
                    //    li.SubItems.Add((Math.Round(Convert.ToDouble(fineshqty.Rows[i]["fqty"].ToString()), 2)).ToString());
                    //    Double d = Convert.ToDouble(fineshqty.Rows[i]["fqty"].ToString());
                    //    Double a1 = d * -1;
                    //    debit += Convert.ToDouble(a1);
                    //}
                    Double bal = Convert.ToDouble(balance);
                    Double astock = Convert.ToDouble(fineshqty.Rows[i]["fqty"].ToString());
                    Double fbalance = bal + astock;
                    balance = Convert.ToString(fbalance);
                    li.SubItems.Add(balance);
                    li.SubItems.Add("1");
                } */
                progressBar1.Increment(1);
                DataTable stockadjustment = conn.getdataset("select sm.*,sim.* from stockadujestmentmaster sm inner join stockadujestmentitemmaster sim on sm.id=sim.stockid where sm.isactive=1 and sim.isactive=1 and sim.itemid='" + proid + "' and sim.batch='" + cmbbatch.Text + "' and sm.stockdate between '" + DTPFrom.Text + "' and '" + DTPTo.Text + "' order by sm.stockdate");
                for (int i = 0; i < stockadjustment.Rows.Count; i++)
                {
                    ListViewItem li;
                    li = LVledger.Items.Add(Convert.ToDateTime(stockadjustment.Rows[i]["stockdate"].ToString()).ToString("dd-MMM-yyyy"));
                    li.SubItems.Add("Stock Adjustment");
                    li.SubItems.Add("");
                    li.SubItems.Add(stockadjustment.Rows[i]["mainremark"].ToString());
                    li.SubItems.Add(Math.Round(Convert.ToDouble("0"), 2).ToString("N2"));
                    if (Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString()) > 0)
                    {
                        li.SubItems.Add((Math.Round(Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString()), 2)).ToString());
                        li.SubItems.Add("");
                        credit += Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                    }
                    else
                    {
                        li.SubItems.Add("");
                        li.SubItems.Add((Math.Round(Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString()), 2)).ToString());
                        Double d = Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                        Double a1 = d * -1;
                        debit += Convert.ToDouble(a1);
                    }
                    Double bal = Convert.ToDouble(balance);
                    Double astock = Convert.ToDouble(stockadjustment.Rows[i]["adjuststock"].ToString());
                    Double fbalance = bal + astock;
                    balance = Convert.ToString(fbalance);
                    li.SubItems.Add(balance);
                    li.SubItems.Add("1");
                    li.SubItems.Add(stockadjustment.Rows[i]["Batch"].ToString());
                }
                #endregion
            }

            //for create ledger


            txttotdebit.Text = debit.ToString("N2");
            txttotcredit.Text = credit.ToString("N2");
            txtbalance.Text = balance;
            progressBar1.Increment(1);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (i == 0)
            {
                _BindItemWiseStockList();
                if (flag == false)
                {
                    if (progressBar1.Value == filelength)
                    {
                        timer1.Enabled = false;
                        timer1.Stop();
                        i = 1;
                    }
                }
            }
        }
        DataTable dt8 = new DataTable();
        public void bindbatch()
        {
            try
            {
                cmbbatch.Enabled = true;
                dt8 = new DataTable();
                string productid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and Product_Name='" + txtitem.Text + "'");
                SqlCommand cmd1 = new SqlCommand("select Productid,Batchno,ProPriceID from ProductPriceMaster where Productid='" + productid + "' and isactive='1'", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                sda1.Fill(dt8);
                cmbbatch.ValueMember = "ProPriceID";
                cmbbatch.DisplayMember = "Batchno";
                cmbbatch.DataSource = dt8;
                cmbbatch.Focus();
            }
            catch
            {
            }
        }
        private void cmbbatch_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbbatch.SelectedIndex = 0;
                cmbbatch.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbbatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
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
                DTPFrom.Focus();
            }
        }

        private void cmbbatch_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbbatch.Text = s;
            }
            catch
            {
            }
        }

        private void cmbbatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
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
            }
            catch (Exception excp)
            {
            }
        }

    }
}
