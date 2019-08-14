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

namespace RamdevSales
{
    public partial class StockStatus : Form
    {
        //  Connection con = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection cs = new Connection();
        double optotal, d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14;
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
        DataTable purchasechallnastock = new DataTable();
        DataTable salereturnstock = new DataTable();
        DataTable purchasereturnstock = new DataTable();
        DataTable stockin = new DataTable();
        DataTable POSSale = new DataTable();
        DataTable production = new DataTable();
        DataTable finishedqty = new DataTable();
        DataTable adjuststock = new DataTable();
        public StockStatus()
        {
            InitializeComponent();
            grdstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            binditemdropdown();
            drpfilter.Text = "Show All Items";
            chkavailable.Checked = true;
            chkzero.Checked = true;
            chknegative.Checked = true;
        }

        public StockStatus(Master master, TabControl tabControl)
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



        DataTable userrights = new DataTable();
        static bool flag = false;
        int filelength = 1;
        public void ok()
        {
            try
            {
                progressBar1.Maximum = 4;
                filelength = 4;

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
           // progressBar1.Maximum = product.Rows.Count;
           // filelength = product.Rows.Count;
            try
            {
                for (int i = 0; i < product.Rows.Count; i++)
                {
                    //DataTable isbatch = cs.getdataset("select * from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                    //if (isbatch.Rows.Count > 0)
                    //{

                    //    for (int k = 0; k < isbatch.Rows.Count; k++)
                    //    {
                            string opening = "0", purchase = "0", purchasec = "0", sale = "0", salec = "0", salereturn = "0", purchasereturn = "0", possale = "0", finish = "0", pro = "0", ajuststock = "0", stockinstr = "0", stockoutstr = "0";
                            DataRow dr = dt.NewRow();
                            dr["Item Code"] = product.Rows[i]["itemnumber"].ToString();
                            dr["Name of Item"] = product.Rows[i]["Product_Name"].ToString();
                            dr["Item Group"] = product.Rows[i]["GroupName"].ToString();
                            dr["Company"] = product.Rows[i]["companyname"].ToString();
                       //     dr["batch"] = isbatch.Rows[k]["batchno"].ToString();
                            //dr["Part No"] = isbatch.Rows[k]["batchpartcode"].ToString();
                            //dr["Godown No"] = isbatch.Rows[k]["godownno"].ToString();
                            //dr["Packing Details"] = isbatch.Rows[k]["batchpacking"].ToString();
                            //opening stock
                            dr["Op. Stock"] = "0";
                            
                            
                            for (int j = 0; j < opstock.Rows.Count; j++)
                            {
                                if (opstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    
                                        dr["Op. Stock"] = opstock.Rows[j]["OpStock"].ToString();
                                        opening = opstock.Rows[j]["OpStock"].ToString();
                                        break;
                                   
                                }

                            }


                            //purchase stock
                            dr["Purchase"] = "0";
                            for (int j = 0; j < purchasestock.Rows.Count; j++)
                            {
                                if (purchasestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    
                                        dr["Purchase"] = purchasestock.Rows[j]["Purchase"].ToString();
                                        purchase = purchasestock.Rows[j]["Purchase"].ToString();
                                        break;
                                    
                                }
                            }

                            //purchase challan stock
                            dr["Purchase Challan"] = "0";
                            for (int j = 0; j < purchasechallnastock.Rows.Count; j++)
                            {
                                if (purchasechallnastock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    
                                        dr["Purchase Challan"] = purchasechallnastock.Rows[j]["PurchaseChallan"].ToString();
                                        purchasec = purchasechallnastock.Rows[j]["PurchaseChallan"].ToString();
                                        break;
                                    
                                }
                            }
                            //stock in stock
                            dr["Stock In"] = "0";
                            for (int j = 0; j < stockin.Rows.Count; j++)
                            {
                                if (stockin.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    
                                        dr["Stock In"] = stockin.Rows[j]["StockIn"].ToString();
                                        stockinstr = stockin.Rows[j]["StockIn"].ToString();
                                        break;
                                    
                                }
                            }


                            //sale stock
                            dr["Sale"] = "0";
                            for (int j = 0; j < salestock.Rows.Count; j++)
                            {
                                if (salestock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    
                                        dr["Sale"] = salestock.Rows[j]["Sale"].ToString();
                                        sale = salestock.Rows[j]["Sale"].ToString();
                                        break;
                                   
                                }
                            }
                            //sale stock
                            dr["Sale Challan"] = "0";
                            for (int j = 0; j < salechallnastock.Rows.Count; j++)
                            {
                                if (salechallnastock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    
                                        dr["Sale Challan"] = salechallnastock.Rows[j]["SaleChallan"].ToString();
                                        salec = salechallnastock.Rows[j]["SaleChallan"].ToString();
                                        break;
                                    
                                }
                            }
                            //stock out stock
                            dr["Stock Out"] = "0";
                            for (int j = 0; j < stockout.Rows.Count; j++)
                            {
                                if (stockout.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    
                                        dr["Stock Out"] = stockout.Rows[j]["StockOut"].ToString();
                                        stockoutstr = stockout.Rows[j]["StockOut"].ToString();
                                        break;
                                    
                                }
                            }


                            //pos sttock
                            dr["POSSale"] = "0";
                            for (int j = 0; j < POSSale.Rows.Count; j++)
                            {
                                if (POSSale.Rows[j]["ItemName"].ToString() == product.Rows[i]["Product_Name"].ToString())
                                {
                                    
                                        dr["POSSale"] = POSSale.Rows[j]["POSSale"].ToString();
                                        possale = POSSale.Rows[j]["POSSale"].ToString();
                                        break;
                                    
                                }
                            }


                            //Sale Return Stock
                            dr["Sale Return"] = "0";
                            for (int j = 0; j < salereturnstock.Rows.Count; j++)
                            {
                                if (salereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    
                                        dr["Sale Return"] = salereturnstock.Rows[j]["SaleReturn"].ToString();
                                        salereturn = salereturnstock.Rows[j]["SaleReturn"].ToString();
                                        break;
                                    
                                }
                            }

                            //Purchase Return Stock
                            dr["Purchase Return"] = "0";
                            for (int j = 0; j < purchasereturnstock.Rows.Count; j++)
                            {
                                if (purchasereturnstock.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                   
                                        dr["Purchase Return"] = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
                                        purchasereturn = purchasereturnstock.Rows[j]["PurchaseReturn"].ToString();
                                        break;
                                    
                                }
                            }
                            dr["Production"] = "0";
                            for (int j = 0; j < production.Rows.Count; j++)
                            {
                                if (production.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    
                                        dr["Production"] = production.Rows[j]["fqty"].ToString();
                                        pro = production.Rows[j]["fqty"].ToString();
                                        break;
                                    
                                }
                            }
                            dr["Finished Qty"] = "0";
                            for (int j = 0; j < finishedqty.Rows.Count; j++)
                            {
                                if (finishedqty.Rows[j]["productid"].ToString() == product.Rows[i]["ProductID"].ToString())
                                {
                                    
                                        dr["Finished Qty"] = finishedqty.Rows[j]["fqty"].ToString();
                                        finish = finishedqty.Rows[j]["fqty"].ToString();
                                        break;
                                    
                                }
                            }

                            //closing
                            Double closing = Convert.ToDouble(opening) + Convert.ToDouble(purchase.ToString()) + Convert.ToDouble(purchasec.ToString()) + Convert.ToDouble(stockinstr.ToString()) - Convert.ToDouble(sale.ToString()) - Convert.ToDouble(salec.ToString()) - Convert.ToDouble(stockoutstr.ToString()) - Convert.ToDouble(possale.ToString()) + Convert.ToDouble(salereturn.ToString()) - Convert.ToDouble(purchasereturn.ToString()) - Convert.ToDouble(pro.ToString() + Convert.ToDouble(finish.ToString()));
                            dr["Closing"] = Math.Round(closing, 2).ToString("N2");
                            string options = cs.ExecuteScalar("select stockvalprice from options");
                            if (!string.IsNullOrEmpty(options))
                            {
                                if (options == "Purchase Price")
                                {
                                    totalamt = cs.getdataset("select PurchasePrice from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                    total = closing * (Convert.ToDouble(totalamt.Rows[0]["PurchasePrice"].ToString()));
                                }
                                else if (options == "Self Value")
                                {
                                    totalamt = cs.getdataset("select SelfVal from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                    total = closing * (Convert.ToDouble(totalamt.Rows[0]["SelfVal"].ToString()));
                                }
                                else if (options == "Sale Price")
                                {
                                    totalamt = cs.getdataset("select SalePrice from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                    total = closing * (Convert.ToDouble(totalamt.Rows[0]["SalePrice"].ToString()));
                                }
                                else if (options == "Basic Price")
                                {
                                    totalamt = cs.getdataset("select BasicPrice from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                    total = closing * (Convert.ToDouble(totalamt.Rows[0]["BasicPrice"].ToString()));
                                }
                                else if (options == "Mrp")
                                {
                                    totalamt = cs.getdataset("select MRP from ProductPriceMaster where isactive=1 and Productid='" + product.Rows[i]["ProductID"].ToString() + "'");
                                    total = closing * (Convert.ToDouble(totalamt.Rows[0]["MRP"].ToString()));
                                }
                                else if (options == "Average Sale")
                                {
                                    Double amount = 0;
                                    Double qty = 0;
                                    DataTable saledata = cs.getdataset("select Total,qty from BillProductMaster where isactive=1 and Billtype='S' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'");
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
                                    DataTable purchasedata = cs.getdataset("select Total,qty from BillProductMaster where isactive=1 and Billtype='P' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'");
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
                                    DataTable salepurchasedata = cs.getdataset("select Total,qty from BillProductMaster where isactive=1 and Billtype='P' and Billtype='S' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'");
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
                                    DataTable purchasedata = cs.getdataset("select Amount,qty from BillProductMaster where isactive=1 and Billtype='P' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'");
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
                                    DataTable saledata = cs.getdataset("select Amount,qty from BillProductMaster where isactive=1 and Billtype='S' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'");
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
                                    DataTable saledata = cs.getdataset("select Rate,qty from BillProductMaster where isactive=1 and Billtype='S' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'");
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
                                    DataTable saledata = cs.getdataset("select Rate,qty from BillProductMaster where isactive=1 and Billtype='P' and productid='" + product.Rows[i]["ProductID"].ToString() + "' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "'");
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
                                    
                                        dr["Adjust Stock"] = adjuststock.Rows[j]["adjuststock"].ToString();
                                        ajuststock = adjuststock.Rows[j]["adjuststock"].ToString();
                                        break;
                                    
                                }
                            }
                            Double finalclosing = Convert.ToDouble(closing) + Convert.ToDouble(ajuststock);
                            dr["Final Closing"] = Math.Round(finalclosing, 2).ToString("N2");
                            dr["Min Qty"] = product.Rows[i]["minstock"].ToString();
                            dr["Max Qty"] = product.Rows[i]["maxstock"].ToString();

                            double reorderqty = Convert.ToDouble(product.Rows[i]["maxstock"].ToString()) - finalclosing;

                            if (Convert.ToDouble(product.Rows[i]["reorderqty"].ToString()) >= reorderqty)
                            {
                                dr["Re-order Qty"] = Math.Round(reorderqty, 2).ToString("N2");
                            }
                            else
                            {
                                dr["Re-order Qty"] = product.Rows[i]["reorderqty"].ToString();
                            }
                            dt.Rows.Add(dr);
                            
                    //    }
                    //}

                }
            }
            catch
            {
            }
            #endregion
        }
        private void getdatatable()
        {
            opstock = cs.getdataset("select productid, sum(cast(opstock as float)) as opstock  from ProductPriceMaster where isactive=1 group by Productid");
            production = cs.getdataset("select ISNULL(SUM(rawqty), 0) AS fqty,productid from tblproductionrawmaterialmaster where isactive=1 group by productid");
            finishedqty = cs.getdataset("select ISNULL(SUM(fQty), 0) AS fqty,productid from tblfinishedgoodsqty where isactive=1 group by productid");
            if (drpitems.Text == "GroupName" || drpitems.Text == "Packing" || drpitems.Text == "Hsn_Sac_Code" || drpitems.Text == "itemnumber" || drpitems.Text == "ProductID" || drpitems.Text == "Product_Name")
            {
                //product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.productid in (select Productid from productmaster where isactive=1 and (GroupName like '%" + txtitems.Text + "%' or Packing like '%" + txtitems.Text + "%' or Hsn_Sac_Code like '%" + txtitems.Text + "%' or ProductID like '%" + txtitems.Text + "%' or itemnumber like '%" + txtitems.Text + "%' or Product_Name like '%" + txtitems.Text + "%')) and p.isactive=1 order by p.product_name");
                product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and p.isactive=1 order by p.product_name");
                POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName from BillPOSProductMaster where isactive=1 and BillRunDate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Itemid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) group by ItemName");
                purchasestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Purchase, productid from billproductmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'P' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid");
                purchasechallnastock = cs.getdataset("select ISNULL(SUM(so.Pqty), 0) AS PurchaseChallan, so.productid from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and so.Billtype = 'PC'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid");
                salestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Sale, productid from billproductmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'S' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid");
                salechallnastock = cs.getdataset("select ISNULL(SUM(so.Pqty), 0) AS SaleChallan, so.productid from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and so.Billtype = 'SC' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid");
                salereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS SaleReturn, productid from billproductmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'SR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid");
                purchasereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS PurchaseReturn, productid from billproductmaster where productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and Billtype = 'PR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid");
                adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid from stockadujestmentitemmaster where itemid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and stockdate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by itemid");
                stockin = cs.getdataset("select ISNULL(SUM(so.Pqty), 0) AS StockIn, so.productid from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and so.Billtype = 'STI'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid");
                stockout = cs.getdataset("select ISNULL(SUM(so.Pqty), 0) AS StockOut, so.productid from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select Productid from productmaster where isactive=1 and (" + drpitems.Text + " like '%" + txtitems.Text + "%' )) and so.Billtype = 'STO' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid");
            }
            else if (drpitems.Text == "companyname")
            {
                product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and p.isactive=1 order by p.product_name");
                POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName from BillPOSProductMaster where isactive=1 and BillRunDate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and Itemid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) group by ItemName");
                purchasestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Purchase, productid from billproductmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'P' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid");
                purchasechallnastock = cs.getdataset("select ISNULL(SUM(so.Pqty), 0) AS PurchaseChallan, so.productid from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and so.Billtype = 'PC'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid");
                salestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Sale, productid from billproductmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'S' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid");
                salechallnastock = cs.getdataset("select ISNULL(SUM(so.Pqty), 0) AS SaleChallan, so.productid from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and so.Billtype = 'SC' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid");
                salereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS SaleReturn, productid from billproductmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'SR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid");
                purchasereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS PurchaseReturn, productid from billproductmaster where productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and Billtype = 'PR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid");
                adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid from stockadujestmentitemmaster where itemid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and stockdate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by itemid");
                stockout = cs.getdataset("select ISNULL(SUM(so.Pqty), 0) AS StockOut, so.productid from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and so.Billtype = 'STO' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid");
                stockin = cs.getdataset("select ISNULL(SUM(so.Pqty), 0) AS StockIn, so.productid from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.productid in (select productid from productmaster where isactive=1 and companyid in (select companyid from companymaster where isactive=1 and (companyname like '%" + txtitems.Text + "%'))) and so.Billtype = 'STI'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid");
            }
            else
            {
                product = cs.getdataset("select p.*,c.companyname from productmaster p inner join (select productid, max(saleprice) SalePrice,max(MRP) MRP, max(purchaseprice) PurchasePrice from productpricemaster group by productid) as pp on pp.productid=p.productid inner join companymaster c on c.companyid=p.companyid where p.isactive=1 order by p.product_name");
                POSSale = cs.getdataset("select ISNULL(SUM(Qty), 0) AS POSSale,ItemName from BillPOSProductMaster where isactive=1 and BillRunDate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' group by ItemName");
                purchasestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Purchase, productid from billproductmaster where Billtype = 'P' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid");
                purchasechallnastock = cs.getdataset("select ISNULL(SUM(so.Pqty), 0) AS PurchaseChallan, so.productid from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'PC'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid");
                salestock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS Sale, productid from billproductmaster where Billtype = 'S' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid");
                salechallnastock = cs.getdataset("select ISNULL(SUM(so.Pqty), 0) AS SaleChallan, so.productid from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'SC' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid");
                salereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS SaleReturn, productid from billproductmaster where Billtype = 'SR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid");
                purchasereturnstock = cs.getdataset("select ISNULL(SUM(Pqty), 0) AS PurchaseReturn, productid from billproductmaster where Billtype = 'PR' and Bill_Run_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by productid");
                adjuststock = cs.getdataset("select ISNULL(SUM(adjuststock), 0) AS adjuststock, itemid from stockadujestmentitemmaster where stockdate<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and isactive = 1 group by itemid");
                stockin = cs.getdataset("select ISNULL(SUM(so.Pqty), 0) AS StockIn, so.productid from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'STI'and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid");
                stockout = cs.getdataset("select ISNULL(SUM(so.Pqty), 0) AS StockOut, so.productid from SaleOrderProductMaster so inner join SaleOrderMaster s on so.BillNo=s.BillNo where so.Billtype = 'STO' and s.Bill_Date<='" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "' and so.isactive = 1 and s.isactive=1 and s.OrderStatus='Pending' group by so.productid");
            }

           
        }

        private void createtable()
        {
            dt = new DataTable();


            dt.Columns.Add("Item Code");
            dt.Columns.Add("Name of Item");
            dt.Columns.Add("Item Group");
            dt.Columns.Add("Company");
          //  dt.Columns.Add("batch");
            //dt.Columns.Add("Part No");
            //dt.Columns.Add("Godown No");
            //dt.Columns.Add("Packing Details");
            dt.Columns.Add("Op. Stock");
            dt.Columns.Add("Purchase");
            dt.Columns.Add("Purchase Challan");
            dt.Columns.Add("Stock Out");
            dt.Columns.Add("Sale");
            dt.Columns.Add("Sale Challan");
            dt.Columns.Add("Stock In");
            dt.Columns.Add("POSSale");
            dt.Columns.Add("Sale Return");
            dt.Columns.Add("Purchase Return");
            dt.Columns.Add("Production");
            dt.Columns.Add("Finished Qty");
            dt.Columns.Add("Closing");
            dt.Columns.Add("Total Amount");
            dt.Columns.Add("Adjust Stock");
            dt.Columns.Add("Final Closing");
            dt.Columns.Add("Min Qty");
            dt.Columns.Add("Max Qty");
            dt.Columns.Add("Re-order Qty");
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
            for (int i = 0; i < grdstock.Rows.Count; i++)
            {
                if (Convert.ToDouble(grdstock.Rows[i].Cells["Min Qty"].Value) > Convert.ToDouble(grdstock.Rows[i].Cells["Final Closing"].Value))
                {
                    for (int j = 0; j < grdstock.Columns.Count; j++)
                    {
                        grdstock.Rows[i].Cells[j].Style.BackColor = Color.OrangeRed;
                        grdstock.Rows[i].Cells[j].Style.ForeColor = Color.White;
                    }
                }
                else if (Convert.ToDouble(grdstock.Rows[i].Cells["Max Qty"].Value) < Convert.ToDouble(grdstock.Rows[i].Cells["Final Closing"].Value))
                {
                    for (int j = 0; j < grdstock.Columns.Count; j++)
                    {
                        grdstock.Rows[i].Cells[j].Style.BackColor = Color.YellowGreen;
                        grdstock.Rows[i].Cells[j].Style.ForeColor = Color.White;
                    }
                }
            }
            
            grdstock.ReadOnly = true;
            // grdstock.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


        }

        private void openingtotal()
        {
            d1 = 0; d2 = 0; d3 = 0; d4 = 0; d5 = 0; d6 = 0; d7 = 0; d8 = 0; d9 = 0; d10 = 0; d11 = 0; d12 = 0; d13 = 0; d14 = 0;
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
                        d11 += Convert.ToDouble(dt.Rows[i]["Closing"].ToString());
                        d12 += Convert.ToDouble(dt.Rows[i]["Total Amount"].ToString());
                        d13 += Convert.ToDouble(dt.Rows[i]["Adjust Stock"].ToString());
                        d14 += Convert.ToDouble(dt.Rows[i]["Final Closing"].ToString());
                    }
                    catch
                    {
                    }
                }
                DataRow dr = dt.NewRow();
                dr["Item Code"] = "Total";
                dr["Name of Item"] = "";
                dr["Company"] = "";
             //   dr["batch"] = "";
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
                dr["Closing"] = d11;
                dr["Total Amount"] = d12;
                dr["Adjust Stock"] = d13;
                dr["Final Closing"] = d14;

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
        private string[] strfinalarray;
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
    }
}
