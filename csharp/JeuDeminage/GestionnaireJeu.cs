using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace JeuDeminage
{
    public class GestionnaireJeu
    {
        private const int MENACES_INITIALES = 10;
        private static GestionnaireJeu instanceUnique;

        private GrilleJeu grille;
        private CompteurTemps compteur;
        private int menacesRestantes;
        private string difficulte;
        private bool? etatPartie;

        public bool? EtatPartie => etatPartie;

        private GestionnaireJeu()
        {
            difficulte = "facile";
            menacesRestantes = MENACES_INITIALES;
            grille = new GrilleJeu(MENACES_INITIALES);
            compteur = new CompteurTemps();
            compteur.Demarrer();
            etatPartie = null;
        }

        public static GestionnaireJeu Instance
        {
            get
            {
                if (instanceUnique == null)
                    instanceUnique = new GestionnaireJeu();
                return instanceUnique;
            }
        }

        public static void ReinitialiserInstance()
        {
            instanceUnique = null;
        }

        public void SignalerCellule(Point position)
        {
            grille.SignalerCellule(position);
        }

        public void RevelerCellule(Point position)
        {
            grille.RevelerCellule(position);
        }

        public void DiminuerCompteurMenaces()
        {
            menacesRestantes--;
        }

        public void AugmenterCompteurMenaces()
        {
            menacesRestantes++;
        }

        public void DeclencherDefaite()
        {
            etatPartie = false;
            compteur.Arreter();
        }

        public void VerifierVictoire()
        {
            bool partieGagnee = true;

            foreach (Cellule cellule in grille.Cellules.Values)
            {
                bool estMenace = cellule is CellulePiegee;
                bool estRevelee = cellule.Statut is StatutRevelee;

                if (!estMenace && !estRevelee && menacesRestantes > 0)
                {
                    partieGagnee = false;
                    break;
                }
            }

            if (partieGagnee)
            {
                etatPartie = true;
                compteur.Arreter();
            }
        }

        public Dictionary<Point, Cellule> RecupererCellules()
        {
            return grille.Cellules.ToDictionary(paire => paire.Key, paire => paire.Value);
        }
    }
}
