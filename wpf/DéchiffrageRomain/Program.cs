using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DéchiffrageRomain
{
    using System;

    public class DigitCountExplanation
    {
        public static void ExplainFormula()
        {
            Console.WriteLine("=== EXPLICATION DE LA FORMULE ===");
            Console.WriteLine("(int)Math.Floor(Math.Log10(Math.Abs(number))) + 1\n");

            // Testons avec différents nombres
            int[] testNumbers = { 1, 5, 10, 99, 100, 999, 1000, 1234, -567 };

            foreach (int number in testNumbers)
            {
                Console.WriteLine($"\n--- Nombre: {number} ---");

                // Étape 1: Math.Abs (valeur absolue)
                int absValue = Math.Abs(number);
                Console.WriteLine($"1. Math.Abs({number}) = {absValue}");

                // Étape 2: Math.Log10 (logarithme base 10)
                double log10Result = Math.Log10(absValue);
                Console.WriteLine($"2. Math.Log10({absValue}) = {log10Result:F3}");

                // Étape 3: Math.Floor (arrondi vers le bas)
                double floorResult = Math.Floor(log10Result);
                Console.WriteLine($"3. Math.Floor({log10Result:F3}) = {floorResult}");

                // Étape 4: Conversion en int et +1
                int digitCount = (int)floorResult + 1;
                Console.WriteLine($"4. (int){floorResult} + 1 = {digitCount}");

                // Vérification avec ToString().Length
                int actualLength = absValue.ToString().Length;
                Console.WriteLine($"   Vérification: {absValue}.ToString().Length = {actualLength}");
                Console.WriteLine($"   ✓ Résultat correct: {digitCount == actualLength}");
            }
        }

        public static void ExplainMathConcepts()
        {
            Console.WriteLine("\n=== CONCEPTS MATHÉMATIQUES ===\n");

            Console.WriteLine("1. LOGARITHME BASE 10 :");
            Console.WriteLine("   Log10(x) = le pouvoir auquel il faut élever 10 pour obtenir x");
            Console.WriteLine("   Log10(100) = 2  car 10² = 100");
            Console.WriteLine("   Log10(1000) = 3 car 10³ = 1000");
            Console.WriteLine("   Log10(1) = 0    car 10⁰ = 1");

            Console.WriteLine("\n2. RELATION AVEC LE NOMBRE DE CHIFFRES :");
            Console.WriteLine("   • 1-9     : Log10 donne 0.0 à 0.95  → Floor = 0 → +1 = 1 chiffre");
            Console.WriteLine("   • 10-99   : Log10 donne 1.0 à 1.99  → Floor = 1 → +1 = 2 chiffres");
            Console.WriteLine("   • 100-999 : Log10 donne 2.0 à 2.99  → Floor = 2 → +1 = 3 chiffres");
            Console.WriteLine("   • Et ainsi de suite...");

            Console.WriteLine("\n3. POURQUOI ÇA MARCHE :");
            Console.WriteLine("   Le logarithme base 10 d'un nombre nous dit dans quelle 'tranche de puissance'");
            Console.WriteLine("   se trouve le nombre. Le Floor nous donne la puissance entière, et +1 nous");
            Console.WriteLine("   donne le nombre de chiffres.");
        }

        public static void ShowEdgeCases()
        {
            Console.WriteLine("\n=== CAS PARTICULIERS ===\n");

            // Cas du zéro
            Console.WriteLine("CAS SPÉCIAL - ZÉRO :");
            Console.WriteLine("Math.Log10(0) → -∞ (indéfini)");
            Console.WriteLine("C'est pourquoi on doit traiter le cas 0 séparément !");

            Console.WriteLine("\nCAS DES NOMBRES NÉGATIFS :");
            Console.WriteLine("Math.Abs(-123) = 123, puis on applique la formule");
            Console.WriteLine("Le signe ne compte pas pour le nombre de chiffres");

            // Démonstration avec cas limites
            Console.WriteLine("\nTEST DES CAS LIMITES :");
            var edgeCases = new[] { 0, 1, 9, 10, 99, 100, 999, 1000 };

            foreach (int num in edgeCases)
            {
                if (num == 0)
                {
                    Console.WriteLine($"{num} → Cas spécial, retourne 1");
                }
                else
                {
                    double log = Math.Log10(num);
                    int result = (int)Math.Floor(log) + 1;
                    Console.WriteLine($"{num} → Log10={log:F3} → Floor={Math.Floor(log)} → Résultat={result}");
                }
            }
        }

        // Version complète avec gestion du zéro
        public static int GetDigitCount(int number)
        {
            if (number == 0) return 1;  // Cas spécial
            return (int)Math.Floor(Math.Log10(Math.Abs(number))) + 1;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            DigitCountExplanation.ExplainFormula();
            DigitCountExplanation.ExplainMathConcepts();
            DigitCountExplanation.ShowEdgeCases();
           /* Romain romain = new Romain();
            Console.WriteLine(romain.IntToRoman(3999));
            Console.WriteLine(romain.RomanToInt("MMMCMXCIX"));*/
        }
    }
}
