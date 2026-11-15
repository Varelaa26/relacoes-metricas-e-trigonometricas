using System;


namespace Relaçoes_métricas_e_trigonométricas
{
    internal class sin_cos_tan
    {
        public static double CalcularSenoDoAngulo(double angulo)
        {
            ValidarAngulo(angulo);
            return Math.Sin(angulo * Math.PI / 180.0);
        }

        public static double CalcularCossenoDoAngulo(double angulo)
        {
            ValidarAngulo(angulo);
            return Math.Cos(angulo * Math.PI / 180.0);
        }

        public static double CalcularTangenteDoAngulo(double angulo)
        {
            ValidarAngulo(angulo);
            return Math.Tan(angulo * Math.PI / 180.0);
        }

        public static double CalcularAngulo(int razao, double valor)
        {
            if (double.IsNaN(valor) || double.IsInfinity(valor) || valor <= 0)
                throw new ArgumentOutOfRangeException("valor", "A razão deve ser maior que zero.");

            double angulo;
            switch (razao)
            {
                case 1:
                    if (valor >= 1) throw new ArgumentOutOfRangeException("valor", "O seno de um ângulo agudo deve estar entre 0 e 1.");
                    angulo = Math.Asin(valor);
                    break;
                case 2:
                    if (valor >= 1) throw new ArgumentOutOfRangeException("valor", "O cosseno de um ângulo agudo deve estar entre 0 e 1.");
                    angulo = Math.Acos(valor);
                    break;
                case 3:
                    angulo = Math.Atan(valor);
                    break;
                default:
                    throw new ArgumentException("Razão trigonométrica inválida.");
            }

            return angulo * 180.0 / Math.PI;
        }

        public static void CalcularLados(double angulo, int ladoConhecido, double valor,
            out double catetoOposto, out double catetoAdjacente, out double hipotenusa)
        {
            ValidarAngulo(angulo);
            if (double.IsNaN(valor) || double.IsInfinity(valor) || valor <= 0)
                throw new ArgumentOutOfRangeException("valor", "O lado conhecido deve ser maior que zero.");

            double seno = CalcularSenoDoAngulo(angulo);
            double cosseno = CalcularCossenoDoAngulo(angulo);
            double tangente = CalcularTangenteDoAngulo(angulo);

            switch (ladoConhecido)
            {
                case 1: // hipotenusa
                    hipotenusa = valor;
                    catetoOposto = hipotenusa * seno;
                    catetoAdjacente = hipotenusa * cosseno;
                    break;
                case 2: // cateto oposto
                    catetoOposto = valor;
                    hipotenusa = catetoOposto / seno;
                    catetoAdjacente = catetoOposto / tangente;
                    break;
                case 3: // cateto adjacente
                    catetoAdjacente = valor;
                    hipotenusa = catetoAdjacente / cosseno;
                    catetoOposto = catetoAdjacente * tangente;
                    break;
                default:
                    throw new ArgumentException("Lado conhecido inválido.");
            }
        }

        private static void ValidarAngulo(double angulo)
        {
            if (double.IsNaN(angulo) || double.IsInfinity(angulo) || angulo <= 0 || angulo >= 90)
                throw new ArgumentOutOfRangeException("angulo", "O ângulo deve estar entre 0° e 90°.");
        }

        public static double CalcularSeno(double catetoOposto, double hipotenusa)
        {
            return catetoOposto / hipotenusa;
        }
        public static double CalcularCosseno(double catetoAdjacente, double hipotenusa)
        {
            return catetoAdjacente / hipotenusa;
        }
        public static double CalcularTangente(double catetoOposto, double catetoAdjacente)
        {
            return catetoOposto / catetoAdjacente;
        }
    }
}
