namespace RamdevSales
{
    partial class StockStatus
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
            this.components = new System.ComponentModel.Container();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.grdstock = new System.Windows.Forms.DataGridView();
            this.BtnPayment = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.chkzero = new System.Windows.Forms.CheckBox();
            this.chkavailable = new System.Windows.Forms.CheckBox();
            this.chknegative = new System.Windows.Forms.CheckBox();
            this.drpfilter = new System.Windows.Forms.ComboBox();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtitems = new System.Windows.Forms.TextBox();
            this.drpitems = new System.Windows.Forms.ComboBox();
            this.btnok = new System.Windows.Forms.Button();
            this.txt6 = new System.Windows.Forms.TextBox();
            this.txt3 = new System.Windows.Forms.TextBox();
            this.txt11 = new System.Windows.Forms.TextBox();
            this.txt9 = new System.Windows.Forms.TextBox();
            this.txt10 = new System.Windows.Forms.TextBox();
            this.txt8 = new System.Windows.Forms.TextBox();
            this.txt7 = new System.Windows.Forms.TextBox();
            this.txt5 = new System.Windows.Forms.TextBox();
            this.txt4 = new System.Windows.Forms.TextBox();
            this.txt2 = new System.Windows.Forms.TextBox();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.btnprint = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdstock)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox7
            // 
            this.textBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(0, 0);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(1113, 31);
            this.textBox7.TabIndex = 248;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "STOCK STATUS";
            // 
            // grdstock
            // 
            this.grdstock.AllowUserToAddRows = false;
            this.grdstock.AllowUserToDeleteRows = false;
            this.grdstock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdstock.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdstock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdstock.Location = new System.Drawing.Point(10, 53);
            this.grdstock.Name = "grdstock";
            this.grdstock.Size = new System.Drawing.Size(1091, 388);
            this.grdstock.TabIndex = 243;
            this.grdstock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdstock_KeyDown);
            this.grdstock.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grdstock_MouseDoubleClick);
            // 
            // BtnPayment
            // 
            this.BtnPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPayment.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPayment.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPayment.ForeColor = System.Drawing.Color.White;
            this.BtnPayment.Location = new System.Drawing.Point(801, 42);
            this.BtnPayment.Name = "BtnPayment";
            this.BtnPayment.Size = new System.Drawing.Size(97, 34);
            this.BtnPayment.TabIndex = 252;
            this.BtnPayment.Text = "&Excel";
            this.BtnPayment.UseVisualStyleBackColor = false;
            this.BtnPayment.Click += new System.EventHandler(this.BtnPayment_Click);
            this.BtnPayment.Enter += new System.EventHandler(this.BtnPayment_Enter);
            this.BtnPayment.Leave += new System.EventHandler(this.BtnPayment_Leave);
            this.BtnPayment.MouseEnter += new System.EventHandler(this.BtnPayment_MouseEnter);
            this.BtnPayment.MouseLeave += new System.EventHandler(this.BtnPayment_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.chkzero);
            this.panel1.Controls.Add(this.chkavailable);
            this.panel1.Controls.Add(this.chknegative);
            this.panel1.Controls.Add(this.drpfilter);
            this.panel1.Controls.Add(this.DTPFrom);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtitems);
            this.panel1.Controls.Add(this.drpitems);
            this.panel1.Controls.Add(this.btnok);
            this.panel1.Controls.Add(this.txt6);
            this.panel1.Controls.Add(this.grdstock);
            this.panel1.Controls.Add(this.txt3);
            this.panel1.Controls.Add(this.txt11);
            this.panel1.Controls.Add(this.txt9);
            this.panel1.Controls.Add(this.txt10);
            this.panel1.Controls.Add(this.txt8);
            this.panel1.Controls.Add(this.txt7);
            this.panel1.Controls.Add(this.txt5);
            this.panel1.Controls.Add(this.txt4);
            this.panel1.Controls.Add(this.txt2);
            this.panel1.Controls.Add(this.txt1);
            this.panel1.Controls.Add(this.btnprint);
            this.panel1.Controls.Add(this.btncancel);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Location = new System.Drawing.Point(1, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1112, 487);
            this.panel1.TabIndex = 253;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(237, 451);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(307, 27);
            this.progressBar1.TabIndex = 287;
            // 
            // chkzero
            // 
            this.chkzero.AutoSize = true;
            this.chkzero.Location = new System.Drawing.Point(291, 34);
            this.chkzero.Name = "chkzero";
            this.chkzero.Size = new System.Drawing.Size(90, 17);
            this.chkzero.TabIndex = 286;
            this.chkzero.Text = "Zero Balance";
            this.chkzero.UseVisualStyleBackColor = true;
            // 
            // chkavailable
            // 
            this.chkavailable.AutoSize = true;
            this.chkavailable.Location = new System.Drawing.Point(291, 18);
            this.chkavailable.Name = "chkavailable";
            this.chkavailable.Size = new System.Drawing.Size(100, 17);
            this.chkavailable.TabIndex = 285;
            this.chkavailable.Text = "Available Stock";
            this.chkavailable.UseVisualStyleBackColor = true;
            // 
            // chknegative
            // 
            this.chknegative.AutoSize = true;
            this.chknegative.Location = new System.Drawing.Point(291, 2);
            this.chknegative.Name = "chknegative";
            this.chknegative.Size = new System.Drawing.Size(100, 17);
            this.chknegative.TabIndex = 284;
            this.chknegative.Text = "Negative Stock";
            this.chknegative.UseVisualStyleBackColor = true;
            // 
            // drpfilter
            // 
            this.drpfilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.drpfilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.drpfilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpfilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drpfilter.FormattingEnabled = true;
            this.drpfilter.Items.AddRange(new object[] {
            "Show All Items",
            "Items Below Min. Level",
            "Items Below Reorder Level",
            "Items Above Max. Level"});
            this.drpfilter.Location = new System.Drawing.Point(10, 454);
            this.drpfilter.Name = "drpfilter";
            this.drpfilter.Size = new System.Drawing.Size(221, 24);
            this.drpfilter.TabIndex = 283;
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(32, 20);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(150, 24);
            this.DTPFrom.TabIndex = 277;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(77, 2);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(48, 16);
            this.Label1.TabIndex = 280;
            this.Label1.Text = "As on";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(966, 448);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(97, 34);
            this.btnSearch.TabIndex = 276;
            this.btnSearch.Text = "&Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtitems
            // 
            this.txtitems.BackColor = System.Drawing.Color.White;
            this.txtitems.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtitems.Location = new System.Drawing.Point(747, 455);
            this.txtitems.Name = "txtitems";
            this.txtitems.Size = new System.Drawing.Size(212, 24);
            this.txtitems.TabIndex = 275;
            // 
            // drpitems
            // 
            this.drpitems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.drpitems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.drpitems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpitems.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drpitems.FormattingEnabled = true;
            this.drpitems.Location = new System.Drawing.Point(550, 454);
            this.drpitems.Name = "drpitems";
            this.drpitems.Size = new System.Drawing.Size(191, 24);
            this.drpitems.TabIndex = 274;
            // 
            // btnok
            // 
            this.btnok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnok.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnok.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnok.ForeColor = System.Drawing.Color.White;
            this.btnok.Location = new System.Drawing.Point(188, 6);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(97, 41);
            this.btnok.TabIndex = 271;
            this.btnok.Text = "&Ok";
            this.btnok.UseVisualStyleBackColor = false;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // txt6
            // 
            this.txt6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt6.Location = new System.Drawing.Point(570, 452);
            this.txt6.Multiline = true;
            this.txt6.Name = "txt6";
            this.txt6.Size = new System.Drawing.Size(10, 22);
            this.txt6.TabIndex = 270;
            this.txt6.Visible = false;
            // 
            // txt3
            // 
            this.txt3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt3.Location = new System.Drawing.Point(285, 452);
            this.txt3.Multiline = true;
            this.txt3.Name = "txt3";
            this.txt3.Size = new System.Drawing.Size(10, 22);
            this.txt3.TabIndex = 269;
            this.txt3.Visible = false;
            // 
            // txt11
            // 
            this.txt11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt11.Location = new System.Drawing.Point(1045, 452);
            this.txt11.Multiline = true;
            this.txt11.Name = "txt11";
            this.txt11.Size = new System.Drawing.Size(10, 22);
            this.txt11.TabIndex = 268;
            this.txt11.Visible = false;
            // 
            // txt9
            // 
            this.txt9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt9.Location = new System.Drawing.Point(855, 452);
            this.txt9.Multiline = true;
            this.txt9.Name = "txt9";
            this.txt9.Size = new System.Drawing.Size(10, 22);
            this.txt9.TabIndex = 267;
            this.txt9.Visible = false;
            // 
            // txt10
            // 
            this.txt10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt10.Location = new System.Drawing.Point(950, 452);
            this.txt10.Multiline = true;
            this.txt10.Name = "txt10";
            this.txt10.Size = new System.Drawing.Size(10, 22);
            this.txt10.TabIndex = 266;
            this.txt10.Visible = false;
            // 
            // txt8
            // 
            this.txt8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt8.Location = new System.Drawing.Point(760, 452);
            this.txt8.Multiline = true;
            this.txt8.Name = "txt8";
            this.txt8.Size = new System.Drawing.Size(10, 22);
            this.txt8.TabIndex = 265;
            this.txt8.Visible = false;
            // 
            // txt7
            // 
            this.txt7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt7.Location = new System.Drawing.Point(665, 452);
            this.txt7.Multiline = true;
            this.txt7.Name = "txt7";
            this.txt7.Size = new System.Drawing.Size(10, 22);
            this.txt7.TabIndex = 264;
            this.txt7.Visible = false;
            // 
            // txt5
            // 
            this.txt5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt5.Location = new System.Drawing.Point(475, 452);
            this.txt5.Multiline = true;
            this.txt5.Name = "txt5";
            this.txt5.Size = new System.Drawing.Size(10, 22);
            this.txt5.TabIndex = 263;
            this.txt5.Visible = false;
            // 
            // txt4
            // 
            this.txt4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt4.Location = new System.Drawing.Point(380, 452);
            this.txt4.Multiline = true;
            this.txt4.Name = "txt4";
            this.txt4.Size = new System.Drawing.Size(10, 22);
            this.txt4.TabIndex = 262;
            this.txt4.Visible = false;
            // 
            // txt2
            // 
            this.txt2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt2.Location = new System.Drawing.Point(190, 452);
            this.txt2.Multiline = true;
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(10, 22);
            this.txt2.TabIndex = 261;
            this.txt2.Visible = false;
            // 
            // txt1
            // 
            this.txt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt1.Location = new System.Drawing.Point(95, 452);
            this.txt1.Multiline = true;
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(10, 22);
            this.txt1.TabIndex = 260;
            this.txt1.Visible = false;
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.Enabled = false;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(903, 10);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 259;
            this.btnprint.Text = "&Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            this.btnprint.Enter += new System.EventHandler(this.btnprint_Enter);
            this.btnprint.Leave += new System.EventHandler(this.btnprint_Leave);
            this.btnprint.MouseEnter += new System.EventHandler(this.btnprint_MouseEnter);
            this.btnprint.MouseLeave += new System.EventHandler(this.btnprint_MouseLeave);
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.ForeColor = System.Drawing.Color.White;
            this.btncancel.Location = new System.Drawing.Point(1004, 10);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(97, 34);
            this.btncancel.TabIndex = 257;
            this.btncancel.Text = "&Close";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            this.btncancel.Enter += new System.EventHandler(this.btncancel_Enter);
            this.btncancel.Leave += new System.EventHandler(this.btncancel_Leave);
            this.btncancel.MouseEnter += new System.EventHandler(this.btncancel_MouseEnter);
            this.btncancel.MouseLeave += new System.EventHandler(this.btncancel_MouseLeave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(497, 234);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(117, 16);
            this.label11.TabIndex = 256;
            this.label11.Text = "Total Op. Stock";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // StockStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1115, 546);
            this.ControlBox = false;
            this.Controls.Add(this.BtnPayment);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StockStatus";
            this.Text = "StockStatus";
            this.Load += new System.EventHandler(this.StockReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdstock)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.DataGridView grdstock;
        internal System.Windows.Forms.Button BtnPayment;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btncancel;
        internal System.Windows.Forms.Button btnprint;
        internal System.Windows.Forms.Button btnok;
        internal System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtitems;
        private System.Windows.Forms.ComboBox drpitems;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox txt6;
        private System.Windows.Forms.TextBox txt3;
        private System.Windows.Forms.TextBox txt11;
        private System.Windows.Forms.TextBox txt9;
        private System.Windows.Forms.TextBox txt10;
        private System.Windows.Forms.TextBox txt8;
        private System.Windows.Forms.TextBox txt7;
        private System.Windows.Forms.TextBox txt5;
        private System.Windows.Forms.TextBox txt4;
        private System.Windows.Forms.TextBox txt2;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.ComboBox drpfilter;
        private System.Windows.Forms.CheckBox chkzero;
        private System.Windows.Forms.CheckBox chkavailable;
        private System.Windows.Forms.CheckBox chknegative;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
    }
}