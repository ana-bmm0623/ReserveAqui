# ReserveAqui - Site de Reservas de Hotéis

## Descrição

O **ReserveAqui** é uma aplicação ASP.NET Framework para reserva de hotéis, simulando uma plataforma real de reservas de acomodações. Este projeto visa oferecer aos usuários uma experiência prática de reserva, onde eles podem se cadastrar, fazer login, visualizar hotéis e reservar quartos disponíveis.

## Recursos e Funcionalidades

- **Autenticação e Registro de Usuários**: Permite o registro de novos usuários e login com autenticação local ou com pelo menos dois autenticadores externos (ex: Google e Facebook) ou com confirmação por e-mail.
- **Telas Públicas**: A aplicação inclui duas páginas públicas para informações gerais.
- **Telas Privadas**: No mínimo seis telas privadas acessíveis somente para usuários autenticados.
- **Entidades e Relacionamentos**: Possui um mínimo de seis entidades, incluindo relacionamentos 1:N e N:N.
- **Validações**: Contém pelo menos cinco validações distintas (excluindo as de login) para garantir a integridade dos dados.
- **Persistência de Dados com Entity Framework**: A aplicação utiliza o Entity Framework para o gerenciamento de dados, permitindo geração de dados com Code First ou DB First.

## Entidades Principais

1. **Usuário** - Representa os usuários do sistema.
2. **Hotel** - Cada hotel pode ter vários quartos (1:N).
3. **Quarto** - Associado a um hotel e disponível para reservas.
4. **Reserva** - Associação entre usuário e quarto para gerenciar reservas.
5. **Serviço Adicional** - Serviços oferecidos aos hóspedes durante a estadia.
6. **Hospede** - Representa informações adicionais dos hóspedes do sistema.

**Relacionamentos**:
- Um hotel possui vários quartos (**1:N**).
- Um hóspede pode fazer várias reservas (**1:N**).
- Uma reserva pode incluir vários serviços adicionais (**N:N**).

## Tecnologias Utilizadas

- **ASP.NET Framework** - Framework principal para construção da aplicação.
- **Entity Framework** - ORM para persistência e gestão de dados.
- **C#** - Linguagem de programação principal do projeto.
- **Bootstrap** - Framework CSS para estilização.
- **Autenticação OAuth2** - Integração com Google e Facebook para login externo.

## Requisitos

- **Microsoft .NET Framework** 4.8 ou superior
- **SQL Server** (ou SQL Server Express) para o banco de dados
- **Visual Studio 2019** ou superior (ou qualquer IDE compatível com ASP.NET Framework)


   
