# Commit Guidelines --- dotnet-playground

Este documento define o padrão oficial de commits do projeto
`dotnet-playground`.

O objetivo é manter clareza, consistência e profissionalismo na evolução
do repositório.

------------------------------------------------------------------------

## Formato do Commit

Todos os commits devem seguir o formato:

tipo: descrição clara do que foi feito

Exemplo:

feat: adiciona primeiro caso sobre n+1 e round-trips

------------------------------------------------------------------------

## Tipos Permitidos

### feat

Utilizado para novas funcionalidades ou novos casos.

Exemplos: feat: adiciona primeiro caso sobre n+1 e round-trips feat:
cria estrutura base para casos de performance

------------------------------------------------------------------------

### fix

Utilizado para correção de bugs ou comportamentos incorretos.

Exemplos: fix: corrige materialização precoce no exemplo good fix:
ajusta cálculo de tempo no benchmark

------------------------------------------------------------------------

### refactor

Utilizado para melhorias internas sem alterar comportamento externo.

Exemplos: refactor: reorganiza estrutura de pastas do caso performance
refactor: simplifica lógica de consulta no exemplo good

------------------------------------------------------------------------

### docs

Utilizado para alterações exclusivamente na documentação.

Exemplos: docs: atualiza readme com instruções de execução docs:
adiciona explicação sobre round-trips

------------------------------------------------------------------------

### chore

Utilizado para configurações, ajustes de ambiente ou manutenção geral.

Exemplos: chore: configura gitignore chore: adiciona estrutura inicial
do projeto

------------------------------------------------------------------------

### test

Utilizado para criação ou atualização de testes.

Exemplos: test: adiciona testes para cenário n+1 test: cobre caso de
erro na consulta otimizada

------------------------------------------------------------------------

## Regras Gerais

-   Utilizar sempre letras minúsculas
-   Não finalizar mensagem com ponto
-   Manter descrição clara e objetiva
-   Evitar mensagens genéricas
-   Evitar emojis
-   Um commit deve representar uma única intenção lógica

------------------------------------------------------------------------

## Exemplos Incorretos

ajustes update mudanças teste subindo projeto

------------------------------------------------------------------------

## Exemplos Corretos

feat: adiciona caso de materialização desnecessária fix: corrige
problema de paginação no exemplo data-access refactor: melhora
organização da pasta shared docs: adiciona roadmap da versão 1.0.0

------------------------------------------------------------------------

## Objetivo do Padrão

Manter histórico organizado facilita:

-   Leitura da evolução do projeto
-   Compreensão das mudanças realizadas
-   Profissionalização do repositório
-   Manutenção futura

Consistência é mais importante que complexidade.
