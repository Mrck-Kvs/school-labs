using System;

namespace JeuPFC
{
    internal class NonDefini : Option
    {
        public override int Affronter(Option adversaire)
        {
            return 0;
        }

        public override Option Reveler()
        {
            return this;
        }
    }
}
