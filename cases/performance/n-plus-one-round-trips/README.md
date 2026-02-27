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

## Saída esperada

A aplicação exibe uma tabela comparando:

- Nome do cenário
- Quantidade de registros
- Tempo de execução em milissegundos

Ao final, é exibido um resumo com:

- Fator de velocidade entre Good e Bad
- Percentual aproximado de redução

---

## Objetivo técnico

Este caso serve como material de apoio para:

- Discussão sobre N+1
- Impacto de múltiplos round-trips
- Materialização precoce (ToList mal posicionado)
- Comparação prática entre abordagens

Trata-se de uma simulação conceitual. Em versões futuras, pode ser expandido para uso com banco real (ex: SQLite) para análise mais próxima de cenários de produção.
