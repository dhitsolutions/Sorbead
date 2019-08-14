namespace RamdevSales
{
    partial class ItemWiseStock
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
            this.label4 = new System.Windows.Forms.Label();
            this.txtopbal = new System.Windows.Forms.TextBox();
            this.txtbalance = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.txttotcredit = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.txttotdebit = new System.Windows.Forms.TextBox();
            this.LVledger = new System.Windows.Forms.ListView();
            this.cmbaccname = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DTPTo = new System.Windows.Forms.DateTimePicker();
            this.BtnViewReport = new System.Windows.Forms.Button();
            this.btngenrpt = new System.Windows.Forms.Button();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.btnclose = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbbatch = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pnlallitem = new System.Windows.Forms.Panel();
            this.grditem = new System.Windows.Forms.DataGridView();
            this.lvallitem = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtitem = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.chkshowall = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.pnlallitem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grditem)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(825, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.label4.TabIndex = 209;
            this.label4.Text = "Opening Stock";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtopbal
            // 
            this.txtopbal.BackColor = System.Drawing.SystemColors.Window;
            this.txtopbal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtopbal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtopbal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtopbal.Location = new System.Drawing.Point(942, 58);
            this.txtopbal.Name = "txtopbal";
            this.txtopbal.Size = new System.Drawing.Size(163, 22);
            this.txtopbal.TabIndex = 5;
            this.txtopbal.TabStop = false;
            this.txtopbal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtopbal.Enter += new System.EventHandler(this.txtopbal_Enter);
            this.txtopbal.Leave += new System.EventHandler(this.txtopbal_Leave);
            // 
            // txtbalance
            // 
            this.txtbalance.BackColor = System.Drawing.SystemColors.Window;
            this.txtbalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtbalance.Location = new System.Drawing.Point(985, 513);
            this.txtbalance.Name = "txtbalance";
            this.txtbalance.Size = new System.Drawing.Size(114, 23);
            this.txtbalance.TabIndex = 9;
            this.txtbalance.TabStop = false;
            this.txtbalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.Color.Blue;
            this.Label6.Location = new System.Drawing.Point(876, 497);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(104, 16);
            this.Label6.TabIndex = 204;
            this.Label6.Text = "Total Qty Out";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.Color.Blue;
            this.Label7.Location = new System.Drawing.Point(1006, 497);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(66, 16);
            this.Label7.TabIndex = 205;
            this.Label7.Text = "Balance";
            // 
            // txttotcredit
            // 
            this.txttotcredit.BackColor = System.Drawing.SystemColors.Window;
            this.txttotcredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotcredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotcredit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txttotcredit.Location = new System.Drawing.Point(757, 513);
            this.txttotcredit.Name = "txttotcredit";
            this.txttotcredit.Size = new System.Drawing.Size(114, 23);
            this.txttotcredit.TabIndex = 7;
            this.txttotcredit.TabStop = false;
            this.txttotcredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.Blue;
            this.Label5.Location = new System.Drawing.Point(768, 497);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(93, 16);
            this.Label5.TabIndex = 203;
            this.Label5.Text = "Total Qty In";
            this.Label5.Click += new System.EventHandler(this.Label5_Click);
            // 
            // txttotdebit
            // 
            this.txttotdebit.BackColor = System.Drawing.SystemColors.Window;
            this.txttotdebit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttotdebit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotdebit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txttotdebit.Location = new System.Drawing.Point(871, 513);
            this.txttotdebit.Name = "txttotdebit";
            this.txttotdebit.Size = new System.Drawing.Size(114, 23);
            this.txttotdebit.TabIndex = 8;
            this.txttotdebit.TabStop = false;
            this.txttotdebit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LVledger
            // 
            this.LVledger.BackColor = System.Drawing.SystemColors.Window;
            this.LVledger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LVledger.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVledger.ForeColor = System.Drawing.Color.Navy;
            this.LVledger.FullRowSelect = true;
            this.LVledger.GridLines = true;
            this.LVledger.HideSelection = false;
            this.LVledger.Location = new System.Drawing.Point(4, 4);
            this.LVledger.MultiSelect = false;
            this.LVledger.Name = "LVledger";
            this.LVledger.Size = new System.Drawing.Size(1077, 371);
            this.LVledger.TabIndex = 6;
            this.LVledger.UseCompatibleStateImageBehavior = false;
            this.LVledger.View = System.Windows.Forms.View.Details;
            this.LVledger.SelectedIndexChanged += new System.EventHandler(this.LVledger_SelectedIndexChanged);
            this.LVledger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LVledger_KeyDown);
            this.LVledger.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LVledger_MouseDoubleClick);
            // 
            // cmbaccname
            // 
            this.cmbaccname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbaccname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbaccname.BackColor = System.Drawing.SystemColors.Window;
            this.cmbaccname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbaccname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbaccname.FormattingEnabled = true;
            this.cmbaccname.Location = new System.Drawing.Point(2, 35);
            this.cmbaccname.Name = "cmbaccname";
            this.cmbaccname.Size = new System.Drawing.Size(21, 24);
            this.cmbaccname.TabIndex = 0;
            this.cmbaccname.Visible = false;
            this.cmbaccname.SelectedIndexChanged += new System.EventHandler(this.cmbaccname_SelectedIndexChanged);
            this.cmbaccname.Enter += new System.EventHandler(this.cmbaccname_Enter);
            this.cmbaccname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbaccname_KeyDown);
            this.cmbaccname.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbaccname_KeyUp);
            this.cmbaccname.Leave += new System.EventHandler(this.cmbaccname_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(154, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 16);
            this.label3.TabIndex = 201;
            this.label3.Text = "Select Item";
            // 
            // DTPTo
            // 
            this.DTPTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPTo.Location = new System.Drawing.Point(659, 60);
            this.DTPTo.Name = "DTPTo";
            this.DTPTo.Size = new System.Drawing.Size(146, 24);
            this.DTPTo.TabIndex = 2;
            this.DTPTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPTo_KeyDown);
            // 
            // BtnViewReport
            // 
            this.BtnViewReport.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnViewReport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnViewReport.ForeColor = System.Drawing.Color.White;
            this.BtnViewReport.Location = new System.Drawing.Point(815, 54);
            this.BtnViewReport.Name = "BtnViewReport";
            this.BtnViewReport.Size = new System.Drawing.Size(97, 34);
            this.BtnViewReport.TabIndex = 3;
            this.BtnViewReport.Text = "OK";
            this.BtnViewReport.UseVisualStyleBackColor = false;
            this.BtnViewReport.Click += new System.EventHandler(this.BtnViewReport_Click);
            this.BtnViewReport.Enter += new System.EventHandler(this.BtnViewReport_Enter);
            this.BtnViewReport.Leave += new System.EventHandler(this.BtnViewReport_Leave);
            this.BtnViewReport.MouseEnter += new System.EventHandler(this.BtnViewReport_MouseEnter);
            this.BtnViewReport.MouseLeave += new System.EventHandler(this.BtnViewReport_MouseLeave);
            // 
            // btngenrpt
            // 
            this.btngenrpt.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btngenrpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btngenrpt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngenrpt.ForeColor = System.Drawing.Color.White;
            this.btngenrpt.Location = new System.Drawing.Point(914, 54);
            this.btngenrpt.Name = "btngenrpt";
            this.btngenrpt.Size = new System.Drawing.Size(97, 34);
            this.btngenrpt.TabIndex = 4;
            this.btngenrpt.Text = "Print";
            this.btngenrpt.UseVisualStyleBackColor = false;
            this.btngenrpt.Click += new System.EventHandler(this.btngenrpt_Click);
            this.btngenrpt.Enter += new System.EventHandler(this.btngenrpt_Enter);
            this.btngenrpt.Leave += new System.EventHandler(this.btngenrpt_Leave);
            this.btngenrpt.MouseEnter += new System.EventHandler(this.btngenrpt_MouseEnter);
            this.btngenrpt.MouseLeave += new System.EventHandler(this.btngenrpt_MouseLeave);
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(500, 61);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(150, 24);
            this.DTPFrom.TabIndex = 1;
            this.DTPFrom.Enter += new System.EventHandler(this.DTPFrom_Enter);
            this.DTPFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPFrom_KeyDown);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(1012, 54);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 5;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            this.btnclose.Enter += new System.EventHandler(this.btnclose_Enter);
            this.btnclose.Leave += new System.EventHandler(this.btnclose_Leave);
            this.btnclose.MouseEnter += new System.EventHandler(this.btnclose_MouseEnter);
            this.btnclose.MouseLeave += new System.EventHandler(this.btnclose_MouseLeave);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(696, 38);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 16);
            this.Label2.TabIndex = 200;
            this.Label2.Text = "To Date";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(531, 38);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 16);
            this.Label1.TabIndex = 199;
            this.Label1.Text = "From Date";
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(0, 1);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(1122, 31);
            this.textBox7.TabIndex = 198;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "ITEM WISE STOCK";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chkshowall);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cmbbatch);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.pnlallitem);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtopbal);
            this.panel1.Controls.Add(this.txtitem);
            this.panel1.Location = new System.Drawing.Point(1, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1121, 514);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(398, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 16);
            this.label8.TabIndex = 290;
            this.label8.Text = "Batch";
            // 
            // cmbbatch
            // 
            this.cmbbatch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbbatch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbbatch.BackColor = System.Drawing.SystemColors.Window;
            this.cmbbatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbatch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbbatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbbatch.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmbbatch.FormattingEnabled = true;
            this.cmbbatch.Location = new System.Drawing.Point(390, 28);
            this.cmbbatch.Name = "cmbbatch";
            this.cmbbatch.Size = new System.Drawing.Size(102, 24);
            this.cmbbatch.TabIndex = 291;
            this.cmbbatch.SelectedIndexChanged += new System.EventHandler(this.cmbbatch_SelectedIndexChanged);
            this.cmbbatch.Enter += new System.EventHandler(this.cmbbatch_Enter);
            this.cmbbatch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbbatch_KeyDown);
            this.cmbbatch.Leave += new System.EventHandler(this.cmbbatch_Leave);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(22, 477);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(307, 27);
            this.progressBar1.TabIndex = 288;
            // 
            // pnlallitem
            // 
            this.pnlallitem.Controls.Add(this.grditem);
            this.pnlallitem.Controls.Add(this.lvallitem);
            this.pnlallitem.Location = new System.Drawing.Point(19, 52);
            this.pnlallitem.Name = "pnlallitem";
            this.pnlallitem.Size = new System.Drawing.Size(362, 389);
            this.pnlallitem.TabIndex = 213;
            this.pnlallitem.Visible = false;
            // 
            // grditem
            // 
            this.grditem.AllowUserToAddRows = false;
            this.grditem.AllowUserToDeleteRows = false;
            this.grditem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grditem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grditem.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grditem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grditem.Location = new System.Drawing.Point(3, 3);
            this.grditem.Name = "grditem";
            this.grditem.ReadOnly = true;
            this.grditem.RowHeadersVisible = false;
            this.grditem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grditem.Size = new System.Drawing.Size(353, 381);
            this.grditem.TabIndex = 283;
            this.grditem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grditem_KeyDown);
            this.grditem.MouseClick += new System.Windows.Forms.MouseEventHandler(this.grditem_MouseClick);
            this.grditem.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grditem_MouseDoubleClick);
            // 
            // lvallitem
            // 
            this.lvallitem.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.lvallitem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvallitem.BackColor = System.Drawing.Color.White;
            this.lvallitem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvallitem.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold);
            this.lvallitem.ForeColor = System.Drawing.Color.Maroon;
            this.lvallitem.FullRowSelect = true;
            this.lvallitem.GridLines = true;
            this.lvallitem.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvallitem.HideSelection = false;
            this.lvallitem.Location = new System.Drawing.Point(300, 310);
            this.lvallitem.MultiSelect = false;
            this.lvallitem.Name = "lvallitem";
            this.lvallitem.Size = new System.Drawing.Size(58, 352);
            this.lvallitem.TabIndex = 260;
            this.lvallitem.UseCompatibleStateImageBehavior = false;
            this.lvallitem.View = System.Windows.Forms.View.Details;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LVledger);
            this.panel2.Location = new System.Drawing.Point(23, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1084, 378);
            this.panel2.TabIndex = 214;
            // 
            // txtitem
            // 
            this.txtitem.BackColor = System.Drawing.SystemColors.Window;
            this.txtitem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtitem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtitem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtitem.Location = new System.Drawing.Point(23, 28);
            this.txtitem.Name = "txtitem";
            this.txtitem.Size = new System.Drawing.Size(358, 22);
            this.txtitem.TabIndex = 0;
            this.txtitem.TabStop = false;
            this.txtitem.TextChanged += new System.EventHandler(this.txtitem_TextChanged);
            this.txtitem.Enter += new System.EventHandler(this.txtitem_Enter);
            this.txtitem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtitem_KeyDown);
            this.txtitem.Leave += new System.EventHandler(this.txtitem_Leave);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // chkshowall
            // 
            this.chkshowall.AutoSize = true;
            this.chkshowall.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkshowall.Location = new System.Drawing.Point(390, 59);
            this.chkshowall.Name = "chkshowall";
            this.chkshowall.Size = new System.Drawing.Size(112, 18);
            this.chkshowall.TabIndex = 292;
            this.chkshowall.Text = "Show All Item";
            this.chkshowall.UseVisualStyleBackColor = true;
            // 
            // ItemWiseStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1132, 546);
            this.ControlBox = false;
            this.Controls.Add(this.txtbalance);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.txttotcredit);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.txttotdebit);
            this.Controls.Add(this.cmbaccname);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DTPTo);
            this.Controls.Add(this.BtnViewReport);
            this.Controls.Add(this.btngenrpt);
            this.Controls.Add(this.DTPFrom);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ItemWiseStock";
            this.Text = "ItemWiseStock";
            this.Load += new System.EventHandler(this.ItemWiseStock_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlallitem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grditem)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtopbal;
        internal System.Windows.Forms.TextBox txtbalance;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox txttotcredit;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txttotdebit;
        internal System.Windows.Forms.ListView LVledger;
        private System.Windows.Forms.ComboBox cmbaccname;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.DateTimePicker DTPTo;
        internal System.Windows.Forms.Button BtnViewReport;
        internal System.Windows.Forms.Button btngenrpt;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.TextBox txtitem;
        private System.Windows.Forms.Panel pnlallitem;
        private System.Windows.Forms.DataGridView grditem;
        internal System.Windows.Forms.ListView lvallitem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbbatch;
        private System.Windows.Forms.CheckBox chkshowall;
    }
}