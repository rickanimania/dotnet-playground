# Caso: N+1, round-trips excessivos e materialização precoce

Este caso demonstra três problemas comuns em aplicações .NET que acessam
banco de dados:

-   N+1 queries
-   round-trips excessivos ao banco
-   materialização precoce (uso inadequado de `ToList()`)

O objetivo é mostrar o impacto dessas decisões e como corrigir o fluxo
de acesso a dados de forma prática e mensurável.

## Contexto

Durante a análise de um processo real, foi identificado um tempo
excessivo de execução ao gerar relatórios com comparação entre bases
distintas.

Primeira execução:

-   1.740 registros processados
-   310 segundos de execução
-   média de 5,6 segundos por registro

Após otimização:

-   mesmo volume de registros
-   47 segundos de execução total
-   aproximadamente 37 registros por segundo

Redução aproximada de 84% no tempo total.

## Problemas identificados

### 1. N+1 queries

Execução de uma consulta principal seguida de múltiplas consultas
adicionais dentro de loops.

Sinais comuns:

-   alto número de queries no log
-   aumento significativo do tempo total conforme o volume cresce

### 2. Round-trips excessivos

Chamadas repetidas ao banco de dados quando os dados poderiam ser
consolidados em uma única consulta.

Impactos típicos:

-   latência acumulada
-   sobrecarga desnecessária no banco

### 3. Materialização precoce

Uso de `ToList()` antes do momento necessário, forçando a execução da
consulta e carregando dados em memória prematuramente.

Problemas causados:

-   consumo desnecessário de memória
-   perda de otimizações do banco (por exemplo, filtros aplicados em
    memória)
-   execução antecipada que dificulta composição de query

## Estrutura do caso

Este caso será dividido em duas abordagens:

### Versão Bad

-   implementação com N+1
-   múltiplos round-trips
-   uso inadequado de `ToList()`
-   filtros aplicados em memória

### Versão Good

-   consulta consolidada (redução de round-trips)
-   uso adequado de `Any()` em cenários de existência (quando aplicável)
-   materialização somente no momento necessário
-   filtros aplicados diretamente na query

## Objetivo técnico

Demonstrar que:

-   gargalos de performance frequentemente estão na forma como os dados
    são acessados
-   pequenas decisões no código podem ter grande impacto em tempo total
    e latência
-   métricas e logs são necessários para validar a melhoria

## Como executar

A forma de execução será definida após a implementação do código
(console ou API).

## Conceitos relacionados

-   `IQueryable` vs `IEnumerable`
-   execução adiada (deferred execution)
-   `Any()` vs `Count()`
-   otimização de consultas SQL
-   redução de latência no acesso a dados

## Observação

Este caso faz parte do repositório `dotnet-playground` e será evoluído
conforme novos exemplos forem adicionados.
