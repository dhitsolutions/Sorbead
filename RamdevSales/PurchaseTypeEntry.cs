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
    public partial class PurchaseTypeEntry : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection cn = new Connection();
        int purchaseid;
        DataTable userrights = new DataTable();
        public PurchaseTypeEntry()
        {
            InitializeComponent();
            bindgroup();
            bindtaxtype();

        }

        public PurchaseTypeEntry(PurchaseTypeMaster purchaseTypeMaster)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgroup();
            bindtaxtype();
            this.purchaseTypeMaster = purchaseTypeMaster;

        }

        //public PurchaseTypeEntry(Purchase purchase)
        //{
        //    // TODO: Complete member initialization
        //    InitializeComponent();
        //    bindgroup();
        //    bindtaxtype();
        //    purchaseid = 1;
        //    this.purchase = purchase;
        //}

        //public PurchaseTypeEntry(DefaultPurchase defaultPurchase)
        //{
        //    // TODO: Complete member initialization
        //    InitializeComponent();
        //    bindgroup();
        //    bindtaxtype();
        //    purchaseid = 1;
        //    this.defaultPurchase = defaultPurchase;
        //}

        public PurchaseTypeEntry(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgroup();
            bindtaxtype();
            purchaseid = 1;
            this.master = master;
            this.tabControl = tabControl;
        }

        //public PurchaseTypeEntry(DefaultPurchase defaultPurchase, Master master, TabControl tabControl)
        //{
        //    // TODO: Complete member initialization
        //    InitializeComponent();
        //    bindgroup();
        //    bindtaxtype();
        //    purchaseid = 1;
        //    this.defaultPurchase = defaultPurchase;
        //    this.master = master;
        //    this.tabControl = tabControl;
        //}

        //public PurchaseTypeEntry(Purchase purchase, Master master, TabControl tabControl)
        //{
        //    // TODO: Complete member initialization
        //    InitializeComponent();
        //    bindgroup();
        //    bindtaxtype();
        //    purchaseid = 1;
        //    this.purchase = purchase;
        //    this.master = master;
        //    this.tabControl = tabControl;
        //}

        public PurchaseTypeEntry(PurchaseTypeMaster purchaseTypeMaster, Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgroup();
            bindtaxtype();
            this.purchaseTypeMaster = purchaseTypeMaster;
            this.master = master;
            this.tabControl = tabControl;
        }
        public static string pvc;
        public PurchaseTypeEntry(DefaultSale defaultSale, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgroup();
            bindtaxtype();
            purchaseid = 1;
            this.defaultSale = defaultSale;
            this.master = master;
            this.tabControl = tabControl;
            pvc = activecontroal;
        }

        public PurchaseTypeEntry(DefaultSaleOrder defaultSaleOrder, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgroup();
            bindtaxtype();
            purchaseid = 1;
            this.defaultSaleOrder = defaultSaleOrder;
            this.master = master;
            this.tabControl = tabControl;
            pvc = activecontroal;
        }

        public PurchaseTypeEntry(SalePurchaseOrderSimpleformate salePurchaseOrderSimpleformate, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgroup();
            bindtaxtype();
            purchaseid = 1;
            this.salePurchaseOrderSimpleformate = salePurchaseOrderSimpleformate;
            this.master = master;
            this.tabControl = tabControl;
            pvc = activecontroal;
        }

        public PurchaseTypeEntry(Stockinout stockinout, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            //this.stockinout = stockinout;
            //this.master = master;
            //this.tabControl = tabControl;
            //this.activecontroal = activecontroal;
            InitializeComponent();
            bindgroup();
            bindtaxtype();
            purchaseid = 1;
            this.stockinout = stockinout;
            this.master = master;
            this.tabControl = tabControl;
            pvc = activecontroal;
        }

        public PurchaseTypeEntry(DefaultSalesorbead defaultSalesorbead, Master master, TabControl tabControl, string activecontroal)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            bindgroup();
            bindtaxtype();
            purchaseid = 1;
            this.defaultSalesorbead = defaultSalesorbead;
            this.master = master;
            this.tabControl = tabControl;
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
                        master.RemoveCurrentTab1(pvc, txtprintname.Text);
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
        private void PurchaseTypeEntry_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtprintname;
            //set the interval  and start the timer
            // timer1.Interval = 1000;
            // timer1.Start();
            userrights = cn.getdataset("Select * from UserRights where isactive=1 and uid='" + Master.userid + "'");
            if (userrights.Rows.Count > 0)
            {
                if (userrights.Rows[32]["a"].ToString() == "False")
                {
                    btnsave.Enabled = false;
                }
                if (userrights.Rows[32]["d"].ToString() == "False")
                {
                    btndelete.Enabled = false;
                }
            }
        }
        private void bindtaxtype()
        {
            DataTable dt = cn.getdataset("select * from taxtype where isactive=1");

            cmbxttype.ValueMember = "id";
            cmbxttype.DisplayMember = "taxtype";
            cmbxttype.DataSource = dt;
            cmbxttype.SelectedIndex = -1;
        }
        public void bindgroup()
        {
            DataTable dt = cn.getdataset("select * from accountgroup order by groupname asc");

            txtgrop.ValueMember = "id";
            txtgrop.DisplayMember = "groupname";
            txtgrop.DataSource = dt;
            txtgrop.SelectedIndex = -1;
            label11.Visible = false;
            // autobind(dt, cmbsaletype);
        }
        string id;
        private PurchaseTypeMaster purchaseTypeMaster;
        // private Purchase purchase;
        // private DefaultPurchase defaultPurchase;
        private DefaultSale defaultSale;
        private Master master;
        private TabControl tabControl;
        private DefaultSaleOrder defaultSaleOrder;
        public void inserttaxslab()
        {
            DataTable dt = cn.getdataset("SELECT DISTINCT Taxslabname FROM Taxslabmaster where isactive=1");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                DataTable dt2 = cn.getdataset("SELECT * FROM Taxslabmaster where isactive=1 and Taxslabname='" + dt.Rows[i]["Taxslabname"].ToString() + "' and saletypename='" + txtprintname.Text + "'");
                if (dt2.Rows.Count == 0)
                {
                    DataTable dt1 = cn.getdataset("SELECT * FROM Taxslabmaster where isactive=1 and Taxslabname='" + dt.Rows[i]["Taxslabname"].ToString() + "'");
                    DataTable saletypeid = cn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + txtprintname.Text + "'");
                    cn.execute("INSERT [dbo].[TaxSlabMaster] ([Taxslabname], [saletypename], [saletypeid], [system], [category], [sgst], [cgst], [igst], [additax], [onwhich], [isonmrp], [isonfreegoods], [isactive]) VALUES ('" + dt.Rows[i]["Taxslabname"].ToString() + "','" + txtprintname.Text + "','" + saletypeid.Rows[0]["Purchasetypeid"].ToString() + "','" + dt1.Rows[0]["system"].ToString() + "','" + dt1.Rows[0]["category"].ToString() + "','" + dt1.Rows[0]["sgst"].ToString() + "','" + dt1.Rows[0]["cgst"].ToString() + "','" + dt1.Rows[0]["igst"].ToString() + "','" + dt1.Rows[0]["additax"].ToString() + "','" + dt1.Rows[0]["onwhich"].ToString() + "','" + dt1.Rows[0]["isonmrp"].ToString() + "','" + dt1.Rows[0]["isonfreegoods"].ToString() + "','1')");
                }
            }
        }
        public void submit()
        {
            try
            {
                if (txtprintname.Text == "")
                {
                    MessageBox.Show("Enter Purchase Type");
                    txtprintname.Focus();
                    return;

                }
                else
                {
                    if (btnsave.Text == "Update")
                    {
                        string chkecom1;
                        if (chkecom.Checked == true)
                        {
                            chkecom1 = "1";
                        }
                        else
                        {
                            chkecom1 = "0";
                        }
                        cn.execute("UPDATE [dbo].[PurchasetypeMaster]SET [Purchasetypename] = '" + txtprintname.Text + "',[Groupid] = '" + txtgrop.SelectedValue + "',[taxtypeid] = '" + cmbxttype.SelectedValue + "',[TaxTypename] = '" + cmbxttype.Text + "',[Region] = '" + cmbregion.Text + "',[type]='P',[Prefix]='" + txtinvoiceprefix.Text + "',[startingno]='" + txtstartfrom.Text + "',TaxCalculation='" + cmbtaxcal.Text + "',PickupPrice='" + cmbbtdefault.Text + "',InvoiceHeading='" + txtinvoiceheading.Text + "',FormType='" + cmbformtype.Text + "',chkecom='" + chkecom1 + "',txtecom='" + txtecom.Text + "',[isactive] = 1 WHERE purchasetypename='" + id + "'");
                        MessageBox.Show("Update Successfully");
                        DialogResult dr = MessageBox.Show("Do you want to Include This Sale Type/Purchase Type To All Tax Slab?", "Tax Slab", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            inserttaxslab();
                        }
                        try
                        {
                            //  purchase.bindsaletype();
                        }
                        catch
                        {
                        }
                        try
                        {
                            //  defaultPurchase.bindsaletype();
                        }
                        catch
                        {
                        }
                        try
                        {
                            defaultSale.bindsaletype();
                        }
                        catch
                        {
                        }
                        try
                        {
                            defaultSaleOrder.bindsaletype();
                        }
                        catch
                        {
                        }
                        try
                        {
                            stockinout.bindsaletype();
                        }
                        catch
                        {
                        }
                        try
                        {
                            salePurchaseOrderSimpleformate.bindsaletype();
                        }
                        catch
                        {
                        }
                        if (string.IsNullOrEmpty(pvc) == true)
                        {
                            master.RemoveCurrentTab();
                        }
                        else
                        {
                            master.RemoveCurrentTab1(pvc, txtprintname.Text);
                        }
                        clear();
                    }
                    else
                    {
                        string chkecom1;
                        if (chkecom.Checked == true)
                        {
                            chkecom1 = "1";
                        }
                        else
                        {
                            chkecom1 = "0";
                        }

                        SqlCommand cmdRecordExist = new SqlCommand("SELECT * FROM PurchasetypeMaster WHERE isactive=1 AND Purchasetypename='" + txtprintname.Text + "' AND FormType='" + cmbformtype.Text + "'", con);
                        SqlDataAdapter sdaRecordExist = new SqlDataAdapter(cmdRecordExist);
                        DataTable recordExistDt = new DataTable();
                        sdaRecordExist.Fill(recordExistDt);

                        if (recordExistDt.Rows.Count > 0)
                        {
                            MessageBox.Show("Purchase Type  " + txtprintname.Text + " is already exist");
                            clear();
                            return;
                        }
                        else
                        {
                            cn.execute("INSERT INTO [dbo].[PurchasetypeMaster]([Purchasetypename],[Groupid],[taxtypeid],[TaxTypename],[Region],[isactive],[Prefix],[startingno],[TaxCalculation],[PickupPrice],[InvoiceHeading],[FormType],[chkecom],[txtecom],[type])VALUES('" + txtprintname.Text + "','" + txtgrop.SelectedValue + "','" + cmbxttype.SelectedValue + "','" + cmbxttype.Text + "','" + cmbregion.Text + "','" + 1 + "','" + txtinvoiceprefix.Text + "','" + txtstartfrom.Text + "','" + cmbtaxcal.Text + "','" + cmbbtdefault.Text + "','" + txtinvoiceheading.Text + "','" + cmbformtype.Text + "','" + chkecom1 + "','" + txtecom.Text + "','P')");
                            MessageBox.Show("Insert Successfully");
                            DialogResult dr = MessageBox.Show("Do you want to Include This Sale Type To All Tax Slab?", "Tax Slab", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                            {
                                inserttaxslab();
                            }
                            try
                            {
                                //   purchase.bindsaletype();
                            }
                            catch
                            {
                            }
                            try
                            {
                                //  defaultPurchase.bindsaletype();
                            }
                            catch
                            {
                            }
                            try
                            {
                                defaultSale.bindsaletype();
                            }
                            catch
                            {
                            }
                            try
                            {
                                defaultSaleOrder.bindsaletype();
                            }
                            catch
                            {
                            }
                            try
                            {
                                stockinout.bindsaletype();
                            }
                            catch
                            {
                            }
                            try
                            {
                                salePurchaseOrderSimpleformate.bindsaletype();
                            }
                            catch
                            {
                            }
                            if (string.IsNullOrEmpty(pvc) == true)
                            {
                                master.RemoveCurrentTab();
                            }
                            else
                            {
                                master.RemoveCurrentTab1(pvc, txtprintname.Text);
                            }
                            clear();
                        }
                    }
                }
            }
            catch
            {
            }
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            submit();

        }
        public void clear()
        {
            txtgrop.Text = "";
            txtprintname.Text = "";
            cmbregion.Text = "";
            cmbxttype.Text = "";
            txtstartfrom.Text = "";
            txtinvoiceprefix.Text = "";
            cmbbtdefault.Text = "";
            txtinvoiceheading.Text = "";
            cmbtaxcal.Text = "";
            txtgrop.SelectedIndex = -1;
            cmbxttype.SelectedIndex = -1;
            cmbregion.SelectedIndex = -1;
            cmbbtdefault.SelectedIndex = -1;
            cmbtaxcal.SelectedIndex = -1;
            cmbformtype.SelectedIndex = -1;
        }
        private void btnreset_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (txtprintname.Text == "" || txtprintname.Text == null)
            {
                MessageBox.Show("Select Invoice Type Name");
            }
            else
            {
                cn.execute("update purchasetypemaster set isactive=0 where purchasetypename='" + txtprintname.Text + "' and type='P'");
                MessageBox.Show("Delete successfully");
                clear();
            }

        }

        private void btncancel_Click(object sender, EventArgs e)
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
                    master.RemoveCurrentTab1(pvc, txtprintname.Text);
                }
            }
        }

        private void txtprintname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //  txtgrop.Focus();
                cmbformtype.Focus();

            }
        }

        private void txtgrop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < txtgrop.Items.Count; i++)
                {
                    s = txtgrop.GetItemText(txtgrop.Items[i]);
                    if (s == txtgrop.Text)
                    {
                        inList = true;
                        txtgrop.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtgrop.Text = "";
                }
                cmbxttype.Focus();

            }
        }

        private void cmbxttype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbxttype.Items.Count; i++)
                {
                    s = cmbxttype.GetItemText(cmbxttype.Items[i]);
                    if (s == cmbxttype.Text)
                    {
                        inList = true;
                        cmbxttype.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbxttype.Text = "";
                }
                cmbregion.Focus();
            }
        }

        private void cmbregion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbregion.Items.Count; i++)
                {
                    s = cmbregion.GetItemText(cmbregion.Items[i]);
                    if (s == cmbregion.Text)
                    {
                        inList = true;
                        cmbregion.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbregion.Text = "";
                }
                // txtinvoiceprefix.Focus();
                cmbtaxcal.Focus();
            }
        }



        internal void updatemode(string str, string p, string p_2)
        {
            try
            {
                id = p;
                // DataTable dt = cn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and purchasetypename='" + p + "'");
                DataTable dt = cn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and purchasetypename='" + p + "' and FormType='" + p_2 + "'");
                if (dt.Rows[0]["chkecom"].ToString() == "True")
                {
                    chkecom.Checked = true;
                    label11.Visible = true;
                    txtecom.Text = dt.Rows[0]["txtecom"].ToString();
                }
                else
                {
                    chkecom.Checked = false;
                    txtecom.Visible = false;
                    label11.Visible = false;
                }
                txtgrop.Text = str;
                txtprintname.Text = dt.Rows[0]["Purchasetypename"].ToString();
                cmbxttype.Text = dt.Rows[0]["TaxTypename"].ToString();
                cmbregion.Text = dt.Rows[0]["Region"].ToString();
                txtinvoiceprefix.Text = dt.Rows[0]["Prefix"].ToString();
                txtstartfrom.Text = dt.Rows[0]["startingno"].ToString();
                cmbtaxcal.Text = dt.Rows[0]["TaxCalculation"].ToString();
                txtinvoiceheading.Text = dt.Rows[0]["InvoiceHeading"].ToString();
                cmbbtdefault.Text = dt.Rows[0]["PickupPrice"].ToString();
                cmbformtype.Text = dt.Rows[0]["FormType"].ToString();
                btnsave.Text = "Update";
            }
            catch
            {
            }
        }

        private void txtinvoiceprefix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtstartfrom.Focus();
            }
        }

        private void txtstartfrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //  cmbtaxcal.Focus();
                txtinvoiceheading.Focus();
            }
        }



        private void cmbtaxcal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbtaxcal.Items.Count; i++)
                {
                    s = cmbtaxcal.GetItemText(cmbtaxcal.Items[i]);
                    if (s == cmbtaxcal.Text)
                    {
                        inList = true;
                        cmbtaxcal.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbtaxcal.Text = "";
                }
                // txtinvoiceheading.Focus();
                chkecom.Focus();
            }
        }

        private void txtinvoiceheading_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbbtdefault.Focus();
            }
        }

        private void cmbbtdefault_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbbtdefault.Items.Count; i++)
                {
                    s = cmbbtdefault.GetItemText(cmbbtdefault.Items[i]);
                    if (s == cmbbtdefault.Text)
                    {
                        inList = true;
                        cmbbtdefault.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbbtdefault.Text = "";
                }
                //cmbformtype.Focus();
                btnsave.Focus();
            }
        }

        private void txtgrop_Enter(object sender, EventArgs e)
        {
            txtgrop.SelectedIndex = 0;
            txtgrop.DroppedDown = true;

        }

        private void cmbxttype_Enter(object sender, EventArgs e)
        {
            cmbxttype.SelectedIndex = 0;
            cmbxttype.DroppedDown = true;

        }

        private void cmbregion_Enter(object sender, EventArgs e)
        {
            cmbregion.SelectedIndex = 0;
            cmbregion.DroppedDown = true;

        }

        private void cmbtaxcal_Enter(object sender, EventArgs e)
        {
            cmbtaxcal.SelectedIndex = 0;
            cmbtaxcal.DroppedDown = true;

        }

        private void cmbbtdefault_Enter(object sender, EventArgs e)
        {
            cmbbtdefault.SelectedIndex = 0;
            cmbbtdefault.DroppedDown = true;

        }

        private void cmbformtype_Enter(object sender, EventArgs e)
        {
            cmbformtype.SelectedIndex = 0;
            cmbformtype.DroppedDown = true;

        }

        public static string s;
        private void cmbformtype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbformtype.Items.Count; i++)
                {
                    s = cmbformtype.GetItemText(cmbformtype.Items[i]);
                    if (s == cmbformtype.Text)
                    {
                        inList = true;
                        cmbformtype.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbformtype.Text = "";
                }
                //chkecom.Focus();
                // btnsave.Focus();
                txtgrop.Focus();
            }
        }

        private void txtstartfrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtprintname_Enter(object sender, EventArgs e)
        {
            txtprintname.BackColor = Color.LightYellow;
        }

        private void txtprintname_Leave(object sender, EventArgs e)
        {
            txtprintname.BackColor = Color.White;
        }

        private void txtgrop_Leave(object sender, EventArgs e)
        {
            txtgrop.Text = s;
            // txtgrop.ForeColor = Color.White;
        }

        private void cmbregion_Leave(object sender, EventArgs e)
        {
            cmbregion.Text = s;
            //cmbregion.ForeColor = Color.White;
        }

        private void txtinvoiceprefix_Enter(object sender, EventArgs e)
        {
            txtinvoiceprefix.BackColor = Color.LightYellow;
        }

        private void txtinvoiceprefix_Leave(object sender, EventArgs e)
        {
            txtinvoiceprefix.BackColor = Color.White;
        }

        private void txtstartfrom_Enter(object sender, EventArgs e)
        {
            txtstartfrom.BackColor = Color.LightYellow;
        }

        private void txtstartfrom_Leave(object sender, EventArgs e)
        {
            txtstartfrom.BackColor = Color.White;
        }

        private void cmbtaxcal_Leave(object sender, EventArgs e)
        {
            cmbtaxcal.Text = s;
        }

        private void txtinvoiceheading_Enter(object sender, EventArgs e)
        {
            txtinvoiceheading.BackColor = Color.LightYellow;
        }

        private void txtinvoiceheading_Leave(object sender, EventArgs e)
        {
            txtinvoiceheading.BackColor = Color.White;
        }

        private void cmbbtdefault_Leave(object sender, EventArgs e)
        {
            cmbbtdefault.Text = s;
        }

        private void cmbformtype_Leave(object sender, EventArgs e)
        {
            cmbformtype.Text = s;
        }

        private void chkecom_CheckedChanged(object sender, EventArgs e)
        {
            if (chkecom.Checked == true)
            {
                txtecom.Visible = true;
                label11.Visible = true;
            }
            else
            {
                txtecom.Visible = false;
                label11.Visible = false;
            }
        }

        private void chkecom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chkecom.Checked == true)
                {
                    txtecom.Focus();
                }
                else
                {
                    txtinvoiceprefix.Focus();
                }
            }
        }

        private void txtecom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsave.Focus();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnsave_MouseEnter(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = false;
            btnsave.BackColor = Color.YellowGreen;
            btnsave.ForeColor = Color.White;
        }

        private void btnsave_MouseLeave(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = true;
            btnsave.BackColor = Color.FromArgb(51, 153, 255);
            btnsave.ForeColor = Color.White;
        }

        private void btnreset_MouseEnter(object sender, EventArgs e)
        {
            btnreset.UseVisualStyleBackColor = false;
            btnreset.BackColor = Color.FromArgb(250, 185, 34);
            btnreset.ForeColor = Color.White;
        }

        private void btnreset_MouseLeave(object sender, EventArgs e)
        {
            btnreset.UseVisualStyleBackColor = true;
            btnreset.BackColor = Color.FromArgb(51, 153, 255);
            btnreset.ForeColor = Color.White;
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

        private void btnsave_Enter(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = false;
            btnsave.BackColor = Color.YellowGreen;
            btnsave.ForeColor = Color.White;
        }

        private void btnsave_Leave(object sender, EventArgs e)
        {
            btnsave.UseVisualStyleBackColor = true;
            btnsave.BackColor = Color.FromArgb(51, 153, 255);
            btnsave.ForeColor = Color.White;
        }

        private void btnreset_Enter(object sender, EventArgs e)
        {
            btnreset.UseVisualStyleBackColor = false;
            btnreset.BackColor = Color.FromArgb(250, 185, 34);
            btnreset.ForeColor = Color.White;
        }

        private void btnreset_Leave(object sender, EventArgs e)
        {
            btnreset.UseVisualStyleBackColor = true;
            btnreset.BackColor = Color.FromArgb(51, 153, 255);
            btnreset.ForeColor = Color.White;
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
        string searchstr;
        private SalePurchaseOrderSimpleformate salePurchaseOrderSimpleformate;
        private string activecontroal;
        private Stockinout stockinout;
        private DefaultSalesorbead defaultSalesorbead;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
            // searchstr = "";
        }

        private void cmbformtype_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbformtype.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbformtype.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void txtgrop_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = txtgrop.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            txtgrop.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbxttype_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbxttype.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbxttype.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbregion_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbregion.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbregion.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbtaxcal_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbtaxcal.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbtaxcal.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbbtdefault_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbbtdefault.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbbtdefault.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbxttype_Leave(object sender, EventArgs e)
        {
            cmbxttype.Text = s;
        }

        private void cmbformtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbformtype.Items.Count; i++)
                {
                    s = cmbformtype.GetItemText(cmbformtype.Items[i]);
                    if (s == cmbformtype.Text)
                    {
                        inList = true;
                        cmbformtype.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbformtype.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void txtgrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < txtgrop.Items.Count; i++)
                {
                    s = txtgrop.GetItemText(txtgrop.Items[i]);
                    if (s == txtgrop.Text)
                    {
                        inList = true;
                        txtgrop.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    txtgrop.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbxttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbxttype.Items.Count; i++)
                {
                    s = cmbxttype.GetItemText(cmbxttype.Items[i]);
                    if (s == cmbxttype.Text)
                    {
                        inList = true;
                        cmbxttype.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbxttype.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbregion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbregion.Items.Count; i++)
                {
                    s = cmbregion.GetItemText(cmbregion.Items[i]);
                    if (s == cmbregion.Text)
                    {
                        inList = true;
                        cmbregion.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbregion.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbtaxcal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbtaxcal.Items.Count; i++)
                {
                    s = cmbtaxcal.GetItemText(cmbtaxcal.Items[i]);
                    if (s == cmbtaxcal.Text)
                    {
                        inList = true;
                        cmbtaxcal.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbtaxcal.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbbtdefault_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbbtdefault.Items.Count; i++)
                {
                    s = cmbbtdefault.GetItemText(cmbbtdefault.Items[i]);
                    if (s == cmbbtdefault.Text)
                    {
                        inList = true;
                        cmbbtdefault.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbbtdefault.Text = "";
                }
            }
            catch (Exception excp)
            {
            }
        }


    }
}
