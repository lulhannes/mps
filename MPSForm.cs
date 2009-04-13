using System;
using System.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MPS
{
    public partial class MPSForm : Form
    {
        public bool MenuOpened { get; set; }

        private Point muis;

        public MPSForm()
        {
            InitializeComponent();

            MenuOpened = false;
            muis = new Point();
        }

        public void UpdateList()
        {
            lbInfecties.Items.Clear();
            foreach(Malware m in Netwerk.Malwares)
            {
                lbInfecties.Items.Add(m.Naam);
            }

        }

        public Control Panel
        {
            get { return splitContainer.Panel1; }
        }

        private void cntxtNetwerk_Opening(object sender, CancelEventArgs e)
        {
            MenuOpened = true;
            muis = Panel.PointToClient(MousePosition);

            bool isGeselecteerd = SpriteManager.Geselecteerde != null;
            {
                cntxtNetwerk.Items[2].Enabled = isGeselecteerd;
            }
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

        private void verwijderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Netwerk.RemoveApparaat(SpriteManager.Geselecteerde);
        }

        private void verbreekVerbindingMetOuderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Netwerk.VerbreekVerbinding(SpriteManager.Geselecteerde);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new AddMalwareForm().ShowDialog();
            UpdateList();
        }
    }
}