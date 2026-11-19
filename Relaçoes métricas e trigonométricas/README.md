# 📐 Relações Métricas e Trigonométricas no Triângulo Retângulo

Aplicação de console interativa desenvolvida em **C#** com o objetivo de automatizar os cálculos métricos e trigonométricos do triângulo retângulo **sem substituir o raciocínio do estudante**. 

Diferente de calculadoras convencionais que entregam apenas o resultado final, o sistema atua como um **tutor guiado**: o usuário identifica os elementos geométricos que possui, escolhe a relação adequada, recebe validações imediatas sobre a coerência matemática das medidas e pode consultar um guia didático contextualizado a qualquer momento.

---

## 🎯 Objetivo Pedagógico

* **Aprendizagem Ativa:** Poupa o estudante do esforço repetitivo de contas manuais (raízes quadradas, conversões angulares e funções inversas), mas exige a identificação correta de hipotenusa, catetos, projeções e ângulos de referência.
* **Ajuda Sensível ao Contexto (`?`):** Durante qualquer menu ou etapa de cálculo, o aluno pode digitar `?` para abrir explicações teóricas, diagramas em ASCII, fórmulas isoladas e exemplos numéricos resolvidos passo a passo — retornando exatamente ao ponto onde parou sem perder os dados.
* **Prevenção de Erros Geométricos:** Validações integradas impedem entradas inconsistentes (como catetos maiores ou iguais à hipotenusa, ângulos fora do intervalo agudo $0^\circ < \theta < 90^\circ$ ou valores de seno/cosseno fora do domínio) e exibem mensagens educativas explicando o motivo.

---

## ✨ Funcionalidades Principais

1. **Triângulo Retângulo (Teorema de Pitágoras)**
   * Cálculo de um lado faltante (hipotenusa ou cateto) a partir de duas medidas conhecidas.
   * Resolução completa do triângulo retângulo (todos os lados e os dois ângulos agudos).
2. **Razões Trigonométricas**
   * Cálculo de **Seno**, **Cosseno** e **Tangente** a partir das medidas dos lados.
   * Cálculo das três razões a partir de um ângulo agudo em graus (com conversão automática para radianos).
   * **Resolução guiada em 3 etapas:** descobre o ângulo por meio das funções inversas ($\text{sen}^{-1}, \cos^{-1}, \text{tg}^{-1}$) e calcula os demais lados do triângulo.
3. **Relações Métricas (Altura e Projeções)**
   * Convenção adotada: $a$ = hipotenusa; $b, c$ = catetos; $h$ = altura relativa à hipotenusa; $m, n$ = projeções ortogonais.
   * 10 caminhos de cálculo cobrindo as relações:
     * $a = m + n$
     * $b^2 = a \cdot m$ e $c^2 = a \cdot n$
     * $h^2 = m \cdot n$
     * $a \cdot h = b \cdot c$
4. **Área e Perímetro**
   * Área por base e altura: $A = \frac{\text{base} \cdot \text{altura}}{2}$.
   * Perímetro de triângulo retângulo a partir dos dois catetos ou de qualquer triângulo a partir dos três lados (com verificação da condição de existência triangular).
5. **Guia de Ajuda Integrado (`Ajuda.txt`)**
   * Manual estruturado por tópicos acessível tanto pelo menu principal quanto pelo atalho `?` em tempo de execução.

---

## 🗂️ Estrutura do Projeto

```text
├── Program.cs          # Interface de console, menus interativos, leitura e fluxo de navegação
├── Pitagoras.cs        # Cálculos do Teorema de Pitágoras com normalização numérica e validações
├── Trigonometria.cs    # Razões trigonométricas, funções inversas e conversão graus/radianos
├── Metricas.cs         # Relações métricas no triângulo retângulo, área e perímetro
├── AjudaArquivo.cs     # Leitor dinâmico das seções de ajuda contextualizadas
└── Ajuda.txt           # Base de conteúdo didático, diagramas ASCII e exemplos resolvidos
```

---

## 🚀 Como Executar o Projeto

### Pré-requisitos
* [.NET SDK](https://dotnet.microsoft.com/download) (versão 6.0 ou superior) instalado na máquina.

### Passo a passo

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/Varelaa26/relacoes-metricas-e-trigonometricas.git
   cd relacoes-metricas-e-trigonometricas
   ```

2. **Compile e execute a aplicação:**
   ```bash
   dotnet run
   ```

> **Nota:** Certifique-se de que o arquivo `Ajuda.txt` esteja configurado para ser copiado para o diretório de saída (`Copy to Output Directory: Copy always` ou `PreserveNewest` no arquivo `.csproj`), garantindo que o comando `?` funcione corretamente durante a execução.

---

## ⌨️ Comandos de Navegação Rápida

Durante o uso do programa no terminal, utilize os seguintes atalhos a qualquer momento:

| Tecla | Ação |
| :---: | :--- |
| **`?`** | Abre a ajuda teórica e exemplos referentes à tela/cálculo atual sem cancelar o progresso |
| **`x`** | Volta para o menu anterior |
| **`0`** | Retorna diretamente ao Menu Principal |

---

## 👥 Autores

Projeto acadêmico interdisciplinar (Matemática e Lógica de Programação):

Sâmili Morando

Samuel Ribeiro de Ramos

Sariannys Margarita Medina Nunez

Tharles Henrique de Sousa

Victor Gabriel Santos Varela

Victor Marques Ferreira

Vinicius Araldi de Oliveira

Vitória Manuela Motta

Yago Rodrigo Vidor

---

## 📚 Referências Bibliográficas

* DANTE, Luiz Roberto. *Matemática: contextos & aplicações*. 3. ed. São Paulo: Ática, 2016.
* IEZZI, Gelson. *Fundamentos de matemática elementar: trigonometria*. 9. ed. São Paulo: Atual, 2013. v. 3.