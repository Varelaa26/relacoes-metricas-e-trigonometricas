using System;
using System.Globalization;

namespace Relaçoes_métricas_e_trigonométricas
{
    internal class Program
    {
        private static string _secaoAjudaAtual;
        private static string _tituloMenuAtual;
        private static string[] _opcoesMenuAtual;

        private static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n--- Relações métricas e trigonométricas ---\n");
                Console.WriteLine("1 - Triângulo retângulo (calcular lado ou resolver completo)");
                Console.WriteLine("2 - Razões trigonométricas");
                Console.WriteLine("3 - Relações métricas (altura e projeções)");
                Console.WriteLine("4 - Área e perímetro");
                Console.WriteLine("5 - Ajuda e exemplos");
                Console.WriteLine("6 - Encerrar");
                Console.Write("O que você deseja fazer? Digite uma opção: ");

                try
                {
                    switch (Console.ReadLine())
                    {
                        case "1": CalcularTriangulo(); break;
                        case "2": CalcularRazoesTrigonométricas(); break;
                        case "3": CalcularRelacoesMetricas(); break;
                        case "4": CalcularAreaEPerimetro(); break;
                        case "5": Ajuda(); break;
                        case "6": Console.WriteLine("Programa encerrado."); return;
                        default: Console.WriteLine("Opção inválida. Escolha uma opção de 1 a 6."); break;
                    }
                }
                catch (MenuPrincipalException)
                {
                    // O usuário pediu para voltar diretamente ao menu principal.
                }
            }
        }

        private static void CalcularTriangulo()
        {
            while (true)
            {
                DefinirMenu("1", "[Triângulo retângulo] Escolha o resultado desejado:",
                    "1 - Encontrar um lado que falta",
                    "2 - Resolver o triângulo completo (lados e ângulos)");

                string pedido = LerOpcaoMenu("Qual cálculo deseja fazer? ");
                if (pedido == "x") return;
                if (pedido == "0") throw new MenuPrincipalException();
                if (pedido != "1" && pedido != "2")
                {
                    Console.WriteLine("Opção inválida. Escolha 1 ou 2.");
                    continue;
                }

                try
                {
                    LerDadosTriangulo(out double oposto, out double adjacente,
                        out double hipotenusa, out string ladoFaltante, out double valorFaltante);

                    if (pedido == "1")
                    {
                        Console.WriteLine("\n[Resultado solicitado] Lado calculado: {0} = {1}", ladoFaltante, valorFaltante);
                    }
                    else
                    {
                        double angulo = Math.Atan(oposto / adjacente) * 180.0 / Math.PI;
                        Console.WriteLine("\n[Resultado solicitado] Triângulo completo:");
                        ImprimirTriangulo(oposto, adjacente, hipotenusa, angulo);
                    }
                    return;
                }
                catch (VoltarMenuException)
                {
                    // x durante o cálculo volta à lista de operações do triângulo.
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Não foi possível atender ao cálculo solicitado: {0}", ex.Message);
                    return;
                }
            }
        }

        private static void LerDadosTriangulo(out double oposto, out double adjacente,
            out double hipotenusa, out string ladoFaltante, out double valorFaltante)
        {
            DefinirMenu("1", "[Triângulo retângulo] Quais medidas você conhece?",
                "1 - Conheço os dois catetos",
                "2 - Conheço a hipotenusa e um cateto");
            int tipo = LerOpcao(1, 2, "Qual conjunto de medidas você tem? ");

            if (tipo == 1)
            {
                oposto = LerNumeroPositivo("Cateto oposto ao ângulo de referência: ");
                adjacente = LerNumeroPositivo("Cateto adjacente ao ângulo de referência: ");
                hipotenusa = Pitagoras.CalcularHipotenusa(oposto, adjacente);
                ladoFaltante = "Hipotenusa";
                valorFaltante = hipotenusa;
                return;
            }

            hipotenusa = LerNumeroPositivo("Hipotenusa (lado oposto ao ângulo reto): ");
            double cateto = LerNumeroPositivoAbaixoDe("Valor do cateto conhecido: ", hipotenusa);
            DefinirMenu("1", "Esse cateto é:",
                "1 - Oposto ao ângulo de referência",
                "2 - Adjacente ao ângulo de referência");
            int posicao = LerOpcao(1, 2, "Escolha o tipo de cateto: ");

            if (posicao == 1)
            {
                oposto = cateto;
                adjacente = Pitagoras.CalcularCateto(hipotenusa, cateto);
                ladoFaltante = "Cateto adjacente";
                valorFaltante = adjacente;
            }
            else
            {
                adjacente = cateto;
                oposto = Pitagoras.CalcularCateto(hipotenusa, cateto);
                ladoFaltante = "Cateto oposto";
                valorFaltante = oposto;
            }
        }

        private static void CalcularRazoesTrigonométricas()
        {
            while (true)
            {
                DefinirMenu("2", "[Razões trigonométricas] Escolha o cálculo:",
                    "1 - Seno pelos lados",
                    "2 - Cosseno pelos lados",
                    "3 - Tangente pelos lados",
                    "4 - Seno, cosseno e tangente por ângulo",
                    "5 - Ângulo e demais lados");

                string escolha = LerOpcaoMenu("Qual cálculo deseja fazer? ");
                if (escolha == "x") return;
                if (escolha == "0") throw new MenuPrincipalException();

                try
                {
                    switch (escolha)
                    {
                        case "1":
                            Console.WriteLine("Seno: {0}", Triogonometria.CalcularSeno(
                                LerNumeroPositivo("Cateto oposto: "), LerNumeroPositivo("Hipotenusa: ")));
                            return;
                        case "2":
                            Console.WriteLine("Cosseno: {0}", Triogonometria.CalcularCosseno(
                                LerNumeroPositivo("Cateto adjacente: "), LerNumeroPositivo("Hipotenusa: ")));
                            return;
                        case "3":
                            Console.WriteLine("Tangente: {0}", Triogonometria.CalcularTangente(
                                LerNumeroPositivo("Cateto oposto: "), LerNumeroPositivo("Cateto adjacente: ")));
                            return;
                        case "4":
                            double angulo = LerAngulo();
                            Console.WriteLine("Seno: {0}; cosseno: {1}; tangente: {2}",
                                Triogonometria.CalcularSenoDoAngulo(angulo),
                                Triogonometria.CalcularCossenoDoAngulo(angulo),
                                Triogonometria.CalcularTangenteDoAngulo(angulo));
                            return;
                        case "5":
                            CalcularAnguloELados();
                            return;
                        default:
                            Console.WriteLine("Opção inválida. Escolha uma opção de 1 a 5.");
                            break;
                    }
                }
                catch (VoltarMenuException)
                {
                    // x durante um cálculo volta à lista de razões trigonométricas.
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Não foi possível calcular: {0}", ex.Message);
                    return;
                }
            }
        }

        private static void CalcularAnguloELados()
        {
            DefinirMenu("2", "[Etapa 1 de 3] Qual razão trigonométrica você conhece?",
                "1 - Seno",
                "2 - Cosseno",
                "3 - Tangente");
            int razao = LerOpcao(1, 3, "Escolha a razão: ");
            double valor = LerNumeroPositivo("[Etapa 2 de 3] Digite o valor dessa razão: ");
            double angulo = Triogonometria.CalcularAngulo(razao, valor);
            Console.WriteLine("Ângulo: {0}°", angulo);

            DefinirMenu("2", "[Etapa 3 de 3] Qual lado você conhece?",
                "1 - Hipotenusa",
                "2 - Cateto oposto",
                "3 - Cateto adjacente");
            int lado = LerOpcao(1, 3, "Escolha o lado conhecido: ");
            double conhecido = LerNumeroPositivo("Digite o valor do lado conhecido: ");
            Triogonometria.CalcularLados(angulo, lado, conhecido,
                out double oposto, out double adjacente, out double hipotenusa);
            ImprimirTriangulo(oposto, adjacente, hipotenusa, angulo);
        }

        private static void CalcularRelacoesMetricas()
        {
            while (true)
            {
                Console.WriteLine("Convenção: b²=a·m; c²=a·n; h²=m·n; a=m+n; a·h=b·c.");
                DefinirMenu("3", "[Relações métricas] Escolha qual medida deseja encontrar:",
                    "1 - Altura h por projeções m e n",
                    "2 - Altura h por catetos b e c",
                    "3 - Cateto b por hipotenusa a e projeção m",
                    "4 - Cateto c por hipotenusa a e projeção n",
                    "5 - Projeção por hipotenusa e cateto",
                    "6 - Hipotenusa a por projeções m e n",
                    "7 - Cateto c pela relação a·h = b·c",
                    "8 - Cateto b pela relação a·h = b·c",
                    "9 - Altura h pela relação a·h = b·c",
                    "10 - Hipotenusa a pela relação a·h = b·c");

                string escolha = LerOpcaoMenu("Qual cálculo deseja fazer? ");
                if (escolha == "x") return;
                if (escolha == "0") throw new MenuPrincipalException();

                try
                {
                    switch (escolha)
                    {
                        case "1":
                            Console.WriteLine("Altura h: {0}", Metricas.CalcularAlturaPelasProjecoes(
                                LerNumeroPositivo("Projeção m: "), LerNumeroPositivo("Projeção n: ")));
                            return;
                        case "2":
                            Console.WriteLine("Altura h: {0}", Metricas.CalcularAlturaPelosCatetos(
                                LerNumeroPositivo("Cateto b: "), LerNumeroPositivo("Cateto c: ")));
                            return;
                        case "3":
                            double a = LerNumeroPositivo("Hipotenusa a: ");
                            Console.WriteLine("Cateto b: {0}", Metricas.CalcularCateto(a,
                                LerNumeroPositivoAbaixoDe("Projeção m: ", a)));
                            return;
                        case "4":
                            a = LerNumeroPositivo("Hipotenusa a: ");
                            Console.WriteLine("Cateto c: {0}", Metricas.CalcularCateto(a,
                                LerNumeroPositivoAbaixoDe("Projeção n: ", a)));
                            return;
                        case "5":
                            a = LerNumeroPositivo("Hipotenusa a: ");
                            DefinirMenu("3", "Qual projeção deseja calcular?",
                                "1 - Projeção m usando o cateto b",
                                "2 - Projeção n usando o cateto c");
                            int qual = LerOpcao(1, 2, "Escolha uma opção: ");
                            double cateto = LerNumeroPositivoAbaixoDe(
                                qual == 1 ? "Cateto b: " : "Cateto c: ", a);
                            Console.WriteLine("Projeção: {0}", Metricas.CalcularProjecao(cateto, a));
                            return;
                        case "6":
                            Console.WriteLine("Hipotenusa a: {0}", Metricas.CalcularHipotenusaPelasProjecoes(
                                LerNumeroPositivo("Projeção m: "), LerNumeroPositivo("Projeção n: ")));
                            return;
                        case "7":
                            Console.WriteLine("Cateto c: {0}", Metricas.CalcularPelaRelacaoAltura(
                                LerNumeroPositivo("Hipotenusa a: "), LerNumeroPositivo("Altura h: "),
                                LerNumeroPositivo("Cateto b: ")));
                            return;
                        case "8":
                            Console.WriteLine("Cateto b: {0}", Metricas.CalcularPelaRelacaoAltura(
                                LerNumeroPositivo("Hipotenusa a: "), LerNumeroPositivo("Altura h: "),
                                LerNumeroPositivo("Cateto c: ")));
                            return;
                        case "9":
                            Console.WriteLine("Altura h: {0}", Metricas.CalcularPelaRelacaoAltura(
                                LerNumeroPositivo("Cateto b: "), LerNumeroPositivo("Cateto c: "),
                                LerNumeroPositivo("Hipotenusa a: ")));
                            return;
                        case "10":
                            Console.WriteLine("Hipotenusa a: {0}", Metricas.CalcularPelaRelacaoAltura(
                                LerNumeroPositivo("Cateto b: "), LerNumeroPositivo("Cateto c: "),
                                LerNumeroPositivo("Altura h: ")));
                            return;
                        default:
                            Console.WriteLine("Opção inválida. Escolha uma opção de 1 a 10.");
                            break;
                    }
                }
                catch (VoltarMenuException)
                {
                    // x durante um cálculo volta à lista de relações métricas.
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Não foi possível calcular: {0}", ex.Message);
                    return;
                }
            }
        }

        private static void CalcularAreaEPerimetro()
        {
            while (true)
            {
                DefinirMenu("4", "[Área e perímetro] Escolha o cálculo:",
                    "1 - Área por base e altura",
                    "2 - Perímetro de triângulo retângulo por dois catetos",
                    "3 - Perímetro por três lados");

                string escolha = LerOpcaoMenu("Qual cálculo deseja fazer? ");
                if (escolha == "x") return;
                if (escolha == "0") throw new MenuPrincipalException();

                try
                {
                    switch (escolha)
                    {
                        case "1":
                            Console.WriteLine("Área: {0}", Metricas.CalcularArea(
                                LerNumeroPositivo("Base: "), LerNumeroPositivo("Altura: ")));
                            return;
                        case "2":
                            Console.WriteLine("Perímetro: {0}", Metricas.CalcularPerimetro(
                                LerNumeroPositivo("Cateto 1: "), LerNumeroPositivo("Cateto 2: ")));
                            return;
                        case "3":
                            Console.WriteLine("Perímetro: {0}", Metricas.CalcularPerimetroDoTriangulo(
                                LerNumeroPositivo("Lado 1: "), LerNumeroPositivo("Lado 2: "),
                                LerNumeroPositivo("Lado 3: ")));
                            return;
                        default:
                            Console.WriteLine("Opção inválida. Escolha uma opção de 1 a 3.");
                            break;
                    }
                }
                catch (VoltarMenuException)
                {
                    // x durante um cálculo volta à lista de área e perímetro.
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Não foi possível calcular: {0}", ex.Message);
                    return;
                }
            }
        }

        private static void Ajuda()
        {
            while (true)
            {
                DefinirMenu("5", "[Ajuda e exemplos] Escolha um assunto:",
                    "1 - Triângulo retângulo e teorema de Pitágoras",
                    "2 - Razões trigonométricas",
                    "3 - Relações métricas (altura e projeções)",
                    "4 - Área e perímetro",
                    "5 - Como usar o programa e validar entradas");

                string opcao = LerOpcaoMenu("Assunto: ");
                if (opcao == "x") return;
                if (opcao == "0") throw new MenuPrincipalException();

                switch (opcao)
                {
                    case "1": Pitagoras.ExibirAjuda(); break;
                    case "2": Triogonometria.ExibirAjuda(); break;
                    case "3": Metricas.ExibirAjudaRelacoesMetricas(); break;
                    case "4": Metricas.ExibirAjudaAreaEPerimetro(); break;
                    case "5": AjudaArquivo.ExibirSecao("5"); break;
                    default: Console.WriteLine("Opção inválida. Escolha uma opção de 1 a 5."); break;
                }
            }
        }

        private static void DefinirMenu(string secaoAjuda, string titulo, params string[] opcoes)
        {
            _secaoAjudaAtual = secaoAjuda;
            _tituloMenuAtual = titulo;
            _opcoesMenuAtual = opcoes;
            ExibirMenuAtual();
        }

        private static void ExibirMenuAtual()
        {
            Console.WriteLine();
            Console.WriteLine(_tituloMenuAtual);
            foreach (string opcao in _opcoesMenuAtual) Console.WriteLine(opcao);
            Console.WriteLine("? - Ajuda");
            Console.WriteLine("x - Voltar");
            Console.WriteLine("0 - Menu principal");
        }

        private static string LerOpcaoMenu(string mensagem)
        {
            return LerEntradaInterativa(mensagem);
        }

        private static string LerEntradaInterativa(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine();
                if (entrada != "?") return entrada;

                AjudaArquivo.ExibirSecao(_secaoAjudaAtual);
                ExibirMenuAtual();
            }
        }

        private static int LerOpcao(int minimo, int maximo, string mensagem)
        {
            while (true)
            {
                string entrada = LerEntradaInterativa(mensagem);
                if (entrada == "x") throw new VoltarMenuException();
                if (entrada == "0") throw new MenuPrincipalException();

                int valor;
                if (int.TryParse(entrada, out valor) && valor >= minimo && valor <= maximo)
                    return valor;

                Console.WriteLine("Opção inválida. Digite um número de {0} a {1}.", minimo, maximo);
            }
        }

        private static double LerNumeroPositivo(string mensagem)
        {
            while (true)
            {
                string entrada = LerEntradaInterativa(mensagem);
                if (entrada == "x") throw new VoltarMenuException();

                double valor;
                if (double.TryParse(entrada, NumberStyles.Float, CultureInfo.CurrentCulture, out valor)
                    && !double.IsNaN(valor) && !double.IsInfinity(valor) && valor > 0)
                    return valor;

                Console.WriteLine("Valor inválido. Digite um número finito maior que zero (use vírgula ou ponto conforme a configuração do sistema).");
            }
        }

        private static double LerNumeroPositivoAbaixoDe(string mensagem, double limite)
        {
            while (true)
            {
                double valor = LerNumeroPositivo(mensagem);
                if (valor < limite) return valor;
                Console.WriteLine("O valor precisa ser menor que {0}.", limite);
            }
        }

        private static double LerAngulo()
        {
            while (true)
            {
                double valor = LerNumeroPositivo("Ângulo em graus (maior que 0 e menor que 90): ");
                if (valor < 90) return valor;
                Console.WriteLine("Um ângulo agudo precisa ser menor que 90°.");
            }
        }

        private static void ImprimirTriangulo(double oposto, double adjacente, double hipotenusa, double angulo)
        {
            Console.WriteLine("Cateto oposto: {0}", oposto);
            Console.WriteLine("Cateto adjacente: {0}", adjacente);
            Console.WriteLine("Hipotenusa: {0}", hipotenusa);
            Console.WriteLine("Ângulo informado/calculado: {0}°; outro ângulo agudo: {1}°", angulo, 90 - angulo);
        }

        private sealed class VoltarMenuException : Exception { }
        private sealed class MenuPrincipalException : Exception { }
    }
}
