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
    public partial class Drawing : Form
    {
        private Master master;
        private TabControl tabControl;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        DataTable totaltable = new DataTable();
        public Drawing()
        {
            InitializeComponent();
          
        }

        public Drawing(Master master, TabControl tabControl)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.master = master;
            this.tabControl = tabControl;

            totaltable.Columns.Add("id", typeof(string));

            DataTable dt = new DataTable();
            dt = conn.getdataset("select * from tabledrawing");
            if (dt.Rows.Count > 0)
            {
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Button newButton = new Button();
                    this.Controls.Add(newButton);
                    newButton.Location = new Point(Convert.ToInt32(dt.Rows[i]["X"].ToString()), Convert.ToInt32(dt.Rows[i]["Y"].ToString()));
                    newButton.Name = dt.Rows[i]["tableid"].ToString();
                    newButton.Text = dt.Rows[i]["tablename"].ToString();
                    newButton.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._6_seaters_table));
                    newButton.Width = 185;
                    newButton.Height = 143;
                    newButton.ForeColor = Color.White;
                    Helper.ControlMover.Init(newButton);
                }
            }
       
        }
        bool isDragging;


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public int currentX, currentY;

        private void btnadd_Click(object sender, EventArgs e)
        {
            string id = conn.ExecuteScalar("select max(id) from tabledrawing");
            Button newButton = new Button();
            this.Controls.Add(newButton);
            newButton.Location = new Point(5,5);
            newButton.Name ="tbl"+(Convert.ToInt32(id) + 1).ToString();
            newButton.Text = txttablename.Text;
            newButton.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._6_seaters_table));
            newButton.Width = 185;
            newButton.Height = 143;
            newButton.ForeColor = Color.White;
            Helper.ControlMover.Init(newButton);
        }

        private void btnconfirm_Click(object sender, EventArgs e)
        {
            try
            {
                totaltable.Rows.Clear();
                foreach (Control control in this.Controls)
                {
                    if (control.GetType() == typeof(Button))
                    {
                        totaltable.Rows.Add(control.Name);
                    }
                }

                for (int i = 0; i < totaltable.Rows.Count; i++)
                {
                    DataTable isavail = new DataTable();
                    isavail = conn.getdataset("select * from tabledrawing where tableid='" +totaltable.Rows[i][0].ToString()+"'");

                    Button btn = this.Controls.Find(totaltable.Rows[i][0].ToString(), true).FirstOrDefault() as Button;
                    
                    //var btn = this.Controls.Find(totaltable.Rows[i][0].ToString(), true);
                    if (isavail.Rows.Count > 0)
                    {
                        conn.execute("update tabledrawing set sectionid='" + 1 + "', tablename='" + btn.Text + "',x='" + btn.Location.X + "',y='" + btn.Location.Y + "' where tableid='" + btn.Name + "' and sectionid='" + 1 + "'");
                    }
                    else
                    {
                        conn.execute("INSERT INTO TableDrawing([sectionid],[tableid],[tablename],[X],[Y])VALUES('" + 1 + "','" + btn.Name + "','" + btn.Text + "','" + btn.Location.X + "','" + btn.Location.Y + "')");
                    }
                }
              
                
                
            }
            catch
            {
            }

        }

        private void txttablename_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
