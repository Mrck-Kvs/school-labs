using System;

namespace JeuPFC
{
    internal class Papier : Option
    {
        public override int Affronter(Option adversaire)
        {
            if (adversaire is Papier)
                return 0;
            if (adversaire is Roche)
                return 1;
            return -1;
        }

        public override Option Reveler()
        {
            return this;
        }
    }
}
