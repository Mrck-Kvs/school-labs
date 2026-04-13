using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PaquetVirtuel
{
    internal class TableDeJeu : PictureBox
    {
        public List<Carte> listeCartesOrdonnees;
        public List<Carte> listeCartesMelangees;

        private const int CARTES_PAR_ENSEIGNE = 13;
        private const int NOMBRE_ENSEIGNES = 4;
        private static Bitmap feuilleSpritesPartagee;

        public TableDeJeu()
        {
            Width = 800;
            Height = 600;
            listeCartesOrdonnees = new List<Carte>();
            listeCartesMelangees = new List<Carte>();
        }

        public void GenererPaquet(bool melanger)
        {
            if (feuilleSpritesPartagee == null)
                feuilleSpritesPartagee = (Bitmap)Bitmap.FromFile("Assets/CardsSprite.png");

            Bitmap feuilleSprites = feuilleSpritesPartagee;
            LinkedList<Carte> paquet = new LinkedList<Carte>();

            for (int i = 0; i < NOMBRE_ENSEIGNES; i++)
            {
                for (int j = 1; j <= CARTES_PAR_ENSEIGNE; j++)
                {
                    string nomCarte = ObtenirNomCarte(j);
                    Enseigne enseigne = (Enseigne)i;
                    Point origine = new Point((j - 1) * ImageCarte.LARGEUR_CARTE, i * ImageCarte.HAUTEUR_CARTE);
                    ImageCarte visuel = new ImageCarte(feuilleSprites, origine);
                    Carte carte = new Carte(nomCarte, enseigne, visuel, j);
                    carte.DefinirNoeud(paquet.AddLast(carte));
                }
            }

            if (melanger)
            {
                Random generateur = new Random();
                listeCartesMelangees = paquet.OrderBy(c => generateur.Next()).ToList();
            }
            else
            {
                listeCartesOrdonnees = paquet.ToList();
            }

            Invalidate();
        }

        private string ObtenirNomCarte(int numero)
        {
            switch (numero)
            {
                case 1:
                    return "As";
                case 11:
                    return "Valet";
                case 12:
                    return "Dame";
                case 13:
                    return "Roi";
                default:
                    return numero.ToString();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DessinerCartes(e.Graphics, listeCartesOrdonnees, 10);
            DessinerCartesDecalees(e.Graphics, listeCartesMelangees, 200);
        }

        private void DessinerCartes(Graphics g, List<Carte> cartes, int positionY)
        {
            int nombreCartes = cartes.Count;
            for (int i = 0; i < nombreCartes; i++)
            {
                var carte = cartes[i];
                g.DrawImage(carte.Visuel.ObtenirRecto(), i * 8, positionY, 80, 120);
            }
        }

        private void DessinerCartesDecalees(Graphics g, List<Carte> cartes, int positionYBase)
        {
            int nombreCartes = cartes.Count;
            for (int i = 0; i < nombreCartes; i++)
            {
                var carte = cartes[i];
                g.DrawImage(carte.Visuel.ObtenirRecto(), i * 8, positionYBase + (i * 2), 80, 120);
            }
        }
    }
}
