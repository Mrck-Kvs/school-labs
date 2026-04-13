using System;
using System.Drawing;

namespace JeuDeminage
{
    internal class CellulePiegee : Cellule
    {
        public CellulePiegee(Point pos) : base(pos) { }

        public override void ExecuterAction()
        {
            GestionnaireJeu.Instance.DeclencherDefaite();
        }
    }
}
