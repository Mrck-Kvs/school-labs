using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JeuDeminage
{
    internal class ControleDemineur : UserControl
    {
        private const int DIMENSION_BOUTON = 50;
        private const int MARGE = 5;

        private List<BoutonCellule> listeBoutons;
        private GestionnaireJeu gestionnaire;

        public ControleDemineur()
        {
            listeBoutons = new List<BoutonCellule>();
            gestionnaire = GestionnaireJeu.Instance;

            InitialiserInterface();
            GenererBoutons();
        }

        private void GenererBoutons()
        {
            for (int colonne = 0; colonne < 9; colonne++)
            {
                for (int ligne = 0; ligne < 9; ligne++)
                {
                    var bouton = CreerBouton(colonne, ligne);
                    listeBoutons.Add(bouton);
                    Controls.Add(bouton);
                }
            }
        }

        private BoutonCellule CreerBouton(int col, int lig)
        {
            var bouton = new BoutonCellule();
            bouton.Size = new Size(DIMENSION_BOUTON, DIMENSION_BOUTON);
            bouton.Location = CalculerPosition(col, lig);
            bouton.Coordonnees = new Point(col, lig);
            bouton.MouseUp += SurClicBouton;
            return bouton;
        }

        private Point CalculerPosition(int col, int lig)
        {
            int posX = MARGE + (DIMENSION_BOUTON + MARGE) * col;
            int posY = MARGE + (DIMENSION_BOUTON + MARGE) * lig;
            return new Point(posX, posY);
        }

        private void RafraichirAffichage()
        {
            var cellules = gestionnaire.RecupererCellules();

            foreach (BoutonCellule bouton in listeBoutons)
            {
                Cellule cellule = cellules[bouton.Coordonnees];
                MettreAJourBouton(bouton, cellule);
            }
        }

        private void MettreAJourBouton(BoutonCellule bouton, Cellule cellule)
        {
            if (cellule.Statut is StatutRevelee)
            {
                AfficherCelluleRevelee(bouton, cellule);
            }
            else if (cellule.Statut is StatutSignalee)
            {
                bouton.BackColor = Color.Yellow;
                bouton.Text = "";
            }
            else
            {
                bouton.BackColor = Color.White;
                bouton.Text = "";
            }
        }

        private void AfficherCelluleRevelee(BoutonCellule bouton, Cellule cellule)
        {
            if (cellule is CelluleLibre)
            {
                bouton.Text = "0";
                bouton.BackColor = Color.LightGray;
            }
            else if (cellule is CelluleIndicateur indicateur)
            {
                bouton.Text = indicateur.NombreMenaces.ToString();
                bouton.BackColor = Color.LightGray;
            }
            else
            {
                bouton.BackColor = Color.Red;
                bouton.Text = "X";
            }
        }

        private void SurClicBouton(object emetteur, MouseEventArgs args)
        {
            var bouton = (BoutonCellule)emetteur;

            if (args.Button == MouseButtons.Left)
                gestionnaire.RevelerCellule(bouton.Coordonnees);
            else if (args.Button == MouseButtons.Right)
                gestionnaire.SignalerCellule(bouton.Coordonnees);

            RafraichirAffichage();
            gestionnaire.VerifierVictoire();

            VerifierFinPartie();
        }

        private void VerifierFinPartie()
        {
            if (gestionnaire.EtatPartie == null)
                return;

            string message = gestionnaire.EtatPartie == true
                ? "Bravo, vous avez gagné !"
                : "Dommage, vous avez perdu...";

            var resultat = MessageBox.Show(
                message,
                "Fin de partie",
                MessageBoxButtons.RetryCancel
            );

            if (resultat == DialogResult.Retry)
            {
                GestionnaireJeu.ReinitialiserInstance();
                gestionnaire = GestionnaireJeu.Instance;
                ReinitialiserBoutons();
            }
            else
            {
                Application.Exit();
            }
        }

        private void ReinitialiserBoutons()
        {
            foreach (var bouton in listeBoutons)
            {
                bouton.Text = "";
                bouton.BackColor = Color.White;
            }
        }

        private void InitialiserInterface()
        {
            SuspendLayout();
            Name = "ControleDemineur";
            Size = new Size(944, 760);
            ResumeLayout(false);
        }
    }
}
