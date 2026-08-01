# 📍 Desafio CRUD de Endereços com Integração ViaCEP

![.NET](https://img.shields.io/badge/.NET-net9.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![REST API](https://img.shields.io/badge/API-ViaCEP-005C9E?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

Aplicação web desenvolvida em **C#** que permite o gerenciamento completo de endereços (CRUD), contando com autenticação de usuário, busca automática de dados de endereço via integração com a API pública do **[ViaCEP](https://viacep.com.br/)** e exportação dos registros para arquivo **CSV**.

---

## 🎯 Objetivos do Projeto

O objetivo principal deste desafio é fornecer uma solução web robusta, segura e amigável para o gerenciamento de endereços residenciais ou comerciais. A aplicação simplifica o cadastro ao consultar dados de localidade pelo CEP e garante a portabilidade dos dados através da exportação em lote.

---

## ✨ Funcionalidades Principais

- 🔐 **Autenticação de Usuários**:
  - Cadastro de novos usuários.
  - Tela de Login segura com controle de sessão / autenticação JWT ou Cookies.
  
- 🏡 **Gerenciamento de Endereços (CRUD)**:
  - **Criar**: Adicionar novos endereços vinculados à conta do usuário.
  - **Visualizar**: Listagem interativa dos endereços cadastrados.
  - **Editar**: Atualização de dados cadastrais de qualquer endereço existente.
  - **Excluir**: Remoção de endereços com confirmação.

- 🔍 **Integração com API ViaCEP**:
  - Busca automática de dados de endereço (*Logradouro*, *Bairro*, *Cidade*, *UF*) ao digitar um **CEP** válido.
  - Preenchimento automático dos campos do formulário para agilizar o cadastro.

- 📝 **Entrada Manual de Dados**:
  - Permissão para inserir ou ajustar manualmente os campos caso o CEP não seja localizado ou necessite de complemento (ex: número, ponto de referência).

- 📊 **Exportação em CSV**:
  - Exportação dos endereços salvos pelo usuário para um arquivo no formato `.csv` para download imediato.

---

## 🛠️ Tecnologias Utilizadas

### **Backend**
- **Linguagem**: C# (.NET 9)
- **Framework**: ASP.NET Core Web API / MVC
- **Persistência de Dados**: Entity Framework Core
- **Integração de APIs**: `HttpClient` / Services para consumo REST da API ViaCEP
- **Exportação de Dados**: Geração de arquivos CSV (`CsvHelper` ou criação de buffer de texto nativo)

### **Frontend**
- **Interface**: Web (HTML5, CSS3, JavaScript)
- **Consumo de APIs**: Axios

---

## 📁 Estrutura do Repositório


---

## 🚀 Como Executar o Projeto

### **Pré-requisitos**
Antes de iniciar, certifique-se de ter instalado em sua máquina:
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) ou superior.
- Um editor de código de sua preferência (ex: [VS Code](https://code.visualstudio.com/), [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/)).
- Git instalado.

---

### **1. Clonar o Repositório**

```bash
git clone https://github.com/seu-usuario/DesafioViaCep.git
cd DesafioViaCep
```

---

### **2. Executar o Backend (C#)**

Navegue até a pasta do projeto backend e execute os comandos:

```bash
cd Backend/ViaCep

# Restaurar as dependências do projeto
dotnet restore

# Executar a aplicação backend
dotnet run
```

O servidor backend estará rodando por padrão nas portas indicadas no terminal (ex: `https://localhost:7198` ou `http://localhost:5270`).

---

### **3. Executar o Frontend**

Navegue até a pasta do frontend e inicie o servidor web de desenvolvimento ou abra o arquivo principal conforme a tecnologia utilizada.

```bash
cd ../../Frontend
# Execute os comandos necessários para rodar o frontend (ex: npm run dev ou live-server)
```

---

## 🔌 Endpoints Principais (API)

| Método | Endpoint | Descrição |
| :--- | :--- | :--- |
| `POST` | `/api/auth/login` | Realiza o login do usuário |
| `POST` | `/api/auth/register` | Cadastra um novo usuário |
| `GET` | `/api/enderecos` | Retorna a lista de endereços do usuário autenticado |
| `POST` | `/api/enderecos` | Cadastra um novo endereço |
| `PUT` | `/api/enderecos/{id}` | Atualiza um endereço existente |
| `DELETE` | `/api/enderecos/{id}` | Remove um endereço |
| `GET` | `/api/enderecos/exportar-csv` | Baixa o arquivo `.csv` com os endereços salvos |
| `GET` | `/api/viacep/{cep}` | Consulta os dados de um endereço pelo CEP no ViaCEP |

---

## 📄 Licença

Este projeto está sob a licença MIT. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.
