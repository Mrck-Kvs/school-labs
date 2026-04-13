using System;
using System.Timers;

namespace JeuDeminage
{
    internal class CompteurTemps
    {
        private Timer horloge;
        private int secondesEcoulees;

        public int Secondes => secondesEcoulees;

        public CompteurTemps()
        {
            secondesEcoulees = 0;
            horloge = new Timer();
            horloge.Interval = 1000;
            horloge.Elapsed += SurTick;
        }

        private void SurTick(object emetteur, ElapsedEventArgs args)
        {
            secondesEcoulees++;
        }

        public void Demarrer()
        {
            horloge.Start();
        }

        public void Arreter()
        {
            horloge.Stop();
        }
    }
}
