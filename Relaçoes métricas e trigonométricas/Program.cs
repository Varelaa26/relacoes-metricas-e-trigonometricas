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
                Console.WriteLine("\n--- Relações métricas e trigonométricas ---");
                Console.WriteLine("1 - Teorema de Pitágoras");
                Console.WriteLine("2 - Razões trigonométricas");
                Console.WriteLine("3 - Relações métricas (altura e projeções)");
                Console.WriteLine("4 - Resolver triângulo retângulo");
                Console.WriteLine("5 - Ajuda e exemplos");
                Console.WriteLine("6 - Encerrar");
                switch (Console.ReadLine())
                {
                    case "1": CalcularPitagoras(); break;
                    case "2": CalcularRazoesTrigonométricas(); break;
                    case "3": CalcularRelacoesMetricas(); break;
                    case "4": ResolverTriangulo(); break;
                    case "5": Ajuda(); break;
                    case "6": return;
                    default: Console.WriteLine("Opção inválida. Escolha um número de 1 a 6."); break;
                }
            }
        }

        private static void CalcularPitagoras()
        {
            Console.WriteLine("1 - Calcular hipotenusa | 2 - Calcular cateto | 0 - Voltar");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Hipotenusa: {0}", Pitagoras.CalcularHipotenusa(LerNumeroPositivo("Cateto 1: "), LerNumeroPositivo("Cateto 2: ")));
                    break;
                case "2":
                    double h = LerNumeroPositivo("Hipotenusa: ");
                    double c = LerNumeroPositivoAbaixoDe("Cateto conhecido: ", h);
                    Console.WriteLine("Cateto restante: {0}", Pitagoras.CalcularCateto(h, c));
                    break;
                case "0": return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }

        private static void CalcularRazoesTrigonométricas()
        {
            Console.WriteLine("1 - Seno pelos lados | 2 - Cosseno pelos lados | 3 - Tangente pelos lados");
            Console.WriteLine("4 - Seno, cosseno e tangente por ângulo | 5 - Ângulo e demais lados | 0 - Voltar");
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
            Console.WriteLine("Razão conhecida: 1 - seno | 2 - cosseno | 3 - tangente");
            int razao = LerOpcao(1, 3);
            double valor = LerNumeroPositivo("Valor da razão: ");
            double angulo = Triogonometria.CalcularAngulo(razao, valor);
            Console.WriteLine("Ângulo: {0}°", angulo);
            Console.WriteLine("Lado conhecido: 1 - hipotenusa | 2 - cateto oposto | 3 - cateto adjacente");
            int lado = LerOpcao(1, 3);
            double conhecido = LerNumeroPositivo("Valor do lado: ");
            double oposto, adjacente, hipotenusa;
            Triogonometria.CalcularLados(angulo, lado, conhecido, out oposto, out adjacente, out hipotenusa);
            ImprimirTriangulo(oposto, adjacente, hipotenusa, angulo);
        }

        private static void CalcularRelacoesMetricas()
        {
            Console.WriteLine("Convenção: a é a hipotenusa; b²=a·m; c²=a·n; h²=m·n; a=m+n; a·h=b·c.");
            Console.WriteLine("1 - h por m,n | 2 - h por b,c | 3 - b por a,m | 4 - c por a,n | 5 - projeção por a e cateto | 6 - a por m,n | 0 - Voltar");
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
                    case "0": return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
            catch (ArgumentException ex) { Console.WriteLine("Não foi possível calcular: {0}", ex.Message); }
        }

        private static void ResolverTriangulo()
        {
            Console.WriteLine("Informe dois lados. Para catetos, a hipotenusa será calculada; para hipotenusa e cateto, o outro cateto será calculado.");
            try
            {
                Console.WriteLine("1 - dois catetos | 2 - hipotenusa e cateto");
                int tipo = LerOpcao(1, 2);
                double oposto, adjacente, hipotenusa;
                if (tipo == 1)
                {
                    oposto = LerNumeroPositivo("Cateto oposto (b): ");
                    adjacente = LerNumeroPositivo("Cateto adjacente (c): ");
                    hipotenusa = Pitagoras.CalcularHipotenusa(oposto, adjacente);
                }
                else
                {
                    hipotenusa = LerNumeroPositivo("Hipotenusa: ");
                    double cateto = LerNumeroPositivoAbaixoDe("Cateto conhecido: ", hipotenusa);
                    Console.WriteLine("Esse cateto é: 1 - oposto | 2 - adjacente");
                    if (LerOpcao(1, 2) == 1) { oposto = cateto; adjacente = Pitagoras.CalcularCateto(hipotenusa, cateto); }
                    else { adjacente = cateto; oposto = Pitagoras.CalcularCateto(hipotenusa, cateto); }
                }
                double angulo = Math.Atan(oposto / adjacente) * 180.0 / Math.PI;
                ImprimirTriangulo(oposto, adjacente, hipotenusa, angulo);
            }
            catch (ArgumentException ex) { Console.WriteLine("Não foi possível resolver o triângulo: {0}", ex.Message); }
        }

        private static void ImprimirTriangulo(double oposto, double adjacente, double hipotenusa, double angulo)
        {
            Console.WriteLine("Cateto oposto: {0}", oposto);
            Console.WriteLine("Cateto adjacente: {0}", adjacente);
            Console.WriteLine("Hipotenusa: {0}", hipotenusa);
            Console.WriteLine("Ângulo informado/calculado: {0}°; outro ângulo agudo: {1}°", angulo, 90 - angulo);
        }

        private static void Ajuda()
        {
            Console.WriteLine("No triângulo retângulo, a hipotenusa fica oposta ao ângulo de 90°. Em relação ao ângulo agudo escolhido, os catetos são oposto e adjacente.");
            Console.WriteLine("sen(θ)=oposto/hipotenusa; cos(θ)=adjacente/hipotenusa; tg(θ)=oposto/adjacente.");
            Console.WriteLine("Exemplo: oposto=3 e hipotenusa=5 => sen(θ)=3/5=0,6. Se θ=30°, sen=0,5, cos≈0,866 e tg≈0,577.");
            Console.WriteLine("Relações métricas: a é a hipotenusa; b e c são catetos; m e n são projeções correspondentes a b e c; h é a altura. b²=a·m; c²=a·n; h²=m·n; a=m+n; a·h=b·c.");
            Console.WriteLine("Exemplo: m=4 e n=9 => a=13 e h=6. Pitágoras: a²=b²+c².");
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
        }
    }
}
