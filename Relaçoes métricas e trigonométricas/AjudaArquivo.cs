using System;
using System.IO;

namespace Relaçoes_métricas_e_trigonométricas
{
    internal static class AjudaArquivo
    {
        public static void ExibirSecao(string identificador)
        {
            string caminho = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Ajuda.txt");
            string borda = "+" + new string('-', 78) + "+";
            try { Console.Clear(); }
            catch (IOException) { }
            catch (PlatformNotSupportedException) { }
            catch (InvalidOperationException) { }

            Console.WriteLine(borda);
            Console.WriteLine("| AJUDA".PadRight(79) + "|");
            Console.WriteLine(borda);

            if (!File.Exists(caminho))
            {
                Console.WriteLine("Arquivo de ajuda não encontrado: {0}", caminho);
            }
            else
            {
                string marcador = "[AJUDA:" + identificador + "]";
                bool imprimir = false;
                bool encontrou = false;
                foreach (string linha in File.ReadLines(caminho))
                {
                    if (linha.Trim() == marcador)
                    {
                        imprimir = true;
                        encontrou = true;
                        continue;
                    }

                    if (imprimir && linha.Trim() == "[FIM]") break;
                    if (imprimir) Console.WriteLine(linha);
                }

                if (!encontrou)
                    Console.WriteLine("Não há conteúdo cadastrado para esse assunto no arquivo Ajuda.txt.");
            }

            Console.WriteLine(borda);
            Console.WriteLine();
            Console.Write("Pressione Enter para sair das informações de ajuda...");
            Console.ReadLine();
        }
    }
}
