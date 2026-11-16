using System;


namespace Relaçoes_métricas_e_trigonométricas
{
    internal class Metricas
    {
        public static double CalcularArea(double baseTriangulo, double altura)
        {
            if (baseTriangulo <= 0 || altura <= 0)
                throw new ArgumentOutOfRangeException("baseTriangulo", "A base e a altura devem ser maiores que zero.");
            return (baseTriangulo * altura) / 2.0;
        }
        public static double CalcularPerimetroDoTriangulo(double lado1, double lado2, double lado3)
        {
            if (lado1 <= 0 || lado2 <= 0 || lado3 <= 0)
                throw new ArgumentOutOfRangeException("lado", "Todos os lados devem ser maiores que zero.");
            return lado1 + lado2 + lado3;
        }
        public static double CalcularPerimetro(double cateto1, double cateto2)
        {
            if (cateto1 <= 0 || cateto2 <= 0)
                throw new ArgumentOutOfRangeException("cateto", "Os catetos devem ser maiores que zero.");
            return cateto1 + cateto2 + Math.Sqrt(cateto1 * cateto1 + cateto2 * cateto2);
        }
        public static double CalcularAltura(double cateto1, double cateto2)
        {
            ValidarPositivo(cateto1, "O primeiro cateto");
            ValidarPositivo(cateto2, "O segundo cateto");
            double hipotenusa = Math.Sqrt(cateto1 * cateto1 + cateto2 * cateto2);
            return cateto1 * cateto2 / hipotenusa; // b·c = a·h
        }

        public static double CalcularAlturaPelosCatetos(double cateto1, double cateto2)
        {
            return CalcularAltura(cateto1, cateto2);
        }

        // a é a hipotenusa; m e n são suas projeções; h é a altura relativa a ela.
        public static double CalcularAlturaPelasProjecoes(double m, double n)
        {
            ValidarPositivo(m, "A projeção m");
            ValidarPositivo(n, "A projeção n");
            return Math.Sqrt(m * n); // h² = m·n
        }

        public static double CalcularCateto(double hipotenusa, double projecao)
        {
            ValidarPositivo(hipotenusa, "A hipotenusa");
            ValidarPositivo(projecao, "A projeção");
            if (projecao >= hipotenusa)
                throw new ArgumentException("A projeção deve ser menor que a hipotenusa.");
            return Math.Sqrt(hipotenusa * projecao); // b² = a·m (ou c² = a·n)
        }

        public static double CalcularProjecao(double cateto, double hipotenusa)
        {
            ValidarPositivo(cateto, "O cateto");
            ValidarPositivo(hipotenusa, "A hipotenusa");
            if (cateto >= hipotenusa)
                throw new ArgumentException("O cateto deve ser menor que a hipotenusa.");
            return cateto * cateto / hipotenusa; // m = b²/a (ou n = c²/a)
        }

        public static double CalcularHipotenusaPelasProjecoes(double m, double n)
        {
            ValidarPositivo(m, "A projeção m");
            ValidarPositivo(n, "A projeção n");
            return m + n; // a = m+n
        }

        private static void ValidarPositivo(double valor, string nome)
        {
            if (double.IsNaN(valor) || double.IsInfinity(valor) || valor <= 0)
                throw new ArgumentOutOfRangeException("valor", nome + " deve ser maior que zero.");
        }
    }
}
