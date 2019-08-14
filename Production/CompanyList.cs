using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Production
{
    public partial class CompanyList : Form
    {
        Connection cl = new Connection();
        OleDbSettings ods = new OleDbSettings();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
       
        private Master master;
        public CompanyList()
        {
            InitializeComponent();
            listView1.Columns.Add("CompanyID",0, HorizontalAlignment.Center);
            listView1.Columns.Add("Company", 500, HorizontalAlignment.Left);
            listView1.Columns.Add("F.Y.From", 200, HorizontalAlignment.Center);
        }

        public CompanyList(Master master)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
         
            master.enablemenu(false);
            listView1.Columns.Add("CompanyID",0, HorizontalAlignment.Center);
            listView1.Columns.Add("Company", 500, HorizontalAlignment.Left);

            listView1.Columns.Add("F.Y.From", 200, HorizontalAlignment.Center);
        }

        public CompanyList(Master master, TabControl tabControl)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.master = master;
            this.tabControl = tabControl;
            master.enablemenu(false);
            listView1.Columns.Add("CompanyID",0, HorizontalAlignment.Center);
            listView1.Columns.Add("Company", 500, HorizontalAlignment.Left);

            listView1.Columns.Add("F.Y.From", 200, HorizontalAlignment.Center);

        }

        public CompanyList(TabControl tabControl)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.tabControl = tabControl;
            master.enablemenu(false);
            listView1.Columns.Add("CompanyID",0, HorizontalAlignment.Center);
            listView1.Columns.Add("Company", 500, HorizontalAlignment.Left);

            listView1.Columns.Add("F.Y.From", 200, HorizontalAlignment.Center);
        }

        private void CompanyList_Load(object sender, EventArgs e)
        {
            try
            {
                if (AddDatabasePath.newcompany == 1)
                {
                    btnNew.Enabled = false;
                }
                bindListView();
                //////  this.ActiveControl = listView1;
                //listView1.HideSelection = false;
                //listView1.Select();
                ////  listView1.Items[0].Checked = true;
                //listView1.FocusedItem = listView1.Items[0];
                //listView1.Items[0].Selected = true;
              //  listView1.Select();
              //  listView1.Items[0].Selected = true;
                listView1.Select();
                this.listView1.Items[0].Selected = true;
               
            }
            catch
            {
            }
           
           
         
        }

        private void bindListView()
        {
            try
            {
                //if (con.State == ConnectionState.Open)
                //{
                //    con.Close();
                //}
                //con.Open();
                ds = ods.getdata("Select * from Company");
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    
                    listView1.Items.Clear();
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        listView1.Items.Add(dt.Rows[i].ItemArray[1].ToString());
                        listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                        String year = Convert.ToDateTime(dt.Rows[i].ItemArray[22].ToString()).Year.ToString()+"-"+Convert.ToDateTime(dt.Rows[i].ItemArray[22].ToString()).AddYears(1).Year.ToString();
                        listView1.Items[i].SubItems.Add(year);
                    }
                 //   listView1.Items[0].Selected = true;
                   // listView1.Select();
                    
                }
                else
                {
                   // AddConString frm = new AddConString();
                   // frm.Show();
                }
             }

            catch (Exception ex)
            {
              //  MessageBox.Show("Error:" + ex.Message);
            }

            finally
            {
                //con.Close();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //this.Close();
               master.RemoveCurrentTab();
            InsertCompany frm = new InsertCompany(master,tabControl);
            master.AddNewTab(frm);
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.Show();
           // tabControl.TabPages.Remove(tabControl.SelectedTab);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                // this.Close();
                   master.RemoveCurrentTab();
                Application.Exit();

            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            openlogin();
           
            
        }
        UpdateSoftware us = new UpdateSoftware();
        private void openlogin()
        {

            String str = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text;
            string c = dt.Rows[listView1.FocusedItem.Index][29].ToString();

            UserLogin ul = new UserLogin(this, master,tabControl);
            //UserLogin ul1 = new UserLogin(master);
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["qry"].ConnectionString = dt.Rows[listView1.FocusedItem.Index][29].ToString();
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
            
            us.updatesof(connectionStringsSection.ConnectionStrings["qry"].ConnectionString);

            DataSet company = ods.getdata("select * from Company where CompanyID='" + dt.Rows[listView1.FocusedItem.Index][1].ToString() + "'"); 
            ul.updatemode(str, dt.Rows[listView1.FocusedItem.Index][1].ToString(), 1,company.Tables[0]);
               master.RemoveCurrentTab();
            master.AddNewTab(ul);
            
            //ul.Show();
         //   this.Close();
          
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
        int genr = 0;
        private TabControl tabControl;
        internal void Passed(int p)
        {
            genr = p;
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyData == Keys.Enter)
            {

                openlogin();
            }
        }

        private void CompanyList_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {

            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.BackColor = Color.FromArgb(248, 152, 94);
            btnCancel.ForeColor = Color.White;
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.BackColor = Color.FromArgb(51, 153, 255);
            btnCancel.ForeColor = Color.White;
        }

        private void btnNew_MouseEnter(object sender, EventArgs e)
        {
            btnNew.UseVisualStyleBackColor = false;
            btnNew.BackColor = Color.FromArgb(39,198,220);
            btnNew.ForeColor = Color.White;
        }

        private void btnNew_MouseLeave(object sender, EventArgs e)
        {
            btnNew.UseVisualStyleBackColor = true;
            btnNew.BackColor = Color.FromArgb(51, 153, 255);
            btnNew.ForeColor = Color.White;
        }

        private void btnNew_Enter(object sender, EventArgs e)
        {
            btnNew.UseVisualStyleBackColor = false;
            btnNew.BackColor = Color.FromArgb(39, 198, 220);
            btnNew.ForeColor = Color.White;
        }

        private void btnNew_Leave(object sender, EventArgs e)
        {
            btnNew.UseVisualStyleBackColor = true;
            btnNew.BackColor = Color.FromArgb(51, 153, 255);
            btnNew.ForeColor = Color.White;
        }

        private void btnCancel_Enter(object sender, EventArgs e)
        {
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.BackColor = Color.FromArgb(248, 152, 94);
            btnCancel.ForeColor = Color.White;
        }

        private void btnCancel_Leave(object sender, EventArgs e)
        {
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.BackColor = Color.FromArgb(51, 153, 255);
            btnCancel.ForeColor = Color.White;
        }
    }
}
