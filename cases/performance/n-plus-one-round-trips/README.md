# Caso: N+1, Round-Trips Excessivos e Materialização Precoce

Este caso demonstra três problemas comuns encontrados em aplicações .NET
que acessam banco de dados:

-   N+1 Queries
-   Round-trips excessivos ao banco
-   Materialização precoce (uso inadequado de ToList)

O objetivo é mostrar o impacto dessas decisões e como corrigi-las de
forma prática e mensurável.

------------------------------------------------------------------------

## Contexto

Durante a análise de um processo real, foi identificado um tempo
excessivo de execução ao gerar relatórios com comparação entre bancos
distintos.

Na primeira execução:

-   1.740 registros processados
-   310 segundos de execução
-   Média de 5,6 segundos por registro

Após otimização:

-   Mesmo volume de registros
-   47 segundos de execução total
-   Aproximadamente 37 registros por segundo

Redução aproximada de 84% no tempo total.

------------------------------------------------------------------------

## Problemas Identificados

### 1️º N+1 Queries

Execução de uma consulta principal seguida de múltiplas consultas
adicionais dentro de loops.

Sintoma comum: - Alto número de queries no log - Crescimento exponencial
do tempo de execução

------------------------------------------------------------------------

### 2️º Round-Trips Excessivos

Chamadas repetidas ao banco de dados quando os dados poderiam ser
consolidados em uma única consulta.

Impacto: - Latência acumulada - Sobrecarga desnecessária no banco

------------------------------------------------------------------------

### 3️º Materialização Precoce

Uso de `.ToList()` antes do momento necessário, forçando a execução da
consulta e carregando dados em memória prematuramente.

Problemas causados: - Consumo excessivo de memória - Perda de otimização
no banco - Filtros aplicados em memória ao invés de no SQL

------------------------------------------------------------------------

## Estrutura do Caso

Este caso será dividido em duas abordagens:

### Versão Bad

-   Implementação com N+1
-   Múltiplos round-trips
-   Uso inadequado de ToList()
-   Filtros aplicados em memória

------------------------------------------------------------------------

### Versão Good

-   Consulta consolidada
-   Redução de round-trips
-   Uso adequado de Any() ao invés de Count() \> 0
-   Materialização apenas no momento necessário
-   Filtros aplicados diretamente na query

------------------------------------------------------------------------

## Objetivo Técnico

Demonstrar que:

-   O problema raramente está apenas na infraestrutura
-   Pequenas decisões no código impactam fortemente performance
-   Métricas são essenciais para validar melhorias
-   Otimização começa pela análise, não pelo chute

------------------------------------------------------------------------

## Como Executar

(Será definido após implementação do código)

------------------------------------------------------------------------

## Conceitos Relacionados

-   IQueryable vs IEnumerable
-   Execução adiada (Deferred Execution)
-   Any() vs Count()
-   Otimização de consultas SQL
-   Redução de latência em acesso a dados

------------------------------------------------------------------------

## Observação

Este caso faz parte do laboratório público dotnet-playground e está
diretamente relacionado aos conteúdos técnicos publicados no LinkedIn,
onde teoria e prática são conectadas através de exemplos reais e
reproduzíveis.

------------------------------------------------------------------------
