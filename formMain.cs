using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MPS
{
    public partial class formMain : Form
    {
        public bool MenuOpened { get; set; }

        private Point muis;

        public formMain()
        {
            InitializeComponent();

            MenuOpened = false;
            muis = new Point();
        }

        public Control Panel
        {
            get { return splitContainer.Panel1; }
        }

        private void cntxtNetwerk_Opening(object sender, CancelEventArgs e)
        {
            MenuOpened = true;
            muis = Panel.PointToClient(MousePosition);
        }

        private void cntxtNetwerk_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            MenuOpened = false;
        }

        private void nieuweComputerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Netwerk.AddComputer(ApparaatType.Pc, 0, 0, SpriteManager.ConvertMuis(muis));
        }

        private void nieuweLaptopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Netwerk.AddComputer(ApparaatType.Laptop, 0, 0, SpriteManager.ConvertMuis(muis));
        }

        private void nieuweRouterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Netwerk.AddNetwerkApparaat(ApparaatType.Router, SpriteManager.ConvertMuis(muis));
        }

        private void nieuweSwitchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Netwerk.AddNetwerkApparaat(ApparaatType.Switch, SpriteManager.ConvertMuis(muis));
        }
    }
}