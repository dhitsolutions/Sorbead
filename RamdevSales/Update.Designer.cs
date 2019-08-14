namespace RamdevSales
{
    partial class Update
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnok = new System.Windows.Forms.Button();
            this.txtdate = new System.Windows.Forms.TextBox();
            this.txtup = new System.Windows.Forms.TextBox();
            this.txtamcday = new System.Windows.Forms.TextBox();
            this.txtsrno = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txtsrno);
            this.panel1.Controls.Add(this.txtamcday);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.btnok);
            this.panel1.Controls.Add(this.txtdate);
            this.panel1.Controls.Add(this.txtup);
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 116);
            this.panel1.TabIndex = 0;
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(23, 75);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 3;
            this.btnclose.Text = "&Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnok
            // 
            this.btnok.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnok.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnok.ForeColor = System.Drawing.Color.White;
            this.btnok.Location = new System.Drawing.Point(181, 75);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(97, 34);
            this.btnok.TabIndex = 2;
            this.btnok.Text = "&OK";
            this.btnok.UseVisualStyleBackColor = false;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // txtdate
            // 
            this.txtdate.Location = new System.Drawing.Point(159, 21);
            this.txtdate.Name = "txtdate";
            this.txtdate.Size = new System.Drawing.Size(119, 20);
            this.txtdate.TabIndex = 1;
            this.txtdate.TextChanged += new System.EventHandler(this.txtdate_TextChanged);
            // 
            // txtup
            // 
            this.txtup.Location = new System.Drawing.Point(23, 21);
            this.txtup.Name = "txtup";
            this.txtup.PasswordChar = '*';
            this.txtup.Size = new System.Drawing.Size(119, 20);
            this.txtup.TabIndex = 0;
            // 
            // txtamcday
            // 
            this.txtamcday.Location = new System.Drawing.Point(159, 47);
            this.txtamcday.Name = "txtamcday";
            this.txtamcday.PasswordChar = '*';
            this.txtamcday.Size = new System.Drawing.Size(119, 20);
            this.txtamcday.TabIndex = 4;
            this.txtamcday.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtamcday_KeyPress);
            // 
            // txtsrno
            // 
            this.txtsrno.Location = new System.Drawing.Point(23, 47);
            this.txtsrno.Name = "txtsrno";
            this.txtsrno.PasswordChar = '*';
            this.txtsrno.Size = new System.Drawing.Size(119, 20);
            this.txtsrno.TabIndex = 5;
            // 
            // Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 120);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Update";
            this.Text = "Version Control";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtdate;
        private System.Windows.Forms.TextBox txtup;
        internal System.Windows.Forms.Button btnok;
        internal System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.TextBox txtamcday;
        private System.Windows.Forms.TextBox txtsrno;
    }
}