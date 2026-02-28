# dotnet-playground

dotnet-playground é um laboratório técnico público com casos reais, controlados e reproduzíveis no ecossistema .NET.

O objetivo deste projeto é transformar conceitos discutidos em conteúdos técnicos (especialmente sobre backend e performance) em exemplos práticos, executáveis e organizados.

Aqui você encontrará estudos aplicados sobre:

- Performance
- Acesso a dados
- Arquitetura
- Observabilidade
- Organização de código
- Comparação entre abordagens inadequadas e otimizadas

---

## Propósito

Este repositório não é apenas um conjunto de exemplos isolados.

Ele foi estruturado para:

- Demonstrar problemas reais enfrentados em projetos .NET
- Comparar abordagens inadequadas versus abordagens otimizadas
- Explicar decisões técnicas com clareza
- Servir como referência prática para desenvolvedores backend
- Documentar evolução técnica de forma pública e organizada

Cada caso busca responder três perguntas:

1. Qual é o problema?
2. Por que ele acontece?
3. Como resolver de forma técnica e sustentável?

---

## Estrutura do Projeto

O projeto está organizado por categorias dentro da pasta `cases/`:

- `performance/` → Estudos sobre otimização, N+1, round-trips, materialização precoce, latência e impacto de consultas mal estruturadas.
- `data-access/` → Padrões e boas práticas com Dapper, EF Core e consultas SQL.
- `architecture/` → Estruturação de projetos, separação de responsabilidades e organização de camadas.
- `observability/` → Logging estruturado, diagnósticos e rastreabilidade.

Pastas complementares:

- `shared/` → Código reutilizável entre casos (ex: utilitários de console).
- `documentation/` → Decisões técnicas e material de apoio.
- `tools/` → Scripts auxiliares.
- `samples/` → Exemplos menores e experimentais.

---

## Caso Atual

### performance / n-plus-one-round-trips

Primeiro caso publicado no repositório.

Demonstra, de forma prática:

- Impacto do padrão N+1
- Excesso de round-trips ao banco de dados
- Diferença entre múltiplas consultas individuais e consulta consolidada
- Comparação entre simulação em memória e execução real com SQLite + Dapper

O caso é dividido em:

- Core (regra de negócio e cenários)
- Console (aplicação executável)
- Common (componentes reutilizáveis)

---

## Como Executar um Caso

Cada caso possui seu próprio projeto executável (Console ou API).

Exemplo:

```bash
dotnet run --project src/NPlusOneRoundTrips.Console
```

Os detalhes específicos de execução estão documentados no README de cada caso.

---

## Roadmap Inicial

Versão 1.0.0:

- Estrutura base do projeto
- Organização por categorias
- Primeira feature publicada (N+1 e Round Trips)
- Documentação padrão definida

Próximos passos previstos:

- Novos casos em performance
- Casos voltados a arquitetura
- Estudos sobre paralelismo e concorrência
- Casos sobre UX de terminal (CLI)

---

## Sobre

Este projeto está alinhado aos conteúdos técnicos que compartilho no LinkedIn, com foco em desenvolvimento backend profissional, análise de gargalos e boas práticas aplicadas.

O objetivo é manter um portfólio técnico evolutivo, transparente e baseado em problemas reais.

---

## Licença

Este projeto está licenciado sob os termos descritos no arquivo LICENSE.
