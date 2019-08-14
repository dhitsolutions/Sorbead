using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Production
{
    public partial class UserLogin : Form
    {
        private Master master;
        private Master master_1;
        private CompanyList companyList;
        Connection cl = new Connection();
        ServerConnection sc = new ServerConnection();
        DataSet ds,ds1 = new DataSet();
        DataTable dt,dt1 = new DataTable();
        public UserLogin()
        {
            InitializeComponent();
        }

        public UserLogin(CompanyList companyList, Master master_1)
        {
            InitializeComponent();
            this.companyList =companyList;
            this.master_1 = master_1;
        }

        public UserLogin(Master master)
        {
            // TODO: Complete member initialization
            this.master = master;
        }

        public UserLogin(CompanyList companyList, Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.companyList = companyList;
            this.master = master;
            this.tabControl = tabControl;
        }

        public UserLogin(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        private void UserLogin_Load(object sender, EventArgs e)
        {
            loadPage();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                // tabControl.TabPages.Remove(tabControl.SelectedTab);
                DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    master.RemoveCurrentTab();
                    CompanyList frm = new CompanyList(master, tabControl);
                    master.AddNewTab(frm);
                }
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void loadPage()
        {
            panel2.Visible = false;
            listView1.Columns.Add("User List",285);
        }

        int cnt = 0;
        DataTable companydt=new DataTable();
        private TabControl tabControl;
        internal void updatemode(string str, string p, int p_2,DataTable dt)
        {
            try
            {
              //  loadPage();
                companydt = dt;
                cnt = 1;
                ds = cl.getdata("select * from UserInfo where isActive=1 and CompanyId=" + str + "");
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        txthCompId.Text=str;
                        //txthUname.Text = dt.Rows[i].ItemArray[2].ToString();
                        listView1.Items.Add(dt.Rows[i].ItemArray[3].ToString());
                    }
                    listView1.Items[0].Selected = true;
                }
                    
                else
                {
                    cl.execute("insert into UserInfo values(1," + p + ",'admin','Super User','admin',1)");
                  //  sc.execute("insert into UserInfo values(1," + p + ",'Admin','Super User','Admin',1)");
                    ds = cl.getdata("select * from UserInfo where isActive=1 and UserName='admin' and CompanyId="+p+"");
                    dt = ds.Tables[0];
                    txthCompId.Text = p;
                    listView1.Items.Add(dt.Rows[0].ItemArray[3].ToString());
                    //UserInfo frm = new UserInfo(p);
                    //frm.Show();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            forpassword();
        }

        private void forpassword()
        {
            panel1.Visible = false;
            panel2.Visible = true;
            lblUName.Text = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text;
            txtPassword.Focus();
        }



        private void btnBack_Click(object sender, EventArgs e)
        {
            master.RemoveCurrentTab();
            CompanyList frm = new CompanyList(master,tabControl);
            master.AddNewTab(frm);
           // frm.Show();
        }

        private void btnBack1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            login();
            
        }

        private void login()
        {
            ds = cl.getdata("select * from UserInfo where isActive=1 and UserName='" + listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text + "' and CompanyId='" + txthCompId.Text + "'");
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][5].ToString() == txtPassword.Text)
                {

                    txtPassword.Text = "";
                    //         master = new Master(this);
                    try
                    {
                        master_1.setheader(companydt, dt.Rows[0]["UserName"].ToString());
                        master_1.enablemenu(true);
                        master_1.disablecompany(false);
                        master_1.compId(txthCompId.Text);
                    }
                    catch
                    {
                    }
                    try
                    {
                        master.setheader(companydt, dt.Rows[0]["UserName"].ToString());
                        master.enablemenu(true);
                        master.disablecompany(false);
                        master.compId(txthCompId.Text);
                    }
                    catch
                    {
                    }
                    //frm.Show();
                  //  this.Close();
                   
                    int tabs = tabControl.TabPages.Count;
                    if (tabs > 0)
                    {
                           master.RemoveCurrentTab();
                        Businessplus bus = new Businessplus();
                        master.AddNewTab(bus);
                    }
                }
                else
                {
                    MessageBox.Show("Password Incorrect. Try again!!!");
                    txtPassword.Text = "";
                }
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnNext.Focus();
            }
        }

        private void lblUName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            lblUName.Text = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text;
            txtPassword.Focus();
        }

        private void listView1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            forpassword();
        }

        private void listView1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                forpassword();
            }

        }

        private void UserLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Close();
        }

        private void lblUName_Enter(object sender, EventArgs e)
        {
            lblUName.BackColor = Color.LightYellow;
        }

        private void lblUName_Leave(object sender, EventArgs e)
        {
            lblUName.BackColor = Color.White;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.LightYellow;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.White;
        }

        private void btnBack1_MouseEnter(object sender, EventArgs e)
        {
            btnBack1.UseVisualStyleBackColor = false;
            btnBack1.BackColor = Color.FromArgb(128, 128, 128);
            btnBack1.ForeColor = Color.White;
        }

        private void btnBack1_MouseLeave(object sender, EventArgs e)
        {
            btnBack1.UseVisualStyleBackColor = true;
            btnBack1.BackColor = Color.FromArgb(51, 153, 255);
            btnBack1.ForeColor = Color.White;
        }

        private void btnBack_MouseEnter(object sender, EventArgs e)
        {
            btnBack.UseVisualStyleBackColor = false;
            btnBack.BackColor = Color.FromArgb(128, 128, 128);
            btnBack.ForeColor = Color.White;
        }

        private void btnBack_MouseLeave(object sender, EventArgs e)
        {
            btnBack.UseVisualStyleBackColor = true;
            btnBack.BackColor = Color.FromArgb(51, 153, 255);
            btnBack.ForeColor = Color.White;
        }

        private void btnNext_MouseEnter(object sender, EventArgs e)
        {
            btnNext.UseVisualStyleBackColor = false;
            btnNext.BackColor = Color.FromArgb(20,209,82);
            btnNext.ForeColor = Color.White;
        }

        private void btnNext_MouseLeave(object sender, EventArgs e)
        {
            btnNext.UseVisualStyleBackColor = true;
            btnNext.BackColor = Color.FromArgb(51, 153, 255);
            btnNext.ForeColor = Color.White;
        }

        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.BackColor = Color.YellowGreen;
            btnLogin.ForeColor = Color.White;
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.BackColor = Color.FromArgb(51, 153, 255);
            btnLogin.ForeColor = Color.White;
        }

        private void btnBack1_Enter(object sender, EventArgs e)
        {
            btnBack1.UseVisualStyleBackColor = false;
            btnBack1.BackColor = Color.FromArgb(128, 128, 128);
            btnBack1.ForeColor = Color.White;
        }

        private void btnBack1_Leave(object sender, EventArgs e)
        {
            btnBack1.UseVisualStyleBackColor = true;
            btnBack1.BackColor = Color.FromArgb(51, 153, 255);
            btnBack1.ForeColor = Color.White;
        }

        private void btnLogin_Enter(object sender, EventArgs e)
        {
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.BackColor = Color.YellowGreen;
            btnLogin.ForeColor = Color.White;
        }

        private void btnLogin_Leave(object sender, EventArgs e)
        {
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.BackColor = Color.FromArgb(51, 153, 255);
            btnLogin.ForeColor = Color.White;
        }

        private void btnBack_Enter(object sender, EventArgs e)
        {
            btnBack.UseVisualStyleBackColor = false;
            btnBack.BackColor = Color.FromArgb(128, 128, 128);
            btnBack.ForeColor = Color.White;
        }

        private void btnBack_Leave(object sender, EventArgs e)
        {
            btnBack.UseVisualStyleBackColor = true;
            btnBack.BackColor = Color.FromArgb(51, 153, 255);
            btnBack.ForeColor = Color.White;
        }

        private void btnNext_Enter(object sender, EventArgs e)
        {
            btnNext.UseVisualStyleBackColor = false;
            btnNext.BackColor = Color.FromArgb(20, 209, 82);
            btnNext.ForeColor = Color.White;
        }

        private void btnNext_Leave(object sender, EventArgs e)
        {
            btnNext.UseVisualStyleBackColor = true;
            btnNext.BackColor = Color.FromArgb(51, 153, 255);
            btnNext.ForeColor = Color.White;
        }

        
    }
}
