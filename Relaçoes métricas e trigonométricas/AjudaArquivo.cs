using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Relaçoes_métricas_e_trigonométricas
{
    internal static class AjudaArquivo
    {
        public static void ExibirSecao(string identificador)
        {
            string caminho = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Ajuda.txt");
            var linhasAjuda = new System.Collections.Generic.List<string>();
            if (!File.Exists(caminho))
            {
                linhasAjuda.Add("Arquivo de ajuda não encontrado: " + caminho);
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
                    if (imprimir) linhasAjuda.Add(linha);
                }

                if (!encontrou)
                    linhasAjuda.Add("Não há conteúdo cadastrado para esse assunto no arquivo Ajuda.txt.");
            }

            LimparTela();
            DesenharCabecalho();
            foreach (string linha in linhasAjuda) Console.WriteLine(linha);

            Console.Write("Pressione Enter para sair das informações de ajuda...");
            // Reposiciona a janela depois de toda a saída para deixar o título visível.
            FixarJanelaNoTopo();
            Console.ReadKey(true);
        }

        private static void DesenharCabecalho()
        {
            string borda = "+" + new string('-', 78) + "+";
            Console.WriteLine(borda);
            Console.WriteLine("| AJUDA".PadRight(79) + "|");
            Console.WriteLine(borda);
        }

        private static void FixarJanelaNoTopo()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
            try { Console.SetWindowPosition(Console.WindowLeft, 0); }
            catch (IOException) { }
            catch (PlatformNotSupportedException) { }
            catch (InvalidOperationException) { }
        }

        private static void LimparTela()
        {
            try { Console.Clear(); }
            catch (IOException) { }
            catch (PlatformNotSupportedException) { }
            catch (InvalidOperationException) { }
        }
    }
}
