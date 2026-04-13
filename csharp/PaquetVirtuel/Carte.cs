using System;
using System.Collections.Generic;

namespace PaquetVirtuel
{
    public class Carte
    {
        public LinkedListNode<Carte> Noeud { get; private set; }

        public string Nom { get; private set; }
        public Enseigne Symbole { get; private set; }
        public ImageCarte Visuel { get; private set; }
        public int Valeur { get; private set; }

        public bool EstRetournee { get; set; }

        public Carte(string nom, Enseigne symbole, ImageCarte visuel, int valeur)
        {
            Nom = nom;
            Symbole = symbole;
            Visuel = visuel;
            Valeur = valeur;
            EstRetournee = false;
        }

        public void DefinirNoeud(LinkedListNode<Carte> noeud)
        {
            Noeud = noeud;
        }

        public override string ToString()
        {
            return $"{Nom} de {Symbole} (Valeur: {Valeur})";
        }
    }
}
