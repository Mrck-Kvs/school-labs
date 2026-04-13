using System;
using System.Windows.Forms;

namespace PaquetVirtuel
{
    public partial class ControlePartie : UserControl
    {
        public ControlePartie()
        {
            InitializeComponent();
        }

        private void BtnOrdonne_Click(object sender, EventArgs e)
        {
            zoneAffichage.GenererPaquet(false);
        }

        private void BtnMelange_Click(object sender, EventArgs e)
        {
            zoneAffichage.GenererPaquet(true);
        }
    }
}
