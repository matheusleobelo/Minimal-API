# API de Gestão de Veículos

Esta é uma API desenvolvida em .NET 8 para gestão de veículos, com autenticação JWT, testes automatizados e suporte a Swagger.

**Créditos ao projeto original:** [DIO Minimal API](https://github.com/digitalinnovationone/minimal-api/tree/main/Api)

---

## Funcionalidades

- CRUD de administradores e veículos
- Login e autenticação via JWT
- Controle de perfis e roles (ex.: Adm, Editor)
- Validação de dados via DTOs
- Paginação e filtros em listagem de veículos
- Documentação interativa via Swagger

---

## Tecnologias utilizadas

- .NET 8 Minimal API
- Entity Framework Core (MySQL / InMemory)
- JWT para autenticação
- MSTest para testes unitários e de integração
- WebApplicationFactory e HttpClient para testes de API
- JsonSerializer para manipulação de JSON
- Swagger/OpenAPI para documentação

---

## Testes

- Mock services (`AdministradorServicoMock`, `VeiculoServicoMock`)
- Testes de login e CRUD
- Configuração `ClassInit` e `ClassCleanup` para setup e teardown
- Testes de autenticação JWT

---

## Como executar

1. Clone o repositório
2. Configure a connection string em `appsettings.json`
3. Execute migrations (caso use banco real)
4. Rode a API:  
```bash
dotnet run
