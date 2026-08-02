# 📍 Desafio ViaCEP - Sistema Gerenciador de Endereços

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-v3-38B2AC?style=for-the-badge&logo=tailwind-css&logoColor=white)
![Font Awesome](https://img.shields.io/badge/Font_Awesome-6-528DD7?style=for-the-badge&logo=font-awesome&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-LocalDB-CC292B?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

Aplicação web desenvolvida em **C# .NET 9** e **ASP.NET Core MVC** para o gerenciamento completo de endereços (CRUD), contando com autenticação segura de usuários (BCrypt), consulta automática de CEPs via integração proxy com a API pública do **[ViaCEP](https://viacep.com.br/)**, exportação para **CSV (Design Pattern Strategy)** e interface responsiva estilizada com **Tailwind CSS v3**, **Font Awesome 6** e tipografia **Google Fonts (Sora & DM Sans)**.

---

## 🎯 Objetivos do Projeto

Fornecer uma solução web robusta, arquiteturalmente organizada e amigável para o gerenciamento de endereços. A aplicação simplifica o cadastro ao consultar dados de localidade pelo CEP diretamente via backend C#, garante segurança das senhas com hashing salgado de alta entropia (BCrypt) e permite extensibilidade para novos formatos de exportação (PDF, Excel) aplicando o padrão de projeto **Strategy**.

---

## ✨ Funcionalidades Principais

- 🔐 **Autenticação & Cadastro de Usuários**:
  - Cadastro de novos usuários com validação forte de senha (mínimo de 8 caracteres, letra maiúscula, minúscula, número e caractere especial).
  - Criptografia segura de senhas via **BCrypt** (`BCrypt.Net-Next`).
  - Sessão mantida por **Cookies de Autenticação** com suporte a Login e Logout.

- 🏡 **Gerenciamento de Endereços (CRUD)**:
  - **Criar**: Adição de novos endereços vinculados exclusivamente à conta do usuário autenticado.
  - **Listar**: Tabela interativa com visualização dos endereços cadastrados.
  - **Editar**: Atualização de dados cadastrais de qualquer endereço do usuário.
  - **Excluir**: Remoção de endereços com confirmação.

- ⚡ **Integração ViaCEP via AJAX (Backend Proxy)**:
  - Ao digitar os 8 dígitos do CEP, uma requisição assíncrona JavaScript (`fetch`) consulta o endpoint da API C# (`/api/viacep/{cep}`), que por sua vez consome o serviço ViaCEP via `HttpClient`.
  - Preenchimento automático instantâneo dos campos *Logradouro*, *Bairro*, *Cidade* e *UF*, posicionando o foco diretamente no campo *Número*.

- 📝 **Entrada Manual & Edição de Campos**:
  - Caso um CEP não seja localizado ou necessite de ajustes (número, complemento, ponto de referência), todos os campos permanecem editáveis manualmente pelo usuário.

- 📥 **Exportação em CSV (Padrão Strategy)**:
  - Exportação dos registros do usuário para arquivo `.csv` baixado diretamente no navegador, construído através do padrão de projeto **Strategy** (`IExportadorStrategy` / `ExportadorContext`).

---

## 🏛️ Padrões de Arquitetura e Design Patterns

A aplicação segue rigorosamente as melhores práticas de desenvolvimento orientado a objetos e arquitetura em camadas:

- **Repository Pattern**: Abstração da camada de dados (`IUsuarioRepository`, `IEnderecoRepository`).
- **Unit of Work Pattern**: Gerenciamento transacional centralizado dos repositórios (`IUnitOfWork`).
- **Strategy Pattern**: Mecanismo flexível e extensível para exportação de dados (`IExportadorStrategy`, `CsvExportadorStrategy`, `ExportadorContext`).
- **Service Layer**: Isolamento de regras de negócio (`UsuarioService`, `EnderecoService`, `ViaCepService`).
- **Middleware Global de Exceções**: Tratamento centralizado de exceções customizadas (`ExceptionHandlingMiddleware`, `NotFoundException`, `ForbiddenException`).
- **Data Transfer Objects / ViewModels**: Isolamento das entidades de domínio através de ViewModels fortemente tipadas.

---

## 🛠️ Tecnologias Utilizadas

### **Backend**
- **C# / .NET 9.0** (ASP.NET Core MVC)
- **Entity Framework Core 9.0** (SQL Server Provider & Tools)
- **BCrypt.Net-Next** (Hashing seguro de senhas)
- **Swashbuckle / Swagger UI** (Documentação interativa de endpoints)

### **Frontend**
- **Razor Views** (`.cshtml`)
- **Tailwind CSS v3** (via CDN com extensão de temas no `tailwind.config`)
- **Font Awesome 6** (Ícones vetoriais profissionais)
- **Google Fonts** (**Sora** para títulos e **DM Sans** para textos)
- **JavaScript Vanilla (ES6+)** (Requisições assíncronas `fetch` para consulta ao ViaCEP)

### **Banco de Dados & DDL**
- **Microsoft SQL Server Express LocalDB**
- **Script SQL DDL Nativo** ([`script.sql`](script.sql) disponível na raiz e na pasta do backend)

---

## 📁 Estrutura do Repositório

```text
DesafioViaCep/
├── script.sql                             # Script SQL DDL nativo para criação das tabelas
├── README.md                              # Documentação oficial do repositório
└── Backend/
    └── ViaCep/
        ├── Controllers/                   # Controllers MVC (Account, Enderecos, Home)
        ├── Data/                          # DbContext do EF Core (ApplicationDbContext)
        ├── Exceptions/                    # Exceções customizadas (NotFoundException, ForbiddenException)
        ├── Middlewares/                   # Middleware global (ExceptionHandlingMiddleware)
        ├── Migrations/                    # Histórico de Migrações do EF Core
        ├── Models/                        # Entidades do Domínio (Usuario, Endereco)
        ├── Properties/                    # Configurações de inicialização (launchSettings.json)
        ├── Repositories/                  # Interfaces e Implementações dos Repositórios e UnitOfWork
        ├── Services/                      # Regras de Negócio e Serviços (ViaCEP, Usuario, Endereco)
        │   └── Exportacao/                # Implementação do Padrão Strategy para Exportação
        ├── ViewModels/                    # ViewModels de Login, Register, Endereco
        ├── Views/                         # Views Razor (Account, Enderecos, Shared/_Layout)
        ├── appsettings.json               # Configurações base da aplicação
        ├── appsettings.Development.json   # Configurações de ambiente de desenvolvimento local
        ├── script.sql                     # Script SQL DDL nativo copia
        └── ViaCep.csproj                  # Arquivo de projeto .NET 9
```

---

## 🚀 Como Executar o Projeto

### **Pré-requisitos**
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) instalado.
- SQL Server ou SQL Server LocalDB (`(localdb)\mssqllocaldb`).

---

### **1. Clonar o Repositório**

```bash
git clone https://github.com/flaviofilho001/DesafioViaCep.git
cd DesafioViaCep/Backend/ViaCep
```

---

### **2. Criar o Banco de Dados e Executar**

Você pode criar a estrutura do banco de dados de **duas formas**:

#### **Opção A: Via Entity Framework Core Migrations (Recomendado)**
Execute o comando abaixo no terminal dentro de `Backend/ViaCep`:

```bash
# Executa a migration e cria o banco ViaCepDb no LocalDB
dotnet ef database update

# Executa o projeto C#
dotnet run
```

#### **Opção B: Via Script SQL DDL Nativo**
Execute o arquivo [`script.sql`](script.sql) no SQL Server Management Studio (SSMS) ou VS Server Explorer, e em seguida execute:

```bash
dotnet run
```

---

### **3. Acessar a Aplicação**

Assim que a aplicação iniciar, o navegador abrirá automaticamente a interface do site em:
- **Aplicação Web**: `https://localhost:7070` ou `http://localhost:5043`
- **Documentação Swagger UI**: `https://localhost:7070/swagger`

---

## 🔌 Endpoints da API REST

| Método | Endpoint | Descrição | Requer Autenticação |
| :--- | :--- | :--- | :---: |
| `GET` | `/Account/Login` | Exibe a tela de login | Não |
| `POST` | `/Account/Login` | Autentica o usuário no sistema | Não |
| `GET` | `/Account/Register` | Exibe a tela de cadastro | Não |
| `POST` | `/Account/Register` | Cadastra um novo usuário com senha criptografada | Não |
| `GET` | `/Enderecos` | Lista os endereços do usuário autenticado | **Sim** |
| `GET` | `/Enderecos/Criar` | Exibe o formulário de cadastro de endereço | **Sim** |
| `POST` | `/Enderecos/Criar` | Cadastra um novo endereço no sistema | **Sim** |
| `GET` | `/Enderecos/Editar/{id}` | Exibe a tela de edição de endereço | **Sim** |
| `POST` | `/Enderecos/Editar/{id}` | Salva as alterações do endereço | **Sim** |
| `POST` | `/Enderecos/Excluir/{id}`| Remove um endereço do usuário | **Sim** |
| `GET` | `/Enderecos/ExportarCsv` | Baixa o arquivo `.csv` (via Strategy Pattern) | **Sim** |
| `GET` | `/api/viacep/{cep}` | Endpoint proxy para consulta ao ViaCEP | Não (`[AllowAnonymous]`) |

---

## 📊 Atendimento aos Critérios de Avaliação

| Critério de Avaliação | Como foi Atendido no Projeto |
| :--- | :--- |
| **1. Qualidade do Código** *(Legibilidade, Estrutura e Organização)* | Arquitetura limpa em camadas (`Controllers`, `Services`, `Repositories`, `Models`, `ViewModels`, `Middlewares`). Nomenclatura C# padronizada, ViewModels fortemente tipadas isolando as entidades e histórico de commits semântico (*Conventional Commits*). |
| **2. Boas Práticas, Segurança e Design Patterns** | Aplicação do **Strategy Pattern** para exportação flexível, **Repository & Unit of Work Patterns** para persistência, **Backend Proxy Pattern** para o ViaCEP, criptografia de senhas com **BCrypt**, proteção **Anti-Forgery Token (CSRF)** e **Middleware Global de Exceções**. |
| **3. Funcionalidade do Sistema** | CRUD completo de endereços por usuário, consulta automática de CEP via AJAX com preenchimento instantâneo, ajuste manual de campos, exportação CSV e entrega dos scripts DDL nativos ([`script.sql`](script.sql)). |
| **4. Design e Usabilidade (UX/UI)** | Interface responsiva estilizada com **Tailwind CSS v3**, ícones vetoriais **Font Awesome 6**, tipografia **Google Fonts** (*Sora* & *DM Sans*), indicadores visuais de carregamento em tempo real e mensagens de feedback (*Toast/TempData*). |

---

## 📄 Licença

Este projeto foi desenvolvido para fins de teste técnico e avaliação de desenvolvimento de software em C# .NET.
