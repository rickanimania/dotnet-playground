# Dotnet Playground — Performance Cases

Este repositório tem como objetivo demonstrar, de forma prática e mensurável, como pequenas decisões de código podem gerar impactos significativos de performance conforme o sistema escala.

Cada case segue o mesmo padrão arquitetural:

- Core
- Infrastructure
- Console
- Saída padronizada via DotnetPlayground.Common

O foco não é micro-otimização.
É clareza semântica, escalabilidade e entendimento do custo invisível das escolhas.

---

# Estrutura do Repositório

```
cases
 └─ performance
     ├─ n-plus-one-round-trips
     ├─ deferred-execution-materialization
     └─ any-vs-count
```

Cada case contém:

```
src
 ├─ <Case>.Core
 ├─ <Case>.Infrastructure
 └─ <Case>.Console
```

---

# Cases Implementados

## Case 1 — N+1 e Round-Trips

Demonstra o impacto de múltiplas idas ao banco.

Comparação entre:

- Execução com múltiplas queries
- Execução consolidada

Evidencia o custo de round-trips desnecessários.

---

## Case 2 — Materialização Precoce vs Execução Tardia

Compara:

- Uso antecipado de `ToList()`
- Execução tardia (deferred execution)

Demonstra o custo invisível de materialização precoce e múltiplas enumerações.

Executado em:

- InMemory
- SQLite + Dapper

---

## Case 3 — Any() vs Count() e custo invisível de existência

Compara:

- `Count() > 0`
- `Any()`

### Problema

Código comum encontrado:

```csharp
if (collection.Count(x => x.IsMatch) > 0)
{
    // ...
}
```

Quando o objetivo é apenas verificar existência.

### Hipótese

- `Count(predicate)` pode forçar varredura completa.
- `Any(predicate)` pode interromper na primeira ocorrência.
- Impacto direto em memória.
- Impacto indireto em banco (agregação desnecessária).

### Experimento

Executado em:

- InMemory
- SQLite + Dapper

Queries utilizadas:

```sql
-- Bad
SELECT COUNT(1) FROM Records WHERE IsMatch = 1;

-- Good
SELECT 1 FROM Records WHERE IsMatch = 1 LIMIT 1;
```

### Métricas Exibidas

- DATASET → total de registros
- AVALIADOS → quantidade efetivamente percorrida (quando mensurável)
- TEMPO (MS) → tempo total de execução

No SQLite, AVALIADOS é exibido como `N/A`, pois o plano de execução é decidido pelo engine.

### Conclusão

Quando o objetivo é verificar existência, `Any()` é:

- Semanticamente correto
- Mais expressivo
- Geralmente mais eficiente

O gargalo muitas vezes não está na infraestrutura.

Está na escolha do método.

---

# Como Executar

Exemplo (Case 3):

```bash
dotnet run --project cases/performance/any-vs-count/src/AnyVsCount.Console
```

Altere o modo no `Program.cs`:

```csharp
const RunMode mode = RunMode.Demo;
```

ou

```csharp
const RunMode mode = RunMode.Stress;
```

---

# Objetivo Educacional

Este playground foi criado para:

- Evidenciar custos invisíveis de implementação
- Demonstrar impacto real de decisões semânticas
- Servir como material de estudo e referência
- Apoiar discussões técnicas baseadas em dados

Performance não começa na infraestrutura.

Começa na escolha do método.