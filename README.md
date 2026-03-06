# dotnet-playground

dotnet-playground é um laboratório técnico público com casos reais,
controlados e reproduzíveis no ecossistema .NET.

O objetivo deste projeto é transformar conceitos discutidos em conteúdos
técnicos (especialmente sobre backend, arquitetura e performance) em
exemplos práticos, executáveis e organizados.

Aqui você encontrará estudos aplicados sobre:

-   Performance
-   Acesso a dados
-   Arquitetura de software
-   Observabilidade
-   Organização de código
-   Comparação entre abordagens inadequadas e otimizadas

------------------------------------------------------------------------

## Propósito

Este repositório não é apenas um conjunto de exemplos isolados.

Ele foi estruturado para:

-   Demonstrar problemas reais enfrentados em projetos .NET
-   Comparar abordagens inadequadas versus abordagens otimizadas
-   Explicar decisões técnicas com clareza
-   Servir como referência prática para desenvolvedores backend
-   Documentar evolução técnica de forma pública e organizada

Cada caso busca responder três perguntas:

1.  Qual é o problema?
2.  Por que ele acontece?
3.  Como resolver de forma técnica e sustentável?

------------------------------------------------------------------------

## Estrutura do Projeto

O projeto está organizado por categorias dentro da pasta `cases/`.

### Categorias principais

-   `performance/` → Estudos sobre otimização, N+1, round-trips,
    materialização precoce, latência e impacto de consultas mal
    estruturadas.

-   `data-access/` → Padrões e boas práticas com Dapper, EF Core e
    consultas SQL.

-   `architecture/` → Estruturação de APIs, separação de
    responsabilidades, organização de camadas e decisões arquiteturais.

-   `observability/` → Logging estruturado, diagnósticos e
    rastreabilidade.

### Pastas complementares

-   `shared/` → Código reutilizável entre casos
-   `documentation/` → Decisões técnicas e material de apoio
-   `tools/` → Scripts auxiliares
-   `samples/` → Exemplos menores e experimentais

------------------------------------------------------------------------

## Casos Publicados

### performance / n-plus-one-round-trips

Primeiro caso publicado no repositório.

Demonstra, de forma prática:

-   Impacto do padrão N+1
-   Excesso de round-trips ao banco de dados
-   Diferença entre múltiplas consultas individuais e consulta
    consolidada
-   Comparação entre simulação em memória e execução real com SQLite +
    Dapper

------------------------------------------------------------------------

### performance / deferred-execution-materialization

Segundo caso publicado no repositório.

Demonstra, de forma prática:

-   Impacto da materialização precoce (`ToList`)
-   Diferença entre filtrar em memória versus filtrar no banco
-   Custo invisível de trafegar e materializar dados desnecessários
-   Comparação entre execução InMemory e SQLite com Dapper

Este caso evidencia como pequenas decisões no acesso a dados podem gerar
diferenças significativas de desempenho conforme o volume cresce.

------------------------------------------------------------------------

### architecture / layered-api-template

Primeiro caso publicado na categoria de arquitetura.

Este exemplo demonstra uma estrutura simples de API organizada em
camadas, com foco em separação clara de responsabilidades e baixo
acoplamento.

A solução foi estruturada em quatro projetos:

-   Api
-   Application
-   Domain
-   Infrastructure

Cada camada possui uma responsabilidade específica e segue regras de
dependência que evitam acoplamento indevido entre os projetos.

Para manter o exemplo simples e focado apenas na arquitetura:

-   Não foi utilizado banco de dados
-   Foi implementado um repositório em memória
-   Foram criados endpoints mínimos apenas para demonstrar o fluxo da
    aplicação

Endpoints implementados:

-   `GET /api/examples`
-   `GET /api/health`

O objetivo deste case é servir como referência simples de organização de
uma API utilizando arquitetura em camadas.

------------------------------------------------------------------------

## Arquitetura dos Casos

Cada caso segue um padrão consistente de organização:

-   Core → Regra de negócio, contratos e cenários
-   Infrastructure → Implementações técnicas (InMemory, SQLite, etc.)
-   Console ou API → Aplicação executável para reproduzir o experimento
-   Shared/Common → Componentes reutilizáveis entre casos

Esse padrão facilita:

-   Evolução incremental
-   Comparação entre abordagens
-   Clareza para desenvolvedores em início de carreira
-   Refatorações futuras sem quebra estrutural

------------------------------------------------------------------------

## Como Executar um Caso

Cada caso possui seu próprio projeto executável (Console ou API).

Exemplo:

``` bash
dotnet run --project src/NPlusOneRoundTrips.Console
```

ou

``` bash
dotnet run --project src/DeferredExecutionMaterialization.Console
```

Para casos baseados em API, basta executar o projeto correspondente e
utilizar o Swagger ou realizar chamadas HTTP diretamente nos endpoints.

Os detalhes específicos de execução estão documentados no README de cada
caso.

------------------------------------------------------------------------

## Roadmap Inicial

Versão 1.0.0:

-   Estrutura base do projeto
-   Organização por categorias
-   Case 1: N+1 e Round Trips
-   Case 2: Deferred Execution e Materialização
-   Case 3: Estrutura de API em camadas
-   Documentação padrão definida

Próximos passos previstos:

-   Novos casos em performance
-   Estudos sobre concorrência e paralelismo
-   Casos sobre arquitetura backend
-   Experimentos com otimização de acesso a dados
-   Casos voltados para observabilidade

------------------------------------------------------------------------

## Sobre

Este projeto está alinhado aos conteúdos técnicos que compartilho no
LinkedIn, com foco em desenvolvimento backend profissional, análise de
gargalos, arquitetura de software e boas práticas aplicadas.

O objetivo é manter um portfólio técnico evolutivo, transparente e
baseado em problemas reais encontrados no desenvolvimento de sistemas.

------------------------------------------------------------------------

## Licença

Este projeto está licenciado sob os termos descritos no arquivo LICENSE.
