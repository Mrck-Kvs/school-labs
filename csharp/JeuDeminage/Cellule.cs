using System;
using System.Drawing;

namespace JeuDeminage
{
    public abstract class Cellule
    {
        protected StatutCellule statutActuel;
        protected Point position;

        public StatutCellule Statut => statutActuel;
        public Point Position => position;

        protected Cellule(Point pos)
        {
            position = pos;
            statutActuel = new StatutMasquee();
        }

        public void BasculerDrapeau()
        {
            if (!(statutActuel is StatutRevelee))
                statutActuel.GererDrapeau(this);
        }

        public void Reveler()
        {
            if (statutActuel is StatutMasquee)
                statutActuel.GererRevelation(this);
        }

        public abstract void ExecuterAction();

        public void ModifierStatut(StatutCellule nouveauStatut)
        {
            statutActuel = nouveauStatut;
        }
    }
}
