using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RamdevSales
{
    public partial class ItemWiseSaleList : Form
    {
        private Master master;
        private TabControl tabControl;

        public ItemWiseSaleList()
        {
            InitializeComponent();
        }

        public ItemWiseSaleList(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }
    }
}
