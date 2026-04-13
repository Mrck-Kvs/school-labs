using System;
using System.Drawing;

namespace JeuPFC
{
    internal abstract class Option
    {
        public Color CouleurActive { get; set; }
        public Color CouleurInactive { get; set; }

        protected Option() { }

        public abstract int Affronter(Option adversaire);

        public abstract Option Reveler();
    }
}
