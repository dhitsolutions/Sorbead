using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RamdevSales.CommonClass
{
    public class CommonMethods
    {
        Connection cs = new Connection();
        DataTable totalamt = new DataTable();
        Double total = 0;

        public double GetOpeningStockByProductID(string productid, string batchno,DateTime date)
        {
            // calculate opening balance
            try
            {
                #region
                string openingstockfromitem = cs.ExecuteScalar("select opstock from productpricemaster where isactive=1 and productid= '" + productid + "' and Batchno='" + batchno + "'");
                string totalPurchase = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from billproductmaster where bill_Run_Date < '" + date.ToString(Master.dateformate) + "' and productid='" + productid + "' and isactive=1 and Billtype='P' and Batch='" + batchno + "'");
                string totalPR = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from billproductmaster where bill_Run_Date < '" + date.ToString(Master.dateformate) + "' and productid='" + productid + "' and isactive=1 and Billtype='PR' and Batch='" + batchno + "'");
                string totalSR = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from billproductmaster where bill_Run_Date < '" + date.ToString(Master.dateformate) + "' and productid='" + productid + "' and isactive=1 and Billtype='SR' and Batch='" + batchno + "'");
                string totalPC = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + date.ToString(Master.dateformate) + "' and productid='" + productid + "' and so.isactive=1 and so.Billtype='PC' and s.OrderStatus='Pending' and Batch='" + batchno + "'");
                string totalSC = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + date.ToString(Master.dateformate) + "' and productid='" + productid + "' and so.isactive=1 and so.Billtype='SC' and s.OrderStatus='Pending' and Batch='" + batchno + "'");
                string totalSTI = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + date.ToString(Master.dateformate) + "' and productid='" + productid + "' and so.isactive=1 and so.Billtype='STI' and Batch='" + batchno + "'");
                string totalSTO = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where bill_Run_Date < '" + date.ToString(Master.dateformate) + "' and productid='" + productid + "' and so.isactive=1 and so.Billtype='STO' and Batch='" + batchno + "'");
                string totalSale = cs.ExecuteScalar("select sum(convert(float,Bags)+free) from billproductmaster where bill_Run_Date < '" + date.ToString(Master.dateformate) + "' and productid='" + productid + "' and isactive=1 and Billtype='S' and Batch='" + batchno + "'");
                string POS = cs.ExecuteScalar("select ISNULL(SUM(Qty), 0) from BillPOSProductMaster where isactive=1 and itemid='" + productid + "' and BillRunDate <'" + date.ToString(Master.dateformate) + "' and Batchno='" + batchno + "'");
                string totalGIN = cs.ExecuteScalar("select sum(qty) from tblgoodissuereturnnoteitemmaster where Date < '" + date.ToString(Master.dateformate) + "' and productid='" + productid + "' and isactive=1 and Billtype='GIN' and Batch='" + batchno + "'");
                string totalGRN = cs.ExecuteScalar("select sum(qty) from tblgoodissuereturnnoteitemmaster where Date < '" + date.ToString(Master.dateformate) + "' and productid='" + productid + "' and isactive=1 and Billtype='GRN' and Batch='" + batchno + "'");
                string totalProd = cs.ExecuteScalar("select sum(rawqty) from tblproductionrawmaterialmaster pm inner join tblproductionmaster p on p.id=pm.productionid and pm.isactive=1 where p.date < '" + date.ToString(Master.dateformate) + "' and pm.productid='" + productid + "' and p.isactive=1 and pm.batchno='" + batchno + "'");
                //string totalFG = cs.ExecuteScalar("select sum(fqty) from tblfinishedgoodsqty where date < '" + date.Text + "' and productid='" + productid + "' and isactive=1 and batchno='" + cmbbatch.Text + "'");
                string totalFG = cs.ExecuteScalar("select sum(qty) from tblfinishedgoodsmaster where productionid in (select proitemid from tblfinishedgoodsQTY where isactive=1 and date < '" + date.ToString(Master.dateformate) + "') and productid='" + productid + "' and isactive=1 and batchno='" + batchno + "'");
                string totalSADG = cs.ExecuteScalar("select sum(adjuststock) from stockadujestmentitemmaster where stockdate < '" + date.ToString(Master.dateformate) + "' and itemid='" + productid + "' and isactive=1 and batch='" + batchno + "'");

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
            catch(Exception ex)
            {
                return 0;
            }
        }

        public double GetStockCalculatedAmount(double closing, string productid, string batchno, string options, string formType,DateTime dateFrom, DateTime dateTo)
        {

            //{
            //    productid = cs.getsinglevalue("Select productid from productmaster where itemnumber='" + productid + "'");
            //}

            if (options == "")
            {
                options = cs.ExecuteScalar("select stockvalprice from options");
            }
            if (!string.IsNullOrEmpty(options))
            {
                if (options == "Purchase Price")
                {
                    totalamt = cs.getdataset("select PurchasePrice from ProductPriceMaster where isactive=1 and Productid='" + productid + "' and batchno='" + batchno + "'");
                    total = closing * (Convert.ToDouble(totalamt.Rows[0]["PurchasePrice"].ToString()));
                }
                else if (options == "Self Value")
                {
                    totalamt = cs.getdataset("select SelfVal from ProductPriceMaster where isactive=1 and Productid='" + productid + "' and batchno='" + batchno + "'");
                    total = closing * (Convert.ToDouble(totalamt.Rows[0]["SelfVal"].ToString()));
                }
                else if (options == "Sale Price")
                {
                    totalamt = cs.getdataset("select SalePrice from ProductPriceMaster where isactive=1 and Productid='" + productid + "' and batchno='" + batchno + "'");
                    total = closing * (Convert.ToDouble(totalamt.Rows[0]["SalePrice"].ToString()));
                }
                else if (options == "Basic Price")
                {
                    totalamt = cs.getdataset("select BasicPrice from ProductPriceMaster where isactive=1 and Productid='" + productid + "' and batchno='" + batchno + "'");
                    total = closing * (Convert.ToDouble(totalamt.Rows[0]["BasicPrice"].ToString()));
                }
                else if (options == "Mrp")
                {
                    totalamt = cs.getdataset("select MRP from ProductPriceMaster where isactive=1 and Productid='" + productid + "' and batchno='" + batchno + "'");
                    total = closing * (Convert.ToDouble(totalamt.Rows[0]["MRP"].ToString()));
                }
                else if (options == "Average Sale")
                {
                    Double amount = 0;
                    Double qty = 0;
                    DataTable saledata = cs.getdataset("select Total,qty from BillProductMaster where isactive=1 and batch='" + batchno + "' and Billtype='S' and productid='" + productid + "' and Bill_Run_Date<='" + dateTo.ToString(Master.dateformate) + "'");
                    for (int s = 0; s < saledata.Rows.Count; s++)
                    {
                        amount += Convert.ToDouble(saledata.Rows[s]["Total"].ToString());
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
                    DataTable purchasedata = cs.getdataset("select Total,qty from BillProductMaster where isactive=1 and batch='" + batchno + "' and Billtype='P' and productid='" + productid + "' and Bill_Run_Date<='" + dateTo.ToString(Master.dateformate) + "'");
                    for (int s = 0; s < purchasedata.Rows.Count; s++)
                    {
                        amount += Convert.ToDouble(purchasedata.Rows[s]["Total"].ToString());
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
                    DataTable salepurchasedata = cs.getdataset("select Total,qty from BillProductMaster where isactive=1 and batch='" + batchno + "' and Billtype='P' and Billtype='S' and productid='" + productid + "' and Bill_Run_Date<='" + dateTo.ToString(Master.dateformate) + "'");
                    for (int s = 0; s < salepurchasedata.Rows.Count; s++)
                    {
                        amount += Convert.ToDouble(salepurchasedata.Rows[s]["Total"].ToString());
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
                    DataTable purchasedata = cs.getdataset("select Amount,qty from BillProductMaster where isactive=1 and batch='" + batchno + "' and Billtype='P' and productid='" + productid + "' and Bill_Run_Date<='" + dateTo.ToString(Master.dateformate) + "'");
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
                    DataTable saledata;
                    Double amount = 0;
                    Double qty = 0;
                    if (formType == "POS")
                    {
                        saledata = cs.getdataset("select isnull(sum(Amount),0) as Amount,isnull(sum(qty),0) as qty from billposproductmaster where isactive=1 and batchno='" + batchno + "' and itemid='" + productid + "' and  BillRunDate between '" + dateFrom.ToString(Master.dateformate) + "' and '" + dateTo.ToString(Master.dateformate) + "'");
                    }
                    else
                    {
                        saledata = cs.getdataset("select isnull(sum(Amount),0) as Amount,isnull(sum(qty),0) as qty from BillProductMaster where isactive=1 and batch='" + batchno + "' and Billtype='S' and productid='" + productid + "' and Bill_Run_Date between '" + dateFrom.ToString(Master.dateformate) + "' and '" + dateTo.ToString(Master.dateformate) + "'");
                    }
                    //for (int s = 0; s < saledata.Rows.Count; s++)
                    //{
                    //    amount += Convert.ToDouble(saledata.Rows[s]["Amount"].ToString());
                    //    qty += Convert.ToDouble(saledata.Rows[s]["qty"].ToString());
                    //}
                    //Double value = amount / qty;
                    //if ((Double.IsNaN(value) || Double.IsInfinity(value)))
                    //{
                    //    value = 0;
                    //}
                    if (saledata.Rows.Count > 0)
                        total = Convert.ToDouble(saledata.Rows[0]["Amount"].ToString());
                    else
                        total = 0;
                }
                else if (options == "Min. Sale Rate")
                {
                    Double amount = 0;
                    Double qty = 0;
                    DataTable saledata = cs.getdataset("select Rate,qty from BillProductMaster where isactive=1 and batch='" + batchno + "' and Billtype='S' and productid='" + productid + "' and Bill_Run_Date<='" + dateTo.ToString(Master.dateformate) + "'");
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
                    DataTable saledata = cs.getdataset("select Rate,qty from BillProductMaster where isactive=1 and batch='" + batchno + "' and Billtype='P' and productid='" + productid + "' and Bill_Run_Date<='" + dateTo.ToString(Master.dateformate) + "'");
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
            return total;
        }
    }
    

}
