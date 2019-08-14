using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using Microsoft.Synchronization.Data;
using System.Data.SqlClient;
using Microsoft.Synchronization;

namespace SyncronizationServices
{
    public partial class Service1 : ServiceBase
    {
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        DataSet ds, ds1, ds2 = new DataSet();
        static string newsqlconnection;
        static string sServerConnection = newsqlconnection;
        static string sClientConnection = ConfigurationManager.ConnectionStrings["qry"].ToString();
        static string DateTimeName = DateTime.Now.ToString("dd_MMM_yyyy hh_mm_ss");
        static string sScope = "AccountCustomerType" + DateTimeName;
        static string sScope1 = "AccountGroup" + DateTimeName;
        static string sScope2 = "Additional" + DateTimeName;
        static string sScope3 = "Billchargesmaster" + DateTimeName;
        static string sScope4 = "BillMaster" + DateTimeName;
        static string sScope5 = "BillPOSMaster" + DateTimeName;
        static string sScope6 = "BillPOSProductMaster" + DateTimeName;
        static string sScope7 = "BillProductMaster" + DateTimeName;
        static string sScope8 = "BillSundry" + DateTimeName;
        static string sScope9 = "ChargesHead" + DateTimeName;
        static string sScope10 = "ChargesHeadApplyOn" + DateTimeName;
        static string sScope11 = "ClientMaster" + DateTimeName;
        static string sScope12 = "ClientProductMargin" + DateTimeName;
        static string sScope13 = "Company" + DateTimeName;
        static string sScope14 = "CompanyMaster" + DateTimeName;
        static string sScope15 = "complainmaster" + DateTimeName;
        static string sScope16 = "complainstatus" + DateTimeName;
        static string sScope17 = "Country_Master" + DateTimeName;
        static string sScope18 = "FormFormat" + DateTimeName;
        static string sScope19 = "fromcompany" + DateTimeName;
        static string sScope20 = "GroupMaster" + DateTimeName;
        static string sScope21 = "InwardMstr" + DateTimeName;
        static string sScope22 = "InwardProductMstr" + DateTimeName;
        static string sScope23 = "ItemGroupMaster" + DateTimeName;
        static string sScope24 = "ItemTaxMaster" + DateTimeName;
        static string sScope25 = "Ledger" + DateTimeName;
        static string sScope26 = "MenuMaster" + DateTimeName;
        static string sScope27 = "Options" + DateTimeName;
        static string sScope28 = "OwnerMaster" + DateTimeName;
        static string sScope29 = "PartyCompanyDiscount" + DateTimeName;
        static string sScope30 = "PartyGroupDiscount" + DateTimeName;
        static string sScope31 = "PartyRates" + DateTimeName;
        static string sScope32 = "PaymentMaster" + DateTimeName;
        static string sScope33 = "paymentreceipt" + DateTimeName;
        static string sScope34 = "ProductMaster" + DateTimeName;
        static string sScope35 = "ProductPriceMaster" + DateTimeName;
        static string sScope36 = "PurchasetypeMaster" + DateTimeName;
        static string sScope37 = "Ref" + DateTimeName;
        static string sScope38 = "SaleOrderchargesmaster" + DateTimeName;
        static string sScope39 = "SaleOrderMaster" + DateTimeName;
        static string sScope40 = "SaleOrderProductMaster" + DateTimeName;
        static string sScope41 = "SaletypeMaster" + DateTimeName;
        static string sScope42 = "Serials" + DateTimeName;
        static string sScope43 = "State_Master" + DateTimeName;
        static string sScope44 = "System_Master" + DateTimeName;
        static string sScope45 = "TaxSlabMaster" + DateTimeName;
        static string sScope46 = "TaxType" + DateTimeName;
        static string sScope47 = "tblcomplainmaster" + DateTimeName;
        static string sScope48 = "tblfinishedgoodsmaster" + DateTimeName;
        static string sScope49 = "tblfinishedgoodsqty" + DateTimeName;
        static string sScope50 = "tblitemcomplainmaster" + DateTimeName;
        static string sScope51 = "tblitemreceivefromcompany" + DateTimeName;
        static string sScope52 = "tblitemsendtocustomer" + DateTimeName;
        static string sScope53 = "tblprocessmaster" + DateTimeName;
        static string sScope54 = "tblproductgeneratedmaster" + DateTimeName;
        static string sScope55 = "tblproductionmaster" + DateTimeName;
        static string sScope56 = "tblproductionrawmaterialmaster" + DateTimeName;
        static string sScope57 = "tblreceivefromcompany" + DateTimeName;
        static string sScope58 = "tblrowmaterialsmaster" + DateTimeName;
        static string sScope59 = "tblsendtocompanyitemmaster" + DateTimeName;
        static string sScope60 = "tblsendtocompanymaster" + DateTimeName;
        static string sScope61 = "tblsendtocustomer" + DateTimeName;
        static string sScope62 = "tbluser_employeetype" + DateTimeName;
        static string sScope63 = "tocompany" + DateTimeName;
        static string sScope64 = "UnitMaster" + DateTimeName;
        static string sScope65 = "updatedatabase" + DateTimeName;
        static string sScope66 = "UserInfo" + DateTimeName;
        static string sScope67 = "UserRights" + DateTimeName;
        static string sScope68 = "Voucher" + DateTimeName;
        public Service1()
        {
            InitializeComponent();
        }
        public void bindserverconnection()
        {
            ds2 = ods.getdata("select * from Company");
            DataTable Sconnnection = new DataTable();
            Sconnnection = ds2.Tables[0];
            if (Sconnnection.Rows.Count > 0)
            {
                newsqlconnection = Sconnnection.Rows[0]["LinkRemote"].ToString();
            }
            sServerConnection = newsqlconnection;
        }
        public void ProvisionServer()
        {
            try
            {
                //AccountCustomerType
                #region

                //    conn.execute("Truncate table scope_info");
                //  conn.execute("Truncate table scope_config");
                //  conn.execute("Truncate table schema_info");
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                //conn.execute("Truncate table scope_info", serverConn);
                // conn.execute("Truncate table scope_config", serverConn);
                //conn.execute("Truncate table schema_info", serverConn);
                DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription(sScope);
                DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("AccountCustomerType", serverConn);
                scopeDesc.Tables.Add(tableDesc);
                SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);
                serverProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision.Apply();
                #endregion

                //AccountGroup
                #region
                SqlConnection serverConn1 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc1 = new DbSyncScopeDescription(sScope1);
                DbSyncTableDescription tableDesc1 = SqlSyncDescriptionBuilder.GetDescriptionForTable("AccountGroup", serverConn1);
                scopeDesc1.Tables.Add(tableDesc1);
                SqlSyncScopeProvisioning serverProvision1 = new SqlSyncScopeProvisioning(serverConn1, scopeDesc1);
                serverProvision1.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision1.Apply();
                #endregion

                //Additional
                #region
                SqlConnection serverConn2 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc2 = new DbSyncScopeDescription(sScope2);
                DbSyncTableDescription tableDesc2 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Additional", serverConn2);
                scopeDesc2.Tables.Add(tableDesc2);
                SqlSyncScopeProvisioning serverProvision2 = new SqlSyncScopeProvisioning(serverConn2, scopeDesc2);
                serverProvision2.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision2.Apply();
                #endregion


                //Billchargesmaster
                #region
                SqlConnection serverConn3 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc3 = new DbSyncScopeDescription(sScope3);
                DbSyncTableDescription tableDesc3 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Billchargesmaster", serverConn3);
                scopeDesc3.Tables.Add(tableDesc3);
                SqlSyncScopeProvisioning serverProvision3 = new SqlSyncScopeProvisioning(serverConn3, scopeDesc3);
                serverProvision3.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision3.Apply();
                #endregion


                //BillMaster
                #region
                SqlConnection serverConn4 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc4 = new DbSyncScopeDescription(sScope4);
                DbSyncTableDescription tableDesc4 = SqlSyncDescriptionBuilder.GetDescriptionForTable("BillMaster", serverConn4);
                scopeDesc4.Tables.Add(tableDesc4);
                SqlSyncScopeProvisioning serverProvision4 = new SqlSyncScopeProvisioning(serverConn4, scopeDesc4);
                serverProvision4.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision4.Apply();
                #endregion


                //BillPOSMaster
                #region
                SqlConnection serverConn5 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc5 = new DbSyncScopeDescription(sScope5);
                DbSyncTableDescription tableDesc5 = SqlSyncDescriptionBuilder.GetDescriptionForTable("BillPOSMaster", serverConn5);
                scopeDesc5.Tables.Add(tableDesc5);
                SqlSyncScopeProvisioning serverProvision5 = new SqlSyncScopeProvisioning(serverConn5, scopeDesc5);
                serverProvision5.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision5.Apply();
                #endregion


                //BillPOSProductMaster
                #region
                SqlConnection serverConn6 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc6 = new DbSyncScopeDescription(sScope6);
                DbSyncTableDescription tableDesc6 = SqlSyncDescriptionBuilder.GetDescriptionForTable("BillPOSProductMaster", serverConn6);
                scopeDesc6.Tables.Add(tableDesc6);
                SqlSyncScopeProvisioning serverProvision6 = new SqlSyncScopeProvisioning(serverConn6, scopeDesc6);
                serverProvision6.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision6.Apply();
                #endregion


                //BillProductMaster
                #region
                SqlConnection serverConn7 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc7 = new DbSyncScopeDescription(sScope7);
                DbSyncTableDescription tableDesc7 = SqlSyncDescriptionBuilder.GetDescriptionForTable("BillProductMaster", serverConn7);
                scopeDesc7.Tables.Add(tableDesc7);
                SqlSyncScopeProvisioning serverProvision7 = new SqlSyncScopeProvisioning(serverConn7, scopeDesc7);
                serverProvision7.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision7.Apply();
                #endregion

                //BillSundry
                #region
                SqlConnection serverConn8 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc8 = new DbSyncScopeDescription(sScope8);
                DbSyncTableDescription tableDesc8 = SqlSyncDescriptionBuilder.GetDescriptionForTable("BillSundry", serverConn8);
                scopeDesc8.Tables.Add(tableDesc8);
                SqlSyncScopeProvisioning serverProvision8 = new SqlSyncScopeProvisioning(serverConn8, scopeDesc8);
                serverProvision8.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision8.Apply();
                #endregion

                //ChargesHead
                #region
                SqlConnection serverConn9 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc9 = new DbSyncScopeDescription(sScope9);
                DbSyncTableDescription tableDesc9 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ChargesHead", serverConn9);
                scopeDesc9.Tables.Add(tableDesc9);
                SqlSyncScopeProvisioning serverProvision9 = new SqlSyncScopeProvisioning(serverConn9, scopeDesc9);
                serverProvision9.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision9.Apply();
                #endregion

                // ChargesHeadApplyOn
                #region
                SqlConnection serverConn10 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc10 = new DbSyncScopeDescription(sScope10);
                DbSyncTableDescription tableDesc10 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ChargesHeadApplyOn", serverConn10);
                scopeDesc10.Tables.Add(tableDesc10);
                SqlSyncScopeProvisioning serverProvision10 = new SqlSyncScopeProvisioning(serverConn10, scopeDesc10);
                serverProvision10.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision10.Apply();
                #endregion

                //ClientMaster
                #region
                SqlConnection serverConn11 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc11 = new DbSyncScopeDescription(sScope11);
                DbSyncTableDescription tableDesc11 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ClientMaster", serverConn11);
                scopeDesc11.Tables.Add(tableDesc11);
                SqlSyncScopeProvisioning serverProvision11 = new SqlSyncScopeProvisioning(serverConn11, scopeDesc11);
                serverProvision11.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision11.Apply();
                #endregion

                //ClientProductMargin
                #region
                SqlConnection serverConn12 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc12 = new DbSyncScopeDescription(sScope12);
                DbSyncTableDescription tableDesc12 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ClientProductMargin", serverConn12);
                scopeDesc12.Tables.Add(tableDesc12);
                SqlSyncScopeProvisioning serverProvision12 = new SqlSyncScopeProvisioning(serverConn12, scopeDesc12);
                serverProvision12.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision12.Apply();
                #endregion

                //Company
                #region
                SqlConnection serverConn13 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc13 = new DbSyncScopeDescription(sScope13);
                DbSyncTableDescription tableDesc13 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Company", serverConn13);
                scopeDesc13.Tables.Add(tableDesc13);
                SqlSyncScopeProvisioning serverProvision13 = new SqlSyncScopeProvisioning(serverConn13, scopeDesc13);
                serverProvision13.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision13.Apply();
                #endregion

                //CompanyMaster
                #region
                SqlConnection serverConn14 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc14 = new DbSyncScopeDescription(sScope14);
                DbSyncTableDescription tableDesc14 = SqlSyncDescriptionBuilder.GetDescriptionForTable("CompanyMaster", serverConn14);
                scopeDesc14.Tables.Add(tableDesc14);
                SqlSyncScopeProvisioning serverProvision14 = new SqlSyncScopeProvisioning(serverConn14, scopeDesc14);
                serverProvision14.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision14.Apply();
                #endregion

                //complainmaster
                #region
                SqlConnection serverConn15 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc15 = new DbSyncScopeDescription(sScope15);
                DbSyncTableDescription tableDesc15 = SqlSyncDescriptionBuilder.GetDescriptionForTable("complainmaster", serverConn15);
                scopeDesc15.Tables.Add(tableDesc15);
                SqlSyncScopeProvisioning serverProvision15 = new SqlSyncScopeProvisioning(serverConn15, scopeDesc15);
                serverProvision15.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision15.Apply();
                #endregion

                //complainstatus
                #region
                SqlConnection serverConn16 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc16 = new DbSyncScopeDescription(sScope16);
                DbSyncTableDescription tableDesc16 = SqlSyncDescriptionBuilder.GetDescriptionForTable("complainstatus", serverConn16);
                scopeDesc16.Tables.Add(tableDesc16);
                SqlSyncScopeProvisioning serverProvision16 = new SqlSyncScopeProvisioning(serverConn16, scopeDesc16);
                serverProvision16.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision16.Apply();
                #endregion

                //Country_Master
                #region
                SqlConnection serverConn17 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc17 = new DbSyncScopeDescription(sScope17);
                DbSyncTableDescription tableDesc17 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Country_Master", serverConn17);
                scopeDesc17.Tables.Add(tableDesc17);
                SqlSyncScopeProvisioning serverProvision17 = new SqlSyncScopeProvisioning(serverConn17, scopeDesc17);
                serverProvision17.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision17.Apply();
                #endregion

                //FormFormat
                #region
                SqlConnection serverConn18 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc18 = new DbSyncScopeDescription(sScope18);
                DbSyncTableDescription tableDesc18 = SqlSyncDescriptionBuilder.GetDescriptionForTable("FormFormat", serverConn18);
                scopeDesc18.Tables.Add(tableDesc18);
                SqlSyncScopeProvisioning serverProvision18 = new SqlSyncScopeProvisioning(serverConn18, scopeDesc18);
                serverProvision18.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision18.Apply();
                #endregion

                //fromcompany
                #region
                SqlConnection serverConn19 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc19 = new DbSyncScopeDescription(sScope19);
                DbSyncTableDescription tableDesc19 = SqlSyncDescriptionBuilder.GetDescriptionForTable("fromcompany", serverConn19);
                scopeDesc19.Tables.Add(tableDesc19);
                SqlSyncScopeProvisioning serverProvision19 = new SqlSyncScopeProvisioning(serverConn19, scopeDesc19);
                serverProvision19.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision19.Apply();
                #endregion

                //GroupMaster
                #region
                SqlConnection serverConn20 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc20 = new DbSyncScopeDescription(sScope20);
                DbSyncTableDescription tableDesc20 = SqlSyncDescriptionBuilder.GetDescriptionForTable("GroupMaster", serverConn20);
                scopeDesc20.Tables.Add(tableDesc20);
                SqlSyncScopeProvisioning serverProvision20 = new SqlSyncScopeProvisioning(serverConn20, scopeDesc20);
                serverProvision20.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision20.Apply();
                #endregion

                //InwardMstr
                #region
                SqlConnection serverConn21 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc21 = new DbSyncScopeDescription(sScope21);
                DbSyncTableDescription tableDesc21 = SqlSyncDescriptionBuilder.GetDescriptionForTable("InwardMstr", serverConn21);
                scopeDesc21.Tables.Add(tableDesc21);
                SqlSyncScopeProvisioning serverProvision21 = new SqlSyncScopeProvisioning(serverConn21, scopeDesc21);
                serverProvision21.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision21.Apply();
                #endregion

                //InwardProductMstr
                #region
                SqlConnection serverConn22 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc22 = new DbSyncScopeDescription(sScope22);
                DbSyncTableDescription tableDesc22 = SqlSyncDescriptionBuilder.GetDescriptionForTable("InwardProductMstr", serverConn22);
                scopeDesc22.Tables.Add(tableDesc22);
                SqlSyncScopeProvisioning serverProvision22 = new SqlSyncScopeProvisioning(serverConn22, scopeDesc22);
                serverProvision22.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision22.Apply();
                #endregion

                //ItemGroupMaster
                #region
                SqlConnection serverConn23 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc23 = new DbSyncScopeDescription(sScope23);
                DbSyncTableDescription tableDesc23 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ItemGroupMaster", serverConn23);
                scopeDesc23.Tables.Add(tableDesc23);
                SqlSyncScopeProvisioning serverProvision23 = new SqlSyncScopeProvisioning(serverConn23, scopeDesc23);
                serverProvision23.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision23.Apply();
                #endregion

                //ItemTaxMaster
                #region
                SqlConnection serverConn24 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc24 = new DbSyncScopeDescription(sScope24);
                DbSyncTableDescription tableDesc24 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ItemTaxMaster", serverConn24);
                scopeDesc24.Tables.Add(tableDesc24);
                SqlSyncScopeProvisioning serverProvision24 = new SqlSyncScopeProvisioning(serverConn24, scopeDesc24);
                serverProvision24.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision24.Apply();
                #endregion

                //Ledger
                #region
                SqlConnection serverConn25 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc25 = new DbSyncScopeDescription(sScope25);
                DbSyncTableDescription tableDesc25 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Ledger", serverConn25);
                scopeDesc25.Tables.Add(tableDesc25);
                SqlSyncScopeProvisioning serverProvision25 = new SqlSyncScopeProvisioning(serverConn25, scopeDesc25);
                serverProvision25.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision25.Apply();
                #endregion

                //MenuMaster
                #region
                SqlConnection serverConn26 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc26 = new DbSyncScopeDescription(sScope26);
                DbSyncTableDescription tableDesc26 = SqlSyncDescriptionBuilder.GetDescriptionForTable("MenuMaster", serverConn26);
                scopeDesc26.Tables.Add(tableDesc26);
                SqlSyncScopeProvisioning serverProvision26 = new SqlSyncScopeProvisioning(serverConn26, scopeDesc26);
                serverProvision26.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision26.Apply();
                #endregion

                //Options
                #region
                SqlConnection serverConn27 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc27 = new DbSyncScopeDescription(sScope27);
                DbSyncTableDescription tableDesc27 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Options", serverConn27);
                scopeDesc27.Tables.Add(tableDesc27);
                SqlSyncScopeProvisioning serverProvision27 = new SqlSyncScopeProvisioning(serverConn27, scopeDesc27);
                serverProvision27.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision27.Apply();
                #endregion

                //OwnerMaster
                #region
                SqlConnection serverConn28 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc28 = new DbSyncScopeDescription(sScope28);
                DbSyncTableDescription tableDesc28 = SqlSyncDescriptionBuilder.GetDescriptionForTable("OwnerMaster", serverConn28);
                scopeDesc28.Tables.Add(tableDesc28);
                SqlSyncScopeProvisioning serverProvision28 = new SqlSyncScopeProvisioning(serverConn28, scopeDesc28);
                serverProvision28.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision28.Apply();
                #endregion

                //PartyCompanyDiscount
                #region
                SqlConnection serverConn29 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc29 = new DbSyncScopeDescription(sScope29);
                DbSyncTableDescription tableDesc29 = SqlSyncDescriptionBuilder.GetDescriptionForTable("PartyCompanyDiscount", serverConn29);
                scopeDesc29.Tables.Add(tableDesc29);
                SqlSyncScopeProvisioning serverProvision29 = new SqlSyncScopeProvisioning(serverConn29, scopeDesc29);
                serverProvision29.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision29.Apply();
                #endregion

                //PartyGroupDiscount
                #region
                SqlConnection serverConn30 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc30 = new DbSyncScopeDescription(sScope30);
                DbSyncTableDescription tableDesc30 = SqlSyncDescriptionBuilder.GetDescriptionForTable("PartyGroupDiscount", serverConn30);
                scopeDesc30.Tables.Add(tableDesc30);
                SqlSyncScopeProvisioning serverProvision30 = new SqlSyncScopeProvisioning(serverConn30, scopeDesc30);
                serverProvision30.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision30.Apply();
                #endregion

                // PartyRates
                #region
                SqlConnection serverConn31 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc31 = new DbSyncScopeDescription(sScope31);
                DbSyncTableDescription tableDesc31 = SqlSyncDescriptionBuilder.GetDescriptionForTable("PartyRates", serverConn31);
                scopeDesc31.Tables.Add(tableDesc31);
                SqlSyncScopeProvisioning serverProvision31 = new SqlSyncScopeProvisioning(serverConn31, scopeDesc31);
                serverProvision31.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision31.Apply();
                #endregion

                //PaymentMaster
                #region
                SqlConnection serverConn32 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc32 = new DbSyncScopeDescription(sScope32);
                DbSyncTableDescription tableDesc32 = SqlSyncDescriptionBuilder.GetDescriptionForTable("PaymentMaster", serverConn32);
                scopeDesc32.Tables.Add(tableDesc32);
                SqlSyncScopeProvisioning serverProvision32 = new SqlSyncScopeProvisioning(serverConn32, scopeDesc32);
                serverProvision32.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision32.Apply();
                #endregion

                //paymentreceipt
                #region
                SqlConnection serverConn33 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc33 = new DbSyncScopeDescription(sScope33);
                DbSyncTableDescription tableDesc33 = SqlSyncDescriptionBuilder.GetDescriptionForTable("paymentreceipt", serverConn33);
                scopeDesc33.Tables.Add(tableDesc33);
                SqlSyncScopeProvisioning serverProvision33 = new SqlSyncScopeProvisioning(serverConn33, scopeDesc33);
                serverProvision33.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision33.Apply();
                #endregion

                //ProductMaster
                #region
                SqlConnection serverConn34 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc34 = new DbSyncScopeDescription(sScope34);
                DbSyncTableDescription tableDesc34 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ProductMaster", serverConn34);
                scopeDesc34.Tables.Add(tableDesc34);
                SqlSyncScopeProvisioning serverProvision34 = new SqlSyncScopeProvisioning(serverConn34, scopeDesc34);
                serverProvision34.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision34.Apply();
                #endregion

                //ProductPriceMaster
                #region
                SqlConnection serverConn35 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc35 = new DbSyncScopeDescription(sScope35);
                DbSyncTableDescription tableDesc35 = SqlSyncDescriptionBuilder.GetDescriptionForTable("ProductPriceMaster", serverConn35);
                scopeDesc35.Tables.Add(tableDesc35);
                SqlSyncScopeProvisioning serverProvision35 = new SqlSyncScopeProvisioning(serverConn35, scopeDesc35);
                serverProvision35.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision35.Apply();
                #endregion

                //PurchasetypeMaster
                #region
                SqlConnection serverConn36 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc36 = new DbSyncScopeDescription(sScope36);
                DbSyncTableDescription tableDesc36 = SqlSyncDescriptionBuilder.GetDescriptionForTable("PurchasetypeMaster", serverConn36);
                scopeDesc36.Tables.Add(tableDesc36);
                SqlSyncScopeProvisioning serverProvision36 = new SqlSyncScopeProvisioning(serverConn36, scopeDesc36);
                serverProvision36.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision36.Apply();
                #endregion

                //Ref
                #region
                SqlConnection serverConn37 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc37 = new DbSyncScopeDescription(sScope37);
                DbSyncTableDescription tableDesc37 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Ref", serverConn37);
                scopeDesc37.Tables.Add(tableDesc37);
                SqlSyncScopeProvisioning serverProvision37 = new SqlSyncScopeProvisioning(serverConn37, scopeDesc37);
                serverProvision37.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision37.Apply();
                #endregion

                //SaleOrderchargesmaster";
                #region
                SqlConnection serverConn38 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc38 = new DbSyncScopeDescription(sScope38);
                DbSyncTableDescription tableDesc38 = SqlSyncDescriptionBuilder.GetDescriptionForTable("SaleOrderchargesmaster", serverConn38);
                scopeDesc38.Tables.Add(tableDesc38);
                SqlSyncScopeProvisioning serverProvision38 = new SqlSyncScopeProvisioning(serverConn38, scopeDesc38);
                serverProvision38.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision38.Apply();
                #endregion

                //SaleOrderMaster
                #region
                SqlConnection serverConn39 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc39 = new DbSyncScopeDescription(sScope39);
                DbSyncTableDescription tableDesc39 = SqlSyncDescriptionBuilder.GetDescriptionForTable("SaleOrderMaster", serverConn39);
                scopeDesc39.Tables.Add(tableDesc39);
                SqlSyncScopeProvisioning serverProvision39 = new SqlSyncScopeProvisioning(serverConn39, scopeDesc39);
                serverProvision39.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision39.Apply();
                #endregion

                //SaleOrderProductMaster
                #region
                SqlConnection serverConn40 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc40 = new DbSyncScopeDescription(sScope40);
                DbSyncTableDescription tableDesc40 = SqlSyncDescriptionBuilder.GetDescriptionForTable("SaleOrderProductMaster", serverConn40);
                scopeDesc40.Tables.Add(tableDesc40);
                SqlSyncScopeProvisioning serverProvision40 = new SqlSyncScopeProvisioning(serverConn40, scopeDesc40);
                serverProvision40.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision40.Apply();
                #endregion

                //SaletypeMaster
                #region
                SqlConnection serverConn41 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc41 = new DbSyncScopeDescription(sScope41);
                DbSyncTableDescription tableDesc41 = SqlSyncDescriptionBuilder.GetDescriptionForTable("SaletypeMaster", serverConn41);
                scopeDesc41.Tables.Add(tableDesc41);
                SqlSyncScopeProvisioning serverProvision41 = new SqlSyncScopeProvisioning(serverConn41, scopeDesc41);
                serverProvision41.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision41.Apply();
                #endregion

                //Serials
                #region
                SqlConnection serverConn42 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc42 = new DbSyncScopeDescription(sScope42);
                DbSyncTableDescription tableDesc42 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Serials", serverConn42);
                scopeDesc42.Tables.Add(tableDesc42);
                SqlSyncScopeProvisioning serverProvision42 = new SqlSyncScopeProvisioning(serverConn42, scopeDesc42);
                serverProvision42.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision42.Apply();
                #endregion

                //State_Master
                #region
                SqlConnection serverConn43 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc43 = new DbSyncScopeDescription(sScope43);
                DbSyncTableDescription tableDesc43 = SqlSyncDescriptionBuilder.GetDescriptionForTable("State_Master", serverConn43);
                scopeDesc43.Tables.Add(tableDesc43);
                SqlSyncScopeProvisioning serverProvision43 = new SqlSyncScopeProvisioning(serverConn43, scopeDesc43);
                serverProvision43.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision43.Apply();
                #endregion

                //System_Master
                #region
                SqlConnection serverConn44 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc44 = new DbSyncScopeDescription(sScope44);
                DbSyncTableDescription tableDesc44 = SqlSyncDescriptionBuilder.GetDescriptionForTable("System_Master", serverConn44);
                scopeDesc44.Tables.Add(tableDesc44);
                SqlSyncScopeProvisioning serverProvision44 = new SqlSyncScopeProvisioning(serverConn44, scopeDesc44);
                serverProvision44.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision44.Apply();
                #endregion

                //TaxSlabMaster
                #region
                SqlConnection serverConn45 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc45 = new DbSyncScopeDescription(sScope45);
                DbSyncTableDescription tableDesc45 = SqlSyncDescriptionBuilder.GetDescriptionForTable("TaxSlabMaster", serverConn45);
                scopeDesc45.Tables.Add(tableDesc45);
                SqlSyncScopeProvisioning serverProvision45 = new SqlSyncScopeProvisioning(serverConn45, scopeDesc45);
                serverProvision45.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision45.Apply();
                #endregion

                //TaxType
                #region
                SqlConnection serverConn46 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc46 = new DbSyncScopeDescription(sScope46);
                DbSyncTableDescription tableDesc46 = SqlSyncDescriptionBuilder.GetDescriptionForTable("TaxType", serverConn46);
                scopeDesc46.Tables.Add(tableDesc46);
                SqlSyncScopeProvisioning serverProvision46 = new SqlSyncScopeProvisioning(serverConn46, scopeDesc46);
                serverProvision46.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision46.Apply();
                #endregion

                //tblcomplainmaster
                #region
                SqlConnection serverConn47 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc47 = new DbSyncScopeDescription(sScope47);
                DbSyncTableDescription tableDesc47 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblcomplainmaster", serverConn47);
                scopeDesc47.Tables.Add(tableDesc47);
                SqlSyncScopeProvisioning serverProvision47 = new SqlSyncScopeProvisioning(serverConn47, scopeDesc47);
                serverProvision47.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision47.Apply();
                #endregion

                //tblfinishedgoodsmaster
                #region
                SqlConnection serverConn48 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc48 = new DbSyncScopeDescription(sScope48);
                DbSyncTableDescription tableDesc48 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblfinishedgoodsmaster", serverConn48);
                scopeDesc48.Tables.Add(tableDesc48);
                SqlSyncScopeProvisioning serverProvision48 = new SqlSyncScopeProvisioning(serverConn48, scopeDesc48);
                serverProvision48.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision48.Apply();
                #endregion

                //tblfinishedgoodsqty
                #region
                SqlConnection serverConn49 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc49 = new DbSyncScopeDescription(sScope49);
                DbSyncTableDescription tableDesc49 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblfinishedgoodsqty", serverConn49);
                scopeDesc49.Tables.Add(tableDesc49);
                SqlSyncScopeProvisioning serverProvision49 = new SqlSyncScopeProvisioning(serverConn49, scopeDesc49);
                serverProvision49.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision49.Apply();
                #endregion

                //tblitemcomplainmaster
                #region
                SqlConnection serverConn50 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc50 = new DbSyncScopeDescription(sScope50);
                DbSyncTableDescription tableDesc50 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblitemcomplainmaster", serverConn50);
                scopeDesc50.Tables.Add(tableDesc50);
                SqlSyncScopeProvisioning serverProvision50 = new SqlSyncScopeProvisioning(serverConn50, scopeDesc50);
                serverProvision50.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision50.Apply();
                #endregion

                //tblitemreceivefromcompany
                #region
                SqlConnection serverConn51 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc51 = new DbSyncScopeDescription(sScope51);
                DbSyncTableDescription tableDesc51 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblitemreceivefromcompany", serverConn51);
                scopeDesc51.Tables.Add(tableDesc51);
                SqlSyncScopeProvisioning serverProvision51 = new SqlSyncScopeProvisioning(serverConn51, scopeDesc51);
                serverProvision51.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision51.Apply();
                #endregion

                //tblitemsendtocustomer
                #region
                SqlConnection serverConn52 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc52 = new DbSyncScopeDescription(sScope52);
                DbSyncTableDescription tableDesc52 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblitemsendtocustomer", serverConn52);
                scopeDesc52.Tables.Add(tableDesc52);
                SqlSyncScopeProvisioning serverProvision52 = new SqlSyncScopeProvisioning(serverConn52, scopeDesc52);
                serverProvision52.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision52.Apply();
                #endregion

                //tblprocessmaster
                #region
                SqlConnection serverConn53 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc53 = new DbSyncScopeDescription(sScope53);
                DbSyncTableDescription tableDesc53 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblprocessmaster", serverConn53);
                scopeDesc53.Tables.Add(tableDesc53);
                SqlSyncScopeProvisioning serverProvision53 = new SqlSyncScopeProvisioning(serverConn53, scopeDesc53);
                serverProvision53.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision53.Apply();
                #endregion

                //tblproductgeneratedmaster
                #region
                SqlConnection serverConn54 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc54 = new DbSyncScopeDescription(sScope54);
                DbSyncTableDescription tableDesc54 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblproductgeneratedmaster", serverConn54);
                scopeDesc54.Tables.Add(tableDesc54);
                SqlSyncScopeProvisioning serverProvision54 = new SqlSyncScopeProvisioning(serverConn54, scopeDesc54);
                serverProvision54.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision54.Apply();
                #endregion

                //tblproductionmaster
                #region
                SqlConnection serverConn55 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc55 = new DbSyncScopeDescription(sScope55);
                DbSyncTableDescription tableDesc55 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblproductionmaster", serverConn55);
                scopeDesc55.Tables.Add(tableDesc55);
                SqlSyncScopeProvisioning serverProvision55 = new SqlSyncScopeProvisioning(serverConn55, scopeDesc55);
                serverProvision55.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision55.Apply();
                #endregion

                //tblproductionrawmaterialmaster
                #region
                SqlConnection serverConn56 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc56 = new DbSyncScopeDescription(sScope56);
                DbSyncTableDescription tableDesc56 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblproductionrawmaterialmaster", serverConn56);
                scopeDesc56.Tables.Add(tableDesc56);
                SqlSyncScopeProvisioning serverProvision56 = new SqlSyncScopeProvisioning(serverConn56, scopeDesc56);
                serverProvision56.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision56.Apply();
                #endregion

                //tblreceivefromcompany
                #region
                SqlConnection serverConn57 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc57 = new DbSyncScopeDescription(sScope57);
                DbSyncTableDescription tableDesc57 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblreceivefromcompany", serverConn57);
                scopeDesc57.Tables.Add(tableDesc57);
                SqlSyncScopeProvisioning serverProvision57 = new SqlSyncScopeProvisioning(serverConn57, scopeDesc57);
                serverProvision57.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision57.Apply();
                #endregion

                //tblrowmaterialsmaster
                #region
                SqlConnection serverConn58 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc58 = new DbSyncScopeDescription(sScope58);
                DbSyncTableDescription tableDesc58 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblrowmaterialsmaster", serverConn58);
                scopeDesc58.Tables.Add(tableDesc58);
                SqlSyncScopeProvisioning serverProvision58 = new SqlSyncScopeProvisioning(serverConn58, scopeDesc58);
                serverProvision58.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision58.Apply();
                #endregion

                //tblsendtocompanyitemmaster
                #region
                SqlConnection serverConn59 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc59 = new DbSyncScopeDescription(sScope59);
                DbSyncTableDescription tableDesc59 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblsendtocompanyitemmaster", serverConn59);
                scopeDesc59.Tables.Add(tableDesc59);
                SqlSyncScopeProvisioning serverProvision59 = new SqlSyncScopeProvisioning(serverConn59, scopeDesc59);
                serverProvision59.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision59.Apply();
                #endregion

                //tblsendtocompanymaster
                #region
                SqlConnection serverConn60 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc60 = new DbSyncScopeDescription(sScope60);
                DbSyncTableDescription tableDesc60 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblsendtocompanymaster", serverConn60);
                scopeDesc60.Tables.Add(tableDesc60);
                SqlSyncScopeProvisioning serverProvision60 = new SqlSyncScopeProvisioning(serverConn60, scopeDesc60);
                serverProvision60.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision60.Apply();
                #endregion

                //tblsendtocustomer
                #region
                SqlConnection serverConn61 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc61 = new DbSyncScopeDescription(sScope61);
                DbSyncTableDescription tableDesc61 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tblsendtocustomer", serverConn61);
                scopeDesc61.Tables.Add(tableDesc61);
                SqlSyncScopeProvisioning serverProvision61 = new SqlSyncScopeProvisioning(serverConn61, scopeDesc61);
                serverProvision61.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision61.Apply();
                #endregion

                //tbluser_employeetype
                #region
                SqlConnection serverConn62 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc62 = new DbSyncScopeDescription(sScope62);
                DbSyncTableDescription tableDesc62 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tbluser_employeetype", serverConn62);
                scopeDesc62.Tables.Add(tableDesc62);
                SqlSyncScopeProvisioning serverProvision62 = new SqlSyncScopeProvisioning(serverConn62, scopeDesc62);
                serverProvision62.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision62.Apply();
                #endregion

                //tocompany
                #region
                SqlConnection serverConn63 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc63 = new DbSyncScopeDescription(sScope63);
                DbSyncTableDescription tableDesc63 = SqlSyncDescriptionBuilder.GetDescriptionForTable("tocompany", serverConn63);
                scopeDesc63.Tables.Add(tableDesc63);
                SqlSyncScopeProvisioning serverProvision63 = new SqlSyncScopeProvisioning(serverConn63, scopeDesc63);
                serverProvision63.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision63.Apply();
                #endregion

                //UnitMaster
                #region
                SqlConnection serverConn64 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc64 = new DbSyncScopeDescription(sScope64);
                DbSyncTableDescription tableDesc64 = SqlSyncDescriptionBuilder.GetDescriptionForTable("UnitMaster", serverConn64);
                scopeDesc64.Tables.Add(tableDesc64);
                SqlSyncScopeProvisioning serverProvision64 = new SqlSyncScopeProvisioning(serverConn64, scopeDesc64);
                serverProvision64.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision64.Apply();
                #endregion

                //updatedatabase
                #region
                SqlConnection serverConn65 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc65 = new DbSyncScopeDescription(sScope65);
                DbSyncTableDescription tableDesc65 = SqlSyncDescriptionBuilder.GetDescriptionForTable("updatedatabase", serverConn65);
                scopeDesc65.Tables.Add(tableDesc65);
                SqlSyncScopeProvisioning serverProvision65 = new SqlSyncScopeProvisioning(serverConn65, scopeDesc65);
                serverProvision65.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision65.Apply();
                #endregion

                //UserInfo
                #region
                SqlConnection serverConn66 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc66 = new DbSyncScopeDescription(sScope66);
                DbSyncTableDescription tableDesc66 = SqlSyncDescriptionBuilder.GetDescriptionForTable("UserInfo", serverConn66);
                scopeDesc66.Tables.Add(tableDesc66);
                SqlSyncScopeProvisioning serverProvision66 = new SqlSyncScopeProvisioning(serverConn66, scopeDesc66);
                serverProvision66.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision66.Apply();
                #endregion

                //UserRights
                #region
                SqlConnection serverConn67 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc67 = new DbSyncScopeDescription(sScope67);
                DbSyncTableDescription tableDesc67 = SqlSyncDescriptionBuilder.GetDescriptionForTable("UserRights", serverConn67);
                scopeDesc67.Tables.Add(tableDesc67);
                SqlSyncScopeProvisioning serverProvision67 = new SqlSyncScopeProvisioning(serverConn67, scopeDesc67);
                serverProvision67.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision67.Apply();
                #endregion

                //Voucher
                #region
                SqlConnection serverConn68 = new SqlConnection(sServerConnection);
                DbSyncScopeDescription scopeDesc68 = new DbSyncScopeDescription(sScope68);
                DbSyncTableDescription tableDesc68 = SqlSyncDescriptionBuilder.GetDescriptionForTable("Voucher", serverConn68);
                scopeDesc68.Tables.Add(tableDesc68);
                SqlSyncScopeProvisioning serverProvision68 = new SqlSyncScopeProvisioning(serverConn68, scopeDesc68);
                serverProvision68.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision68.Apply();
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("Server side" + ex.Message);
            }
        }
        public void ProvisionClient()
        {
            try
            {
                //AccountCustomerType
                #region
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope, serverConn);
                SqlSyncScopeProvisioning clientProvision = new SqlSyncScopeProvisioning(clientConn, scopeDesc);
                clientProvision.Apply();
                #endregion

                //AccountGroup
                #region
                SqlConnection serverConn1 = new SqlConnection(sServerConnection);
                SqlConnection clientConn1 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc1 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope1, serverConn1);
                SqlSyncScopeProvisioning clientProvision1 = new SqlSyncScopeProvisioning(clientConn1, scopeDesc1);
                clientProvision1.Apply();
                #endregion

                //Additional
                #region
                SqlConnection serverConn2 = new SqlConnection(sServerConnection);
                SqlConnection clientConn2 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc2 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope2, serverConn2);
                SqlSyncScopeProvisioning clientProvision2 = new SqlSyncScopeProvisioning(clientConn2, scopeDesc2);
                clientProvision2.Apply();
                #endregion
                //Billchargesmaster
                #region
                SqlConnection serverConn3 = new SqlConnection(sServerConnection);
                SqlConnection clientConn3 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc3 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope3, serverConn3);
                SqlSyncScopeProvisioning clientProvision3 = new SqlSyncScopeProvisioning(clientConn3, scopeDesc3);
                clientProvision3.Apply();
                #endregion
                //BillMaster
                #region
                SqlConnection serverConn4 = new SqlConnection(sServerConnection);
                SqlConnection clientConn4 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc4 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope4, serverConn4);
                SqlSyncScopeProvisioning clientProvision4 = new SqlSyncScopeProvisioning(clientConn4, scopeDesc4);
                clientProvision4.Apply();
                #endregion
                //BillPOSMaster
                #region
                SqlConnection serverConn5 = new SqlConnection(sServerConnection);
                SqlConnection clientConn5 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc5 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope5, serverConn5);
                SqlSyncScopeProvisioning clientProvision5 = new SqlSyncScopeProvisioning(clientConn5, scopeDesc5);
                clientProvision5.Apply();
                #endregion
                //BillPOSProductMaster
                #region
                SqlConnection serverConn6 = new SqlConnection(sServerConnection);
                SqlConnection clientConn6 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc6 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope6, serverConn6);
                SqlSyncScopeProvisioning clientProvision6 = new SqlSyncScopeProvisioning(clientConn6, scopeDesc6);
                clientProvision6.Apply();
                #endregion
                //BillProductMaster
                #region
                SqlConnection serverConn7 = new SqlConnection(sServerConnection);
                SqlConnection clientConn7 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc7 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope7, serverConn7);
                SqlSyncScopeProvisioning clientProvision7 = new SqlSyncScopeProvisioning(clientConn7, scopeDesc7);
                clientProvision7.Apply();
                #endregion
                //BillSundry
                #region
                SqlConnection serverConn8 = new SqlConnection(sServerConnection);
                SqlConnection clientConn8 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc8 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope8, serverConn8);
                SqlSyncScopeProvisioning clientProvision8 = new SqlSyncScopeProvisioning(clientConn8, scopeDesc8);
                clientProvision8.Apply();
                #endregion
                //ChargesHead
                #region
                SqlConnection serverConn9 = new SqlConnection(sServerConnection);
                SqlConnection clientConn9 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc9 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope9, serverConn9);
                SqlSyncScopeProvisioning clientProvision9 = new SqlSyncScopeProvisioning(clientConn9, scopeDesc9);
                clientProvision9.Apply();
                #endregion
                //ChargesHeadApplyOn
                #region
                SqlConnection serverConn10 = new SqlConnection(sServerConnection);
                SqlConnection clientConn10 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc10 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope10, serverConn10);
                SqlSyncScopeProvisioning clientProvision10 = new SqlSyncScopeProvisioning(clientConn10, scopeDesc10);
                clientProvision10.Apply();
                #endregion
                //ClientMaster
                #region
                SqlConnection serverConn11 = new SqlConnection(sServerConnection);
                SqlConnection clientConn11 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc11 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope11, serverConn11);
                SqlSyncScopeProvisioning clientProvision11 = new SqlSyncScopeProvisioning(clientConn11, scopeDesc11);
                clientProvision11.Apply();
                #endregion
                //ClientProductMargin
                #region
                SqlConnection serverConn12 = new SqlConnection(sServerConnection);
                SqlConnection clientConn12 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc12 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope12, serverConn12);
                SqlSyncScopeProvisioning clientProvision12 = new SqlSyncScopeProvisioning(clientConn12, scopeDesc12);
                clientProvision12.Apply();
                #endregion
                //Company
                #region
                SqlConnection serverConn13 = new SqlConnection(sServerConnection);
                SqlConnection clientConn13 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc13 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope13, serverConn13);
                SqlSyncScopeProvisioning clientProvision13 = new SqlSyncScopeProvisioning(clientConn13, scopeDesc13);
                clientProvision13.Apply();
                #endregion
                //CompanyMaster
                #region
                SqlConnection serverConn14 = new SqlConnection(sServerConnection);
                SqlConnection clientConn14 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc14 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope14, serverConn14);
                SqlSyncScopeProvisioning clientProvision14 = new SqlSyncScopeProvisioning(clientConn14, scopeDesc14);
                clientProvision14.Apply();
                #endregion
                //complainmaster
                #region
                SqlConnection serverConn15 = new SqlConnection(sServerConnection);
                SqlConnection clientConn15 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc15 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope15, serverConn15);
                SqlSyncScopeProvisioning clientProvision15 = new SqlSyncScopeProvisioning(clientConn15, scopeDesc15);
                clientProvision15.Apply();
                #endregion
                //complainstatus
                #region
                SqlConnection serverConn16 = new SqlConnection(sServerConnection);
                SqlConnection clientConn16 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc16 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope16, serverConn16);
                SqlSyncScopeProvisioning clientProvision16 = new SqlSyncScopeProvisioning(clientConn16, scopeDesc16);
                clientProvision16.Apply();
                #endregion
                //Country_Master
                #region
                SqlConnection serverConn17 = new SqlConnection(sServerConnection);
                SqlConnection clientConn17 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc17 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope17, serverConn17);
                SqlSyncScopeProvisioning clientProvision17 = new SqlSyncScopeProvisioning(clientConn17, scopeDesc17);
                clientProvision17.Apply();
                #endregion
                //FormFormat
                #region
                SqlConnection serverConn18 = new SqlConnection(sServerConnection);
                SqlConnection clientConn18 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc18 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope18, serverConn18);
                SqlSyncScopeProvisioning clientProvision18 = new SqlSyncScopeProvisioning(clientConn18, scopeDesc18);
                clientProvision18.Apply();
                #endregion
                //fromcompany
                #region
                SqlConnection serverConn19 = new SqlConnection(sServerConnection);
                SqlConnection clientConn19 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc19 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope19, serverConn19);
                SqlSyncScopeProvisioning clientProvision19 = new SqlSyncScopeProvisioning(clientConn19, scopeDesc19);
                clientProvision19.Apply();
                #endregion
                //GroupMaster
                #region
                SqlConnection serverConn20 = new SqlConnection(sServerConnection);
                SqlConnection clientConn20 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc20 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope20, serverConn20);
                SqlSyncScopeProvisioning clientProvision20 = new SqlSyncScopeProvisioning(clientConn20, scopeDesc20);
                clientProvision20.Apply();
                #endregion
                //InwardMstr
                #region
                SqlConnection serverConn21 = new SqlConnection(sServerConnection);
                SqlConnection clientConn21 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc21 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope21, serverConn21);
                SqlSyncScopeProvisioning clientProvision21 = new SqlSyncScopeProvisioning(clientConn21, scopeDesc21);
                clientProvision21.Apply();
                #endregion
                //InwardProductMstr
                #region
                SqlConnection serverConn22 = new SqlConnection(sServerConnection);
                SqlConnection clientConn22 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc22 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope22, serverConn22);
                SqlSyncScopeProvisioning clientProvision22 = new SqlSyncScopeProvisioning(clientConn22, scopeDesc22);
                clientProvision22.Apply();
                #endregion
                //ItemGroupMaster
                #region
                SqlConnection serverConn23 = new SqlConnection(sServerConnection);
                SqlConnection clientConn23 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc23 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope23, serverConn23);
                SqlSyncScopeProvisioning clientProvision23 = new SqlSyncScopeProvisioning(clientConn23, scopeDesc23);
                clientProvision23.Apply();
                #endregion
                //ItemTaxMaster
                #region
                SqlConnection serverConn24 = new SqlConnection(sServerConnection);
                SqlConnection clientConn24 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc24 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope24, serverConn24);
                SqlSyncScopeProvisioning clientProvision24 = new SqlSyncScopeProvisioning(clientConn24, scopeDesc24);
                clientProvision24.Apply();
                #endregion
                //Ledger
                #region
                SqlConnection serverConn25 = new SqlConnection(sServerConnection);
                SqlConnection clientConn25 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc25 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope25, serverConn25);
                SqlSyncScopeProvisioning clientProvision25 = new SqlSyncScopeProvisioning(clientConn25, scopeDesc25);
                clientProvision25.Apply();
                #endregion
                //MenuMaster
                #region
                SqlConnection serverConn26 = new SqlConnection(sServerConnection);
                SqlConnection clientConn26 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc26 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope26, serverConn26);
                SqlSyncScopeProvisioning clientProvision26 = new SqlSyncScopeProvisioning(clientConn26, scopeDesc26);
                clientProvision26.Apply();
                #endregion
                //Options
                #region
                SqlConnection serverConn27 = new SqlConnection(sServerConnection);
                SqlConnection clientConn27 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc27 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope27, serverConn27);
                SqlSyncScopeProvisioning clientProvision27 = new SqlSyncScopeProvisioning(clientConn27, scopeDesc27);
                clientProvision27.Apply();
                #endregion
                //OwnerMaster
                #region
                SqlConnection serverConn28 = new SqlConnection(sServerConnection);
                SqlConnection clientConn28 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc28 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope28, serverConn28);
                SqlSyncScopeProvisioning clientProvision28 = new SqlSyncScopeProvisioning(clientConn28, scopeDesc28);
                clientProvision28.Apply();
                #endregion
                //PartyCompanyDiscount
                #region
                SqlConnection serverConn29 = new SqlConnection(sServerConnection);
                SqlConnection clientConn29 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc29 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope29, serverConn29);
                SqlSyncScopeProvisioning clientProvision29 = new SqlSyncScopeProvisioning(clientConn29, scopeDesc29);
                clientProvision29.Apply();
                #endregion
                //PartyGroupDiscount
                #region
                SqlConnection serverConn30 = new SqlConnection(sServerConnection);
                SqlConnection clientConn30 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc30 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope30, serverConn30);
                SqlSyncScopeProvisioning clientProvision30 = new SqlSyncScopeProvisioning(clientConn30, scopeDesc30);
                clientProvision30.Apply();
                #endregion
                //PartyRates
                #region
                SqlConnection serverConn31 = new SqlConnection(sServerConnection);
                SqlConnection clientConn31 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc31 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope31, serverConn31);
                SqlSyncScopeProvisioning clientProvision31 = new SqlSyncScopeProvisioning(clientConn31, scopeDesc31);
                clientProvision31.Apply();
                #endregion
                //PaymentMaster
                #region
                SqlConnection serverConn32 = new SqlConnection(sServerConnection);
                SqlConnection clientConn32 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc32 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope32, serverConn32);
                SqlSyncScopeProvisioning clientProvision32 = new SqlSyncScopeProvisioning(clientConn32, scopeDesc32);
                clientProvision32.Apply();
                #endregion
                //paymentreceipt
                #region
                SqlConnection serverConn33 = new SqlConnection(sServerConnection);
                SqlConnection clientConn33 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc33 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope33, serverConn33);
                SqlSyncScopeProvisioning clientProvision33 = new SqlSyncScopeProvisioning(clientConn33, scopeDesc33);
                clientProvision33.Apply();
                #endregion
                //ProductMaster
                #region
                SqlConnection serverConn34 = new SqlConnection(sServerConnection);
                SqlConnection clientConn34 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc34 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope34, serverConn34);
                SqlSyncScopeProvisioning clientProvision34 = new SqlSyncScopeProvisioning(clientConn34, scopeDesc34);
                clientProvision34.Apply();
                #endregion
                //ProductPriceMaster
                #region
                SqlConnection serverConn35 = new SqlConnection(sServerConnection);
                SqlConnection clientConn35 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc35 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope35, serverConn35);
                SqlSyncScopeProvisioning clientProvision35 = new SqlSyncScopeProvisioning(clientConn35, scopeDesc35);
                clientProvision35.Apply();
                #endregion
                //PurchasetypeMaster
                #region
                SqlConnection serverConn36 = new SqlConnection(sServerConnection);
                SqlConnection clientConn36 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc36 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope36, serverConn36);
                SqlSyncScopeProvisioning clientProvision36 = new SqlSyncScopeProvisioning(clientConn36, scopeDesc36);
                clientProvision36.Apply();
                #endregion
                //Ref
                #region
                SqlConnection serverConn37 = new SqlConnection(sServerConnection);
                SqlConnection clientConn37 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc37 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope37, serverConn37);
                SqlSyncScopeProvisioning clientProvision37 = new SqlSyncScopeProvisioning(clientConn37, scopeDesc37);
                clientProvision37.Apply();
                #endregion
                //SaleOrderchargesmaster
                #region
                SqlConnection serverConn38 = new SqlConnection(sServerConnection);
                SqlConnection clientConn38 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc38 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope38, serverConn38);
                SqlSyncScopeProvisioning clientProvision38 = new SqlSyncScopeProvisioning(clientConn38, scopeDesc38);
                clientProvision38.Apply();
                #endregion
                //SaleOrderMaster
                #region
                SqlConnection serverConn39 = new SqlConnection(sServerConnection);
                SqlConnection clientConn39 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc39 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope39, serverConn39);
                SqlSyncScopeProvisioning clientProvision39 = new SqlSyncScopeProvisioning(clientConn39, scopeDesc39);
                clientProvision39.Apply();
                #endregion
                //SaleOrderProductMaster
                #region
                SqlConnection serverConn40 = new SqlConnection(sServerConnection);
                SqlConnection clientConn40 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc40 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope40, serverConn40);
                SqlSyncScopeProvisioning clientProvision40 = new SqlSyncScopeProvisioning(clientConn40, scopeDesc40);
                clientProvision40.Apply();
                #endregion
                //SaletypeMaster
                #region
                SqlConnection serverConn41 = new SqlConnection(sServerConnection);
                SqlConnection clientConn41 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc41 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope41, serverConn41);
                SqlSyncScopeProvisioning clientProvision41 = new SqlSyncScopeProvisioning(clientConn41, scopeDesc41);
                clientProvision41.Apply();
                #endregion
                //Serials
                #region
                SqlConnection serverConn42 = new SqlConnection(sServerConnection);
                SqlConnection clientConn42 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc42 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope42, serverConn42);
                SqlSyncScopeProvisioning clientProvision42 = new SqlSyncScopeProvisioning(clientConn42, scopeDesc42);
                clientProvision42.Apply();
                #endregion
                //State_Master
                #region
                SqlConnection serverConn43 = new SqlConnection(sServerConnection);
                SqlConnection clientConn43 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc43 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope43, serverConn43);
                SqlSyncScopeProvisioning clientProvision43 = new SqlSyncScopeProvisioning(clientConn43, scopeDesc43);
                clientProvision43.Apply();
                #endregion
                //System_Master
                #region
                SqlConnection serverConn44 = new SqlConnection(sServerConnection);
                SqlConnection clientConn44 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc44 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope44, serverConn44);
                SqlSyncScopeProvisioning clientProvision44 = new SqlSyncScopeProvisioning(clientConn44, scopeDesc44);
                clientProvision44.Apply();
                #endregion
                //TaxSlabMaster
                #region
                SqlConnection serverConn45 = new SqlConnection(sServerConnection);
                SqlConnection clientConn45 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc45 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope45, serverConn45);
                SqlSyncScopeProvisioning clientProvision45 = new SqlSyncScopeProvisioning(clientConn45, scopeDesc45);
                clientProvision45.Apply();
                #endregion
                //TaxType
                #region
                SqlConnection serverConn46 = new SqlConnection(sServerConnection);
                SqlConnection clientConn46 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc46 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope46, serverConn46);
                SqlSyncScopeProvisioning clientProvision46 = new SqlSyncScopeProvisioning(clientConn46, scopeDesc46);
                clientProvision46.Apply();
                #endregion
                //tblcomplainmaster
                #region
                SqlConnection serverConn47 = new SqlConnection(sServerConnection);
                SqlConnection clientConn47 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc47 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope47, serverConn47);
                SqlSyncScopeProvisioning clientProvision47 = new SqlSyncScopeProvisioning(clientConn47, scopeDesc47);
                clientProvision47.Apply();
                #endregion
                //tblfinishedgoodsmaster
                #region
                SqlConnection serverConn48 = new SqlConnection(sServerConnection);
                SqlConnection clientConn48 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc48 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope48, serverConn48);
                SqlSyncScopeProvisioning clientProvision48 = new SqlSyncScopeProvisioning(clientConn48, scopeDesc48);
                clientProvision48.Apply();
                #endregion
                //tblfinishedgoodsqty
                #region
                SqlConnection serverConn49 = new SqlConnection(sServerConnection);
                SqlConnection clientConn49 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc49 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope49, serverConn49);
                SqlSyncScopeProvisioning clientProvision49 = new SqlSyncScopeProvisioning(clientConn49, scopeDesc49);
                clientProvision49.Apply();
                #endregion
                //tblitemcomplainmaster
                #region
                SqlConnection serverConn50 = new SqlConnection(sServerConnection);
                SqlConnection clientConn50 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc50 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope50, serverConn50);
                SqlSyncScopeProvisioning clientProvision50 = new SqlSyncScopeProvisioning(clientConn50, scopeDesc50);
                clientProvision50.Apply();
                #endregion
                //tblitemreceivefromcompany
                #region
                SqlConnection serverConn51 = new SqlConnection(sServerConnection);
                SqlConnection clientConn51 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc51 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope51, serverConn51);
                SqlSyncScopeProvisioning clientProvision51 = new SqlSyncScopeProvisioning(clientConn51, scopeDesc51);
                clientProvision51.Apply();
                #endregion
                //tblitemsendtocustomer
                #region
                SqlConnection serverConn52 = new SqlConnection(sServerConnection);
                SqlConnection clientConn52 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc52 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope52, serverConn52);
                SqlSyncScopeProvisioning clientProvision52 = new SqlSyncScopeProvisioning(clientConn52, scopeDesc52);
                clientProvision52.Apply();
                #endregion
                //tblprocessmaster
                #region
                SqlConnection serverConn53 = new SqlConnection(sServerConnection);
                SqlConnection clientConn53 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc53 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope53, serverConn53);
                SqlSyncScopeProvisioning clientProvision53 = new SqlSyncScopeProvisioning(clientConn53, scopeDesc53);
                clientProvision53.Apply();
                #endregion
                //tblproductgeneratedmaster
                #region
                SqlConnection serverConn54 = new SqlConnection(sServerConnection);
                SqlConnection clientConn54 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc54 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope54, serverConn54);
                SqlSyncScopeProvisioning clientProvision54 = new SqlSyncScopeProvisioning(clientConn54, scopeDesc54);
                clientProvision54.Apply();
                #endregion
                //tblproductionmaster
                #region
                SqlConnection serverConn55 = new SqlConnection(sServerConnection);
                SqlConnection clientConn55 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc55 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope55, serverConn55);
                SqlSyncScopeProvisioning clientProvision55 = new SqlSyncScopeProvisioning(clientConn55, scopeDesc55);
                clientProvision55.Apply();
                #endregion
                //tblproductionrawmaterialmaster
                #region
                SqlConnection serverConn56 = new SqlConnection(sServerConnection);
                SqlConnection clientConn56 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc56 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope56, serverConn56);
                SqlSyncScopeProvisioning clientProvision56 = new SqlSyncScopeProvisioning(clientConn56, scopeDesc56);
                clientProvision56.Apply();
                #endregion
                //tblreceivefromcompany
                #region
                SqlConnection serverConn57 = new SqlConnection(sServerConnection);
                SqlConnection clientConn57 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc57 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope57, serverConn57);
                SqlSyncScopeProvisioning clientProvision57 = new SqlSyncScopeProvisioning(clientConn57, scopeDesc57);
                clientProvision57.Apply();
                #endregion
                //tblrowmaterialsmaster
                #region
                SqlConnection serverConn58 = new SqlConnection(sServerConnection);
                SqlConnection clientConn58 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc58 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope58, serverConn58);
                SqlSyncScopeProvisioning clientProvision58 = new SqlSyncScopeProvisioning(clientConn58, scopeDesc58);
                clientProvision58.Apply();
                #endregion
                //tblsendtocompanyitemmaster
                #region
                SqlConnection serverConn59 = new SqlConnection(sServerConnection);
                SqlConnection clientConn59 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc59 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope59, serverConn59);
                SqlSyncScopeProvisioning clientProvision59 = new SqlSyncScopeProvisioning(clientConn59, scopeDesc59);
                clientProvision59.Apply();
                #endregion
                //tblsendtocompanymaster
                #region
                SqlConnection serverConn60 = new SqlConnection(sServerConnection);
                SqlConnection clientConn60 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc60 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope60, serverConn60);
                SqlSyncScopeProvisioning clientProvision60 = new SqlSyncScopeProvisioning(clientConn60, scopeDesc60);
                clientProvision60.Apply();
                #endregion
                //tblsendtocustomer
                #region
                SqlConnection serverConn61 = new SqlConnection(sServerConnection);
                SqlConnection clientConn61 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc61 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope61, serverConn61);
                SqlSyncScopeProvisioning clientProvision61 = new SqlSyncScopeProvisioning(clientConn61, scopeDesc61);
                clientProvision61.Apply();
                #endregion
                //tbluser_employeetype
                #region
                SqlConnection serverConn62 = new SqlConnection(sServerConnection);
                SqlConnection clientConn62 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc62 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope62, serverConn62);
                SqlSyncScopeProvisioning clientProvision62 = new SqlSyncScopeProvisioning(clientConn62, scopeDesc62);
                clientProvision62.Apply();
                #endregion
                //tocompany
                #region
                SqlConnection serverConn63 = new SqlConnection(sServerConnection);
                SqlConnection clientConn63 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc63 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope63, serverConn63);
                SqlSyncScopeProvisioning clientProvision63 = new SqlSyncScopeProvisioning(clientConn63, scopeDesc63);
                clientProvision63.Apply();
                #endregion
                //UnitMaster
                #region
                SqlConnection serverConn64 = new SqlConnection(sServerConnection);
                SqlConnection clientConn64 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc64 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope64, serverConn64);
                SqlSyncScopeProvisioning clientProvision64 = new SqlSyncScopeProvisioning(clientConn64, scopeDesc64);
                clientProvision64.Apply();
                #endregion
                //updatedatabase
                #region
                SqlConnection serverConn65 = new SqlConnection(sServerConnection);
                SqlConnection clientConn65 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc65 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope65, serverConn65);
                SqlSyncScopeProvisioning clientProvision65 = new SqlSyncScopeProvisioning(clientConn65, scopeDesc65);
                clientProvision65.Apply();
                #endregion
                //UserInfo
                #region
                SqlConnection serverConn66 = new SqlConnection(sServerConnection);
                SqlConnection clientConn66 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc66 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope66, serverConn66);
                SqlSyncScopeProvisioning clientProvision66 = new SqlSyncScopeProvisioning(clientConn66, scopeDesc66);
                clientProvision66.Apply();
                #endregion
                //UserRights
                #region
                SqlConnection serverConn67 = new SqlConnection(sServerConnection);
                SqlConnection clientConn67 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc67 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope67, serverConn67);
                SqlSyncScopeProvisioning clientProvision67 = new SqlSyncScopeProvisioning(clientConn67, scopeDesc67);
                clientProvision67.Apply();
                #endregion
                //Voucher
                #region
                SqlConnection serverConn68 = new SqlConnection(sServerConnection);
                SqlConnection clientConn68 = new SqlConnection(sClientConnection);
                DbSyncScopeDescription scopeDesc68 = SqlSyncDescriptionBuilder.GetDescriptionForScope(sScope68, serverConn68);
                SqlSyncScopeProvisioning clientProvision68 = new SqlSyncScopeProvisioning(clientConn68, scopeDesc68);
                clientProvision68.Apply();
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("Client Side" + ex.Message);
            }
        }
        public string Sync()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
                syncOrchestrator.LocalProvider = new SqlSyncProvider(sScope, clientConn);
                syncOrchestrator.RemoteProvider = new SqlSyncProvider(sScope, serverConn);
                syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
                string str = "Start Time: " + syncStats.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;

            }
            catch
            {
                return "";
            }
        }
        public string Sync1()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope1, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope1, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync2()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope2, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope2, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync3()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope3, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope3, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync4()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope4, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope4, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync5()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope5, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope5, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync6()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope6, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope6, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync7()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope7, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope7, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync8()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope8, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope8, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync9()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope9, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope9, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync10()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
                syncOrchestrator.LocalProvider = new SqlSyncProvider(sScope10, clientConn);
                syncOrchestrator.RemoteProvider = new SqlSyncProvider(sScope10, serverConn);
                syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
                string str = "Start Time: " + syncStats.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;

            }
            catch
            {
                return "";
            }
        }
        public string Sync11()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope11, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope11, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync12()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope12, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope12, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync13()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope13, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope13, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync14()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope14, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope14, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync15()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope15, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope15, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync16()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope16, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope16, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync17()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope17, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope17, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync18()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope18, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope18, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync19()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope19, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope19, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync20()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
                syncOrchestrator.LocalProvider = new SqlSyncProvider(sScope20, clientConn);
                syncOrchestrator.RemoteProvider = new SqlSyncProvider(sScope20, serverConn);
                syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
                string str = "Start Time: " + syncStats.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;

            }
            catch
            {
                return "";
            }
        }
        public string Sync21()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope21, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope21, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync22()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope22, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope22, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync23()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope23, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope23, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync24()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope24, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope24, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync25()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope25, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope25, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync26()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope26, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope26, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync27()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope27, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope27, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync28()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope28, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope28, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync29()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope29, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope29, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync30()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
                syncOrchestrator.LocalProvider = new SqlSyncProvider(sScope30, clientConn);
                syncOrchestrator.RemoteProvider = new SqlSyncProvider(sScope30, serverConn);
                syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
                string str = "Start Time: " + syncStats.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;

            }
            catch
            {
                return "";
            }
        }
        public string Sync31()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope31, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope31, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync32()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope32, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope32, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync33()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope33, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope33, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync34()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope34, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope34, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync35()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope35, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope35, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync36()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope36, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope36, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync37()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope37, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope37, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync38()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope38, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope38, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync39()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope39, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope39, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync40()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
                syncOrchestrator.LocalProvider = new SqlSyncProvider(sScope40, clientConn);
                syncOrchestrator.RemoteProvider = new SqlSyncProvider(sScope40, serverConn);
                syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
                string str = "Start Time: " + syncStats.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;

            }
            catch
            {
                return "";
            }
        }
        public string Sync41()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope41, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope41, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync42()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope42, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope42, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync43()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope43, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope43, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync44()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope44, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope44, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync45()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope45, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope45, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync46()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope46, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope46, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync47()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope47, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope47, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync48()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope48, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope48, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync49()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope49, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope49, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync50()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
                syncOrchestrator.LocalProvider = new SqlSyncProvider(sScope50, clientConn);
                syncOrchestrator.RemoteProvider = new SqlSyncProvider(sScope50, serverConn);
                syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
                string str = "Start Time: " + syncStats.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;

            }
            catch
            {
                return "";
            }
        }
        public string Sync51()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope51, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope51, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync52()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope52, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope52, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync53()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope53, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope53, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync54()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope54, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope54, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync55()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope55, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope55, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync56()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope56, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope56, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync57()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope57, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope57, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync58()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope58, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope58, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync59()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope59, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope59, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync60()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
                syncOrchestrator.LocalProvider = new SqlSyncProvider(sScope60, clientConn);
                syncOrchestrator.RemoteProvider = new SqlSyncProvider(sScope60, serverConn);
                syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
                string str = "Start Time: " + syncStats.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;

            }
            catch
            {
                return "";
            }
        }
        public string Sync61()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope61, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope61, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync62()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope62, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope62, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync63()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope63, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope63, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync64()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope64, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope64, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync65()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope65, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope65, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync66()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope66, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope66, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync67()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope67, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope67, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }
        public string Sync68()
        {
            try
            {
                SqlConnection serverConn = new SqlConnection(sServerConnection);
                SqlConnection clientConn = new SqlConnection(sClientConnection);
                SyncOrchestrator syncOrchestrator1 = new SyncOrchestrator();
                syncOrchestrator1.LocalProvider = new SqlSyncProvider(sScope68, clientConn);
                syncOrchestrator1.RemoteProvider = new SqlSyncProvider(sScope68, serverConn);
                syncOrchestrator1.Direction = SyncDirectionOrder.UploadAndDownload;
                ((SqlSyncProvider)syncOrchestrator1.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                SyncOperationStatistics syncStats1 = syncOrchestrator1.Synchronize();
                string str = "Start Time: " + syncStats1.SyncStartTime + Environment.NewLine;
                str += "Total Changes Uploaded: " + syncStats1.UploadChangesTotal + Environment.NewLine;
                str += "Total Changes Downloaded: " + syncStats1.DownloadChangesTotal + Environment.NewLine;
                str += "Complete Time: " + syncStats1.SyncEndTime + Environment.NewLine;
                str += String.Empty + Environment.NewLine;
                return str;
            }
            catch
            {
                return "";
            }
        }

        static void Program_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            Console.WriteLine(e.Conflict.Type);
            Console.WriteLine(e.Error);

        }
        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
