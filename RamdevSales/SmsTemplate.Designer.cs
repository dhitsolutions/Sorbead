namespace RamdevSales
{
    partial class SmsTemplate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtheader = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnactive = new System.Windows.Forms.Button();
            this.chkselectall = new System.Windows.Forms.CheckBox();
            this.LVsms = new System.Windows.Forms.ListView();
            this.btnlog = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnconfigure = new System.Windows.Forms.Button();
            this.btndeactive = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtheader
            // 
            this.txtheader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtheader.BackColor = System.Drawing.SystemColors.Highlight;
            this.txtheader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtheader.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtheader.ForeColor = System.Drawing.Color.White;
            this.txtheader.Location = new System.Drawing.Point(0, 1);
            this.txtheader.Name = "txtheader";
            this.txtheader.ReadOnly = true;
            this.txtheader.Size = new System.Drawing.Size(964, 31);
            this.txtheader.TabIndex = 173;
            this.txtheader.TabStop = false;
            this.txtheader.Text = "SMS Template";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btndeactive);
            this.panel1.Controls.Add(this.btnactive);
            this.panel1.Controls.Add(this.chkselectall);
            this.panel1.Controls.Add(this.LVsms);
            this.panel1.Controls.Add(this.btnlog);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.btnconfigure);
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 453);
            this.panel1.TabIndex = 174;
            // 
            // btnactive
            // 
            this.btnactive.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnactive.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnactive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnactive.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnactive.ForeColor = System.Drawing.Color.White;
            this.btnactive.Location = new System.Drawing.Point(100, 17);
            this.btnactive.Name = "btnactive";
            this.btnactive.Size = new System.Drawing.Size(97, 34);
            this.btnactive.TabIndex = 28;
            this.btnactive.Text = "Active";
            this.btnactive.UseVisualStyleBackColor = false;
            this.btnactive.Click += new System.EventHandler(this.btnactive_Click);
            // 
            // chkselectall
            // 
            this.chkselectall.AutoSize = true;
            this.chkselectall.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkselectall.Location = new System.Drawing.Point(12, 31);
            this.chkselectall.Name = "chkselectall";
            this.chkselectall.Size = new System.Drawing.Size(82, 18);
            this.chkselectall.TabIndex = 27;
            this.chkselectall.Text = "Select All";
            this.chkselectall.UseVisualStyleBackColor = true;
            this.chkselectall.CheckedChanged += new System.EventHandler(this.chkselectall_CheckedChanged);
            // 
            // LVsms
            // 
            this.LVsms.BackColor = System.Drawing.SystemColors.Window;
            this.LVsms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVsms.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVsms.ForeColor = System.Drawing.Color.Navy;
            this.LVsms.FullRowSelect = true;
            this.LVsms.GridLines = true;
            this.LVsms.HideSelection = false;
            this.LVsms.Location = new System.Drawing.Point(12, 54);
            this.LVsms.MultiSelect = false;
            this.LVsms.Name = "LVsms";
            this.LVsms.Size = new System.Drawing.Size(941, 390);
            this.LVsms.TabIndex = 10;
            this.LVsms.UseCompatibleStateImageBehavior = false;
            this.LVsms.View = System.Windows.Forms.View.Details;
            this.LVsms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVsms_KeyDown);
            this.LVsms.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVsms_MouseDoubleClick);
            // 
            // btnlog
            // 
            this.btnlog.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnlog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlog.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlog.ForeColor = System.Drawing.Color.White;
            this.btnlog.Location = new System.Drawing.Point(687, 15);
            this.btnlog.Name = "btnlog";
            this.btnlog.Size = new System.Drawing.Size(163, 34);
            this.btnlog.TabIndex = 9;
            this.btnlog.Text = "&View Sent Sms Log";
            this.btnlog.UseVisualStyleBackColor = false;
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(856, 15);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 8;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnconfigure
            // 
            this.btnconfigure.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnconfigure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnconfigure.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnconfigure.ForeColor = System.Drawing.Color.White;
            this.btnconfigure.Location = new System.Drawing.Point(538, 15);
            this.btnconfigure.Name = "btnconfigure";
            this.btnconfigure.Size = new System.Drawing.Size(143, 34);
            this.btnconfigure.TabIndex = 7;
            this.btnconfigure.Text = "&Configure SMS API";
            this.btnconfigure.UseVisualStyleBackColor = false;
            this.btnconfigure.Click += new System.EventHandler(this.btnconfigure_Click);
            // 
            // btndeactive
            // 
            this.btndeactive.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btndeactive.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btndeactive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndeactive.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndeactive.ForeColor = System.Drawing.Color.White;
            this.btndeactive.Location = new System.Drawing.Point(203, 17);
            this.btndeactive.Name = "btndeactive";
            this.btndeactive.Size = new System.Drawing.Size(97, 34);
            this.btndeactive.TabIndex = 29;
            this.btndeactive.Text = "DeActive";
            this.btndeactive.UseVisualStyleBackColor = false;
            this.btndeactive.Click += new System.EventHandler(this.btndeactive_Click);
            // 
            // SmsTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 485);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtheader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SmsTemplate";
            this.Text = "SmsTemplate";
            this.Load += new System.EventHandler(this.SmsTemplate_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtheader;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnlog;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Button btnconfigure;
        internal System.Windows.Forms.ListView LVsms;
        private System.Windows.Forms.CheckBox chkselectall;
        internal System.Windows.Forms.Button btnactive;
        internal System.Windows.Forms.Button btndeactive;
    }
}