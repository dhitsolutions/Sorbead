using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace RamdevSales
{
    public partial class SendSMS : Form
    {
        Connection conn = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        private Master master;
        private TabControl tabControl;
        private string p;
        public static string pagename;
        public string[] newstrfinalarray;

        public SendSMS()
        {
            InitializeComponent();
        }

        public SendSMS(Master master, TabControl tabControl, string p, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            this.p = p;
            pagename = p;
            newstrfinalarray = strfinalarray;
        }

        private void txtpart1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblmsgcount.Text = "Appx. Characters   " + txtpart1.Text.Length;
                int msglenth = txtpart1.Text.Length;
                int smscount = 0;
                //if (txtpart1.Text.Length <= 1 && txtpart1.Text.Length >= 160)
                //{
                //    smscount = 1;
                //}
                //if (msglenth == 160)
                //{
                //    msglenth = 0;
                //    smscount = smscount + 1;
                //}
                lblmsg.Text = "Appx No. of SMS  " + smscount;
            }
            catch
            {
            }
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
        private void SendSMS_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt1 = conn.getdataset("select * from tblsmsmenumaster where isactive=1 and smsmenu='" + pagename + "'");
                txtpart1.Text = dt1.Rows[0]["message"].ToString();
                
            }
            catch
            {
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void lblconfiguresmsapi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SmsTemplate frm = new SmsTemplate(master, tabControl);
            master.AddNewTab(frm);
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable api = conn.getdataset("select * from tblsmsapi where isactive=1");
                string returnmsg = txtpart1.Text;
                string newmessage = string.Empty;
                
                if (api.Rows.Count > 0)
                {

                    foreach (string day in newstrfinalarray)
                    {
                        if (pagename == "Account Master")
                        {
                            DataTable sms = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + day + "'");

                            returnmsg = returnmsg.Replace("{ClientID}", " " + sms.Rows[0]["ClientID"].ToString() + " ").Replace("{PrintName}", " " + sms.Rows[0]["PrintName"].ToString() + " ").Replace("{GroupName}", " " + sms.Rows[0]["GroupName"].ToString() + " ").Replace("{Opbal}", " " + sms.Rows[0]["Opbal"].ToString() + " ").Replace("{Dr_cr}", " " + sms.Rows[0]["Dr_cr"].ToString() + " ").Replace("{Address}", " " + sms.Rows[0]["Address"].ToString() + " ").Replace("{City}", " " + sms.Rows[0]["City"].ToString() + " ").Replace("{State}", " " + sms.Rows[0]["State"].ToString() + " ").Replace("{Phone}", " " + sms.Rows[0]["Phone"].ToString() + " ").Replace("{Mobile}", " " + sms.Rows[0]["Mobile"].ToString() + " ").Replace("{Email}", " " + sms.Rows[0]["Email"].ToString() + " ").Replace("{Cstno}", " " + sms.Rows[0]["Cstno"].ToString() + " ").Replace("{Vatno}", " " + sms.Rows[0]["Vatno"].ToString() + " ").Replace("{GroupID}", " " + sms.Rows[0]["GroupID"].ToString() + " ").Replace("{isactive}", " " + sms.Rows[0]["isactive"].ToString() + " ").Replace("{ismaintain}", " " + sms.Rows[0]["ismaintain"].ToString() + " ").Replace("{GstNo}", " " + sms.Rows[0]["GstNo"].ToString() + " ").Replace("{AdharNo}", " " + sms.Rows[0]["AdharNo"].ToString() + " ").Replace("{statecode}", " " + sms.Rows[0]["statecode"].ToString() + " ").Replace("{crelimite}", " " + sms.Rows[0]["crelimite"].ToString() + " ").Replace("{billlimite}", " " + sms.Rows[0]["billlimite"].ToString() + " ").Replace("{credaysale}", " " + sms.Rows[0]["credaysale"].ToString() + " ").Replace("{credaypurchase}", " " + sms.Rows[0]["credaypurchase"].ToString() + " ").Replace("{accountnumber}", " " + sms.Rows[0]["accountnumber"].ToString() + " ").Replace("{customertypeid}", " " + sms.Rows[0]["customertypeid"].ToString() + " ").Replace("{customertype}", " " + sms.Rows[0]["customertype"].ToString() + " ").Replace("{noteorremarks}", " " + sms.Rows[0]["noteorremarks"].ToString() + " ").Replace("{AccountName}", " " + sms.Rows[0]["AccountName"].ToString() + " ");
                            #region
                            //if (returnmsg.Contains("{ClientID}") == true)
                            //{
                            //    //  returnmsg.Replace("{AccountName}", sms.Rows[0]["AccountName"].ToString());
                            //  // sb.Replace("{ClientID}", sms.Rows[0]["ClientID"].ToString());
                            //  //  newmessage += Regex.Replace(returnmsg, "{ClientID}", sms.Rows[0]["ClientID"].ToString());
                            //    newmessage = returnmsg.Replace("{ClientID}", " " + sms.Rows[0]["ClientID"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{AccountName}") == true)
                            //{
                            //  // sb.Replace("{AccountName}", sms.Rows[0]["AccountName"].ToString());
                            //  //  returnmsg.Replace("{AccountName}", sms.Rows[0]["AccountName"].ToString());
                            //  //  newmessage += Regex.Replace(returnmsg, "{AccountName}", sms.Rows[0]["AccountName"].ToString());
                            //    newmessage += newmessage.Replace("{AccountName}", " " + sms.Rows[0]["AccountName"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{PrintName}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{PrintName}", " " + sms.Rows[0]["PrintName"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{GroupName}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{GroupName}", " " + sms.Rows[0]["GroupName"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{Opbal}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{Opbal}", " " + sms.Rows[0]["Opbal"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{Dr_cr}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{Dr_cr}", " " + sms.Rows[0]["Dr_cr"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{Address}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{Address}", " " + sms.Rows[0]["Address"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{City}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{City}", " " + sms.Rows[0]["City"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{State}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{State}", " " + sms.Rows[0]["State"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{Phone}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{Phone}", " " + sms.Rows[0]["Phone"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{Mobile}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{Mobile}", " " + sms.Rows[0]["Mobile"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{Email}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{Email}", " " + sms.Rows[0]["Email"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{Cstno}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{Cstno}", " " + sms.Rows[0]["Cstno"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{Vatno}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{Vatno}", " " + sms.Rows[0]["Vatno"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{GroupID}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{GroupID}", " " + sms.Rows[0]["GroupID"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{isactive}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{isactive}", " " + sms.Rows[0]["isactive"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{ismaintain}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{ismaintain}", " " + sms.Rows[0]["ismaintain"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{GstNo}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{GstNo}", " " + sms.Rows[0]["GstNo"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{AdharNo}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{AdharNo}", " " + sms.Rows[0]["AdharNo"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{statecode}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{statecode}", " " + sms.Rows[0]["statecode"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{crelimite}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{crelimite}", " " + sms.Rows[0]["crelimite"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{billlimite}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{billlimite}", " " + sms.Rows[0]["billlimite"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{credaysale}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{credaysale}", " " + sms.Rows[0]["credaysale"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{credaypurchase}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{credaypurchase}", " " + sms.Rows[0]["credaypurchase"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{accountnumber}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{accountnumber}", " " + sms.Rows[0]["accountnumber"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{customertypeid}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{customertypeid}", " " + sms.Rows[0]["customertypeid"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{customertype}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{customertype}", " " + sms.Rows[0]["customertype"].ToString() + " ");
                            //}
                            //if (returnmsg.Contains("{noteorremarks}") == true)
                            //{
                            //    newmessage += returnmsg.Replace("{noteorremarks}", " " + sms.Rows[0]["noteorremarks"].ToString() + " ");
                            //}
#endregion
                            try
                            {
                                #region
                                string strUrl = api.Rows[0]["part1"].ToString() + sms.Rows[0]["Mobile"].ToString() + api.Rows[0]["part2"].ToString() + returnmsg + api.Rows[0]["part3"].ToString();
                                // Create a request object  
                                WebRequest request = HttpWebRequest.Create(strUrl);
                                // Get the response back  
                                HttpWebResponse response1 = (HttpWebResponse)request.GetResponse();
                                Stream s = (Stream)response1.GetResponseStream();
                                StreamReader readStream = new StreamReader(s);
                                string dataString = readStream.ReadToEnd();
                                response1.Close();
                                s.Close();
                                readStream.Close();

                                #endregion
                            }
                            
                            catch
                            {
                            }
                        }
                    }
                    MessageBox.Show("SMS Send Successfuly");
                    master.RemoveCurrentTab();
                }
                else
                {
                    MessageBox.Show("Configure SMS API");
                }
                
            }
            catch
            {
            }
        }
    }
}
