using System;
using System.Collections.Generic;
using System.Drawing;

namespace JeuDeminage
{
    internal class GrilleJeu
    {
        private static readonly Random generateur = new Random();
        private readonly int nbLignes;
        private readonly int nbColonnes;
        private Dictionary<Point, Cellule> tableauCellules;

        public Dictionary<Point, Cellule> Cellules => tableauCellules;

        public GrilleJeu(int totalMenaces, int lignes = 9, int colonnes = 9)
        {
            nbLignes = lignes;
            nbColonnes = colonnes;
            tableauCellules = new Dictionary<Point, Cellule>();

            PlacerMenacesAleatoirement(totalMenaces);
            RemplirCellulesRestantes();
        }

        private void PlacerMenacesAleatoirement(int nombreMenaces)
        {
            int menacesPlacees = 0;

            while (menacesPlacees < nombreMenaces)
            {
                int col = generateur.Next(0, nbColonnes);
                int lig = generateur.Next(0, nbLignes);
                Point emplacement = new Point(col, lig);

                if (!tableauCellules.ContainsKey(emplacement))
                {
                    tableauCellules.Add(emplacement, new CellulePiegee(emplacement));
                    menacesPlacees++;
                }
            }
        }

        private void RemplirCellulesRestantes()
        {
            for (int ligne = 0; ligne < nbLignes; ligne++)
            {
                for (int colonne = 0; colonne < nbColonnes; colonne++)
                {
                    Point position = new Point(colonne, ligne);

                    if (!tableauCellules.ContainsKey(position))
                    {
                        var adjacentes = ObtenirPositionsAdjacentes(position);
                        int menacesProches = CompterMenacesAdjacentes(adjacentes);

                        if (menacesProches == 0)
                            tableauCellules[position] = new CelluleLibre(position, adjacentes);
                        else
                            tableauCellules[position] = new CelluleIndicateur(position, menacesProches);
                    }
                }
            }
        }

        private List<Point> ObtenirPositionsAdjacentes(Point centre)
        {
            var adjacentes = new List<Point>();

            for (int dLigne = -1; dLigne <= 1; dLigne++)
            {
                for (int dColonne = -1; dColonne <= 1; dColonne++)
                {
                    if (dLigne == 0 && dColonne == 0)
                        continue;

                    Point voisin = new Point(centre.X + dColonne, centre.Y + dLigne);

                    if (EstDansLimites(voisin))
                        adjacentes.Add(voisin);
                }
            }

            return adjacentes;
        }

        private bool EstDansLimites(Point p)
        {
            return p.X >= 0 && p.X < nbColonnes && p.Y >= 0 && p.Y < nbLignes;
        }

        private int CompterMenacesAdjacentes(List<Point> positions)
        {
            int compteur = 0;

            foreach (Point pos in positions)
            {
                if (tableauCellules.ContainsKey(pos) && tableauCellules[pos] is CellulePiegee)
                    compteur++;
            }

            return compteur;
        }

        public void SignalerCellule(Point position)
        {
            tableauCellules[position].BasculerDrapeau();
        }

        public void RevelerCellule(Point position)
        {
            Cellule cellule = tableauCellules[position];
            cellule.Reveler();
        }
    }
}
