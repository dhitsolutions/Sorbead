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

namespace RamdevSales
{
    public partial class FinishedGoods : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        public FinishedGoods()
        {
            InitializeComponent();
        }

        public FinishedGoods(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        public FinishedGoods(FinishedGoodsList finishedGoodsList, Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.finishedGoodsList = finishedGoodsList;
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
        public static string proid, batch;
        public void bindbatch()
        {
            try
            {
                proid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and Product_Name='" + cmbproductitem.Text + "'");
                SqlCommand cmd = new SqlCommand("select ProductID,Batchno from ProductPriceMaster where isactive=1 and Productid='" + proid + "' order by Batchno", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt11 = new DataTable();
                sda.Fill(dt11);

                cmbbatch.ValueMember = "ProductID";
                cmbbatch.DisplayMember = "Batchno";
                cmbbatch.DataSource = dt11;
                cmbbatch.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        public void binditem()
        {
            SqlCommand cmd = new SqlCommand("select id,mainitemname from tblproductionmaster where isactive=1 and isfinished=0", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt11 = new DataTable();
            sda.Fill(dt11);

            cmbproductitem.ValueMember = "id";
            cmbproductitem.DisplayMember = "mainitemname";
            cmbproductitem.DataSource = dt11;
            cmbproductitem.SelectedIndex = -1;
        }
        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        int cnt = 0;
        DataTable userrights = new DataTable();
        private void FinishedGoods_Load(object sender, EventArgs e)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (cnt == 0)
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[19]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                        if (userrights.Rows[19]["a"].ToString() == "False")
                        {
                            btnsubmit.Enabled = false;
                        }
                    }
                    this.ActiveControl = cmbproductitem;
                    binditem();
                }
                else
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[19]["u"].ToString() == "False")
                        {
                            btnsubmit.Enabled = false;
                        }
                        if (userrights.Rows[19]["d"].ToString() == "False")
                        {
                            btndelete.Enabled = false;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void cmbproductitem_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbproductitem.SelectedIndex = 0;
                cmbproductitem.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbproductitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool inList = false;
            for (int i = 0; i < cmbproductitem.Items.Count; i++)
            {
                s = cmbproductitem.GetItemText(cmbproductitem.Items[i]);
                if (s == cmbproductitem.Text)
                {
                    inList = true;
                    cmbproductitem.Text = s;
                    break;
                }
            }
            if (!inList)
            {
                cmbproductitem.Text = "";
            }
        }
        public static string s;
        private FinishedGoodsList finishedGoodsList;
        private void cmbproductitem_Leave(object sender, EventArgs e)
        {
            cmbproductitem.Text = s;
        }
        public void bindprodata()
        {
            try
            {
                DataTable dt = conn.getdataset("select * from tblproductionmaster where isactive=1 and id='" + cmbproductitem.SelectedValue + "'");
                txtaqty.Text = dt.Rows[0]["mqty"].ToString();
                txtaltqty.Text = dt.Rows[0]["maqty"].ToString();

            }
            catch
            {
            }
        }
        private void cmbproductitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbproductitem.Items.Count; i++)
                {
                    s = cmbproductitem.GetItemText(cmbproductitem.Items[i]);
                    if (s == cmbproductitem.Text)
                    {
                        inList = true;
                        cmbproductitem.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbproductitem.Text = "";
                }
                if (btnsubmit.Text != "Update")
                {
                    bindprodata();
                }
                bindbatch();
                cmbbatch.Focus();
            }
        }

        private void txtfqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtremarks.Focus();
            }
        }

        private void txtremarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsubmit.Focus();
            }
        }

        private void btnsubmit_Enter(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = false;
            btnsubmit.BackColor = Color.YellowGreen;
            btnsubmit.ForeColor = Color.White;
        }

        private void btnsubmit_Leave(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = true;
            btnsubmit.BackColor = Color.FromArgb(51, 153, 255);
            btnsubmit.ForeColor = Color.White;
        }

        private void btnsubmit_MouseEnter(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = false;
            btnsubmit.BackColor = Color.YellowGreen;
            btnsubmit.ForeColor = Color.White;
        }

        private void btnsubmit_MouseLeave(object sender, EventArgs e)
        {
            btnsubmit.UseVisualStyleBackColor = true;
            btnsubmit.BackColor = Color.FromArgb(51, 153, 255);
            btnsubmit.ForeColor = Color.White;
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
        public void clearall()
        {
            cmbproductitem.SelectedIndex = -1;
            cmbbatch.SelectedIndex = -1;
            txtaqty.Text = "";
            txtfqty.Text = "";
            txtremarks.Text = "";
            txtaltqty.Text = "";
            btnsubmit.Text = "Submit";
        }
        public void binddata()
        {
            try
            {
                DataTable batchno = conn.getdataset("Select * from tblproductionmaster where isactive=1 and id='" + cmbproductitem.SelectedValue + "'");
                if (btnsubmit.Text == "Update")
                {
                    if (userrights.Rows.Count > 0)
                    {
                        if (userrights.Rows[19]["u"].ToString() == "True")
                        {
                            //   proid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and Product_Name='" + cmbproductitem.Text + "'");
                            conn.execute("Update tblfinishedgoodsqty set batchno='" + cmbbatch.Text + "',productid='" + proid + "', proitem='" + cmbproductitem.Text + "',aqty='" + txtaqty.Text + "',altqty='" + txtaltqty.Text + "',fqty='" + txtfqty.Text + "',remarks='" + txtremarks.Text + "' Where proitemid='" + lblid.Text + "'");
                            conn.execute("Update ProductPriceMaster set Productid='" + batchno.Rows[0]["mainitemid"].ToString() + "',Batchno='" + batchno.Rows[0]["proidmanual"].ToString() + "',OpStock='" + txtfqty.Text + "' where Productid='" + batchno.Rows[0]["mainitemid"].ToString() + "' and Batchno='" + batchno.Rows[0]["proidmanual"].ToString() + "'");
                            MessageBox.Show("Data Update Successfully.");
                            clearall();
                        }
                        else
                        {
                            MessageBox.Show("You don't have Permission To Update");
                            return;
                        }
                    }
                }
                else
                {
                    //  proid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and Product_Name='" + cmbproductitem.Text + "'");
                    conn.execute("INSERT INTO [dbo].[tblfinishedgoodsqty]([proitemid],[proitem],[aqty],[altqty],[fqty],[remarks],[isactive],[productid],[batchno])VALUES('" + cmbproductitem.SelectedValue + "','" + cmbproductitem.Text + "','" + txtaqty.Text + "','" + txtaltqty.Text + "','" + txtfqty.Text + "','" + txtremarks.Text + "','" + "1" + "','" + proid + "','" + cmbbatch.Text + "')");
                    conn.execute("Update tblproductionmaster set isfinished=1 where id='" + cmbproductitem.SelectedValue + "'");
                    conn.execute("INSERT INTO [dbo].[ProductPriceMaster]([Productid],[Batchno],[BasicPrice],[SalePrice],[MRP],[PurchasePrice],[OpStock],[SelfVal],[minsaleprice],[oploose],[opstockval],[isactive]) VALUES('" + batchno.Rows[0]["mainitemid"].ToString() + "','" + batchno.Rows[0]["proidmanual"].ToString() + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "0" + "','" + txtfqty.Text + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "1" + "')");
                    MessageBox.Show("Data Inserted Successfully.");
                    clearall();
                }
            }
            catch
            {
            }
        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            binddata();
        }


        internal void Updatedata(string iid)
        {
            try
            {
                userrights = conn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
                if (userrights.Rows.Count > 0)
                {
                    if (userrights.Rows[19]["d"].ToString() == "False")
                    {
                        btndelete.Enabled = false;
                    }
                }
                cnt = 1;
                //binditem();
                SqlCommand cmd = new SqlCommand("select id,mainitemname from tblproductionmaster where isactive=1 and isfinished=1", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt11 = new DataTable();
                sda.Fill(dt11);

                cmbproductitem.ValueMember = "id";
                cmbproductitem.DisplayMember = "mainitemname";
                cmbproductitem.DataSource = dt11;
                cmbproductitem.SelectedIndex = -1;
                DataTable d = conn.getdataset("select proitem,aqty,altqty,fqty,remarks,proitemid from tblfinishedgoodsqty where isactive=1 and proitemid='" + iid + "'");
                cmbproductitem.Text = d.Rows[0]["proitem"].ToString();
                txtaqty.Text = d.Rows[0]["aqty"].ToString();
                txtaltqty.Text = d.Rows[0]["altqty"].ToString();
                txtfqty.Text = d.Rows[0]["fqty"].ToString();
                txtremarks.Text = d.Rows[0]["remarks"].ToString();
                lblid.Text = d.Rows[0]["proitemid"].ToString();
                btnsubmit.Text = "Update";

                this.ActiveControl = cmbproductitem;

            }
            catch
            {
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            DialogResult dr1 = MessageBox.Show("Do you want to Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr1 == DialogResult.Yes)
            {
                conn.execute("Update tblfinishedgoodsqty set isactive='0' where proitemid='" + lblid.Text + "'");
                conn.execute("Update tblproductionmaster set isfinished=0 where id='" + lblid.Text + "'");
                MessageBox.Show("Data Delete Successfully.");
                clearall();
            }

        }

        private void cmbbatch_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbbatch.SelectedIndex = 0;
                cmbbatch.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbbatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool inList = false;
            for (int i = 0; i < cmbbatch.Items.Count; i++)
            {
                s = cmbbatch.GetItemText(cmbbatch.Items[i]);
                if (s == cmbbatch.Text)
                {
                    inList = true;
                    cmbbatch.Text = s;
                    break;
                }
            }
            if (!inList)
            {
                cmbbatch.Text = "";
            }
        }

        private void cmbbatch_Leave(object sender, EventArgs e)
        {
            cmbbatch.Text = s;
        }

        private void cmbbatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbbatch.Items.Count; i++)
                {
                    s = cmbbatch.GetItemText(cmbbatch.Items[i]);
                    if (s == cmbbatch.Text)
                    {
                        inList = true;
                        cmbbatch.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbbatch.Text = "";
                }
                txtfqty.Focus();
            }
        }
    }
}
