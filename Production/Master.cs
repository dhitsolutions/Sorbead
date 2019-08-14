using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;
using System.Threading;
using Microsoft.Win32;
using System.Diagnostics;

namespace Production
{
    public partial class Master : Form
    {
        private UserLogin userLogin;
        public static string companyId = string.Empty;
        public static string Taxactation = string.Empty;
        public static string dateformate = string.Empty;
        public static string statusreg = string.Empty;
        DataTable dt10 = new DataTable();
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        DataTable dtreg = new DataTable();
        DataSet ds = new DataSet();
        public static string Decryptexpdate1;
        public static string Decryptinstoaldate1;
        ServerConnection con = new ServerConnection();
        datetime date = new datetime();
        public Master()
        {

            InitializeComponent();
            //Console.Write(DateTime.Now + "");
            //RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
            //rkey.SetValue("sShortDate", "yyyy-MM-dd");
            CultureInfo en = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = en;
            lblcompanyid.Text = "Company Id: " + companyId;
            ds = ods.getdata("Select * from tblreg");
            dtreg = ds.Tables[0];
           
            #region
            if (dtreg.Rows.Count > 0)
            {
               
               
               // Decryptinstoaldata(dtreg.Rows[0]["d14"]).ToString(dateformate));
                string t = dtreg.Rows[0]["d14"].ToString();
                Decryptexpdate(t);
                string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
              //  DateTime dtdecr = Convert.ToDateTime(Decryptexpdate1);
                DateTime dtdecr = DateTime.ParseExact(Decryptexpdate1, "dd-MM-yyyy", en);
                //DateTime dtcurrentdate = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"));
                //DateTime dtexpdate = Convert.ToDateTime(dtdecr.ToString("MM-dd-yyyy"));
                DateTime dtcurrentdate = Convert.ToDateTime(DateTime.Now.ToString());
                DateTime dtexpdate = dtdecr;
                if (dtexpdate <= dtcurrentdate)
                {
                    enablemenu(false);
                    AMC ci = new AMC(this, tabControl);
                    AddNewTab(ci);
                }
                else
                {
                    string reg = dtreg.Rows[0]["d16"].ToString();
                    Decrypstatus(reg);
                    if (statusreg == "Reg")
                    {
                        registerNowToolStripMenuItem.Visible = false;
                        CompanyList ci = new CompanyList(this, tabControl);
                        AddNewTab(ci);
                    }
                    else
                    {
                        utilityandreportsdisable();
                        string t1 = dtreg.Rows[0]["d18"].ToString();
                        Decryptinstoaldata(t1);
                        DateTime dtdecrtotal = DateTime.ParseExact(Decryptinstoaldate1, "dd-MM-yyyy", en);
                        string d1 = DateTime.Now.ToString("dd-MM-yyyy");
                        //DateTime dtdecrtotal = Convert.ToDateTime(Decryptinstoaldate1);
                       // string d1 = DateTime.Now.ToString("MM-dd-yyyy");
                        DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString());
                        DateTime dtexpdate1 = dtdecrtotal;
                        //DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"));
                        //DateTime dtexpdate1 = Convert.ToDateTime(dtdecrtotal.ToString("dd/MM/yyyy"));
                        //dtexpdate1 = dtexpdate1.AddDays(7);
                        if (dtexpdate1 <= dtcurrentdate1)
                        {
                            enablemenu(false);
                            RegisterNow ci = new RegisterNow(this, tabControl);
                            AddNewTab(ci);
                        }
                        else
                        {
                            CompanyList ci = new CompanyList(this, tabControl);
                            AddNewTab(ci);
                        }

                    }
                }
            }
            else
            {
                CompanyList ci = new CompanyList(this, tabControl);
                AddNewTab(ci);
            }
            #endregion
          



        }
        public void utilityandreportsdisable()
        {
            partywisediscount.Enabled = false;
            itemMasterPriceChangeToolStripMenuItem.Enabled = false;
            XReportToolStripMenuItem.Enabled = false;
          //  ZReportToolStripMenuItem.Enabled = false;
            ReprintLastReceiptToolStripMenuItem.Enabled = false;
         //   billReprintToolStripMenuItem.Enabled = false;
           // dateWiseBillInvoiceToolStripMenuItem.Enabled = false;
            //inwardDetailsToolStripMenuItem.Enabled = false;
            billToolStripMenuItem.Enabled = false;
            ledgerToolStripMenuItem.Enabled = false;
            pOSBillListToolStripMenuItem1.Enabled = false;
            pOSItemListToolStripMenuItem1.Enabled = false;
            cashBankBookToolStripMenuItem.Enabled = false;
            listOfPOSToolStripMenuItem1.Enabled = false;
            gSTR1ToolStripMenuItem1.Enabled = false;
            gSTR2ToolStripMenuItem1.Enabled = false;
            gSTR3ToolStripMenuItem1.Enabled = false;
            serialNoTrackingToolStripMenuItem.Enabled = false;
            pOSRegisterToolStripMenuItem1.Enabled = false;
            outstandinganAlysisToolStripMenuItem.Enabled = false;
            printBarcodeLabelsToolStripMenuItem.Enabled = false;
            trialBalanceToolStripMenuItem.Enabled = false;
            bankEntryRegisterToolStripMenuItem.Enabled = false;
            debitNoteToolStripMenuItem1.Enabled = false;
            creditNoteToolStripMenuItem1.Enabled = false;
            dayBookToolStripMenuItem.Enabled = false;
            agentCommissionToolStripMenuItem.Enabled = false;

        }
        public Master(UserLogin userLogin)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.userLogin = userLogin;
            // enablemenu(true);
        }
        public void enablemenu(bool a)
        {
            try
            {

                // InitializeComponent();
                //  this.userLogin = userLogin;
                registerNowToolStripMenuItem.Enabled = a;
                optionsToolStripMenuItem.Enabled = a;
                if (statusreg == "Reg")
                {
                    ledgerToolStripMenuItem.Enabled = a;
                    billToolStripMenuItem.Enabled = a;
                    ReprintLastReceiptToolStripMenuItem.Enabled = a;
                    outstandinganAlysisToolStripMenuItem.Enabled = a;
                    trialBalanceToolStripMenuItem.Enabled = a;
                    bankEntryRegisterToolStripMenuItem.Enabled = a;
                    dayBookToolStripMenuItem.Enabled = a;
                    agentCommissionToolStripMenuItem.Enabled = a;
                }
                pOSToolStripMenuItem.Enabled = a;
                quickReceiptToolStripMenuItem.Enabled = a;
                paymentToolStripMenuItem.Enabled = a;
                purchaseToolStripMenuItem.Enabled = a;
                saleToolStripMenuItem.Enabled = a;
                clientsToolStripMenuItem.Enabled = a;
                itemToolStripMenuItem1.Enabled = a;
                masterToolStripMenuItem.Visible = a;
                SalesToolStripMenuItem.Visible = a;
                PrintToolStripMenuItem.Visible = a;
                UtilityToolStripMenuItem.Visible = a;
                ViewToolStripMenuItem.Visible = a;
                ExitToolStripMenuItem.Visible = a;
                bankEntryToolStripMenuItem.Enabled = a;
                
                
            }
            catch
            {
            }
        }

        public void disablecompany(bool a)
        {
            try
            {
                companyToolStripMenuItem.Visible = a;
            }
            catch
            {
            }
        }

        public void compId(string p)
        {
            // lblCompanyId.Text = "Company Id: "+ p;
            if (p != string.Empty)
            {
                companyId = p;
                lblcompanyid.Text = "Company Id: " + companyId;
                DataTable tax =conn.getdataset("select Taxation from Options");
                Taxactation = tax.Rows[0]["Taxation"].ToString();
            }
        }

        //public void getView()
        //{
        //    try
        //    {
        //        dt10 = con.getdataset("select a, u, d, v, p mId, uId, cId from UserRights where mId=4 and cId= " + Master.companyId + " and isActive=1");
        //        if (dt10 != null)
        //        {
        //            if (Convert.ToBoolean(dt10.Rows[0][3]) == false)
        //            {
        //                purchaseToolStripMenuItem.Visible = false;
        //            }
        //        }
        //    }
        //    catch { }
        //    finally
        //    {
        //        userLogin.Close();

        //    }

        //}

        private void Master_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            RemoveBorderOfButton();
          
           

        }

        private void InvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form1 frm = new Form1();
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void FloatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ClientRegistration frm = new ClientRegistration(this, tabControl);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void clientWiseProductMarginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ClientWiseProductMargin frm = new ClientWiseProductMargin();
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void CashPickupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ProductMaster frm = new ProductMaster();
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void PettyCashPickupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompanyMaster frm = new CompanyMaster();
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void HeldTransctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PaymentDetail frm = new PaymentDetail();
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void paymentStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PaymentStatus frm = new PaymentStatus();
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void QuantityInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Inward frm = new Inward();
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void XReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //rptstkin frm = new rptstkin();
            //frm.MdiParent = this;
           // frm.StartPosition = FormStartPosition.CenterScreen;
           // frm.Show();
        }

        private void dateWiseBillInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
         //   DateWiseReport frm = new DateWiseReport(this, tabControl);
         //   AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void inwardDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //StockIn frm = new StockIn();
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void ReprintLastReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
         //   StockReport frm = new StockReport(this, tabControl);
          //  AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void ZReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ClientWiseSelling frm = new ClientWiseSelling();
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void itemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Itemmaster frm = new Itemmaster();
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void saleToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // string[] strfinalarray = new string[5] { "S", "D", "Sale", "S", "" };
           // DateWiseReport frm = new DateWiseReport(this, tabControl, strfinalarray);
           // AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.Manual;
            //frm.Show();

        }

        private void billToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemWiseStock frm = new ItemWiseStock(this, tabControl);
            AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void itemToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Itemmaster frm = new Itemmaster(this, tabControl);
            AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();

            //string title = "TabPage " + (tabControl1.TabCount + 1).ToString();
            //TabPage myTabPage = new TabPage(title);
            //tabControl1.TabPages.Add(myTabPage);
        }
        private Control getControlByName(Control ctl, string ctlName)
        {
            foreach (Control child in ctl.Controls)
            {
                if (child.Name == ctlName)
                    return child;
                else if (child.HasChildren)
                {
                    Control retChild = getControlByName(child, ctlName);
                    if (retChild != null)
                        return retChild;
                }
            }
            return null;
        }

        public void RemoveCurrentTab1(string previouscontroal,string settext)
        {
            //DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dr == DialogResult.Yes)
            //{
            int tabs = tabControl.TabPages.Count;
            if (tabs > 1)
            {
                tabControl.TabPages.Remove(tabControl.SelectedTab);
                tabs = tabControl.TabPages.Count;
                if (tabs > 0)
                {
                    TabPage t = tabControl.TabPages[tabs - 1];
                    tabControl.SelectedTab = t;
                    Control ctl = getControlByName(this, previouscontroal);
                    ctl.Focus();
                    ctl.Text = settext;
                }
            }
            else
            {
                tabControl.TabPages.Remove(tabControl.SelectedTab);
                if (tabs > 0)
                {
                    Businessplus bus = new Businessplus();
                    AddNewTab(bus);
                }
                // }
            }

        }
        public void RemoveCurrentTab()
        {
            //DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dr == DialogResult.Yes)
            //{
            int tabs = tabControl.TabPages.Count;
            if (tabs > 1)
            {
                tabControl.TabPages.Remove(tabControl.SelectedTab);
                tabs = tabControl.TabPages.Count;
                if (tabs > 0)
                {
                    TabPage t = tabControl.TabPages[tabs - 1];
                    tabControl.SelectedTab = t;
                }
            }
            else
            {
                tabControl.TabPages.Remove(tabControl.SelectedTab);
                if (tabs > 0)
                {
                    Businessplus bus = new Businessplus();
                    AddNewTab(bus);
                }
                // }
            }

        }
        public void AddNewTab(Form frm)
        {
            try
            {
                if (tabControl.SelectedTab.Name == "Businessplus")
                {
                    tabControl.TabPages.Remove(tabControl.SelectedTab);
                }
            }
            catch
            {
            }
            TabPage tab = new TabPage(frm.Text);
            frm.TopLevel = false;

            frm.Parent = tab;

            frm.Visible = true;
            tab.Text = frm.Text;
            tabControl.TabPages.Add(tab);


            frm.Location = new Point(0, 0);
            tabControl.SelectedTab = tab;
            int count = tabControl.TabPages.Count;
            tabControl.TabPages[count - 1].Name = frm.Text;
            tabControl.TabPages[count - 1].Text = frm.Text;
            string apppath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            tabControl.TabPages[count - 1].BackgroundImage = Image.FromFile(apppath + "/BusinessPlusBackground.jpg");
            tabControl.TabPages[count - 1].BackgroundImageLayout = ImageLayout.None;



            //Graphics g = tabControl.CreateGraphics();
            //Rectangle rect = new Rectangle(tabControl.SelectedIndex * tabControl.ItemSize.Width + 2, 2, tabControl.ItemSize.Width - 2, tabControl.ItemSize.Height - 2);
            //g.FillRectangle(Brushes.LightBlue, rect);
            //g.DrawString(tabControl.SelectedTab.Text, new Font(tabControl.SelectedTab.Font, FontStyle.Bold), Brushes.Black, rect);

            //     tabControl.TabPages[count - 1].Font = new Font(tabControl.SelectedTab.Font, FontStyle.Bold);
            frm.Focus();

        }


        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Group frm = new Group();
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  ClientRegistration frm = new ClientRegistration(this, tabControl);
           // AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void ledgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
         //   Ledger frm = new Ledger(this, tabControl);
          //  AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void quickReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  QReceipt frm = new QReceipt(this, tabControl);
        //    AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void paymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  QPayment frm = new QPayment(this, tabControl);
           // AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void pOSBillListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           // POSBillList frm = new POSBillList(this, tabControl);
        //    AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void pOSItemListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        //    POSItemList frm = new POSItemList(this, tabControl);
          //  AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void pOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
         //   DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='POS' and setdefault=1");
        //    POS bd = new POS(this, tabControl);
         //   DefaultPOS p = new DefaultPOS(this, tabControl);
          //  if (dt1.Rows[0]["formname"].ToString() == p.Text)
         //   {
         //       AddNewTab(p);
                //bd.MdiParent = this.MdiParent;
                //bd.StartPosition = FormStartPosition.CenterScreen;
                //bd.Show();
         //   }
            //else if (dt1.Rows[0]["formname"].ToString() == bd.Text)
            //{
            //    AddNewTab(bd);
            //    //p.MdiParent = this.MdiParent;
            //    //p.StartPosition = FormStartPosition.CenterScreen;
            //    //p.Show();
            //}

        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
         //   string[] strfinalarray = new string[5] { "P", "C", "Purchase", "P", "" };
            //DateWisePurchaseReport frm = new DateWisePurchaseReport("p",this,tabControl);
            //AddNewTab(frm);
        //    DateWiseReport frm = new DateWiseReport(this, tabControl, strfinalarray);
        //    AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;

            //    frm.Show();

        }

        private void purchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DateWisePurchaseOrderReport frm = new DateWisePurchaseOrderReport();
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertCompany frm = new InsertCompany(this, tabControl);
            AddNewTab(frm);
            //frm.Passed(1);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CompanyList frm = new CompanyList(this, tabControl);
            //AddNewTab(frm);
            CultureInfo en = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = en;
            ds = ods.getdata("Select * from tblreg");
            dtreg = ds.Tables[0];

            #region
            if (dtreg.Rows.Count > 0)
            {


                // Decryptinstoaldata(dtreg.Rows[0]["d14"]).ToString(dateformate));
                string t = dtreg.Rows[0]["d14"].ToString();
                Decryptexpdate(t);
                string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                //  DateTime dtdecr = Convert.ToDateTime(Decryptexpdate1);
                DateTime dtdecr = DateTime.ParseExact(Decryptexpdate1, "dd-MM-yyyy", en);
                //DateTime dtcurrentdate = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"));
                //DateTime dtexpdate = Convert.ToDateTime(dtdecr.ToString("MM-dd-yyyy"));
                DateTime dtcurrentdate = Convert.ToDateTime(DateTime.Now.ToString());
                DateTime dtexpdate = dtdecr;
                if (dtexpdate <= dtcurrentdate)
                {
                    enablemenu(false);
                    AMC ci = new AMC(this, tabControl);
                    AddNewTab(ci);
                }
                else
                {
                    string reg = dtreg.Rows[0]["d16"].ToString();
                    Decrypstatus(reg);
                    if (statusreg == "Reg")
                    {
                        registerNowToolStripMenuItem.Visible = false;
                        CompanyList ci = new CompanyList(this, tabControl);
                        AddNewTab(ci);
                    }
                    else
                    {
                        utilityandreportsdisable();
                        string t1 = dtreg.Rows[0]["d18"].ToString();
                        Decryptinstoaldata(t1);
                        DateTime dtdecrtotal = DateTime.ParseExact(Decryptinstoaldate1, "dd-MM-yyyy", en);
                        string d1 = DateTime.Now.ToString("dd-MM-yyyy");
                        //DateTime dtdecrtotal = Convert.ToDateTime(Decryptinstoaldate1);
                        // string d1 = DateTime.Now.ToString("MM-dd-yyyy");
                        DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString());
                        DateTime dtexpdate1 = dtdecrtotal;
                        //DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"));
                        //DateTime dtexpdate1 = Convert.ToDateTime(dtdecrtotal.ToString("dd/MM/yyyy"));
                        //dtexpdate1 = dtexpdate1.AddDays(7);
                        if (dtexpdate1 <= dtcurrentdate1)
                        {
                            enablemenu(false);
                            RegisterNow ci = new RegisterNow(this, tabControl);
                            AddNewTab(ci);
                        }
                        else
                        {
                            CompanyList ci = new CompanyList(this, tabControl);
                            AddNewTab(ci);
                        }

                    }
                }
            }
            else
            {
                CompanyList ci = new CompanyList(this, tabControl);
                AddNewTab(ci);
            }
            #endregion
           
        }
        public static string Decryptexpdate(string cipherText)
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
                    Decryptexpdate1 = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
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
        public static string Decryptinstoaldata(string cipherText)
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
                    Decryptinstoaldate1 = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        private void mapCompanyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ds = ods.getdata("Select * from tblreg");
            dtreg = ds.Tables[0];
            CultureInfo en = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = en;

            #region
            if (dtreg.Rows.Count > 0)
            {


                // Decryptinstoaldata(dtreg.Rows[0]["d14"]).ToString(dateformate));
                string t = dtreg.Rows[0]["d14"].ToString();
                Decryptexpdate(t);
                string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                //  DateTime dtdecr = Convert.ToDateTime(Decryptexpdate1);
                DateTime dtdecr = DateTime.ParseExact(Decryptexpdate1, "dd-MM-yyyy", en);
                //DateTime dtcurrentdate = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"));
                //DateTime dtexpdate = Convert.ToDateTime(dtdecr.ToString("MM-dd-yyyy"));
                DateTime dtcurrentdate = Convert.ToDateTime(DateTime.Now.ToString());
                DateTime dtexpdate = dtdecr;
                if (dtexpdate <= dtcurrentdate)
                {
                    enablemenu(false);
                    AMC ci = new AMC(this, tabControl);
                    AddNewTab(ci);
                }
                else
                {
                    string reg = dtreg.Rows[0]["d16"].ToString();
                    Decrypstatus(reg);
                    if (statusreg == "Reg")
                    {
                        registerNowToolStripMenuItem.Visible = false;
                        CompanyList ci = new CompanyList(this, tabControl);
                        AddNewTab(ci);
                    }
                    else
                    {
                        utilityandreportsdisable();
                        string t1 = dtreg.Rows[0]["d18"].ToString();
                        Decryptinstoaldata(t1);
                        DateTime dtdecrtotal = DateTime.ParseExact(Decryptinstoaldate1, "dd-MM-yyyy", en);
                        string d1 = DateTime.Now.ToString("dd-MM-yyyy");
                        //DateTime dtdecrtotal = Convert.ToDateTime(Decryptinstoaldate1);
                        // string d1 = DateTime.Now.ToString("MM-dd-yyyy");
                        DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString());
                        DateTime dtexpdate1 = dtdecrtotal;
                        //DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"));
                        //DateTime dtexpdate1 = Convert.ToDateTime(dtdecrtotal.ToString("dd/MM/yyyy"));
                        //dtexpdate1 = dtexpdate1.AddDays(7);
                        if (dtexpdate1 <= dtcurrentdate1)
                        {
                            enablemenu(false);
                            RegisterNow ci = new RegisterNow(this, tabControl);
                            AddNewTab(ci);
                        }
                        else
                        {
                            CompanyList ci = new CompanyList(this, tabControl);
                            AddNewTab(ci);
                        }

                    }
                }
            }
            else
            {
                CompanyList ci = new CompanyList(this, tabControl);
                AddNewTab(ci);
            }
            #endregion

           
        }

        private void sQLServerSettingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //AddConString frm = new AddConString();
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
        //    UserList frm = new UserList(this, tabControl);
         //   AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void userProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
        //    UserRights frm = new UserRights(this, tabControl);
         //   AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void companyInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompanyInfo frm = new CompanyInfo();
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void closeCompanyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enablemenu(false);
            disablecompany(true);
            lblcompanyid.Text = "";
            companyId = string.Empty;
            CompanyList ci = new CompanyList(this);

            ci.MdiParent = this;
            ci.StartPosition = FormStartPosition.CenterScreen;
            ci.Show();
        }

        private void purchaseReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PurchaseReturnList frm = new PurchaseReturnList();
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void saleReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  string[] strfinalarray = new string[5] { "SR", "C", "SaleReturn", "SR", "" };
       //     DateWiseReport frm = new DateWiseReport(this, tabControl, strfinalarray);
         //   AddNewTab(frm);
            //DatewiseSaleReturn frm = new DatewiseSaleReturn(this, tabControl);
            //AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }
        public static string currency = string.Empty;
        internal void setheader(DataTable companydt, string p)
        {

            ds = ods.getdata("Select * from tblreg");
            dtreg = ds.Tables[0];
            DataTable date = conn.getdataset("Select * from Options");
            dateformate = date.Rows[0]["dateformate"].ToString();
            string fy = DateTime.Parse(companydt.Rows[0][22].ToString()).ToString(dateformate);
            string to = DateTime.Parse(companydt.Rows[0][23].ToString()).ToString(dateformate);
            //string fy = Convert.ToDateTime(f).ToString(dateformate);
            // string to = Convert.ToDateTime(y).ToString(dateformate);
            string reg = dtreg.Rows[0]["d16"].ToString();
            Decrypstatus(reg);
            this.Text = "Total Business/Production ERP[" + statusreg + "] #180125 [" + companydt.Rows[0][2].ToString() + "] [FY = " + fy + " to " + to + "] User=" + p;
            lblcurrency.Text = "[" + companydt.Rows[0][30].ToString() + "]";
            currency = lblcurrency.Text;
        }

        private void pOSBillListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openingStockEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  OpStock frm = new OpStock(this, tabControl);
         //   AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }



        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeCompanyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form2 = new Master();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options frm = new Options(this, tabControl);
            AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void purchaseTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // PurchaseTypeMaster frm = new PurchaseTypeMaster(this, tabControl);
        //    AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void companyInfoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CompanyInfo frm = new CompanyInfo(this, tabControl);
            AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void itemMasterPriceChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // ItemMasterPriceChange frm = new ItemMasterPriceChange(this, tabControl);
          //  AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void cashBankBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  CashBook frm = new CashBook(this, tabControl);
        //    AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void partywisediscount_Click(object sender, EventArgs e)
        {
            //PartyGroupwiseDiscount frm = new PartyGroupwiseDiscount(this, tabControl);
           // AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void SalesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void accountGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // Group frm = new Group(this, tabControl);
         //   AddNewTab(frm);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void CalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Process.Start("Calc");
        }

        private void SoftwareInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private void listOfPOSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           // ListPOS frm = new ListPOS(this, tabControl);
          //  AddNewTab(frm);
        }

        private void importItemToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sqlServerSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changeFinancialYearToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void setDatabasePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDatabasePath rn = new AddDatabasePath(this, tabControl);
            AddNewTab(rn);
        }

        private void deleteCompanyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
           // Saletypemaster frm = new Saletypemaster(this, tabControl);
          //  AddNewTab(frm);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
           // string[] strfinalarray = new string[5] { "PR", "D", "PurchaseReturn", "PR", "" };
          //  DateWiseReport frm = new DateWiseReport(this, tabControl, strfinalarray);
          //  AddNewTab(frm);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //string[] strfinalarray = new string[5] { "SO", "D", "Sale Order", "SO", "" };
           // SaleOrderList sol = new SaleOrderList(this, tabControl, strfinalarray);
           // AddNewTab(sol);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //string[] strfinalarray = new string[5] { "SC", "D", "Sale Challan", "SC", "" };
          //  SaleOrderList sol = new SaleOrderList(this, tabControl, strfinalarray);
        //    AddNewTab(sol);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
         //   string[] strfinalarray = new string[5] { "PO", "C", "Purchase Order", "PO", "" };
           // SaleOrderList sol = new SaleOrderList(this, tabControl, strfinalarray);
           // AddNewTab(sol);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            //string[] strfinalarray = new string[5] { "PC", "C", "Purchase Challan", "PC", "" };
           // SaleOrderList sol = new SaleOrderList(this, tabControl, strfinalarray);
         //   AddNewTab(sol);
        }

        private void registerNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterNow rn = new RegisterNow(this, tabControl);
            AddNewTab(rn);
        }


        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage CurrentTab = tabControl.TabPages[e.Index];
            Rectangle ItemRect = tabControl.GetTabRect(e.Index);
            SolidBrush FillBrush = new SolidBrush(Color.Blue);
            SolidBrush TextBrush = new SolidBrush(Color.White);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            //If we are currently painting the Selected TabItem we'll
            //change the brush colors and inflate the rectangle.
            if (System.Convert.ToBoolean(e.State & DrawItemState.Selected))
            {
                FillBrush.Color = Color.White;
                TextBrush.Color = Color.DarkBlue;
                ItemRect.Inflate(2, 2);
            }

            //Set up rotation for left and right aligned tabs
            if (tabControl.Alignment == TabAlignment.Left || tabControl.Alignment == TabAlignment.Right)
            {
                float RotateAngle = 90;
                if (tabControl.Alignment == TabAlignment.Left)
                    RotateAngle = 270;
                PointF cp = new PointF(ItemRect.Left + (ItemRect.Width / 2), ItemRect.Top + (ItemRect.Height / 2));
                e.Graphics.TranslateTransform(cp.X, cp.Y);
                e.Graphics.RotateTransform(RotateAngle);
                ItemRect = new Rectangle(-(ItemRect.Height / 2), -(ItemRect.Width / 2), ItemRect.Height, ItemRect.Width);
            }

            //Next we'll paint the TabItem with our Fill Brush
            e.Graphics.FillRectangle(FillBrush, ItemRect);

            //Now draw the text.
            e.Graphics.DrawString(CurrentTab.Text, e.Font, TextBrush, (RectangleF)ItemRect, sf);

            //Reset any Graphics rotation
            e.Graphics.ResetTransform();

            //Finally, we should Dispose of our brushes.
            FillBrush.Dispose();
            TextBrush.Dispose();
        }

        private void gSTR1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          //  GSTR1 rn = new GSTR1(this, tabControl);
         //   AddNewTab(rn);
        }

        private void aMCInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AMC rn = new AMC(this, tabControl);
            AddNewTab(rn);
        }

        private void serialNoTrackingToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  serialnotracking rn = new serialnotracking(this, tabControl);
           // AddNewTab(rn);
        }

        private void gSTR2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        //    GSTR2 rn = new GSTR2(this, tabControl);
          //  AddNewTab(rn);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
         //   ChargesHead rn = new ChargesHead(this, tabControl);
           // AddNewTab(rn);
        }

        private void pOSRegisterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
         //   POSRegister rn = new POSRegister(this, tabControl);
          //  AddNewTab(rn);
        }

        private void billReprintToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gSTR3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void btnItemPanel_Click(object sender, EventArgs e)
        {
            
            
        }

        private void btnAccountpanel_Click(object sender, EventArgs e)
        {
          //  ClientRegistration frm = new ClientRegistration(this, tabControl);
         //   AddNewTab(frm);
        }

        private void btnSalePanel_Click(object sender, EventArgs e)
        {
          //  string[] strfinalarray = new string[5] { "S", "D", "Sale", "S", "" };
         //   DateWiseReport frm = new DateWiseReport(this, tabControl, strfinalarray);
        //    AddNewTab(frm);
        }

        private void btnPurchasePanel_Click(object sender, EventArgs e)
        {
         //   string[] strfinalarray = new string[5] { "P", "C", "Purchase", "P", "" };
         //   DateWiseReport frm = new DateWiseReport(this, tabControl, strfinalarray);
         //   AddNewTab(frm);
        }

        private void btnQuickPaymentPanel_Click(object sender, EventArgs e)
        {
           // QPayment frm = new QPayment(this, tabControl);
          //  AddNewTab(frm);
        }

        private void btnQuickReceiptPanel_Click(object sender, EventArgs e)
        {
           // QReceipt frm = new QReceipt(this, tabControl);
            //AddNewTab(frm);
        }

        private void btnPOSPanel_Click(object sender, EventArgs e)
        {
            //DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='POS' and setdefault=1");
            //POS bd = new POS(this, tabControl);
          //  DefaultPOS p = new DefaultPOS(this, tabControl);
           // if (dt1.Rows[0]["formname"].ToString() == p.Text)
           // {
             //   AddNewTab(p);
                //bd.MdiParent = this.MdiParent;
                //bd.StartPosition = FormStartPosition.CenterScreen;
                //bd.Show();
         //   }
            //else if (dt1.Rows[0]["formname"].ToString() == bd.Text)
            //{
            //    AddNewTab(bd);
            //    //p.MdiParent = this.MdiParent;
            //    //p.StartPosition = FormStartPosition.CenterScreen;
            //    //p.Show();
            //}

        }

        private void btnLedgerPanel_Click(object sender, EventArgs e)
        {
           // Ledger frm = new Ledger(this, tabControl);
          //  AddNewTab(frm);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ItemWiseStock frm = new ItemWiseStock(this, tabControl);
            AddNewTab(frm);
        }

        private void PnlButton_Click(object sender, EventArgs e)
        {
            
        }
        public void RemoveBorderOfButton()
        {
           
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            TaxSlab frm = new TaxSlab(this, tabControl);
            AddNewTab(frm);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            PrimaryUnit frm = new PrimaryUnit(this, tabControl);
            AddNewTab(frm);
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            ItemGroup frm = new ItemGroup(this, tabControl);
            AddNewTab(frm);
        }

        private void btnItemPanel_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void btnItemPanel_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void btnItemPanel_Enter(object sender, EventArgs e)
        {
            //btnItemPanel.BackColor = Color.LightYellow;
            //btnItemPanel.ForeColor = Color.Red;
        }

        private void btnAccountpanel_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void btnAccountpanel_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void btnSalePanel_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void btnSalePanel_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void btnItemPanel_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void btnPurchasePanel_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void btnPurchasePanel_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void btnQuickPaymentPanel_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void btnQuickPaymentPanel_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void btnQuickReceiptPanel_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void btnQuickReceiptPanel_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void btnPOSPanel_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void btnLedgerPanel_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void btnPOSPanel_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void btnLedgerPanel_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void outstandinganAlysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // outstandinganalysis frm = new outstandinganalysis(this, tabControl);
          //  AddNewTab(frm);
        }

        private void printBarcodeLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  PrintBarcode frm = new PrintBarcode(this, tabControl);
         //   AddNewTab(frm);
        }

        private void trialBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // TrialBalance tb = new TrialBalance(this, tabControl);
          //  AddNewTab(tb);
        }

        private void bankEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // BankEntry be = new BankEntry(this, tabControl);
           // AddNewTab(be);
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            CompanyMaster frm = new CompanyMaster(this, tabControl);
            AddNewTab(frm);
        }

        private void debitNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // string[] debitorcredit = new string[5] { "D", "", "", "", "" };
           // DebitandCreditNote frm = new DebitandCreditNote(this, tabControl, debitorcredit);
           // AddNewTab(frm);
        }

        private void creditNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // string[] debitorcredit = new string[5] { "C", "", "", "", "" };
           // DebitandCreditNote frm = new DebitandCreditNote(this, tabControl, debitorcredit);
          //  AddNewTab(frm);
        }

        private void bankEntryRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // BankEntryReport be = new BankEntryReport(this, tabControl);
          //  AddNewTab(be);
        }

        private void debitNoteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          //  DebitNoteReport be = new DebitNoteReport(this, tabControl);
         //   AddNewTab(be);
        }

        private void creditNoteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           // CreditNoteReport be = new CreditNoteReport(this, tabControl);
          //  AddNewTab(be);
        }

        private void dayBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DayBook be = new DayBook(this, tabControl);
            //AddNewTab(be);
        }

        private void agentCommissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  AgentCommissionReport be = new AgentCommissionReport(this, tabControl);
           // AddNewTab(be);
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            ProcessList pl = new ProcessList(this,tabControl);
            AddNewTab(pl);
        }

        private void productionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Production p = new Production(this,tabControl);
            AddNewTab(p);
        }

        private void prodcutionRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            productionregister pr = new productionregister(this,tabControl);
            AddNewTab(pr);
        }

        private void productionPlanningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductionPlanning pr = new ProductionPlanning(this, tabControl);
            AddNewTab(pr);
        }

        private void productionUtilizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product_Utilization pu = new Product_Utilization(this,tabControl);
            AddNewTab(pu);
        }

        private void finishedGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FinishedGoods fg = new FinishedGoods(this,tabControl);
            AddNewTab(fg);
        }

        private void finishedGoodsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FinishedGoodsList fl = new FinishedGoodsList(this,tabControl);
            AddNewTab(fl);
        }

        private void versionControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update u = new Update(this,tabControl);
            AddNewTab(u);
        }

    }
}
