using System;
using System.Drawing;

namespace JeuDeminage
{
    internal class CelluleIndicateur : Cellule
    {
        private int nombreMenacesAdjacentes;

        public int NombreMenaces => nombreMenacesAdjacentes;

        public CelluleIndicateur(Point pos, int menaces) : base(pos)
        {
            nombreMenacesAdjacentes = menaces;
        }

        public override void ExecuterAction()
        {
            GestionnaireJeu.Instance.VerifierVictoire();
        }
    }
}
