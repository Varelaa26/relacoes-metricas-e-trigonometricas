using System;
using System.Globalization;
using System.IO;

namespace Relaçoes_métricas_e_trigonométricas
{
    internal class Program
    {
        private static string _secaoAjudaAtual;
        private static string _tituloMenuAtual;
        private static string[] _opcoesMenuAtual;
        private static string _statusAtual;

        private static void Main()
        {
            while (true)
            {
                DesenharMenuPrincipal();

                try
                {
                    string opcaoPrincipal = Console.ReadLine();
                    switch (opcaoPrincipal)
                    {
                        case "1": _statusAtual = null; CalcularTriangulo(); break;
                        case "2": _statusAtual = null; CalcularRazoesTrigonométricas(); break;
                        case "3": _statusAtual = null; CalcularRelacoesMetricas(); break;
                        case "4": _statusAtual = null; CalcularAreaEPerimetro(); break;
                        case "5": _statusAtual = null; Ajuda(); break;
                        case "6":
                            MostrarResultado("Programa encerrado", "Pressione Enter para fechar o programa.",
                                new string[] { "Até a próxima!" });
                            return;
                        default: _statusAtual = "Opção inválida. Escolha uma opção de 1 a 6."; break;
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
                    _statusAtual = "Opção inválida. Escolha 1 ou 2.";
                    continue;
                }

                try
                {
                    LerDadosTriangulo(out double oposto, out double adjacente,
                        out double hipotenusa, out string ladoFaltante, out double valorFaltante);

                    if (pedido == "1")
                    {
                        MostrarResultado("Resultado solicitado",
                            string.Format("{0} = {1}", ladoFaltante, valorFaltante));
                    }
                    else
                    {
                        double angulo = Math.Atan(oposto / adjacente) * 180.0 / Math.PI;
                        MostrarResultado("Triângulo completo", LinhasTriangulo(oposto, adjacente, hipotenusa, angulo));
                    }
                    return;
                }
                catch (VoltarMenuException)
                {
                    // x durante o cálculo volta à lista de operações do triângulo.
                }
                catch (ArgumentException ex)
                {
                    _statusAtual = "Não foi possível atender ao cálculo solicitado: " + ex.Message;
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
                            MostrarResultado("Resultado - seno", string.Format("Seno: {0}", Triogonometria.CalcularSeno(
                                LerNumeroPositivo("Cateto oposto: "), LerNumeroPositivo("Hipotenusa: "))));
                            return;
                        case "2":
                            MostrarResultado("Resultado - cosseno", string.Format("Cosseno: {0}", Triogonometria.CalcularCosseno(
                                LerNumeroPositivo("Cateto adjacente: "), LerNumeroPositivo("Hipotenusa: "))));
                            return;
                        case "3":
                            MostrarResultado("Resultado - tangente", string.Format("Tangente: {0}", Triogonometria.CalcularTangente(
                                LerNumeroPositivo("Cateto oposto: "), LerNumeroPositivo("Cateto adjacente: "))));
                            return;
                        case "4":
                            double angulo = LerAngulo();
                            MostrarResultado("Razões trigonométricas",
                                string.Format("Ângulo: {0}°", angulo),
                                string.Format("Seno: {0}", Triogonometria.CalcularSenoDoAngulo(angulo)),
                                string.Format("Cosseno: {0}", Triogonometria.CalcularCossenoDoAngulo(angulo)),
                                string.Format("Tangente: {0}", Triogonometria.CalcularTangenteDoAngulo(angulo)));
                            return;
                        case "5":
                            CalcularAnguloELados();
                            return;
                        default:
                            _statusAtual = "Opção inválida. Escolha uma opção de 1 a 5.";
                            break;
                    }
                }
                catch (VoltarMenuException)
                {
                    // x durante um cálculo volta à lista de razões trigonométricas.
                }
                catch (ArgumentException ex)
                {
                    _statusAtual = "Não foi possível calcular: " + ex.Message;
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
            DefinirMenu("2", string.Format("[Etapa 3 de 3] Ângulo calculado: {0}°. Qual lado você conhece?", angulo),
                "1 - Hipotenusa",
                "2 - Cateto oposto",
                "3 - Cateto adjacente");
            int lado = LerOpcao(1, 3, "Escolha o lado conhecido: ");
            double conhecido = LerNumeroPositivo("Digite o valor do lado conhecido: ");
            Triogonometria.CalcularLados(angulo, lado, conhecido,
                out double oposto, out double adjacente, out double hipotenusa);
            MostrarResultado("Triângulo resolvido", LinhasTriangulo(oposto, adjacente, hipotenusa, angulo));
        }

        private static void CalcularRelacoesMetricas()
        {
            while (true)
            {
                DefinirMenu("3", "[Relações métricas] Escolha uma medida. Convenções: b²=a·m; c²=a·n; h²=m·n; a=m+n; a·h=b·c.",
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
                            MostrarValor("Altura h", Metricas.CalcularAlturaPelasProjecoes(
                                LerNumeroPositivo("Projeção m: "), LerNumeroPositivo("Projeção n: ")));
                            return;
                        case "2":
                            MostrarValor("Altura h", Metricas.CalcularAlturaPelosCatetos(
                                LerNumeroPositivo("Cateto b: "), LerNumeroPositivo("Cateto c: ")));
                            return;
                        case "3":
                            double a = LerNumeroPositivo("Hipotenusa a: ");
                            MostrarValor("Cateto b", Metricas.CalcularCateto(a,
                                LerNumeroPositivoAbaixoDe("Projeção m: ", a)));
                            return;
                        case "4":
                            a = LerNumeroPositivo("Hipotenusa a: ");
                            MostrarValor("Cateto c", Metricas.CalcularCateto(a,
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
                            MostrarValor("Projeção", Metricas.CalcularProjecao(cateto, a));
                            return;
                        case "6":
                            MostrarValor("Hipotenusa a", Metricas.CalcularHipotenusaPelasProjecoes(
                                LerNumeroPositivo("Projeção m: "), LerNumeroPositivo("Projeção n: ")));
                            return;
                        case "7":
                            MostrarValor("Cateto c", Metricas.CalcularPelaRelacaoAltura(
                                LerNumeroPositivo("Hipotenusa a: "), LerNumeroPositivo("Altura h: "),
                                LerNumeroPositivo("Cateto b: ")));
                            return;
                        case "8":
                            MostrarValor("Cateto b", Metricas.CalcularPelaRelacaoAltura(
                                LerNumeroPositivo("Hipotenusa a: "), LerNumeroPositivo("Altura h: "),
                                LerNumeroPositivo("Cateto c: ")));
                            return;
                        case "9":
                            MostrarValor("Altura h", Metricas.CalcularPelaRelacaoAltura(
                                LerNumeroPositivo("Cateto b: "), LerNumeroPositivo("Cateto c: "),
                                LerNumeroPositivo("Hipotenusa a: ")));
                            return;
                        case "10":
                            MostrarValor("Hipotenusa a", Metricas.CalcularPelaRelacaoAltura(
                                LerNumeroPositivo("Cateto b: "), LerNumeroPositivo("Cateto c: "),
                                LerNumeroPositivo("Altura h: ")));
                            return;
                        default:
                            _statusAtual = "Opção inválida. Escolha uma opção de 1 a 10.";
                            break;
                    }
                }
                catch (VoltarMenuException)
                {
                    // x durante um cálculo volta à lista de relações métricas.
                }
                catch (ArgumentException ex)
                {
                    _statusAtual = "Não foi possível calcular: " + ex.Message;
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
                            MostrarValor("Área", Metricas.CalcularArea(
                                LerNumeroPositivo("Base: "), LerNumeroPositivo("Altura: ")));
                            return;
                        case "2":
                            MostrarValor("Perímetro", Metricas.CalcularPerimetro(
                                LerNumeroPositivo("Cateto 1: "), LerNumeroPositivo("Cateto 2: ")));
                            return;
                        case "3":
                            MostrarValor("Perímetro", Metricas.CalcularPerimetroDoTriangulo(
                                LerNumeroPositivo("Lado 1: "), LerNumeroPositivo("Lado 2: "),
                                LerNumeroPositivo("Lado 3: ")));
                            return;
                        default:
                            _statusAtual = "Opção inválida. Escolha uma opção de 1 a 3.";
                            break;
                    }
                }
                catch (VoltarMenuException)
                {
                    // x durante um cálculo volta à lista de área e perímetro.
                }
                catch (ArgumentException ex)
                {
                    _statusAtual = "Não foi possível calcular: " + ex.Message;
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
                    default: _statusAtual = "Opção inválida. Escolha uma opção de 1 a 5."; break;
                }
            }
        }

        private static void DefinirMenu(string secaoAjuda, string titulo, params string[] opcoes)
        {
            _secaoAjudaAtual = secaoAjuda;
            _tituloMenuAtual = titulo;
            _opcoesMenuAtual = opcoes;
        }

        private static void DesenharMenuPrincipal()
        {
            LimparTela();
            DesenharQuadro(new string[]
            {
                "RELAÇÕES MÉTRICAS E TRIGONOMÉTRICAS",
                "1 - Triângulo retângulo (calcular lado ou resolver completo)",
                "2 - Razões trigonométricas",
                "3 - Relações métricas (altura e projeções)",
                "4 - Área e perímetro",
                "5 - Ajuda e exemplos",
                "6 - Encerrar",
                _statusAtual ?? string.Empty,
                "O que você deseja fazer? Digite uma opção:"
            });
            Console.Write("> ");
        }

        private static string LerOpcaoMenu(string mensagem)
        {
            return LerEntradaInterativa(mensagem);
        }

        private static string LerEntradaInterativa(string mensagem)
        {
            while (true)
            {
                DesenharTelaEntrada(mensagem);
                string entrada = Console.ReadLine();
                if (entrada != "?")
                {
                    _statusAtual = null;
                    return entrada;
                }

                AjudaArquivo.ExibirSecao(_secaoAjudaAtual);
            }
        }

        private static void DesenharTelaEntrada(string mensagem)
        {
            LimparTela();
            var linhas = new System.Collections.Generic.List<string>();
            linhas.Add(_tituloMenuAtual ?? "");
            if (_opcoesMenuAtual != null) linhas.AddRange(_opcoesMenuAtual);
            linhas.Add("? - Ajuda");
            linhas.Add("x - Voltar");
            linhas.Add("0 - Menu principal");
            if (!string.IsNullOrEmpty(_statusAtual)) linhas.Add(_statusAtual);
            linhas.Add(mensagem);
            DesenharQuadro(linhas);
            Console.Write("> ");
        }

        private static void MostrarResultado(string titulo, params string[] linhas)
        {
            MostrarResultado(titulo, "Pressione Enter para voltar ao menu.", linhas);
        }

        private static void MostrarResultado(string titulo, string instrucao, string[] linhas)
        {
            LimparTela();
            var conteudo = new System.Collections.Generic.List<string>();
            conteudo.Add(titulo);
            conteudo.AddRange(linhas);
            conteudo.Add(string.Empty);
            conteudo.Add(instrucao);
            DesenharQuadro(conteudo);
            Console.ReadLine();
        }

        private static void DesenharQuadro(System.Collections.Generic.IEnumerable<string> linhas)
        {
            const int larguraConteudo = 76;
            string borda = "+" + new string('-', larguraConteudo + 2) + "+";
            Console.WriteLine(borda);
            foreach (string linha in linhas)
            {
                string restante = linha ?? string.Empty;
                if (restante.Length == 0)
                {
                    Console.WriteLine("| " + new string(' ', larguraConteudo) + " |");
                    continue;
                }

                while (restante.Length > larguraConteudo)
                {
                    int corte = restante.LastIndexOf(' ', larguraConteudo);
                    if (corte <= 0) corte = larguraConteudo;
                    Console.WriteLine("| " + restante.Substring(0, corte).PadRight(larguraConteudo) + " |");
                    restante = restante.Substring(corte).TrimStart();
                }
                Console.WriteLine("| " + restante.PadRight(larguraConteudo) + " |");
            }
            Console.WriteLine(borda);
        }

        private static void LimparTela()
        {
            try { Console.Clear(); }
            catch (IOException) { }
            catch (PlatformNotSupportedException) { }
            catch (InvalidOperationException) { }
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

                _statusAtual = string.Format("Opção inválida. Digite um número de {0} a {1}.", minimo, maximo);
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

                _statusAtual = "Valor inválido. Digite um número finito maior que zero (use vírgula ou ponto conforme a configuração do sistema).";
            }
        }

        private static double LerNumeroPositivoAbaixoDe(string mensagem, double limite)
        {
            while (true)
            {
                double valor = LerNumeroPositivo(mensagem);
                if (valor < limite) return valor;
                _statusAtual = string.Format("O valor precisa ser menor que {0}.", limite);
            }
        }

        private static double LerAngulo()
        {
            while (true)
            {
                double valor = LerNumeroPositivo("Ângulo em graus (maior que 0 e menor que 90): ");
                if (valor < 90) return valor;
                _statusAtual = "Um ângulo agudo precisa ser menor que 90°.";
            }
        }

        private static string[] LinhasTriangulo(double oposto, double adjacente, double hipotenusa, double angulo)
        {
            return new string[]
            {
                string.Format("Cateto oposto: {0}", oposto),
                string.Format("Cateto adjacente: {0}", adjacente),
                string.Format("Hipotenusa: {0}", hipotenusa),
                string.Format("Ângulo informado/calculado: {0}°; outro ângulo agudo: {1}°", angulo, 90 - angulo)
            };
        }

        private static void MostrarValor(string nome, double valor)
        {
            MostrarResultado("Resultado do cálculo", string.Format("{0}: {1}", nome, valor));
        }

        private sealed class VoltarMenuException : Exception { }
        private sealed class MenuPrincipalException : Exception { }
    }
}
