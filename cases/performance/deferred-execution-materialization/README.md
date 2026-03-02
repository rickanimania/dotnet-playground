# Deferred Execution & Materialization (ToList vs Filtro no Banco)

Este projeto faz parte da série **Dotnet Playground -- Performance
Cases**.

O objetivo é demonstrar, de forma prática e mensurável, o impacto da
materialização precoce (`ToList`) versus execução tardia com filtro
aplicado no banco de dados.

------------------------------------------------------------------------

## Problema

Em aplicações .NET, especialmente APIs, é comum encontrarmos código
assim:

``` csharp
var result = context.Entities
                    .ToList()
                    .Where(x => x.IsActive)
                    .ToList();
```

O código funciona.\
Compila.\
Passa nos testes.

Mas esconde um custo invisível:

-   Materializa todos os registros
-   Trafega dados desnecessários
-   Aumenta uso de memória
-   Aumenta tempo de resposta
-   Escala pior conforme o volume cresce

------------------------------------------------------------------------

## Hipótese

Filtrar no banco de dados antes da materialização reduz:

-   Volume de dados trafegados
-   Custo de IO
-   Uso de memória
-   Tempo total de execução

------------------------------------------------------------------------

## Experimento

O projeto executa dois cenários:

### Bad (Materialização Precoce)

``` sql
SELECT * FROM Records;
```

Filtragem realizada em memória após o `ToList()`.

------------------------------------------------------------------------

### Good (Execução Tardia / Filtro no Banco)

``` sql
SELECT * FROM Records WHERE IsActive = 1;
```

Apenas os registros necessários são materializados.

------------------------------------------------------------------------

## Arquitetura

O projeto segue separação por camadas:

-   Core → Regras e abstrações
-   Infrastructure → Implementações InMemory e SQLite (Dapper)
-   Console → Execução do experimento
-   Common → Utilitários compartilhados (Console UI e Reports)

Cada cenário roda:

-   Simulação InMemory
-   Execução real com SQLite + Dapper

------------------------------------------------------------------------

## Exemplo de Resultado (Stress Mode)

    INMEMORY
    Bad(ToList)  → 10000 registros → 6 ms
    Good(Filter) → 2500 registros  → 0.4 ms

    SQLITE (DAPPER)
    Bad(ToList)  → 10000 registros → 66 ms
    Good(Filter) → 2500 registros  → 2.8 ms

Redução superior a **90%** no tempo total.

------------------------------------------------------------------------

## Conclusão

O problema não está necessariamente:

-   No banco
-   Na infraestrutura
-   No servidor

Frequentemente está na forma como os dados são acessados.

A performance não quebra de uma vez.\
Ela degrada --- silenciosamente --- a partir de pequenas decisões.

------------------------------------------------------------------------

## Como Executar

1.  Restaurar pacotes NuGet
2.  Executar o projeto Console
3.  Alterar o `RunMode` entre `Demo` e `Stress` para testar volumes
    diferentes

------------------------------------------------------------------------

## Objetivo Educacional

Este projeto foi construído para:

-   Demonstrar custo estrutural de decisões simples
-   Servir como laboratório prático
-   Apoiar conteúdos técnicos publicados no LinkedIn
-   Reforçar autoridade técnica baseada em evidência prática

------------------------------------------------------------------------

## Próximos Cases

Este case faz parte de uma série maior abordando:

-   N+1 Queries
-   Materialização precoce
-   Any() vs Count()
-   IQueryable vs IEnumerable
-   Round-trips desnecessários

------------------------------------------------------------------------

Desenvolvido como parte do **Dotnet Playground -- Performance Series**.
