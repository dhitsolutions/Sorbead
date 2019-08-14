namespace RamdevSales
{
    partial class ProductionPlanning
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
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnllockorder = new System.Windows.Forms.Panel();
            this.btnsavepanel = new System.Windows.Forms.Button();
            this.btnclosepenal = new System.Windows.Forms.Button();
            this.txtremarks = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.DTPFrom = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtorderno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnlock = new System.Windows.Forms.Button();
            this.btnplace = new System.Windows.Forms.Button();
            this.lvaltitem = new System.Windows.Forms.ListView();
            this.btnok = new System.Windows.Forms.Button();
            this.lvmainitem = new System.Windows.Forms.ListView();
            this.btnaddraw = new System.Windows.Forms.Button();
            this.txtpunit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtpqty = new System.Windows.Forms.TextBox();
            this.lblcessamt = new System.Windows.Forms.Label();
            this.cmbprocessname = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbitemtoproduce = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnreste = new System.Windows.Forms.Button();
            this.lvprocess = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.pnllockorder.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Highlight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(0, 0);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(1091, 31);
            this.textBox7.TabIndex = 179;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "PRODUCTION PLANNING";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pnllockorder);
            this.panel1.Controls.Add(this.lvprocess);
            this.panel1.Controls.Add(this.btnlock);
            this.panel1.Controls.Add(this.btnplace);
            this.panel1.Controls.Add(this.lvaltitem);
            this.panel1.Controls.Add(this.btnok);
            this.panel1.Controls.Add(this.lvmainitem);
            this.panel1.Controls.Add(this.btnaddraw);
            this.panel1.Controls.Add(this.txtpunit);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtpqty);
            this.panel1.Controls.Add(this.lblcessamt);
            this.panel1.Controls.Add(this.cmbprocessname);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbitemtoproduce);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnprint);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.btnreste);
            this.panel1.Location = new System.Drawing.Point(2, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1089, 588);
            this.panel1.TabIndex = 0;
            // 
            // pnllockorder
            // 
            this.pnllockorder.Controls.Add(this.btnsavepanel);
            this.pnllockorder.Controls.Add(this.btnclosepenal);
            this.pnllockorder.Controls.Add(this.txtremarks);
            this.pnllockorder.Controls.Add(this.label6);
            this.pnllockorder.Controls.Add(this.DTPFrom);
            this.pnllockorder.Controls.Add(this.label5);
            this.pnllockorder.Controls.Add(this.txtorderno);
            this.pnllockorder.Controls.Add(this.label3);
            this.pnllockorder.Location = new System.Drawing.Point(215, 196);
            this.pnllockorder.Name = "pnllockorder";
            this.pnllockorder.Size = new System.Drawing.Size(378, 245);
            this.pnllockorder.TabIndex = 200;
            this.pnllockorder.Visible = false;
            // 
            // btnsavepanel
            // 
            this.btnsavepanel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnsavepanel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnsavepanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsavepanel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsavepanel.ForeColor = System.Drawing.Color.White;
            this.btnsavepanel.Location = new System.Drawing.Point(168, 200);
            this.btnsavepanel.Name = "btnsavepanel";
            this.btnsavepanel.Size = new System.Drawing.Size(97, 34);
            this.btnsavepanel.TabIndex = 205;
            this.btnsavepanel.Text = "Submit";
            this.btnsavepanel.UseVisualStyleBackColor = false;
            this.btnsavepanel.Click += new System.EventHandler(this.btnsavepanel_Click);
            this.btnsavepanel.Enter += new System.EventHandler(this.btnsavepanel_Enter);
            this.btnsavepanel.Leave += new System.EventHandler(this.btnsavepanel_Leave);
            this.btnsavepanel.MouseEnter += new System.EventHandler(this.btnsavepanel_MouseEnter);
            this.btnsavepanel.MouseLeave += new System.EventHandler(this.btnsavepanel_MouseLeave);
            // 
            // btnclosepenal
            // 
            this.btnclosepenal.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclosepenal.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclosepenal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclosepenal.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclosepenal.ForeColor = System.Drawing.Color.White;
            this.btnclosepenal.Location = new System.Drawing.Point(271, 200);
            this.btnclosepenal.Name = "btnclosepenal";
            this.btnclosepenal.Size = new System.Drawing.Size(97, 34);
            this.btnclosepenal.TabIndex = 201;
            this.btnclosepenal.Text = "Close";
            this.btnclosepenal.UseVisualStyleBackColor = false;
            this.btnclosepenal.Click += new System.EventHandler(this.btnclosepenal_Click);
            this.btnclosepenal.Enter += new System.EventHandler(this.btnclosepenal_Enter);
            this.btnclosepenal.Leave += new System.EventHandler(this.btnclosepenal_Leave);
            this.btnclosepenal.MouseEnter += new System.EventHandler(this.btnclosepenal_MouseEnter);
            this.btnclosepenal.MouseLeave += new System.EventHandler(this.btnclosepenal_MouseLeave);
            // 
            // txtremarks
            // 
            this.txtremarks.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtremarks.Location = new System.Drawing.Point(14, 89);
            this.txtremarks.Multiline = true;
            this.txtremarks.Name = "txtremarks";
            this.txtremarks.Size = new System.Drawing.Size(354, 105);
            this.txtremarks.TabIndex = 201;
            this.txtremarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtremarks_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(14, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 17);
            this.label6.TabIndex = 202;
            this.label6.Text = "Remarks";
            // 
            // DTPFrom
            // 
            this.DTPFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFrom.Location = new System.Drawing.Point(187, 37);
            this.DTPFrom.Name = "DTPFrom";
            this.DTPFrom.Size = new System.Drawing.Size(181, 22);
            this.DTPFrom.TabIndex = 204;
            this.DTPFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPFrom_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(216, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 16);
            this.label5.TabIndex = 203;
            this.label5.Text = "Lock Order Date";
            // 
            // txtorderno
            // 
            this.txtorderno.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtorderno.Location = new System.Drawing.Point(14, 35);
            this.txtorderno.Name = "txtorderno";
            this.txtorderno.Size = new System.Drawing.Size(158, 24);
            this.txtorderno.TabIndex = 201;
            this.txtorderno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtorderno_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(57, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.TabIndex = 202;
            this.label3.Text = "Order No";
            // 
            // btnlock
            // 
            this.btnlock.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlock.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlock.ForeColor = System.Drawing.Color.White;
            this.btnlock.Location = new System.Drawing.Point(775, 546);
            this.btnlock.Name = "btnlock";
            this.btnlock.Size = new System.Drawing.Size(151, 34);
            this.btnlock.TabIndex = 199;
            this.btnlock.Text = "&Lock Order";
            this.btnlock.UseVisualStyleBackColor = false;
            this.btnlock.Click += new System.EventHandler(this.btnlock_Click);
            // 
            // btnplace
            // 
            this.btnplace.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnplace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnplace.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnplace.ForeColor = System.Drawing.Color.White;
            this.btnplace.Location = new System.Drawing.Point(929, 546);
            this.btnplace.Name = "btnplace";
            this.btnplace.Size = new System.Drawing.Size(149, 34);
            this.btnplace.TabIndex = 198;
            this.btnplace.Text = "&Place Order";
            this.btnplace.UseVisualStyleBackColor = false;
            this.btnplace.Click += new System.EventHandler(this.btnplace_Click);
            // 
            // lvaltitem
            // 
            this.lvaltitem.BackColor = System.Drawing.SystemColors.Window;
            this.lvaltitem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvaltitem.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvaltitem.ForeColor = System.Drawing.Color.Navy;
            this.lvaltitem.FullRowSelect = true;
            this.lvaltitem.GridLines = true;
            this.lvaltitem.HideSelection = false;
            this.lvaltitem.Location = new System.Drawing.Point(10, 349);
            this.lvaltitem.MultiSelect = false;
            this.lvaltitem.Name = "lvaltitem";
            this.lvaltitem.Size = new System.Drawing.Size(1068, 189);
            this.lvaltitem.TabIndex = 7;
            this.lvaltitem.UseCompatibleStateImageBehavior = false;
            this.lvaltitem.View = System.Windows.Forms.View.Details;
            // 
            // btnok
            // 
            this.btnok.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnok.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnok.ForeColor = System.Drawing.Color.White;
            this.btnok.Location = new System.Drawing.Point(981, 241);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(97, 34);
            this.btnok.TabIndex = 6;
            this.btnok.Text = "&OK";
            this.btnok.UseVisualStyleBackColor = false;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            this.btnok.Enter += new System.EventHandler(this.btnok_Enter);
            this.btnok.Leave += new System.EventHandler(this.btnok_Leave);
            this.btnok.MouseEnter += new System.EventHandler(this.btnok_MouseEnter);
            this.btnok.MouseLeave += new System.EventHandler(this.btnok_MouseLeave);
            // 
            // lvmainitem
            // 
            this.lvmainitem.BackColor = System.Drawing.SystemColors.Window;
            this.lvmainitem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvmainitem.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvmainitem.ForeColor = System.Drawing.Color.Navy;
            this.lvmainitem.FullRowSelect = true;
            this.lvmainitem.GridLines = true;
            this.lvmainitem.HideSelection = false;
            this.lvmainitem.Location = new System.Drawing.Point(10, 83);
            this.lvmainitem.MultiSelect = false;
            this.lvmainitem.Name = "lvmainitem";
            this.lvmainitem.Size = new System.Drawing.Size(1068, 152);
            this.lvmainitem.TabIndex = 5;
            this.lvmainitem.UseCompatibleStateImageBehavior = false;
            this.lvmainitem.View = System.Windows.Forms.View.Details;
            this.lvmainitem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvmainitem_KeyDown);
            this.lvmainitem.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvmainitem_MouseDoubleClick);
            // 
            // btnaddraw
            // 
            this.btnaddraw.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnaddraw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaddraw.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaddraw.ForeColor = System.Drawing.Color.White;
            this.btnaddraw.Location = new System.Drawing.Point(774, 43);
            this.btnaddraw.Name = "btnaddraw";
            this.btnaddraw.Size = new System.Drawing.Size(97, 31);
            this.btnaddraw.TabIndex = 4;
            this.btnaddraw.Text = "Add Item";
            this.btnaddraw.UseVisualStyleBackColor = false;
            this.btnaddraw.Click += new System.EventHandler(this.btnaddraw_Click);
            this.btnaddraw.Enter += new System.EventHandler(this.btnaddraw_Enter);
            this.btnaddraw.Leave += new System.EventHandler(this.btnaddraw_Leave);
            this.btnaddraw.MouseEnter += new System.EventHandler(this.btnaddraw_MouseEnter);
            this.btnaddraw.MouseLeave += new System.EventHandler(this.btnaddraw_MouseLeave);
            // 
            // txtpunit
            // 
            this.txtpunit.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpunit.Location = new System.Drawing.Point(638, 50);
            this.txtpunit.Name = "txtpunit";
            this.txtpunit.Size = new System.Drawing.Size(104, 24);
            this.txtpunit.TabIndex = 3;
            this.txtpunit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpunit_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(635, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 197;
            this.label1.Text = "Unit";
            // 
            // txtpqty
            // 
            this.txtpqty.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpqty.Location = new System.Drawing.Point(528, 50);
            this.txtpqty.Name = "txtpqty";
            this.txtpqty.Size = new System.Drawing.Size(104, 24);
            this.txtpqty.TabIndex = 2;
            this.txtpqty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpqty_KeyDown);
            // 
            // lblcessamt
            // 
            this.lblcessamt.AutoSize = true;
            this.lblcessamt.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcessamt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblcessamt.Location = new System.Drawing.Point(525, 30);
            this.lblcessamt.Name = "lblcessamt";
            this.lblcessamt.Size = new System.Drawing.Size(68, 17);
            this.lblcessamt.TabIndex = 195;
            this.lblcessamt.Text = "Quantity";
            // 
            // cmbprocessname
            // 
            this.cmbprocessname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbprocessname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbprocessname.BackColor = System.Drawing.SystemColors.Window;
            this.cmbprocessname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbprocessname.FormattingEnabled = true;
            this.cmbprocessname.Location = new System.Drawing.Point(311, 52);
            this.cmbprocessname.Name = "cmbprocessname";
            this.cmbprocessname.Size = new System.Drawing.Size(202, 21);
            this.cmbprocessname.TabIndex = 1;
            this.cmbprocessname.SelectedIndexChanged += new System.EventHandler(this.cmbprocessname_SelectedIndexChanged);
            this.cmbprocessname.Enter += new System.EventHandler(this.cmbprocessname_Enter);
            this.cmbprocessname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbprocessname_KeyDown);
            this.cmbprocessname.Leave += new System.EventHandler(this.cmbprocessname_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(358, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 17);
            this.label2.TabIndex = 193;
            this.label2.Text = "Process Name";
            // 
            // cmbitemtoproduce
            // 
            this.cmbitemtoproduce.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbitemtoproduce.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbitemtoproduce.BackColor = System.Drawing.SystemColors.Window;
            this.cmbitemtoproduce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbitemtoproduce.FormattingEnabled = true;
            this.cmbitemtoproduce.Location = new System.Drawing.Point(10, 52);
            this.cmbitemtoproduce.Name = "cmbitemtoproduce";
            this.cmbitemtoproduce.Size = new System.Drawing.Size(289, 21);
            this.cmbitemtoproduce.TabIndex = 0;
            this.cmbitemtoproduce.SelectedIndexChanged += new System.EventHandler(this.cmbitemtoproduce_SelectedIndexChanged);
            this.cmbitemtoproduce.Enter += new System.EventHandler(this.cmbitemtoproduce_Enter);
            this.cmbitemtoproduce.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbitemtoproduce_KeyDown);
            this.cmbitemtoproduce.Leave += new System.EventHandler(this.cmbitemtoproduce_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(22, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 17);
            this.label4.TabIndex = 190;
            this.label4.Text = "Item To Produce";
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(878, 3);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(97, 34);
            this.btnprint.TabIndex = 9;
            this.btnprint.Text = "&Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            this.btnprint.Enter += new System.EventHandler(this.btnprint_Enter);
            this.btnprint.Leave += new System.EventHandler(this.btnprint_Leave);
            this.btnprint.MouseEnter += new System.EventHandler(this.btnprint_MouseEnter);
            this.btnprint.MouseLeave += new System.EventHandler(this.btnprint_MouseLeave);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(981, 3);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(97, 34);
            this.btnclose.TabIndex = 10;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            this.btnclose.Enter += new System.EventHandler(this.btnclose_Enter);
            this.btnclose.Leave += new System.EventHandler(this.btnclose_Leave);
            this.btnclose.MouseEnter += new System.EventHandler(this.btnclose_MouseEnter);
            this.btnclose.MouseLeave += new System.EventHandler(this.btnclose_MouseLeave);
            // 
            // btnreste
            // 
            this.btnreste.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnreste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnreste.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnreste.ForeColor = System.Drawing.Color.White;
            this.btnreste.Location = new System.Drawing.Point(774, 3);
            this.btnreste.Name = "btnreste";
            this.btnreste.Size = new System.Drawing.Size(97, 34);
            this.btnreste.TabIndex = 8;
            this.btnreste.Text = "&Reset";
            this.btnreste.UseVisualStyleBackColor = false;
            this.btnreste.Enter += new System.EventHandler(this.btnreste_Enter);
            this.btnreste.Leave += new System.EventHandler(this.btnreste_Leave);
            this.btnreste.MouseEnter += new System.EventHandler(this.btnreste_MouseEnter);
            this.btnreste.MouseLeave += new System.EventHandler(this.btnreste_MouseLeave);
            // 
            // lvprocess
            // 
            this.lvprocess.BackColor = System.Drawing.SystemColors.Window;
            this.lvprocess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvprocess.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvprocess.ForeColor = System.Drawing.Color.Navy;
            this.lvprocess.FullRowSelect = true;
            this.lvprocess.GridLines = true;
            this.lvprocess.HideSelection = false;
            this.lvprocess.Location = new System.Drawing.Point(10, 241);
            this.lvprocess.MultiSelect = false;
            this.lvprocess.Name = "lvprocess";
            this.lvprocess.Size = new System.Drawing.Size(965, 102);
            this.lvprocess.TabIndex = 201;
            this.lvprocess.UseCompatibleStateImageBehavior = false;
            this.lvprocess.View = System.Windows.Forms.View.Details;
            // 
            // ProductionPlanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 625);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProductionPlanning";
            this.Text = "ProductionPlanning";
            this.Load += new System.EventHandler(this.ProductionPlanning_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnllockorder.ResumeLayout(false);
            this.pnllockorder.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnprint;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Button btnreste;
        private System.Windows.Forms.ComboBox cmbitemtoproduce;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbprocessname;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtpunit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtpqty;
        private System.Windows.Forms.Label lblcessamt;
        internal System.Windows.Forms.Button btnaddraw;
        internal System.Windows.Forms.ListView lvmainitem;
        internal System.Windows.Forms.ListView lvaltitem;
        internal System.Windows.Forms.Button btnok;
        internal System.Windows.Forms.Button btnlock;
        internal System.Windows.Forms.Button btnplace;
        private System.Windows.Forms.Panel pnllockorder;
        private System.Windows.Forms.TextBox txtorderno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtremarks;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.DateTimePicker DTPFrom;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Button btnsavepanel;
        internal System.Windows.Forms.Button btnclosepenal;
        internal System.Windows.Forms.ListView lvprocess;
    }
}