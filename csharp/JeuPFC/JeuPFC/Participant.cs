using System;

namespace JeuPFC
{
    internal class Participant
    {
        private string pseudo;
        private int points;
        private Option selectionActuelle;

        public string Pseudo
        {
            get => pseudo;
            set => pseudo = value;
        }

        public int Points
        {
            get => points;
            set => points = value;
        }

        public Option Selection
        {
            get => selectionActuelle;
            set => selectionActuelle = value;
        }

        public Participant()
        {
            points = 0;
            selectionActuelle = new NonDefini();
        }

        public Participant(string nom) : this()
        {
            pseudo = nom;
        }

        public Option ChoisirAleatoirement()
        {
            int valeur = GestionnaireMatch.Aleatoire.Next(0, 3);

            switch (valeur)
            {
                case 0:
                    selectionActuelle = new Roche();
                    break;
                case 1:
                    selectionActuelle = new Papier();
                    break;
                case 2:
                    selectionActuelle = new Lame();
                    break;
                default:
                    selectionActuelle = new NonDefini();
                    break;
            }

            return selectionActuelle;
        }

        public void AjouterPoint()
        {
            points++;
        }
    }
}
