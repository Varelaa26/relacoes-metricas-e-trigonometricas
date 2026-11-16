using System;
using System.Globalization;

namespace Relaçoes_métricas_e_trigonométricas
{
    internal class Program
    {
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
                Console.WriteLine("6 - Encerrar"/n);
                Console.Write("O que você deseja fazer? Digite uma opção: ");
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
        }

        private static void CalcularTriangulo()
        {
            Console.WriteLine("\n[Triângulo retângulo] Escolha o resultado desejado:");
            Console.WriteLine("1 - Encontrar um lado que falta");
            Console.WriteLine("2 - Resolver o triângulo completo (lados e ângulos)");
            Console.WriteLine("0 - Voltar ao menu principal");
            Console.Write("Qual cálculo deseja fazer? ");
            string pedido = Console.ReadLine();
            if (pedido == "0") return;
            if (pedido != "1" && pedido != "2") { Console.WriteLine("Opção inválida. Escolha 0, 1 ou 2."); return; }

            try
            {
                LerDadosTriangulo(out double oposto, out double adjacente, out double hipotenusa, out string ladoFaltante, out double valorFaltante);
                if (pedido == "1")
                {
                    Console.WriteLine("\n[Resultado solicitado] Lados calculados a partir dos dados informados:");
                    Console.WriteLine("{0}: {1}", ladoFaltante, valorFaltante);
                }
                else
                {
                    double angulo = Math.Atan(oposto / adjacente) * 180.0 / Math.PI;
                    Console.WriteLine("\n[Resultado solicitado] Triângulo completo:");
                    ImprimirTriangulo(oposto, adjacente, hipotenusa, angulo);
                }
            }
            catch (ArgumentException ex) { Console.WriteLine("Não foi possível atender ao cálculo solicitado: {0}", ex.Message); }
        }

        private static void LerDadosTriangulo(out double oposto, out double adjacente, out double hipotenusa, out string ladoFaltante, out double valorFaltante)
        {
            Console.WriteLine("\n[Etapa 1 de 2] Informe quais medidas você já conhece.");
            Console.WriteLine("1 - Conheço os dois catetos");
            Console.WriteLine("2 - Conheço a hipotenusa e um cateto");
            Console.Write("Qual conjunto de medidas você tem? ");
            int tipo = LerOpcao(1, 2);
            Console.WriteLine("\n[Etapa 2 de 2] Agora informe as medidas pedidas.");
            if (tipo == 1)
            {
                oposto = LerNumeroPositivo("Cateto oposto ao ângulo de referência: ");
                adjacente = LerNumeroPositivo("Cateto adjacente ao ângulo de referência: ");
                hipotenusa = Pitagoras.CalcularHipotenusa(oposto, adjacente);
                ladoFaltante = "Hipotenusa";
                valorFaltante = hipotenusa;
            }
            else
            {
                hipotenusa = LerNumeroPositivo("Hipotenusa (lado maior, oposto ao ângulo reto): ");
                double cateto = LerNumeroPositivoAbaixoDe("Valor do cateto conhecido: ", hipotenusa);
                Console.WriteLine("Esse cateto é: 1 - oposto ao ângulo de referência | 2 - adjacente");
                Console.Write("Escolha 1 ou 2: ");
                if (LerOpcao(1, 2) == 1)
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
        }

        private static void CalcularRazoesTrigonométricas()
        {
            Console.WriteLine("1 - Seno pelos lados | 2 - Cosseno pelos lados | 3 - Tangente pelos lados");
            Console.WriteLine("4 - Seno, cosseno e tangente por ângulo | 5 - Ângulo e demais lados | 0 - Voltar");
            Console.Write("[Razões trigonométricas] Qual cálculo deseja fazer? ");
            try
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        double op = LerNumeroPositivo("Cateto oposto: ");
                        double hip = LerNumeroPositivo("Hipotenusa: ");
                        Console.WriteLine("Seno: {0}", Triogonometria.CalcularSeno(op, hip)); break;
                    case "2":
                        double adj = LerNumeroPositivo("Cateto adjacente: ");
                        double hip2 = LerNumeroPositivo("Hipotenusa: ");
                        Console.WriteLine("Cosseno: {0}", Triogonometria.CalcularCosseno(adj, hip2)); break;
                    case "3":
                        Console.WriteLine("Tangente: {0}", Triogonometria.CalcularTangente(LerNumeroPositivo("Cateto oposto: "), LerNumeroPositivo("Cateto adjacente: "))); break;
                    case "4":
                        double ang = LerAngulo();
                        Console.WriteLine("Seno: {0}; cosseno: {1}; tangente: {2}", Triogonometria.CalcularSenoDoAngulo(ang), Triogonometria.CalcularCossenoDoAngulo(ang), Triogonometria.CalcularTangenteDoAngulo(ang)); break;
                    case "5": CalcularAnguloELados(); break;
                    case "0": return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
            catch (ArgumentException ex) { Console.WriteLine("Não foi possível calcular: {0}", ex.Message); }
        }

        private static void CalcularAnguloELados()
        {
            Console.WriteLine("[Etapa 1 de 3] Qual razão trigonométrica você conhece?");
            Console.WriteLine("1 - seno | 2 - cosseno | 3 - tangente");
            Console.Write("Escolha a razão: ");
            int razao = LerOpcao(1, 3);
            double valor = LerNumeroPositivo("[Etapa 2 de 3] Digite o valor dessa razão: ");
            double angulo = Triogonometria.CalcularAngulo(razao, valor);
            Console.WriteLine("Ângulo: {0}°", angulo);
            Console.WriteLine("[Etapa 3 de 3] Qual lado você conhece para calcular os demais?");
            Console.WriteLine("Lado conhecido: 1 - hipotenusa | 2 - cateto oposto | 3 - cateto adjacente");
            Console.Write("Escolha o lado: ");
            int lado = LerOpcao(1, 3);
            double conhecido = LerNumeroPositivo("Digite o valor do lado conhecido: ");
            double oposto, adjacente, hipotenusa;
            Triogonometria.CalcularLados(angulo, lado, conhecido, out oposto, out adjacente, out hipotenusa);
            ImprimirTriangulo(oposto, adjacente, hipotenusa, angulo);
        }

        private static void CalcularRelacoesMetricas()
        {
            Console.WriteLine("\n[Relações métricas] Escolha qual medida deseja encontrar:");
            Console.WriteLine("Convenção: a é a hipotenusa; b²=a·m; c²=a·n; h²=m·n; a=m+n; a·h=b·c.");
            Console.WriteLine("1 - h por m,n | 2 - h por b,c | 3 - b por a,m | 4 - c por a,n | 5 - projeção por a e cateto | 6 - a por m,n");
            Console.WriteLine("7 - c por a,h,b | 8 - b por a,h,c | 9 - h por b,c,a | 10 - a por b,c,h | 0 - Voltar");
            Console.Write("Qual cálculo deseja fazer? ");
            try
            {
                switch (Console.ReadLine())
                {
                    case "1": Console.WriteLine("Altura h: {0}", Metricas.CalcularAlturaPelasProjecoes(LerNumeroPositivo("m: "), LerNumeroPositivo("n: "))); break;
                    case "2": Console.WriteLine("Altura h: {0}", Metricas.CalcularAlturaPelosCatetos(LerNumeroPositivo("Cateto b: "), LerNumeroPositivo("Cateto c: "))); break;
                    case "3":
                        double a = LerNumeroPositivo("Hipotenusa a: ");
                        Console.WriteLine("Cateto b: {0}", Metricas.CalcularCateto(a, LerNumeroPositivoAbaixoDe("Projeção m: ", a))); break;
                    case "4":
                        a = LerNumeroPositivo("Hipotenusa a: ");
                        Console.WriteLine("Cateto c: {0}", Metricas.CalcularCateto(a, LerNumeroPositivoAbaixoDe("Projeção n: ", a))); break;
                    case "5":
                        a = LerNumeroPositivo("Hipotenusa a: ");
                        Console.WriteLine("1 - calcular projeção m por b | 2 - calcular projeção n por c");
                        int qual = LerOpcao(1, 2);
                        double cateto = LerNumeroPositivoAbaixoDe(qual == 1 ? "Cateto b: " : "Cateto c: ", a);
                        Console.WriteLine("Projeção: {0}", Metricas.CalcularProjecao(cateto, a)); break;
                    case "6": Console.WriteLine("Hipotenusa a: {0}", Metricas.CalcularHipotenusaPelasProjecoes(LerNumeroPositivo("m: "), LerNumeroPositivo("n: "))); break;
                    case "7": Console.WriteLine("Cateto c: {0}", Metricas.CalcularPelaRelacaoAltura(LerNumeroPositivo("Hipotenusa a: "), LerNumeroPositivo("Altura h: "), LerNumeroPositivo("Cateto b: "))); break;
                    case "8": Console.WriteLine("Cateto b: {0}", Metricas.CalcularPelaRelacaoAltura(LerNumeroPositivo("Hipotenusa a: "), LerNumeroPositivo("Altura h: "), LerNumeroPositivo("Cateto c: "))); break;
                    case "9": Console.WriteLine("Altura h: {0}", Metricas.CalcularPelaRelacaoAltura(LerNumeroPositivo("Cateto b: "), LerNumeroPositivo("Cateto c: "), LerNumeroPositivo("Hipotenusa a: "))); break;
                    case "10": Console.WriteLine("Hipotenusa a: {0}", Metricas.CalcularPelaRelacaoAltura(LerNumeroPositivo("Cateto b: "), LerNumeroPositivo("Cateto c: "), LerNumeroPositivo("Altura h: "))); break;
                    case "0": return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
            catch (ArgumentException ex) { Console.WriteLine("Não foi possível calcular: {0}", ex.Message); }
        }

        private static void CalcularAreaEPerimetro()
        {
            Console.WriteLine("1 - Área por base e altura | 2 - Perímetro de triângulo retângulo | 3 - Perímetro por três lados | 0 - Voltar");
            Console.Write("Escolha: ");
            try
            {
                switch (Console.ReadLine())
                {
                    case "1": Console.WriteLine("Área: {0}", Metricas.CalcularArea(LerNumeroPositivo("Base: "), LerNumeroPositivo("Altura: "))); break;
                    case "2": Console.WriteLine("Perímetro: {0}", Metricas.CalcularPerimetro(LerNumeroPositivo("Cateto 1: "), LerNumeroPositivo("Cateto 2: "))); break;
                    case "3": Console.WriteLine("Perímetro: {0}", Metricas.CalcularPerimetroDoTriangulo(LerNumeroPositivo("Lado 1: "), LerNumeroPositivo("Lado 2: "), LerNumeroPositivo("Lado 3: "))); break;
                    case "0": return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
            catch (ArgumentException ex) { Console.WriteLine("Não foi possível calcular: {0}", ex.Message); }
        }

        private static void ImprimirTriangulo(double oposto, double adjacente, double hipotenusa, double angulo)
        {
            Console.WriteLine("Cateto oposto: {0}", oposto);
            Console.WriteLine("Cateto adjacente: {0}", adjacente);
            Console.WriteLine("Hipotenusa: {0}", hipotenusa);
            Console.WriteLine("Ângulo informado/calculado: {0}°; outro ângulo agudo: {1}°", angulo, 90 - angulo);
        }

        private static int LerOpcao(int minimo, int maximo)
        {
            while (true)
            {
                int valor;
                if (int.TryParse(Console.ReadLine(), out valor) && valor >= minimo && valor <= maximo) return valor;
                Console.Write("Opção inválida. Digite de {0} a {1}: ", minimo, maximo);
            }
        }

        private static double LerNumeroPositivo(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                double valor;
                if (double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.CurrentCulture, out valor) && !double.IsNaN(valor) && !double.IsInfinity(valor) && valor > 0) return valor;
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
        }.

        private static void Ajuda()
        {
            Console.WriteLine("\n[Ajuda e exemplos] Este programa permite calcular relações métricas e trigonométricas em triângulos retângulos.\n");
            Console.WriteLine("Qual assunto você deseja ajuda? Escolha uma opção:\n");
            Console.WriteLine("1 - Triângulo retângulo (calcular lado ou resolver completo)");
            Console.WriteLine("2 - Razões trigonométricas");
            Console.WriteLine("3 - Relações métricas (altura e projeções)");
            Console.WriteLine("4 - Área e perímetro");
            Console.WriteLine("6 - Voltar"/n);
            Console.Write("O que você deseja fazer? Digite uma opção: ");

            switch
        }

    }
}
