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
using System.Configuration;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Management;
using LoggingFramework;

namespace RamdevSales
{
    public partial class Master : Form
    {
        private UserLogin userLogin;
        public static string companyId = string.Empty;
        public static string Taxactation = string.Empty;
        public static string dateformate = string.Empty;
        public static string statusreg = string.Empty;
        public static string userid = string.Empty;
        public static string expdate = string.Empty;
        public static string regdate = string.Empty;
        public static string status = string.Empty;
        DataTable dt10 = new DataTable();
        Connection conn = new Connection();
        OleDbSettings ods = new OleDbSettings();
        DataTable dtreg, dtcom, dtcount, dtedu, dtserail, dtreg1 = new DataTable();
        DataSet ds, ds1, ds2, ds3, ds4, ds5 = new DataSet();
        public static string Decryptexpdate1;
        public static string Decryptdateregstr;
        public static string Decryptinstoaldate1;
        public static string Decryptmaca;
        ServerConnection con = new ServerConnection();
        SqlConnection constring = new SqlConnection("Data Source=184.168.47.17;Initial Catalog=BusinessPlus;User ID=Businessplus;Password=Businessplus1!");
        datetime date = new datetime();
        public static string Encryptexpdate(string clearText)
        {
            string EncryptionKey = "00";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                    expdate = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Encryptregdate(string clearText)
        {
            string EncryptionKey = "00";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                    regdate = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Encryptstatus(string clearText)
        {
            string EncryptionKey = "00";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                    status = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        SqlConnection con123 = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        datetime defaultdateformate = new datetime();
        public static String sMacAddress = string.Empty;
        public void getmacserial()
        {
            ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
            foreach (ManagementObject getserial in MOS.Get())
            {
                sMacAddress = getserial["SerialNumber"].ToString();
            }
        }
        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();

                }
            } return sMacAddress;
        }
        public static string Decryptmacadd(string cipherText)
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
                    Decryptmaca = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static string srno = string.Empty;
        public static string Decryptsrno(string cipherText)
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
                    srno = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public void UUID()
        {

            string uuid = string.Empty;

            ManagementClass mc = new ManagementClass("Win32_ComputerSystemProduct");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                uuid = mo.Properties["UUID"].Value.ToString();
            }
            sMacAddress = uuid;
        }
        public string GetCPUId()
        {

            string cpuInfo = String.Empty;
            //create an instance of the Managemnet class with the
            //Win32_Processor class
            ManagementClass mgmt = new ManagementClass("Win32_Processor");
            //create a ManagementObjectCollection to loop through
            ManagementObjectCollection objCol = mgmt.GetInstances();
            //start our loop for all processors found
            foreach (ManagementObject obj in objCol)
            {
                if (cpuInfo == String.Empty)
                {
                    // only return cpuInfo from first CPU
                    cpuInfo = obj.Properties["ProcessorId"].Value.ToString();
                }
            }
            return cpuInfo;
        }

        public Master()
        {
            InitializeComponent();
            CultureInfo en1 = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = en1;
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
            regkey.SetValue("sShortDate", "dd-MM-yyyy");
            ds4 = ods.getdata("Select * from tblHeader");
            dtserail = ds4.Tables[0];
            ds5 = ods.getdata("Select * from tblreg");
            dtreg1 = ds5.Tables[0];
            if (dtreg1.Rows.Count > 0)
            {
                getmacserial();
                if (sMacAddress == "To be filled by O.E.M.")
                {
                    UUID();
                }
                if (dtserail.Rows.Count > 0)
                {
                    if (dtserail.Rows[0]["TillNo"].ToString() != "Updated")
                    {
                        ods.execute("UPDATE [tblHeader] SET [TillNo] ='" + "Updated" + "'");
                        ods.execute("UPDATE [tblreg] SET [d7] ='" + sMacAddress + "'");
                        try
                        {

                            Decryptsrno(dtreg1.Rows[0]["d1"].ToString());
                            //  DataTable seraverdata = conn.getdataset("select * from tblRegistrationMaster where isactive=1 and srno='"+srno+"'", constring);
                            con.execute("UPDATE [businessplus].[tblRegistrationMaster] SET mac='" + sMacAddress + "' where srno='" + srno + "'", constring);
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    ods.execute("INSERT INTO [tblHeader]([TillNo])VALUES('" + "Updated" + "')");
                    ods.execute("UPDATE [tblreg] SET [d7] ='" + sMacAddress + "'");
                    try
                    {

                        Decryptsrno(dtreg1.Rows[0]["d1"].ToString());
                        //  DataTable seraverdata = conn.getdataset("select * from tblRegistrationMaster where isactive=1 and srno='"+srno+"'", constring);
                        con.execute("UPDATE [businessplus].[tblRegistrationMaster] SET mac='" + sMacAddress + "' where srno='" + srno + "'", constring);
                    }
                    catch
                    {
                    }

                }
            }
            //regkey.SetValue("sLongDate", "yyyy-MM-dd");
            //Console.Write(DateTime.Now + "");
            //RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
            //rkey.SetValue("sShortDate", "yyyy-MM-dd");
            //ods.execute("delete from tblBody");
            ds1 = ods.getdata("Select * from Company");
            dtcom = ds1.Tables[0];
            ds = ods.getdata("Select * from tblreg");
            dtreg = ds.Tables[0];
            if (dtcom.Rows.Count != 0)
            {
                if (dtreg.Rows.Count == 0)
                {
                    MessageBox.Show("You Are Not Authorized to Use This Software");
                    enablemenu(false);
                    disablecompany(false);
                    return;
                }
            }
            if (dtreg.Rows.Count > 0)
            {
                // GetMACAddress();
                getmacserial();
                if (sMacAddress == "To be filled by O.E.M.")
                {
                    UUID();
                }
                if (!string.IsNullOrEmpty(dtreg.Rows[0]["d7"].ToString()))
                {
                    if (sMacAddress != dtreg.Rows[0]["d7"].ToString())
                    {
                        MessageBox.Show("You Are Not Authorized to Use This Software");
                        ods.execute("UPDATE [tblBody] SET [Quantity] ='" + "1" + "'");
                        enablemenu(false);
                        disablecompany(false);
                        AMC ci = new AMC(this, tabControl);
                        AddNewTab(ci);
                        return;

                    }
                }
                ds2 = ods.getdata("Select * from tblBody");
                dtcount = ds2.Tables[0];
                if (dtcount.Rows.Count > 0)
                {
                    try
                    {
                        if (dtcount.Rows[0]["Quantity"].ToString() == "1")
                        {
                            enablemenu(false);
                            disablecompany(false);
                            AMC ci = new AMC(this, tabControl);
                            AddNewTab(ci);
                            return;

                        }
                        CultureInfo en = new CultureInfo("en-US");
                        Thread.CurrentThread.CurrentCulture = en;
                        DateTime regdate1 = Convert.ToDateTime(dtcount.Rows[0]["Description"].ToString());
                        DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"), en);
                        TimeSpan day1 = dtcurrentdate1.Date - regdate1.Date;
                        int daydiff1 = day1.Days;
                        if (daydiff1 == 365)
                        {
                            ods.execute("UPDATE [tblBody] SET [Quantity] ='" + "1" + "'");
                            enablemenu(false);
                            disablecompany(false);
                            AMC ci = new AMC(this, tabControl);
                            AddNewTab(ci);

                        }
                    }
                    catch
                    {
                    }
                }
                CultureInfo en2 = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = en2;
                string datereg = dtreg.Rows[0]["d10"].ToString();
                Decryptdatereg(datereg);
                DateTime regdate = Convert.ToDateTime(Decryptdateregstr);
                DateTime dtcurrentdate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"), en2);
                TimeSpan day = dtcurrentdate.Date - regdate.Date;
                int daydiff = day.Days;
                if (dtcount.Rows.Count > 0)
                {
                    ods.execute("UPDATE [tblBody] SET [itemcode] ='" + daydiff + "',[Description]='" + regdate + "'");
                }
                else
                {
                    ods.execute("INSERT INTO [tblBody]([itemcode],[Description])VALUES('" + daydiff + "','" + regdate + "')");
                }
            }
            //ds2 = ods.getdata("Select * from tblBody");
            //dtcount = ds2.Tables[0];
            if (dtcom.Rows.Count > 0)
            {
                try
                {
                    Decryptsrno(dtreg.Rows[0]["d1"].ToString());
                    DataTable serverdata = con.getdataset("select * from tblRegistrationMaster where isactive=1 and srno='" + srno + "'", constring);
                    if (serverdata.Rows.Count > 0)
                    {
                        getmacserial();
                        if (sMacAddress != serverdata.Rows[0]["mac"].ToString())
                        {
                            ods.execute("UPDATE [tblreg] SET [d7] ='" + serverdata.Rows[0]["mac"].ToString() + "'");
                        }
                        CultureInfo en = new CultureInfo("en-US");
                        Thread.CurrentThread.CurrentCulture = en;
                        string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                        string sa = DateTime.Parse(serverdata.Rows[0]["expdate"].ToString()).ToString("dd-MM-yyyy");
                        DateTime dtdecr = Convert.ToDateTime(sa, en);
                        DateTime dtcurrentdate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"), en);
                        DateTime dtexpdate = dtdecr;
                        string serveramc = serverdata.Rows[0]["AMCDay"].ToString();
                        if (string.IsNullOrEmpty(serverdata.Rows[0]["AMCDay"].ToString()))
                        {
                            serveramc = "364";
                        }
                        if (!string.IsNullOrEmpty(serveramc))
                        {

                            string sa1 = DateTime.Parse(serverdata.Rows[0]["expdate"].ToString()).ToString("dd-MM-yyyy");
                            DateTime dtdecr1 = Convert.ToDateTime(sa1, en);
                            DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"), en);
                            //  DateTime dtexpdate1 = dtdecr1.AddDays(Convert.ToInt32(serveramc));
                            sa1 = dtdecr1.ToString("dd-MM-yyyy");
                            Encryptexpdate(sa);
                            Encryptregdate(sa1);
                            ods.execute("UPDATE [tblreg] SET [d14] ='" + expdate + "'");
                            if (dtdecr1 <= dtcurrentdate1)
                            {
                                ods.execute("UPDATE [tblBody] SET [Quantity] ='" + "1" + "'");
                                Encryptexpdate(sa);
                                //  Encryptstatus("Edu");
                                Encryptregdate(sa1);
                                ods.execute("UPDATE [tblreg] SET [d14] ='" + expdate + "'");
                                enablemenu(false);
                                disablecompany(false);
                                AMC ci = new AMC(this, tabControl);
                                AddNewTab(ci);
                                return;

                            }
                        }
                        if (dtexpdate <= dtcurrentdate)
                        {
                            string sa1 = DateTime.Parse(serverdata.Rows[0]["expdate"].ToString()).ToString("dd-MM-yyyy");
                            DateTime dtdecr1 = Convert.ToDateTime(sa1, en);
                            DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"), en);
                            //  DateTime dtexpdate1 = dtdecr1.AddDays(Convert.ToInt32(serverdata.Rows[0]["AMCDay"].ToString()));
                            sa1 = dtdecr1.ToString("dd-MM-yyyy");
                            if (dtdecr1 <= dtcurrentdate1)
                            {
                                ods.execute("UPDATE [tblBody] SET [Quantity] ='" + "1" + "'");
                                Encryptexpdate(sa);
                                //   Encryptstatus("Edu");
                                Encryptregdate(sa1);
                                ods.execute("UPDATE [tblreg] SET [d14] ='" + expdate + "'");
                                enablemenu(false);
                                disablecompany(false);
                                AMC ci = new AMC(this, tabControl);
                                AddNewTab(ci);
                                return;

                            }
                        }
                    }
                }
                catch
                {
                }
                if (dtcom.Rows[0]["Catalyst"].ToString() == "True")
                {
                    CultureInfo en = new CultureInfo("en-US");
                    Thread.CurrentThread.CurrentCulture = en;
                    lblcompanyid.Text = "Company Id: " + companyId;

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
                        DateTime dtcurrentdate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"), en);
                        DateTime dtexpdate = dtdecr;
                        DateTime exppopup = dtdecr;
                        exppopup = exppopup.AddDays(-30);
                        if (exppopup <= dtcurrentdate)
                        {
                            MessageBox.Show("Your AMC has been Expired Soon Kindly contact Authorized person to extend your AMC date.");
                        }
                        if (dtexpdate <= dtcurrentdate)
                        {
                            ods.execute("UPDATE [tblBody] SET [Quantity] ='" + "1" + "'");
                            enablemenu(false);
                            disablecompany(false);
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
                                ds3 = ods.getdata("Select * from SQLSetting");
                                dtedu = ds3.Tables[0];
                                //  utilityandreportsdisable();
                                if (dtedu.Rows[0]["OT4"].ToString() == "2")
                                {
                                    enablemenu(false);
                                    disablecompany(false);
                                    RegisterNow ci = new RegisterNow(this, tabControl);
                                    AddNewTab(ci);
                                    return;
                                }
                                string t1 = dtreg.Rows[0]["d18"].ToString();
                                Decryptinstoaldata(t1);
                                DateTime dtdecrtotal = DateTime.ParseExact(Decryptinstoaldate1, "dd-MM-yyyy", en);
                                string d1 = DateTime.Now.ToString("dd-MM-yyyy");
                                //DateTime dtdecrtotal = Convert.ToDateTime(Decryptinstoaldate1);
                                // string d1 = DateTime.Now.ToString("MM-dd-yyyy");
                                DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"), en);
                                DateTime dtexpdate1 = dtdecrtotal;
                                //DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"));
                                //DateTime dtexpdate1 = Convert.ToDateTime(dtdecrtotal.ToString("dd/MM/yyyy"));
                                //dtexpdate1 = dtexpdate1.AddDays(7);
                                if (dtexpdate1 <= dtcurrentdate1)
                                {
                                    if (dtedu.Rows[0]["OT4"].ToString() == "1")
                                    {
                                        ods.execute("UPDATE [SQLSetting] SET [OT4] ='" + "2" + "'");
                                    }
                                    enablemenu(false);
                                    disablecompany(false);
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
                else
                {
                    #region
                    if (dtreg.Rows.Count > 0)
                    {
                        // Decryptinstoaldata(dtreg.Rows[0]["d14"]).ToString(dateformate));
                        CultureInfo en = new CultureInfo("en-US");
                        Thread.CurrentThread.CurrentCulture = en;
                        string t = dtreg.Rows[0]["d14"].ToString();
                        Decryptexpdate(t);
                        string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                        //  DateTime dtdecr = Convert.ToDateTime(Decryptexpdate1);
                        DateTime dtdecr = DateTime.ParseExact(Decryptexpdate1, "dd-MM-yyyy", en);
                        //DateTime dtcurrentdate = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"));
                        //DateTime dtexpdate = Convert.ToDateTime(dtdecr.ToString("MM-dd-yyyy"));
                        DateTime dtcurrentdate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"), en);
                        DateTime dtexpdate = dtdecr;
                        DateTime exppopup = dtdecr;
                        exppopup = exppopup.AddDays(-30);
                        if (exppopup <= dtcurrentdate)
                        {
                            MessageBox.Show("Your AMC has been Expired Soon Kindly contact Authorized person to extend your AMC date.");
                        }
                        if (dtexpdate <= dtcurrentdate)
                        {
                            ods.execute("UPDATE [tblBody] SET [Quantity] ='" + "1" + "'");
                            enablemenu(false);
                            disablecompany(false);
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
                                UserLogin ul = new UserLogin(this, tabControl);
                                AddNewTab(ul);
                            }
                            else
                            {
                                //   utilityandreportsdisable();
                                ds3 = ods.getdata("Select * from SQLSetting");
                                dtedu = ds3.Tables[0];
                                //  utilityandreportsdisable();
                                if (dtedu.Rows[0]["OT4"].ToString() == "2")
                                {
                                    enablemenu(false);
                                    disablecompany(false);
                                    RegisterNow ci = new RegisterNow(this, tabControl);
                                    AddNewTab(ci);
                                    return;
                                }
                                string t1 = dtreg.Rows[0]["d18"].ToString();
                                Decryptinstoaldata(t1);
                                DateTime dtdecrtotal = DateTime.ParseExact(Decryptinstoaldate1, "dd-MM-yyyy", en);
                                string d1 = DateTime.Now.ToString("dd-MM-yyyy");
                                //DateTime dtdecrtotal = Convert.ToDateTime(Decryptinstoaldate1);
                                // string d1 = DateTime.Now.ToString("MM-dd-yyyy");
                                DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"), en);
                                DateTime dtexpdate1 = dtdecrtotal;
                                //DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"));
                                //DateTime dtexpdate1 = Convert.ToDateTime(dtdecrtotal.ToString("dd/MM/yyyy"));
                                //dtexpdate1 = dtexpdate1.AddDays(7);
                                if (dtexpdate1 <= dtcurrentdate1)
                                {
                                    if (dtedu.Rows[0]["OT4"].ToString() == "1")
                                    {
                                        ods.execute("UPDATE [SQLSetting] SET [OT4] ='" + "2" + "'");
                                    }
                                    enablemenu(false);
                                    disablecompany(false);
                                    RegisterNow ci = new RegisterNow(this, tabControl);
                                    AddNewTab(ci);
                                }
                                else
                                {
                                    UserLogin ul = new UserLogin(this, tabControl);
                                    AddNewTab(ul);
                                }

                            }
                        }
                    }
                    #endregion
                }
            }
            else
            {
                CompanyList ci = new CompanyList(this, tabControl);
                AddNewTab(ci);
            }




        }
        public void utilityandreportsdisable()
        {
            partywisediscount.Enabled = false;
            itemMasterPriceChangeToolStripMenuItem.Enabled = false;
            XReportToolStripMenuItem.Enabled = false;
            //  ZReportToolStripMenuItem.Enabled = false;
            stockDetailsToolStripMenuItem.Enabled = false;
            //   billReprintToolStripMenuItem.Enabled = false;
            // dateWiseBillInvoiceToolStripMenuItem.Enabled = false;
            //inwardDetailsToolStripMenuItem.Enabled = false;
            itemProductWiseStockToolStripMenuItem.Enabled = false;
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
            saleRegisterDetailedToolStripMenuItem.Enabled = false;
            purchaseRegisterDetailedToolStripMenuItem.Enabled = false;
            quickPaymentRegisterToolStripMenuItem.Enabled = false;
            quickPaymentRegisterToolStripMenuItem.Enabled = false;
            additionalFieldsToolStripMenuItem.Enabled = false;
            quickReceiptRegisterToolStripMenuItem.Enabled = false;
            customerListToolStripMenuItem.Enabled = false;
            itemPriceListToolStripMenuItem.Enabled = false;
            addPromotionOfferToolStripMenuItem.Enabled = false;
            itemWiseSaleListToolStripMenuItem.Enabled = false;
            pOSListThermalPrintToolStripMenuItem.Enabled = false;

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
                masterToolStripMenuItem.Visible = a;
                SalesToolStripMenuItem.Visible = a;
                PrintToolStripMenuItem.Visible = a;
                UtilityToolStripMenuItem.Visible = a;
                ViewToolStripMenuItem.Visible = a;
                ExitToolStripMenuItem.Visible = a;
                ledgerToolStripMenuItem.Enabled = a;
                itemProductWiseStockToolStripMenuItem.Enabled = a;
                stockDetailsToolStripMenuItem.Enabled = a;
                outstandinganAlysisToolStripMenuItem.Enabled = a;
                trialBalanceToolStripMenuItem.Enabled = a;
                bankEntryRegisterToolStripMenuItem.Enabled = a;
                dayBookToolStripMenuItem.Enabled = a;
                agentCommissionToolStripMenuItem.Enabled = a;
                saleRegisterDetailedToolStripMenuItem.Enabled = a;
                purchaseRegisterDetailedToolStripMenuItem.Enabled = a;
                quickPaymentRegisterToolStripMenuItem.Enabled = a;
                quickPaymentRegisterToolStripMenuItem.Enabled = a;
                additionalFieldsToolStripMenuItem.Enabled = a;
                quickReceiptRegisterToolStripMenuItem.Enabled = a;
                customerListToolStripMenuItem.Enabled = a;
                itemPriceListToolStripMenuItem.Enabled = a;
                addPromotionOfferToolStripMenuItem.Enabled = a;
                itemWiseSaleListToolStripMenuItem.Enabled = a;
                pOSListThermalPrintToolStripMenuItem.Enabled = a;
                aMCInfoToolStripMenuItem.Enabled = a;
                gSTVouchersToolStripMenuItem.Enabled = a;

                accountGroupToolStripMenuItem.Enabled = a;
                toolStripMenuItem12.Enabled = a;
                toolStripMenuItem9.Enabled = a;
                toolStripMenuItem10.Enabled = a;
                toolStripMenuItem11.Enabled = a;
                openingStockEditorToolStripMenuItem.Enabled = a;
                opbalanceeditor.Enabled = a;
                StockAdjustMent.Enabled = a;
                toolStripMenuItem8.Enabled = a;
                toolStripMenuItem7.Enabled = a;
                toolStripMenuItem1.Enabled = a;
                purchaseTypeToolStripMenuItem.Enabled = a;
                toolStripMenuItem13.Enabled = a;
                gSTVouchersToolStripMenuItem.Enabled = a;
                outstandinganAlysisToolStripMenuItem.Enabled = a;
                trialBalanceToolStripMenuItem.Enabled = a;
                ledgerToolStripMenuItem.Enabled = a;
                stockDetailsToolStripMenuItem.Enabled = a;
                gSTReportsToolStripMenuItem.Enabled = a;
                partywisediscount.Enabled = a;
                optionsToolStripMenuItem.Enabled = a;
                printBarcodeLabelsToolStripMenuItem.Enabled = a;
                additionalFieldsToolStripMenuItem.Enabled = a;

                DataTable userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    DataTable bydefault = new DataTable();
                    bydefault = conn.getdataset("Select * from Options");
                    registerNowToolStripMenuItem.Enabled = a;
                    optionsToolStripMenuItem.Enabled = a;
                    if (statusreg == "Reg")
                    {
                        if (userrights.Rows[0]["a"].ToString() == "False" && userrights.Rows[0]["u"].ToString() == "False" && userrights.Rows[0]["d"].ToString() == "False" && userrights.Rows[0]["v"].ToString() == "False" && userrights.Rows[0]["p"].ToString() == "False")
                        {
                            saleToolStripMenuItem.Visible = false;
                            saleToolStripMenuItem.Enabled = false;
                            serialNoReportsToolStripMenuItem.Visible = false;
                            saleRegisterToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            saleToolStripMenuItem.Enabled = a;
                            saleToolStripMenuItem.Visible = true;
                            serialNoReportsToolStripMenuItem.Visible = true;
                            saleRegisterToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[1]["a"].ToString() == "False" && userrights.Rows[1]["u"].ToString() == "False" && userrights.Rows[1]["d"].ToString() == "False" && userrights.Rows[1]["v"].ToString() == "False" && userrights.Rows[1]["p"].ToString() == "False")
                        {
                            toolStripMenuItem3.Visible = false;
                            toolStripMenuItem3.Enabled = false;
                        }
                        else
                        {
                            toolStripMenuItem3.Enabled = a;
                            toolStripMenuItem3.Visible = true;
                        }
                        if (userrights.Rows[2]["a"].ToString() == "False" && userrights.Rows[2]["u"].ToString() == "False" && userrights.Rows[2]["d"].ToString() == "False" && userrights.Rows[2]["v"].ToString() == "False" && userrights.Rows[2]["p"].ToString() == "False")
                        {
                            quickReceiptToolStripMenuItem.Visible = false;
                            quickReceiptToolStripMenuItem.Enabled = false;
                            quickReceiptRegisterToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            quickReceiptToolStripMenuItem.Enabled = a;
                            quickReceiptToolStripMenuItem.Visible = true;
                            quickReceiptRegisterToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[3]["a"].ToString() == "False" && userrights.Rows[3]["u"].ToString() == "False" && userrights.Rows[3]["d"].ToString() == "False" && userrights.Rows[3]["v"].ToString() == "False" && userrights.Rows[3]["p"].ToString() == "False")
                        {
                            purchaseToolStripMenuItem.Visible = false;
                            purchaseToolStripMenuItem.Enabled = false;
                            purchaseRegisterToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            purchaseToolStripMenuItem.Enabled = a;
                            purchaseToolStripMenuItem.Visible = true;
                            purchaseRegisterToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[4]["a"].ToString() == "False" && userrights.Rows[4]["u"].ToString() == "False" && userrights.Rows[4]["d"].ToString() == "False" && userrights.Rows[4]["v"].ToString() == "False" && userrights.Rows[4]["p"].ToString() == "False")
                        {
                            toolStripMenuItem5.Enabled = false;
                            toolStripMenuItem5.Visible = false;
                        }
                        else
                        {
                            toolStripMenuItem5.Enabled = a;
                            toolStripMenuItem5.Visible = true;
                        }
                        if (userrights.Rows[5]["a"].ToString() == "False" && userrights.Rows[5]["u"].ToString() == "False" && userrights.Rows[5]["d"].ToString() == "False" && userrights.Rows[5]["v"].ToString() == "False" && userrights.Rows[5]["p"].ToString() == "False")
                        {
                            paymentToolStripMenuItem.Enabled = false;
                            paymentToolStripMenuItem.Visible = false;
                            quickPaymentRegisterToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            paymentToolStripMenuItem.Enabled = a;
                            paymentToolStripMenuItem.Visible = true;
                            quickPaymentRegisterToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[6]["a"].ToString() == "False" && userrights.Rows[6]["u"].ToString() == "False" && userrights.Rows[6]["d"].ToString() == "False" && userrights.Rows[6]["v"].ToString() == "False" && userrights.Rows[6]["p"].ToString() == "False")
                        {
                            debitNoteToolStripMenuItem.Visible = false;
                            debitNoteToolStripMenuItem.Enabled = false;
                            debitNoteToolStripMenuItem1.Visible = false;
                        }
                        else
                        {
                            debitNoteToolStripMenuItem.Enabled = a;
                            debitNoteToolStripMenuItem.Visible = true;
                            debitNoteToolStripMenuItem1.Visible = true;
                        }
                        if (userrights.Rows[7]["a"].ToString() == "False" && userrights.Rows[7]["u"].ToString() == "False" && userrights.Rows[7]["d"].ToString() == "False" && userrights.Rows[7]["v"].ToString() == "False" && userrights.Rows[7]["p"].ToString() == "False")
                        {
                            creditNoteToolStripMenuItem.Visible = false;
                            creditNoteToolStripMenuItem.Enabled = false;
                            creditNoteToolStripMenuItem1.Visible = false;
                        }
                        else
                        {
                            creditNoteToolStripMenuItem.Enabled = a;
                            creditNoteToolStripMenuItem.Visible = true;
                            creditNoteToolStripMenuItem1.Visible = true;
                        }
                        if (userrights.Rows[8]["a"].ToString() == "False" && userrights.Rows[8]["u"].ToString() == "False" && userrights.Rows[8]["d"].ToString() == "False" && userrights.Rows[8]["v"].ToString() == "False" && userrights.Rows[8]["p"].ToString() == "False")
                        {
                            userToolStripMenuItem.Visible = false;
                            userToolStripMenuItem.Enabled = false;
                            userProfileToolStripMenuItem.Enabled = false;
                            userProfileToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            userToolStripMenuItem.Enabled = a;
                            userProfileToolStripMenuItem.Enabled = a;
                            userToolStripMenuItem.Visible = true;
                            userProfileToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[9]["a"].ToString() == "False" && userrights.Rows[9]["u"].ToString() == "False" && userrights.Rows[9]["d"].ToString() == "False" && userrights.Rows[9]["v"].ToString() == "False" && userrights.Rows[9]["p"].ToString() == "False")
                        {
                            pOSToolStripMenuItem.Enabled = false;
                            pOSToolStripMenuItem.Visible = false;
                            pOSBillListToolStripMenuItem.Visible = false;
                            listOfPOSToolStripMenuItem1.Visible = false;
                            pOSRegisterToolStripMenuItem.Visible = false;
                            pOSListThermalPrintToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            pOSToolStripMenuItem.Enabled = a;
                            pOSToolStripMenuItem.Visible = true;
                            pOSBillListToolStripMenuItem.Visible = true;
                            listOfPOSToolStripMenuItem1.Visible = true;
                            pOSRegisterToolStripMenuItem.Visible = true;
                            pOSListThermalPrintToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[10]["a"].ToString() == "False" && userrights.Rows[10]["u"].ToString() == "False" && userrights.Rows[10]["d"].ToString() == "False" && userrights.Rows[10]["v"].ToString() == "False" && userrights.Rows[10]["p"].ToString() == "False")
                        {
                            itemToolStripMenuItem1.Enabled = false;
                            itemToolStripMenuItem1.Visible = false;
                            itemProductWiseStockToolStripMenuItem.Visible = false;
                            itemPriceListToolStripMenuItem.Visible = false;
                            itemMasterPriceChangeToolStripMenuItem.Visible = false;
                        }
                        else
                        {

                            itemToolStripMenuItem1.Enabled = a;
                            itemToolStripMenuItem1.Visible = true;
                            itemProductWiseStockToolStripMenuItem.Visible = true;
                            itemPriceListToolStripMenuItem.Visible = true;
                            itemMasterPriceChangeToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[11]["a"].ToString() == "False" && userrights.Rows[11]["u"].ToString() == "False" && userrights.Rows[11]["d"].ToString() == "False" && userrights.Rows[11]["v"].ToString() == "False" && userrights.Rows[11]["p"].ToString() == "False")
                        {
                            clientsToolStripMenuItem.Enabled = false;
                            clientsToolStripMenuItem.Visible = false;
                            accountsToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            clientsToolStripMenuItem.Enabled = a;
                            clientsToolStripMenuItem.Visible = true;
                            accountsToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[12]["a"].ToString() == "False" && userrights.Rows[12]["u"].ToString() == "False" && userrights.Rows[12]["d"].ToString() == "False" && userrights.Rows[12]["v"].ToString() == "False" && userrights.Rows[12]["p"].ToString() == "False")
                        {
                            toolStripMenuItem4.Enabled = false;
                            toolStripMenuItem4.Visible = false;
                        }
                        else
                        {
                            toolStripMenuItem4.Enabled = a;
                            toolStripMenuItem4.Visible = true;
                        }
                        if (userrights.Rows[13]["a"].ToString() == "False" && userrights.Rows[13]["u"].ToString() == "False" && userrights.Rows[13]["d"].ToString() == "False" && userrights.Rows[13]["v"].ToString() == "False" && userrights.Rows[13]["p"].ToString() == "False")
                        {
                            saleReturnToolStripMenuItem.Enabled = false;
                            saleReturnToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            saleReturnToolStripMenuItem.Enabled = a;
                            saleReturnToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[14]["a"].ToString() == "False" && userrights.Rows[14]["u"].ToString() == "False" && userrights.Rows[14]["d"].ToString() == "False" && userrights.Rows[14]["v"].ToString() == "False" && userrights.Rows[14]["p"].ToString() == "False")
                        {
                        }
                        else
                        {
                        }
                        if (userrights.Rows[15]["a"].ToString() == "False" && userrights.Rows[15]["u"].ToString() == "False" && userrights.Rows[15]["d"].ToString() == "False" && userrights.Rows[15]["v"].ToString() == "False" && userrights.Rows[15]["p"].ToString() == "False")
                        {
                            toolStripMenuItem6.Enabled = false;
                            toolStripMenuItem6.Visible = false;
                        }
                        else
                        {
                            toolStripMenuItem6.Enabled = a;
                            toolStripMenuItem6.Visible = true;
                        }
                        if (userrights.Rows[16]["a"].ToString() == "False" && userrights.Rows[16]["u"].ToString() == "False" && userrights.Rows[16]["d"].ToString() == "False" && userrights.Rows[16]["v"].ToString() == "False" && userrights.Rows[16]["p"].ToString() == "False")
                        {
                            toolStripMenuItem2.Visible = false;
                            toolStripMenuItem2.Enabled = false;
                        }
                        else
                        {
                            toolStripMenuItem2.Enabled = a;
                            toolStripMenuItem2.Visible = true;
                        }
                        if (userrights.Rows[17]["a"].ToString() == "False" && userrights.Rows[17]["u"].ToString() == "False" && userrights.Rows[17]["d"].ToString() == "False" && userrights.Rows[17]["v"].ToString() == "False" && userrights.Rows[17]["p"].ToString() == "False")
                        {
                        }
                        else
                        {
                        }
                        if (userrights.Rows[18]["a"].ToString() == "False" && userrights.Rows[18]["u"].ToString() == "False" && userrights.Rows[18]["d"].ToString() == "False" && userrights.Rows[18]["v"].ToString() == "False" && userrights.Rows[18]["p"].ToString() == "False")
                        {
                        }
                        else
                        {
                        }
                        if (userrights.Rows[19]["a"].ToString() == "False" && userrights.Rows[19]["u"].ToString() == "False" && userrights.Rows[19]["d"].ToString() == "False" && userrights.Rows[19]["v"].ToString() == "False" && userrights.Rows[19]["p"].ToString() == "False")
                        {
                            if (bydefault.Rows[0]["production"].ToString() == "True")
                            {
                                processMenuItem.Visible = false;
                                productionToolStripMenuItem.Visible = false;
                                finishedGoodsToolStripMenuItem.Visible = false;
                                productionAnalysisToolStripMenuItem.Visible = false;
                            }
                        }
                        else
                        {
                            if (bydefault.Rows[0]["production"].ToString() == "True")
                            {
                                processMenuItem.Visible = a;
                                productionToolStripMenuItem.Visible = a;
                                finishedGoodsToolStripMenuItem.Visible = a;
                                productionAnalysisToolStripMenuItem.Visible = a;
                            }
                        }
                        if (userrights.Rows[20]["a"].ToString() == "False" && userrights.Rows[20]["u"].ToString() == "False" && userrights.Rows[20]["d"].ToString() == "False" && userrights.Rows[20]["v"].ToString() == "False" && userrights.Rows[20]["p"].ToString() == "False")
                        {
                            bankEntryToolStripMenuItem.Enabled = false;
                            bankEntryToolStripMenuItem.Visible = false;
                            bankEntryRegisterToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            bankEntryToolStripMenuItem.Enabled = a;
                            bankEntryToolStripMenuItem.Enabled = true;
                            bankEntryRegisterToolStripMenuItem.Visible = true;
                        }

                        if (userrights.Rows[21]["a"].ToString() == "False" && userrights.Rows[21]["u"].ToString() == "False" && userrights.Rows[21]["d"].ToString() == "False" && userrights.Rows[21]["v"].ToString() == "False" && userrights.Rows[21]["p"].ToString() == "False")
                        {
                            accountGroupToolStripMenuItem.Enabled = false;
                            accountGroupToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            accountGroupToolStripMenuItem.Enabled = a;
                            accountGroupToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[22]["a"].ToString() == "False" && userrights.Rows[22]["u"].ToString() == "False" && userrights.Rows[22]["d"].ToString() == "False" && userrights.Rows[22]["v"].ToString() == "False" && userrights.Rows[22]["p"].ToString() == "False")
                        {
                            toolStripMenuItem12.Enabled = false;
                            toolStripMenuItem12.Visible = false;
                        }
                        else
                        {
                            toolStripMenuItem12.Enabled = a;
                            toolStripMenuItem12.Visible = true;
                        }
                        if (userrights.Rows[23]["a"].ToString() == "False" && userrights.Rows[23]["u"].ToString() == "False" && userrights.Rows[23]["d"].ToString() == "False" && userrights.Rows[23]["v"].ToString() == "False" && userrights.Rows[23]["p"].ToString() == "False")
                        {
                            toolStripMenuItem9.Enabled = false;
                            toolStripMenuItem9.Visible = false;
                        }
                        else
                        {
                            toolStripMenuItem9.Enabled = a;
                            toolStripMenuItem9.Visible = true;
                        }
                        if (userrights.Rows[24]["a"].ToString() == "False" && userrights.Rows[24]["u"].ToString() == "False" && userrights.Rows[24]["d"].ToString() == "False" && userrights.Rows[24]["v"].ToString() == "False" && userrights.Rows[24]["p"].ToString() == "False")
                        {
                            toolStripMenuItem10.Enabled = false;
                            toolStripMenuItem10.Visible = false;
                        }
                        else
                        {
                            toolStripMenuItem10.Enabled = a;
                            toolStripMenuItem10.Visible = true;
                        }
                        if (userrights.Rows[25]["a"].ToString() == "False" && userrights.Rows[25]["u"].ToString() == "False" && userrights.Rows[25]["d"].ToString() == "False" && userrights.Rows[25]["v"].ToString() == "False" && userrights.Rows[25]["p"].ToString() == "False")
                        {
                            toolStripMenuItem11.Enabled = false;
                            toolStripMenuItem11.Visible = false;
                        }
                        else
                        {
                            toolStripMenuItem11.Enabled = a;
                            toolStripMenuItem11.Visible = true;
                        }
                        if (userrights.Rows[26]["a"].ToString() == "False" && userrights.Rows[26]["u"].ToString() == "False" && userrights.Rows[26]["d"].ToString() == "False" && userrights.Rows[26]["v"].ToString() == "False" && userrights.Rows[26]["p"].ToString() == "False")
                        {
                            openingStockEditorToolStripMenuItem.Enabled = false;
                            openingStockEditorToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            openingStockEditorToolStripMenuItem.Enabled = a;
                            openingStockEditorToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[27]["a"].ToString() == "False" && userrights.Rows[27]["u"].ToString() == "False" && userrights.Rows[27]["d"].ToString() == "False" && userrights.Rows[27]["v"].ToString() == "False" && userrights.Rows[27]["p"].ToString() == "False")
                        {
                            opbalanceeditor.Enabled = false;
                            opbalanceeditor.Visible = false;
                        }
                        else
                        {
                            opbalanceeditor.Enabled = a;
                            opbalanceeditor.Visible = true;
                        }
                        if (userrights.Rows[28]["a"].ToString() == "False" && userrights.Rows[28]["u"].ToString() == "False" && userrights.Rows[28]["d"].ToString() == "False" && userrights.Rows[28]["v"].ToString() == "False" && userrights.Rows[28]["p"].ToString() == "False")
                        {
                            StockAdjustMent.Enabled = false;
                            StockAdjustMent.Visible = false;
                        }
                        else
                        {
                            StockAdjustMent.Enabled = a;
                            StockAdjustMent.Visible = true;
                        }
                        if (userrights.Rows[29]["a"].ToString() == "False" && userrights.Rows[29]["u"].ToString() == "False" && userrights.Rows[29]["d"].ToString() == "False" && userrights.Rows[29]["v"].ToString() == "False" && userrights.Rows[29]["p"].ToString() == "False")
                        {
                            toolStripMenuItem8.Enabled = false;
                            toolStripMenuItem8.Visible = false;
                        }
                        else
                        {
                            toolStripMenuItem8.Enabled = a;
                            toolStripMenuItem8.Visible = true;
                        }
                        if (userrights.Rows[30]["a"].ToString() == "False" && userrights.Rows[30]["u"].ToString() == "False" && userrights.Rows[30]["d"].ToString() == "False" && userrights.Rows[30]["v"].ToString() == "False" && userrights.Rows[30]["p"].ToString() == "False")
                        {
                            toolStripMenuItem7.Enabled = false;
                            toolStripMenuItem7.Visible = false;
                        }
                        else
                        {
                            toolStripMenuItem7.Enabled = a;
                            toolStripMenuItem7.Visible = true;
                        }
                        if (userrights.Rows[31]["a"].ToString() == "False" && userrights.Rows[31]["u"].ToString() == "False" && userrights.Rows[31]["d"].ToString() == "False" && userrights.Rows[31]["v"].ToString() == "False" && userrights.Rows[31]["p"].ToString() == "False")
                        {
                            toolStripMenuItem1.Enabled = false;
                            toolStripMenuItem1.Visible = false;
                        }
                        else
                        {
                            toolStripMenuItem1.Enabled = a;
                            toolStripMenuItem1.Visible = true;
                        }
                        if (userrights.Rows[32]["a"].ToString() == "False" && userrights.Rows[32]["u"].ToString() == "False" && userrights.Rows[32]["d"].ToString() == "False" && userrights.Rows[32]["v"].ToString() == "False" && userrights.Rows[32]["p"].ToString() == "False")
                        {
                            purchaseTypeToolStripMenuItem.Enabled = false;
                            purchaseTypeToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            purchaseTypeToolStripMenuItem.Enabled = a;
                            purchaseTypeToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[33]["a"].ToString() == "False" && userrights.Rows[33]["u"].ToString() == "False" && userrights.Rows[33]["d"].ToString() == "False" && userrights.Rows[33]["v"].ToString() == "False" && userrights.Rows[33]["p"].ToString() == "False")
                        {
                            toolStripMenuItem13.Enabled = false;
                            toolStripMenuItem13.Visible = false;
                        }
                        else
                        {
                            toolStripMenuItem13.Enabled = a;
                            toolStripMenuItem13.Visible = true;
                        }
                        if (userrights.Rows[34]["a"].ToString() == "False" && userrights.Rows[34]["u"].ToString() == "False" && userrights.Rows[34]["d"].ToString() == "False" && userrights.Rows[34]["v"].ToString() == "False" && userrights.Rows[34]["p"].ToString() == "False")
                        {
                            gSTVouchersToolStripMenuItem.Enabled = false;
                            gSTVouchersToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            gSTVouchersToolStripMenuItem.Enabled = a;
                            gSTVouchersToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[37]["a"].ToString() == "False" && userrights.Rows[37]["u"].ToString() == "False" && userrights.Rows[37]["d"].ToString() == "False" && userrights.Rows[37]["v"].ToString() == "False" && userrights.Rows[37]["p"].ToString() == "False")
                        {
                            outstandinganAlysisToolStripMenuItem.Enabled = false;
                            outstandinganAlysisToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            outstandinganAlysisToolStripMenuItem.Enabled = a;
                            outstandinganAlysisToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[38]["a"].ToString() == "False" && userrights.Rows[38]["u"].ToString() == "False" && userrights.Rows[38]["d"].ToString() == "False" && userrights.Rows[38]["v"].ToString() == "False" && userrights.Rows[38]["p"].ToString() == "False")
                        {
                            trialBalanceToolStripMenuItem.Enabled = false;
                            trialBalanceToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            trialBalanceToolStripMenuItem.Enabled = a;
                            trialBalanceToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[39]["a"].ToString() == "False" && userrights.Rows[39]["u"].ToString() == "False" && userrights.Rows[39]["d"].ToString() == "False" && userrights.Rows[39]["v"].ToString() == "False" && userrights.Rows[39]["p"].ToString() == "False")
                        {
                            ledgerToolStripMenuItem.Enabled = false;
                            ledgerToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            ledgerToolStripMenuItem.Enabled = a;
                            ledgerToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[40]["a"].ToString() == "False" && userrights.Rows[40]["u"].ToString() == "False" && userrights.Rows[40]["d"].ToString() == "False" && userrights.Rows[40]["v"].ToString() == "False" && userrights.Rows[40]["p"].ToString() == "False")
                        {
                            stockDetailsToolStripMenuItem.Enabled = false;
                            stockDetailsToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            stockDetailsToolStripMenuItem.Enabled = a;
                            stockDetailsToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[41]["a"].ToString() == "False" && userrights.Rows[41]["u"].ToString() == "False" && userrights.Rows[41]["d"].ToString() == "False" && userrights.Rows[41]["v"].ToString() == "False" && userrights.Rows[41]["p"].ToString() == "False")
                        {
                            gSTReportsToolStripMenuItem.Enabled = false;
                            gSTReportsToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            gSTReportsToolStripMenuItem.Enabled = a;
                            gSTReportsToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[42]["a"].ToString() == "False" && userrights.Rows[42]["u"].ToString() == "False" && userrights.Rows[42]["d"].ToString() == "False" && userrights.Rows[42]["v"].ToString() == "False" && userrights.Rows[42]["p"].ToString() == "False")
                        {
                            partywisediscount.Enabled = false;
                            partywisediscount.Visible = false;
                        }
                        else
                        {
                            partywisediscount.Enabled = a;
                            partywisediscount.Visible = true;
                        }
                        if (userrights.Rows[43]["a"].ToString() == "False" && userrights.Rows[43]["u"].ToString() == "False" && userrights.Rows[43]["d"].ToString() == "False" && userrights.Rows[43]["v"].ToString() == "False" && userrights.Rows[43]["p"].ToString() == "False")
                        {
                            optionsToolStripMenuItem.Enabled = false;
                            optionsToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            optionsToolStripMenuItem.Enabled = a;
                            optionsToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[44]["a"].ToString() == "False" && userrights.Rows[44]["u"].ToString() == "False" && userrights.Rows[44]["d"].ToString() == "False" && userrights.Rows[44]["v"].ToString() == "False" && userrights.Rows[44]["p"].ToString() == "False")
                        {
                            printBarcodeLabelsToolStripMenuItem.Enabled = false;
                            printBarcodeLabelsToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            printBarcodeLabelsToolStripMenuItem.Enabled = a;
                            printBarcodeLabelsToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[45]["a"].ToString() == "False" && userrights.Rows[45]["u"].ToString() == "False" && userrights.Rows[45]["d"].ToString() == "False" && userrights.Rows[45]["v"].ToString() == "False" && userrights.Rows[45]["p"].ToString() == "False")
                        {
                            additionalFieldsToolStripMenuItem.Enabled = false;
                            additionalFieldsToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            additionalFieldsToolStripMenuItem.Enabled = a;
                            additionalFieldsToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[46]["a"].ToString() == "False" && userrights.Rows[46]["u"].ToString() == "False" && userrights.Rows[46]["d"].ToString() == "False" && userrights.Rows[46]["v"].ToString() == "False" && userrights.Rows[46]["p"].ToString() == "False")
                        {
                            salePurchaseDetailsToolStripMenuItem.Enabled = false;
                            salePurchaseDetailsToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            salePurchaseDetailsToolStripMenuItem.Enabled = a;
                            salePurchaseDetailsToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[47]["a"].ToString() == "False" && userrights.Rows[47]["u"].ToString() == "False" && userrights.Rows[47]["d"].ToString() == "False" && userrights.Rows[47]["v"].ToString() == "False" && userrights.Rows[47]["p"].ToString() == "False")
                        {
                            stockInToolStripMenuItem.Enabled = false;
                            stockInToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            stockInToolStripMenuItem.Enabled = a;
                            stockInToolStripMenuItem.Visible = true;
                        }
                        if (userrights.Rows[48]["a"].ToString() == "False" && userrights.Rows[48]["u"].ToString() == "False" && userrights.Rows[48]["d"].ToString() == "False" && userrights.Rows[48]["v"].ToString() == "False" && userrights.Rows[48]["p"].ToString() == "False")
                        {
                            stockOutToolStripMenuItem.Enabled = false;
                            stockOutToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            stockOutToolStripMenuItem.Enabled = a;
                            stockOutToolStripMenuItem.Visible = true;
                        }
                        
                    }
                    else
                    {
                        gSTVouchersToolStripMenuItem.Enabled = a;
                        registerNowToolStripMenuItem.Enabled = a;
                        optionsToolStripMenuItem.Enabled = a;
                        ledgerToolStripMenuItem.Enabled = a;
                        itemProductWiseStockToolStripMenuItem.Enabled = a;
                        stockDetailsToolStripMenuItem.Enabled = a;
                        outstandinganAlysisToolStripMenuItem.Enabled = a;
                        trialBalanceToolStripMenuItem.Enabled = a;
                        bankEntryRegisterToolStripMenuItem.Enabled = a;
                        dayBookToolStripMenuItem.Enabled = a;
                        agentCommissionToolStripMenuItem.Enabled = a;
                        saleRegisterDetailedToolStripMenuItem.Enabled = a;
                        purchaseRegisterDetailedToolStripMenuItem.Enabled = a;
                        quickPaymentRegisterToolStripMenuItem.Enabled = a;
                        quickPaymentRegisterToolStripMenuItem.Enabled = a;
                        additionalFieldsToolStripMenuItem.Enabled = a;
                        quickReceiptRegisterToolStripMenuItem.Enabled = a;
                        customerListToolStripMenuItem.Enabled = a;
                        itemPriceListToolStripMenuItem.Enabled = a;
                        addPromotionOfferToolStripMenuItem.Enabled = a;
                        itemWiseSaleListToolStripMenuItem.Enabled = a;
                        pOSListThermalPrintToolStripMenuItem.Enabled = a;
                        aMCInfoToolStripMenuItem.Enabled = a;
                        pOSToolStripMenuItem.Enabled = a;
                        quickReceiptToolStripMenuItem.Enabled = a;
                        paymentToolStripMenuItem.Enabled = a;
                        purchaseToolStripMenuItem.Enabled = a;

                        clientsToolStripMenuItem.Enabled = a;
                        itemToolStripMenuItem1.Enabled = a;
                        masterToolStripMenuItem.Visible = a;
                        SalesToolStripMenuItem.Visible = a;
                        PrintToolStripMenuItem.Visible = a;
                        UtilityToolStripMenuItem.Visible = a;
                        ViewToolStripMenuItem.Visible = a;
                        ExitToolStripMenuItem.Visible = a;
                        bankEntryToolStripMenuItem.Enabled = a;
                        bankEntryToolStripMenuItem.Enabled = a;
                        toolStripMenuItem2.Enabled = a;
                        if (bydefault.Rows[0]["production"].ToString() == "True")
                        {
                            processMenuItem.Visible = a;
                            productionToolStripMenuItem.Visible = a;
                            finishedGoodsToolStripMenuItem.Visible = a;
                            productionAnalysisToolStripMenuItem.Visible = a;

                        }

                        accountGroupToolStripMenuItem.Enabled = a;
                        toolStripMenuItem12.Enabled = a;
                        toolStripMenuItem9.Enabled = a;
                        toolStripMenuItem10.Enabled = a;
                        toolStripMenuItem11.Enabled = a;
                        openingStockEditorToolStripMenuItem.Enabled = a;
                        opbalanceeditor.Enabled = a;
                        StockAdjustMent.Enabled = a;
                        toolStripMenuItem8.Enabled = a;
                        toolStripMenuItem7.Enabled = a;
                        toolStripMenuItem1.Enabled = a;
                        purchaseTypeToolStripMenuItem.Enabled = a;
                        toolStripMenuItem13.Enabled = a;
                        gSTVouchersToolStripMenuItem.Enabled = a;
                        outstandinganAlysisToolStripMenuItem.Enabled = a;
                        trialBalanceToolStripMenuItem.Enabled = a;
                        ledgerToolStripMenuItem.Enabled = a;
                        stockDetailsToolStripMenuItem.Enabled = a;
                        gSTReportsToolStripMenuItem.Enabled = a;
                        partywisediscount.Enabled = a;
                        optionsToolStripMenuItem.Enabled = a;
                        printBarcodeLabelsToolStripMenuItem.Enabled = a;
                        additionalFieldsToolStripMenuItem.Enabled = a;

                    }
                    //pOSToolStripMenuItem.Enabled = a;
                    // quickReceiptToolStripMenuItem.Enabled = a;
                    //paymentToolStripMenuItem.Enabled = a;
                    // purchaseToolStripMenuItem.Enabled = a;

                    //clientsToolStripMenuItem.Enabled = a;
                    //itemToolStripMenuItem1.Enabled = a;

                    //    if (userrights.Rows[0]["a"].ToString() == "True")
                    //  {

                    // }
                    // else
                    // {
                    //   saleToolStripMenuItem.Enabled = false;
                    // }

                    // bankEntryToolStripMenuItem.Enabled = a;


                    panel1.Left = this.Width - 218;
                    bydefault = conn.getdataset("Select * from Options");
                    if (bydefault.Rows[0]["showsidebox"].ToString() == "False")
                    {
                        //By Default Close
                        panel1.Visible = a;
                        panel1.Left = this.Width - 60;
                    }
                    else
                    {
                        //By Default Open
                        panel1.Visible = a;
                        panel1.Width = 198;
                    }
                    if (bydefault.Rows[0]["natureofbusiness"].ToString() == "True")
                    {
                        callManagementToolStripMenuItem1.Visible = a;
                        callToolStripMenuItem.Visible = a;

                    }
                    if (bydefault.Rows[0]["issync"].ToString() == "True")
                    {

                        syncronizationToolStripMenuItem.Visible = a;
                    }
                }
                else
                {
                    #region
                    gSTVouchersToolStripMenuItem.Enabled = a;
                    registerNowToolStripMenuItem.Enabled = a;
                    optionsToolStripMenuItem.Enabled = a;
                    if (statusreg == "Reg")
                    {
                        ledgerToolStripMenuItem.Enabled = a;
                        itemProductWiseStockToolStripMenuItem.Enabled = a;
                        stockDetailsToolStripMenuItem.Enabled = a;
                        outstandinganAlysisToolStripMenuItem.Enabled = a;
                        trialBalanceToolStripMenuItem.Enabled = a;
                        bankEntryRegisterToolStripMenuItem.Enabled = a;
                        dayBookToolStripMenuItem.Enabled = a;
                        agentCommissionToolStripMenuItem.Enabled = a;
                        saleRegisterDetailedToolStripMenuItem.Enabled = a;
                        purchaseRegisterDetailedToolStripMenuItem.Enabled = a;
                        quickPaymentRegisterToolStripMenuItem.Enabled = a;
                        quickPaymentRegisterToolStripMenuItem.Enabled = a;
                        additionalFieldsToolStripMenuItem.Enabled = a;
                        quickReceiptRegisterToolStripMenuItem.Enabled = a;
                        customerListToolStripMenuItem.Enabled = a;
                        itemPriceListToolStripMenuItem.Enabled = a;
                        addPromotionOfferToolStripMenuItem.Enabled = a;
                        itemWiseSaleListToolStripMenuItem.Enabled = a;
                        pOSListThermalPrintToolStripMenuItem.Enabled = a;
                        aMCInfoToolStripMenuItem.Enabled = a;
                    }
                    pOSToolStripMenuItem.Enabled = a;
                    quickReceiptToolStripMenuItem.Enabled = a;
                    paymentToolStripMenuItem.Enabled = a;
                    purchaseToolStripMenuItem.Enabled = a;

                    clientsToolStripMenuItem.Enabled = a;
                    itemToolStripMenuItem1.Enabled = a;
                    masterToolStripMenuItem.Visible = a;
                    //    if (userrights.Rows[0]["a"].ToString() == "True")
                    //  {

                    // }
                    // else
                    // {
                    //   saleToolStripMenuItem.Enabled = false;
                    // }
                    SalesToolStripMenuItem.Visible = a;
                    PrintToolStripMenuItem.Visible = a;
                    UtilityToolStripMenuItem.Visible = a;
                    ViewToolStripMenuItem.Visible = a;
                    ExitToolStripMenuItem.Visible = a;
                    bankEntryToolStripMenuItem.Enabled = a;

                    DataTable bydefault = new DataTable();
                    panel1.Left = this.Width - 218;
                    bydefault = conn.getdataset("Select * from Options");
                    if (bydefault.Rows[0]["showsidebox"].ToString() == "False")
                    {
                        //By Default Close
                        panel1.Visible = a;
                        panel1.Left = this.Width - 60;
                    }
                    else
                    {
                        //By Default Open
                        panel1.Visible = a;
                        panel1.Width = 198;
                    }
                    if (bydefault.Rows[0]["natureofbusiness"].ToString() == "True")
                    {
                        callManagementToolStripMenuItem1.Visible = a;
                        callToolStripMenuItem.Visible = a;

                    }
                    if (bydefault.Rows[0]["production"].ToString() == "True")
                    {
                        processMenuItem.Visible = a;
                        productionToolStripMenuItem.Visible = a;
                        finishedGoodsToolStripMenuItem.Visible = a;
                        productionAnalysisToolStripMenuItem.Visible = a;

                    }
                    if (bydefault.Rows[0]["issync"].ToString() == "True")
                    {

                        syncronizationToolStripMenuItem.Visible = a;
                    }
                    #endregion
                }
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
                DataTable tax = conn.getdataset("select Taxation from Options");
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
        DataTable options = new DataTable();
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
            ClientRegistration frm = new ClientRegistration(this, tabControl);
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
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
            options = conn.getdataset("select * from options");
            LogGenerator.Info("Company Logout CompanyID=" + Master.companyId);
            LogGenerator.Info("User Logout UserID=" + Master.userid);
            if (options.Rows[0]["userlog"].ToString() == "True")
            {
                conn.execute("INSERT INTO [dbo].[USerLog]([userid],[loginpalce],[Status],[isactive],[datetime])VALUES('" + Master.userid + "','" + "Company" + "','" + "Logout" + "','" + "1" + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
            }
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
            DateWiseReport frm = new DateWiseReport(this, tabControl);
            AddNewTab(frm);
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
            StockReport frm = new StockReport(this, tabControl);
            AddNewTab(frm);
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
            string[] strfinalarray = new string[5] { "S", "D", "Sale", "S", "" };
            DateWiseReport frm = new DateWiseReport(this, tabControl, strfinalarray);
            AddNewTab(frm);
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

        public void RemoveCurrentTab1(string previouscontroal, string settext)
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
            Group frm = new Group();
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientRegistration frm = new ClientRegistration(this, tabControl);
            AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void ledgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ledger frm = new Ledger(this, tabControl);
            AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void quickReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QReceipt frm = new QReceipt(this, tabControl);
            AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void paymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QPayment frm = new QPayment(this, tabControl);
            AddNewTab(frm);
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
            POSBillList frm = new POSBillList(this, tabControl);
            AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void pOSItemListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            POSItemList frm = new POSItemList(this, tabControl);
            AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void pOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            options = conn.getdataset("select * from options");
            LogGenerator.Info("POS User LogIN UserID=" + Master.userid);
            if (options.Rows[0]["userlog"].ToString() == "True")
            {
                conn.execute("INSERT INTO [dbo].[USerLog]([userid],[loginpalce],[Status],[isactive],[datetime])VALUES('" + Master.userid + "','" + "POS" + "','" + "Login" + "','" + "1" + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
            }
            DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='POS' and setdefault=1");
            POSNEW bd = new POSNEW();
            DefaultPOS p = new DefaultPOS(this, tabControl);
            if (dt1.Rows[0]["formname"].ToString() == p.Text)
            {
                AddNewTab(p);
                //bd.MdiParent = this.MdiParent;
                //bd.StartPosition = FormStartPosition.CenterScreen;
                //bd.Show();
            }
            else
            {
                bd.Size = new Size(this.Height, this.Width);
                bd.StartPosition = FormStartPosition.CenterScreen;
                bd.ShowDialog();

            }
            //    DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='POS' and setdefault=1");
            ////    POS bd = new POS(this, tabControl);
            //    DefaultPOS p = new DefaultPOS(this, tabControl);
            //    if (dt1.Rows[0]["formname"].ToString() == p.Text)
            //    {
            //        AddNewTab(p);
            //        //bd.MdiParent = this.MdiParent;
            //        //bd.StartPosition = FormStartPosition.CenterScreen;
            //        //bd.Show();
            //    }
            //    //else if (dt1.Rows[0]["formname"].ToString() == bd.Text)
            //    //{
            //    //    AddNewTab(bd);
            //    //    //p.MdiParent = this.MdiParent;
            //    //    //p.StartPosition = FormStartPosition.CenterScreen;
            //    //    //p.Show();
            //    //}

        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strfinalarray = new string[5] { "P", "C", "Purchase", "P", "" };
            //DateWisePurchaseReport frm = new DateWisePurchaseReport("p",this,tabControl);
            //AddNewTab(frm);
            DateWiseReport frm = new DateWiseReport(this, tabControl, strfinalarray);
            AddNewTab(frm);
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
                DateTime dtcurrentdate = Convert.ToDateTime(DateTime.Now.ToString(), en);
                DateTime dtexpdate = dtdecr;
                if (dtexpdate <= dtcurrentdate)
                {
                    enablemenu(false);
                    disablecompany(false);
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
                        // utilityandreportsdisable();
                        string t1 = dtreg.Rows[0]["d18"].ToString();
                        Decryptinstoaldata(t1);
                        DateTime dtdecrtotal = DateTime.ParseExact(Decryptinstoaldate1, "dd-MM-yyyy", en);
                        string d1 = DateTime.Now.ToString("dd-MM-yyyy");
                        //DateTime dtdecrtotal = Convert.ToDateTime(Decryptinstoaldate1);
                        // string d1 = DateTime.Now.ToString("MM-dd-yyyy");
                        DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString(), en);
                        DateTime dtexpdate1 = dtdecrtotal;
                        //DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"));
                        //DateTime dtexpdate1 = Convert.ToDateTime(dtdecrtotal.ToString("dd/MM/yyyy"));
                        //dtexpdate1 = dtexpdate1.AddDays(7);
                        if (dtexpdate1 <= dtcurrentdate1)
                        {
                            enablemenu(false);
                            disablecompany(false);
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
        public static string Decryptdatereg(string cipherText)
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
                    Decryptdateregstr = Encoding.Unicode.GetString(ms.ToArray());
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
                DateTime dtcurrentdate = Convert.ToDateTime(DateTime.Now.ToString(), en);
                DateTime dtexpdate = dtdecr;
                if (dtexpdate <= dtcurrentdate)
                {
                    enablemenu(false);
                    disablecompany(false);
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
                        //utilityandreportsdisable();
                        string t1 = dtreg.Rows[0]["d18"].ToString();
                        Decryptinstoaldata(t1);
                        DateTime dtdecrtotal = DateTime.ParseExact(Decryptinstoaldate1, "dd-MM-yyyy", en);
                        string d1 = DateTime.Now.ToString("dd-MM-yyyy");
                        //DateTime dtdecrtotal = Convert.ToDateTime(Decryptinstoaldate1);
                        // string d1 = DateTime.Now.ToString("MM-dd-yyyy");
                        DateTime dtcurrentdate1 = Convert.ToDateTime(DateTime.Now.ToString(), en);
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
            UserList frm = new UserList(this, tabControl);
            AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void userProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserRights frm = new UserRights(this, tabControl);
            AddNewTab(frm);
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
            string[] strfinalarray = new string[5] { "SR", "C", "SaleReturn", "SR", "" };
            DateWiseReport frm = new DateWiseReport(this, tabControl, strfinalarray);
            AddNewTab(frm);
            //DatewiseSaleReturn frm = new DatewiseSaleReturn(this, tabControl);
            //AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }
        public static string currency = string.Empty;
        public string CurrentUserid="1";
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
            this.Text = "Total Business ERP[" + statusreg + "] #190814 [" + companydt.Rows[0][2].ToString() + "] [FY = " + fy + " to " + to + "] User=" + p;
            lblcurrency.Text = "[" + companydt.Rows[0][30].ToString() + "]";
            currency = lblcurrency.Text;
            DataTable Userid = conn.getdataset("Select * from Userinfo where UserName='"+p+"'");
            CurrentUserid = Userid.Rows[0]["Userid"].ToString();
        }

        private void pOSBillListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openingStockEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpStock frm = new OpStock(this, tabControl);
            AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }



        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeCompanyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LogGenerator.Info("Company Logout CompanyID=" + Master.companyId);
            LogGenerator.Info("User Logout UserID=" + Master.userid);
            options = conn.getdataset("select * from options");
            if (options.Rows[0]["userlog"].ToString() == "True")
            {
                conn.execute("INSERT INTO [dbo].[USerLog]([userid],[loginpalce],[Status],[isactive],[datetime])VALUES('" + Master.userid + "','" + "Company" + "','" + "Logout" + "','" + "1" + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
            }
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
            PurchaseTypeMaster frm = new PurchaseTypeMaster(this, tabControl);
            AddNewTab(frm);
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
            ItemMasterPriceChange frm = new ItemMasterPriceChange(this, tabControl);
            AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void cashBankBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashBook frm = new CashBook(this, tabControl);
            AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void partywisediscount_Click(object sender, EventArgs e)
        {
            PartyGroupwiseDiscount frm = new PartyGroupwiseDiscount(this, tabControl);
            AddNewTab(frm);
            //frm.MdiParent = this;
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
        }

        private void SalesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void accountGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Group frm = new Group(this, tabControl);
            AddNewTab(frm);
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
            ListPOS frm = new ListPOS(this, tabControl);
            AddNewTab(frm);
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
            Saletypemaster frm = new Saletypemaster(this, tabControl);
            AddNewTab(frm);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string[] strfinalarray = new string[5] { "PR", "D", "PurchaseReturn", "PR", "" };
            DateWiseReport frm = new DateWiseReport(this, tabControl, strfinalarray);
            AddNewTab(frm);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            string[] strfinalarray = new string[5] { "SO", "D", "Sale Order", "SO", "" };
            SaleOrderList sol = new SaleOrderList(this, tabControl, strfinalarray);
            AddNewTab(sol);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string[] strfinalarray = new string[5] { "SC", "D", "Sale Challan", "SC", "" };
            SaleOrderList sol = new SaleOrderList(this, tabControl, strfinalarray);
            AddNewTab(sol);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            string[] strfinalarray = new string[5] { "PO", "C", "Purchase Order", "PO", "" };
            SaleOrderList sol = new SaleOrderList(this, tabControl, strfinalarray);
            AddNewTab(sol);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            string[] strfinalarray = new string[5] { "PC", "C", "Purchase Challan", "PC", "" };
            SaleOrderList sol = new SaleOrderList(this, tabControl, strfinalarray);
            AddNewTab(sol);
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

        private void aMCInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AMC rn = new AMC(this, tabControl);
            AddNewTab(rn);
        }

        private void serialNoTrackingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialnotracking rn = new serialnotracking(this, tabControl);
            AddNewTab(rn);
        }

        private void gSTR2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GSTR2 rn = new GSTR2(this, tabControl);
            AddNewTab(rn);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            ChargesHead rn = new ChargesHead(this, tabControl);
            AddNewTab(rn);
        }

        private void pOSRegisterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            POSRegister rn = new POSRegister(this, tabControl);
            AddNewTab(rn);
        }

        private void billReprintToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gSTR3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GSTR_3B rn = new GSTR_3B(this, tabControl);
            AddNewTab(rn);
        }

        private void btnItemPanel_Click(object sender, EventArgs e)
        {
            // btnItemPanel.BackColor = Color.LightYellow;
            // btnItemPanel.ForeColor = Color.Red;
            Itemmaster frm = new Itemmaster(this, tabControl);
            AddNewTab(frm);

        }

        private void btnAccountpanel_Click(object sender, EventArgs e)
        {
            ClientRegistration frm = new ClientRegistration(this, tabControl);
            AddNewTab(frm);
        }

        private void btnSalePanel_Click(object sender, EventArgs e)
        {
            string[] strfinalarray = new string[5] { "S", "D", "Sale", "S", "" };
            DateWiseReport frm = new DateWiseReport(this, tabControl, strfinalarray);
            AddNewTab(frm);
        }

        private void btnPurchasePanel_Click(object sender, EventArgs e)
        {
            string[] strfinalarray = new string[5] { "P", "C", "Purchase", "P", "" };
            DateWiseReport frm = new DateWiseReport(this, tabControl, strfinalarray);
            AddNewTab(frm);
        }

        private void btnQuickPaymentPanel_Click(object sender, EventArgs e)
        {
            QPayment frm = new QPayment(this, tabControl);
            AddNewTab(frm);
        }

        private void btnQuickReceiptPanel_Click(object sender, EventArgs e)
        {
            QReceipt frm = new QReceipt(this, tabControl);
            AddNewTab(frm);
        }

        private void btnPOSPanel_Click(object sender, EventArgs e)
        {
            DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='POS' and setdefault=1");
            //POS bd = new POS(this, tabControl);
            DefaultPOS p = new DefaultPOS(this, tabControl);
            if (dt1.Rows[0]["formname"].ToString() == p.Text)
            {
                AddNewTab(p);
                //bd.MdiParent = this.MdiParent;
                //bd.StartPosition = FormStartPosition.CenterScreen;
                //bd.Show();
            }
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
            Ledger frm = new Ledger(this, tabControl);
            AddNewTab(frm);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ItemWiseStock frm = new ItemWiseStock(this, tabControl);
            AddNewTab(frm);
        }

        private void PnlButton_Click(object sender, EventArgs e)
        {
            if (panel1.Width == 198)
            {

                panel1.Width = 65;

                panel1.Left = this.Width - 60;

                panel1.Height = this.Height;
            }
            else
            {

                panel1.Width = 198;

                panel1.Left = this.Width - 218;
                //647
                panel1.Height = this.Height;
            }
            //if (panel1.Width == 178)
            //{

            //    panel1.Width = 55;

            //    panel1.Left = this.Width - 60;

            //    panel1.Height = this.Height;
            //}
            //else
            //{

            //    panel1.Width = 178;

            //    panel1.Left = this.Width - 183;
            //    //647
            //    panel1.Height = this.Height;
            //}
        }
        public void RemoveBorderOfButton()
        {
            btnItemPanel.FlatAppearance.BorderSize = 0;
            btnAccountpanel.FlatAppearance.BorderSize = 0;
            btnSalePanel.FlatAppearance.BorderSize = 0;
            btnPurchasePanel.FlatAppearance.BorderSize = 0;
            btnQuickPaymentPanel.FlatAppearance.BorderSize = 0;
            btnQuickReceiptPanel.FlatAppearance.BorderSize = 0;
            btnPOSPanel.FlatAppearance.BorderSize = 0;
            btnLedgerPanel.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.BorderSize = 0;
            btnCashBook.FlatAppearance.BorderSize = 0;
            btnSaleRegister.FlatAppearance.BorderSize = 0;
            btnoutstanding.FlatAppearance.BorderSize = 0;
            btnDayBook.FlatAppearance.BorderSize = 0;
            btnPurchaseRegister.FlatAppearance.BorderSize = 0;
            btnSaleOrder.FlatAppearance.BorderSize = 0;
            btnPurchaseOrder.FlatAppearance.BorderSize = 0;
            btnBankEntry.FlatAppearance.BorderSize = 0;
            btnPOSReg.FlatAppearance.BorderSize = 0;
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
            //btnItemPanel.BackColor = Color.LightYellow;
            //btnItemPanel.ForeColor = Color.Red;
        }

        private void btnItemPanel_MouseLeave(object sender, EventArgs e)
        {
            //btnItemPanel.BackColor = Color.DimGray;
            //btnItemPanel.ForeColor = Color.White;
        }

        private void btnItemPanel_Enter(object sender, EventArgs e)
        {
            //btnItemPanel.BackColor = Color.LightYellow;
            //btnItemPanel.ForeColor = Color.Red;
        }

        private void btnAccountpanel_MouseEnter(object sender, EventArgs e)
        {
            //btnAccountpanel.BackColor = Color.LightYellow;
            //btnAccountpanel.ForeColor = Color.Red;
        }

        private void btnAccountpanel_MouseLeave(object sender, EventArgs e)
        {
            //btnAccountpanel.BackColor = Color.DimGray;
            //btnAccountpanel.ForeColor = Color.White;
        }

        private void btnSalePanel_MouseEnter(object sender, EventArgs e)
        {
            //btnSalePanel.BackColor = Color.LightYellow;
            //btnSalePanel.ForeColor = Color.Red;
        }

        private void btnSalePanel_MouseLeave(object sender, EventArgs e)
        {
            //btnSalePanel.BackColor = Color.DimGray;
            //btnSalePanel.ForeColor = Color.White;
        }

        private void btnItemPanel_MouseClick(object sender, MouseEventArgs e)
        {
            //btnItemPanel.BackColor = Color.LightYellow;
            //btnItemPanel.ForeColor = Color.Red;
        }

        private void btnPurchasePanel_MouseEnter(object sender, EventArgs e)
        {
            //btnPurchasePanel.BackColor = Color.LightYellow;
            //btnPurchasePanel.ForeColor = Color.Red;
        }

        private void btnPurchasePanel_MouseLeave(object sender, EventArgs e)
        {
            //btnPurchasePanel.BackColor = Color.DimGray;
            //btnPurchasePanel.ForeColor = Color.White;
        }

        private void btnQuickPaymentPanel_MouseEnter(object sender, EventArgs e)
        {
            //btnQuickPaymentPanel.BackColor = Color.LightYellow;
            //btnQuickPaymentPanel.ForeColor = Color.Red;
        }

        private void btnQuickPaymentPanel_MouseLeave(object sender, EventArgs e)
        {
            //btnQuickPaymentPanel.BackColor = Color.DimGray;
            //btnQuickPaymentPanel.ForeColor = Color.White;
        }

        private void btnQuickReceiptPanel_MouseEnter(object sender, EventArgs e)
        {
            //btnQuickReceiptPanel.BackColor = Color.LightYellow;
            //btnQuickReceiptPanel.ForeColor = Color.Red;
        }

        private void btnQuickReceiptPanel_MouseLeave(object sender, EventArgs e)
        {
            //btnQuickReceiptPanel.BackColor = Color.DimGray;
            //btnQuickReceiptPanel.ForeColor = Color.White;
        }

        private void btnPOSPanel_MouseEnter(object sender, EventArgs e)
        {
            //btnPOSPanel.BackColor = Color.LightYellow;
            //btnPOSPanel.ForeColor = Color.Red;
        }

        private void btnLedgerPanel_MouseEnter(object sender, EventArgs e)
        {
            //btnLedgerPanel.BackColor = Color.LightYellow;
            //btnLedgerPanel.ForeColor = Color.Red;
        }

        private void btnPOSPanel_MouseLeave(object sender, EventArgs e)
        {
            //btnPOSPanel.BackColor = Color.DimGray;
            //btnPOSPanel.ForeColor = Color.White;
        }

        private void btnLedgerPanel_MouseLeave(object sender, EventArgs e)
        {
            //btnLedgerPanel.BackColor = Color.DimGray;
            //btnLedgerPanel.ForeColor = Color.White;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            //button1.BackColor = Color.LightYellow;
            //button1.ForeColor = Color.Red;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            //button1.BackColor = Color.DimGray;
            //button1.ForeColor = Color.White;
        }

        private void outstandinganAlysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            outstandinganalysis frm = new outstandinganalysis(this, tabControl);
            AddNewTab(frm);
        }

        private void printBarcodeLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintBarcode frm = new PrintBarcode(this, tabControl);
            AddNewTab(frm);
        }

        private void trialBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrialBalance tb = new TrialBalance(this, tabControl);
            AddNewTab(tb);
        }

        private void bankEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BankEntry be = new BankEntry(this, tabControl);
            AddNewTab(be);
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            CompanyMaster frm = new CompanyMaster(this, tabControl);
            AddNewTab(frm);
        }

        private void debitNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] debitorcredit = new string[5] { "D", "", "", "", "" };
            DebitandCreditNote frm = new DebitandCreditNote(this, tabControl, debitorcredit);
            AddNewTab(frm);
        }

        private void creditNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] debitorcredit = new string[5] { "C", "", "", "", "" };
            DebitandCreditNote frm = new DebitandCreditNote(this, tabControl, debitorcredit);
            AddNewTab(frm);
        }

        private void bankEntryRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BankEntryReport be = new BankEntryReport(this, tabControl);
            AddNewTab(be);
        }

        private void debitNoteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DebitNoteReport be = new DebitNoteReport(this, tabControl);
            AddNewTab(be);
        }

        private void creditNoteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreditNoteReport be = new CreditNoteReport(this, tabControl);
            AddNewTab(be);
        }

        private void dayBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DayBook be = new DayBook(this, tabControl);
            AddNewTab(be);
        }

        private void agentCommissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AgentCommissionReport be = new AgentCommissionReport(this, tabControl);
            AddNewTab(be);
        }

        private void versionControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update u = new Update(this, tabControl);
            AddNewTab(u);
        }

        private void complainReceiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComplainReceiveList u = new ComplainReceiveList(this, tabControl);
            AddNewTab(u);
        }

        private void sendToCompanyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendToCompanyList u = new SendToCompanyList(this, tabControl);
            AddNewTab(u);
        }

        private void receiveFromCompanyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiveFromCompanyList u = new ReceiveFromCompanyList(this, tabControl);
            AddNewTab(u);
        }

        private void toCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendToCustomerList u = new SendToCustomerList(this, tabControl);
            AddNewTab(u);
        }

        private void serialNoTrackingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            serialnotrackingreport u = new serialnotrackingreport(this, tabControl);
            AddNewTab(u);
        }

        private void callManagementRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callmanagementregister u = new callmanagementregister(this, tabControl);
            AddNewTab(u);
        }

        private void saleRegisterDetailedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saleregisterdetailed u = new saleregisterdetailed(this, tabControl);
            AddNewTab(u);
        }

        private void purchaseRegisterDetailedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            purchaseregisterdetailed u = new purchaseregisterdetailed(this, tabControl);
            AddNewTab(u);
        }

        private void quickPaymentRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quickpaymentregister u = new quickpaymentregister(this, tabControl);
            AddNewTab(u);
        }

        private void quickReceiptRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quickreceptregister u = new quickreceptregister(this, tabControl);
            AddNewTab(u);
        }

        private void additionalFieldsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdditionalFields u = new AdditionalFields(this, tabControl);
            AddNewTab(u);
        }
        int filelength = 1;
        private void syncronizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblsync.Visible = true;
            lblsync.Text = "Synchronization In Process";
            processbar.Maximum = 8;
            filelength = 8;
            processbar.Visible = true;
            processbar.PerformStep();
            Syncronization syn = new Syncronization();
            processbar.PerformStep();
            processbar.PerformStep();
            processbar.PerformStep();
            syn.bindserverconnection();
            processbar.PerformStep();
            processbar.PerformStep();
            processbar.PerformStep();
            processbar.PerformStep();
            if (processbar.Value == filelength)
            {
                lblsync.Text = "Synchronization In Complate";
                processbar.Value = 0;
                processbar.Visible = false;
                lblsync.Visible = false;
            }
            #region
            // // string mainstr = "";
            // lblsync.Visible = true;
            // lblsync.Text = "Synchronization In Process";
            // processbar.Maximum = 8;
            // filelength = 8;
            // processbar.Visible = true;
            // Syncronization syn = new Syncronization();
            // syn.bindserverconnection();
            // syn.ProvisionServer();

            // syn.ProvisionClient();

            //// string str = syn.Sync();
            //// processbar.PerformStep();
            //// str = syn.Sync1();
            //// processbar.PerformStep();
            //// str = syn.Sync2();
            //// processbar.PerformStep();
            // string str = syn.Sync3();
            // processbar.PerformStep();
            // str = syn.Sync4();
            // processbar.PerformStep();
            //// str = syn.Sync5();
            //// processbar.PerformStep();
            //// str = syn.Sync6();
            //// processbar.PerformStep();
            // str = syn.Sync7();
            // processbar.PerformStep();
            //// str = syn.Sync8();
            //// processbar.PerformStep();
            //// str = syn.Sync9();
            //// processbar.PerformStep();
            //// str = syn.Sync10();
            //// processbar.PerformStep();
            //// str = syn.Sync11();
            //// processbar.PerformStep();
            //// str = syn.Sync12();
            //// processbar.PerformStep();
            //// str = syn.Sync13();
            //// processbar.PerformStep();
            //// str = syn.Sync14();
            //// processbar.PerformStep();
            //// str = syn.Sync15();
            //// processbar.PerformStep();
            //// str = syn.Sync16();
            //// processbar.PerformStep();
            //// str = syn.Sync17();
            //// processbar.PerformStep();
            //// str = syn.Sync18();
            //// processbar.PerformStep();
            //// str = syn.Sync19();
            //// processbar.PerformStep();
            //// str = syn.Sync20();
            //// processbar.PerformStep();
            //// str = syn.Sync21();
            //// processbar.PerformStep();
            //// str = syn.Sync22();
            //// processbar.PerformStep();
            //// str = syn.Sync23();
            //// processbar.PerformStep();
            //// str = syn.Sync24();
            //// processbar.PerformStep();
            // str = syn.Sync25();
            // processbar.PerformStep();
            //// str = syn.Sync26();
            //// processbar.PerformStep();
            //// str = syn.Sync27();
            //// processbar.PerformStep();
            //// str = syn.Sync28();
            //// processbar.PerformStep();
            //// str = syn.Sync29();
            //// processbar.PerformStep();
            //// str = syn.Sync30();
            //// processbar.PerformStep();
            //// str = syn.Sync31();
            //// processbar.PerformStep();
            //// str = syn.Sync32();
            //// processbar.PerformStep();
            //// str = syn.Sync33();
            //// processbar.PerformStep();
            //// str = syn.Sync34();
            //// processbar.PerformStep();
            //// str = syn.Sync35();
            //// processbar.PerformStep();
            //// str = syn.Sync36();
            //// processbar.PerformStep();
            //// str = syn.Sync37();
            //// processbar.PerformStep();
            // str = syn.Sync38();
            // processbar.PerformStep();
            // str = syn.Sync39();
            // processbar.PerformStep();
            // str = syn.Sync40();
            // processbar.PerformStep();
            //// str = syn.Sync41();
            //// processbar.PerformStep();
            // str = syn.Sync42();
            // processbar.PerformStep();
            //// str = syn.Sync43();
            //// processbar.PerformStep();
            //// str = syn.Sync44();
            //// processbar.PerformStep();
            //// str = syn.Sync45();
            //// processbar.PerformStep();
            //// str = syn.Sync46();
            //// processbar.PerformStep();
            //// str = syn.Sync47();
            //// processbar.PerformStep();
            //// str = syn.Sync48();
            //// processbar.PerformStep();
            //// str = syn.Sync49();
            //// processbar.PerformStep();
            //// str = syn.Sync50();
            //// processbar.PerformStep();
            //// str = syn.Sync51();
            //// processbar.PerformStep();
            //// str = syn.Sync52();
            //// processbar.PerformStep();
            //// str = syn.Sync53();
            //// processbar.PerformStep();
            //// str = syn.Sync54();
            //// processbar.PerformStep();
            //// str = syn.Sync55();
            //// processbar.PerformStep();
            //// str = syn.Sync56();
            //// processbar.PerformStep();
            //// str = syn.Sync57();
            //// processbar.PerformStep();
            //// str = syn.Sync58();
            //// processbar.PerformStep();
            //// str = syn.Sync59();
            //// processbar.PerformStep();
            //// str = syn.Sync60();
            //// processbar.PerformStep();
            //// str = syn.Sync61();
            //// processbar.PerformStep();
            //// str = syn.Sync62();
            //// processbar.PerformStep();
            //// str = syn.Sync63();
            //// processbar.PerformStep();
            //// str = syn.Sync64();
            //// processbar.PerformStep();
            //// str = syn.Sync65();
            //// processbar.PerformStep();
            //// str = syn.Sync66();
            //// processbar.PerformStep();
            //// str = syn.Sync67();
            //// processbar.PerformStep();
            //// str = syn.Sync68();
            //// processbar.PerformStep();

            // if (processbar.Value == filelength)
            // {
            //     lblsync.Text = "Synchronization In Complate";
            //     processbar.Value = 0;
            //     processbar.Visible = false;
            //     lblsync.Visible = false;
            // }
            ////// lblsync.Text = "Synchronization";
            //// //mainstr += str + Environment.NewLine;
            // // //MessageBox.Show(mainstr);
            #endregion
        }

        private void customerListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerAndSupplierList cs = new CustomerAndSupplierList(this, tabControl);
            AddNewTab(cs);
        }

        private void itemPriceListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemPriceList ip = new ItemPriceList(this, tabControl);
            AddNewTab(ip);
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            Customertype ip = new Customertype(this, tabControl);
            AddNewTab(ip);
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            Employeetype ip = new Employeetype(this, tabControl);
            AddNewTab(ip);
        }

        private void addPromotionOfferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PromotionOffer ip = new PromotionOffer(this, tabControl);
            AddNewTab(ip);
        }

        private void processMenuItem_Click(object sender, EventArgs e)
        {
            ProcessList pl = new ProcessList(this, tabControl);
            AddNewTab(pl);
        }

        private void productionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Production p = new Production(this, tabControl);
            AddNewTab(p);
        }

        private void finishedGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FinishedGoods fg = new FinishedGoods(this, tabControl);
            AddNewTab(fg);
        }

        private void prodcutionRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            productionregister pr = new productionregister(this, tabControl);
            AddNewTab(pr);
        }

        private void productionPlanningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductionPlanning pr = new ProductionPlanning(this, tabControl);
            AddNewTab(pr);
        }

        private void productionUtilizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product_Utilization pu = new Product_Utilization(this, tabControl);
            AddNewTab(pu);
        }

        private void finishedGoodsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FinishedGoodsList fl = new FinishedGoodsList(this, tabControl);
            AddNewTab(fl);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCashBook_Click(object sender, EventArgs e)
        {
            CashBook frm = new CashBook(this, tabControl);
            AddNewTab(frm);
        }

        private void btnSaleRegister_Click(object sender, EventArgs e)
        {
            saleregisterdetailed u = new saleregisterdetailed(this, tabControl);
            AddNewTab(u);
        }

        private void btnoutstanding_Click(object sender, EventArgs e)
        {
            outstandinganalysis frm = new outstandinganalysis(this, tabControl);
            AddNewTab(frm);
        }

        private void btnDayBook_Click(object sender, EventArgs e)
        {
            DayBook be = new DayBook(this, tabControl);
            AddNewTab(be);
        }

        private void btnPurchaseRegister_Click(object sender, EventArgs e)
        {
            purchaseregisterdetailed u = new purchaseregisterdetailed(this, tabControl);
            AddNewTab(u);
        }

        private void btnPOSReg_Click(object sender, EventArgs e)
        {
            POSRegister rn = new POSRegister(this, tabControl);
            AddNewTab(rn);
        }

        private void btnSaleOrder_Click(object sender, EventArgs e)
        {
            string[] strfinalarray = new string[5] { "SO", "D", "Sale Order", "SO", "" };
            SaleOrderList sol = new SaleOrderList(this, tabControl, strfinalarray);
            AddNewTab(sol);
        }

        private void btnPurchaseOrder_Click(object sender, EventArgs e)
        {
            string[] strfinalarray = new string[5] { "PO", "C", "Purchase Order", "PO", "" };
            SaleOrderList sol = new SaleOrderList(this, tabControl, strfinalarray);
            AddNewTab(sol);
        }

        private void btnBankEntry_Click(object sender, EventArgs e)
        {
            BankEntry be = new BankEntry(this, tabControl);
            AddNewTab(be);
        }

        private void customerWiseCallRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerWiseCallReport be = new CustomerWiseCallReport(this, tabControl);
            AddNewTab(be);
        }

        private void pOSListThermalPrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListPOSThemalPrint tp = new ListPOSThemalPrint(this, tabControl);
            AddNewTab(tp);
        }

        private void StockAdjustMent_Click(object sender, EventArgs e)
        {
            StockAdjustmentList frm = new StockAdjustmentList(this, tabControl);
            AddNewTab(frm);
        }

        private void itemWiseSaleListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  ItemWiseSaleList frm = new ItemWiseSaleList(this, tabControl);
            // AddNewTab(frm);
            SaleItemGroupWise frm = new SaleItemGroupWise(this, tabControl);
            AddNewTab(frm);
        }

        private void itemVsSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemSaleChart frm = new ItemSaleChart(this, tabControl);
            AddNewTab(frm);
        }

        private void changeFinancialYearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangeFinancialYear frm = new ChangeFinancialYear(this, tabControl);
            AddNewTab(frm);
        }

        private void opbalanceeditor_Click(object sender, EventArgs e)
        {
            OpBalanceEditor frm = new OpBalanceEditor(this, tabControl);
            AddNewTab(frm);
        }

        private void gSTR1DetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GSTR1 rn = new GSTR1(this, tabControl);
            AddNewTab(rn);
        }

        private void gSTR1SummeryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GSTR1Summery rn = new GSTR1Summery(this, tabControl);
            AddNewTab(rn);
        }

        private void itemVsSaleOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            itemwisesaleorder frm = new itemwisesaleorder(this, tabControl);
            AddNewTab(frm);
        }

        private void Master_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                options = conn.getdataset("select * from options");
                LogGenerator.Info("Company Logout CompanyID=" + Master.companyId);
                LogGenerator.Info("User Logout UserID=" + Master.userid);
                if (options.Rows[0]["userlog"].ToString() == "True")
                {
                    conn.execute("INSERT INTO [dbo].[USerLog]([userid],[loginpalce],[Status],[isactive],[datetime])VALUES('" + Master.userid + "','" + "Company" + "','" + "Logout" + "','" + "1" + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                }
            }
            catch
            {
            }
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SmsTemplate frm = new SmsTemplate(this, tabControl);
            AddNewTab(frm);
        }

        private void rewriteBookOfAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rewritrbooksofaccount frm = new Rewritrbooksofaccount(this, tabControl);
            AddNewTab(frm);
        }

        private void orderLockRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lockproduct frm = new lockproduct(this, tabControl);
            AddNewTab(frm);
        }

        private void gSTVouchersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GSTVouchersList frm = new GSTVouchersList(this, tabControl);
            AddNewTab(frm);
        }

        private void partyWiseItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            partywiseitem frm = new partywiseitem(this, tabControl);
            AddNewTab(frm);
        }

        private void salePurchaseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalePurchaseDetailsReport frmdetails = new SalePurchaseDetailsReport(this, tabControl);
            AddNewTab(frmdetails);
        }

        private void stockInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strfinalarray = new string[5] { "STI", "C", "Stock In", "STI", "" };
            StockinoutList sol = new StockinoutList(this, tabControl, strfinalarray);
            AddNewTab(sol);
        }

        private void stockOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strfinalarray = new string[5] { "STO", "D", "Stock Out", "STO", "" };
            StockinoutList sol = new StockinoutList(this, tabControl, strfinalarray);
            AddNewTab(sol);
        }

        private void stockWithoutBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strfinalarray = new string[5] { "STO", "D", "Stock Out", "STO", "" };
            StockList sol = new StockList(this, tabControl);
            AddNewTab(sol);
        }

        private void itemProductWiseStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemWiseStock frm = new ItemWiseStock(this, tabControl);
            AddNewTab(frm);
        }

        private void stockDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockReport frm = new StockReport(this, tabControl);
            AddNewTab(frm);
        }

        private void stockStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockStatus frm = new StockStatus(this, tabControl);
            AddNewTab(frm);
        }

    }
}
