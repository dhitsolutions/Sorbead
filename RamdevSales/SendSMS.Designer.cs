namespace RamdevSales
{
    partial class SendSMS
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
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblconfiguresmsapi = new System.Windows.Forms.LinkLabel();
            this.btnclose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblmsg = new System.Windows.Forms.Label();
            this.lblmsgcount = new System.Windows.Forms.Label();
            this.btnok = new System.Windows.Forms.Button();
            this.txtpart1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox1
            // 
            this.TextBox1.BackColor = System.Drawing.SystemColors.Highlight;
            this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox1.ForeColor = System.Drawing.Color.White;
            this.TextBox1.Location = new System.Drawing.Point(1, 0);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new System.Drawing.Size(731, 31);
            this.TextBox1.TabIndex = 12;
            this.TextBox1.TabStop = false;
            this.TextBox1.Text = "SEND SMS";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblconfiguresmsapi);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblmsg);
            this.panel1.Controls.Add(this.lblmsgcount);
            this.panel1.Controls.Add(this.btnok);
            this.panel1.Controls.Add(this.txtpart1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(2, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(730, 250);
            this.panel1.TabIndex = 13;
            // 
            // lblconfiguresmsapi
            // 
            this.lblconfiguresmsapi.AutoSize = true;
            this.lblconfiguresmsapi.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblconfiguresmsapi.Location = new System.Drawing.Point(518, 186);
            this.lblconfiguresmsapi.Name = "lblconfiguresmsapi";
            this.lblconfiguresmsapi.Size = new System.Drawing.Size(195, 16);
            this.lblconfiguresmsapi.TabIndex = 191;
            this.lblconfiguresmsapi.TabStop = true;
            this.lblconfiguresmsapi.Text = "Define a New SMS Template";
            this.lblconfiguresmsapi.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblconfiguresmsapi_LinkClicked);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(630, 6);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 185;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(10, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(585, 17);
            this.label2.TabIndex = 190;
            this.label2.Text = "To Enable sending SMS,make sure you have correctly entered SMS API in Options";
            // 
            // lblmsg
            // 
            this.lblmsg.AutoSize = true;
            this.lblmsg.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblmsg.Location = new System.Drawing.Point(518, 87);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(129, 17);
            this.lblmsg.TabIndex = 189;
            this.lblmsg.Text = "Appx No. of SMS";
            // 
            // lblmsgcount
            // 
            this.lblmsgcount.AutoSize = true;
            this.lblmsgcount.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsgcount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblmsgcount.Location = new System.Drawing.Point(518, 63);
            this.lblmsgcount.Name = "lblmsgcount";
            this.lblmsgcount.Size = new System.Drawing.Size(131, 17);
            this.lblmsgcount.TabIndex = 188;
            this.lblmsgcount.Text = "Appx. Characters";
            // 
            // btnok
            // 
            this.btnok.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnok.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnok.ForeColor = System.Drawing.Color.White;
            this.btnok.Location = new System.Drawing.Point(521, 133);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(161, 41);
            this.btnok.TabIndex = 187;
            this.btnok.Text = "&Send SMS";
            this.btnok.UseVisualStyleBackColor = false;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // txtpart1
            // 
            this.txtpart1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpart1.Location = new System.Drawing.Point(10, 27);
            this.txtpart1.Multiline = true;
            this.txtpart1.Name = "txtpart1";
            this.txtpart1.Size = new System.Drawing.Size(502, 187);
            this.txtpart1.TabIndex = 182;
            this.txtpart1.TextChanged += new System.EventHandler(this.txtpart1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 181;
            this.label1.Text = "SEND SMS";
            // 
            // SendSMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 282);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SendSMS";
            this.Text = "SendSMS";
            this.Load += new System.EventHandler(this.SendSMS_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtpart1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.Label lblmsgcount;
        internal System.Windows.Forms.Button btnok;
        internal System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.LinkLabel lblconfiguresmsapi;
    }
}