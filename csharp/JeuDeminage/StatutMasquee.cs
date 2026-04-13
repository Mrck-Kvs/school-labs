using System;

namespace JeuDeminage
{
    internal class StatutMasquee : StatutCellule
    {
        public override void GererDrapeau(Cellule cellule)
        {
            if (cellule is CellulePiegee)
                GestionnaireJeu.Instance.DiminuerCompteurMenaces();

            cellule.ModifierStatut(new StatutSignalee());
        }

        public override void GererRevelation(Cellule cellule)
        {
            if (cellule.Statut is StatutMasquee)
            {
                cellule.ModifierStatut(new StatutRevelee());
                cellule.ExecuterAction();
            }
        }
    }
}
