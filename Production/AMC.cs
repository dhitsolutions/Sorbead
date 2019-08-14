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
using System.Net.Mail;
using System.Net;

namespace Production
{
    public partial class AMC : Form
    {
        OleDbSettings ods = new OleDbSettings();
        ServerConnection con = new ServerConnection();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        string cname, cemail;
        public static string encryptdate = string.Empty;
        private Master master;
        private TabControl tabControl;
        public AMC()
        {
            InitializeComponent();
        }

        public AMC(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }
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
        public static string Encrypt(string clearText)
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
                    encryptdate = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
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
                }
            }
            return cipherText;
        }

        private void AMC_Load(object sender, EventArgs e)
        {
            try
            {
              ds= ods.getdata("Select * from Company");
              dt = ds.Tables[0];
              txtserialkey.Text = dt.Rows[0]["srno"].ToString();
              lbldate.Text = DateTime.Parse(dt.Rows[0]["expdate"].ToString()).ToString(Master.dateformate);
              cname = dt.Rows[0]["regcompanyname"].ToString();
              cemail = dt.Rows[0]["regemail"].ToString();
              //Encrypt(dt.Rows[0]["expdate"].ToString());
             // Decrypt(txtextendkey.Text);
            }
            catch
            {
            }
        }
        public static string SendingEmail(string Name, string Email, string Subject, string Message)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(Email);

            msg.Subject = Subject;
            msg.From = new MailAddress(Email);
            msg.IsBodyHtml = true;
            msg.Body = "<p><strong>Contact Name:</strong> " + Name + "<br />";
            msg.Body += "<strong>Email:</strong> " + Email + "<br />";
            msg.Body += "<strong>Message:</strong> " + Message + "<br />";
            // set the reply to address to the address entered in the form  
            msg.ReplyToList.Add(new MailAddress(Email));
            // set up smtp client and credentials  
            SmtpClient smtpClnt = new SmtpClient();
            smtpClnt.Host = "smtp.gmail.com";
            smtpClnt.EnableSsl = true;
            smtpClnt.Credentials = new NetworkCredential("hiteshb577@gmail.com", "9979512136"); // send the message  
            smtpClnt.Port = 587;
            smtpClnt.Send(msg);
            return "Ok";
        }
        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                //Decrypt(txtextendkey.Text);
                //string d = DateTime.Now.ToString(Master.dateformate);
                //DateTime dt = Convert.ToDateTime(d);
                //dt = dt.AddDays(364);
                //string s = dt.ToString(Master.dateformate);
                //con.execute("UPDATE [businessplus].[tblRegistrationMaster] SET licanceno='" + txtextendkey.Text + "',regdate='" + DateTime.Now.ToString(Master.dateformate) + "',regtime='" + DateTime.Now.ToString("H:mm tt") + "',reregdate='" + DateTime.Now.ToString(Master.dateformate) + "',reregtime='" + DateTime.Now.ToString("H:mm tt") + "',expdate='" + s + "',exptime='" + DateTime.Now.ToString("H:mm tt") + "' where srno='" + txtserialkey.Text + "'");
                //ods.execute("UPDATE [Company] SET licanceno='" + txtextendkey.Text + "',regdate='" + DateTime.Now.ToString(Master.dateformate) + "',regtime='" + DateTime.Now.ToString("H:mm tt") + "',reregdate='" + DateTime.Now.ToString(Master.dateformate) + "',reregtime='" + DateTime.Now.ToString("H:mm tt") + "',expdate='" + s + "',exptime='" + DateTime.Now.ToString("H:mm tt") + "',status='" + "Reg" + "',installationdate='" + DateTime.Now.ToString(Master.dateformate) + "' where CompanyID='" + Master.companyId + "'");
                //MessageBox.Show("Registration Sucessfully Done");
                //master.RemoveCurrentTab();
                
                //SendingEmail(cname,cemail, "Thank You For Re New", "Your Licanceno is '" + txtextendkey.Text + "'");
            }
            catch
            {
            }
        }

        private void txtserialkey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtextendkey.Focus();
            }
        }

        private void txtextendkey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnok.Focus();
            }
        }

        private void txtserialkey_Enter(object sender, EventArgs e)
        {
            txtserialkey.BackColor = Color.LightYellow;
        }

        private void txtserialkey_Leave(object sender, EventArgs e)
        {
            txtserialkey.BackColor = Color.White;
        }

        private void txtextendkey_Enter(object sender, EventArgs e)
        {
            txtextendkey.BackColor = Color.LightYellow;
        }

        private void txtextendkey_Leave(object sender, EventArgs e)
        {
            txtextendkey.BackColor = Color.White;
        }






    }
}
