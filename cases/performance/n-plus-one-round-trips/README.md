# N+1 e Round Trips

Este caso demonstra, de forma controlada e reproduzível, o impacto do padrão N+1 e do excesso de round-trips em operações de acesso a dados.

Problemas como esse são comuns em aplicações que utilizam ORMs ou consultas mal estruturadas, resultando em múltiplas chamadas desnecessárias ao banco de dados.

A implementação compara duas abordagens:

- **Bad**: simula materialização precoce e consultas item a item (N+1).
- **Good**: reduz chamadas desnecessárias e executa apenas uma consulta principal.

O objetivo é evidenciar, de forma mensurável, a diferença de tempo entre as duas estratégias.

---

## Estrutura

O caso está organizado da seguinte forma:

```text
src/
  NPlusOneRoundTrips.Core/
    - Simulador de acesso a dados
    - Cenários Bad e Good
    - Runner de execução

  NPlusOneRoundTrips.Console/
    - Aplicação Console
    - Classe de apresentação (ConsoleReportPrinter)

shared/
  DotnetPlayground.Common/
    - Componentes reutilizáveis para aplicações console
```

O projeto **Core** não depende do Console, permitindo futura reutilização em uma API ou outro host.

---

## Como executar

### Pré-requisitos

- .NET 8 SDK instalado

### Via Visual Studio

1. Defina `NPlusOneRoundTrips.Console` como Startup Project.
2. Execute a aplicação.

### Via CLI

```bash
dotnet run --project src/NPlusOneRoundTrips.Console
```

---

## Modos de execução

A execução é controlada pelo `RunMode` no arquivo `Program.cs`.

Existem dois modos recomendados:

### Demo

Indicado para demonstração visual do impacto do N+1.

- totalRecords: 500
- inMemoryDelayMs: 2

Nesse modo, a latência simulada evidencia o custo de múltiplos round-trips no cenário Bad.

---

### Stress

Indicado para execução com volume maior.

- totalRecords: 10000
- inMemoryDelayMs: 0

Nesse modo, o foco é comparar round-trips reais no SQLite:

- Bad → múltiplas consultas individuais
- Good → consulta única consolidada

---

## Saída da aplicação

A aplicação exibe duas comparações:

1. **InMemory** (simulação controlada)
2. **SQLite (Dapper)** (execução real contra banco local)

Para cada cenário são exibidos:

- Nome do cenário
- Quantidade de registros
- Tempo de execução em milissegundos

Ao final, é exibido um resumo com:

- Fator de velocidade entre Good e Bad
- Percentual aproximado de redução

---

## Observação sobre o modo InMemory

O modo InMemory utiliza uma simulação de round-trip através de um delay configurável (`inMemoryDelayMs`).

- Com delay > 0, o custo do N+1 torna-se evidente.
- Com delay = 0, o impacto tende a ser menor, pois não há rede ou disco envolvidos.

O objetivo do InMemory é permitir uma demonstração controlada do efeito de múltiplas chamadas.

Já o modo SQLite executa consultas reais utilizando Dapper, aproximando o comportamento de um cenário do dia a dia.

---

## Objetivo técnico

Este caso serve como material de apoio para:

- Discussão sobre N+1
- Impacto de múltiplos round-trips
- Materialização precoce (ex: ToList mal posicionado)
- Diferença entre execução em memória e acesso real a banco
- Separação de responsabilidades (Core / Infra / Console)

Trata-se de um laboratório técnico voltado para estudo e demonstração prática de padrões de acesso a dados.
