﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace RamdevSales
{
    public partial class Employeetype : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        DataTable dt = new DataTable();
        private Master master;
        int a;
        private TabControl tabControl;
        string ABC = "";
        public static string pvc;
        private Itementry itementry;
        private Accountentry accountentry;
        private string activecontroal;
        private UserInfo userInfo;
        private ComboBox cmbPosition;
        DataTable userrights = new DataTable();

        public Employeetype()
        {
            InitializeComponent();
            a = 0;
        }

        public Employeetype(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization

            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
            a = 0;
        }

        public Employeetype(Itementry itementry, TabControl tabControl, Master master, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();

            this.itementry = itementry;
            this.tabControl = tabControl;
            this.master = master;
            a = 1;
            pvc = activecontroal;
        }

        public Employeetype(Accountentry accountentry, TabControl tabControl, Master master, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.accountentry = accountentry;
            this.tabControl = tabControl;
            this.master = master;
            a = 1;
            pvc = activecontroal;
        }

        public Employeetype(UserInfo userInfo, TabControl tabControl, Master master, ComboBox cmbPosition)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.userInfo = userInfo;
            this.tabControl = tabControl;
            this.master = master;
            this.cmbPosition = cmbPosition;
            a = 1;
            pvc = activecontroal;
        }

        public Employeetype(UserInfo userInfo, TabControl tabControl, Master master, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.userInfo = userInfo;
            this.tabControl = tabControl;
            this.master = master;
            a = 1;
            pvc = activecontroal;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (string.IsNullOrEmpty(pvc) == true)
                    {
                        master.RemoveCurrentTab();
                    }
                    else
                    {
                        master.RemoveCurrentTab1(pvc, txtgrpname.Text);
                    }

                }
                return true;
            }
            if (keyData == (Keys.Alt | Keys.U))
            {
                DialogResult dr = MessageBox.Show("Do you want to Update?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    submit();
                }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void submit()
        {
            try
            {
                if (txtgrpname.Text != "")
                {
                    if (Button18.Text == "Update")
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                        con.Open();

                        SqlCommand cmd = new SqlCommand("select * from tbluser_employeetype where employeetype='" + ABC + "'", con);
                        string id = cmd.ExecuteScalar().ToString();

                        cmd = new SqlCommand("UPDATE [dbo].[tbluser_employeetype] SET employeetype='" + txtgrpname.Text + "' where id=" + id, con);
                        //  SqlCommand cmd = new SqlCommand("INSERT INTO [Billing].[dbo].[BillMaster] ([Bill_No] ,[Bill_Date],[ClientID],[PO_No],[Bill_Amt],[Bill_vat_Amt],[Bill_Net_Amt],[CompanyID])VALUES ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text + "','" + Convert.ToDouble(TxtBillTotal.Text) + "','" + Convert.ToDouble(TxtVAT16Amnt.Text) + "','" + Convert.ToDouble(TxtVATExmptAmnt.Text) + "','" + cmp + "')", con);
                        cmd.ExecuteNonQuery();

                        Button18.Text = "SAVE";
                        listviewbind();
                        if (a == 1)
                        {
                            //accountentry.bindcustype();
                            userInfo.bindemptype();
                            if (string.IsNullOrEmpty(pvc) == true)
                            {
                                master.RemoveCurrentTab();
                            }
                            else
                            {
                                master.RemoveCurrentTab1(pvc, txtgrpname.Text);
                            }
                        }
                        else
                        {
                            this.ActiveControl = txtgrpname;
                            txtgrpname.Focus();
                        }
                        txtgrpname.Text = "";
                    }
                    else
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                        con.Open();
                        SqlCommand cmd = new SqlCommand("Select * from tbluser_employeetype where employeetype='" + txtgrpname.Text + "' and isactive=1", con);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("User/Employee Type Name Already Exist..");
                        }
                        else
                        {
                            SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[tbluser_employeetype]([employeetype],[isactive])values ('" + txtgrpname.Text + "',1)", con);
                            //  SqlCommand cmd = new SqlCommand("INSERT INTO [Billing].[dbo].[BillMaster] ([Bill_No] ,[Bill_Date],[ClientID],[PO_No],[Bill_Amt],[Bill_vat_Amt],[Bill_Net_Amt],[CompanyID])VALUES ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString("MM-dd-yyyy") + "','" + cmbcustname.SelectedValue + "','" + txtpono.Text + "','" + Convert.ToDouble(TxtBillTotal.Text) + "','" + Convert.ToDouble(TxtVAT16Amnt.Text) + "','" + Convert.ToDouble(TxtVATExmptAmnt.Text) + "','" + cmp + "')", con);
                            cmd1.ExecuteNonQuery();
                            listviewbind();

                            Button18.Text = "SAVE";
                            if (a == 1)
                            {
                                userInfo.bindemptype();
                                if (string.IsNullOrEmpty(pvc) == true)
                                {
                                    master.RemoveCurrentTab();
                                }
                                else
                                {
                                    master.RemoveCurrentTab1(pvc, txtgrpname.Text);
                                }
                            }
                            else
                            {
                                this.ActiveControl = txtgrpname;
                                txtgrpname.Focus();
                            }
                            txtgrpname.Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("User/Employee Type cannot be Blank");
                    this.ActiveControl = txtgrpname;
                    return;
                }
            }
            catch
            {
                con.Close();
            }
        }
        private void listviewbind()
        {
            try
            {
                LVclient.Items.Clear();

                SqlCommand cmd = new SqlCommand("Select * from tbluser_employeetype where isactive=1 order by employeetype", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    LVclient.Items.Add(dt.Rows[i].ItemArray[1].ToString());

                }
            }
            catch
            {
            }
        }
        private void ItemGroup_Load(object sender, EventArgs e)
        {
            con.Open();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            LVclient.Columns.Add("User/Employee Type Name", 300, HorizontalAlignment.Center);

            userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[33]["a"].ToString() == "False")
                {
                    Button18.Enabled = false;
                }
                if (userrights.Rows[33]["d"].ToString() == "False")
                {
                    btndelete.Enabled = false;
                }
                listviewbind();
            }


            // binddrop();
            con.Close();
            // txtgrpname.Focus();
            this.ActiveControl = txtgrpname;
        }

        private void txtgrpname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Button18.Focus();
            }
        }

        private void txtgrpname_Leave(object sender, EventArgs e)
        {
            txtgrpname.BackColor = Color.White;
        }

        private void txtgrpname_Enter(object sender, EventArgs e)
        {
            txtgrpname.BackColor = Color.LightYellow;
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            submit();
        }
        public void open()
        {
            try
            {
                if (LVclient.SelectedItems.Count > 0)
                {
                    ABC = LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text;
                    txtgrpname.Text = LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text;
                    Button18.Text = "Update";
                    LVclient.Items[LVclient.FocusedItem.Index].Remove();
                }
            }
            catch
            {
            }
        }
        private void LVclient_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[33]["u"].ToString() == "True")
                {
                    open();
                }
                else
                {
                    MessageBox.Show("You Don't Have Permission To View");
                    return;
                }
            }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(pvc) == true)
                {
                    master.RemoveCurrentTab();
                }
                else
                {
                    master.RemoveCurrentTab1(pvc, txtgrpname.Text);
                }
            }
        }

        private void LVclient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button18_MouseEnter(object sender, EventArgs e)
        {
            Button18.UseVisualStyleBackColor = false;
            Button18.BackColor = Color.YellowGreen;
            Button18.ForeColor = Color.White;
        }

        private void Button18_MouseLeave(object sender, EventArgs e)
        {
            Button18.UseVisualStyleBackColor = true;
            Button18.BackColor = Color.FromArgb(51, 153, 255);
            Button18.ForeColor = Color.White;
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
        }

        private void Button18_Enter(object sender, EventArgs e)
        {
            Button18.UseVisualStyleBackColor = false;
            Button18.BackColor = Color.YellowGreen;
            Button18.ForeColor = Color.White;
        }

        private void Button18_Leave(object sender, EventArgs e)
        {
            Button18.UseVisualStyleBackColor = true;
            Button18.BackColor = Color.FromArgb(51, 153, 255);
            Button18.ForeColor = Color.White;
        }

        private void btnClose_Enter(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = false;
            btnClose.BackColor = Color.FromArgb(248, 152, 94);
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_Leave(object sender, EventArgs e)
        {
            btnClose.UseVisualStyleBackColor = true;
            btnClose.BackColor = Color.FromArgb(51, 153, 255);
            btnClose.ForeColor = Color.White;
        }

        internal void Update(string group)
        {
            if (group != "" && group != null)
            {
                SqlCommand cmd5 = new SqlCommand("select * from tbluser_employeetype where id='" + group + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd5);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtgrpname.Text = dt.Rows[0]["employeetype"].ToString();
                    ABC = dt.Rows[0]["employeetype"].ToString();
                    Button18.Text = "Update";
                }
            }
            //throw new NotImplementedException();
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

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete User/Employee Type?", "User/Employee Type", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    // this.Enabled = false;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    string id = LVclient.Items[LVclient.FocusedItem.Index].SubItems[0].Text; ;
                    SqlCommand cmd = new SqlCommand("update tbluser_employeetype set isactive=0 where employeetype='" + txtgrpname.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    listviewbind();
                    txtgrpname.Text = "";
                }
            }
            catch (Exception excp)
            {

            }
        }

        private void LVclient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[33]["u"].ToString() == "True")
                    {
                        open();
                    }
                    else
                    {
                        MessageBox.Show("You Don't Have Permission To View");
                        return;
                    }
                }
            }
        }
    }
}
