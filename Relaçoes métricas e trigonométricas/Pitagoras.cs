using System;
namespace Relaçoes_métricas_e_trigonométricas
{
    class Pitagoras
    {
        public static double CalcularHipotenusa(double cateto1, double cateto2)
        {
            ValidarLado(cateto1, "O primeiro cateto");
            ValidarLado(cateto2, "O segundo cateto");
            double escala = Math.Max(cateto1, cateto2);
            return ValidarResultado(escala * Math.Sqrt(Math.Pow(cateto1 / escala, 2) + Math.Pow(cateto2 / escala, 2)));
        }


        public static double CalcularCateto(double hipotenusa, double cateto)
        {
            ValidarLado(hipotenusa, "A hipotenusa");
            ValidarLado(cateto, "O cateto");
            if (cateto >= hipotenusa)
                throw new ArgumentException("O cateto deve ser menor que a hipotenusa.");
            double razao = cateto / hipotenusa;
            return ValidarResultado(hipotenusa * Math.Sqrt((1.0 - razao) * (1.0 + razao)));
        }

        private static void ValidarLado(double valor, string nome)
        {
            if (double.IsNaN(valor) || double.IsInfinity(valor) || valor <= 0)
                throw new ArgumentOutOfRangeException("valor", nome + " deve ser finito e maior que zero.");
        }

        private static double ValidarResultado(double resultado)
        {
            if (double.IsNaN(resultado) || double.IsInfinity(resultado) || resultado <= 0)
                throw new ArgumentOutOfRangeException("resultado", "O resultado excede o intervalo numérico suportado.");
            return resultado;
        }
    }
}
