using System;

namespace JeuDeminage
{
    public abstract class StatutCellule
    {
        public abstract void GererDrapeau(Cellule cellule);
        public abstract void GererRevelation(Cellule cellule);
    }
}
