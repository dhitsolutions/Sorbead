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
using System.Security.Cryptography;
using System.IO;
using System.Globalization;

namespace RamdevSales
{
    public partial class DebitandCreditNote : Form
    {
        private Master master;
        private TabControl tabControl;
        private string[] debitorcredit;
        public static string activecontroal;
        OleDbSettings ods = new OleDbSettings();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();

        public DebitandCreditNote()
        {
            InitializeComponent();
        }

        public DebitandCreditNote(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        public DebitandCreditNote(Master master, TabControl tabControl, string[] debitorcredit)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            this.debitorcredit = debitorcredit;
        }
        public void bindcustomer()
        {
            string qry = "";
            qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or groupid=99 or groupID=17 or groupid=18 or groupid=11 or groupid=24 or groupid=25 or groupid=26 or groupid=27) order by AccountName";
            //select id,groupname from accountgroup order by groupname asc
            SqlCommand cmd1 = new SqlCommand(qry, con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cmbaccountName.ValueMember = "ClientID";
            cmbaccountName.DisplayMember = "AccountName";
            cmbaccountName.DataSource = dt1;
            cmbaccountName.SelectedIndex = -1;

        }
        DataTable userrights = new DataTable();
        public void loadpage()
        {
            try
            {
                if (cnt == 0)
                {
                    userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                    if (debitorcredit[0] == "D")
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[6]["a"].ToString() == "False")
                            {
                                BtnSubmit.Enabled = false;
                            }
                            if (userrights.Rows[6]["d"].ToString() == "False")
                            {
                                btndelete.Enabled = false;
                            }
                            if (userrights.Rows[6]["p"].ToString() == "False")
                            {
                                btnprint.Enabled = false;
                            }
                        }
                        txtheader.Text = "DEBIT NOTE";
                        this.Text = "DEBIT NOTE";
                        cmbdrcr.Text = "D";
                    }
                    else
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[7]["a"].ToString() == "False")
                            {
                                BtnSubmit.Enabled = false;
                            }
                            if (userrights.Rows[7]["d"].ToString() == "False")
                            {
                                btndelete.Enabled = false;
                            }
                            if (userrights.Rows[7]["p"].ToString() == "False")
                            {
                                btnprint.Enabled = false;
                            }
                        }
                        txtheader.Text = "CREDIT NOTE";
                        this.Text = "CREDIT NOTE";
                        cmbdrcr.Text = "C";
                    }
                }
                if (txtheader.Text == "DEBIT NOTE")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[6]["p"].ToString() == "False")
                        {
                            btnprint.Enabled = false;
                        }
                    }
                }
                else
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[7]["p"].ToString() == "False")
                        {
                            btnprint.Enabled = false;
                        }
                    }
                }

                this.ActiveControl = TxtRundate;
                DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                TxtRundate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                TxtRundate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                TxtRundate.CustomFormat = Master.dateformate;
                lvserial.Columns.Add("DC", 100, HorizontalAlignment.Left);
                lvserial.Columns.Add("Account Name", 260, HorizontalAlignment.Left);
                lvserial.Columns.Add("Debit", 120, HorizontalAlignment.Left);
                lvserial.Columns.Add("Credit", 120, HorizontalAlignment.Left);
                lvserial.Columns.Add("type", 0, HorizontalAlignment.Left);
                lvserial.Columns.Add("Short Narration", 150, HorizontalAlignment.Left);
                lvserial.Columns.Add("AccountID", 0, HorizontalAlignment.Left);

                bindcustomer();
                txtcredittotal.Text = "0";
                txtdebittotal.Text = "0";
            }
            catch
            {
            }
        }
        int cnt = 0;
        private void DebitandCreditNote_Load(object sender, EventArgs e)
        {
            if (cnt == 0)
            {
                loadpage();
            }
        }

        private void TxtRundate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (BtnSubmit.Text != "Update")
                {
                    voucherno();
                }
                txtvchno.Focus();
            }
        }

        private void txtvchno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbdrcr.Focus();
                //  cmbdrcr.BackColor = Color.LightBlue;
            }
        }

        private void txtvchno_Enter(object sender, EventArgs e)
        {
            txtvchno.BackColor = Color.LightYellow;
        }

        private void txtvchno_Leave(object sender, EventArgs e)
        {
            txtvchno.BackColor = Color.White;
        }

        private void cmbdrcr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // cmbdrcr.BackColor = Color.White;
                cmbaccountName.Focus();
            }
        }

        private void cmbaccountName_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbaccountName.SelectedIndex = 0;
                cmbaccountName.DroppedDown = true;
            }
            catch
            {
            }
        }
        public static string s;
        private void cmbaccountName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {


                //for (int j = 0; j < lvserial.Items.Count; j++)
                //{
                //    if (lvserial.Items[j].ToString() == aname && lvserial.Items[j].ToString() == drcr)
                //    {
                //        //  DO NOT FILL
                //        MessageBox.Show("Same Account Cannot be Debited And Credited in a Single Voucher.");
                //    }
                //}
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbaccountName.Items.Count; i++)
                {
                    s = cmbaccountName.GetItemText(cmbaccountName.Items[i]);
                    if (s == cmbaccountName.Text)
                    {
                        inList = true;
                        cmbaccountName.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbaccountName.Text = "";
                }

                txtAmount.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbaccountName;
                activecontroal = privouscontroal.Name;
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);

                client.Passed(1);
                //   client.Show();
                master.AddNewTab(client);
            }
            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = cmbaccountName;
                activecontroal = privouscontroal.Name;
                string iid = cmbaccountName.SelectedValue.ToString();

                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
        }

        private void cmbaccountName_Leave(object sender, EventArgs e)
        {
            cmbaccountName.Text = s;
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!String.IsNullOrEmpty(txtAmount.Text))
                {
                    txtshortnarration.Focus();
                }
            }
        }

        private void txtAmount_Enter(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.LightYellow;
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            txtAmount.BackColor = Color.White;
        }
        ListViewItem li;
        private void txtshortnarration_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string aname = cmbaccountName.Text;
                    string drcr = cmbdrcr.Text;
                    if (drcr == "D")
                    {
                        drcr = "C";
                    }
                    else
                    {
                        drcr = "D";
                    }
                    foreach (ListViewItem lstItem in lvserial.Items)
                    {
                        if (lstItem.SubItems[1].Text == aname && lstItem.SubItems[0].Text == drcr)
                        {
                            //  DO NOT FILL
                            MessageBox.Show("Same Account Cannot be Debited And Credited in a Single Voucher.");
                            txtshortnarration.Focus();
                            return;
                        }
                    }
                    if (rowid >= 0)
                    {
                        lvserial.Items[rowid].SubItems[0].Text = cmbdrcr.Text;
                        lvserial.Items[rowid].SubItems[1].Text = cmbaccountName.Text;
                        if (cmbdrcr.Text == "D")
                        {
                            lvserial.Items[rowid].SubItems[2].Text = txtAmount.Text;
                            lvserial.Items[rowid].SubItems[3].Text = "0";
                            lvserial.Items[rowid].SubItems[4].Text = "DEBIT NOTE";
                        }
                        else
                        {
                            lvserial.Items[rowid].SubItems[2].Text = "0";
                            lvserial.Items[rowid].SubItems[3].Text = txtAmount.Text;
                            lvserial.Items[rowid].SubItems[4].Text = "CREDIT NOTE";
                        }

                        lvserial.Items[rowid].SubItems[5].Text = txtshortnarration.Text;
                        lvserial.Items[rowid].SubItems[6].Text = Convert.ToString(cmbaccountName.SelectedValue);
                        rowid = -1;
                        decimal debit = 0;
                        decimal credit = 0;
                        foreach (ListViewItem lstItem in lvserial.Items)
                        {
                            debit += decimal.Parse(lstItem.SubItems[2].Text);
                            credit += decimal.Parse(lstItem.SubItems[3].Text);
                        }
                        txtdebittotal.Text = Convert.ToString(debit);
                        txtcredittotal.Text = Convert.ToString(credit);
                        if (txtcredittotal.Text == txtdebittotal.Text)
                        {
                            BtnSubmit.Focus();
                        }
                        else
                        {
                            cmbdrcr.Focus();
                        }
                        txtAmount.Text = "";
                        txtshortnarration.Text = "";
                    }
                    else
                    {
                        //lvserial.Items.Clear();
                        li = lvserial.Items.Add(cmbdrcr.Text);
                        li.SubItems.Add(cmbaccountName.Text);
                        if (cmbdrcr.Text == "D")
                        {
                            li.SubItems.Add(txtAmount.Text);
                            li.SubItems.Add("0");
                            li.SubItems.Add("DEBIT NOTE");
                        }
                        else
                        {
                            li.SubItems.Add("0");
                            li.SubItems.Add(txtAmount.Text);
                            li.SubItems.Add("CREDIT NOTE");
                        }
                        li.SubItems.Add(txtshortnarration.Text);
                        li.SubItems.Add(Convert.ToString(cmbaccountName.SelectedValue));
                        decimal debit = 0;
                        decimal credit = 0;
                        foreach (ListViewItem lstItem in lvserial.Items)
                        {
                            debit += decimal.Parse(lstItem.SubItems[2].Text);
                            credit += decimal.Parse(lstItem.SubItems[3].Text);
                        }
                        txtdebittotal.Text = Convert.ToString(debit);
                        txtcredittotal.Text = Convert.ToString(credit);
                        if (txtcredittotal.Text == txtdebittotal.Text)
                        {
                            BtnSubmit.Focus();
                        }
                        else
                        {
                            cmbdrcr.Focus();
                        }
                        txtAmount.Text = "";
                        txtshortnarration.Text = "";
                        if (cmbdrcr.Text == "D")
                        {
                            cmbdrcr.Text = "C";
                        }
                        else
                        {
                            cmbdrcr.Text = "D";
                        }
                    }
                }
            }
            catch
            {
            }
        }
        public void clearall()
        {
            txtvchno.Text = "";
            cmbdrcr.SelectedIndex = -1;
            cmbaccountName.SelectedIndex = -1;
            txtAmount.Text = "";
            txtshortnarration.Text = "";
            txtlongnarration.Text = "";
            txtdebittotal.Text = "";
            txtcredittotal.Text = "";
            lvserial.Items.Clear();
            TxtRundate.Focus();
            BtnSubmit.Text = "&Submit";
        }
        string vono, strvono;
        public void voucherno()
        {
            //vono = conn.ExecuteScalar("select max(Voucherid) as Voucherid from Ledger where isactive=1 and OT7='Bank Entry'");
            vono = conn.ExecuteScalar("select max(VoucherID) as VoucherID from tbldebitcreditnote where isactive=1 and TranType='" + txtheader.Text + "'");
            Int64 id, count;
            if (vono == "")
            {

                id = 1;
                count = 1;
            }
            else
            {
                id = Convert.ToInt32(vono) + 1;
                count = Convert.ToInt32(vono) + 1;
            }
            strvono = Convert.ToString(id);
            txtvchno.Text = strvono;
        }
        private void txtshortnarration_Enter(object sender, EventArgs e)
        {
            txtshortnarration.BackColor = Color.LightYellow;
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
            if (keyData == (Keys.Alt | Keys.U))
            {
                enterdata();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        string amount;
        string partyname123;
        string partynamedebit;
        string crdr;
        public void enterdata()
        {
            try
            {
                if (BtnSubmit.Text == "Update")
                {
                    if (txtheader.Text == "CREDIT NOTE")
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[6]["u"].ToString() == "False")
                            {
                                MessageBox.Show("You don't have Permission To Update");
                                return;
                            }
                        }
                    }
                    else if (txtheader.Text == "DEBIT NOTE")
                    {
                        if (userrights.Rows.Count > 0)
                        {
                            if (userrights.Rows[7]["u"].ToString() == "False")
                            {
                                MessageBox.Show("You don't have Permission To Update");
                                return;
                            }
                        }
                    }
                    if (txtdebittotal.Text == txtcredittotal.Text)
                    {
                        for (int i = 0; i < lvserial.Items.Count; i++)
                        {
                            DataTable due = conn.getdataset("select * from ClientMaster where isactive=1 and AccountName='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "'");
                            //if (due.Rows.Count > 0)
                            //{
                            DateTime billdate = TxtRundate.Value;
                            string creditdays = due.Rows[0]["credaysale"].ToString();
                            if (string.IsNullOrEmpty(creditdays))
                            {
                                creditdays = "0";
                            }
                            DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                            string due1 = duedate.ToString(Master.dateformate);
                            //  }
                            partyname123 = "";
                            string dc = lvserial.Items[i].SubItems[0].Text.Replace(",", "");
                            if (dc == "D")
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [isactive]='0' where [VoucherID]='" + txtvchno.Text + "'and [OT4]='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "' and [AccountName]='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "'");
                                conn.execute("UPDATE [dbo].[tbldebitcreditnote] SET [isactive]='0' where [VoucherID]='" + txtvchno.Text + "'and [OT4]='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "' and [AccountName]='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "'");
                                for (int j = 0; j < lvserial.Items.Count; j++)
                                {

                                    if (lvserial.Items[j].SubItems[0].Text.Replace(",", "") == "C")
                                    {
                                        partyname123 += lvserial.Items[j].SubItems[1].Text.Replace(",", "") + ",";
                                    }
                                }
                                if (lvserial.Items[i].SubItems[0].Text.Replace(",", "") == "D")
                                {
                                    amount = lvserial.Items[i].SubItems[2].Text.Replace(",", "");
                                }
                                else
                                {
                                    amount = lvserial.Items[i].SubItems[3].Text.Replace(",", "");
                                }
                                partyname123 = partyname123.TrimEnd(',');
                                string cashornot = "";
                                if (lvserial.Items[i].SubItems[1].Text.Replace(",", "") == "Cash")
                                {
                                    cashornot = "Cash";
                                }
                                else
                                {
                                    cashornot = "Credit";
                                }
                                // conn.execute("UPDATE [dbo].[Ledger] SET [AccountID]='" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[AccountName]='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "',[Amount]='" + amount + "',[DC]='" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "',[ShortNarration]='" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',[OT4]='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "',[OT1]='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "',[OT2]='" + txtdebittotal.Text + "',[OT3]='" + txtcredittotal.Text + "',[OT5]='" + txtlongnarration.Text + "' where [VoucherID]='" + txtvchno.Text + "'and [OT4]='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "'");
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountName],[Amount],[DC],[ShortNarration],[isactive],[OT1],[OT2],[OT3],[AccountID],[OT4],[OT5],[OT6],[OD1]) values ('" + txtvchno.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + txtheader.Text + "','" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "','" + amount + "','" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',1,'" + cashornot + "','" + txtdebittotal.Text + "','" + txtcredittotal.Text + "','" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "','" + txtlongnarration.Text + "','" + partyname123 + "','" + Convert.ToDateTime(due1).ToString(Master.dateformate) + "')");
                                conn.execute("INSERT INTO [dbo].[tbldebitcreditnote]([VoucherID],[Date1],[TranType],[AccountName],[Amount],[DC],[ShortNarration],[isactive],[OT1],[OT2],[OT3],[AccountID],[OT4],[OT5],[OT6],[OD1]) values ('" + txtvchno.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + txtheader.Text + "','" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "','" + amount + "','" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',1,'" + cashornot + "','" + txtdebittotal.Text + "','" + txtcredittotal.Text + "','" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "','" + txtlongnarration.Text + "','" + partyname123 + "','" + Convert.ToDateTime(due1).ToString(Master.dateformate) + "')");
                            }
                            else
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [isactive]='0' where [VoucherID]='" + txtvchno.Text + "'and [OT4]='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "' and [AccountName]='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "'");
                                conn.execute("UPDATE [dbo].[tbldebitcreditnote] SET [isactive]='0' where [VoucherID]='" + txtvchno.Text + "'and [OT4]='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "' and [AccountName]='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "'");
                                for (int j = 0; j < lvserial.Items.Count; j++)
                                {

                                    if (lvserial.Items[j].SubItems[0].Text.Replace(",", "") == "D")
                                    {
                                        partyname123 += lvserial.Items[j].SubItems[1].Text.Replace(",", "") + ",";
                                    }
                                }
                                if (lvserial.Items[i].SubItems[0].Text.Replace(",", "") == "D")
                                {
                                    amount = lvserial.Items[i].SubItems[2].Text.Replace(",", "");
                                }
                                else
                                {
                                    amount = lvserial.Items[i].SubItems[3].Text.Replace(",", "");
                                }
                                partyname123 = partyname123.TrimEnd(',');
                                //conn.execute("UPDATE [dbo].[Ledger] SET [AccountID]='" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[AccountName]='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "',[Amount]='" + amount + "',[DC]='" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "',[ShortNarration]='" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',[OT4]='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "',[OT1]='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "',[OT2]='" + txtdebittotal.Text + "',[OT3]='" + txtcredittotal.Text + "',[OT5]='" + txtlongnarration.Text + "' where [VoucherID]='" + txtvchno.Text + "'and [OT4]='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "'");
                                string cashornot = "";
                                if (lvserial.Items[i].SubItems[1].Text.Replace(",", "") == "Cash")
                                {
                                    cashornot = "Cash";
                                }
                                else
                                {
                                    cashornot = "Credit";
                                }
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountName],[Amount],[DC],[ShortNarration],[isactive],[OT1],[OT2],[OT3],[AccountID],[OT4],[OT5],[OT6],[OD1]) values ('" + txtvchno.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + txtheader.Text + "','" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "','" + amount + "','" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',1,'" + cashornot + "','" + txtdebittotal.Text + "','" + txtcredittotal.Text + "','" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "','" + txtlongnarration.Text + "','" + partyname123 + "','" + Convert.ToDateTime(due1).ToString(Master.dateformate) + "')");
                                conn.execute("INSERT INTO [dbo].[tbldebitcreditnote]([VoucherID],[Date1],[TranType],[AccountName],[Amount],[DC],[ShortNarration],[isactive],[OT1],[OT2],[OT3],[AccountID],[OT4],[OT5],[OT6],[OD1]) values ('" + txtvchno.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + txtheader.Text + "','" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "','" + amount + "','" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',1,'" + cashornot + "','" + txtdebittotal.Text + "','" + txtcredittotal.Text + "','" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "','" + txtlongnarration.Text + "','" + partyname123 + "','" + Convert.ToDateTime(due1).ToString(Master.dateformate) + "')");
                            }

                        }
                        MessageBox.Show(txtheader.Text + " Entry Saved");

                        if (txtheader.Text == "CREDIT NOTE")
                        {
                            if (userrights.Rows.Count > 0)
                            {
                                if (userrights.Rows[7]["p"].ToString() == "True")
                                {
                                    print();
                                }
                            }
                        }
                        else if (txtheader.Text == "DEBIT NOTE")
                        {
                            if (userrights.Rows.Count > 0)
                            {
                                if (userrights.Rows[6]["p"].ToString() == "True")
                                {
                                    print();
                                }
                            }
                        }
                        clearall();
                        this.ActiveControl = TxtRundate;
                    }
                    else
                    {
                        MessageBox.Show("Debit & Credit Totals Shouldbe Equal");
                        cmbdrcr.Focus();
                    }
                }
                else
                {
                    voucherno();
                    if (txtdebittotal.Text == txtcredittotal.Text)
                    {
                        if (!string.IsNullOrEmpty(cmbaccountName.Text))
                        {
                            for (int i = 0; i < lvserial.Items.Count; i++)
                            {
                                DataTable due = conn.getdataset("select * from ClientMaster where isactive=1 and AccountName='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "'");
                                //if (due.Rows.Count > 0)
                                //{
                                DateTime billdate = TxtRundate.Value;
                                string creditdays = due.Rows[0]["credaysale"].ToString();
                                if (string.IsNullOrEmpty(creditdays))
                                {
                                    creditdays = "0";
                                }
                                DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                                string due1 = duedate.ToString(Master.dateformate);
                                //  }
                                partyname123 = "";
                                string dc = lvserial.Items[i].SubItems[0].Text.Replace(",", "");
                                if (dc == "D")
                                {
                                    for (int j = 0; j < lvserial.Items.Count; j++)
                                    {
                                        if (lvserial.Items[j].SubItems[0].Text.Replace(",", "") == "C")
                                        {
                                            partyname123 += lvserial.Items[j].SubItems[1].Text.Replace(",", "") + ",";
                                        }
                                    }
                                    if (lvserial.Items[i].SubItems[0].Text.Replace(",", "") == "D")
                                    {
                                        amount = lvserial.Items[i].SubItems[2].Text.Replace(",", "");
                                    }
                                    else
                                    {
                                        amount = lvserial.Items[i].SubItems[3].Text.Replace(",", "");
                                    }
                                    partyname123 = partyname123.TrimEnd(',');
                                    string cashornot = "";
                                    if (lvserial.Items[i].SubItems[1].Text.Replace(",", "") == "Cash")
                                    {
                                        cashornot = "Cash";
                                    }
                                    else
                                    {
                                        cashornot = "Credit";
                                    }
                                    conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountName],[Amount],[DC],[ShortNarration],[isactive],[OT1],[OT2],[OT3],[AccountID],[OT4],[OT5],[OT6],[OD1]) values ('" + txtvchno.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + txtheader.Text + "','" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "','" + amount + "','" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',1,'" + cashornot + "','" + txtdebittotal.Text + "','" + txtcredittotal.Text + "','" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "','" + txtlongnarration.Text + "','" + partyname123 + "','" + Convert.ToDateTime(due1).ToString(Master.dateformate) + "')");
                                    conn.execute("INSERT INTO [dbo].[tbldebitcreditnote]([VoucherID],[Date1],[TranType],[AccountName],[Amount],[DC],[ShortNarration],[isactive],[OT1],[OT2],[OT3],[AccountID],[OT4],[OT5],[OT6],[OD1]) values ('" + txtvchno.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + txtheader.Text + "','" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "','" + amount + "','" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',1,'" + cashornot + "','" + txtdebittotal.Text + "','" + txtcredittotal.Text + "','" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "','" + txtlongnarration.Text + "','" + partyname123 + "','" + Convert.ToDateTime(due1).ToString(Master.dateformate) + "')");
                                }

                                else
                                {
                                    for (int j = 0; j < lvserial.Items.Count; j++)
                                    {

                                        if (lvserial.Items[j].SubItems[0].Text.Replace(",", "") == "D")
                                        {
                                            partyname123 += lvserial.Items[j].SubItems[1].Text.Replace(",", "") + ",";
                                        }
                                    }
                                    if (lvserial.Items[i].SubItems[0].Text.Replace(",", "") == "D")
                                    {
                                        amount = lvserial.Items[i].SubItems[2].Text.Replace(",", "");
                                    }
                                    else
                                    {
                                        amount = lvserial.Items[i].SubItems[3].Text.Replace(",", "");
                                    }
                                    partyname123 = partyname123.TrimEnd(',');
                                    string cashornot = "";
                                    if (lvserial.Items[i].SubItems[1].Text.Replace(",", "") == "Cash")
                                    {
                                        cashornot = "Cash";
                                    }
                                    else
                                    {
                                        cashornot = "Credit";
                                    }
                                    conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountName],[Amount],[DC],[ShortNarration],[isactive],[OT1],[OT2],[OT3],[AccountID],[OT4],[OT5],[OT6],[OD1]) values ('" + txtvchno.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + txtheader.Text + "','" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "','" + amount + "','" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',1,'" + cashornot + "','" + txtdebittotal.Text + "','" + txtcredittotal.Text + "','" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "','" + txtlongnarration.Text + "','" + partyname123 + "','" + Convert.ToDateTime(due1).ToString(Master.dateformate) + "')");
                                    conn.execute("INSERT INTO [dbo].[tbldebitcreditnote]([VoucherID],[Date1],[TranType],[AccountName],[Amount],[DC],[ShortNarration],[isactive],[OT1],[OT2],[OT3],[AccountID],[OT4],[OT5],[OT6],[OD1]) values ('" + txtvchno.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + txtheader.Text + "','" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "','" + amount + "','" + lvserial.Items[i].SubItems[0].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[5].Text.Replace(",", "") + "',1,'" + cashornot + "','" + txtdebittotal.Text + "','" + txtcredittotal.Text + "','" + lvserial.Items[i].SubItems[6].Text.Replace(",", "") + "','" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "','" + txtlongnarration.Text + "','" + partyname123 + "','" + Convert.ToDateTime(due1).ToString(Master.dateformate) + "')");
                                }
                            }
                            MessageBox.Show(txtheader.Text + " Entry Saved");
                            if (txtheader.Text == "CREDIT NOTE")
                            {
                                if (userrights.Rows.Count > 0)
                                {
                                    if (userrights.Rows[6]["p"].ToString() == "True")
                                    {
                                        print();
                                    }
                                }
                            }
                            else if (txtheader.Text == "DEBIT NOTE")
                            {
                                if (userrights.Rows.Count > 0)
                                {
                                    if (userrights.Rows[7]["p"].ToString() == "True")
                                    {
                                        print();
                                    }
                                }
                            }
                            clearall();
                            this.ActiveControl = TxtRundate;
                        }
                        else
                        {
                            MessageBox.Show("Account Name is Required Field.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debit & Credit Totals Shouldbe Equal");
                        cmbdrcr.Focus();
                    }
                }
            }
            catch
            {
            }
        }
        private void txtshortnarration_Leave(object sender, EventArgs e)
        {
            txtshortnarration.BackColor = Color.White;
        }

        private void txtlongnarration_Enter(object sender, EventArgs e)
        {
            txtlongnarration.BackColor = Color.LightYellow;
        }

        private void txtlongnarration_Leave(object sender, EventArgs e)
        {
            txtlongnarration.BackColor = Color.White;
        }

        private void txtdebittotal_Enter(object sender, EventArgs e)
        {
            txtdebittotal.BackColor = Color.LightYellow;
        }

        private void txtdebittotal_Leave(object sender, EventArgs e)
        {
            txtdebittotal.BackColor = Color.White;
        }

        private void txtcredittotal_Enter(object sender, EventArgs e)
        {
            txtcredittotal.BackColor = Color.LightYellow;
        }

        private void txtcredittotal_Leave(object sender, EventArgs e)
        {
            txtcredittotal.BackColor = Color.White;
        }

        private void BtnSubmit_Enter(object sender, EventArgs e)
        {
            BtnSubmit.UseVisualStyleBackColor = false;
            BtnSubmit.BackColor = Color.YellowGreen;
            BtnSubmit.ForeColor = Color.White;
        }

        private void BtnSubmit_Leave(object sender, EventArgs e)
        {
            BtnSubmit.UseVisualStyleBackColor = true;
            BtnSubmit.BackColor = Color.FromArgb(51, 153, 255);
            BtnSubmit.ForeColor = Color.White;
        }

        private void BtnSubmit_MouseEnter(object sender, EventArgs e)
        {
            BtnSubmit.UseVisualStyleBackColor = false;
            BtnSubmit.BackColor = Color.YellowGreen;
            BtnSubmit.ForeColor = Color.White;
        }

        private void BtnSubmit_MouseLeave(object sender, EventArgs e)
        {
            BtnSubmit.UseVisualStyleBackColor = true;
            BtnSubmit.BackColor = Color.FromArgb(51, 153, 255);
            BtnSubmit.ForeColor = Color.White;
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

        private void cmbdrcr_Enter(object sender, EventArgs e)
        {
            try
            {
                // cmbdrcr.SelectedIndex = 0;
                // cmbdrcr.DroppedDown = true;
            }
            catch
            {
            }
        }
        public static string statusreg = string.Empty;
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
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataSet ds = ods.getdata("Select * from tblreg");
            string reg = Convert.ToString(ds.Tables[0].Rows[0]["d16"].ToString());
            Decrypstatus(reg);
            if (txtheader.Text == "CREDIT NOTE")
            {
                if (statusreg == "Edu")
                {
                    string sale = conn.ExecuteScalar("select count(id) from tbldebitcreditnote where isactive=1 and TranType='CREDIT NOTE'");
                    if (sale == "10")
                    {
                        MessageBox.Show("You Are Not Authorized to Add More Then 10 Note");
                        return;
                    }
                }
            }
            if (txtheader.Text == "DEBIT NOTE")
            {
                if (statusreg == "Edu")
                {
                    string sale = conn.ExecuteScalar("select count(id) from tbldebitcreditnote where isactive=1 and TranType='DEBIT NOTE'");
                    if (sale == "10")
                    {
                        MessageBox.Show("You Are Not Authorized to Add More Then 10 Note");
                        return;
                    }
                }
            }
            enterdata();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        Int32 rowid = -1;
        public void open()
        {
            try
            {
                if (userrights.Rows[6]["u"].ToString() == "True")
                {
                    if (lvserial.SelectedItems.Count > 0)
                    {
                        rowid = lvserial.FocusedItem.Index;
                        cmbdrcr.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[0].Text;
                        cmbaccountName.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[1].Text;
                        if (lvserial.Items[lvserial.FocusedItem.Index].SubItems[0].Text == "D")
                        {
                            txtAmount.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[2].Text;
                            txtdebittotal.Text = "0";
                        }
                        else
                        {
                            txtAmount.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[3].Text;
                            txtcredittotal.Text = "0";
                        }
                        txtshortnarration.Text = lvserial.Items[lvserial.FocusedItem.Index].SubItems[5].Text;
                        cmbdrcr.Focus();
                    }
                }
            }
            catch
            {
            }
        }
        private void lvserial_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            open();
        }

        internal void updatemode(string str, string type, string[] debitorcredit)
        {
            userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            cnt = 1;
            DataTable update = conn.getdataset("select * from Ledger where isactive=1 and VoucherID='" + str + "' and TranType='" + type + "'");
            //string[] a = new string[update.Rows.Count];
            //for (int j = 0; j < update.Rows.Count; j++)
            //{
            //    a[j] = update.Rows[j]["DC"].ToString();
            //}
            //string[] debitorcredit = new string[5] { a[], "", "", "", "" };
            if (debitorcredit[0] == "D")
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[6]["a"].ToString() == "False")
                    {
                        BtnSubmit.Enabled = false;
                    }
                    if (userrights.Rows[6]["d"].ToString() == "False")
                    {
                        btndelete.Enabled = false;
                    }
                }
                txtheader.Text = "DEBIT NOTE";
                this.Text = "DEBIT NOTE";
                cmbdrcr.Text = "D";
            }
            else
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[7]["a"].ToString() == "False")
                    {
                        BtnSubmit.Enabled = false;
                    }
                    if (userrights.Rows[7]["d"].ToString() == "False")
                    {
                        btndelete.Enabled = false;
                    }
                }
                txtheader.Text = "CREDIT NOTE";
                this.Text = "CREDIT NOTE";
                cmbdrcr.Text = "C";
            }
            loadpage();
            txtvchno.Text = str;
            TxtRundate.Text = Convert.ToDateTime(update.Rows[0]["Date1"].ToString()).ToString(Master.dateformate);
            for (int i = 0; i < update.Rows.Count; i++)
            {
                li = lvserial.Items.Add(update.Rows[i]["DC"].ToString());
                li.SubItems.Add(update.Rows[i]["AccountName"].ToString());
                if (update.Rows[i]["DC"].ToString() == "D")
                {
                    li.SubItems.Add(update.Rows[i]["Amount"].ToString());
                    li.SubItems.Add("0");
                    li.SubItems.Add(update.Rows[i]["OT4"].ToString());
                }
                else
                {
                    li.SubItems.Add("0");
                    li.SubItems.Add(update.Rows[i]["Amount"].ToString());
                    li.SubItems.Add(update.Rows[i]["OT4"].ToString());
                }
                li.SubItems.Add(update.Rows[i]["ShortNarration"].ToString());
                li.SubItems.Add(Convert.ToString(update.Rows[i]["AccountID"].ToString()));
                decimal debit = 0;
                decimal credit = 0;
                foreach (ListViewItem lstItem in lvserial.Items)
                {
                    debit += decimal.Parse(lstItem.SubItems[2].Text);
                    credit += decimal.Parse(lstItem.SubItems[3].Text);
                }
                txtdebittotal.Text = Convert.ToString(debit);
                txtcredittotal.Text = Convert.ToString(credit);
                BtnSubmit.Text = "Update";
            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbaccountName;
            activecontroal = privouscontroal.Name;
            Accountentry client = new Accountentry(this, master, tabControl, activecontroal);

            client.Passed(1);
            //   client.Show();
            master.AddNewTab(client);
        }

        private void btnAccountEdit_Click(object sender, EventArgs e)
        {
            if (cmbaccountName.Text != "" && cmbaccountName.Text != null)
            {
                var privouscontroal = cmbaccountName;
                activecontroal = privouscontroal.Name;
                string iid = cmbaccountName.SelectedValue.ToString();
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
            else
            {
                MessageBox.Show("Please Select Account Name.");
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    conn.execute("Update Ledger set isactive=0 where VoucherID='" + txtvchno.Text + "' and TranType='" + txtheader.Text + "'");
                    conn.execute("Update tbldebitcreditnote set isactive=0 where VoucherID='" + txtvchno.Text + "' and TranType='" + txtheader.Text + "'");
                    MessageBox.Show("Delete Successfully");
                }
            }
            catch (Exception excp)
            {

            }
        }

        private void btndelete_Enter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(255, 77, 77);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_Leave(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = true;
            btndelete.BackColor = Color.FromArgb(51, 153, 255);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_MouseEnter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(255, 77, 77);
            btndelete.ForeColor = Color.White;
        }

        private void btndelete_MouseLeave(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = true;
            btndelete.BackColor = Color.FromArgb(51, 153, 255);
            btndelete.ForeColor = Color.White;
        }

        private void lvserial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                lvserial.Items[lvserial.FocusedItem.Index].Remove();
                decimal debit = 0;
                decimal credit = 0;
                foreach (ListViewItem lstItem in lvserial.Items)
                {
                    debit += decimal.Parse(lstItem.SubItems[2].Text);
                    credit += decimal.Parse(lstItem.SubItems[3].Text);
                }
                txtdebittotal.Text = Convert.ToString(debit);
                txtcredittotal.Text = Convert.ToString(credit);
            }
            if (e.KeyCode == Keys.Enter)
            {
                open();
            }
        }

        private void cmbaccountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbaccountName.Items.Count; i++)
                {
                    s = cmbaccountName.GetItemText(cmbaccountName.Items[i]);
                    if (s == cmbaccountName.Text)
                    {
                        inList = true;
                        cmbaccountName.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbaccountName.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }
        Printing prn = new Printing();
        public void print()
        {
            try
            {
                //conn.execute("UPDATE [dbo].[tbldebitcreditnote] SET [isactive]='0' where [VoucherID]='" + txtvchno.Text + "'and [OT4]='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "' and [AccountName]='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "'");

                DialogResult dr1 = MessageBox.Show("Do you want to Print Bill?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    prn.execute("delete from printing");
                    DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "'");
                    DataTable dtclient = conn.getdataset("select * from clientmaster where isactive=1 and AccountName ='" + lvserial.Items[0].SubItems[1].Text + "'");//lvserial.Items[i].SubItems[1].Text
                    if (lvserial.Items.Count > 0)
                    {
                        for (int i = 0; i < lvserial.Items.Count; i++)
                        {
                            //string save = conn.ExecuteScalar("select billno from tbldebitcreditnote where isactive=1 and VoucherID='" + txtvchno.Text + "' and OT4='" + lvserial.Items[i].SubItems[4].Text.Replace(",", "") + "' and AccountName='" + lvserial.Items[i].SubItems[1].Text.Replace(",", "") + "'");
                            //if (!string.IsNullOrEmpty(save))
                            //{
                            //ChangeNumbersToWords sh = new ChangeNumbersToWords();
                            //String s1 = Math.Round(Convert.ToDouble(lvserial.Items[i].SubItems[5].Text), 2).ToString("########.00");
                            //string[] words = s1.Split('.');


                            //string str = sh.changeToWords(words[0]);
                            //string str1 = sh.changeToWords(words[1]);
                            //if (str1 == " " || str1 == null || words[1] == "00")
                            //{
                            //    str1 = "Zero ";
                            //}
                            //String inword = "Rupees " + str + "and " + str1 + "Paise Only";

                            String s1 = Math.Round(Convert.ToDouble(lvserial.Items[i].SubItems[2].Text), 2).ToString("########.00");
                            if (s1 == ".00")
                            {
                                s1 = "0";
                                num = s1;
                            }
                            else
                            {
                                num = s1;
                            }
                            GenerateWordsinRs();
                            string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25,T26,T27,T28,T29)VALUES";
                            qry += "('" + lvserial.Items[i].SubItems[0].Text + "','" + lvserial.Items[i].SubItems[1].Text + "','" + lvserial.Items[i].SubItems[2].Text + "','" + lvserial.Items[i].SubItems[3].Text + "','" + lvserial.Items[i].SubItems[4].Text + "','" + lvserial.Items[i].SubItems[5].Text + "','" + lvserial.Items[i].SubItems[6].Text + "','" + TxtRundate.Text + "','" + dt1.Rows[0][1].ToString() + "','" + dt1.Rows[0][2].ToString() + "','" + dt1.Rows[0][3].ToString() + "','" + dt1.Rows[0][4].ToString() + "','" + dt1.Rows[0][5].ToString() + "','" + dt1.Rows[0][6].ToString() + "','" + dt1.Rows[0][7].ToString() + "','" + dt1.Rows[0][8].ToString() + "','" + dt1.Rows[0][9].ToString() + "','" + dt1.Rows[0][10].ToString() + "','" + dt1.Rows[0][11].ToString() + "','" + dt1.Rows[0][12].ToString() + "','" + dt1.Rows[0][13].ToString() + "','" + txtdebittotal.Text + "','" + txtcredittotal.Text + "','" + txtlongnarration.Text + "','" + txtvchno.Text + "','" + dtclient.Rows[0]["Address"].ToString() + "','" + dtclient.Rows[0]["City"].ToString() + "','" + dtclient.Rows[0]["State"].ToString() + "','" + inword + "')";
                            prn.execute(qry);

                            // }
                            //else
                            //{
                            //    MessageBox.Show("You don't have Permission To Print Bill First Save Bill");
                            //    BtnSubmit.Focus();
                            //    return;
                            //}

                        }
                        Print popup = new Print("DebitCreditNote");
                        popup.ShowDialog();
                        popup.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Please Check Records..");
                    }

                }

            }
            catch
            {
            }
        }
        string num = "";
        string inword = "";
        public void GenerateWordsinRs()
        {
            decimal numberrs = Convert.ToDecimal(num);
            CultureInfo ci = new CultureInfo("en-IN");
            string aaa = String.Format("{0:#,##0.##}", numberrs);
            aaa = aaa + " " + ci.NumberFormat.CurrencySymbol.ToString();
            inword = aaa;


            string input = num;
            string a = "";
            string b = "";

            // take decimal part of input. convert it to word. add it at the end of method.
            string decimals = "";

            if (input.Contains("."))
            {
                decimals = input.Substring(input.IndexOf(".") + 1);
                // remove decimal part from input
                input = input.Remove(input.IndexOf("."));

            }
            string strWords = NumbersToWords(Convert.ToInt32(input));

            if (!num.Contains("."))
            {
                a = "In words :" + strWords + " Rupees Only";
            }
            else
            {
                a = "In words :" + strWords + " Rupees";
            }

            if (decimals.Length > 0)
            {
                // if there is any decimal part convert it to words and add it to strWords.
                string strwords2 = NumbersToWords(Convert.ToInt32(decimals));
                b = " and " + strwords2 + " Paisa Only ";
            }

            inword = a + b;
        }
        public static string NumbersToWords(int inputNumber)
        {
            int inputNo = inputNumber;

            if (inputNo == 0)
                return "Zero";

            int[] numbers = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (inputNo < 0)
            {
                sb.Append("Minus ");
                inputNo = -inputNo;
            }

            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            numbers[0] = inputNo % 1000; // units
            numbers[1] = inputNo / 1000;
            numbers[2] = inputNo / 100000;
            numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
            numbers[3] = inputNo / 10000000; // crores
            numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

            for (int i = 3; i > 0; i--)
            {
                if (numbers[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (numbers[i] == 0) continue;
                u = numbers[i] % 10; // ones
                t = numbers[i] / 10;
                h = numbers[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
        private void btnprint_Click(object sender, EventArgs e)
        {
            print();
        }
    }
}
