# Gerador de Senhas Fortes

## Problema

- Criar e armazenar senhas fortes pode ser trabalhoso quando é necessário garantir um padrão mínimo de caracteres e recuperar posteriormente uma senha específica.

## Objetivo

- Permitir que uma pessoa gere uma senha forte de forma simples e rápida seguindo critérios predefinidos, receba a senha e um GUID para identificá-la e consulte posteriormente a senha armazenada utilizando esse identificador.

## Usuários

- Pessoa que solicita a geração e consulta a senha.

## Histórias

- Como usuário, quero gerar uma senha forte.
- Como usuário, quero poder recuperar a senha gerada anteriormente.

## Requisitos funcionais

- RF-01 - O sistema recebe a solicitação da geração de uma senha.
- RF-02 - A senha deve ter pelo menos 16 caracteres, contendo caracteres especiais e não pode incluir espaços em branco.
- RF-03 - Toda senha gerada é armazenada no banco.
- RF-04: Após gerar e armazenar a senha, o sistema retorna ao solicitante a senha gerada e o GUID associado.
- RF-05: O sistema permite consultar uma senha armazenada por meio do GUID recebido anteriormente.
- RF-06: Ao consultar um GUID existente, o sistema retorna a senha associada a ele.
- RF-07: Quando o GUID informado não corresponde a uma senha armazenada, o sistema retorna HTTP 404 Not Found.
- RF-08: As senhas geradas permanecem disponíveis para consulta após o reinício da aplicação.

## Regras de negócio

- RN-01: Toda senha gerada deve ter pelo menos 16 caracteres.
- RN-02: Toda senha gerada deve conter pelo menos um caractere especial.
- RN-03: Nenhuma senha gerada pode conter espaços em branco.
- RN-04: Cada senha gerada deve ser associada a um GUID único.
- RN-05: Toda senha gerada deve ser persistida no banco de dados antes de ser retornada ao solicitante.
- RN-06: O GUID deve identificar exclusivamente a senha armazenada e permitir sua consulta posterior.
- RN-07: A consulta por GUID deve retornar somente a senha associada ao identificador informado.
- RN-08: Um GUID que não esteja associado a uma senha armazenada não pode retornar uma senha.
- RN-09: As senhas geradas devem permanecer armazenadas e disponíveis para consulta após o reinício da aplicação.
- RN-10: A senha retornada na consulta deve corresponder exatamente à senha originalmente gerada e armazenada.

## Casos de borda

- GUID inexistente na consulta: retornar HTTP 404 e informar que a senha não foi encontrada.
- GUID em formato inválido: retornar HTTP 400 com ProblemDetails.
- Senha gerada com menos de 16 caracteres: não permitir que seja armazenada ou retornada.
- Senha gerada sem caracteres especiais: não permitir que seja armazenada ou retornada.
- Senha gerada contendo espaços em branco: não permitir que seja armazenada ou retornada.
- Falha ao consultar o banco de dados: retornar um erro apropriado, sem apresentar uma senha incorreta.

## Fora de escopo

- Cadastro e login de usuários.
- Edição de uma senha já gerada.
- Exclusão de senha já gerada.

## Critérios de aceite

- Criar senha com pelo menos 16 caracteres com caracteres especiais e sem espaços em branco.
- Retornar a senha com base na consulta do GUID.
- Retornar senha e GUID ao solicitar criar uma nova senha.
- Ao consultar um GUID inexistente, o sistema retorna HTTP 404 Not Found.
