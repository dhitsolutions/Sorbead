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

namespace RamdevSales
{
    public partial class SmsTemplate : Form
    {
        private Master master;
        private TabControl tabControl;
        Connection conn = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public SmsTemplate()
        {
            InitializeComponent();
        }

        public SmsTemplate(Master master, TabControl tabControl)
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
        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void SmsTemplate_Load(object sender, EventArgs e)
        {
            try
            {
                LVsms.CheckBoxes = true;
                LVsms.Columns.Add("", 20, HorizontalAlignment.Left);
                LVsms.Columns.Add("Send SMS On", 200, HorizontalAlignment.Center);
                LVsms.Columns.Add("SMS Template", 500, HorizontalAlignment.Left);
                LVsms.Columns.Add("id", 0, HorizontalAlignment.Center);
                DataTable dt = conn.getdataset("select * from tblsmsmenumaster where isactive=1");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //LVsms.Items.Add(LVsms.CheckBoxes=true);
                        LVsms.Items.Add("");
                        if (dt.Rows[i]["active"].ToString() == "True")
                        {
                            LVsms.Items[i].Checked = true;
                        }
                        else
                        {
                            LVsms.Items[i].Checked = false;
                        }
                        LVsms.Items[i].SubItems.Add(dt.Rows[i]["smsmenu"].ToString());
                        LVsms.Items[i].SubItems.Add(dt.Rows[i]["message"].ToString());
                        LVsms.Items[i].SubItems.Add(dt.Rows[i]["id"].ToString());
                    }
                }
            }
            catch
            {
            }
        }

        private void btnconfigure_Click(object sender, EventArgs e)
        {
            IntgrateApi frm = new IntgrateApi(master, tabControl);
            master.AddNewTab(frm);
        }
        public void setpage()
        {
            try
            {
                DynamicSMSTemplate frm = new DynamicSMSTemplate(master, tabControl, LVsms.Items[LVsms.FocusedItem.Index].SubItems[1].Text, LVsms.Items[LVsms.FocusedItem.Index].SubItems[3].Text);
                master.AddNewTab(frm);
            }
            catch
            {
            }
        }
        private void LVsms_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            setpage();
        }

        private void LVsms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setpage();
            }
        }
        private string[] strfinalarray;
        private void chkselectall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkselectall.Checked == true)
                {
                    for (int i = 0; i < LVsms.Items.Count; i++)
                    {
                        LVsms.Items[i].Checked = true;
                    }
                }
                else
                {
                    for (int i = 0; i < LVsms.Items.Count; i++)
                    {
                        LVsms.Items[i].Checked = false;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnactive_Click(object sender, EventArgs e)
        {
            try
            {
                strfinalarray = new string[LVsms.CheckedItems.Count];
                int j = 0;
                for (int i = 0; i < LVsms.Items.Count; i++)
                {
                    if (Convert.ToBoolean(LVsms.Items[i].Checked) == true)
                    {
                        string id = LVsms.Items[i].SubItems[3].Text;
                        conn.execute("UPDATE [dbo].[tblsmsmenumaster] SET active='"+"1"+"' where id='"+id+"'");
                    }
                }
            }
            catch
            {
            }
        }

        private void btndeactive_Click(object sender, EventArgs e)
        {
            try
            {
                strfinalarray = new string[LVsms.CheckedItems.Count];
                int j = 0;
                for (int i = 0; i < LVsms.Items.Count; i++)
                {
                    if (Convert.ToBoolean(LVsms.Items[i].Checked) == true)
                    {
                        string id = LVsms.Items[i].SubItems[3].Text;
                        conn.execute("UPDATE [dbo].[tblsmsmenumaster] SET active='" + "0" + "' where id='" + id + "'");
                    }
                }
            }
            catch
            {
            }
        }
    }
}
