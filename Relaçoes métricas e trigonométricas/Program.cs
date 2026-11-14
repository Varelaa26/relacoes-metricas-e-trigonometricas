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
            Console.WriteLine("3 - Não sei ao certo");
            Console.WriteLine("4 - Encerrar");

            Console.Write("Digite o número da opção desejada: ");
            string escolha = Console.ReadLine();
            switch (escolha)
            {
                case "1":
                    CalcularPitagoras();
                    break;
                case "2":
                    //RazoesTrigonométricas.Calcular();
                    break;
                case "3":
                    //Ajuda();
                    break;
                case "4":
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

                if (double.TryParse(Console.ReadLine(), out valor) && valor > 0)
                {
                    return valor;
                }

                Console.WriteLine("Valor inválido. Digite um número maior que zero.");
            }
        }

        static void Ajuda()
        {
            // Implementar função de ajuda, pensei em um menu explicando cada opção
            // Ainda não tenho certeza se vou fazer o menu interativo ou apenas um manual simples
        }
    }
}
