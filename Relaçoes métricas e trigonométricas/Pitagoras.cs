using System;

namespace Relaçoes_métricas_e_trigonométricas
{
    class Pitagoras
    {
        public static double CalcularHipotenusa(double cateto1, double cateto2)
        {
            return Math.Sqrt(Math.Pow(cateto1, 2) + Math.Pow(cateto2, 2));
        }


        public static double CalcularCateto(double hipotenusa, double cateto)
        {
            return Math.Sqrt(Math.Pow(hipotenusa, 2) - Math.Pow(cateto, 2));
        }

        // Adicionarei também a descrição e explicação do teorema, pra ser chamado lá na função de ajuda
    }
}
