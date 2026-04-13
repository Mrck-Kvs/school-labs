using System;

namespace JeuPFC
{
    internal class Roche : Option
    {
        public override int Affronter(Option adversaire)
        {
            if (adversaire is Roche)
                return 0;
            if (adversaire is Lame)
                return 1;
            return -1;
        }

        public override Option Reveler()
        {
            return this;
        }
    }
}
