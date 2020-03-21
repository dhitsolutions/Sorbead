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
using ClosedXML.Excel;
using System.IO;
using RamdevSales.CommonClass;

namespace RamdevSales
{
    public partial class ItemWiseStockSummary : Form
    {
        //  Connection con = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection cs = new Connection();
        CommonMethods methods = new CommonMethods();
        public double optotal, d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14, d15;
        public double d16, d17, d18, d19, d20, d21, d22, d23, d24;
        public static string iid = "";
        DataTable dt = new DataTable();
        DataTable totalamt = new DataTable();
        Printing prn = new Printing();
        Double total = 0;
        private Master master;

        private TabControl tabControl;
        DataTable product = new DataTable();
        DataTable opstock = new DataTable();
        DataTable salestock = new DataTable();
        DataTable salechallnastock = new DataTable();
        DataTable stockout = new DataTable();
        DataTable purchasestock = new DataTable();
        DataTable GRNstock = new DataTable();
        DataTable GINstock = new DataTable();
        DataTable purchasechallnastock = new DataTable();
        DataTable salereturnstock = new DataTable();
        DataTable purchasereturnstock = new DataTable();
        DataTable stockin = new DataTable();
        DataTable POSSale = new DataTable();
        DataTable production = new DataTable();
        DataTable finishedqty = new DataTable();
        DataTable reuseqty = new DataTable();
        DataTable adjuststock = new DataTable();
        DataTable finishedrejectqty = new DataTable();
        public ItemWiseStockSummary()
        {
            InitializeComponent();
            //        grdstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            chkavailable.Checked = true;
            chkzero.Checked = true;
            chknegative.Checked = true;
        }

        public ItemWiseStockSummary(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            //       grdstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.master = master;
            this.tabControl = tabControl;
            isMaster = true;
            btnsubmit.Text = "Print";
            btndelete.Visible = false;
            //chkavailable.Checked = true;
            //chkzero.Checked = true;
            //chknegative.Checked = true;
        }

        public ItemWiseStockSummary(string p)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            //DTPfrom.Enabled = false;
            //DTPTo.Enabled = false;
            //          grdstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.p = p;
        }


        DataTable userrights = new DataTable();
        static bool flag = false;
        int filelength = 1;
        public void ok()
        {
            try
            {
                progressBar1.Maximum = 4;
                filelength = 4;
                if (DTPTo.Text == "")
                {
                    DTPTo.Value = DateTime.Now;
                }
                if (DTPfrom.Text == "")
                {
                    DTPfrom.Value = DateTime.Now;
                }
                DTPTo.CustomFormat = Master.dateformate;
                DTPfrom.CustomFormat = Master.dateformate;
                //  MessageBox.Show("start: " + DateTime.Now.ToString("HH:mm:ss"));
                progressBar1.Increment(1);
                createtable();
                //                MessageBox.Show("after create table: "+ DateTime.Now.ToString("HH:mm:ss"));
                progressBar1.Increment(1);

                getdatatable();

                //              MessageBox.Show("after get table: " + DateTime.Now.ToString("HH:mm:ss"));
                //get productmaster
                progressBar1.Increment(1);

                setdatatable();

                //            MessageBox.Show("after set table: " + DateTime.Now.ToString("HH:mm:ss"));
                setgrid();
                progressBar1.Increment(1);
                //          MessageBox.Show("after set grid: " + DateTime.Now.ToString("HH:mm:ss"));
            }
            catch
            {
            }
        }
        string[] closingval = new String[2];

        public string[] okfortrading()
        {
            try
            {
                progressBar1.Maximum = 4;
                filelength = 4;
                if (DTPTo.Text == "")
                {
                    DTPTo.Value = DateTime.Now;
                }
                DTPTo.CustomFormat = Master.dateformate;
                //  MessageBox.Show("start: " + DateTime.Now.ToString("HH:mm:ss"));
                progressBar1.Increment(1);
                createtable();
                //                MessageBox.Show("after create table: "+ DateTime.Now.ToString("HH:mm:ss"));
                progressBar1.Increment(1);

                //for trading account
                getdatatable4trading();

                //              MessageBox.Show("after get table: " + DateTime.Now.ToString("HH:mm:ss"));
                //get productmaster
                progressBar1.Increment(1);

                setdatatable4trading();

                //            MessageBox.Show("after set table: " + DateTime.Now.ToString("HH:mm:ss"));
                setgrid();
                progressBar1.Increment(1);

                return closingval;
                //          MessageBox.Show("after set grid: " + DateTime.Now.ToString("HH:mm:ss"));
            }
            catch
            {
                return closingval;
            }
        }
        private void setdatatable4trading()
        {

            {
                #region
                try
                {
                    // progressBar1.Maximum = product.Rows.Count;
                    // filelength = product.Rows.Count;
                    for (int i = 0; i < product.Rows.Count; i++)
                    {
                        DataTable isbatch = cs.getdataset("select * from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                        if (isbatch.Rows.Count > 0)
                        {

                            for (int k = 0; k < isbatch.Rows.Count; k++)
                            {
                                string opening = "0", purchase = "0", purchasec = "0", sale = "0", salec = "0", salereturn = "0", purchasereturn = "0", possale = "0", finish = "0", reuse = "0", pro = "0", ajuststock = "0", stockinstr = "0", stockoutstr = "0", GRN = "0", GIN = "0";
                                DataRow dr = dt.NewRow();
                                dr["Item Code"] = product.Rows[i]["ProductID"].ToString();
                                dr["Name of Item"] = product.Rows[i]["Product_Name"].ToString();
                                dr["Company"] = product.Rows[i]["companyname"].ToString();
                                dr["batch"] = isbatch.Rows[k]["batchno"].ToString();

                                //opening stock
                                dr["Op. Stock"] = "0";
                                //DataRow[] drop = opstock.Select("productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                //if (drop.Length > 0)
                                //{
                                //    DataRow[] drop1 = opstock.Select("batchno='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                //    if (drop1.Length > 0)
                                //    {
                                //        dr["Op. Stock"] = drop1[0]["OpStock"].ToString();
                                //        opening = drop1[0]["OpStock"].ToString();
                                //    }
                                //}
                                #region Opstock
                                for (int j = 0; j < opstock.Rows.Count; j++)
                                {
                                    if (opstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (opstock.Rows[j]["batchno"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Op. Stock"] = opstock.Rows[j]["OpStock"].ToString();
                                            opening = opstock.Rows[j]["OpStock"].ToString();
                                            break;
                                        }
                                    }

                                }
                                #endregion Opstock

                                //purchase stock
                                dr["Purchase"] = "0";
                                //DataRow[] DrPurchase = purchasestock.Select("productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                //if (DrPurchase.Length > 0)
                                //{
                                //    DataRow[] drpurstock1 = purchasestock.Select("batch='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                //    if (drpurstock1.Length > 0)
                                //    {
                                //        dr["Purchase"] = drpurstock1[0]["Purchase"].ToString();
                                //        purchase = drpurstock1[0]["Purchase"].ToString();
                                //    }
                                //}
                                #region Pstock
                                for (int j = 0; j < purchasestock.Rows.Count; j++)
                                {
                                    if (purchasestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (purchasestock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Purchase"] = purchasestock.Rows[j]["Purchase"].ToString();
                                            purchase = purchasestock.Rows[j]["Purchase"].ToString();
                                            break;
                                        }
                                    }
                                }
                                //GRN stock
                                #endregion Pstock
                                dr["GRN"] = "0";
                                //DataRow[] DrGRN = GRNstock.Select("productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                //if (DrGRN.Length > 0)
                                //{
                                //    DataRow[] DrGRN1 = GRNstock.Select("batch='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                //    {
                                //        if (DrGRN1.Length > 0)
                                //        {
                                //            dr["GRN"] = DrGRN1[0]["GRN"].ToString();
                                //            GRN = DrGRN1[0]["GRN"].ToString();
                                //        }
                                //    }
                                //}
                                #region GRN
                                for (int j = 0; j < GRNstock.Rows.Count; j++)
                                {
                                    if (GRNstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (GRNstock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["GRN"] = GRNstock.Rows[j]["GRN"].ToString();
                                            GRN = GRNstock.Rows[j]["GRN"].ToString();
                                            break;
                                        }
                                    }
                                }
                                #endregion GRN
                                //purchase challan stock
                                dr["Purchase Challan"] = "0";
                                //DataRow[] drPurChallan = purchasechallnastock.Select("productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                //{
                                //    if (drPurChallan.Length > 0)
                                //    {
                                //        DataRow[] drPurChallan1 = purchasechallnastock.Select("batch='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                //        if (drPurChallan1.Length > 0)
                                //        {
                                //            dr["Purchase Challan"] = drPurChallan1[0]["PurchaseChallan"].ToString();
                                //            purchasec = drPurChallan1[0]["PurchaseChallan"].ToString();
                                //        }
                                //    }
                                //}
                                #region PChallan
                                for (int j = 0; j < purchasechallnastock.Rows.Count; j++)
                                {
                                    if (purchasechallnastock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (purchasechallnastock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Purchase Challan"] = purchasechallnastock.Rows[j]["PurchaseChallan"].ToString();
                                            purchasec = purchasechallnastock.Rows[j]["PurchaseChallan"].ToString();
                                            break;
                                        }
                                    }
                                }
                                #endregion PChallan
                                //stock in stock
                                dr["Stock In"] = "0";
                                //DataRow[] drStockin = stockin.Select("productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                //if (drStockin.Length > 0)
                                //{
                                //    DataRow[] drstockin1 = stockin.Select("batch='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                //    if (drstockin1.Length > 0)
                                //    {
                                //        dr["Stock In"] = drstockin1[0]["StockIn"].ToString();
                                //        stockinstr = drstockin1[0]["StockIn"].ToString();
                                //    }
                                //}
                                #region Stockin
                                for (int j = 0; j < stockin.Rows.Count; j++)
                                {
                                    if (stockin.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (stockin.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Stock In"] = stockin.Rows[j]["StockIn"].ToString();
                                            stockinstr = stockin.Rows[j]["StockIn"].ToString();
                                            break;
                                        }
                                    }
                                }
                                #endregion Stockin


                                //sale stock
                                dr["Sale"] = "0";
                                //DataRow[] drSale = salestock.Select("productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                //{
                                //    if (drSale.Length > 0)
                                //    {
                                //        DataRow[] drSale1 = salestock.Select("batch='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                //        if (drSale1.Length > 0)
                                //        {
                                //            dr["Sale"] = drSale1[0]["Sale"].ToString();
                                //            sale = drSale1[0]["Sale"].ToString();
                                //        }
                                //    }
                                //}
                                #region SaleStock
                                for (int j = 0; j < salestock.Rows.Count; j++)
                                {
                                    if (salestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (salestock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Sale"] = salestock.Rows[j]["Sale"].ToString();
                                            sale = salestock.Rows[j]["Sale"].ToString();
                                            break;
                                        }
                                    }
                                }
                                #endregion SaleStock
                                //GIN stock

                                dr["GIN"] = "0";
                                //DataRow[] drGIN = GINstock.Select("productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                //if (drGIN.Length > 0)
                                //{
                                //    DataRow[] drGIN1 = GINstock.Select("batch='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                //    if (drGIN1.Length > 0)
                                //    {
                                //        dr["GIN"] = drGIN1[0]["GIN"].ToString();
                                //        GIN = drGIN1[0]["GIN"].ToString();
                                //    }
                                //}
                                #region GIN
                                for (int j = 0; j < GINstock.Rows.Count; j++)
                                {
                                    if (GINstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (GINstock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["GIN"] = GINstock.Rows[j]["GIN"].ToString();
                                            GIN = GINstock.Rows[j]["GIN"].ToString();
                                            break;
                                        }
                                    }
                                }
                                #endregion GIN
                                //sale challan stock
                                dr["Sale Challan"] = "0";
                                //DataRow[] drSaleChallan = salechallnastock.Select("productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                //{
                                //    if (drSaleChallan.Length > 0)
                                //    {
                                //        DataRow[] drSaleChallan1 = salechallnastock.Select("batch='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                //        if (drSaleChallan1.Length > 0)
                                //        {
                                //            dr["Sale Challan"] = drSaleChallan1[0]["SaleChallan"].ToString();
                                //            salec = drSaleChallan1[0]["SaleChallan"].ToString();
                                //        }
                                //    }
                                //}

                                #region SaleChallan
                                for (int j = 0; j < salechallnastock.Rows.Count; j++)
                                {
                                    if (salechallnastock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (salechallnastock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Sale Challan"] = salechallnastock.Rows[j]["SaleChallan"].ToString();
                                            salec = salechallnastock.Rows[j]["SaleChallan"].ToString();
                                            break;
                                        }
                                    }
                                }
                                #endregion SaleChallan
                                //stock out stock
                                dr["Stock Out"] = "0";
                                //DataRow[] drStockout = stockout.Select("productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                //if (drStockout.Length > 0)
                                //{
                                //    DataRow[] drStockout1 = stockout.Select("batch='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                //    if (drStockout1.Length > 0)
                                //    {
                                //        dr["Stock Out"] = drStockout1[0]["StockOut"].ToString();
                                //        stockoutstr = drStockout1[0]["StockOut"].ToString();
                                //    }
                                //}
                                #region Stockout
                                for (int j = 0; j < stockout.Rows.Count; j++)
                                {
                                    if (stockout.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (stockout.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Stock Out"] = stockout.Rows[j]["StockOut"].ToString();
                                            stockoutstr = stockout.Rows[j]["StockOut"].ToString();
                                            break;
                                        }
                                    }
                                }
                                #endregion Stockout

                                //pos sttock
                                dr["POSSale"] = "0";
                                //DataRow[] drPOSSale = POSSale.Select("ItemName='" + product.Rows[i]["Product_Name"].ToString() + "'");
                                //{
                                //    if (drPOSSale.Length > 0)
                                //    {
                                //        DataRow[] drPOSSale1 = POSSale.Select("batchno='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                //        if (drPOSSale1.Length > 0)
                                //        {
                                //            dr["POSSale"] = drPOSSale1[0]["POSSale"].ToString();
                                //            possale = drPOSSale1[0]["POSSale"].ToString();
                                //        }
                                //    }
                                //}
                                #region POSSale
                                for (int j = 0; j < POSSale.Rows.Count; j++)
                                {
                                    if (POSSale.Rows[j]["ItemName"].ToString() == product.Rows[i]["Product_Name"].ToString())
                                    {
                                        if (POSSale.Rows[j]["batchno"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["POSSale"] = POSSale.Rows[j]["POSSale"].ToString();
                                            possale = POSSale.Rows[j]["POSSale"].ToString();
                                            break;
                                        }
                                    }
                                }
                                #endregion POSSale

                                //Sale Return Stock
                                dr["Sale Return"] = "0";
                                //DataRow[] DrSaleReturn = salereturnstock.Select("productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                //{
                                //    if (DrSaleReturn.Length > 0)
                                //    {
                                //        DataRow[] drsalereturn1 = salereturnstock.Select("batch='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                //        if (drsalereturn1.Length > 0)
                                //        {
                                //            dr["Sale Return"] = drsalereturn1[0]["SaleReturn"].ToString();
                                //            salereturn = drsalereturn1[0]["SaleReturn"].ToString();
                                //        }
                                //    }
                                //}
                                #region SaleReturn
                                for (int j = 0; j < salereturnstock.Rows.Count; j++)
                                {
                                    if (salereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (salereturnstock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Sale Return"] = salereturnstock.Rows[j]["SaleReturn"].ToString();
                                            salereturn = salereturnstock.Rows[j]["SaleReturn"].ToString();
                                            break;
                                        }
                                    }
                                }
                                #endregion SaleReturn
                                //Purchase Return Stock
                                dr["Purchase Return"] = "0";
                                //DataRow[] drPurchaseReturn = purchasereturnstock.Select("productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                //{
                                //    if (drPurchaseReturn.Length > 0)
                                //    {
                                //        DataRow[] drPurchaseReturn1 = purchasereturnstock.Select("batch='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                //        {
                                //            if (drPurchaseReturn1.Length > 0)
                                //            {
                                //                dr["Purchase Return"] = drPurchaseReturn1[0]["PurchaseReturn"].ToString();
                                //                purchasereturn = drPurchaseReturn1[0]["PurchaseReturn"].ToString();
                                //            }
                                //        }
                                //    }
                                //}
                                #region PReturn
                                for (int j = 0; j < purchasereturnstock.Rows.Count; j++)
                                {
                                    if (purchasereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (purchasereturnstock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Purchase Return"] = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
                                            purchasereturn = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
                                            break;
                                        }
                                    }
                                }
                                #endregion PReturn
                                dr["Production"] = "0";
                                //DataRow[] drProduction = production.Select("productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                //if (drProduction.Length > 0)
                                //{
                                //    DataRow[] drProduction1 = production.Select("batchno='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                //    if (drProduction1.Length > 0)
                                //    {
                                //        dr["Production"] = drProduction1[0]["fqty"].ToString();
                                //        pro = drProduction1[0]["fqty"].ToString();
                                //    }
                                //}
                                #region production
                                for (int j = 0; j < production.Rows.Count; j++)
                                {
                                    if (production.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (production.Rows[j]["batchno"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Production"] = production.Rows[j]["fqty"].ToString();
                                            pro = production.Rows[j]["fqty"].ToString();
                                            break;
                                        }
                                    }
                                }
                                #endregion production

                                #region FinishedQty
                                dr["Finished Qty"] = "0";
                                for (int j = 0; j < finishedqty.Rows.Count; j++)
                                {
                                    if (finishedqty.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (finishedqty.Rows[j]["batchno"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Finished Qty"] = finishedqty.Rows[j]["fqty"].ToString();
                                            finish = finishedqty.Rows[j]["fqty"].ToString();
                                            break;
                                        }
                                    }
                                }
                                #endregion FinishedQty
                                //    
                                //DataRow[] drfinishedQty = finishedqty.Select("productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                //if (drfinishedQty.Length > 0)
                                //{
                                //    DataRow[] drfinishedQty1 = finishedqty.Select("batchno='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                //    if (drfinishedQty1.Length > 0)
                                //    {
                                //        dr["Finished Qty"] = drfinishedQty1[0]["fqty"].ToString();
                                //        finish = drfinishedQty1[0]["fqty"].ToString();
                                //    }
                                //}
                                dr["ReUse"] = "0";
                                DataRow[] drreuseQty = reuseqty.Select("productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                if (drreuseQty.Length > 0)
                                {
                                    DataRow[] drreuseQty1 = reuseqty.Select("batchno='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                    if (drreuseQty1.Length > 0)
                                    {
                                        dr["ReUse"] = drreuseQty1[0]["reuse"].ToString();
                                        reuse = drreuseQty1[0]["reuse"].ToString();
                                    }
                                }
                                //closing
                                Double closing = Convert.ToDouble(opening) + Convert.ToDouble(purchase.ToString()) + Convert.ToDouble(GRN.ToString()) + Convert.ToDouble(purchasec.ToString()) + Convert.ToDouble(stockinstr.ToString()) + Convert.ToDouble(finish.ToString()) + Convert.ToDouble(reuse.ToString()) - Convert.ToDouble(sale.ToString()) - Convert.ToDouble(GIN.ToString()) - Convert.ToDouble(salec.ToString()) - Convert.ToDouble(stockoutstr.ToString()) - Convert.ToDouble(possale.ToString()) + Convert.ToDouble(salereturn.ToString()) - Convert.ToDouble(purchasereturn.ToString()) - Convert.ToDouble(pro.ToString());
                                dr["Closing"] = Math.Round(closing, 2).ToString("N2");
                                string options = cs.ExecuteScalar("select stockvalprice from options");
                                if (!string.IsNullOrEmpty(options))
                                {
                                    if (options == "Purchase Price")
                                    {
                                        totalamt = cs.getdataset("select PurchasePrice from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "' and batchno='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                        total = closing * (Convert.ToDouble(totalamt.Rows[0]["PurchasePrice"].ToString()));
                                    }
                                    else if (options == "Self Value")
                                    {
                                        totalamt = cs.getdataset("select SelfVal from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "' and batchno='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                        total = closing * (Convert.ToDouble(totalamt.Rows[0]["SelfVal"].ToString()));
                                    }
                                    else if (options == "Sale Price")
                                    {
                                        totalamt = cs.getdataset("select SalePrice from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "' and batchno='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                        total = closing * (Convert.ToDouble(totalamt.Rows[0]["SalePrice"].ToString()));
                                    }
                                    else if (options == "Basic Price")
                                    {
                                        totalamt = cs.getdataset("select BasicPrice from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "' and batchno='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                        total = closing * (Convert.ToDouble(totalamt.Rows[0]["BasicPrice"].ToString()));
                                    }
                                    else if (options == "Mrp")
                                    {
                                        totalamt = cs.getdataset("select MRP from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "' and batchno='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                        total = closing * (Convert.ToDouble(totalamt.Rows[0]["MRP"].ToString()));
                                    }
                                    else if (options == "Average Sale")
                                    {
                                        Double amount = 0;
                                        Double qty = 0;
                                        DataTable saledata = cs.getdataset("select Total,qty from BillProductMaster where isactive=1 and batch='" + isbatch.Rows[k]["batchno"].ToString() + "' and Billtype='S' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + DateTime.Now.ToString(Master.dateformate) + "'");
                                        for (int s = 0; s < saledata.Rows.Count; s++)
                                        {
                                            amount += Convert.ToDouble(saledata.Rows[s]["Amount"].ToString());
                                            qty += Convert.ToDouble(saledata.Rows[s]["qty"].ToString());
                                        }
                                        Double value = amount / qty;
                                        if ((Double.IsNaN(value) || Double.IsInfinity(value)))
                                        {
                                            value = 0;
                                        }
                                        total = value * closing;
                                    }
                                    else if (options == "Average Purchase")
                                    {
                                        Double amount = 0;
                                        Double qty = 0;
                                        DataTable purchasedata = cs.getdataset("select Total,qty from BillProductMaster where isactive=1 and batch='" + isbatch.Rows[k]["batchno"].ToString() + "' and Billtype='P' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + DateTime.Now.ToString(Master.dateformate) + "'");
                                        for (int s = 0; s < purchasedata.Rows.Count; s++)
                                        {
                                            amount += Convert.ToDouble(purchasedata.Rows[s]["Amount"].ToString());
                                            qty += Convert.ToDouble(purchasedata.Rows[s]["qty"].ToString());
                                        }
                                        Double value = amount / qty;
                                        if ((Double.IsNaN(value) || Double.IsInfinity(value)))
                                        {
                                            value = 0;
                                        }
                                        total = value * closing;
                                    }
                                    else if (options == "Average")
                                    {
                                        Double amount = 0;
                                        Double qty = 0;
                                        DataTable salepurchasedata = cs.getdataset("select Total,qty from BillProductMaster where isactive=1 and batch='" + isbatch.Rows[k]["batchno"].ToString() + "' and Billtype='P' and Billtype='S' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + DateTime.Now.ToString(Master.dateformate) + "'");
                                        for (int s = 0; s < salepurchasedata.Rows.Count; s++)
                                        {
                                            amount += Convert.ToDouble(salepurchasedata.Rows[s]["Amount"].ToString());
                                            qty += Convert.ToDouble(salepurchasedata.Rows[s]["qty"].ToString());
                                        }
                                        Double value = amount / qty;
                                        if ((Double.IsNaN(value) || Double.IsInfinity(value)))
                                        {
                                            value = 0;
                                        }
                                        total = value * closing;
                                    }
                                    else if (options == "Net Rate (Purchase)")
                                    {
                                        Double amount = 0;
                                        Double qty = 0;
                                        DataTable purchasedata = cs.getdataset("select Amount,qty from BillProductMaster where isactive=1 and batch='" + isbatch.Rows[k]["batchno"].ToString() + "' and Billtype='P' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + DateTime.Now.ToString(Master.dateformate) + "'");
                                        for (int s = 0; s < purchasedata.Rows.Count; s++)
                                        {
                                            amount += Convert.ToDouble(purchasedata.Rows[s]["Amount"].ToString());
                                            qty += Convert.ToDouble(purchasedata.Rows[s]["qty"].ToString());
                                        }
                                        Double value = amount / qty;
                                        if ((Double.IsNaN(value) || Double.IsInfinity(value)))
                                        {
                                            value = 0;
                                        }
                                        total = value * closing;
                                    }
                                    else if (options == "Net Rate (Sale)")
                                    {
                                        Double amount = 0;
                                        Double qty = 0;
                                        DataTable saledata = cs.getdataset("select Amount,qty from BillProductMaster where isactive=1 and batch='" + isbatch.Rows[k]["batchno"].ToString() + "' and Billtype='S' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + DateTime.Now.ToString(Master.dateformate) + "'");
                                        for (int s = 0; s < saledata.Rows.Count; s++)
                                        {
                                            amount += Convert.ToDouble(saledata.Rows[s]["Amount"].ToString());
                                            qty += Convert.ToDouble(saledata.Rows[s]["qty"].ToString());
                                        }
                                        Double value = amount / qty;
                                        if ((Double.IsNaN(value) || Double.IsInfinity(value)))
                                        {
                                            value = 0;
                                        }
                                        total = value * closing;
                                    }
                                    else if (options == "Min. Sale Rate")
                                    {
                                        Double amount = 0;
                                        Double qty = 0;
                                        DataTable saledata = cs.getdataset("select Rate,qty from BillProductMaster where isactive=1 and batch='" + isbatch.Rows[k]["batchno"].ToString() + "' and Billtype='S' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + DateTime.Now.ToString(Master.dateformate) + "'");
                                        if (saledata.Rows.Count > 0)
                                        {
                                            DataRow[] dr1 = saledata.Select("Rate = MIN(Rate)");

                                            amount = Convert.ToDouble(dr1[0][0].ToString());
                                        }
                                        if ((Double.IsNaN(amount) || Double.IsInfinity(amount)))
                                        {
                                            amount = 0;
                                        }
                                        for (int s = 0; s < saledata.Rows.Count; s++)
                                        {
                                            // amount += Convert.ToDouble(saledata.Rows[s]["Amount"].ToString());
                                            qty += Convert.ToDouble(saledata.Rows[s]["qty"].ToString());
                                        }
                                        Double value = amount / qty;
                                        if ((Double.IsNaN(value) || Double.IsInfinity(value)))
                                        {
                                            value = 0;
                                        }
                                        total = value * closing;


                                    }
                                    else if (options == "Min. Purchase Rate")
                                    {
                                        Double amount = 0;
                                        Double qty = 0;
                                        DataTable saledata = cs.getdataset("select Rate,qty from BillProductMaster where isactive=1 and batch='" + isbatch.Rows[k]["batchno"].ToString() + "' and Billtype='P' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + DateTime.Now.ToString(Master.dateformate) + "'");
                                        if (saledata.Rows.Count > 0)
                                        {
                                            DataRow[] dr1 = saledata.Select("Rate = MIN(Rate)");

                                            amount = Convert.ToDouble(dr1[0][0].ToString());
                                        }
                                        if ((Double.IsNaN(amount) || Double.IsInfinity(amount)))
                                        {
                                            amount = 0;
                                        }
                                        for (int s = 0; s < saledata.Rows.Count; s++)
                                        {
                                            // amount += Convert.ToDouble(saledata.Rows[s]["Amount"].ToString());
                                            qty += Convert.ToDouble(saledata.Rows[s]["qty"].ToString());
                                        }
                                        Double value = amount / qty;
                                        if ((Double.IsNaN(value) || Double.IsInfinity(value)))
                                        {
                                            value = 0;
                                        }
                                        total = value * closing;
                                    }
                                }
                                // DataTable totalamt = cs.getdataset("select PurchasePrice,SelfVal from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "' and batchno='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                // Double total = closing * (Convert.ToDouble(totalamt.Rows[0]["PurchasePrice"].ToString()) + Convert.ToDouble(totalamt.Rows[0]["SelfVal"].ToString()));
                                dr["Total Amount"] = total;
                                //Damage
                                dr["Adjust Stock"] = "0";
                                for (int j = 0; j < adjuststock.Rows.Count; j++)
                                {
                                    if (adjuststock.Rows[j]["itemid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                    {
                                        if (adjuststock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                        {
                                            dr["Adjust Stock"] = adjuststock.Rows[j]["adjuststock"].ToString();
                                            ajuststock = adjuststock.Rows[j]["adjuststock"].ToString();
                                            break;
                                        }
                                    }
                                }
                                Double finalclosing = Convert.ToDouble(closing) + Convert.ToDouble(ajuststock);
                                dr["Final Closing"] = Math.Round(finalclosing, 2).ToString("N2");
                                dt.Rows.Add(dr);


                            }
                        }

                    }
                }
                catch
                {
                }
                #endregion
            }
        }

        private void getdatatable4trading()
        {

            {
                opstock = cs.getdataset("select * from productpricemaster where isactive=1");
                production = cs.getdataset("select ISNULL(SUM(rawqty), 0) AS fqty,productid,batchno from tblproductionrawmaterialmaster where isactive=1 group by productid,batchno");
                finishedqty = cs.getdataset("select ISNULL(SUM(Qty), 0) AS fqty,productid,batchno from tblfinishedgoodsmaster where isactive=1 group by productid,batchno union all select ISNULL(SUM(Qty*-1), 0) AS fqty,productid,batchno from tblfinishedgoodsrejectionitems where isactive=1 group by productid,batchno");
                //    finishedrejectqty = cs.getdataset("select ISNULL(SUM(Qty), 0) AS fqty,productid,batchno from tblfinishedgoodsrejectionitems where isactive=1 group by productid,batchno");
                reuseqty = cs.getdataset("select ISNULL(SUM(aQty)-sum(fqty), 0) AS reuse,productid,batchno from tblfinishedgoodsqty where isactive=1 and rejectionorreuse='Reuse' group by productid,batchno");


                product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 order by p.product_name");
                POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName,batchno from BillPOSProductMaster where isactive=1 and BillRunDate<='" + DateTime.Now.ToString(Master.dateformate) + "' group by ItemName,batchno");
                purchasestock = cs.getdataset("select ISNULL(SUM(convert(float,Bags)+free), 0) AS Purchase, productid,batch from billproductmaster where Billtype = 'P' and Bill_Run_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                purchasechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,so.Bags)+so.free), 0) AS PurchaseChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'PC'and s.Bill_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                salestock = cs.getdataset("select ISNULL(SUM(convert(float,Bags)+free), 0) AS Sale, productid,batch from billproductmaster where Billtype = 'S' and Bill_Run_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                salechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,so.Bags)+so.free), 0) AS SaleChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'SC' and s.Bill_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                salereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Bags)+free), 0) AS SaleReturn, productid,batch from billproductmaster where Billtype = 'SR' and Bill_Run_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                purchasereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Bags)+free), 0) AS PurchaseReturn, productid,batch from billproductmaster where Billtype = 'PR' and Bill_Run_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid,batch from stockadujestmentitemmaster where stockdate<='" + DateTime.Now.ToString(Master.dateformate) + "' and isactive = 1 group by itemid,batch");
                stockin = cs.getdataset("select ISNULL(SUM(convert(float,so.Bags)+so.free), 0) AS StockIn, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'STI'and s.Bill_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                stockout = cs.getdataset("select ISNULL(SUM(convert(float,so.Bags)+so.free), 0) AS StockOut, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'STO' and s.Bill_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                GRNstock = cs.getdataset("select ISNULL(sum(qty), 0) AS GRN, productid,batch from tblgoodissuereturnnoteitemmaster where Billtype = 'GRN' and Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                GINstock = cs.getdataset("select ISNULL(sum(qty), 0) AS GIN, productid,batch from tblgoodissuereturnnoteitemmaster where Billtype = 'GIN' and Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
            }
        }
        public void StockReport_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if (p == "POS")
                {
                }
                else
                {
                    userrights = cs.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[40]["p"].ToString() == "False")
                        {

                        }
                        if (userrights.Rows[40]["v"].ToString() == "False")
                        {
                            btnok.Enabled = false;
                        }
                    }
                }
                DTPTo.CustomFormat = Master.dateformate;
                buttonclicked = 0;
                binddrop();
                con.Close();
            }
            catch
            {
            }
        }
        static int buttonclicked;
        public void binddrop()
        {
            try
            {
                DataTable dt = new DataTable();
                // SqlCommand cmd = new SqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='ProductMaster'", con);
                //SqlCommand cmd = new SqlCommand("select column_name from INFORMATION_SCHEMA.COLUMNS where (TABLE_NAME='ProductMaster' or TABLE_NAME='Companymaster') and (column_name like '%ProductID%' or column_name like '%Product_Name%' or column_name like '%GroupName%' or column_name like '%Packing%' or column_name like '%itemnumber%' or column_name like '%companyname%' )", con);
                SqlCommand cmd = new SqlCommand("select  COLUMN_NAME,s.value from INFORMATION_SCHEMA.COLUMNS i_s LEFT OUTER JOIN sys.extended_properties s ON s.major_id = OBJECT_ID(i_s.TABLE_SCHEMA+'.'+i_s.TABLE_NAME) AND s.minor_id = i_s.ORDINAL_POSITION AND s.name = 'MS_Description' where (TABLE_NAME='ProductMaster' or TABLE_NAME='Companymaster') and ( column_name like '%Product_Name%' or column_name like '%itemnumber%' or column_name like '%companyname%' )", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                // dt = sql.getdataset("select * from psm");
                DataRow dr;
                dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);
                dr["value"] = "--Select Column Name--";
                if (dt.Rows.Count != 0)
                {
                    // cmbname.DataSource = dt.DefaultView;
                    // cmbname.ValueMember = "sp_id";
                    // cmbname.DisplayMember = "p_name";
                    // btnclr.Enabled = true;
                    // cmbname.SelectedIndex = -1;
                    txtsearch.DataSource = dt;
                    txtsearch.DisplayMember = "COLUMN_NAME";
                    txtsearch.ValueMember = "value";
                }

            }
            catch
            {
            }
        }



        private void createtable()
        {
            dt = new DataTable();
            dt.Columns.Add("Item Code");
            dt.Columns.Add("Name of Item");
            dt.Columns.Add("Company");
            dt.Columns.Add("batch");
            dt.Columns.Add("Op. Stock");
            dt.Columns.Add("Op. Amt");
            dt.Columns.Add("Purchase");
            dt.Columns.Add("Purchase Amt");
            dt.Columns.Add("Purchase Challan");
            dt.Columns.Add("Stock Out");
            dt.Columns.Add("GRN");
            dt.Columns.Add("Sale");
            dt.Columns.Add("Sale Amt");
            dt.Columns.Add("Sale Challan");
            dt.Columns.Add("Stock In");
            dt.Columns.Add("GIN");
            dt.Columns.Add("POSSale");
            dt.Columns.Add("POSSale Amt");
            dt.Columns.Add("Sale Return");
            dt.Columns.Add("Purchase Return");
            dt.Columns.Add("Production");
            dt.Columns.Add("Finished Qty");
            dt.Columns.Add("ReUse");
            dt.Columns.Add("Closing");
            dt.Columns.Add("Total Amount");
            dt.Columns.Add("Adjust Stock");
            dt.Columns.Add("Final Closing");
            //dt.Columns.Add("Todays Physical Stock Entry");
            //dt.Columns.Add("Physical Stock Amt");
            //dt.Columns.Add("Remarks");
            dt.Columns.Add("Final Closing Amt");
        }
        private void getdatatable()
        {
            opstock = cs.getdataset("select * from productpricemaster where isactive=1");
            production = cs.getdataset("select ISNULL(SUM(rawqty), 0) AS fqty,productid,batchno from tblproductionrawmaterialmaster where isactive=1 group by productid,batchno");
            finishedqty = cs.getdataset("select ISNULL(SUM(Qty), 0) AS fqty,productid,batchno from tblfinishedgoodsmaster where isactive=1 group by productid,batchno union all select ISNULL(SUM(Qty*-1), 0) AS fqty,productid,batchno from tblfinishedgoodsrejectionitems where isactive=1 group by productid,batchno");
            //    finishedrejectqty = cs.getdataset("select ISNULL(SUM(Qty), 0) AS fqty,productid,batchno from tblfinishedgoodsrejectionitems where isactive=1 group by productid,batchno");
            reuseqty = cs.getdataset("select ISNULL(SUM(aQty)-sum(fqty), 0) AS reuse,productid,batchno from tblfinishedgoodsqty where isactive=1 and rejectionorreuse='Reuse' group by productid,batchno");


            product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 order by p.product_name");
            POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName,batchno from BillPOSProductMaster where isactive=1 and BillRunDate between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' group by ItemName,batchno");
            purchasestock = cs.getdataset("select ISNULL(SUM(convert(float,Bags)+free), 0) AS Purchase, productid,batch from billproductmaster where Billtype = 'P' and Bill_Run_Date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
            purchasechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,so.Bags)+so.free), 0) AS PurchaseChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'PC'and s.Bill_Date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
            salestock = cs.getdataset("select ISNULL(SUM(convert(float,Bags)+free), 0) AS Sale, productid,batch from billproductmaster where Billtype = 'S' and Bill_Run_Date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
            salechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,so.Bags)+so.free), 0) AS SaleChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'SC' and s.Bill_Date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
            salereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Bags)+free), 0) AS SaleReturn, productid,batch from billproductmaster where Billtype = 'SR' and Bill_Run_Date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
            purchasereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Bags)+free), 0) AS PurchaseReturn, productid,batch from billproductmaster where Billtype = 'PR' and Bill_Run_Date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
            adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid,batch,type from stockadujestmentitemmaster where stockdate between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and isactive = 1 group by itemid,batch,type");
            stockin = cs.getdataset("select ISNULL(SUM(convert(float,so.Bags)+so.free), 0) AS StockIn, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'STI'and s.Bill_Date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
            stockout = cs.getdataset("select ISNULL(SUM(convert(float,so.Bags)+so.free), 0) AS StockOut, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'STO' and s.Bill_Date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
            GRNstock = cs.getdataset("select ISNULL(sum(qty), 0) AS GRN, productid,batch from tblgoodissuereturnnoteitemmaster where Billtype = 'GRN' and Date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
            GINstock = cs.getdataset("select ISNULL(sum(qty), 0) AS GIN, productid,batch from tblgoodissuereturnnoteitemmaster where Billtype = 'GIN' and Date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
        }

        private void setdatatable()
        {
            #region
            try
            {
                dt.Rows.Clear();
                // progressBar1.Maximum = product.Rows.Count;
                // filelength = product.Rows.Count;
                for (int i = 0; i < product.Rows.Count; i++)
                {
                    DataTable isbatch = cs.getdataset("select * from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                    if (isbatch.Rows.Count > 0)
                    {

                        for (int k = 0; k < isbatch.Rows.Count; k++)
                        {
                            string opening = "0", purchase = "0", purchasec = "0", sale = "0", salec = "0", salereturn = "0", purchasereturn = "0", possale = "0", finish = "0", reuse = "0", pro = "0", ajuststock = "0", stockinstr = "0", stockoutstr = "0", GRN = "0", GIN = "0";
                            DataRow dr = dt.NewRow();
                            dr["Item Code"] = product.Rows[i]["itemnumber"].ToString();
                            dr["Name of Item"] = product.Rows[i]["Product_Name"].ToString();
                            dr["Company"] = product.Rows[i]["companyname"].ToString();
                            dr["batch"] = isbatch.Rows[k]["batchno"].ToString();

                            //opening stock
                            dr["Op. Stock"] = "0";

                            #region Opstock
                            for (int j = 0; j < opstock.Rows.Count; j++)
                            {
                                if (opstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    if (opstock.Rows[j]["batchno"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                    {
                                        Double opbal = methods.GetOpeningStockByProductID(opstock.Rows[j]["productid"].ToString(), opstock.Rows[j]["batchno"].ToString(), Convert.ToDateTime(DTPfrom.Text));
                                        dr["Op. Stock"] = opbal.ToString("N2");
                                        opening = opbal.ToString("N2");
                                        break;
                                    }
                                }

                            }
                            #endregion Opstock

                            //  dt.Columns.Add("Op. Amt");
                            double amt = methods.GetStockCalculatedAmount(Convert.ToDouble(opening), product.Rows[i]["ProductID"].ToString(), isbatch.Rows[k]["batchno"].ToString(), "", "Opening", Convert.ToDateTime(DTPfrom.Text), Convert.ToDateTime(DTPTo.Text));
                            dr["Op. Amt"] = amt.ToString("N2");

                            //purchase stock
                            dr["Purchase"] = "0";




                            #region Pstock
                            for (int j = 0; j < purchasestock.Rows.Count; j++)
                            {
                                if (purchasestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    if (purchasestock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                    {
                                        dr["Purchase"] = purchasestock.Rows[j]["Purchase"].ToString();
                                        purchase = purchasestock.Rows[j]["Purchase"].ToString();
                                        break;
                                    }
                                }
                            }
                            //GRN stock
                            #endregion Pstock

                            // dt.Columns.Add("Purchase Amt");
                            amt = methods.GetStockCalculatedAmount(Convert.ToDouble(purchase), product.Rows[i]["ProductID"].ToString(), isbatch.Rows[k]["batchno"].ToString(), "Purchase Price", "Purchase", Convert.ToDateTime(DTPfrom.Text), Convert.ToDateTime(DTPTo.Text));
                            dr["Purchase Amt"] = amt.ToString("N2");


                            dr["GRN"] = "0";

                            #region GRN
                            for (int j = 0; j < GRNstock.Rows.Count; j++)
                            {
                                if (GRNstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    if (GRNstock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                    {
                                        dr["GRN"] = GRNstock.Rows[j]["GRN"].ToString();
                                        GRN = GRNstock.Rows[j]["GRN"].ToString();
                                        break;
                                    }
                                }
                            }
                            #endregion GRN
                            //purchase challan stock
                            dr["Purchase Challan"] = "0";

                            #region PChallan
                            for (int j = 0; j < purchasechallnastock.Rows.Count; j++)
                            {
                                if (purchasechallnastock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    if (purchasechallnastock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                    {
                                        dr["Purchase Challan"] = purchasechallnastock.Rows[j]["PurchaseChallan"].ToString();
                                        purchasec = purchasechallnastock.Rows[j]["PurchaseChallan"].ToString();
                                        break;
                                    }
                                }
                            }
                            #endregion PChallan
                            //stock in stock
                            dr["Stock In"] = "0";

                            #region Stockin
                            for (int j = 0; j < stockin.Rows.Count; j++)
                            {
                                if (stockin.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    if (stockin.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                    {
                                        dr["Stock In"] = stockin.Rows[j]["StockIn"].ToString();
                                        stockinstr = stockin.Rows[j]["StockIn"].ToString();
                                        break;
                                    }
                                }
                            }
                            #endregion Stockin


                            //sale stock
                            dr["Sale"] = "0";

                            #region SaleStock
                            for (int j = 0; j < salestock.Rows.Count; j++)
                            {
                                if (salestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    if (salestock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                    {
                                        dr["Sale"] = salestock.Rows[j]["Sale"].ToString();
                                        sale = salestock.Rows[j]["Sale"].ToString();
                                        break;
                                    }
                                }
                            }
                            #endregion SaleStock

                            // dt.Columns.Add("Sale Amt");
                            amt = methods.GetStockCalculatedAmount(Convert.ToDouble(sale), product.Rows[i]["ProductID"].ToString(), isbatch.Rows[k]["batchno"].ToString(), "Basic Price", "Sale", Convert.ToDateTime(DTPfrom.Text), Convert.ToDateTime(DTPTo.Text));
                            dr["Sale Amt"] = amt.ToString("N2");

                            //GIN stock

                            dr["GIN"] = "0";

                            #region GIN
                            for (int j = 0; j < GINstock.Rows.Count; j++)
                            {
                                if (GINstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    if (GINstock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                    {
                                        dr["GIN"] = GINstock.Rows[j]["GIN"].ToString();
                                        GIN = GINstock.Rows[j]["GIN"].ToString();
                                        break;
                                    }
                                }
                            }
                            #endregion GIN
                            //sale challan stock
                            dr["Sale Challan"] = "0";


                            #region SaleChallan
                            for (int j = 0; j < salechallnastock.Rows.Count; j++)
                            {
                                if (salechallnastock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    if (salechallnastock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                    {
                                        dr["Sale Challan"] = salechallnastock.Rows[j]["SaleChallan"].ToString();
                                        salec = salechallnastock.Rows[j]["SaleChallan"].ToString();
                                        break;
                                    }
                                }
                            }
                            #endregion SaleChallan
                            //stock out stock
                            dr["Stock Out"] = "0";

                            #region Stockout
                            for (int j = 0; j < stockout.Rows.Count; j++)
                            {
                                if (stockout.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    if (stockout.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                    {
                                        dr["Stock Out"] = stockout.Rows[j]["StockOut"].ToString();
                                        stockoutstr = stockout.Rows[j]["StockOut"].ToString();
                                        break;
                                    }
                                }
                            }
                            #endregion Stockout

                            //pos sttock
                            dr["POSSale"] = "0";

                            #region POSSale
                            for (int j = 0; j < POSSale.Rows.Count; j++)
                            {
                                if (POSSale.Rows[j]["ItemName"].ToString() == product.Rows[i]["Product_Name"].ToString())
                                {
                                    if (POSSale.Rows[j]["batchno"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                    {
                                        dr["POSSale"] = POSSale.Rows[j]["POSSale"].ToString();
                                        possale = POSSale.Rows[j]["POSSale"].ToString();
                                        break;
                                    }
                                }
                            }
                            #endregion POSSale

                            //    dt.Columns.Add("POSSale Amt");
                            amt = methods.GetStockCalculatedAmount(Convert.ToDouble(possale), product.Rows[i]["ProductID"].ToString(), isbatch.Rows[k]["batchno"].ToString(), "Net Rate (Sale)", "POS", Convert.ToDateTime(DTPfrom.Text), Convert.ToDateTime(DTPTo.Text));
                            dr["POSSale Amt"] = amt.ToString("N2");


                            //Sale Return Stock
                            dr["Sale Return"] = "0";

                            #region SaleReturn
                            for (int j = 0; j < salereturnstock.Rows.Count; j++)
                            {
                                if (salereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    if (salereturnstock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                    {
                                        dr["Sale Return"] = salereturnstock.Rows[j]["SaleReturn"].ToString();
                                        salereturn = salereturnstock.Rows[j]["SaleReturn"].ToString();
                                        break;
                                    }
                                }
                            }
                            #endregion SaleReturn
                            //Purchase Return Stock
                            dr["Purchase Return"] = "0";

                            #region PReturn
                            for (int j = 0; j < purchasereturnstock.Rows.Count; j++)
                            {
                                if (purchasereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    if (purchasereturnstock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                    {
                                        dr["Purchase Return"] = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
                                        purchasereturn = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
                                        break;
                                    }
                                }
                            }
                            #endregion PReturn
                            dr["Production"] = "0";

                            #region production
                            for (int j = 0; j < production.Rows.Count; j++)
                            {
                                if (production.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    if (production.Rows[j]["batchno"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                    {
                                        dr["Production"] = production.Rows[j]["fqty"].ToString();
                                        pro = production.Rows[j]["fqty"].ToString();
                                        break;
                                    }
                                }
                            }
                            #endregion production

                            #region FinishedQty
                            dr["Finished Qty"] = "0";
                            for (int j = 0; j < finishedqty.Rows.Count; j++)
                            {
                                if (finishedqty.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    if (finishedqty.Rows[j]["batchno"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                    {
                                        dr["Finished Qty"] = finishedqty.Rows[j]["fqty"].ToString();
                                        finish = finishedqty.Rows[j]["fqty"].ToString();
                                        break;
                                    }
                                }
                            }
                            #endregion FinishedQty

                            dr["ReUse"] = "0";
                            DataRow[] drreuseQty = reuseqty.Select("productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                            if (drreuseQty.Length > 0)
                            {
                                DataRow[] drreuseQty1 = reuseqty.Select("batchno='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                                if (drreuseQty1.Length > 0)
                                {
                                    dr["ReUse"] = drreuseQty1[0]["reuse"].ToString();
                                    reuse = drreuseQty1[0]["reuse"].ToString();
                                }
                            }
                            //closing
                            Double closing = Convert.ToDouble(opening) + Convert.ToDouble(purchase.ToString()) + Convert.ToDouble(GRN.ToString()) + Convert.ToDouble(purchasec.ToString()) + Convert.ToDouble(stockinstr.ToString()) + Convert.ToDouble(finish.ToString()) + Convert.ToDouble(reuse.ToString()) - Convert.ToDouble(sale.ToString()) - Convert.ToDouble(GIN.ToString()) - Convert.ToDouble(salec.ToString()) - Convert.ToDouble(stockoutstr.ToString()) - Convert.ToDouble(possale.ToString()) + Convert.ToDouble(salereturn.ToString()) - Convert.ToDouble(purchasereturn.ToString()) - Convert.ToDouble(pro.ToString());
                            dr["Closing"] = Math.Round(closing, 2).ToString("N2");
                            total = methods.GetStockCalculatedAmount(closing, product.Rows[i]["ProductID"].ToString(), isbatch.Rows[k]["batchno"].ToString(), "", "Closing", Convert.ToDateTime(DTPfrom.Text), Convert.ToDateTime(DTPTo.Text));
                            // DataTable totalamt = cs.getdataset("select PurchasePrice,SelfVal from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "' and batchno='" + isbatch.Rows[k]["batchno"].ToString() + "'");
                            // Double total = closing * (Convert.ToDouble(totalamt.Rows[0]["PurchasePrice"].ToString()) + Convert.ToDouble(totalamt.Rows[0]["SelfVal"].ToString()));
                            dr["Total Amount"] = total;
                            //Damage
                            dr["Adjust Stock"] = "0";
                            for (int j = 0; j < adjuststock.Rows.Count; j++)
                            {
                                if (adjuststock.Rows[j]["itemid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    if (adjuststock.Rows[j]["batch"].ToString() == isbatch.Rows[k]["batchno"].ToString())
                                    {
                                        if (adjuststock.Rows[j]["type"].ToString() == "D")
                                        {
                                            dr["Adjust Stock"] = Convert.ToDouble(adjuststock.Rows[j]["adjuststock"].ToString()) * -1;
                                        }
                                        else
                                        {
                                            dr["Adjust Stock"] = Convert.ToDouble(adjuststock.Rows[j]["adjuststock"].ToString());
                                        }
                                        ajuststock = adjuststock.Rows[j]["adjuststock"].ToString();
                                        break;
                                    }
                                }
                            }
                            Double finalclosing = Convert.ToDouble(closing) + Convert.ToDouble(ajuststock);
                            dr["Final Closing"] = Math.Round(finalclosing, 2).ToString("N2");
                            dr["Final Closing Amt"] = Math.Round(methods.GetStockCalculatedAmount(finalclosing, product.Rows[i]["ProductID"].ToString(), isbatch.Rows[k]["batchno"].ToString(), "", "Closing", Convert.ToDateTime(DTPfrom.Text), Convert.ToDateTime(DTPTo.Text)), 2).ToString("N2");
                            //dr["Todays Physical Stock Entry"] = "0";
                            //dr["Physical Stock Amt"] = "0";
                            //dr["Remarks"] = "";
                            dt.Rows.Add(dr);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
            #endregion
        }

        private void setgrid()
        {
            #region






            bindgrid();
            #endregion
        }


        private double getopbal(string productid, string batchno)
        {
            // calculate opening balance

            #region
            progressBar1.Increment(1);
            string proid = productid;
            string openingstockfromitem = cs.ExecuteScalar("select opstock from productpricemaster where isactive=1 and productid= '" + proid + "' and Batchno='" + batchno + "'");
            string totalPurchase = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from billproductmaster where bill_Run_Date < '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and productid='" + proid + "' and isactive=1 and Billtype='P' and Batch='" + batchno + "'");
            string totalPR = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from billproductmaster where bill_Run_Date < '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and productid='" + proid + "' and isactive=1 and Billtype='PR' and Batch='" + batchno + "'");
            string totalSR = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from billproductmaster where bill_Run_Date < '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and productid='" + proid + "' and isactive=1 and Billtype='SR' and Batch='" + batchno + "'");
            string totalPC = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='PC' and s.OrderStatus='Pending' and Batch='" + batchno + "'");
            string totalSC = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='SC' and s.OrderStatus='Pending' and Batch='" + batchno + "'");
            string totalSTI = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='STI' and Batch='" + batchno + "'");
            string totalSTO = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and productid='" + proid + "' and so.isactive=1 and so.Billtype='STO' and Batch='" + batchno + "'");
            string totalSale = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from billproductmaster where bill_Run_Date < '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and productid='" + proid + "' and isactive=1 and Billtype='S' and Batch='" + batchno + "'");
            string POS = cs.ExecuteScalar("select ISNULL(SUM(Qty), 0) from BillPOSProductMaster where isactive=1 and itemid='" + proid + "' and BillRunDate <'" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and Batchno='" + batchno + "'");
            string totalGIN = cs.ExecuteScalar("select sum(qty) from tblgoodissuereturnnoteitemmaster where Date < '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and productid='" + proid + "' and isactive=1 and Billtype='GIN' and Batch='" + batchno + "'");
            string totalGRN = cs.ExecuteScalar("select sum(qty) from tblgoodissuereturnnoteitemmaster where Date < '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and productid='" + proid + "' and isactive=1 and Billtype='GRN' and Batch='" + batchno + "'");
            string totalProd = cs.ExecuteScalar("select sum(rawqty) from tblproductionrawmaterialmaster pm inner join tblproductionmaster p on p.id=pm.productionid and pm.isactive=1 where p.date < '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and pm.productid='" + proid + "' and p.isactive=1 and pm.batchno='" + batchno + "'");
            //string totalFG = cs.ExecuteScalar("select sum(fqty) from tblfinishedgoodsqty where date < '" + DTPFrom.Text + "' and productid='" + proid + "' and isactive=1 and batchno='" + cmbbatch.Text + "'");
            string totalFG = cs.ExecuteScalar("select sum(qty) from tblfinishedgoodsmaster where productionid in (select proitemid from tblfinishedgoodsQTY where isactive=1 and date < '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "') and productid='" + proid + "' and isactive=1 and batchno='" + batchno + "'");
            string totalSADG = cs.ExecuteScalar("select sum(adjuststock) from stockadujestmentitemmaster where stockdate < '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and itemid='" + proid + "' and isactive=1 and batch='" + batchno + "'");

            double purchaseamt = 0, saleamt = 0;

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
            if (totalSADG == "" || totalSADG == "NULL")
            {
                totalSADG = "0.00";
            }
            if (totalFG == "" || totalFG == "NULL")
            {
                totalFG = "0.00";
            }
            if (totalProd == "" || totalProd == "NULL")
            {
                totalProd = "0.00";
            }
            if (POS == "" || POS == "NULL")
            {
                POS = "0.00";
            }
            totalPurchase = (Convert.ToDouble(totalPurchase) + Convert.ToDouble(totalSR) + Convert.ToDouble(totalPC) + Convert.ToDouble(totalSTI) + Convert.ToDouble(totalGRN) + Convert.ToDouble(totalFG)).ToString();
            totalSale = (Convert.ToDouble(totalSale) + Convert.ToDouble(totalPR) + Convert.ToDouble(totalSC) + Convert.ToDouble(POS) + Convert.ToDouble(totalSTO) + Convert.ToDouble(totalGIN) + Convert.ToDouble(totalProd)).ToString();

            if (Convert.ToDouble(totalSADG) > 0)
            {
                totalPurchase = (Convert.ToDouble(totalPurchase) + Convert.ToDouble(totalSADG)).ToString();
            }
            else
            {
                totalSale = (Convert.ToDouble(totalSale) - Convert.ToDouble(totalSADG)).ToString();
            }
            Double opbal;
            string DC = "";
            if (Convert.ToDouble(totalPurchase) >= Convert.ToDouble(totalSale))
            {
                opbal = Convert.ToDouble(openingstockfromitem) + Convert.ToDouble(totalPurchase) - Convert.ToDouble(totalSale);
                //     txtopbal.Text = opbal.ToString("N2");
                DC = "D";
            }
            else
            {
                opbal = Convert.ToDouble(openingstockfromitem) - Convert.ToDouble(totalSale) + Convert.ToDouble(totalPurchase);
                //      txtopbal.Text = opbal.ToString("N2");
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
            return opbal;
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
        private void bindgrid()
        {
            openingtotal();
            grdstock.DataSource = dt;
            grdstock.BackgroundColor = Color.LightGray;
            grdstock.ForeColor = Color.Black;

            grdstock.Columns[0].Width = 49;
            grdstock.Columns[1].Width = 200;
            for (int i = 2; i < grdstock.Columns.Count - 1; i++)
            {
                grdstock.Columns[i].Width = 50;
            }

            grdstock.Columns[8].Visible = false;
            grdstock.Columns[9].Visible = false;
            grdstock.Columns[10].Visible = false;
            grdstock.Columns[11].Visible = false;
            grdstock.Columns[12].Visible = false;
            grdstock.Columns[13].Visible = false;
            grdstock.Columns[14].Visible = false;
            grdstock.Columns[15].Visible = false;
            grdstock.Columns[10].Visible = false;
            grdstock.Columns[11].Visible = false;
            grdstock.Columns[12].Visible = false;
            grdstock.Columns[13].Visible = false;
            grdstock.Columns[14].Visible = false;
            grdstock.Columns[15].Visible = false;

            //   grdstock.Columns[18].Visible = false;
            //   grdstock.Columns[19].Visible = false;
            grdstock.Columns[20].Visible = false;
            grdstock.Columns[21].Visible = false;
            grdstock.Columns[22].Visible = false;
            //grdstock.Columns[25].Visible = false;
            // grdstock.Columns[26].Visible = false;

            for (int i = 0; i < 27; i++)
            {
                grdstock.Columns[i].ReadOnly = true;
            }

            grdstock.Columns[28].ReadOnly = true;

            for (int i = 0; i < 27; i++)
            {
                grdstock.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
            }
            if (isMaster == true)
            {
                grdstock.Columns[27].Visible = false;
                grdstock.Columns[28].Visible = false;
                grdstock.Columns[29].Visible = false;
                grdstock.Columns[1].Width = 250;
                btnsubmit.Text = "Print";
            }
            grdstock.Columns[28].DefaultCellStyle.BackColor = Color.LightGray;


            grdstock.Columns[27].DefaultCellStyle.BackColor = Color.White;
            grdstock.Columns[29].DefaultCellStyle.BackColor = Color.White;
            grdstock.Columns[29].Width = 300;
            grdstock.Columns[30].DefaultCellStyle.BackColor = Color.LightGray;
            grdstock.Columns[30].DisplayIndex = 27;
            foreach (DataGridViewColumn column in grdstock.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // grdstock.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


        }
        bool isMaster = false;
        private void openingtotal()
        {
            d1 = 0; d2 = 0; d3 = 0; d4 = 0; d5 = 0; d6 = 0; d7 = 0; d8 = 0; d9 = 0; d10 = 0; d11 = 0; d12 = 0; d13 = 0; d14 = 0; d15 = 0;
            d16 = 0; d17 = 0; d18 = 0; d19 = 0; d20 = 0; d21 = 0; d22 = 0; d23 = 0; d24 = 0;
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        d1 += Convert.ToDouble(dt.Rows[i]["Op. Stock"].ToString());
                        d2 += Convert.ToDouble(dt.Rows[i]["Purchase"].ToString());
                        d3 += Convert.ToDouble(dt.Rows[i]["Purchase Challan"].ToString());
                        d4 += Convert.ToDouble(dt.Rows[i]["Sale"].ToString());
                        d5 += Convert.ToDouble(dt.Rows[i]["Sale Challan"].ToString());
                        d6 += Convert.ToDouble(dt.Rows[i]["POSSale"].ToString());
                        d7 += Convert.ToDouble(dt.Rows[i]["Sale Return"].ToString());
                        d8 += Convert.ToDouble(dt.Rows[i]["Purchase Return"].ToString());
                        d9 += Convert.ToDouble(dt.Rows[i]["Production"].ToString());
                        d10 += Convert.ToDouble(dt.Rows[i]["Finished Qty"].ToString());
                        d11 += Convert.ToDouble(dt.Rows[i]["ReUse"].ToString());
                        d12 += Convert.ToDouble(dt.Rows[i]["Closing"].ToString());
                        d13 += Convert.ToDouble(dt.Rows[i]["Total Amount"].ToString());
                        d14 += Convert.ToDouble(dt.Rows[i]["Adjust Stock"].ToString());
                        d15 += Convert.ToDouble(dt.Rows[i]["Final Closing"].ToString());

                        d16 += Convert.ToDouble(dt.Rows[i]["Op. Amt"].ToString());
                        d17 += Convert.ToDouble(dt.Rows[i]["Purchase Amt"].ToString());
                        d18 += Convert.ToDouble(dt.Rows[i]["Sale Amt"].ToString());
                        d19 += Convert.ToDouble(dt.Rows[i]["Stock Out"].ToString());
                        d20 += Convert.ToDouble(dt.Rows[i]["GRN"].ToString());
                        d21 += Convert.ToDouble(dt.Rows[i]["Stock In"].ToString());
                        d22 += Convert.ToDouble(dt.Rows[i]["GIN"].ToString());
                        d23 += Convert.ToDouble(dt.Rows[i]["POSSale Amt"].ToString());
                        d24 += Convert.ToDouble(dt.Rows[i]["Final Closing Amt"].ToString());


                    }
                    catch
                    {
                    }
                }
                DataRow dr = dt.NewRow();
                dr["Item Code"] = "Total";
                dr["Name of Item"] = "";
                dr["Company"] = "";
                dr["batch"] = "";
                dr["Op. Stock"] = d1;
                dr["Purchase"] = d2;
                dr["Purchase Challan"] = d3;
                dr["Sale"] = d4;
                dr["Sale Challan"] = d5;
                dr["POSSale"] = d6;
                dr["Sale Return"] = d7;
                dr["Purchase Return"] = d8;
                dr["Production"] = d9;
                dr["Finished Qty"] = d10;
                dr["ReUse"] = d11;
                dr["Closing"] = d12;
                dr["Total Amount"] = d13;
                dr["Adjust Stock"] = d14;
                dr["Final Closing"] = d15;

                dr["Op. Amt"] = d16;
                dr["Purchase Amt"] = d17;
                dr["Sale Amt"] = d18;
                dr["Stock Out"] = d19;
                dr["GRN"] = d20;
                dr["Stock In"] = d21;
                dr["GIN"] = d22;
                dr["POSSale Amt"] = d23;
                dr["Final Closing Amt"] = d24;




                closingval[0] = d13.ToString("N2");
                closingval[1] = d15.ToString("N2");

                dt.Rows.Add(dr);
            }
        }

        private void grdstock_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[10]["u"].ToString() == "True")
                    {
                        this.Enabled = false;
                        // iid = grdstock.Rows[grdstock.e.Index].SubItems[0].Text;
                        iid = grdstock.CurrentRow.Cells[1].Value.ToString();
                        ItemWiseStock dlg = new ItemWiseStock(master, tabControl);
                        dlg.getitemname(1, iid);
                        master.AddNewTab(dlg);
                        dlg.Show();
                    }
                    else
                    {
                        MessageBox.Show("You Don't Have Permission to Update.");
                        return;
                    }
                }
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void grdstock_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void BtnPayment_Click(object sender, EventArgs e)
        {

        }

        private void btncancel_Enter(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = false;
            btncancel.BackColor = Color.FromArgb(248, 152, 94);
            btncancel.ForeColor = Color.White;
        }

        private void btncancel_Leave(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = true;
            btncancel.BackColor = Color.FromArgb(51, 153, 255);
            btncancel.ForeColor = Color.White;
        }

        private void btncancel_MouseEnter(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = false;
            btncancel.BackColor = Color.FromArgb(248, 152, 94);
            btncancel.ForeColor = Color.White;
        }

        private void btncancel_MouseLeave(object sender, EventArgs e)
        {
            btncancel.UseVisualStyleBackColor = true;
            btncancel.BackColor = Color.FromArgb(51, 153, 255);
            btncancel.ForeColor = Color.White;
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (dr == DialogResult.Yes)
                {
                    master.RemoveCurrentTab();
                    //this.Close();
                }
            }
            catch
            {
                this.Close();
            }

        }

        private void btnprint_Click(object sender, EventArgs e)
        {

        }
        int i;
        private string p;
        private void btnok_Click(object sender, EventArgs e)
        {
            filelength = 1;
            progressBar1.Value = 0;
            i = 0;
            timer1.Interval = 1000;
            timer1.Start();
            timer1.Tick += new EventHandler(timer1_Tick);

            //this.StockReport_Load(sender, e);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //dt.Rows.Clear();
            //ok();
            filelength = 1;
            progressBar1.Value = 0;
            i = 0;
            timer1.Interval = 1000;
            timer1.Start();
            timer1.Tick += new EventHandler(timer1_Tick);
            //this.StockReport_Load(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i == 0)
            {
                dt.Rows.Clear();
                ok();
                if (flag == false)
                {
                    if (progressBar1.Value == filelength)
                    {
                        // flag = true;
                        timer1.Enabled = false;   //Add this line
                        timer1.Stop();
                        i = 1;
                    }
                }

            }
        }

        private void grdstock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdstock_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (grdstock.CurrentCell.ColumnIndex == 27)
                {

                    string proid = cs.getsinglevalue("Select Productid from productmaster where isactive=1 and itemnumber='" + grdstock.Rows[grdstock.CurrentRow.Index].Cells["Item Code"].Value.ToString() + "'");
                    double stock = methods.GetStockCalculatedAmount(Convert.ToDouble(grdstock.Rows[grdstock.CurrentRow.Index].Cells[27].Value), proid, grdstock.Rows[grdstock.CurrentRow.Index].Cells["batch"].Value.ToString(), "", "", Convert.ToDateTime(DTPfrom.Text), Convert.ToDateTime(DTPTo.Text));
                    grdstock.Rows[grdstock.CurrentRow.Index].Cells[28].Value = stock.ToString("N2");

                    double totalstock = 0, totalstockval = 0;
                    for (int i = 0; i < grdstock.Rows.Count - 1; i++)
                    {
                        if (grdstock.Rows[i].Cells[27].Value == "")
                            grdstock.Rows[i].Cells[27].Value = "0";
                        if (grdstock.Rows[i].Cells[28].Value == "")
                            grdstock.Rows[i].Cells[28].Value = "0";

                        totalstock += Convert.ToDouble(grdstock.Rows[i].Cells[27].Value);
                        totalstockval += Convert.ToDouble(grdstock.Rows[i].Cells[28].Value);
                    }
                    grdstock.Rows[grdstock.Rows.Count - 1].Cells[27].Value = totalstock.ToString("N2");
                    grdstock.Rows[grdstock.Rows.Count - 1].Cells[28].Value = totalstockval.ToString("N2");

                }
            }
            catch
            {
            }
        }
        public static string stockid;
        public static string entryid;
        public static string defaultmail;
        public static string custname;
        public static string autoprint = "false";
        public void bindid()
        {
            String str = cs.ExecuteScalar("select max(id) from dailystockentry");
            Int64 id;
            //     Object data = dr[1];

            if (str == "")
            {

                id = Convert.ToInt64("1");
            }
            else
            {
                id = Convert.ToInt32(str) + 1;
            }
            stockid = id.ToString();
        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            if (btnsubmit.Text == "Print")
            {
                DataTable dt1 = cs.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                defaultmail = dt1.Rows[0][11].ToString();
                custname = dt1.Rows[0][2].ToString();
                prn.execute("delete from printing");

                string salereturn = cs.getsinglevalue("select isnull(sum(totalnet),0) from BillMaster where isactive=1 and BillType='SR' and Bill_Date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                //string getdis = cs.getsinglevalue("select isnull(sum(discount),0) from (select sum(adddisamt) as discount from BillPOSMaster where isactive=1 and BillDate between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' union select sum(discount) as discount from BillPOSProductMaster where isactive=1 and BillRunDate between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "')entries");
                string getdis = cs.getsinglevalue("select sum(adddisamt) as discount from BillPOSMaster where isactive=1 and BillDate between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' ");
                string getpay = cs.getsinglevalue("select isnull(sum(netamt),0) from paymentreceipt where isactive=1 and type='P' and date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                string getexp = cs.getsinglevalue("select isnull(sum(ep.netamt),0) from tblExpvoucherproductmaster ep inner join tblExpvouchermaster e on e.id=ep.fkid and e.isactive=1 where ep.isactive=1 and e.date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                string damageamt = getdamageamt();
                string cashsale = cs.getsinglevalue("select isnull(sum(totalnet),0) from BillPOSMaster where isactive=1 and Terms='Cash' and  BillDate>='" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                string cardsale = cs.getsinglevalue("select isnull(sum(totalnet),0) from BillPOSMaster where isactive=1 and Terms='Credit/Debit Card' and  BillDate>='" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                string Walletsale = cs.getsinglevalue("select isnull(sum(totalnet),0) from BillPOSMaster where isactive=1 and Terms='E-Wallet' and  BillDate>='" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                string CreditSale = cs.getsinglevalue("select isnull(sum(totalnet),0) from BillPOSMaster where isactive=1 and Terms='Credit' and  BillDate>='" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                for (int i = 0; i < grdstock.Rows.Count - 1; i++)
                {
                    if (Convert.ToDouble(grdstock.Rows[i].Cells["Op. Stock"].Value) != 0 || Convert.ToDouble(grdstock.Rows[i].Cells["Op. Amt"].Value) != 0 ||
                        Convert.ToDouble(grdstock.Rows[i].Cells["Purchase"].Value) != 0 || Convert.ToDouble(grdstock.Rows[i].Cells["Purchase Amt"].Value) != 0 ||
                        Convert.ToDouble(grdstock.Rows[i].Cells["POSSale"].Value) != 0 || Convert.ToDouble(grdstock.Rows[i].Cells["POSSale Amt"].Value) != 0 ||
                        Convert.ToDouble(grdstock.Rows[i].Cells["Closing"].Value) != 0 || Convert.ToDouble(grdstock.Rows[i].Cells["Total Amount"].Value) != 0 ||
                        Convert.ToDouble(grdstock.Rows[i].Cells["Adjust Stock"].Value) != 0 || Convert.ToDouble(grdstock.Rows[i].Cells["Physical Stock Amt"].Value) != 0 ||
                        Convert.ToDouble(grdstock.Rows[i].Cells["Final Closing"].Value) != 0 || Convert.ToDouble(grdstock.Rows[i].Cells["Todays Physical Stock Entry"].Value) != 0)
                    {

                        string proid = cs.getsinglevalue("Select Productid from productmaster where isactive=1 and itemnumber='" + grdstock.Rows[i].Cells["Item Code"].Value + "'");
                        //  double closing = getclosing(grdstock.Rows[i].Cells[8].Value);
                        double stock = methods.GetStockCalculatedAmount(Convert.ToDouble(grdstock.Rows[i].Cells[25].Value), proid, grdstock.Rows[i].Cells["batch"].Value.ToString(), "", "", Convert.ToDateTime(DTPfrom.Text), Convert.ToDateTime(DTPTo.Text));
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58)VALUES";
                        qry += "('" + grdstock.Rows[i].Cells[0].Value + "','" + grdstock.Rows[i].Cells[1].Value + "','" + grdstock.Rows[i].Cells[2].Value + "','" + grdstock.Rows[i].Cells[3].Value + "','" + grdstock.Rows[i].Cells[4].Value + "','" + grdstock.Rows[i].Cells[5].Value + "','" + grdstock.Rows[i].Cells[6].Value + "','" + grdstock.Rows[i].Cells[7].Value + "','" + grdstock.Rows[i].Cells[8].Value + "','" + grdstock.Rows[i].Cells[9].Value + "','" + grdstock.Rows[i].Cells[10].Value + "','" + grdstock.Rows[i].Cells[11].Value + "','" + grdstock.Rows[i].Cells[12].Value + "','" + grdstock.Rows[i].Cells[13].Value + "','" + grdstock.Rows[i].Cells[14].Value + "','" + grdstock.Rows[i].Cells[15].Value + "','" + grdstock.Rows[i].Cells[16].Value + "','" + grdstock.Rows[i].Cells[17].Value + "','" + grdstock.Rows[i].Cells[18].Value + "','" + grdstock.Rows[i].Cells[19].Value + "','" + grdstock.Rows[i].Cells[20].Value + "','" + grdstock.Rows[i].Cells[21].Value + "','" + grdstock.Rows[i].Cells[22].Value + "','" + grdstock.Rows[i].Cells[23].Value + "','" + grdstock.Rows[i].Cells[24].Value + "','" + grdstock.Rows[i].Cells[25].Value + "','" + grdstock.Rows[i].Cells[26].Value + "','" + grdstock.Rows[i].Cells[27].Value + "','" + grdstock.Rows[i].Cells[28].Value + "','" + grdstock.Rows[i].Cells[29].Value + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + DTPTo.Text + "','" + txtremarks.Text + "','" + stock + "','" + getdis + "','" + getexp + "','" + getpay + "','" + salereturn + "','" + grdstock.Rows[i].Cells[30].Value + "','" + damageamt + "','" + cashsale + "','" + cardsale + "','" + Walletsale + "','" + CreditSale + "','" + DTPfrom.Text + "')";
                        prn.execute(qry);
                    }
                }
                Print popup = new Print("DailyStockEntry");
                //  MousedblClick();
                //   bd.btnCalculator_Click(sender, e);

                defaultmail = "";

                popup.btnpreview_Click(sender, e);

                autoprint = "false";
            }
            else
            {
                try
                {
                    //dt.Columns.Add("Todays Physical Stock Entry");
                    //dt.Columns.Add("Physical Stock Amt");
                    //dt.Columns.Add("Remarks");
                    double cnt = 0;
                    for (int i = 0; i < grdstock.Rows.Count; i++)
                    {
                        try
                        {
                            cnt += Convert.ToDouble(grdstock.Rows[i].Cells[27].Value);
                        }
                        catch
                        {
                        }
                    }
                    if (cnt == 0)
                    {
                        MessageBox.Show("Can not to submit without physical stock entry");
                        return;
                    }
                    else
                    {
                        this.Enabled = false;
                        if (btnsubmit.Text == "Update")
                        {
                            if (grdstock.Rows.Count > 0)
                            {
                                cs.execute("Update dailystockentryitemmaster set isactive=0 where stockid='" + lblid.Text + "'");
                                for (int i = 0; i < grdstock.Rows.Count; i++)
                                {
                                    if (Convert.ToDouble(grdstock.Rows[i].Cells["Final Closing"].Value) != 0 || Convert.ToDouble(grdstock.Rows[i].Cells["Todays Physical Stock Entry"].Value) != 0)
                                    {
                                        string proid = cs.getsinglevalue("Select Productid from productmaster where isactive=1 and itemnumber='" + grdstock.Rows[i].Cells["Item Code"].Value + "'");
                                        //  cs.execute("INSERT INTO [dbo].[dailystockentryitemmaster]([stockid],[itemid],[itemname],[closingstock],[adjuststock],[remarks],[isactive],[stockdate],[batch],[batchid])VALUES('" + lblid.Text + "','" + grdstock.Rows[i].Cells[0].Value + "','" + grdstock.Rows[i].Cells[2].Value + "','0','" + grdstock.Rows[i].Cells[6].Value + "','" + grdstock.Rows[i].Cells[7].Value + "','" + "1" + "','" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "','" + grdstock.Rows[i].Cells[4].Value + "','" + grdstock.Rows[i].Cells[8].Value + "')");
                                        cs.execute("INSERT INTO [dbo].[dailystockentryitemmaster]([stockid],[itemid],[itemname],[closingstock],[adjuststock],[remarks],[isactive],[stockdate],[batch],[batchid])VALUES('" + stockid + "','" + proid + "','" + grdstock.Rows[i].Cells["Name of Item"].Value + "','0','" + grdstock.Rows[i].Cells["Todays Physical Stock Entry"].Value + "','" + grdstock.Rows[i].Cells["Remarks"].Value + "','" + "1" + "','" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "','" + grdstock.Rows[i].Cells["batch"].Value + "','0')");
                                    }
                                }
                                cs.execute("Update dailystockentry set stockdate='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "',mainremark='" + txtremarks.Text + "' where id='" + lblid.Text + "'");
                                master.RemoveCurrentTab();
                                MessageBox.Show("Update Data Successfully");
                                this.Close();
                            }
                        }
                        else
                        {
                            if (grdstock.Rows.Count > 0)
                            {
                                bindid();
                                for (int i = 0; i < grdstock.Rows.Count - 1; i++)
                                {
                                    if (Convert.ToDouble(grdstock.Rows[i].Cells["Final Closing"].Value) != 0 || Convert.ToDouble(grdstock.Rows[i].Cells["Todays Physical Stock Entry"].Value) != 0)
                                    {
                                        string proid = cs.getsinglevalue("Select Productid from productmaster where isactive=1 and itemnumber='" + grdstock.Rows[i].Cells["Item Code"].Value + "'");
                                        cs.execute("INSERT INTO [dbo].[dailystockentryitemmaster]([stockid],[itemid],[itemname],[closingstock],[adjuststock],[remarks],[isactive],[stockdate],[batch],[batchid])VALUES('" + stockid + "','" + proid + "','" + grdstock.Rows[i].Cells["Name of Item"].Value + "','0','" + grdstock.Rows[i].Cells["Todays Physical Stock Entry"].Value + "','" + grdstock.Rows[i].Cells["Remarks"].Value + "','" + "1" + "','" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "','" + grdstock.Rows[i].Cells["batch"].Value + "','0')");
                                    }
                                }

                                cs.execute("INSERT INTO [dbo].[dailystockentry]([stockdate],[mainremark],[isactive]) VALUES('" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "','" + txtremarks.Text + "','" + "1" + "')");
                                entryid = cs.getsinglevalue("select max(id) from dailystockentry");
                                MessageBox.Show("Insert Data Successfully");

                                DataTable dt1 = cs.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                                defaultmail = dt1.Rows[0][11].ToString();
                                custname = dt1.Rows[0][2].ToString();
                                prn.execute("delete from printing");


                                string salereturn = cs.getsinglevalue("select isnull(sum(totalnet),0) from BillMaster where isactive=1 and BillType='SR' and Bill_Date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                                //string getdis = cs.getsinglevalue("select isnull(sum(discount),0) from (select sum(adddisamt) as discount from BillPOSMaster where isactive=1 and BillDate between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' union select sum(discount) as discount from BillPOSProductMaster where isactive=1 and BillRunDate between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "')entries");
                                string getdis = cs.getsinglevalue("select sum(adddisamt) as discount from BillPOSMaster where isactive=1 and BillDate between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "' ");
                                string getpay = cs.getsinglevalue("select isnull(sum(netamt),0) from paymentreceipt where isactive=1 and type='P' and date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                                string getexp = cs.getsinglevalue("select isnull(sum(ep.netamt),0) from tblExpvoucherproductmaster ep inner join tblExpvouchermaster e on e.id=ep.fkid and e.isactive=1 where ep.isactive=1 and e.date between '" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and '" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                                string damageamt = getdamageamt();
                                string cashsale = cs.getsinglevalue("select isnull(sum(totalnet),0) from BillPOSMaster where isactive=1 and Terms='Cash' and  BillDate>='" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                                string cardsale = cs.getsinglevalue("select isnull(sum(totalnet),0) from BillPOSMaster where isactive=1 and Terms='Credit/Debit Card' and  BillDate>='" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                                string Walletsale = cs.getsinglevalue("select isnull(sum(totalnet),0) from BillPOSMaster where isactive=1 and Terms='E-Wallet' and  BillDate>='" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                                string CreditSale = cs.getsinglevalue("select isnull(sum(totalnet),0) from BillPOSMaster where isactive=1 and Terms='Credit' and  BillDate>='" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and BillDate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
                                for (int i = 0; i < grdstock.Rows.Count - 1; i++)
                                {
                                    if (Convert.ToDouble(grdstock.Rows[i].Cells["Op. Stock"].Value) != 0 || Convert.ToDouble(grdstock.Rows[i].Cells["Op. Amt"].Value) != 0 ||
                                        Convert.ToDouble(grdstock.Rows[i].Cells["Purchase"].Value) != 0 || Convert.ToDouble(grdstock.Rows[i].Cells["Purchase Amt"].Value) != 0 ||
                                        Convert.ToDouble(grdstock.Rows[i].Cells["POSSale"].Value) != 0 || Convert.ToDouble(grdstock.Rows[i].Cells["POSSale Amt"].Value) != 0 ||
                                        Convert.ToDouble(grdstock.Rows[i].Cells["Closing"].Value) != 0 || Convert.ToDouble(grdstock.Rows[i].Cells["Total Amount"].Value) != 0 ||
                                        Convert.ToDouble(grdstock.Rows[i].Cells["Adjust Stock"].Value) != 0 || Convert.ToDouble(grdstock.Rows[i].Cells["Physical Stock Amt"].Value) != 0 ||
                                        Convert.ToDouble(grdstock.Rows[i].Cells["Final Closing"].Value) != 0 || Convert.ToDouble(grdstock.Rows[i].Cells["Todays Physical Stock Entry"].Value) != 0)
                                    {

                                        string proid = cs.getsinglevalue("Select Productid from productmaster where isactive=1 and itemnumber='" + grdstock.Rows[i].Cells["Item Code"].Value + "'");
                                        //  double closing = getclosing(grdstock.Rows[i].Cells[8].Value);
                                        double stock = methods.GetStockCalculatedAmount(Convert.ToDouble(grdstock.Rows[i].Cells[25].Value), proid, grdstock.Rows[i].Cells["batch"].Value.ToString(), "", "", Convert.ToDateTime(DTPfrom.Text), Convert.ToDateTime(DTPTo.Text));
                                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39,T40,T41,T42,T43,T44,T45,T46,T47,T48,T49,T50,T51,T52,T53,T54,T55,T56,T57,T58)VALUES";
                                        qry += "('" + grdstock.Rows[i].Cells[0].Value + "','" + grdstock.Rows[i].Cells[1].Value + "','" + grdstock.Rows[i].Cells[2].Value + "','" + grdstock.Rows[i].Cells[3].Value + "','" + grdstock.Rows[i].Cells[4].Value + "','" + grdstock.Rows[i].Cells[5].Value + "','" + grdstock.Rows[i].Cells[6].Value + "','" + grdstock.Rows[i].Cells[7].Value + "','" + grdstock.Rows[i].Cells[8].Value + "','" + grdstock.Rows[i].Cells[9].Value + "','" + grdstock.Rows[i].Cells[10].Value + "','" + grdstock.Rows[i].Cells[11].Value + "','" + grdstock.Rows[i].Cells[12].Value + "','" + grdstock.Rows[i].Cells[13].Value + "','" + grdstock.Rows[i].Cells[14].Value + "','" + grdstock.Rows[i].Cells[15].Value + "','" + grdstock.Rows[i].Cells[16].Value + "','" + grdstock.Rows[i].Cells[17].Value + "','" + grdstock.Rows[i].Cells[18].Value + "','" + grdstock.Rows[i].Cells[19].Value + "','" + grdstock.Rows[i].Cells[20].Value + "','" + grdstock.Rows[i].Cells[21].Value + "','" + grdstock.Rows[i].Cells[22].Value + "','" + grdstock.Rows[i].Cells[23].Value + "','" + grdstock.Rows[i].Cells[24].Value + "','" + grdstock.Rows[i].Cells[25].Value + "','" + grdstock.Rows[i].Cells[26].Value + "','" + grdstock.Rows[i].Cells[27].Value + "','" + grdstock.Rows[i].Cells[28].Value + "','" + grdstock.Rows[i].Cells[29].Value + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + DTPTo.Text + "','" + txtremarks.Text + "','" + stock + "','" + getdis + "','" + getexp + "','" + getpay + "','" + salereturn + "','" + grdstock.Rows[i].Cells[30].Value + "','" + damageamt + "','" + cashsale + "','" + cardsale + "','" + Walletsale + "','" + CreditSale + "','" + DTPfrom.Text + "')";
                                        prn.execute(qry);
                                    }
                                }


                                Print popup = new Print("DailyStockEntry");
                                autoprint = "False";
                                //  MousedblClick();
                                //   bd.btnCalculator_Click(sender, e);
                                popup.btnpreview_Click(sender, e);
                                master.RemoveCurrentTab();
                                autoprint = "false";
                                this.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    this.Close();
                }
            }
        }
        int columnindex;
        private string getdamageamt()
        {
            DataTable main = new DataTable();
            main.Columns.Add("Date", typeof(string));
            main.Columns.Add("Damage No", typeof(string));
            main.Columns.Add("Item No", typeof(string));
            main.Columns.Add("Item", typeof(string));
            main.Columns.Add("Company", typeof(string));
            main.Columns.Add("Group", typeof(string));

            main.Columns.Add("EntryTime Cl.Stock", typeof(string));
            main.Columns.Add("Damage Stock", typeof(string));
            main.Columns.Add("Price", typeof(string));
            main.Columns.Add("Total", typeof(string));
            main.Columns.Add("Remarks", typeof(string));

            DataTable dt = cs.getdataset(@"select s.*,p.itemnumber,p.GroupName,c.companyname from stockadujestmentitemmaster s inner join ProductMaster p on p.ProductID=s.itemid and p.isactive=1 inner join CompanyMaster c on c.CompanyID=p.CompanyID where s.isactive=1 and type='D' and s.stockdate>='" + Convert.ToDateTime(DTPfrom.Text).ToString(Master.dateformate) + "' and s.stockdate<='" + Convert.ToDateTime(DTPTo.Text).ToString(Master.dateformate) + "'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable productid = cs.getdataset("select productID from ProductMaster where isactive=1 and Product_Name='" + dt.Rows[i]["ItemName"].ToString() + "'");
                DataTable mrp = cs.getdataset("select purchaseprice from Productpricemaster where isactive=1 and productid='" + productid.Rows[0]["productID"].ToString() + "' and propriceid='" + dt.Rows[i]["batchid"].ToString() + "'");


                DataRow dr = main.NewRow();
                string d = Convert.ToDateTime(dt.Rows[i]["stockdate"]).ToString(Master.dateformate);
                dr["Date"] = d;
                dr["Damage No"] = dt.Rows[i]["stockid"].ToString();
                dr["Item No"] = dt.Rows[i]["itemnumber"].ToString();
                dr["Item"] = dt.Rows[i]["ItemName"].ToString();
                dr["Company"] = dt.Rows[i]["companyname"].ToString();
                dr["Group"] = dt.Rows[i]["GroupName"].ToString();
                dr["EntryTime Cl.Stock"] = dt.Rows[0]["ClosingStock"].ToString();
                dr["Damage Stock"] = (Convert.ToDouble(dt.Rows[i]["adjuststock"].ToString()) * -1);
                dr["Price"] = mrp.Rows[0]["purchaseprice"].ToString();
                Double taxamt = (Convert.ToDouble(dt.Rows[i]["adjuststock"].ToString()) * -1) * Convert.ToDouble(mrp.Rows[0]["purchaseprice"].ToString());
                dr["Total"] = taxamt;
                dr["Remarks"] = dt.Rows[i]["remarks"].ToString();
                main.Rows.Add(dr);
            }
            Double[] tot = new Double[dt.Columns.Count];
            for (int i = 0; i < main.Rows.Count; i++)
            {
                for (int j = 6; j < main.Columns.Count - 1; j++)
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
            return tot[9].ToString("N2");
        }
        private void FilterGridviewByEmpId(string query, int index)
        {
            SetAllRowsVisible(grdstock, false);
            foreach (DataGridViewRow row in grdstock.Rows)
            {
                if (row.Cells[index].Value.ToString().ToUpper().Contains(query))
                {
                    CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[grdstock.DataSource];
                    currencyManager1.SuspendBinding();
                    row.Visible = true;
                    currencyManager1.ResumeBinding();

                }
            }
        }
        private void SetAllRowsVisible(DataGridView grdstock, bool p)
        {
            foreach (DataGridViewRow row in grdstock.Rows)
            {
                CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[grdstock.DataSource];
                currencyManager1.SuspendBinding();
                row.Visible = p;
                currencyManager1.ResumeBinding();

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string columnname = "";

                if (txtsearch.Text == "companyname")
                {
                    columnname = "companyname";
                    columnindex = 2;
                }
                else if (txtsearch.Text == "GroupName")
                {
                    columnname = "GroupName";
                    columnindex = 5;
                }
                else if (txtsearch.Text == "Product_Name")
                {
                    columnname = "Product_Name";
                    columnindex = 1;
                }
                else if (txtsearch.Text == "itemnumber")
                {
                    columnname = "itemnumber";
                    columnindex = 0;

                }
                string query = txtser.Text.ToUpper();
                FilterGridviewByEmpId(query, columnindex);
            }
            catch
            {
            }
            //LoadData();
        }
    }
}