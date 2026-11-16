using System;

namespace Relaçoes_métricas_e_trigonométricas
{
    class Pitagoras
    {
        public static double CalcularHipotenusa(double cateto1, double cateto2)
        {
            ValidarLado(cateto1, "O primeiro cateto");
            ValidarLado(cateto2, "O segundo cateto");
            return Math.Sqrt(cateto1 * cateto1 + cateto2 * cateto2);
        }


        public static double CalcularCateto(double hipotenusa, double cateto)
        {
            ValidarLado(hipotenusa, "A hipotenusa");
            ValidarLado(cateto, "O cateto");
            if (cateto >= hipotenusa)
                throw new ArgumentException("O cateto deve ser menor que a hipotenusa.");
            return Math.Sqrt(hipotenusa * hipotenusa - cateto * cateto);
        }

        private static void ValidarLado(double valor, string nome)
        {
            if (double.IsNaN(valor) || double.IsInfinity(valor) || valor <= 0)
                throw new ArgumentOutOfRangeException("valor", nome + " deve ser finito e maior que zero.");
        }

        // Adicionarei também a descrição e explicação do teorema, pra ser chamado lá na função de ajuda
    }
}
