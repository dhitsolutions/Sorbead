namespace RamdevSales
{
    partial class DynamicSMSTemplate
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
            this.lblmsg = new System.Windows.Forms.Label();
            this.lblmsgcount = new System.Windows.Forms.Label();
            this.btnok = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.LVdysms = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtpart1 = new System.Windows.Forms.TextBox();
            this.btndelete = new System.Windows.Forms.Button();
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
            this.txtheader.Location = new System.Drawing.Point(1, 1);
            this.txtheader.Name = "txtheader";
            this.txtheader.ReadOnly = true;
            this.txtheader.Size = new System.Drawing.Size(693, 31);
            this.txtheader.TabIndex = 175;
            this.txtheader.TabStop = false;
            this.txtheader.Text = "Set SMS Template For";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btndelete);
            this.panel1.Controls.Add(this.lblmsg);
            this.panel1.Controls.Add(this.lblmsgcount);
            this.panel1.Controls.Add(this.btnok);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.LVdysms);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtpart1);
            this.panel1.Location = new System.Drawing.Point(1, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(693, 274);
            this.panel1.TabIndex = 176;
            // 
            // lblmsg
            // 
            this.lblmsg.AutoSize = true;
            this.lblmsg.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblmsg.Location = new System.Drawing.Point(11, 249);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(129, 17);
            this.lblmsg.TabIndex = 186;
            this.lblmsg.Text = "Appx No. of SMS";
            // 
            // lblmsgcount
            // 
            this.lblmsgcount.AutoSize = true;
            this.lblmsgcount.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsgcount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblmsgcount.Location = new System.Drawing.Point(11, 225);
            this.lblmsgcount.Name = "lblmsgcount";
            this.lblmsgcount.Size = new System.Drawing.Size(131, 17);
            this.lblmsgcount.TabIndex = 185;
            this.lblmsgcount.Text = "Appx. Characters";
            // 
            // btnok
            // 
            this.btnok.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnok.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnok.ForeColor = System.Drawing.Color.White;
            this.btnok.Location = new System.Drawing.Point(402, 229);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(86, 34);
            this.btnok.TabIndex = 183;
            this.btnok.Text = "&OK";
            this.btnok.UseVisualStyleBackColor = false;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(593, 229);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 184;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // LVdysms
            // 
            this.LVdysms.BackColor = System.Drawing.SystemColors.Window;
            this.LVdysms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVdysms.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVdysms.ForeColor = System.Drawing.Color.Navy;
            this.LVdysms.FullRowSelect = true;
            this.LVdysms.GridLines = true;
            this.LVdysms.HideSelection = false;
            this.LVdysms.Location = new System.Drawing.Point(456, 29);
            this.LVdysms.MultiSelect = false;
            this.LVdysms.Name = "LVdysms";
            this.LVdysms.Size = new System.Drawing.Size(234, 187);
            this.LVdysms.TabIndex = 182;
            this.LVdysms.UseCompatibleStateImageBehavior = false;
            this.LVdysms.View = System.Windows.Forms.View.Details;
            this.LVdysms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVdysms_KeyDown);
            this.LVdysms.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVdysms_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(465, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 17);
            this.label2.TabIndex = 181;
            this.label2.Text = "Choose Your Dynamic Field";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 180;
            this.label1.Text = "Edit SMS";
            // 
            // txtpart1
            // 
            this.txtpart1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpart1.Location = new System.Drawing.Point(12, 29);
            this.txtpart1.Multiline = true;
            this.txtpart1.Name = "txtpart1";
            this.txtpart1.Size = new System.Drawing.Size(438, 187);
            this.txtpart1.TabIndex = 179;
            this.txtpart1.TextChanged += new System.EventHandler(this.txtpart1_TextChanged);
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(491, 229);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(100, 34);
            this.btndelete.TabIndex = 187;
            this.btndelete.Text = "&Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // DynamicSMSTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 307);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtheader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DynamicSMSTemplate";
            this.Text = "DynamicSMSTemplate";
            this.Load += new System.EventHandler(this.DynamicSMSTemplate_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtheader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtpart1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.ListView LVdysms;
        internal System.Windows.Forms.Button btnok;
        internal System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.Label lblmsgcount;
        private System.Windows.Forms.Button btndelete;
    }
}