# Case 3 --- Any() vs Count() e custo invisível de existência

## Contexto

Este case demonstra o impacto da escolha entre:

-   `Count() > 0`
-   `Any()`

Quando o objetivo é apenas verificar se **existe ao menos um elemento**
que satisfaça uma condição.

Embora ambos funcionem e retornem o mesmo resultado lógico, o custo de
execução pode ser significativamente diferente --- especialmente
conforme o dataset cresce.

------------------------------------------------------------------------

## Problema

É comum encontrar código como:

``` csharp
if (collection.Count(x => x.IsMatch) > 0)
{
    // ...
}
```

Quando o objetivo real é apenas saber se existe ao menos um item que
atenda à condição.

Esse padrão:

-   Funciona corretamente
-   É semanticamente válido
-   Mas pode induzir custo invisível

------------------------------------------------------------------------

## Hipótese

-   `Count(predicate)` força uma contagem completa.
-   `Any(predicate)` pode interromper a execução na primeira ocorrência.
-   Em memória, isso impacta diretamente a quantidade de elementos
    avaliados.
-   No banco, pode impactar plano de execução e tempo total.

------------------------------------------------------------------------

## Estrutura do Experimento

O experimento compara dois cenários:

-   **Bad(Count)**\
    Uso de `Count() > 0`

-   **Good(Any)**\
    Uso de `Any()`

A execução ocorre em dois ambientes:

### 1. InMemory

-   Lista com N registros
-   Apenas um registro satisfaz a condição
-   A ocorrência está próxima do início
-   Métricas observadas:
    -   Dataset total
    -   Quantidade efetivamente avaliada
    -   Tempo de execução

### 2. SQLite + Dapper

-   Tabela `Records`
-   Índice em `IsMatch`
-   Queries executadas:

``` sql
-- Bad
SELECT COUNT(1) FROM Records WHERE IsMatch = 1;

-- Good
SELECT 1 FROM Records WHERE IsMatch = 1 LIMIT 1;
```

No banco de dados não é possível medir diretamente quantas linhas foram
avaliadas pelo plano de execução, então a evidência principal é o tempo.

------------------------------------------------------------------------

## Métricas Exibidas

O console apresenta:

-   **DATASET** → quantidade total de registros
-   **AVALIADOS** → quantidade efetivamente percorrida (quando
    aplicável)
-   **TEMPO (MS)** → tempo total de execução

### InMemory

-   `AVALIADOS` representa exatamente quantos elementos foram
    percorridos.
-   `Any()` para cedo.
-   `Count()` percorre todos.

### SQLite

-   `AVALIADOS` não é mensurável diretamente → exibido como `N/A`.
-   A evidência principal é a diferença de tempo.

------------------------------------------------------------------------

## Conclusão

Quando o objetivo é verificar **existência**, `Any()` é:

-   Semanticamente correto
-   Mais expressivo
-   Geralmente mais eficiente

`Count() > 0` pode:

-   Induzir varredura completa em memória
-   Forçar agregação desnecessária no banco
-   Escalar mal conforme o dataset cresce

O gargalo muitas vezes não está na infraestrutura.

Está na escolha do método.

------------------------------------------------------------------------

## Execução

``` bash
dotnet run --project src/AnyVsCount.Console
```

Altere o modo no `Program.cs`:

``` csharp
const RunMode mode = RunMode.Demo;
```

ou

``` csharp
const RunMode mode = RunMode.Stress;
```

------------------------------------------------------------------------

## Estrutura do Case

    cases/performance/any-vs-count
     ├─ src
     │   ├─ AnyVsCount.Core
     │   ├─ AnyVsCount.Infrastructure
     │   └─ AnyVsCount.Console
     └─ README.md

------------------------------------------------------------------------

## Objetivo Educacional

Este case demonstra que pequenas escolhas de API podem gerar impacto
significativo em escala.

Não é uma micro-otimização.

É uma decisão semântica correta que também melhora performance.

------------------------------------------------------------------------

**Fim do Case 3**
