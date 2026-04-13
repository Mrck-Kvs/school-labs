using System;

namespace JeuDeminage
{
    internal class StatutSignalee : StatutCellule
    {
        public override void GererDrapeau(Cellule cellule)
        {
            if (cellule is CellulePiegee)
                GestionnaireJeu.Instance.AugmenterCompteurMenaces();

            cellule.ModifierStatut(new StatutMasquee());
        }

        public override void GererRevelation(Cellule cellule)
        {
            // Une cellule signalée ne peut pas être révélée directement
        }
    }
}
