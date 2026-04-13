using System;

namespace JeuPFC
{
    internal class GestionnaireMatch
    {
        private readonly Participant participantUn;
        private readonly Participant participantDeux;

        private static GestionnaireMatch instanceUnique;
        private static readonly object verrou = new object();

        public static readonly Random Aleatoire = new Random();

        public Participant ParticipantUn => participantUn;
        public Participant ParticipantDeux => participantDeux;

        private GestionnaireMatch()
        {
            participantUn = new Participant("Joueur 1");
            participantDeux = new Participant("Ordinateur");
        }

        public static GestionnaireMatch Instance
        {
            get
            {
                if (instanceUnique == null)
                {
                    lock (verrou)
                    {
                        if (instanceUnique == null)
                            instanceUnique = new GestionnaireMatch();
                    }
                }
                return instanceUnique;
            }
        }

        public static void Reinitialiser()
        {
            lock (verrou)
            {
                instanceUnique = null;
            }
        }

        public void JouerManche()
        {
            participantDeux.ChoisirAleatoirement();

            int resultat = participantUn.Selection.Affronter(participantDeux.Selection);

            if (resultat > 0)
                participantUn.AjouterPoint();
            else if (resultat < 0)
                participantDeux.AjouterPoint();
        }

        public string ObtenirResultat()
        {
            if (participantUn.Points == participantDeux.Points)
                return "Match nul !";

            Participant vainqueur = participantUn.Points > participantDeux.Points
                ? participantUn
                : participantDeux;

            return FormulerVictoire(vainqueur);
        }

        private string FormulerVictoire(Participant gagnant)
        {
            return $"{gagnant.Pseudo} remporte la partie avec {gagnant.Points} point(s) !";
        }
    }

    enum TypeOption
    {
        Roche,
        Papier,
        Lame
    }
}
