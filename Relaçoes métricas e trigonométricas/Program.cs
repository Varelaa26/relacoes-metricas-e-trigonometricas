using System;
using Relaçoes_métricas_e_trigonométricas;

namespace Relaçoes_métricas_e_trigonométricas
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("--- Relaçoes métricas e trigonométricas ---");

            Console.WriteLine("Escolha o que quer calcular: ");
            Console.WriteLine("1 - Pitágoras");
            Console.WriteLine("2 - Razões trigonométricas");
            Console.WriteLine("3 - Relações métricas (altura e projeções)");
            Console.WriteLine("4 - Não sei ao certo");
            Console.WriteLine("5 - Encerrar");

            Console.Write("Digite o número da opção desejada: ");
            string escolha = Console.ReadLine();
            switch (escolha)
            {
                case "1":
                    CalcularPitagoras();
                    break;
                case "2":
                    CalcularRazoesTrigonométricas();
                    break;
                case "3":
                    CalcularRelacoesMetricas();
                    break;
                case "4":
                    Ajuda();
                    break;
                case "5":
                    Console.WriteLine("Encerrando o programa...");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Reiniciando o programa...");
                    Main();
                    return;
            }
        }
        static void CalcularPitagoras()
        {
            Console.WriteLine("Escolha o que quer calcular: ");
            Console.WriteLine("1 - Hipotenusa");
            Console.WriteLine("2 - Cateto");
            Console.WriteLine("3 - Voltar");

            Console.Write("Digite o número da opção desejada: ");
            string escolha = Console.ReadLine();
            switch (escolha)
            {
                case "1":
                    double c1 = LerNumeroPositivo("Digite o valor do primeiro cateto: ");
                    double c2 = LerNumeroPositivo("Digite o valor do segundo cateto: ");

                    double hipotenusa = Pitagoras.CalcularHipotenusa(c1, c2);
                    Console.WriteLine("O valor da hipotenusa é: {0}", hipotenusa);
                    break;
                case "2":
                    double resultado;
                    while (true)
                    {
                        double h = LerNumeroPositivo("Digite o valor da hipotenusa: ");
                        double cateto = LerNumeroPositivo("Digite o valor do cateto: ");

                        if (cateto >= h)
                        {
                            Console.WriteLine("Valor inválido: o cateto deve ser menor que a hipotenusa. Tente novamente.");
                            continue;
                        }

                        resultado = Pitagoras.CalcularCateto(h, cateto);
                        break;
                    }

                    Console.WriteLine("O valor do cateto é: {0}", resultado);
                    break;
                case "3":
                    Main();
                    return;
                default:
                    Console.WriteLine("Opção inválida. Reiniciando o programa...");
                    CalcularPitagoras();
                    return;
            }
        }
        static double LerNumeroPositivo(string mensagem)
        {
            double valor;

            while (true)
            {
                Console.Write(mensagem);

                if (double.TryParse(Console.ReadLine(), out valor) &&
                    !double.IsNaN(valor) && !double.IsInfinity(valor) && valor > 0)
                {
                    return valor;
                }

                Console.WriteLine("Valor inválido. Digite um número maior que zero.");
            }
        }

        static void CalcularRelacoesMetricas()
        {
            try
            {
                Console.WriteLine("Relações métricas no triângulo retângulo:");
                Console.WriteLine("1 - Altura pela projeções (h² = m·n)");
                Console.WriteLine("2 - Altura pelos catetos (b·c = a·h)");
                Console.WriteLine("3 - Cateto pela hipotenusa e sua projeção (b² = a·m)");
                Console.WriteLine("4 - Projeção pela hipotenusa e cateto (m = b²/a)");
                Console.WriteLine("5 - Hipotenusa pela soma das projeções (a = m+n)");
                Console.WriteLine("6 - Ver explicação das relações");
                Console.WriteLine("7 - Voltar");
                Console.Write("Digite o número da opção desejada: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        double mAltura = LerNumeroPositivo("Digite a projeção m: ");
                        double nAltura = LerNumeroPositivo("Digite a projeção n: ");
                        Console.WriteLine("Altura h: {0}", Metricas.CalcularAltura(mAltura, nAltura));
                        break;
                    case "2":
                        double b = LerNumeroPositivo("Digite o primeiro cateto: ");
                        double c = LerNumeroPositivo("Digite o segundo cateto: ");
                        Console.WriteLine("Altura h: {0}", Metricas.CalcularAlturaPelosCatetos(b, c));
                        break;
                    case "3":
                        double hipotenusaCateto = LerNumeroPositivo("Digite a hipotenusa a: ");
                        double projecaoCateto = LerNumeroPositivo("Digite a projeção correspondente (m ou n): ");
                        Console.WriteLine("Cateto: {0}", Metricas.CalcularCateto(hipotenusaCateto, projecaoCateto));
                        break;
                    case "4":
                        double catetoProjecao = LerNumeroPositivo("Digite o cateto correspondente: ");
                        double hipotenusaProjecao = LerNumeroPositivo("Digite a hipotenusa a: ");
                        Console.WriteLine("Projeção correspondente: {0}", Metricas.CalcularProjecao(catetoProjecao, hipotenusaProjecao));
                        break;
                    case "5":
                        double m = LerNumeroPositivo("Digite a projeção m: ");
                        double n = LerNumeroPositivo("Digite a projeção n: ");
                        Console.WriteLine("Hipotenusa a: {0}", Metricas.CalcularHipotenusaPelasProjecoes(m, n));
                        break;
                    case "6":
                        Console.WriteLine("A altura h traçada do ângulo reto divide a hipotenusa a em duas projeções, m e n.");
                        Console.WriteLine("As relações são: a = m+n; h² = m·n; b² = a·m; c² = a·n; b·c = a·h.");
                        break;
                    case "7":
                        Main();
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Não foi possível calcular: {0}", ex.Message);
            }
        }

        static void CalcularRazoesTrigonométricas()
        {
            try
            {
                Console.WriteLine("Escolha o que deseja calcular:");
                Console.WriteLine("1 - Seno a partir dos lados");
                Console.WriteLine("2 - Cosseno a partir dos lados");
                Console.WriteLine("3 - Tangente a partir dos lados");
                Console.WriteLine("4 - Seno, cosseno e tangente a partir de um ângulo");
                Console.WriteLine("5 - Ângulo e lados a partir de uma razão e um lado conhecido");
                Console.WriteLine("6 - Voltar");
                Console.Write("Digite o número da opção desejada: ");
                string escolha = Console.ReadLine();
                switch (escolha)
                {
                    case "1":
                        double catetoOpostoSeno = LerNumeroPositivo("Digite o valor do cateto oposto: ");
                        double hipotenusaSeno = LerNumeroPositivo("Digite o valor da hipotenusa: ");
                        double seno = Triogonometria.CalcularSeno(catetoOpostoSeno, hipotenusaSeno);
                        Console.WriteLine("O valor do seno é: {0}", seno);
                        break;
                    case "2":
                        double catetoAdjacenteCosseno = LerNumeroPositivo("Digite o valor do cateto adjacente: ");
                        double hipotenusaCosseno = LerNumeroPositivo("Digite o valor da hipotenusa: ");
                        double cosseno = Triogonometria.CalcularCosseno(catetoAdjacenteCosseno, hipotenusaCosseno);
                        Console.WriteLine("O valor do cosseno é: {0}", cosseno);
                        break;
                    case "3":
                        double catetoOpostoTangente = LerNumeroPositivo("Digite o valor do cateto oposto: ");
                        double catetoAdjacenteTangente = LerNumeroPositivo("Digite o valor do cateto adjacente: ");
                        double tangente = Triogonometria.CalcularTangente(catetoOpostoTangente, catetoAdjacenteTangente);
                        Console.WriteLine("O valor da tangente é: {0}", tangente);
                        break;
                    case "4":
                        double angulo = LerNumeroPositivo("Digite o ângulo em graus (entre 0 e 90): ");
                        Console.WriteLine("Seno: {0}", Triogonometria.CalcularSenoDoAngulo(angulo));
                        Console.WriteLine("Cosseno: {0}", Triogonometria.CalcularCossenoDoAngulo(angulo));
                        Console.WriteLine("Tangente: {0}", Triogonometria.CalcularTangenteDoAngulo(angulo));
                        break;
                    case "5":
                        CalcularAnguloELados();
                        break;
                    case "6":
                        Main();
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Reiniciando o programa...");
                        CalcularRazoesTrigonométricas();
                        return;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Não foi possível calcular a razão trigonométrica: {0}", ex.Message);
            }
        }

        static void CalcularAnguloELados()
        {
            Console.WriteLine("Escolha a razão conhecida:");
            Console.WriteLine("1 - Seno");
            Console.WriteLine("2 - Cosseno");
            Console.WriteLine("3 - Tangente");
            Console.Write("Opção: ");
            int razao;
            if (!int.TryParse(Console.ReadLine(), out razao) || razao < 1 || razao > 3)
            {
                Console.WriteLine("Opção inválida.");
                return;
            }

            double valorRazao = LerNumeroPositivo("Digite o valor da razão: ");
            double angulo = Triogonometria.CalcularAngulo(razao, valorRazao);
            Console.WriteLine("Ângulo: {0}°", angulo);

            Console.WriteLine("Qual lado você conhece?");
            Console.WriteLine("1 - Hipotenusa");
            Console.WriteLine("2 - Cateto oposto ao ângulo");
            Console.WriteLine("3 - Cateto adjacente ao ângulo");
            Console.Write("Opção: ");
            int lado;
            if (!int.TryParse(Console.ReadLine(), out lado) || lado < 1 || lado > 3)
            {
                Console.WriteLine("Opção inválida.");
                return;
            }

            double valorLado = LerNumeroPositivo("Digite o valor do lado conhecido: ");
            double oposto, adjacente, hipotenusa;
            Triogonometria.CalcularLados(angulo, lado, valorLado, out oposto, out adjacente, out hipotenusa);
            Console.WriteLine("Cateto oposto: {0}", oposto);
            Console.WriteLine("Cateto adjacente: {0}", adjacente);
            Console.WriteLine("Hipotenusa: {0}", hipotenusa);
        }

        static void Ajuda()
        {
            Console.WriteLine("Escolha Pitágoras para lados do triângulo, razões trigonométricas para ângulos e relações métricas para altura e projeções.");
            Console.WriteLine("Nas relações métricas, a é a hipotenusa, b e c são os catetos, h é a altura, e m e n são as projeções dos catetos sobre a hipotenusa.");
        }
    }
}
