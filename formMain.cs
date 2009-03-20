using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MPS
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
            treeNetwerken.ExpandAll();
        }

        public Control Panel
        {
            get { return splitContainer2.Panel1; }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Netwerk.Update();
        }
    }
}