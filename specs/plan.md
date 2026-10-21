# Plano técnico

## Contexto

Implementa a especificação do gerador de senha, respeitando a constituição
do projeto.

## Arquitetura

- API HTTP em Minimal API, um arquivo de endpoints por recurso
- Camada de dados com EF Core e um repositório por agregado
- Endpoint só valida entrada, chama o serviço e traduz o resultado em resposta

## Decisões

- D-01: SQLite como banco de dados.
- D-02: Entity Framework Core para persistência, permitindo mapear a entidade de senha e realizar operações de armazenamento e consulta.
- D-03: GUID gerado pela aplicação para identificar cada senha armazenada, permitindo sua consulta posterior.
- D-04: Geração de senhas utilizando recursos nativos do .NET, evitando dependências externas e atendendo aos requisitos de tamanho mínimo, caracteres especiais e ausência de espaços em branco.
- D-05: Separação entre endpoints, serviços de aplicação e persistência, para manter as regras de negócio fora da camada HTTP e evitar acesso direto ao banco pelos endpoints.
- D-06: Testes unitários com xUnit para validar as regras de geração e consulta, e testes de integração para verificar o comportamento dos endpoints.
- D-07: HTTP 201 Created para a geração de uma senha, HTTP 200 OK para consultas bem-sucedidas, HTTP 404 Not Found quando o GUID não corresponder a uma senha armazenada e HTTP 400 com ProblemDetails quando houver falha.
- D-08: Não implementar criptografia das senhas armazenadas, autenticação ou login.

## Modelo de dados

StrongPassword

- Id (Guid), Password (string, not null)

## Contratos

- POST /api/v1/passwords - Sem parâmetro de entrada. Retorna guid e password
- GET /api/v1/passwords/{guid} - Retorna guid e password

## Riscos

- Crescimento da tabela de senhas armazenadas, fora de escopo.
