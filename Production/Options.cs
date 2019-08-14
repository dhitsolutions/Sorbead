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
    public partial class Options : Form
    {
        Connection conn = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        int flag;
        private Master master;
        private TabControl tabControl;
        DataTable dt = new DataTable();
        public Options()
        {
            InitializeComponent();
        }

        public Options(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        private void Options_Load(object sender, EventArgs e)
        {
             dt = conn.getdataset("select * from options");
            flag = 1;
            if (dt.Rows.Count > 0)
            {
                autoroundoffpurchase.Checked = Convert.ToBoolean(dt.Rows[0]["autoroundoffpurchase"].ToString());
                showcustomersupplierseperatly.Checked = Convert.ToBoolean(dt.Rows[0]["showcustomersupplierseperate"].ToString());
                autoroundoffsale.Checked = Convert.ToBoolean(dt.Rows[0]["autoroundoffsale"].ToString());
                chkitemsinasedingorderinsale.Checked = Convert.ToBoolean(dt.Rows[0]["itemsinasedingorderinsale"].ToString());
                chkround.Checked = Convert.ToBoolean(dt.Rows[0]["autoroundoffpos"].ToString());
                chkcess.Checked = Convert.ToBoolean(dt.Rows[0]["cess"].ToString());
                chksaledialog.Checked = Convert.ToBoolean(dt.Rows[0]["savedialogbox"].ToString());
                chkcusreq.Checked= Convert.ToBoolean(dt.Rows[0]["requiredcustomerdetailinpos"].ToString());
                chkside.Checked = Convert.ToBoolean(dt.Rows[0]["showsidebox"].ToString());
                chkmultiprint.Checked = Convert.ToBoolean(dt.Rows[0]["multyprintinpos"].ToString());
                chkprintpopup.Checked = Convert.ToBoolean(dt.Rows[0]["requirprintpopupinpos"].ToString());
                chkallitemlist.Checked = Convert.ToBoolean(dt.Rows[0]["showallitemlistinpos"].ToString());
                chkinvoice.Checked = Convert.ToBoolean(dt.Rows[0]["invoicenoinpos"].ToString());
                chkagentnameshowinpos.Checked = Convert.ToBoolean(dt.Rows[0]["showagentnameinsale"].ToString());
                //set the interval  and start the timer
               // timer1.Interval = 1000;
               // timer1.Start();
            }
            DataTable dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='P'");
            binddrop(cmbpurchase, dt1, lbldefaultpurchase);
            dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='S'");
            binddrop(cmbsale, dt1, lbldefaultsale);
            dt1 = conn.getdataset("select * from FormFormat where isactive=1 and type='POS'");
            binddrop(cmbpos, dt1, lbldefaultpos);

            flag = 0;
            DataTable tax = conn.getdataset("select * from Options");
            if (tax.Rows[0]["countryName"].ToString() == "" && tax.Rows[0]["StateName"].ToString() == "" && tax.Rows[0]["Taxation"].ToString() == "")
            {
                bindtaxdropdown();
            }
            else
            {
                bindtaxdropdown();
                ddlcontry.Text = tax.Rows[0]["countryName"].ToString();
                ddlstate.Text = tax.Rows[0]["StateName"].ToString();
                ddltaxation.Text = tax.Rows[0]["Taxation"].ToString();
                lblcountry.Text = tax.Rows[0]["countryName"].ToString();
                lblstate.Text = tax.Rows[0]["StateName"].ToString();
                lbltaxation.Text = tax.Rows[0]["Taxation"].ToString();
                lblbydefault.Text = tax.Rows[0]["DefaultPrice"].ToString();
                ddlbydefault.Text = tax.Rows[0]["DefaultPrice"].ToString();
                lbldefaultsaletype.Text = tax.Rows[0]["autosaletype"].ToString();
                ddldefaultsaletype.Text = tax.Rows[0]["autosaletype"].ToString();
                ddldefaultbill.Text = tax.Rows[0]["defaultbill"].ToString();
                lbldefaultbill.Text = tax.Rows[0]["defaultbill"].ToString();
                ddldateformat.Text = tax.Rows[0]["dateformate"].ToString();
                lbldateformat.Text = tax.Rows[0]["dateformate"].ToString();
               // cmbs.Text = tax.Rows[0]["salevoucherno"].ToString();
                lblsale.Text = tax.Rows[0]["salevoucherno"].ToString();
                //cmbsr.Text = tax.Rows[0]["salervoucherno"].ToString();
                lblsalereturn.Text = tax.Rows[0]["salervoucherno"].ToString();
                //cmbp.Text = tax.Rows[0]["purchasevoucherno"].ToString();
                lblpurchase.Text = tax.Rows[0]["purchasevoucherno"].ToString();
                //cmbpr.Text = tax.Rows[0]["purchaservoucherno"].ToString();
                lblpurchasereturn.Text = tax.Rows[0]["purchaservoucherno"].ToString();
                lblsaleorder.Text = tax.Rows[0]["saleovoucherno"].ToString();
                lblsalechallan.Text = tax.Rows[0]["salecvoucherno"].ToString();
                lblpurchaseorder.Text = tax.Rows[0]["purchaseovoucherno"].ToString();
                lblpurchasechallan.Text = tax.Rows[0]["purchasecvoucherno"].ToString();
                cmbsalebills.Text = tax.Rows[0]["noofcopyofsalebills"].ToString();
                cmbsaleorder.Text = tax.Rows[0]["noofcopyofsaleorderbills"].ToString();
                cmbsalereturn.Text = tax.Rows[0]["noofcopyofsalereturnbills"].ToString();
                cmbsalechallan.Text = tax.Rows[0]["noofcopyofsalechallanbills"].ToString();
                cmbagent.Text = tax.Rows[0]["requiragentnameinpos"].ToString();
                lblagentsectioninpos.Text = tax.Rows[0]["requiragentnameinpos"].ToString();
              //  cmbpos1 .Text = tax.Rows[0]["posbillno"].ToString();
                lblpos.Text = tax.Rows[0]["posbillno"].ToString();
                lblproduction.Text = tax.Rows[0]["productionidtype"].ToString();

            }

        }
        public void bindtaxdropdown()
        {

            SqlCommand cmddept = new SqlCommand("select id,System from System_Master", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmddept);
            DataTable dtdept1 = new DataTable();
            sda.Fill(dtdept1);

            ddltaxation.ValueMember = "id";
            ddltaxation.DisplayMember = "System";
            ddltaxation.DataSource = dtdept1;
            ddltaxation.SelectedIndex = -1;

            SqlCommand cmddept1 = new SqlCommand("select id,CountryName from Country_Master", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmddept1);
            DataTable dtdept11 = new DataTable();
            sda1.Fill(dtdept11);

            ddlcontry.ValueMember = "id";
            ddlcontry.DisplayMember = "CountryName";
            ddlcontry.DataSource = dtdept11;
            ddlcontry.SelectedIndex = -1;

            SqlCommand cmddept11 = new SqlCommand("select Purchasetypeid,Purchasetypename from PurchasetypeMaster", con);
            SqlDataAdapter sda11 = new SqlDataAdapter(cmddept11);
            DataTable dtdept111 = new DataTable();
            sda11.Fill(dtdept111);

            ddldefaultsaletype.ValueMember = "Purchasetypeid";
            ddldefaultsaletype.DisplayMember = "Purchasetypename";
            ddldefaultsaletype.DataSource = dtdept111;
            ddldefaultsaletype.SelectedIndex = -1;




        }
        public void binddrop(ComboBox cmb, DataTable dt, Label lbldefaultpurchase)
        {
            try
            {
                cmb.ValueMember = "id";
                cmb.DisplayMember = "name";
                cmb.DataSource = dt;
                cmb.SelectedIndex = -1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dt.Rows[i]["setdefault"].ToString()) == true)
                    {
                        lbldefaultpurchase.Text = dt.Rows[i]["name"].ToString();
                    }
                }
            }
            catch
            {
            }
        }
        private void btnapply_Click(object sender, EventArgs e)
        {
            
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
        private void autoroundoffpurchase_CheckedChanged(object sender, EventArgs e)
        {
            conn.execute("update options set autoroundoffpurchase='" + autoroundoffpurchase.Checked + "'");
        }

        private void showcustomersupplierseperatly_CheckedChanged(object sender, EventArgs e)
        {
            conn.execute("update options set showcustomersupplierseperate='" + showcustomersupplierseperatly.Checked + "'");
        }

        private void cmbpurchase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpurchase.Text != "" && flag == 0)
            {
                conn.execute("update FormFormat set setdefault=1 where name='" + cmbpurchase.Text + "' and type='P' and isactive=1");
                conn.execute("update FormFormat set setdefault=0 where name='" + lbldefaultpurchase.Text + "' and type='P' and isactive=1");
                DataTable dt = conn.getdataset("select * from FormFormat where isactive=1 and type='P'");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dt.Rows[i]["setdefault"].ToString()) == true)
                    {
                        lbldefaultpurchase.Text = dt.Rows[i]["name"].ToString();
                    }
                }
                bool inList = false;
                for (int i = 0; i < cmbpurchase.Items.Count; i++)
                {
                    s = cmbpurchase.GetItemText(cmbpurchase.Items[i]);
                    if (s == cmbpurchase.Text)
                    {
                        inList = true;
                        cmbpurchase.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbpurchase.Text = "";
                }
            }
        }

        private void autoroundoffsale_CheckedChanged(object sender, EventArgs e)
        {
            conn.execute("update options set autoroundoffsale='" + autoroundoffsale.Checked + "'");
        }

        private void cmbsale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbsale.Text != "" && flag == 0)
            {
                conn.execute("update FormFormat set setdefault=1 where name='" + cmbsale.Text + "' and type='S' and isactive=1");
                conn.execute("update FormFormat set setdefault=0 where name='" + lbldefaultsale.Text + "' and type='S' and isactive=1");
                DataTable dt = conn.getdataset("select * from FormFormat where isactive=1 and type='S'");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dt.Rows[i]["setdefault"].ToString()) == true)
                    {
                        lbldefaultsale.Text = dt.Rows[i]["name"].ToString();
                    }
                }
                bool inList = false;
                for (int i = 0; i < cmbsale.Items.Count; i++)
                {
                    s = cmbsale.GetItemText(cmbsale.Items[i]);
                    if (s == cmbsale.Text)
                    {
                        inList = true;
                        cmbsale.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbsale.Text = "";
                }
            }
        }

        private void chkitemsinasedingorderinsale_CheckedChanged(object sender, EventArgs e)
        {
            conn.execute("update options set itemsinasedingorderinsale='" + chkitemsinasedingorderinsale.Checked + "'");
        }
        private void ddlcontry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlcontry.Text != "" && flag == 0)
                {
                    SqlCommand cmddept2 = new SqlCommand("select id,StateName from State_Master where CountryID='" + ddlcontry.SelectedValue + "'", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmddept2);
                    DataTable dtdept12 = new DataTable();
                    sda2.Fill(dtdept12);

                    ddlstate.ValueMember = "id";
                    ddlstate.DisplayMember = "StateName";
                    ddlstate.DataSource = dtdept12;
                    ddlstate.SelectedIndex = -1;

                    conn.execute("Update Options Set countryName='" + ddlcontry.Text + "'");
                    lblcountry.Text = ddlcontry.Text;
                    //DataTable dt = conn.getdataset("select countryName from Options");
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    //if (Convert.ToBoolean(dt.Rows[i]["countryName"].ToString()) == true)
                    //    //{
                    //    lblcountry.Text = dt.Rows[0]["countryName"].ToString();
                    //    //}
                    //}
                    bool inList = false;
                    for (int i = 0; i < ddlcontry.Items.Count; i++)
                    {
                        s = ddlcontry.GetItemText(ddlcontry.Items[i]);
                        if (s == ddlcontry.Text)
                        {
                            inList = true;
                            ddlcontry.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        ddlcontry.Text = "";
                    }
                }
            }
            catch
            {

            }
        }

        private void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlstate.Text != "" && flag == 0)
                {
                    conn.execute("Update Options Set StateName='" + ddlstate.Text + "'");
                    lblstate.Text = ddlstate.Text;
                    //DataTable dt = conn.getdataset("select StateName from Options");
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    //if (Convert.ToBoolean(dt.Rows[i]["countryName"].ToString()) == true)
                    //    //{
                    //    lblstate.Text = dt.Rows[0]["StateName"].ToString();
                    //    //}
                    //}
                    bool inList = false;
                    for (int i = 0; i < ddlstate.Items.Count; i++)
                    {
                        s = ddlstate.GetItemText(ddlstate.Items[i]);
                        if (s == ddlstate.Text)
                        {
                            inList = true;
                            ddlstate.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        ddlstate.Text = "";
                    }
                }
            }
            catch
            {

            }
        }

        private void ddltaxation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddltaxation.Text != "" && flag == 0)
                {
                    conn.execute("Update Options Set Taxation='" + ddltaxation.Text + "'");
                    lbltaxation.Text = ddltaxation.Text;
                    //DataTable dt = conn.getdataset("select Taxation from Options");
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    //if (Convert.ToBoolean(dt.Rows[i]["countryName"].ToString()) == true)
                    //    //{
                    //    lbltaxation.Text = dt.Rows[0]["Taxation"].ToString();
                    //    //}
                    //}
                    bool inList = false;
                    for (int i = 0; i < ddltaxation.Items.Count; i++)
                    {
                        s = ddltaxation.GetItemText(ddltaxation.Items[i]);
                        if (s == ddltaxation.Text)
                        {
                            inList = true;
                            ddltaxation.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        ddltaxation.Text = "";
                    }
                }
            }
            catch
            {

            }
        }

        private void cmbpos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpos.Text != "" && flag == 0)
            {
                conn.execute("update FormFormat set setdefault=1 where name='" + cmbpos.Text + "' and type='POS' and isactive=1");
                conn.execute("update FormFormat set setdefault=0 where name='" + lbldefaultpos.Text + "' and type='POS' and isactive=1");
                DataTable dt = conn.getdataset("select * from FormFormat where isactive=1 and type='POS'");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dt.Rows[i]["setdefault"].ToString()) == true)
                    {
                        lbldefaultpos.Text = dt.Rows[i]["name"].ToString();
                    }
                }
                bool inList = false;
                for (int i = 0; i < cmbpos.Items.Count; i++)
                {
                    s = cmbpos.GetItemText(cmbpos.Items[i]);
                    if (s == cmbpos.Text)
                    {
                        inList = true;
                        cmbpos.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbpos.Text = "";
                }
            }
        }

        private void ddlbydefault_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddltaxation.Text != "" && flag == 0)
                {
                    conn.execute("Update Options Set DefaultPrice='" + ddlbydefault.Text + "'");
                    lblbydefault.Text = ddlbydefault.Text;
                    bool inList = false;
                    for (int i = 0; i < ddlbydefault.Items.Count; i++)
                    {
                        s = ddlbydefault.GetItemText(ddlbydefault.Items[i]);
                        if (s == ddlbydefault.Text)
                        {
                            inList = true;
                            ddlbydefault.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        ddlbydefault.Text = "";
                    }
                    //DataTable dt = conn.getdataset("select Taxation from Options");
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    //if (Convert.ToBoolean(dt.Rows[i]["countryName"].ToString()) == true)
                    //    //{
                    //    lbltaxation.Text = dt.Rows[0]["Taxation"].ToString();
                    //    //}
                    //}
                }
               
            }
            catch
            {

            }
        }

        private void chkround_CheckedChanged(object sender, EventArgs e)
        {
            conn.execute("update options set autoroundoffpos='" + chkround.Checked + "'");
        }

        private void ddldefaultsaletype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddldefaultsaletype.Text != "" && flag == 0)
                {
                    conn.execute("update options set autosaletype='" + ddldefaultsaletype.Text + "'");
                    lbldefaultsaletype.Text = ddldefaultsaletype.Text;
                    bool inList = false;
                    for (int i = 0; i < ddldefaultsaletype.Items.Count; i++)
                    {
                        s = ddldefaultsaletype.GetItemText(ddldefaultsaletype.Items[i]);
                        if (s == ddldefaultsaletype.Text)
                        {
                            inList = true;
                            ddldefaultsaletype.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        ddldefaultsaletype.Text = "";
                    }
                }
               
            }
            catch
            {

            }
        }

        private void ddldefaultbill_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddldefaultbill.Text != "" && flag == 0)
                {
                    conn.execute("update options set defaultbill='" + ddldefaultbill.Text + "'");
                    lbldefaultbill.Text = ddldefaultbill.Text;
                    bool inList = false;
                    for (int i = 0; i < ddldefaultbill.Items.Count; i++)
                    {
                        s = ddldefaultbill.GetItemText(ddldefaultbill.Items[i]);
                        if (s == ddldefaultbill.Text)
                        {
                            inList = true;
                            ddldefaultbill.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        ddldefaultbill.Text = "";
                    }
                }
                
            }
            catch
            {

            }
        }

        
        private void ddldateformat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddldateformat.Text != "" && flag == 0)
                {
                    conn.execute("update options set dateformate='" + ddldateformat.Text + "'");
                    lbldateformat.Text = ddldateformat.Text;
                    bool inList = false;
                    for (int i = 0; i < ddldateformat.Items.Count; i++)
                    {
                        s = ddldateformat.GetItemText(ddldateformat.Items[i]);
                        if (s == ddldateformat.Text)
                        {
                            inList = true;
                            ddldateformat.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        ddldateformat.Text = "";
                    }
                }
            }
            catch
            {

            }
        }

        private void chkcess_CheckedChanged(object sender, EventArgs e)
        {
            conn.execute("update options set cess='" + chkcess.Checked + "'");
        }

        private void chksaledialog_CheckedChanged(object sender, EventArgs e)
        {
            conn.execute("update options set savedialogbox='" + chksaledialog.Checked + "'");
        }

        private void cmbs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbs.Text != "" && flag==0)
                {
                    conn.execute("update options set salevoucherno='" + cmbs.Text + "'");
                    lblsale.Text = cmbs.Text;
                    bool inList = false;
                    for (int i = 0; i < cmbs.Items.Count; i++)
                    {
                        s = cmbs.GetItemText(cmbs.Items[i]);
                        if (s == cmbs.Text)
                        {
                            inList = true;
                            cmbs.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbs.Text = "";
                    }
                }
            }
            catch
            {
            }
        }

        private void cmbsr_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbsr.Text != "" && flag == 0)
                {
                    conn.execute("update options set salervoucherno='" + cmbsr.Text + "'");
                    lblsalereturn.Text = cmbsr.Text;
                    bool inList = false;
                    for (int i = 0; i < cmbsr.Items.Count; i++)
                    {
                        s = cmbsr.GetItemText(cmbsr.Items[i]);
                        if (s == cmbsr.Text)
                        {
                            inList = true;
                            cmbsr.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbsr.Text = "";
                    }
                }
            }
            catch
            {
            }
        }

        private void cmbp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbp.Text != "" && flag == 0)
                {
                    conn.execute("update options set purchasevoucherno='" + cmbp.Text + "'");
                    lblpurchase.Text = cmbp.Text;
                    bool inList = false;
                    for (int i = 0; i < cmbp.Items.Count; i++)
                    {
                        s = cmbp.GetItemText(cmbp.Items[i]);
                        if (s == cmbp.Text)
                        {
                            inList = true;
                            cmbp.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbp.Text = "";
                    }
                }
            }
            catch
            {
            }
        }

        private void cmbpr_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbpr.Text != "" && flag == 0)
                {
                    conn.execute("update options set purchaservoucherno='" + cmbpr.Text + "'");
                    lblpurchasereturn.Text = cmbpr.Text;
                    bool inList = false;
                    for (int i = 0; i < cmbpr.Items.Count; i++)
                    {
                        s = cmbpr.GetItemText(cmbpr.Items[i]);
                        if (s == cmbpr.Text)
                        {
                            inList = true;
                            cmbpr.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbpr.Text = "";
                    }
                }
            }
            catch
            {
            }
        }

        private void cmbso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbso.Text != "" && flag == 0)
                {
                    conn.execute("update options set saleovoucherno='" + cmbso.Text + "'");
                    lblsaleorder.Text = cmbso.Text;
                    bool inList = false;
                    for (int i = 0; i < cmbso.Items.Count; i++)
                    {
                        s = cmbso.GetItemText(cmbso.Items[i]);
                        if (s == cmbso.Text)
                        {
                            inList = true;
                            cmbso.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbso.Text = "";
                    }
                }
            }
            catch
            {
            }
        }

        private void cmbsc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbsc.Text != "" && flag == 0)
                {
                    conn.execute("update options set salecvoucherno='" + cmbsc.Text + "'");
                    lblsalechallan.Text = cmbsc.Text;
                    bool inList = false;
                    for (int i = 0; i < cmbsc.Items.Count; i++)
                    {
                        s = cmbsc.GetItemText(cmbsc.Items[i]);
                        if (s == cmbsc.Text)
                        {
                            inList = true;
                            cmbsc.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbsc.Text = "";
                    }
                }
            }
            catch
            {
            }
        }

        private void cmbpo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbpo.Text != "" && flag == 0)
                {
                    conn.execute("update options set purchaseovoucherno='" + cmbpo.Text + "'");
                    lblpurchaseorder.Text = cmbpo.Text;
                    bool inList = false;
                    for (int i = 0; i < cmbpo.Items.Count; i++)
                    {
                        s = cmbpo.GetItemText(cmbpo.Items[i]);
                        if (s == cmbpo.Text)
                        {
                            inList = true;
                            cmbpo.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbpo.Text = "";
                    }
                }
            }
            catch
            {
            }
        }

        private void cmbpc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbpc.Text != "" && flag == 0)
                {
                    conn.execute("update options set purchasecvoucherno='" + cmbpc.Text + "'");
                    lblpurchasechallan.Text = cmbpc.Text;
                    bool inList = false;
                    for (int i = 0; i < cmbpc.Items.Count; i++)
                    {
                        s = cmbpc.GetItemText(cmbpc.Items[i]);
                        if (s == cmbpc.Text)
                        {
                            inList = true;
                            cmbpc.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbpc.Text = "";
                    }
                }
            }
            catch
            {
            }
        }

        private void chkcusreq_CheckedChanged(object sender, EventArgs e)
        {
            conn.execute("update options set requiredcustomerdetailinpos='" + chkcusreq.Checked + "'");
        }

        private void chkside_CheckedChanged(object sender, EventArgs e)
        {
            conn.execute("update options set showsidebox='" + chkside.Checked + "'");
        }

        private void chkmultiprint_CheckedChanged(object sender, EventArgs e)
        {
            conn.execute("update options set multyprintinpos='" + chkmultiprint.Checked + "'");
        }
        string searchstr;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //empty the string for every 1 seconds
           // searchstr = "";
        }

        private void cmbpurchase_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbpurchase.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbpurchase.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbsale_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbsale.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbsale.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbpos_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbpos.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbpos.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void ddlbydefault_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = ddlbydefault.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            ddlbydefault.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void ddldefaultsaletype_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = ddldefaultsaletype.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            ddldefaultsaletype.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void ddldefaultbill_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = ddldefaultbill.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            ddldefaultbill.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void ddlcontry_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = ddlcontry.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            ddlcontry.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void ddlstate_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = ddlstate.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            ddlstate.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void ddltaxation_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = ddltaxation.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            ddltaxation.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void ddldateformat_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = ddldateformat.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            ddldateformat.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbs_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbs.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbs.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbsr_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbsr.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbsr.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbp_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbp.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbp.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbpr_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbpr.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbpr.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbso_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbso.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbso.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbsc_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbsc.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbsc.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbpo_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            //{
            //    searchstr = searchstr + Convert.ToChar(e.KeyCode);
            //    // If the Search string is greater than 1 then use custom logic
            //    if (searchstr.Length > 1)
            //    {
            //        int index;
            //        // Search the Item that matches the string typed
            //        index = cmbpo.FindString(searchstr);
            //        // Select the Item in the Combo
            //        if (index > 0)
            //        {
            //            cmbpo.SelectedIndex = index;
            //        }
            //    }


            //}
        }

        private void cmbpc_KeyUp(object sender, KeyEventArgs e)
        {
            if (Char.IsLetter(Convert.ToChar(e.KeyCode)) || Char.IsDigit(Convert.ToChar(e.KeyCode)))
            {
                searchstr = searchstr + Convert.ToChar(e.KeyCode);
                // If the Search string is greater than 1 then use custom logic
                if (searchstr.Length > 1)
                {
                    int index;
                    // Search the Item that matches the string typed
                    index = cmbpc.FindString(searchstr);
                    // Select the Item in the Combo
                    if (index > 0)
                    {
                        cmbpc.SelectedIndex = index;
                    }
                }


            }
        }

        private void cmbsalebills_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbsalebills.Text != "" && flag == 0)
                {
                    conn.execute("update options set noofcopyofsalebills='" + cmbsalebills.Text + "'");
                    bool inList = false;
                    for (int i = 0; i < cmbsalebills.Items.Count; i++)
                    {
                        s = cmbsalebills.GetItemText(cmbsalebills.Items[i]);
                        if (s == cmbsalebills.Text)
                        {
                            inList = true;
                            cmbsalebills.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbsalebills.Text = "";
                    }
                }
            }
            catch
            {
            }
        }

        private void cmbsaleorder_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbsaleorder.Text != "" && flag == 0)
                {
                    conn.execute("update options set noofcopyofsaleorderbills='" + cmbsaleorder.Text + "'");
                    bool inList = false;
                    for (int i = 0; i < cmbsaleorder.Items.Count; i++)
                    {
                        s = cmbsaleorder.GetItemText(cmbsaleorder.Items[i]);
                        if (s == cmbsaleorder.Text)
                        {
                            inList = true;
                            cmbsaleorder.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbsaleorder.Text = "";
                    }
                }
            }
            catch
            {
            }
        }

        private void cmbsalereturn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbsalereturn.Text != "" && flag == 0)
                {
                    conn.execute("update options set noofcopyofsalereturnbills='" + cmbsalereturn.Text + "'");
                    bool inList = false;
                    for (int i = 0; i < cmbsalereturn.Items.Count; i++)
                    {
                        s = cmbsalereturn.GetItemText(cmbsalereturn.Items[i]);
                        if (s == cmbsalereturn.Text)
                        {
                            inList = true;
                            cmbsalereturn.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbsalereturn.Text = "";
                    }
                }
            }
            catch
            {
            }
        }

        private void cmbsalechallan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbsalechallan.Text != "" && flag == 0)
                {
                    conn.execute("update options set noofcopyofsalechallanbills='" + cmbsalechallan.Text + "'");
                    bool inList = false;
                    for (int i = 0; i < cmbsalechallan.Items.Count; i++)
                    {
                        s = cmbsalechallan.GetItemText(cmbsalechallan.Items[i]);
                        if (s == cmbsalechallan.Text)
                        {
                            inList = true;
                            cmbsalechallan.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbsalechallan.Text = "";
                    }
                }
            }
            catch
            {
            }
        }

        private void btndelete_Enter(object sender, EventArgs e)
        {
            btndelete.UseVisualStyleBackColor = false;
            btndelete.BackColor = Color.FromArgb(248, 152, 94);
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
            btndelete.BackColor = Color.FromArgb(248, 152, 94);
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
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }

        private void chkprintpopup_CheckedChanged(object sender, EventArgs e)
        {
            conn.execute("update options set requirprintpopupinpos='" + chkprintpopup.Checked + "'");
        }

        private void chkallitemlist_CheckedChanged(object sender, EventArgs e)
        {
            conn.execute("update options set showallitemlistinpos='" + chkallitemlist.Checked + "'");
        }

        private void cmbpos1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbpos1.Text != "" && flag == 0)
                {
                    conn.execute("update options set posbillno='" + cmbpos1.Text + "'");
                    lblpos.Text = cmbpos1.Text;
                    bool inList = false;
                    for (int i = 0; i < cmbpos1.Items.Count; i++)
                    {
                        s = cmbpos1.GetItemText(cmbpos1.Items[i]);
                        if (s == cmbpos1.Text)
                        {
                            inList = true;
                            cmbpos1.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbpos1.Text = "";
                    }
                }
            }
            catch
            {
            }
        }
        public static string s;
        private void ddlbydefault_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < ddlbydefault.Items.Count; i++)
                {
                    s = ddlbydefault.GetItemText(ddlbydefault.Items[i]);
                    if (s == ddlbydefault.Text)
                    {
                        inList = true;
                        ddlbydefault.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    ddlbydefault.Text = "";
                }

            }
        }

        private void ddlbydefault_Leave(object sender, EventArgs e)
        {
            ddlbydefault.Text = s;
        }

        private void ddldefaultsaletype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < ddldefaultsaletype.Items.Count; i++)
                {
                    s = ddldefaultsaletype.GetItemText(ddldefaultsaletype.Items[i]);
                    if (s == ddldefaultsaletype.Text)
                    {
                        inList = true;
                        ddldefaultsaletype.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    ddldefaultsaletype.Text = "";
                }

            }
        }

        private void ddldefaultsaletype_Leave(object sender, EventArgs e)
        {
            ddldefaultsaletype.Text = s;
        }

        private void ddldefaultbill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < ddldefaultbill.Items.Count; i++)
                {
                    s = ddldefaultbill.GetItemText(ddldefaultbill.Items[i]);
                    if (s == ddldefaultbill.Text)
                    {
                        inList = true;
                        ddldefaultbill.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    ddldefaultbill.Text = "";
                }

            }
        }

        private void ddldefaultbill_Leave(object sender, EventArgs e)
        {
            ddldefaultbill.Text = s;
        }

        private void cmbpurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbpurchase.Items.Count; i++)
                {
                    s = cmbpurchase.GetItemText(cmbpurchase.Items[i]);
                    if (s == cmbpurchase.Text)
                    {
                        inList = true;
                        cmbpurchase.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbpurchase.Text = "";
                }

            }
        }

        private void cmbpurchase_Leave(object sender, EventArgs e)
        {
            cmbpurchase.Text = s;
        }

        private void cmbsale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbsale.Items.Count; i++)
                {
                    s = cmbsale.GetItemText(cmbsale.Items[i]);
                    if (s == cmbsale.Text)
                    {
                        inList = true;
                        cmbsale.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbsale.Text = "";
                }

            }
        }

        private void cmbsale_Leave(object sender, EventArgs e)
        {
            cmbsale.Text = s;
        }

        private void cmbpos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbpos.Items.Count; i++)
                {
                    s = cmbpos.GetItemText(cmbpos.Items[i]);
                    if (s == cmbpos.Text)
                    {
                        inList = true;
                        cmbpos.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbpos.Text = "";
                }

            }
        }

        private void cmbpos_Leave(object sender, EventArgs e)
        {
            cmbpos.Text = s;
        }

        private void ddlcontry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < ddlcontry.Items.Count; i++)
                {
                    s = ddlcontry.GetItemText(ddlcontry.Items[i]);
                    if (s == ddlcontry.Text)
                    {
                        inList = true;
                        ddlcontry.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    ddlcontry.Text = "";
                }

            }
        }

        private void ddlcontry_Leave(object sender, EventArgs e)
        {
            ddlcontry.Text = s;
        }

        private void ddlstate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < ddlstate.Items.Count; i++)
                {
                    s = ddlstate.GetItemText(ddlstate.Items[i]);
                    if (s == ddlstate.Text)
                    {
                        inList = true;
                        ddlstate.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    ddlstate.Text = "";
                }

            }
        }

        private void ddlstate_Leave(object sender, EventArgs e)
        {
            ddlstate.Text = s;
        }

        private void ddltaxation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < ddltaxation.Items.Count; i++)
                {
                    s = ddltaxation.GetItemText(ddltaxation.Items[i]);
                    if (s == ddltaxation.Text)
                    {
                        inList = true;
                        ddltaxation.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    ddltaxation.Text = "";
                }

            }
        }

        private void ddltaxation_Leave(object sender, EventArgs e)
        {
            ddltaxation.Text = s;
        }

        private void ddldateformat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < ddldateformat.Items.Count; i++)
                {
                    s = ddldateformat.GetItemText(ddldateformat.Items[i]);
                    if (s == ddldateformat.Text)
                    {
                        inList = true;
                        ddldateformat.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    ddldateformat.Text = "";
                }

            }
        }

        private void ddldateformat_Leave(object sender, EventArgs e)
        {
            ddldateformat.Text = s;
        }

        private void cmbs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbs.Items.Count; i++)
                {
                    s = cmbs.GetItemText(cmbs.Items[i]);
                    if (s == cmbs.Text)
                    {
                        inList = true;
                        cmbs.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbs.Text = "";
                }

            }
        }

        private void cmbs_Leave(object sender, EventArgs e)
        {
            cmbs.Text = s;
        }

        private void cmbsr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbsr.Items.Count; i++)
                {
                    s = cmbsr.GetItemText(cmbsr.Items[i]);
                    if (s == cmbsr.Text)
                    {
                        inList = true;
                        cmbsr.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbsr.Text = "";
                }

            }
        }

        private void cmbsr_Leave(object sender, EventArgs e)
        {
            cmbsr.Text = s;
        }

        private void cmbp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbp.Items.Count; i++)
                {
                    s = cmbp.GetItemText(cmbp.Items[i]);
                    if (s == cmbp.Text)
                    {
                        inList = true;
                        cmbp.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbp.Text = "";
                }

            }
        }

        private void cmbp_Leave(object sender, EventArgs e)
        {
            cmbp.Text = s;
        }

        private void cmbpr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbpr.Items.Count; i++)
                {
                    s = cmbpr.GetItemText(cmbpr.Items[i]);
                    if (s == cmbpr.Text)
                    {
                        inList = true;
                        cmbpr.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbpr.Text = "";
                }

            }
        }

        private void cmbpr_Leave(object sender, EventArgs e)
        {
            cmbpr.Text = s;
        }

        private void cmbso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbso.Items.Count; i++)
                {
                    s = cmbso.GetItemText(cmbso.Items[i]);
                    if (s == cmbso.Text)
                    {
                        inList = true;
                        cmbso.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbso.Text = "";
                }

            }
        }

        private void cmbso_Leave(object sender, EventArgs e)
        {
            cmbso.Text = s;
        }

        private void cmbsc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbsc.Items.Count; i++)
                {
                    s = cmbsc.GetItemText(cmbsc.Items[i]);
                    if (s == cmbsc.Text)
                    {
                        inList = true;
                        cmbsc.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbsc.Text = "";
                }

            }
        }

        private void cmbsc_Leave(object sender, EventArgs e)
        {
            cmbsc.Text = s;
        }

        private void cmbpo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbpo.Items.Count; i++)
                {
                    s = cmbpo.GetItemText(cmbpo.Items[i]);
                    if (s == cmbpo.Text)
                    {
                        inList = true;
                        cmbpo.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbpo.Text = "";
                }

            }
        }

        private void cmbpo_Leave(object sender, EventArgs e)
        {
            cmbpo.Text = s;
        }

        private void cmbpc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbpc.Items.Count; i++)
                {
                    s = cmbpc.GetItemText(cmbpc.Items[i]);
                    if (s == cmbpc.Text)
                    {
                        inList = true;
                        cmbpc.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbpc.Text = "";
                }

            }
        }

        private void cmbpc_Leave(object sender, EventArgs e)
        {
            cmbpc.Text = s;
        }

        private void cmbpos1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbpos1.Items.Count; i++)
                {
                    s = cmbpos1.GetItemText(cmbpos1.Items[i]);
                    if (s == cmbpos1.Text)
                    {
                        inList = true;
                        cmbpos1.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbpos1.Text = "";
                }

            }
        }

        private void cmbpos1_Layout(object sender, LayoutEventArgs e)
        {
            
        }

        private void cmbpos1_Leave(object sender, EventArgs e)
        {
            cmbpos1.Text = s;
        }

        private void cmbsalebills_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbsalebills.Items.Count; i++)
                {
                    s = cmbsalebills.GetItemText(cmbsalebills.Items[i]);
                    if (s == cmbsalebills.Text)
                    {
                        inList = true;
                        cmbsalebills.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbsalebills.Text = "";
                }

            }
        }

        private void cmbsalebills_Leave(object sender, EventArgs e)
        {
            cmbsalebills.Text = s;
        }

        private void cmbsaleorder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbsaleorder.Items.Count; i++)
                {
                    s = cmbsaleorder.GetItemText(cmbsaleorder.Items[i]);
                    if (s == cmbsaleorder.Text)
                    {
                        inList = true;
                        cmbsaleorder.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbsaleorder.Text = "";
                }

            }
        }

        private void cmbsaleorder_Leave(object sender, EventArgs e)
        {
            cmbsaleorder.Text = s;
        }

        private void cmbsalereturn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbsalereturn.Items.Count; i++)
                {
                    s = cmbsalereturn.GetItemText(cmbsalereturn.Items[i]);
                    if (s == cmbsalereturn.Text)
                    {
                        inList = true;
                        cmbsalereturn.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbsalereturn.Text = "";
                }

            }
        }

        private void cmbsalereturn_Leave(object sender, EventArgs e)
        {
            cmbsalereturn.Text = s;
        }

        private void cmbsalechallan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbsalechallan.Items.Count; i++)
                {
                    s = cmbsalechallan.GetItemText(cmbsalechallan.Items[i]);
                    if (s == cmbsalechallan.Text)
                    {
                        inList = true;
                        cmbsalechallan.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbsalechallan.Text = "";
                }

            }
        }

        private void cmbsalechallan_Leave(object sender, EventArgs e)
        {
            cmbsalechallan.Text = s;
        }

        private void chkinvoice_CheckedChanged(object sender, EventArgs e)
        {
            conn.execute("update options set invoicenoinpos='" + chkinvoice.Checked + "'");
        }

        private void chkagentnameshowinpos_CheckedChanged(object sender, EventArgs e)
        {
            conn.execute("update options set showagentnameinsale='" + chkagentnameshowinpos.Checked + "'");
        }

        private void cmbagent_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbagent.Text != "" && flag == 0)
                {
                    conn.execute("update options set requiragentnameinpos='" + cmbagent.Text + "'");
                    lblagentsectioninpos.Text = cmbagent.Text;
                    bool inList = false;
                    for (int i = 0; i < cmbagent.Items.Count; i++)
                    {
                        s = cmbagent.GetItemText(cmbagent.Items[i]);
                        if (s == cmbagent.Text)
                        {
                            inList = true;
                            cmbagent.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbagent.Text = "";
                    }
                }

            }
            catch
            {

            }
        }

        private void cmbagent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbagent.Items.Count; i++)
                {
                    s = cmbagent.GetItemText(cmbagent.Items[i]);
                    if (s == cmbagent.Text)
                    {
                        inList = true;
                        cmbagent.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbagent.Text = "";
                }
                lblagentsectioninpos.Text = cmbagent.Text;
            }
        }

        private void cmbproduction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbproduction.Items.Count; i++)
                {
                    s = cmbproduction.GetItemText(cmbproduction.Items[i]);
                    if (s == cmbproduction.Text)
                    {
                        inList = true;
                        cmbproduction.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbproduction.Text = "";
                }

            }
        }

        private void cmbproduction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbproduction.Text != "" && flag == 0)
                {
                    conn.execute("update options set productionidtype='" + cmbproduction.Text + "'");
                    lblproduction.Text = cmbproduction.Text;
                    bool inList = false;
                    for (int i = 0; i < cmbproduction.Items.Count; i++)
                    {
                        s = cmbproduction.GetItemText(cmbproduction.Items[i]);
                        if (s == cmbproduction.Text)
                        {
                            inList = true;
                            cmbproduction.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbproduction.Text = "";
                    }
                }
            }
            catch
            {
            }
            
        }

        private void cmbproduction_Leave(object sender, EventArgs e)
        {
            cmbproduction.Text = s;
        }

       
        
        
    }
}
