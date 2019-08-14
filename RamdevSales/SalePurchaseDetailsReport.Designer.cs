namespace RamdevSales
{
    partial class SalePurchaseDetailsReport
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
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.txtFilter3 = new System.Windows.Forms.TextBox();
            this.cmbFilter3 = new System.Windows.Forms.ComboBox();
            this.txtFilter2 = new System.Windows.Forms.TextBox();
            this.cmbFilter2 = new System.Windows.Forms.ComboBox();
            this.txtFilter1 = new System.Windows.Forms.TextBox();
            this.cmbFilter1 = new System.Windows.Forms.ComboBox();
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.Label2 = new System.Windows.Forms.Label();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.Label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grdData = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotalRate = new System.Windows.Forms.TextBox();
            this.txtTotalQty = new System.Windows.Forms.TextBox();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox14.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.ForeColor = System.Drawing.Color.White;
            this.textBox14.Location = new System.Drawing.Point(0, 1);
            this.textBox14.Multiline = true;
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(1080, 29);
            this.textBox14.TabIndex = 207;
            this.textBox14.TabStop = false;
            this.textBox14.Text = "SALE PURCHASE DETAIL REPORT";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btncancel);
            this.panel1.Controls.Add(this.BtnViewReport);
            this.panel1.Controls.Add(this.txtFilter3);
            this.panel1.Controls.Add(this.cmbFilter3);
            this.panel1.Controls.Add(this.txtFilter2);
            this.panel1.Controls.Add(this.cmbFilter2);
            this.panel1.Controls.Add(this.txtFilter1);
            this.panel1.Controls.Add(this.cmbFilter1);
            this.panel1.Controls.Add(this.DTPTo);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Controls.Add(this.DTPFrom);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1080, 83);
            this.panel1.TabIndex = 208;
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(975, 2);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(97, 34);
            this.btnExcel.TabIndex = 260;
            this.btnExcel.Text = "&Excel";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(876, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(97, 34);
            this.btnPrint.TabIndex = 259;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.ForeColor = System.Drawing.Color.White;
            this.btncancel.Location = new System.Drawing.Point(975, 39);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(97, 34);
            this.btncancel.TabIndex = 258;
            this.btncancel.Text = "&Close";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.ForeColor = System.Drawing.Color.White;
            this.BtnViewReport.Location = new System.Drawing.Point(876, 39);
            this.BtnViewReport.Name = "BtnViewReport";
            this.BtnViewReport.Size = new System.Drawing.Size(97, 34);
            this.BtnViewReport.TabIndex = 244;
            this.BtnViewReport.Text = "&OK";
            this.BtnViewReport.UseVisualStyleBackColor = false;
            this.BtnViewReport.Click += new System.EventHandler(this.BtnViewReport_Click);
            // 
            // txtFilter3
            // 
            this.txtFilter3.Location = new System.Drawing.Point(736, 50);
            this.txtFilter3.Name = "txtFilter3";
            this.txtFilter3.Size = new System.Drawing.Size(129, 20);
            this.txtFilter3.TabIndex = 243;
            this.txtFilter3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter3_KeyDown);
            // 
            // cmbFilter3
            // 
            this.cmbFilter3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter3.FormattingEnabled = true;
            this.cmbFilter3.Items.AddRange(new object[] {
            "Select Filter3",
            "billno",
            "Productname",
            "batch",
            "Rate",
            "totalnet",
            "Customer",
            "Supplier"});
            this.cmbFilter3.Location = new System.Drawing.Point(590, 50);
            this.cmbFilter3.Name = "cmbFilter3";
            this.cmbFilter3.Size = new System.Drawing.Size(130, 21);
            this.cmbFilter3.TabIndex = 242;
            this.cmbFilter3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFilter3_KeyDown);
            // 
            // txtFilter2
            // 
            this.txtFilter2.Location = new System.Drawing.Point(445, 52);
            this.txtFilter2.Name = "txtFilter2";
            this.txtFilter2.Size = new System.Drawing.Size(129, 20);
            this.txtFilter2.TabIndex = 241;
            this.txtFilter2.TextChanged += new System.EventHandler(this.txtFilter2_TextChanged);
            this.txtFilter2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter2_KeyDown);
            // 
            // cmbFilter2
            // 
            this.cmbFilter2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter2.FormattingEnabled = true;
            this.cmbFilter2.Items.AddRange(new object[] {
            "Select Filter2",
            "billno",
            "Productname",
            "batch",
            "Rate",
            "totalnet",
            "Customer",
            "Supplier"});
            this.cmbFilter2.Location = new System.Drawing.Point(301, 53);
            this.cmbFilter2.Name = "cmbFilter2";
            this.cmbFilter2.Size = new System.Drawing.Size(130, 21);
            this.cmbFilter2.TabIndex = 240;
            this.cmbFilter2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFilter2_KeyDown);
            // 
            // txtFilter1
            // 
            this.txtFilter1.Location = new System.Drawing.Point(156, 53);
            this.txtFilter1.Name = "txtFilter1";
            this.txtFilter1.Size = new System.Drawing.Size(129, 20);
            this.txtFilter1.TabIndex = 239;
            this.txtFilter1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter1_KeyDown);
            // 
            // cmbFilter1
            // 
            this.cmbFilter1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter1.FormattingEnabled = true;
            this.cmbFilter1.Items.AddRange(new object[] {
            "Select Filter1",
            "billno",
            "Productname",
            "batch",
            "Rate",
            "totalnet",
            "Customer",
            "Supplier"});
            this.cmbFilter1.Location = new System.Drawing.Point(13, 52);
            this.cmbFilter1.Name = "cmbFilter1";
            this.cmbFilter1.Size = new System.Drawing.Size(130, 21);
            this.cmbFilter1.TabIndex = 238;
            this.cmbFilter1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFilter1_KeyDown);
            // 
            // DTPTo
            // 
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTo.Location = new System.Drawing.Point(445, 8);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(129, 22);
            this.DTPTo.TabIndex = 235;
            this.DTPTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPTo_KeyDown);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(368, 11);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 16);
            this.Label2.TabIndex = 237;
            this.Label2.Text = "To Date";
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(157, 9);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(128, 22);
            this.DTPFrom.TabIndex = 234;
            this.DTPFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPFrom_KeyDown);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(62, 11);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 236;
            this.Label1.Text = "From Date";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtTotalAmount);
            this.panel2.Controls.Add(this.txtTotalQty);
            this.panel2.Controls.Add(this.txtTotalRate);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.grdData);
            this.panel2.Location = new System.Drawing.Point(1, 113);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1079, 415);
            this.panel2.TabIndex = 209;
            // 
            // grdData
            // 
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            this.grdData.BackgroundColor = System.Drawing.Color.White;
            this.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdData.Location = new System.Drawing.Point(5, 3);
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdData.Size = new System.Drawing.Size(1069, 370);
            this.grdData.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(411, 376);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 261;
            this.label3.Text = "Total Rate";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(632, 376);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 16);
            this.label4.TabIndex = 262;
            this.label4.Text = "Total Qty";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(839, 377);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 16);
            this.label5.TabIndex = 263;
            this.label5.Text = "Total Amount";
            // 
            // txtTotalRate
            // 
            this.txtTotalRate.Location = new System.Drawing.Point(494, 377);
            this.txtTotalRate.Name = "txtTotalRate";
            this.txtTotalRate.ReadOnly = true;
            this.txtTotalRate.Size = new System.Drawing.Size(120, 20);
            this.txtTotalRate.TabIndex = 264;
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.Location = new System.Drawing.Point(709, 376);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.ReadOnly = true;
            this.txtTotalQty.Size = new System.Drawing.Size(120, 20);
            this.txtTotalQty.TabIndex = 265;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(942, 377);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(120, 20);
            this.txtTotalAmount.TabIndex = 266;
            // 
            // SalePurchaseDetailsReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1092, 531);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox14);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SalePurchaseDetailsReport";
            this.Text = "SalePurchaseDetailsReport";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.ComboBox cmbFilter1;
        private System.Windows.Forms.TextBox txtFilter1;
        private System.Windows.Forms.ComboBox cmbFilter2;
        private System.Windows.Forms.TextBox txtFilter2;
        private System.Windows.Forms.ComboBox cmbFilter3;
        private System.Windows.Forms.TextBox txtFilter3;
        internal System.Windows.Forms.Button BtnViewReport;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView grdData;
        private System.Windows.Forms.Button btncancel;
        internal System.Windows.Forms.Button btnPrint;
        internal System.Windows.Forms.Button btnExcel;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotalRate;
        private System.Windows.Forms.TextBox txtTotalQty;
        private System.Windows.Forms.TextBox txtTotalAmount;
    }
}