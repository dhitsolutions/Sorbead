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
    public partial class Print : Form
    {
        Printing prnt = new Printing();
          Connection conn = new Connection();
          public static string flagforprint;
        private string p;
        public Print()
        {
            InitializeComponent();
        }

        public Print(string p)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.p = p;
            binddrop();
            
        }
        private void Print_Load(object sender, EventArgs e)
        {
           this.ActiveControl=btnpreview;
           //set the interval  and start the timer
         //  timer1.Interval = 1000;
         //  timer1.Start();
        }

        private void binddrop()
        {
            try
            {

                DataSet ds = prnt.getdata("select * from design where TransactionType='" + p + "'");
                DataTable dt = ds.Tables[0];
                cmbformat.ValueMember = "DesignID";
                cmbformat.DisplayMember = "DesignName";
                cmbformat.DataSource = dt;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["SetDefault"].ToString() == "Y")
                    {
                        cmbformat.SelectedIndex = i;
                    }
                }
            }
            catch
            {
            }
        }
        public void btnpreview_Click(object sender, EventArgs e)
        {
            flagforprint = "0";
            //SalePurchaseDetailsReport
            if (p == "SalePurchaseDetailsReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SalePurchaseDetailsReport");

                sql.Show();

                this.Close();

            }
            if (p == "SaleOrderBillsList")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SaleOrderBillsList");

                sql.Show();

                this.Close();

            }
            if (p == "GSTR-3B")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "GSTR-3B");

                sql.Show();

                this.Close();

            }
            if (p == "CustomerSupplier")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "CustomerSupplier");

                sql.Show();

                this.Close();

            }
            if (p == "QuickReceiptRegister")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "QuickReceiptRegister");

                sql.Show();

                this.Close();

            }
            if (p == "QuickPaymentRegister")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "QuickPaymentRegister");

                sql.Show();

                this.Close();

            }
            if (p == "SaleRegister")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SaleRegister");

                sql.Show();

                this.Close();

            }
            if (p == "PurchaseRegister")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "PurchaseRegister");

                sql.Show();

                this.Close();

            }
            if (p == "PurchaseReturn")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "PurchaseReturn");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                if (DateWiseReport.autopdf == "false")
                {
                    sql.Show();
                }
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "DebitCreditNote")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "DebitCreditNote");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "ItemWiseSale")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ItemWiseSale");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "Sale")
            {
                string str = Application.StartupPath + "\\"+lblrptname.Text;
                SQLReport sql = new SQLReport(str,"Sale");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                if (DateWiseReport.autopdf == "false")
                {
                    sql.Show();
                }
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "Purchase")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Purchase");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                if (DateWiseReport.autopdf == "false")
                {
                    sql.Show();
                }
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "Pos")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str,"Pos");
                 //DataTable bill = conn.getdataset("select defaultbill from Options");
                 //if (bill.Rows.Count > 0)
                 //{
                 //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                 //    {
                        sql.Show();
                 //    }
                 //    else
                 //    {
                 //        //SaleReport sale = new SaleReport(str);
                 //        sql.Show();
                 //        sql.Hide();
                 //    }
                 //}
                 this.Close();
            }

            if (p == "Ledger")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str,"Ledger");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "QuickPayment")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "QuickPayment");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //QuickReceipt
            if (p == "QuickReceipt")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "QuickReceipt");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }

            if (p == "ItemWiseStock")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ItemWiseStock");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "CashBook")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "CashBook");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //SaleBillsList
            if (p == "SaleBillsList")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SaleBillsList");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //OutstandingAnalysis
            if (p == "Outstanding")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Outstanding");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                       sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //Print Barcode 1
            if (p == "Barcode1")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Barcode1");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //Print Barcode 2
            if (p == "Barcode2")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Barcode2");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //Print Barcode 3
            if (p == "Barcode3")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Barcode3");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Trialbalance")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Trialbalance");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "SaleReturn")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SaleReturn");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                if (DateWiseReport.autopdf == "false")
                {
                    sql.Show();
                }
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                //        sql.Show();
                //        sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Sale Order")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Sale Order");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                if (SaleOrderList.autopdf == "false")
                {
                    sql.Show();
                }
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
               // sql.Show();
             //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Sale Challan")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Sale Challan");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                if (SaleOrderList.autopdf == "false")
                {
                    sql.Show();
                }
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Pos List")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Pos List");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
           // 
            if (p == "Pos Item List")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "PosItemList");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "BankVoucher")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "BankVoucher");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "BankEnteryReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "BankEnteryReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "DebitReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "DebitReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "CreditReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "CreditReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "StockEvaluation")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "StockEvaluation");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "ItemReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ItemReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "AccountLabel")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "AccountLabel");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "AccountReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "AccountReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "AgentReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "AgentReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                // sql.Show();
                //   sql.Hide();
                //    }
                //}
                this.Close();

            }
            if (p == "Purchase Order")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Purchase Order");
                if (SaleOrderList.autopdf == "false")
                {
                    sql.Show();
                }
                
                this.Close();
            }
            //ComplainReceive
            if (p == "ComplainReceive")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ComplainReceive");

                sql.Show();

                this.Close();
            }
            //
            if (p == "SendToCompany")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SendToCompany");

                sql.Show();

                this.Close();
            }
            //SendToCustomer
            if (p == "SendToCustomer")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SendToCustomer");

                sql.Show();

                this.Close();
            }
            //ProductionProcessRegister
            if (p == "ProductionProcessRegister")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ProductionProcessRegister");

                sql.Show();

                this.Close();
            }
            //ProductionRegister
            if (p == "ProductionRegister")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ProductionRegister");

                sql.Show();

                this.Close();
            }
            //ProductionUtilization
            if (p == "ProductionUtilization")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ProductionUtilization");

                sql.Show();

                this.Close();
            }
            //FinishedGoodsList
            if (p == "FinishedGoodsList")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "FinishedGoodsList");

                sql.Show();

                this.Close();
            }
            //ProductionPlanning
            if (p == "ProductionPlanning")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ProductionPlanning");

                sql.Show();

                this.Close();
            }
            //Production
            if (p == "Production")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Production");

                sql.Show();

                this.Close();
            }
            //ProductionProcess
            if (p == "ProductionProcess")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ProductionProcess");

                sql.Show();

                this.Close();
            }
            //PosBillListThermalPrint
            if (p == "PosBillListThermalPrint")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "PosBillListThermalPrint");

                sql.Show();

                this.Close();
            }
            //ItemWiseSale
            if (p == "ItemWiseSale")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ItemWiseSale");

                sql.Show();

                this.Close();
            }

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    this.Close();
                }
                return true;
            }



            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void cmbformat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = prnt.getdata("select * from design where DesignName='" + cmbformat.Text + "'");
                DataTable dt = ds.Tables[0];
                lblrptname.Text = dt.Rows[0]["Path"].ToString();
                bool inList = false;
                for (int i = 0; i < cmbformat.Items.Count; i++)
                {
                    s = cmbformat.GetItemText(cmbformat.Items[i]);
                    if (s == cmbformat.Text)
                    {
                        inList = true;
                        cmbformat.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbformat.Text = "";
                }
            }
            catch
            {
            }
        }

        private void cmbformat_Enter(object sender, EventArgs e)
        {
            
        }

        private void cmbformat_Leave(object sender, EventArgs e)
        {
            cmbformat.Text = s;
        }
        string searchstr;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
          //  searchstr = "";
        }

        private void cmbformat_KeyUp(object sender, KeyEventArgs e)
        {
            if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            {
                searchstr = searchstr + Convert.ToChar(e.KeyCode);
                // If the Search string is greater than 1 then use custom logic
                if (searchstr.Length > 1)
                {
                    int index;
                    // Search the Item that matches the string typed
                    index = cmbformat.FindString(searchstr);
                    // Select the Item in the Combo
                    if (index > 0)
                    {
                        cmbformat.SelectedIndex = index;
                    }
                }


            }
        }

        public void btnprint_Click(object sender, EventArgs e)
        {
            flagforprint = "1";
            if (p == "SalePurchaseDetailsReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SalePurchaseDetailsReport");

                sql.Show();
                sql.Hide();

                this.Close();

            }
            if (p == "SaleOrderBillsList")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SaleOrderBillsList");
                
                sql.Show();
                sql.Hide();
                
                this.Close();
                
            }
            if (p == "GSTR-3B")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "GSTR-3B");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "CustomerSupplier")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "CustomerSupplier");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "QuickReceiptRegister")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "QuickReceiptRegister");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "QuickPaymentRegister")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "QuickPaymentRegister");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "SaleRegister")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SaleRegister");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "PurchaseRegister")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "PurchaseRegister");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "PurchaseReturn")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "PurchaseReturn");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "DebitCreditNote")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "DebitCreditNote");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            if (p == "ItemWiseSale")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ItemWiseSale");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            else if (p == "Sale")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Sale");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            else if (p == "Purchase")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Purchase");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();
                //SaleReport sale = new SaleReport(str);
                //sale.Show();
            }
            else if (p == "Pos")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Pos");
                DataTable bill = conn.getdataset("select defaultbill from Options");
                if (bill.Rows.Count > 0)
                {
                    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                    {
                        sql.Show();
                    }
                    else
                    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                    }
                }
                this.Close();
            }

            else if (p == "Ledger")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Ledger");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                       sql.Show();
                       sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "QuickPayment")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "QuickPayment");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                       sql.Show();
                       sql.Hide();
                //    }
                //}
                this.Close();

            }
            //QuickReceipt
            else if (p == "QuickReceipt")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "QuickReceipt");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }

            else if (p == "ItemWiseStock")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ItemWiseStock");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "CashBook")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "CashBook");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //SaleBillsList
            else if (p == "SaleBillsList")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SaleBillsList");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //OutstandingAnalysis
            else if (p == "Outstanding")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Outstanding");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //Print Barcode 1
            else if (p == "Barcode1")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Barcode1");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //Print Barcode 2
            else if (p == "Barcode2")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Barcode2");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            //Print Barcode 3
            else if (p == "Barcode3")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Barcode3");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                       sql.Show();
                       sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "Trialbalance")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Trialbalance");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                //        sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "SaleReturn")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SaleReturn");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
               // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                        sql.Show();
                        sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "Sale Order")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Sale Order");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "Sale Challan")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Sale Challan");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
               // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                 sql.Show();
                   sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "Pos List")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Pos List");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "Pos Item List")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "PosItemList");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "BankVoucher")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "BankVoucher");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "BankEnterReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "BankEnterReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "CreditReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "CreditReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "DebitReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "DebitReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "StockEvaluation")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "StockEvaluation");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "ItemReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ItemReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "AccountLabel")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "AccountLabel");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "AccountReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "AccountReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "AgentReport")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "AgentReport");
                //DataTable bill = conn.getdataset("select defaultbill from Options");
                //if (bill.Rows.Count > 0)
                //{
                //    if (bill.Rows[0]["defaultbill"].ToString() == "Preview")
                //    {
                // sql.Show();
                //    }
                //    else
                //    {
                //        //SaleReport sale = new SaleReport(str);
                sql.Show();
                sql.Hide();
                //    }
                //}
                this.Close();

            }
            else if (p == "Purchase Order")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Purchase Order");
                sql.Show();
                sql.Hide();
                this.Close();
            }
            else if (p == "ComplainReceive")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ComplainReceive");
                sql.Show();
                sql.Hide();
                this.Close();
            }
            //SendToCompany
            else if (p == "SendToCompany")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SendToCompany");
                sql.Show();
                sql.Hide();
                this.Close();
            }
            //SendToCustomer
            else if (p == "SendToCustomer")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "SendToCustomer");
                sql.Show();
                sql.Hide();
                this.Close();
            }
            //ProductionProcessRegister
            else if (p == "ProductionProcessRegister")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ProductionProcessRegister");

                sql.Show();

                this.Close();
            }
            //ProductionRegister
            else if (p == "ProductionRegister")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ProductionRegister");

                sql.Show();

                this.Close();
            }
            //ProductionUtilization
            else if (p == "ProductionUtilization")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ProductionUtilization");

                sql.Show();

                this.Close();
            }
            //FinishedGoodsList
            else if (p == "FinishedGoodsList")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "FinishedGoodsList");

                sql.Show();

                this.Close();
            }
            //ProductionPlanning
            else if (p == "ProductionPlanning")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ProductionPlanning");

                sql.Show();

                this.Close();
            }
            //Production
            else if (p == "Production")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "Production");

                sql.Show();

                this.Close();
            }
            //ProductionProcess
            else if (p == "ProductionProcess")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ProductionProcess");

                sql.Show();

                this.Close();
            }
            else if (p == "PosBillListThermalPrint")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "PosBillListThermalPrint");

                sql.Show();

                this.Close();
            }
//            ItemWiseSale
            else if (p == "ItemWiseSale")
            {
                string str = Application.StartupPath + "\\" + lblrptname.Text;
                SQLReport sql = new SQLReport(str, "ItemWiseSale");

                sql.Show();

                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Enter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_Leave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
        }
        public static string s;
        private void cmbformat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbformat.Items.Count; i++)
                {
                    s = cmbformat.GetItemText(cmbformat.Items[i]);
                    if (s == cmbformat.Text)
                    {
                        inList = true;
                        cmbformat.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbformat.Text = "";
                }

            }
        }

        private void btnpreview_Enter(object sender, EventArgs e)
        {
            btnpreview.UseVisualStyleBackColor = false;
            btnpreview.BackColor = Color.FromArgb(20, 209, 82);
            btnpreview.ForeColor = Color.White;
        }

        private void btnpreview_Leave(object sender, EventArgs e)
        {
            btnpreview.UseVisualStyleBackColor = false;
            btnpreview.BackColor = Color.FromArgb(51, 153, 255);
            btnpreview.ForeColor = Color.White;
        }

        private void btnpreview_MouseEnter(object sender, EventArgs e)
        {
            btnpreview.UseVisualStyleBackColor = false;
            btnpreview.BackColor = Color.FromArgb(20, 209, 82);
            btnpreview.ForeColor = Color.White;
        }

        private void btnpreview_MouseLeave(object sender, EventArgs e)
        {
            btnpreview.UseVisualStyleBackColor = false;
            btnpreview.BackColor = Color.FromArgb(51, 153, 255);
            btnpreview.ForeColor = Color.White;
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

    }
}
