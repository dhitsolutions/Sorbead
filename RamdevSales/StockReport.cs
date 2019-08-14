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
using ClosedXML.Excel;
using System.IO;

namespace RamdevSales
{
    public partial class StockReport : Form
    {
        //  Connection con = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection cs = new Connection();
        public double optotal, d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14,d15;
        public static string iid = "";
        DataTable dt = new DataTable();
        DataTable totalamt = new DataTable();
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
        public StockReport()
        {
            InitializeComponent();
            grdstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            binditemdropdown();
            drpfilter.Text = "Show All Items";
            chkavailable.Checked = true;
            chkzero.Checked = true;
            chknegative.Checked = true;
        }

        public StockReport(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            grdstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.master = master;
            this.tabControl = tabControl;
            binditemdropdown();
            drpfilter.Text = "Show All Items";
            chkavailable.Checked = true;
            chkzero.Checked = true;
            chknegative.Checked = true;
        }

        public StockReport(Master master, TabControl tabControl, string p)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            grdstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.master = master;
            this.tabControl = tabControl;
            this.p = p;
            binditemdropdown();
            drpfilter.Text = "Show All Items";
            chkavailable.Checked = true;
            chkzero.Checked = true;
            chknegative.Checked = true;
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
                if (DTPFrom.Text == "")
                {
                    DTPFrom.Value = DateTime.Now;
                }
                DTPFrom.CustomFormat = Master.dateformate;
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
        string [] closingval = new String[2];

        public string[] okfortrading()
        {
            try
            {
                progressBar1.Maximum = 4;
                filelength = 4;
                if (DTPFrom.Text == "")
                {
                    DTPFrom.Value = DateTime.Now;
                }
                DTPFrom.CustomFormat = Master.dateformate;
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
                                //Adjust Stock
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
                if (drpitems.Text == "GroupName" || drpitems.Text == "Packing" || drpitems.Text == "Hsn_Sac_Code" || drpitems.Text == "itemnumber" || drpitems.Text == "ProductID" || drpitems.Text == "Product_Name")
                {
                    //product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.productid in (select Productid from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' or Packing like '%" + txtitems.Text + "%' or Hsn_Sac_Code like '%" + txtitems.Text + "%' or ProductID like '%" + txtitems.Text + "%' or itemnumber like '%" + txtitems.Text + "%' or Product_Name like '%" + txtitems.Text + "%')) and p.isactive=1 order by p.product_name");
                    product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and p.isactive=1 order by p.product_name");
                    POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName,batchno from BillPOSProductMaster where isactive=1 and BillRunDate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Itemid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) group by ItemName,batchno");
                    purchasestock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS Purchase, productid,batch from billproductmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'P' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                    purchasechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS PurchaseChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and so.Billtype = 'PC'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                    salestock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS Sale, productid,batch from billproductmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'S' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                    salechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS SaleChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and so.Billtype = 'SC' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                    salereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS SaleReturn, productid,batch from billproductmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'SR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                    purchasereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS PurchaseReturn, productid,batch from billproductmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'PR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                    adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid,batch from stockadujestmentitemmaster where itemid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and stockdate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by itemid,batch");
                    stockin = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS StockIn, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and so.Billtype = 'STI'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                    stockout = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS StockOut, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and so.Billtype = 'STO' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                    GRNstock = cs.getdataset("select ISNULL(SUM(qty), 0) AS GRN, productid,batch from tblgoodissuereturnnoteitemmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'GRN' and Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                    GINstock = cs.getdataset("select ISNULL(SUM(qty), 0) AS GIN, productid,batch from tblgoodissuereturnnoteitemmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'GIN' and Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                }
                else if (drpitems.Text == "companyname")
                {
                    product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and p.isactive=1 order by p.product_name");
                    POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName,batchno from BillPOSProductMaster where isactive=1 and BillRunDate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Itemid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) group by ItemName,batchno");
                    purchasestock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS Purchase, productid,batch from billproductmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'P' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                    purchasechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,So.Pqty)+So.free), 0) AS PurchaseChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and so.Billtype = 'PC'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                    salestock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS Sale, productid,batch from billproductmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'S' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                    salechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS SaleChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and so.Billtype = 'SC' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                    salereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS SaleReturn, productid,batch from billproductmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'SR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                    purchasereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS PurchaseReturn, productid,batch from billproductmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'PR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                    adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid,batch from stockadujestmentitemmaster where itemid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and stockdate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by itemid,batch");
                    stockout = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS StockOut, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and so.Billtype = 'STO' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                    stockin = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS StockIn, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and so.Billtype = 'STI'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                    GRNstock = cs.getdataset("select ISNULL(SUM(qty), 0) AS GRN, productid,batch from tblgoodissuereturnnoteitemmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'GRN' and Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                    GINstock = cs.getdataset("select ISNULL(SUM(qty), 0) AS GIN, productid,batch from tblgoodissuereturnnoteitemmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'GIN' and Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                }
                else
                {
                    product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 order by p.product_name");
                    POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName,batchno from BillPOSProductMaster where isactive=1 and BillRunDate<='" + DateTime.Now.ToString(Master.dateformate) + "' group by ItemName,batchno");
                    purchasestock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS Purchase, productid,batch from billproductmaster where Billtype = 'P' and Bill_Run_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                    purchasechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS PurchaseChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'PC'and s.Bill_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                    salestock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS Sale, productid,batch from billproductmaster where Billtype = 'S' and Bill_Run_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                    salechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS SaleChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'SC' and s.Bill_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                    salereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS SaleReturn, productid,batch from billproductmaster where Billtype = 'SR' and Bill_Run_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                    purchasereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS PurchaseReturn, productid,batch from billproductmaster where Billtype = 'PR' and Bill_Run_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                    adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid,batch from stockadujestmentitemmaster where stockdate<='" + DateTime.Now.ToString(Master.dateformate) + "' and isactive = 1 group by itemid,batch");
                    stockin = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS StockIn, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'STI'and s.Bill_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                    stockout = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS StockOut, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'STO' and s.Bill_Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                    GRNstock = cs.getdataset("select ISNULL(sum(qty), 0) AS GRN, productid,batch from tblgoodissuereturnnoteitemmaster where Billtype = 'GRN' and Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                    GINstock = cs.getdataset("select ISNULL(sum(qty), 0) AS GIN, productid,batch from tblgoodissuereturnnoteitemmaster where Billtype = 'GIN' and Date<='" + DateTime.Now.ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                }

  

            }
        }
        public void StockReport_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                userrights = cs.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[40]["p"].ToString() == "False")
                    {
                        BtnPayment.Enabled = false;
                        btnprint.Enabled = false;
                    }
                    if (userrights.Rows[40]["v"].ToString() == "False")
                    {
                        btnok.Enabled = false;
                    }
                }
                DTPFrom.CustomFormat = Master.dateformate;
               
                con.Close();
            }
            catch
            {
            }
        }

        private void setgrid()
        {
            #region
            DataTable maindt = new DataTable();
            maindt = dt.Clone();

            if (dt.Rows.Count > 0)
            {

                if (drpfilter.Text == "Items Below Min. Level")
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        
                        string min = cs.getsinglevalue("select minstock from productmaster where isactive=1 and productid='" + dt.Rows[i]["Item Code"].ToString() + "'");
                        if (Convert.ToDouble(min) > Convert.ToDouble(dt.Rows[i]["Final Closing"].ToString()))
                        {
                            maindt.ImportRow(dt.Rows[i]);
                        }
                    }
                    dt = maindt;
                }
                else if (drpfilter.Text == "Items Below Reorder Level")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string reorder = cs.getsinglevalue("select reorderqty from productmaster where isactive=1 and productid='" + dt.Rows[i]["Item Code"].ToString() + "'");
                        if (Convert.ToDouble(reorder) > Convert.ToDouble(dt.Rows[i]["Final Closing"].ToString()))
                        {
                            maindt.ImportRow(dt.Rows[i]);
                        }
                    }
                    dt = maindt;
                }
                else if (drpfilter.Text == "Items Above Max. Level")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string maxstock = cs.getsinglevalue("select maxstock from productmaster where isactive=1 and productid='" + dt.Rows[i]["Item Code"].ToString() + "'");
                        if (Convert.ToDouble(maxstock) < Convert.ToDouble(dt.Rows[i]["Final Closing"].ToString()))
                        {
                            maindt.ImportRow(dt.Rows[i]);
                        }
                    }
                    dt = maindt;
                }

            }
            maindt = dt.Clone();

            if (dt.Rows.Count > 0)
            {

                int flag = 0;
                if (chknegative.Checked == false && chkavailable.Checked == false && chkzero.Checked == false)
                {
                    dt.Rows.Clear();
                }
                else if (chknegative.Checked == false && chkavailable.Checked == false && chkzero.Checked == true)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(dt.Rows[i]["Final Closing"].ToString()) == 0)
                        {
                            maindt.ImportRow(dt.Rows[i]);
                        }
                    }
                    dt = maindt;
                }
                else if (chknegative.Checked == false && chkavailable.Checked == true && chkzero.Checked == true)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(dt.Rows[i]["Final Closing"].ToString()) >= 0)
                        {
                            maindt.ImportRow(dt.Rows[i]);
                        }
                    }
                    dt = maindt;
                }
                else if (chknegative.Checked == true && chkavailable.Checked == false && chkzero.Checked == false)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(dt.Rows[i]["Final Closing"].ToString()) < 0)
                        {
                            maindt.ImportRow(dt.Rows[i]);
                        }
                    }
                    dt = maindt;
                }
                else if (chknegative.Checked == true && chkavailable.Checked == false && chkzero.Checked == true)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(dt.Rows[i]["Final Closing"].ToString()) <= 0)
                        {
                            maindt.ImportRow(dt.Rows[i]);
                        }
                    }
                    dt = maindt;
                }
                else if (chknegative.Checked == true && chkavailable.Checked == true && chkzero.Checked == false)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(dt.Rows[i]["Final Closing"].ToString()) < 0 || Convert.ToDouble(dt.Rows[i]["Final Closing"].ToString()) > 0)
                        {
                            maindt.ImportRow(dt.Rows[i]);
                        }
                    }
                    dt = maindt;
                }
                else if (chknegative.Checked == true && chkavailable.Checked == true && chkzero.Checked == false)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(dt.Rows[i]["Final Closing"].ToString()) < 0 || Convert.ToDouble(dt.Rows[i]["Final Closing"].ToString()) > 0)
                        {
                            maindt.ImportRow(dt.Rows[i]);
                        }
                    }
                    dt = maindt;
                }
                if (chknegative.Checked == false && chkavailable.Checked == true && chkzero.Checked == false)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(dt.Rows[i]["Final Closing"].ToString()) > 0)
                        {
                            maindt.ImportRow(dt.Rows[i]);
                        }
                    }
                    dt = maindt;
                }
            }

            ////get opening stock
            //opstock = cs.getdataset("SELECT TOP (100) PERCENT p.ProductID AS [Item Code], p.Product_Name AS [Name of Item], c.companyname AS Company, pp.OpStock AS [Op. Packs] FROM dbo.ProductMaster AS p LEFT OUTER JOIN dbo.ProductPriceMaster AS pp ON pp.Productid = p.ProductID INNER JOIN dbo.CompanyMaster AS c ON c.CompanyID = p.CompanyID ORDER BY [Name of Item]");
            //salestock = cs.getdataset("SELECT TOP (100) PERCENT p.ProductID AS [Item Code], p.Product_Name AS [Name of Item], c.companyname AS Company, ISNULL(SUM(sb.Pqty), 0) AS sale FROM dbo.ProductMaster AS p LEFT OUTER JOIN dbo.ProductPriceMaster AS pp ON pp.Productid = p.ProductID INNER JOIN dbo.CompanyMaster AS c ON c.CompanyID = p.CompanyID LEFT OUTER JOIN dbo.BillProductMaster AS sb ON sb.Productname = p.Product_Name AND sb.Billtype = 's' AND sb.isactive = 1 GROUP BY p.ProductID, p.Product_Name, c.companyname ORDER BY [Name of Item]");
            //purchasestock = cs.getdataset("SELECT TOP (100) PERCENT p.ProductID AS [Item Code], p.Product_Name AS [Name of Item], c.companyname AS Company, ISNULL(SUM(sb.Pqty), 0) AS Purchase FROM dbo.ProductMaster AS p LEFT OUTER JOIN dbo.ProductPriceMaster AS pp ON pp.Productid = p.ProductID INNER JOIN dbo.CompanyMaster AS c ON c.CompanyID = p.CompanyID LEFT OUTER JOIN dbo.BillProductMaster AS sb ON sb.Productname = p.Product_Name AND sb.Billtype = 'P' AND sb.isactive = 1 GROUP BY p.ProductID, p.Product_Name, c.companyname ORDER BY [Name of Item]");
            //purchasereturnstock = cs.getdataset("SELECT TOP (100) PERCENT p.ProductID AS [Item Code], p.Product_Name AS [Name of Item], c.companyname AS Company, ISNULL(SUM(sb.Pqty), 0) AS [Purchase Return] FROM dbo.ProductMaster AS p LEFT OUTER JOIN dbo.ProductPriceMaster AS pp ON pp.Productid = p.ProductID INNER JOIN dbo.CompanyMaster AS c ON c.CompanyID = p.CompanyID LEFT OUTER JOIN dbo.BillProductMaster AS sb ON sb.Productname = p.Product_Name AND sb.Billtype = 'PR' AND sb.isactive = 1 GROUP BY p.ProductID, p.Product_Name, c.companyname ORDER BY [Name of Item]");
            //salereturnstock = cs.getdataset("SELECT TOP (100) PERCENT p.ProductID AS [Item Code], p.Product_Name AS [Name of Item], c.companyname AS Company, ISNULL(SUM(sb.Pqty), 0) AS [Sale Return] FROM dbo.ProductMaster AS p LEFT OUTER JOIN dbo.ProductPriceMaster AS pp ON pp.Productid = p.ProductID INNER JOIN dbo.CompanyMaster AS c ON c.CompanyID = p.CompanyID LEFT OUTER JOIN dbo.BillProductMaster AS sb ON sb.Productname = p.Product_Name AND sb.Billtype = 'sr' AND sb.isactive = 1 GROUP BY p.ProductID, p.Product_Name, c.companyname ORDER BY [Name of Item]"); 



            //for (int i = 0; i < opstock.Rows.Count; i++)
            //{
            //    try
            //    {
            //        Double closing = Convert.ToDouble(opstock.Rows[i]["Op. Packs"].ToString()) + Convert.ToDouble(purchasestock.Rows[i]["Purchase"].ToString()) - Convert.ToDouble(salestock.Rows[i]["sale"].ToString()) + Convert.ToDouble(salereturnstock.Rows[i]["Sale Return"].ToString()) - Convert.ToDouble(purchasereturnstock.Rows[i]["Purchase Return"].ToString());
            //        dt.Rows.Add(opstock.Rows[i]["Item Code"].ToString(), opstock.Rows[i]["Name of Item"].ToString(), opstock.Rows[i]["Company"].ToString(), opstock.Rows[i]["Op. Packs"].ToString(), purchasestock.Rows[i]["Purchase"].ToString(), salestock.Rows[i]["sale"].ToString(), salereturnstock.Rows[i]["Sale Return"].ToString(), purchasereturnstock.Rows[i]["Purchase Return"].ToString(), Math.Round(closing, 0).ToString());
            //    }
            //    catch
            //    {
            //    }
            //}



            // dt = cs.getdataset("Select * from Stock_Management");
            bindgrid();
            #endregion
        }
        private void setdatatable()
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
                                    DataTable saledata = cs.getdataset("select Total,qty from BillProductMaster where isactive=1 and batch='" + isbatch.Rows[k]["batchno"].ToString() + "' and Billtype='S' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'");
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
                                    DataTable purchasedata = cs.getdataset("select Total,qty from BillProductMaster where isactive=1 and batch='" + isbatch.Rows[k]["batchno"].ToString() + "' and Billtype='P' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'");
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
                                    DataTable salepurchasedata = cs.getdataset("select Total,qty from BillProductMaster where isactive=1 and batch='" + isbatch.Rows[k]["batchno"].ToString() + "' and Billtype='P' and Billtype='S' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'");
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
                                    DataTable purchasedata = cs.getdataset("select Amount,qty from BillProductMaster where isactive=1 and batch='" + isbatch.Rows[k]["batchno"].ToString() + "' and Billtype='P' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'");
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
                                    DataTable saledata = cs.getdataset("select Amount,qty from BillProductMaster where isactive=1 and batch='" + isbatch.Rows[k]["batchno"].ToString() + "' and Billtype='S' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'");
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
                                    DataTable saledata = cs.getdataset("select Rate,qty from BillProductMaster where isactive=1 and batch='" + isbatch.Rows[k]["batchno"].ToString() + "' and Billtype='S' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'");
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
                                    DataTable saledata = cs.getdataset("select Rate,qty from BillProductMaster where isactive=1 and batch='" + isbatch.Rows[k]["batchno"].ToString() + "' and Billtype='P' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'");
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
                            //Adjust Stock
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
        private void getdatatable()
        {
            opstock = cs.getdataset("select * from productpricemaster where isactive=1");
            //production = cs.getdataset("select ISNULL(SUM(rawqty), 0) AS fqty,productid,batchno from tblproductionrawmaterialmaster where isactive=1 group by productid,batchno");
            production = cs.getdataset("select ISNULL(SUM(rawqty), 0) AS fqty,productid,batchno from tblproductionrawmaterialmaster where isactive=1 and productionid in (select id from tblproductionmaster where isactive=1 and date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "') group by productid,batchno");
            //finishedqty = cs.getdataset("select ISNULL(SUM(Qty), 0) AS fqty,productid,batchno from tblfinishedgoodsmaster where isactive=1 group by productid,batchno union all select ISNULL(SUM(Qty*-1), 0) AS fqty,productid,batchno from tblfinishedgoodsrejectionitems where isactive=1 group by productid,batchno");
            finishedqty = cs.getdataset("select ISNULL(SUM(Qty), 0) AS fqty,productid,batchno from tblfinishedgoodsmaster where isactive=1 and productionid in (select id from tblproductionmaster where isactive=1 and date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "') group by productid,batchno union all  select ISNULL(SUM(Qty*-1), 0) AS fqty,productid,batchno from tblfinishedgoodsrejectionitems where isactive=1 and  date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' group by productid,batchno");
        //    finishedrejectqty = cs.getdataset("select ISNULL(SUM(Qty), 0) AS fqty,productid,batchno from tblfinishedgoodsrejectionitems where isactive=1 group by productid,batchno");
            reuseqty = cs.getdataset("select ISNULL(SUM(aQty)-sum(fqty), 0) AS reuse,productid,batchno from tblfinishedgoodsqty where isactive=1 and rejectionorreuse='Reuse' and date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' group by productid,batchno");
            if (drpitems.Text == "GroupName" || drpitems.Text == "Packing" || drpitems.Text == "Hsn_Sac_Code" || drpitems.Text == "itemnumber" || drpitems.Text == "ProductID" || drpitems.Text == "Product_Name")
            {
                //product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.productid in (select Productid from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' or Packing like '%" + txtitems.Text + "%' or Hsn_Sac_Code like '%" + txtitems.Text + "%' or ProductID like '%" + txtitems.Text + "%' or itemnumber like '%" + txtitems.Text + "%' or Product_Name like '%" + txtitems.Text + "%')) and p.isactive=1 order by p.product_name");
                product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and p.isactive=1 order by p.product_name");
                POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName,batchno from BillPOSProductMaster where isactive=1 and BillRunDate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Itemid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) group by ItemName,batchno");
                purchasestock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS Purchase, productid,batch from billproductmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'P' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                purchasechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS PurchaseChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and so.Billtype = 'PC'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                salestock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS Sale, productid,batch from billproductmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'S' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                salechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS SaleChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and so.Billtype = 'SC' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                salereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS SaleReturn, productid,batch from billproductmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'SR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                purchasereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS PurchaseReturn, productid,batch from billproductmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'PR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid,batch from stockadujestmentitemmaster where itemid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and stockdate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by itemid,batch");
                stockin = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS StockIn, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and so.Billtype = 'STI'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                stockout = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS StockOut, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and so.Billtype = 'STO' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                GRNstock = cs.getdataset("select ISNULL(SUM(qty), 0) AS GRN, productid,batch from tblgoodissuereturnnoteitemmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'GRN' and Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                GINstock = cs.getdataset("select ISNULL(SUM(qty), 0) AS GIN, productid,batch from tblgoodissuereturnnoteitemmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'GIN' and Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
            }
            else if (drpitems.Text == "companyname")
            {
                product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and p.isactive=1 order by p.product_name");
                POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName,batchno from BillPOSProductMaster where isactive=1 and BillRunDate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Itemid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) group by ItemName,batchno");
                purchasestock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS Purchase, productid,batch from billproductmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'P' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                purchasechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,So.Pqty)+So.free), 0) AS PurchaseChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and so.Billtype = 'PC'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                salestock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS Sale, productid,batch from billproductmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'S' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                salechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS SaleChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and so.Billtype = 'SC' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                salereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS SaleReturn, productid,batch from billproductmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'SR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                purchasereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS PurchaseReturn, productid,batch from billproductmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'PR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid,batch from stockadujestmentitemmaster where itemid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and stockdate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by itemid,batch");
                stockout = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS StockOut, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and so.Billtype = 'STO' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                stockin = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS StockIn, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and so.Billtype = 'STI'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                GRNstock = cs.getdataset("select ISNULL(SUM(qty), 0) AS GRN, productid,batch from tblgoodissuereturnnoteitemmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'GRN' and Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                GINstock = cs.getdataset("select ISNULL(SUM(qty), 0) AS GIN, productid,batch from tblgoodissuereturnnoteitemmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'GIN' and Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
            }
            else
            {
                product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 order by p.product_name");
                POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName,batchno from BillPOSProductMaster where isactive=1 and BillRunDate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' group by ItemName,batchno");
                purchasestock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS Purchase, productid,batch from billproductmaster where Billtype = 'P' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                purchasechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS PurchaseChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'PC'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                salestock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS Sale, productid,batch from billproductmaster where Billtype = 'S' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                salechallnastock = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS SaleChallan, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'SC' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                salereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS SaleReturn, productid,batch from billproductmaster where Billtype = 'SR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                purchasereturnstock = cs.getdataset("select ISNULL(SUM(convert(float,Pqty)+free), 0) AS PurchaseReturn, productid,batch from billproductmaster where Billtype = 'PR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid,batch from stockadujestmentitemmaster where stockdate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by itemid,batch");
                stockin = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS StockIn, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'STI'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                stockout = cs.getdataset("select ISNULL(SUM(convert(float,so.Pqty)+so.free), 0) AS StockOut, so.productid,so.batch from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'STO' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid,so.batch");
                GRNstock = cs.getdataset("select ISNULL(sum(qty), 0) AS GRN, productid,batch from tblgoodissuereturnnoteitemmaster where Billtype = 'GRN' and Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
                GINstock = cs.getdataset("select ISNULL(sum(qty), 0) AS GIN, productid,batch from tblgoodissuereturnnoteitemmaster where Billtype = 'GIN' and Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid,batch");
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
            dt.Columns.Add("Purchase");
            dt.Columns.Add("Purchase Challan");
            dt.Columns.Add("Stock Out");
            dt.Columns.Add("GRN");
            dt.Columns.Add("Sale");
            dt.Columns.Add("Sale Challan");
            dt.Columns.Add("Stock In");
            dt.Columns.Add("GIN");
            dt.Columns.Add("POSSale");
            dt.Columns.Add("Sale Return");
            dt.Columns.Add("Purchase Return");
            dt.Columns.Add("Production");
            dt.Columns.Add("Finished Qty");
            dt.Columns.Add("ReUse");
            dt.Columns.Add("Closing");
            dt.Columns.Add("Total Amount");
            dt.Columns.Add("Adjust Stock");
            dt.Columns.Add("Final Closing");
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
            grdstock.Columns[0].Width = 49;
            grdstock.Columns[1].Width = 300;
            grdstock.ReadOnly = true;
            // grdstock.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            if (p == "IS")
            {
                for (int i = 4; i < 22; i++)
                    grdstock.Columns[i].Visible = false;
            }

        }

        private void openingtotal()
        {
            d1 = 0; d2 = 0; d3 = 0; d4 = 0; d5 = 0; d6 = 0; d7 = 0; d8 = 0; d9 = 0; d10 = 0; d11 = 0; d12 = 0; d13 = 0; d14 = 0; d15 = 0;
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



        private void BtnPayment_MouseEnter(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = false;
            BtnPayment.BackColor = Color.FromArgb(176, 111, 193);
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_MouseLeave(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = true;
            BtnPayment.BackColor = Color.FromArgb(51, 153, 255);
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_Enter(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = false;
            BtnPayment.BackColor = Color.FromArgb(176, 111, 193);
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_Leave(object sender, EventArgs e)
        {
            BtnPayment.UseVisualStyleBackColor = true;
            BtnPayment.BackColor = Color.FromArgb(51, 153, 255);
            BtnPayment.ForeColor = Color.White;
        }

        private void BtnPayment_Click(object sender, EventArgs e)
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
                            wb.Worksheets.Add(dt, "Stock");
                            // wb.Worksheets.Add(dt1, "ItemPrice");
                            wb.SaveAs(folderPath + "Stock_Management" + DateTimeName + ".xlsx");
                        }
                        MessageBox.Show("Export Data Sucessfully");
                        DialogResult dr = MessageBox.Show("Do you want to Open Document?", "Stock Management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folderPath + "Stock_Management" + DateTimeName + ".xlsx");
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
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
                //this.Close();
            }
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

        private void btnprint_Click(object sender, EventArgs e)
        {
            Printing prndata = new Printing();
            if (grdstock.Rows.Count > 0)
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print Stock Report?", "Stock Report", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    DataTable dt = new DataTable();
                    DataTable dt1 = cs.getdataset("select * from company WHERE isactive=1");
                    dt = (DataTable)grdstock.DataSource;
                    prndata.execute("delete from printing");
                    int j = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Opstock = "", ClosingStock = "", Purchase = "", Purchasec = "", Sale = "", Salec = "", PurchaseReturn = "", SaleReturn = "", total = "", possale = "", production = "";
                        string ItemName = "", company = "";
                        ItemName = dt.Rows[i][1].ToString();
                        company = dt.Rows[i][2].ToString();
                        Opstock = dt.Rows[i][4].ToString();
                        Purchase = dt.Rows[i][5].ToString();
                        Purchasec = dt.Rows[i][6].ToString();
                        Sale = dt.Rows[i][7].ToString();
                        Salec = dt.Rows[i][8].ToString();
                        possale = dt.Rows[i][9].ToString();
                        SaleReturn = dt.Rows[i][10].ToString();
                        PurchaseReturn = dt.Rows[i][11].ToString();
                        production = dt.Rows[i][12].ToString();
                        ClosingStock = dt.Rows[i][14].ToString();
                        total = dt.Rows[i][15].ToString();
                        string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29,T30,T31,T32,T33,T34,T35,T36,T37,T38,T39)VALUES";
                        qry += "('" + j++ + "','" + dt1.Rows[0][0].ToString() + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + ItemName + "','" + company + "','" + Opstock + "','" + Purchase + "','" + Sale + "','" + SaleReturn + "','" + PurchaseReturn + "','" + ClosingStock + "','" + total + "','" + txt1.Text + "','" + txt2.Text + "','" + txt3.Text + "','" + txt4.Text + "','" + txt5.Text + "','" + txt6.Text + "','" + txt7.Text + "','" + txt8.Text + "','" + possale + "','" + production + "','" + txt9.Text + "','" + Purchasec + "','" + Salec + "','" + txt10.Text + "','" + txt11.Text + "')";
                        prndata.execute(qry);


                    }
                    string reportName = "StockEvaluation";
                    //  string reportName = "Sale";
                    Print popup = new Print(reportName);
                    popup.ShowDialog();
                    popup.Dispose();
                }
            }
            else
            {
                MessageBox.Show("No Records For Print..");
            }
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

        private void txtitems_TextChanged(object sender, EventArgs e)
        {

        }

        private void drpitems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
