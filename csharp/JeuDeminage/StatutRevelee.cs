using System;

namespace JeuDeminage
{
    internal class StatutRevelee : StatutCellule
    {
        public override void GererDrapeau(Cellule cellule)
        {
            // Une cellule déjà révélée ne peut pas recevoir de drapeau
        }

        public override void GererRevelation(Cellule cellule)
        {
            // Une cellule déjà révélée ne peut pas être révélée à nouveau
        }
    }
}
