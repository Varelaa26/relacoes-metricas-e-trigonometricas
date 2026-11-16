using System;


namespace Relaçoes_métricas_e_trigonométricas
{
    internal class Metricas
    {
        public static double CalcularArea(double baseTriangulo, double altura)
        {
            ValidarPositivo(baseTriangulo, "A base");
            ValidarPositivo(altura, "A altura");
            return ValidarResultado(baseTriangulo * (altura / 2.0), "A área calculada excede o intervalo numérico suportado.");
        }
        public static double CalcularPerimetroDoTriangulo(double lado1, double lado2, double lado3)
        {
            ValidarPositivo(lado1, "O lado 1"); ValidarPositivo(lado2, "O lado 2"); ValidarPositivo(lado3, "O lado 3");
            if (lado1 + lado2 <= lado3 || lado1 + lado3 <= lado2 || lado2 + lado3 <= lado1)
                throw new ArgumentException("As medidas não formam um triângulo: a soma de quaisquer dois lados deve superar o terceiro.");
            return ValidarResultado(lado1 + lado2 + lado3, "O perímetro excede o intervalo numérico suportado.");
        }
        public static double CalcularPerimetro(double cateto1, double cateto2)
        {
            return ValidarResultado(cateto1 + cateto2 + Pitagoras.CalcularHipotenusa(cateto1, cateto2), "O perímetro excede o intervalo numérico suportado.");
        }
        public static double CalcularAltura(double cateto1, double cateto2)
        {
            ValidarPositivo(cateto1, "O primeiro cateto");
            ValidarPositivo(cateto2, "O segundo cateto");
            double hipotenusa = Pitagoras.CalcularHipotenusa(cateto1, cateto2);
            return ValidarResultado(cateto1 * (cateto2 / hipotenusa), "A altura calculada excede o intervalo numérico suportado."); // b·c = a·h
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
            return ValidarResultado(Math.Sqrt(m) * Math.Sqrt(n), "A altura calculada excede o intervalo numérico suportado."); // h² = m·n
        }

        public static double CalcularCateto(double hipotenusa, double projecao)
        {
            ValidarPositivo(hipotenusa, "A hipotenusa");
            ValidarPositivo(projecao, "A projeção");
            if (projecao >= hipotenusa)
                throw new ArgumentException("A projeção deve ser menor que a hipotenusa.");
            return ValidarResultado(Math.Sqrt(hipotenusa) * Math.Sqrt(projecao), "O cateto calculado excede o intervalo numérico suportado."); // b² = a·m (ou c² = a·n)
        }

        public static double CalcularProjecao(double cateto, double hipotenusa)
        {
            ValidarPositivo(cateto, "O cateto");
            ValidarPositivo(hipotenusa, "A hipotenusa");
            if (cateto >= hipotenusa)
                throw new ArgumentException("O cateto deve ser menor que a hipotenusa.");
            return ValidarResultado(cateto * (cateto / hipotenusa), "A projeção calculada excede o intervalo numérico suportado."); // m = b²/a (ou n = c²/a)
        }

        public static double CalcularHipotenusaPelasProjecoes(double m, double n)
        {
            ValidarPositivo(m, "A projeção m");
            ValidarPositivo(n, "A projeção n");
            return ValidarResultado(m + n, "A hipotenusa calculada excede o intervalo numérico suportado."); // a = m+n
        }

        // Calcula o quarto valor da relação a·h = b·c; os argumentos são os outros três valores.
        public static double CalcularPelaRelacaoAltura(double primeiro, double segundo, double terceiro)
        {
            ValidarPositivo(primeiro, "A primeira medida");
            ValidarPositivo(segundo, "A segunda medida");
            ValidarPositivo(terceiro, "A terceira medida");
            return ValidarResultado(primeiro * (segundo / terceiro), "O resultado excede o intervalo numérico suportado.");
        }

        private static void ValidarPositivo(double valor, string nome)
        {
            if (double.IsNaN(valor) || double.IsInfinity(valor) || valor <= 0)
                throw new ArgumentOutOfRangeException("valor", nome + " deve ser maior que zero.");
        }

        private static double ValidarResultado(double resultado, string mensagem)
        {
            if (double.IsNaN(resultado) || double.IsInfinity(resultado) || resultado <= 0)
                throw new ArgumentOutOfRangeException("resultado", mensagem);
            return resultado;
        }
    }
}
