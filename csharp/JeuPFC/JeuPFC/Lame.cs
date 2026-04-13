using System;

namespace JeuPFC
{
    internal class Lame : Option
    {
        public override int Affronter(Option adversaire)
        {
            if (adversaire is Lame)
                return 0;
            if (adversaire is Papier)
                return 1;
            return -1;
        }

        public override Option Reveler()
        {
            return this;
        }
    }
}
