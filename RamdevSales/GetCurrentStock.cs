using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RamdevSales
{
    
    class GetCurrentStock
    {
        Connection conn = new Connection();
        public string getstock(string p, string batch)
        {
            string currentstock;
            string proid = conn.ExecuteScalar("select Productid from ProductMaster where isactive=1 and Product_Name='" + p + "'");
            string openingstockfromitem = conn.ExecuteScalar("select opstock from productpricemaster where isactive=1 and productid= '" + proid + "' and Batchno='" + batch + "'");
            string totalPurchase = conn.ExecuteScalar("select sum(convert(float,Pqty)+free) from billproductmaster where productid='" + proid + "' and isactive=1 and Billtype='P' and Batch='" + batch + "'");
            string totalPR = conn.ExecuteScalar("select sum(convert(float,Pqty)+free) from billproductmaster where productid='" + proid + "' and isactive=1 and Billtype='PR' and Batch='" + batch + "'");
            string totalSR = conn.ExecuteScalar("select sum(convert(float,Pqty)+free) from billproductmaster where productid='" + proid + "' and isactive=1 and Billtype='SR' and Batch='" + batch + "'");
            string totalPC = conn.ExecuteScalar("select sum(convert(float,Pqty)+free) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where  productid='" + proid + "' and so.isactive=1 and so.Billtype='PC' and s.OrderStatus='Pending' and Batch='" + batch + "'");
            string totalSC = conn.ExecuteScalar("select sum(convert(float,Pqty)+free) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where  productid='" + proid + "' and so.isactive=1 and so.Billtype='SC' and s.OrderStatus='Pending' and Batch='" + batch + "'");
            string totalSTI = conn.ExecuteScalar("select sum(convert(float,Pqty)+free) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where  productid='" + proid + "' and so.isactive=1 and so.Billtype='STI' and s.OrderStatus='Pending' and Batch='" + batch + "'");
            string totalSTO = conn.ExecuteScalar("select sum(convert(float,Pqty)+free) from saleorderproductmaster so inner join SaleOrderMaster s on s.billno=so.billno and s.isactive=1 where  productid='" + proid + "' and so.isactive=1 and so.Billtype='STO' and s.OrderStatus='Pending' and Batch='" + batch + "'");
            string totalPOS = conn.ExecuteScalar("select sum(convert(float,Qty)) from billposproductmaster where itemid='" + proid + "' and isactive=1 and Batchno='" + batch + "'");
            string totalSale = conn.ExecuteScalar("select sum(convert(float,Pqty)+free) from billproductmaster where productid='" + proid + "' and isactive=1 and Billtype='S' and Batch='" + batch + "'");
            string totalGIN = conn.ExecuteScalar("select sum(qty) from tblgoodissuereturnnoteitemmaster where productid='" + proid + "' and isactive=1 and Billtype='GIN' and Batch='" + batch + "'");
            string totalGRN = conn.ExecuteScalar("select sum(qty) from tblgoodissuereturnnoteitemmaster where productid='" + proid + "' and isactive=1 and Billtype='GRN' and Batch='" + batch + "'");
            string totalProd = conn.ExecuteScalar("select sum(rawqty) from tblproductionrawmaterialmaster pm inner join tblproductionmaster p on p.id=pm.productionid and pm.isactive=1 where pm.productid='" + proid + "' and p.isactive=1 and pm.batchno='" + batch + "'");
            //string totalFG = conn.ExecuteScalar("select sum(fqty) from tblfinishedgoodsqty where productid='" + proid + "' and isactive=1 and batchno='" + batch + "'");
            string totalFG = conn.ExecuteScalar("select sum(qty) from tblfinishedgoodsmaster where productid='" + proid + "' and isactive=1 and batchno='" + batch + "'");
            string totalSADG = conn.ExecuteScalar("select sum(adjuststock) from stockadujestmentitemmaster where itemid='" + proid + "' and isactive=1 and batch='" + batch + "'");

            if (openingstockfromitem == "" || openingstockfromitem == "NULL")
            {
                openingstockfromitem = "0.00";
            }
            if (totalPurchase == "" || totalPurchase == "NULL")
            {
                totalPurchase = "0.00";
            }
            if (totalPOS == "" || totalPOS == "NULL")
            {
                totalPOS = "0.00";
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
            totalPurchase = (Convert.ToDouble(totalPurchase) + Convert.ToDouble(totalSR) + Convert.ToDouble(totalPC) + Convert.ToDouble(totalSTI) + Convert.ToDouble(totalGRN) + Convert.ToDouble(totalFG)).ToString();
            totalSale = (Convert.ToDouble(totalSale) + Convert.ToDouble(totalPR) + Convert.ToDouble(totalSC) + Convert.ToDouble(totalSTO) + Convert.ToDouble(totalGIN) + Convert.ToDouble(totalProd) + Convert.ToDouble(totalPOS)).ToString();

            if (Convert.ToDouble(totalSADG) > 0)
            {
                totalPurchase = (Convert.ToDouble(totalPurchase) + Convert.ToDouble(totalSADG)).ToString();
            }
            else
            {
                totalSale = (Convert.ToDouble(totalSale) + Convert.ToDouble(totalSADG)).ToString();
            }

            Double opbal;
            string DC = "";
            if (Convert.ToDouble(totalPurchase) >= Convert.ToDouble(totalSale))
            {
                opbal = Convert.ToDouble(openingstockfromitem) + Convert.ToDouble(totalPurchase) - Convert.ToDouble(totalSale);
                currentstock = opbal.ToString("N2");
                DC = "D";
            }
            else
            {
                opbal = Convert.ToDouble(openingstockfromitem) - Convert.ToDouble(totalSale) + Convert.ToDouble(totalPurchase);
                currentstock = opbal.ToString("N2");
                DC = "C";
            }
            return currentstock;
        }
    }
}
