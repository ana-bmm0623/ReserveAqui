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


 ## Configuração e Execução

### 1. Clonar o Repositório

```bash
git clone https://github.com/seu-usuario/ReserveAqui.git
cd ReserveAqui
```

### 2. Configurar o Banco de Dados
- No Visual Studio:

- Abra o projeto ReserveAqui.sln.

- No Package Manager Console, execute os seguintes comandos para criar o banco de dados e aplicar as migrações:

```bash
Copiar código
Update-Database
```

### 3. Configurar o Envio de E-mails (Opcional)
- Para habilitar o envio de e-mails de confirmação e redefinição de senha, configure o arquivo Web.config com as credenciais do SMTP. Exemplo:

```xml
<system.net>
  <mailSettings>
    <smtp from="seu-email@gmail.com">
      <network host="smtp.gmail.com" port="587" userName="seu-email@gmail.com" password="sua-senha" enableSsl="true" />
    </smtp>
  </mailSettings>
</system.net>
```
### 4. Executar o Projeto
- No Visual Studio, selecione IIS Express para iniciar o servidor.
O navegador abrirá automaticamente a aplicação em http://localhost:porta.

### 5. Acessar a Aplicação
- Usuário Comum
-> Registre-se na aplicação para começar a fazer reservas.

- Administrador
-> Para acessar a área administrativa, um usuário deve ter a função Admin. Isso pode ser configurado manualmente no banco de dados ou através da implementação de um código de inicialização para adicionar a função Admin.

- Estrutura de Pastas
     * Controllers: Contém os controladores da aplicação, responsáveis por processar requisições e chamar as views.

   * Models: Contém os modelos que representam as entidades do sistema.

   * Views: Contém as views que são renderizadas para o usuário.
  
   * Content: Contém arquivos CSS e de estilo.
     
   * Scripts: Contém os scripts JavaScript e bibliotecas usadas na aplicação.


## Contribuição
Faça um fork deste repositório.

Crie uma nova branch para suas alterações:

```bash

git checkout -b minha-nova-feature
Commit suas alterações:
```
```bash

git commit -m 'Adicionar nova feature'
Envie para a branch:
```
``bash
git push origin minha-nova-feature
Abra um Pull Request.
``
## Licença
- Este projeto é licenciado sob a Licença MIT - consulte o arquivo LICENSE para mais detalhes.

   
