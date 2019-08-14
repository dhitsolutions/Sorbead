namespace Production
{
    partial class Drawing
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
            this.btnadd = new System.Windows.Forms.Button();
            this.btnconfirm = new System.Windows.Forms.Button();
            this.txttablename = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnadd
            // 
            this.btnadd.Location = new System.Drawing.Point(65, 46);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(75, 23);
            this.btnadd.TabIndex = 11;
            this.btnadd.Text = "Add";
            this.btnadd.UseVisualStyleBackColor = true;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // btnconfirm
            // 
            this.btnconfirm.Location = new System.Drawing.Point(65, 75);
            this.btnconfirm.Name = "btnconfirm";
            this.btnconfirm.Size = new System.Drawing.Size(75, 23);
            this.btnconfirm.TabIndex = 12;
            this.btnconfirm.Text = "Confirm";
            this.btnconfirm.UseVisualStyleBackColor = true;
            this.btnconfirm.Click += new System.EventHandler(this.btnconfirm_Click);
            // 
            // txttablename
            // 
            this.txttablename.Location = new System.Drawing.Point(65, 18);
            this.txttablename.Name = "txttablename";
            this.txttablename.Size = new System.Drawing.Size(90, 20);
            this.txttablename.TabIndex = 13;
            this.txttablename.TextChanged += new System.EventHandler(this.txttablename_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txttablename);
            this.panel1.Controls.Add(this.btnadd);
            this.panel1.Controls.Add(this.btnconfirm);
            this.panel1.Location = new System.Drawing.Point(573, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 133);
            this.panel1.TabIndex = 14;
            // 
            // Drawing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 478);
            this.Controls.Add(this.panel1);
            this.Name = "Drawing";
            this.Text = "Drawing";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.Button btnconfirm;
        private System.Windows.Forms.TextBox txttablename;
        private System.Windows.Forms.Panel panel1;

    }
}