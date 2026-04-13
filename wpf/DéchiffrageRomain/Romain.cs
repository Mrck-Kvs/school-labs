using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DéchiffrageRomain
{
    internal class Romain
    {
        private const int MAX_ROMAN_NUMERALS = 3999;
        private const int MIN_ROMAN_NUMERALS = -3999;
        private List<(int, string)> _romanNumerals;
        private Dictionary<char, int> _hash;
        private int _romanNumeral;
        private string _romanSymbols;

        public int RomanNumeral { get => _romanNumeral; set => _romanNumeral = value; }
        public string RomanSymbols
        {
            get => _romanSymbols; 
            set {
                _romanSymbols = value;
            }
        }

        public int RomanNumeral1 { get => _romanNumeral;
            set {
                _romanNumeral = value;
            }
        }

        public Romain()
        {
            // list qui contien tout les chiffres romains avec les soutractions
            _romanNumerals = new List<(int, string)> { (1000, "M"), (900, "CM"), (500, "D"), (400, "CD"), (100, "C"), (90, "XC"), (50, "L"), (40, "XL"), (10, "X"), (9, "IX"), (5, "V"), (4, "IV"), (1, "I") };
            // filtrage de la liste pour faire un dictionnaire qui contien que les chiffre romain. Filtrage grâce length == 1
            _hash = _romanNumerals.Where(rN => rN.Item2.Length == 1).Select(rN => (rN.Item2[0], rN.Item1)).ToDictionary(rN => rN.Item1, rN => rN.Item2);
        }

        public int RomanToInt(string romanSymbols)
        {
            int romanNumeral = 0;
            for (int i = 0; i < romanSymbols.Length; i++)
            {
                int currentRomanNumeral = _hash[romanSymbols[i]];
                int nextRomanNumeral = (i + 1 < romanSymbols.Length) ? _hash[romanSymbols[i + 1]] : 0;
                if (currentRomanNumeral < nextRomanNumeral)
                {
                    romanNumeral += nextRomanNumeral - currentRomanNumeral;
                    i++;
                }
                else
                {
                    romanNumeral += currentRomanNumeral;
                }
            }
            return romanNumeral;
        }

        public string IntToRoman(int romanNumeral)
        {
            string romanSymbols = "";
            for (int i = 0; i < _romanNumerals.Count; i++)
            {
                (int, string) currentRomanNumeral = _romanNumerals[i];
                if (currentRomanNumeral.Item1 <= romanNumeral)
                {
                    romanNumeral -= currentRomanNumeral.Item1;
                    romanSymbols += currentRomanNumeral.Item2;
                    if (currentRomanNumeral.Item1 <= romanNumeral)
                    {
                        i--;
                    }
                }
            }
            return romanSymbols;
        }
    }
}
