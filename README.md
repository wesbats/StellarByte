# Projeto StellarByte

O projeto StellarByte é uma aplicação que gerencia um E-commerce. Ele foi desenvolvido utilizando a plataforma ASP.NET 8.0 e segue uma arquitetura dividida em quatro projetos principais: `Application`, `Domain`, `Infrastructure`, e `StellarByte`.

## Estrutura do Projeto

- **Application**: Contém os serviços da aplicação, como autenticação, computadores, usuários e pedidos.

  - `AuthService.cs`: Serviço de autenticação.
  - `ComputerService.cs`: Serviço relacionado a operações com computadores.
  - `HashingService.cs`: Serviço para funções de hash.
  - `JwtService.cs`: Serviço para geração e validação de tokens JWT.
  - `OrderService.cs`: Serviço relacionado a operações com pedidos.
  - `UserService.cs`: Serviço relacionado a operações com usuários.

- **Domain**: Contém as entidades, exceções, mapeadores, opções, requisições, respostas e validadores do domínio da aplicação.

  - **Entities**: Contém as entidades principais da aplicação.
  - **Exceptions**: Contém exceções personalizadas.
  - **Mappers**: Mapeadores para transformação de dados.
  - **Options**: Configurações e opções do sistema.
  - **Request**: Classes de requisição.
  - **Responses**: Classes de resposta.
  - **Validator**: Validadores para as requisições.

- **Infrastructure**: Contém os repositórios para acesso a dados.

  - **Repositories**: Repositórios para as entidades principais (Computador, Usuário, Pedido).

- **StellarByte**: Projeto principal que contém a configuração da aplicação, middlewares, controladores e a lógica de inicialização.

  - `appsettings.Development.json`: Configurações específicas para o ambiente de desenvolvimento.
  - `appsettings.json`: Configurações principais da aplicação.
  - **Controllers**: Controladores para as entidades (Autenticação, Computadores, Pedidos, Usuários).
  - **Middlewares**: Middlewares customizados para a aplicação.
  - `Program.cs`: Arquivo principal de inicialização.
  - **Properties**: Propriedades do projeto.
  - `StellarByte.http`: Arquivo de exemplo para testes HTTP.
  - `Web.csproj`: Projeto principal da aplicação.

## Funcionalidades Principais

- **Autenticação (AuthController)**: Fornece endpoints para autenticação de usuários.

- **Computadores (ComputerController)**: Gerenciamento de computadores, incluindo listagem, criação, atualização e exclusão.

- **Pedidos (OrderController)**: Gerenciamento de pedidos, incluindo listagem, criação, atualização e exclusão, além de operações relacionadas aos itens do pedido e pagamento.

- **Usuários (UserController)**: Gerenciamento de usuários, incluindo listagem, criação, atualização e exclusão.

Este resumo fornece uma visão geral da estrutura e funcionalidades do projeto StellarByte. Para detalhes específicos, consulte o código-fonte fornecido.
