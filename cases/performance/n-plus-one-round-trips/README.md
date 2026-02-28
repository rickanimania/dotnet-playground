# N+1 e Round Trips

Este caso demonstra, de forma controlada e reproduzível, o impacto do padrão N+1 e do excesso de round-trips em operações de acesso a dados.

A implementação é dividida em dois cenários:

- Bad: simula materialização precoce e consultas item a item (N+1).
- Good: reduz chamadas desnecessárias e executa apenas uma consulta principal.

O objetivo é evidenciar a diferença de tempo de execução entre as duas abordagens.

---

## Estrutura

O caso está organizado da seguinte forma:

src/
  NPlusOneRoundTrips.Core/
    - Simulador de acesso a dados
    - Cenários Bad e Good
    - Runner de execução
  NPlusOneRoundTrips.Console/
    - Aplicação Console
    - Classe de apresentação (ConsoleReportPrinter)

O projeto Core não depende do Console, permitindo futura reutilização em uma API.

---

## Como executar

Pré-requisitos:
- .NET 8 SDK instalado

Via Visual Studio:
1. Defina NPlusOneRoundTrips.Console como Startup Project.
2. Execute a aplicação.

Via CLI (opcional):
dotnet run --project src/NPlusOneRoundTrips.Console

---

## Configuração

No arquivo Program.cs é possível ajustar:

- totalRecords: quantidade de registros simulados.

Exemplo:

const int totalRecords = 100;

Quanto maior o número de registros, maior será o impacto do cenário Bad.

---

## Modos recomendados

Este caso possui dois modos de execucao sugeridos, configurados no Program.cs.

### Demo
Indicado para demonstracao rapida e visual.

- totalRecords: 500
- inMemoryDelayMs: 2

Neste modo, a latencia simulada evidencia o custo do N+1 no cenario Bad.

### Stress
Indicado para executar com volume maior sem tornar a simulacao lenta.

- totalRecords: 10000
- inMemoryDelayMs: 0

Neste modo, o foco eh comparar round-trips reais no SQLite (Bad com varias consultas vs Good com consulta unica).

---

## Saída esperada

A aplicação exibe uma tabela comparando:

- Nome do cenário
- Quantidade de registros
- Tempo de execução em milissegundos

Ao final, é exibido um resumo com:

- Fator de velocidade entre Good e Bad
- Percentual aproximado de redução

## Observacao sobre o modo InMemory

O modo InMemory utiliza uma simulacao de round-trip atraves de um delay configuravel (inMemoryDelayMs).

- Com delay > 0, o custo de multiplas chamadas (N+1) fica evidente no tempo total.
- Com delay = 0, o custo do N+1 em memoria tende a ser pequeno, pois nao existe rede, disco ou parse de resultados, e as operacoes sao apenas loops e buscas em colecao.

O objetivo do InMemory eh permitir uma demonstracao controlada do efeito de round-trips.
Ja o modo SQLite executa consultas reais, aproximando o caso de um cenario do dia a dia.

---


## Objetivo técnico

Este caso serve como material de apoio para:

- Discussão sobre N+1
- Impacto de múltiplos round-trips
- Materialização precoce (ToList mal posicionado)
- Comparação prática entre abordagens

Trata-se de uma simulação conceitual. Em versões futuras, pode ser expandido para uso com banco real (ex: SQLite) para análise mais próxima de cenários de produção.
