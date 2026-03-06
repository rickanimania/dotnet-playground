# Case --- Arquitetura de API: Template de API em Camadas

## Contexto

Quando falamos sobre arquitetura de APIs, é muito comum encontrar
diagramas explicando conceitos como:

-   Controllers
-   Services
-   Repositórios
-   Entities
-   DTOs
-   Separação de responsabilidades

No entanto, arquitetura só faz sentido quando ela está refletida em
código real.

Este case foi criado para demonstrar uma implementação simples e prática
de uma API utilizando arquitetura em camadas, sem complexidade
desnecessária e sem dependências externas como banco de dados ou
frameworks adicionais.

A ideia é fornecer uma estrutura mínima, organizada e fácil de entender,
que qualquer pessoa possa baixar, executar e estudar.

------------------------------------------------------------------------

## Objetivo

O objetivo deste exemplo é demonstrar:

-   Separação clara de responsabilidades entre as camadas
-   Uma estrutura de API simples e organizada
-   Como evitar regras de negócio dentro de controllers
-   Como isolar domínio e infraestrutura
-   Um ponto de partida limpo para novos projetos de API

Este projeto foi intencionalmente mantido pequeno e simples, com foco
apenas na estrutura arquitetural.

------------------------------------------------------------------------

## Estrutura da Arquitetura

A solução está dividida em quatro projetos:

LayeredApiTemplate.Api\
LayeredApiTemplate.Application\
LayeredApiTemplate.Domain\
LayeredApiTemplate.Infrastructure

Cada camada possui uma responsabilidade específica.

------------------------------------------------------------------------

## Camada Api

Responsável pela interface com o mundo externo.

Contém:

-   Controllers
-   Configuração de injeção de dependência
-   Configuração de inicialização da aplicação

A camada Api não deve conter regras de negócio ou lógica de acesso a
dados.

------------------------------------------------------------------------

## Camada Application

Responsável pelos casos de uso da aplicação e pela orquestração das
operações.

Contém:

-   Serviços de aplicação
-   DTOs
-   Interfaces utilizadas pela API

Essa camada coordena a comunicação entre a API e o domínio.

------------------------------------------------------------------------

## Camada Domain

Representa o núcleo do modelo de negócio.

Contém:

-   Entidades
-   Interfaces do domínio
-   Conceitos de negócio

A camada Domain não depende de nenhuma outra camada.

------------------------------------------------------------------------

## Camada Infrastructure

Responsável pelas implementações técnicas.

Contém:

-   Implementações de repositórios
-   Integrações externas
-   Lógica de acesso a dados

Neste exemplo foi utilizado um repositório em memória apenas para manter
o projeto simples e evitar dependência de banco de dados.

------------------------------------------------------------------------

## Regras de Dependência

As camadas seguem as seguintes regras de dependência:

Api -\> Application\
Application -\> Domain\
Infrastructure -\> Domain\
Domain -\> nenhuma dependência

Essa organização garante que o domínio permaneça isolado e independente.

------------------------------------------------------------------------

## Endpoints Disponíveis

A API possui dois endpoints simples apenas para demonstrar a arquitetura
funcionando.

### GET /api/examples

Retorna uma lista simples de exemplos.

Esse endpoint demonstra o fluxo completo entre as camadas:

Controller -\> Service -\> Repository -\> Entity -\> DTO

------------------------------------------------------------------------

### GET /api/health

Endpoint simples utilizado para verificar se a API está em execução.

Exemplo de resposta:

{ "status": "ok", "timestamp": "2026-03-06T12:00:00Z" }

------------------------------------------------------------------------

## Como Executar o Projeto

1.  Abra a solução no Visual Studio.
2.  Defina o projeto LayeredApiTemplate.Api como projeto de
    inicialização.
3.  Execute a aplicação.
4.  Utilize o Swagger ou faça chamadas diretas aos endpoints.

Exemplos:

GET /api/examples\
GET /api/health

------------------------------------------------------------------------

## Por que este exemplo é simples

Este projeto evita propositalmente adicionar complexidade como:

-   Banco de dados
-   ORMs
-   Autenticação
-   Frameworks de validação
-   Mensageria
-   CQRS
-   MediatR

A intenção é manter o foco na arquitetura e na organização do código.

------------------------------------------------------------------------

## Para quem este exemplo é útil

Este exemplo pode ser útil para:

-   Desenvolvedores que estão aprendendo arquitetura de APIs
-   Desenvolvedores estudando separação de responsabilidades
-   Desenvolvedores iniciando novos projetos backend
-   Times que desejam definir uma estrutura base para APIs

------------------------------------------------------------------------

## Conclusão

Uma API bem estruturada não precisa ser complexa.

Mas precisa ter:

-   Responsabilidades bem definidas
-   Camadas organizadas
-   Baixo acoplamento
-   Facilidade de manutenção

Boa arquitetura não significa adicionar mais camadas.

Significa tornar as responsabilidades claras.
