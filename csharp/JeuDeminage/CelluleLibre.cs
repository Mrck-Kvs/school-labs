using System;
using System.Collections.Generic;
using System.Drawing;

namespace JeuDeminage
{
    internal class CelluleLibre : Cellule
    {
        private List<Point> cellulesAdjacentes;

        public CelluleLibre(Point pos, List<Point> adjacentes) : base(pos)
        {
            cellulesAdjacentes = adjacentes;
        }

        public override void ExecuterAction()
        {
            var gestionnaire = GestionnaireJeu.Instance;
            foreach (Point adjacent in cellulesAdjacentes)
            {
                gestionnaire.RevelerCellule(adjacent);
            }
        }
    }
}
